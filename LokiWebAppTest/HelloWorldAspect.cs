using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Cometary;
using Microsoft.CodeAnalysis.CSharp;

namespace LokiWebAppTest {
	internal sealed class DeepThoughtEditor : CompilationEditor
	{
		public string Namespace { get; }
    
		public DeepThoughtEditor(string @namespace)
		{
			Namespace = @namespace;
		}
  
		/// <inheritdoc />
		protected override void Initialize(CSharpCompilation compilation, CancellationToken cancellationToken)
		{
			CompilationPipeline += EditCompilation;
		}

		/// <summary>
		///   Edits the given <paramref name="compilation"/>, adding a <see cref="CSharpSyntaxTree"/>
		///   defining the 'Answers' class.
		/// </summary>
		private CSharpCompilation EditCompilation(CSharpCompilation compilation, CancellationToken cancellationToken)
		{
			if (this.State == compilation.Stat)
				return compilation;

			var tree = SyntaxFactory.ParseSyntaxTree("namespace {Namespace} {public static class Answers {public static int LifeTheUniverseAndEverything => 42;}}");

	return compilation.AddSyntaxTrees(tree);
}
}