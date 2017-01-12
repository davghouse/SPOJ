using Microsoft.CodeDom.Providers.DotNetCompilerPlatform;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;

namespace Spoj.Solver.UnitTests.Solutions
{
    public abstract class SolutionTestsBase
    {
        private static readonly CSharpCodeProvider _compiler = new CSharpCodeProvider();
        private static readonly CompilerParameters _compilerParameters = new CompilerParameters();

        static SolutionTestsBase()
        {
            // Little hack here, see http://stackoverflow.com/a/40311406/1676558.
            object compilerSettings = typeof(CSharpCodeProvider)
                .GetField("_compilerSettings", BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue(_compiler);
            FieldInfo compilerSettingsFullPathField = compilerSettings
                .GetType()
                .GetField("_compilerFullPath", BindingFlags.Instance | BindingFlags.NonPublic);
            string desiredCompilerSettingsFullPath = ((string)compilerSettingsFullPathField
                .GetValue(compilerSettings))
                .Replace(@"bin\roslyn\", @"roslyn\");
            compilerSettingsFullPathField.SetValue(compilerSettings, desiredCompilerSettingsFullPath);

            _compilerParameters.GenerateExecutable = true;
            _compilerParameters.GenerateInMemory = true;
            _compilerParameters.CompilerOptions = "/optimize";
            _compilerParameters.ReferencedAssemblies.AddRange(new[]
            {
                "System.dll",
                "System.Core.dll",
                "System.Numerics.dll"
            });
        }

        public abstract string SolutionSource { get; }
        public abstract IReadOnlyList<string> TestInputs { get; }
        public abstract IReadOnlyList<string> TestOutputs { get; }

        protected void TestSolution()
        {
            CompilerResults compilerResults = _compiler
                .CompileAssemblyFromSource(_compilerParameters, SolutionSource);
            var compilationErrors = compilerResults.Errors
                .Cast<CompilerError>()
                .Select(e => e.ErrorText);

            Assert.IsTrue(!compilationErrors.Any(), message: string.Join(Environment.NewLine, compilationErrors));

            for (int i = 0; i < TestInputs.Count; ++i)
            {
                using (var @in = new StringReader(TestInputs[i]))
                {
                    using (var @out = new StringWriter())
                    {
                        Console.SetIn(@in);
                        Console.SetOut(@out);

                        compilerResults.CompiledAssembly.EntryPoint.Invoke(null, null);

                        VerifyOutput(TestOutputs[i], @out.ToString());
                    }
                }
            }
        }

        protected virtual void VerifyOutput(string expectedOutput, string actualOutput)
            => Assert.AreEqual(expectedOutput, actualOutput);
    }
}
