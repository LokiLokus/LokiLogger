using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Fody;
using LokiLogger.Model;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace LokiLogger.Fody {
	public class ModuleWeaver :
		BaseModuleWeaver{
		
		private static readonly MethodInfo _lokiInvokeMethod;
		private static readonly MethodInfo _lokiReturnMethod;

		static ModuleWeaver()
		{
			_lokiInvokeMethod = typeof( Loki )
				.GetMethods()
				.Single(x => x.Name == nameof(Loki.WriteInvoke));
			_lokiReturnMethod = typeof( Loki )
				.GetMethods()
				.Single(x => x.Name == nameof(Loki.WriteReturn));
		}
		
		public override void Execute()
		{
			List<MethodDefinition> methodsToAdd = new List<MethodDefinition>();
			string methodString = "";
			ModuleDefinition.Assembly.MainModule.Types
				.Where(x => x.IsClass).Select(x => x.Methods).ToList().ForEach(x =>
				{
					x.ToList().ForEach(y =>
					{
						CustomAttribute custAttr = y.CustomAttributes.FirstOrDefault(t =>
							t.Constructor.FullName.Contains("System.Void LokiLogger.LokiAttribute"));
						if (y.CustomAttributes.Select(d => d.Constructor).Select(d => d.FullName).Any(t => t.Contains("System.Void LokiLogger.LokiAttribute")))
						{
							methodsToAdd.Add(y);
						}
					});
				});
			
			
			methodString += methodsToAdd.Count;
			
			foreach (MethodDefinition methodDefinition in methodsToAdd)
			{
				ProcessMethod(methodDefinition);
				
			}
		}
		
		private void ProcessMethod( MethodDefinition method )
		{
			
			
			WeaveInvoke(method);
			//var returnIns = method.Body.Instructions[method.Body.Instructions.Count-2];
			//Instruction.Create(OpCodes.Call, ModuleDefinition.ImportReference( _lokiReturnMethod));



		}

		private void WeaveReturn(MethodDefinition method)
		{
			ILProcessor processor = method.Body.GetILProcessor();
			List<Instruction> returnInstructions = method.Body.Instructions.Where(instruction => instruction.OpCode == OpCodes.Ret).ToList();
			foreach (var returnInstruction in returnInstructions)
			{
				Instruction loadNameInstruction = processor.Create(OpCodes.Ldstr, name);
				Instruction callExitReference = processor.Create(OpCodes.Call, exitReference);

				processor.InsertBefore(returnInstruction, loadNameInstruction);
				processor.InsertAfter(loadNameInstruction, callExitReference);
			}
			
		}
		public List<Instruction> LokiReturnInstructions(MethodDefinition method)
		{
			
			throw new NotImplementedException();
		}

		
		private void WeaveInvoke(MethodDefinition method)
		{
			ILProcessor processor = method.Body.GetILProcessor();
			Instruction current = method.Body.Instructions.First();
			Instruction first = Instruction.Create( OpCodes.Nop );
			processor.InsertBefore( current, first );
			current = first;

			//Insert all instructions for debug output after Nop
			foreach ( Instruction instruction in LokiInvokeInstructions( method ) )
			{
				processor.InsertAfter( current, instruction );
				current = instruction;
			}
		}
		
		private List<Instruction> LokiInvokeInstructions(MethodDefinition method)
		{
		
			var newInstructions = new List<Instruction>();
			var arrayDef = new VariableDefinition(new ArrayType(ModuleDefinition.TypeSystem.Object));
			//var arrayDef = new VariableDefinition(new ArrayType(ModuleDefinition.TypeSystem.Object));
			method.Body.Variables.Add(arrayDef);
			newInstructions.Add(Instruction.Create(OpCodes.Ldc_I4, method.Parameters.Count));               
			newInstructions.Add(Instruction.Create(OpCodes.Newarr, ModuleDefinition.TypeSystem.Object)); 
			newInstructions.Add(Instruction.Create(OpCodes.Stloc, arrayDef)); 
			
			for (int i = 0; i < method.Parameters.Count; i++)
			{
				newInstructions.Add(Instruction.Create(OpCodes.Ldloc, arrayDef)); 
				newInstructions.Add(Instruction.Create(OpCodes.Ldc_I4, i)); 
				newInstructions.Add(Instruction.Create(OpCodes.Ldarg, method.Parameters[i])); 
				if (method.Parameters[i].ParameterType.IsValueType)
				{
					newInstructions.Add(Instruction.Create(OpCodes.Box, method.Parameters[i].ParameterType)); 
				}
				else
				{ 
					newInstructions.Add(Instruction.Create(OpCodes.Castclass, ModuleDefinition.TypeSystem.Object)); 
				}
				newInstructions.Add(Instruction.Create(OpCodes.Stelem_Ref)); 
			}
			newInstructions.Add(Instruction.Create(OpCodes.Ldstr,method.Name));
			newInstructions.Add(Instruction.Create(OpCodes.Ldstr,method.DeclaringType.FullName));
			newInstructions.Add(Instruction.Create(OpCodes.Ldloc, arrayDef)); 
			newInstructions.Add(Instruction.Create(OpCodes.Call, ModuleDefinition.ImportReference( _lokiInvokeMethod ) ));
			return newInstructions;
		}
		

		public override IEnumerable<string> GetAssembliesForScanning()
		{
			yield return "netstandard";
			yield return "mscorlib";
		}
	}
}