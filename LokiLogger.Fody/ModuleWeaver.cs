using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Fody;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace LokiLogger.Fody {
	public class ModuleWeaver :
		BaseModuleWeaver{
		
		private static readonly MethodInfo _stringJoinMethod;
		private static readonly MethodInfo _stringFormatMethod;
		private static readonly MethodInfo _debugWriteLineMethod;

		static ModuleWeaver()
		{
			_stringJoinMethod = typeof( string )
				.GetMethods()
				.Where( x => x.Name == nameof( string.Join ) )
				.Single( x =>
				{
					var parameters = x.GetParameters();
					return parameters.Length == 2 &&
					       parameters[0].ParameterType == typeof( string ) &&
					       parameters[1].ParameterType == typeof( object[] );
				} );

			//Find string.Format(string, object) method
			_stringFormatMethod = typeof( string )
				.GetMethods()
				.Where( x => x.Name == nameof( string.Format ) )
				.Single( x =>
				{
					var parameters = x.GetParameters();
					return parameters.Length == 2 &&
					       parameters[0].ParameterType == typeof( string ) &&
					       parameters[1].ParameterType == typeof( object );
				} );

			Console.WriteLine();
			//Find Debug.WriteLine(string) method
			
			_debugWriteLineMethod = typeof( System.Console )
				.GetMethods()
				.Where( x => x.Name == nameof(System.Console.WriteLine ) )
				.Single( x =>
				{
					var parameters = x.GetParameters();
					return parameters.Length == 1 &&
					       parameters[0].ParameterType == typeof( string );
				} );
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
						if (y.CustomAttributes.Select(d => d.Constructor).Select(d => d.FullName).Any(t => t.Contains("System.Void LokiLogger.LokiAttribute")))
						{
							methodsToAdd.Add(y);
						}
					});
				});
			
			
			methodString += methodsToAdd.Count;
			
			System.IO.File.WriteAllText("/home/lokilokus/RiderProjects/lokilogger/LokiLogger.Fody/DataMethod.txt",methodString);
			foreach (MethodDefinition methodDefinition in methodsToAdd)
			{
				ProcessMethod(methodDefinition);
				
			}
			
			/*
			foreach (TypeDefinition typeDefinition in ModuleDefinition.Assembly.MainModule.Types.Where(x => x.IsClass))
			{
				foreach (MethodDefinition typeDefinitionMethod in typeDefinition.Methods)
				{
					var tmp = typeDefinitionMethod.CustomAttributes.Where(x => x.GetType() == typeof(LokiAttribute));
					foreach (CustomAttribute customAttribute in tmp)
					{
						System.IO.File.AppendAllText("/home/lokilokus/Documents/Data",customAttribute.Fields.SingleOrDefault(x => x.Name == "Dasda").Argument.Value as string);
					}
					System.IO.File.AppendAllText("/home/lokilokus/Documents/Data",tmp.Count() + "");
				}
			}*/
			
			//ModuleDefinition.Types.SingleOrDefault().CustomAttributes.
			//ModuleDefinition.Types.Where(x => x.IsClass).Select(x => x.Methods).Where(x => x.)
			
			
		}
		
		
		
		
		
		
		private void ProcessMethod( MethodDefinition method )
		{
			ILProcessor processor = method.Body.GetILProcessor();
			Instruction current = method.Body.Instructions.First();
			System.IO.File.AppendAllText("/home/lokilokus/RiderProjects/lokilogger/LokiLogger.Fody/DataMethod.txt",current.OpCode.Name+"\n");
			//Create Nop instruction to use as a starting point 
			//for the rest of our instructions
			Instruction first = Instruction.Create( OpCodes.Nop );
			processor.InsertBefore( current, first );
			current = first;

			//Insert all instructions for debug output after Nop
			foreach ( Instruction instruction in GetInstructions( method ) )
			{
				processor.InsertAfter( current, instruction );
				System.IO.File.AppendAllText("/home/lokilokus/RiderProjects/lokilogger/LokiLogger.Fody/DataMethod.txt",current.OpCode.Name+"\n");
				current = instruction;
			}
		}

		private IEnumerable<Instruction> GetInstructions( MethodDefinition method )
		{
			
			yield return Instruction.Create( OpCodes.Ldstr, $"DEBUG: {method.DeclaringType.FullName}.<{method.Name}({{0}})" );
			yield return Instruction.Create( OpCodes.Ldstr, "," );

			yield return Instruction.Create( OpCodes.Ldc_I4, method.Parameters.Count );
			yield return Instruction.Create( OpCodes.Newarr, ModuleDefinition.ImportReference( typeof( object ) ) );

			for ( int i = 0; i < method.Parameters.Count; i++ )
			{
				yield return Instruction.Create( OpCodes.Dup );
				yield return Instruction.Create( OpCodes.Ldc_I4, i );
				yield return Instruction.Create( OpCodes.Ldarg, method.Parameters[i] );
				if ( method.Parameters[i].ParameterType.IsValueType )
					yield return Instruction.Create( OpCodes.Box, method.Parameters[i].ParameterType );
				yield return Instruction.Create( OpCodes.Stelem_Ref );
			}

			yield return Instruction.Create( OpCodes.Call, ModuleDefinition.ImportReference( _stringJoinMethod ) );
			yield return Instruction.Create( OpCodes.Call, ModuleDefinition.ImportReference( _stringFormatMethod ) );
			yield return Instruction.Create( OpCodes.Call, ModuleDefinition.ImportReference( _debugWriteLineMethod ) );
		}
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		

		public override IEnumerable<string> GetAssembliesForScanning()
		{
			yield return "netstandard";
			yield return "mscorlib";
		}
		
		string GetNamespace()
		{
			var namespaceFromConfig = GetNamespaceFromConfig();
			var namespaceFromAttribute = GetNamespaceFromAttribute();
			if (namespaceFromConfig != null && namespaceFromAttribute != null)
			{
				throw new WeavingException("Configuring namespace from both Config and Attribute is not supported.");
			}

			if (namespaceFromAttribute != null)
			{
				return namespaceFromAttribute;
			}

			return namespaceFromConfig;
		}
		string GetNamespaceFromConfig()
		{
			var attribute = Config?.Attribute("Namespace");
			if (attribute == null)
			{
				return null;
			}

			var value = attribute.Value;
			ValidateNamespace(value);
			return value;
		}

		string GetNamespaceFromAttribute()
		{
			var attributes = ModuleDefinition.Assembly.CustomAttributes;
			var namespaceAttribute = attributes
				.SingleOrDefault(x => x.AttributeType.FullName == "NamespaceAttribute");
			if (namespaceAttribute == null)
			{
				return null;
			}

			attributes.Remove(namespaceAttribute);
			var value = (string)namespaceAttribute.ConstructorArguments.First().Value;
			ValidateNamespace(value);
			return value;
		}

		static void ValidateNamespace(string value)
		{
			if (value is null || string.IsNullOrWhiteSpace(value))
			{
				throw new WeavingException("Invalid namespace");
			}
		}

			
	}
}