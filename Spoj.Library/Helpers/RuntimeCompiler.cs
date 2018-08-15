using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Spoj.Library.Helpers
{
    // We used to compile to in-memory executables and then override Console.In/Out. Now some problems
    // use FastIO, which relies on the standard input/output streams (the default values for Console.In/Out).
    // So overriding Console.In/Out isn't an option for them. The easiest way to test those solutions is
    // to save the exes to disk and then start them in a process with redirected standard input/output.
    // For simplicity I'm taking that approach for all problems, regardless of I/O requirements.
    public static class RuntimeCompiler
    {
        private static readonly CSharpParseOptions _parseOptions = new CSharpParseOptions(
            languageVersion: LanguageVersion.CSharp6, // limited by SPOJ/ideone (gmcs 4.6.2)
            documentationMode: DocumentationMode.None,
            kind: SourceCodeKind.Regular);
        private static readonly MetadataReference[] _metadataReferences = new[]
        {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(Queue<int>).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(System.Numerics.BigInteger).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(IO.FastIO).Assembly.Location)
        };
        private static readonly CSharpCompilationOptions _compilationOptions = new CSharpCompilationOptions(
            outputKind: OutputKind.ConsoleApplication,
            optimizationLevel: OptimizationLevel.Release);

        public static (bool success, IEnumerable<string> diagnostics) CompileToExe(string exeName, string source)
        {
            var compilation = CSharpCompilation.Create(
               assemblyName: $"{exeName}",
               syntaxTrees: new[] { CSharpSyntaxTree.ParseText(source, _parseOptions) },
               references: _metadataReferences,
               options: _compilationOptions);
            var emitResult = compilation.Emit($"{exeName}.exe");

            return (emitResult.Success, emitResult.Diagnostics.Select(d => d.ToString()));
        }

        public static (int exitCode, string output) ExecuteExe(string exeName, string input)
        {
            using (var process = new Process())
            {
                process.StartInfo = new ProcessStartInfo
                {
                    FileName = $"{exeName}.exe",
                    UseShellExecute = false,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true
                };

                process.Start();
                process.PriorityClass = ProcessPriorityClass.RealTime;
                // Avoid deadlock for large I/O: https://blogs.msdn.microsoft.com/oldnewthing/20110707-00/?p=10223.
                Task.Run(() =>
                {
                    process.StandardInput.Write(input);
                    process.StandardInput.Close();
                });
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                return (process.ExitCode, output);
            }
        }
    }
}
