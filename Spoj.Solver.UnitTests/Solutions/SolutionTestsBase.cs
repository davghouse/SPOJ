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
        private static readonly string[] _tags;
        private static readonly CSharpCodeProvider _compiler = new CSharpCodeProvider();
        private static readonly CompilerParameters _compilerParameters = new CompilerParameters();

        static SolutionTestsBase()
        {
            _tags = Solver.Solutions.Tags.Split(default(char[]), StringSplitOptions.RemoveEmptyEntries);

            // Little hack here, see http://stackoverflow.com/a/40311406.
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

        // Called from derived classes to allow descriptive names to appear in the Test Explorer.
        protected void TestSolution()
        {
            TestFormatting();

            CompilerResults compilerResults = _compiler
                .CompileAssemblyFromSource(_compilerParameters, SolutionSource);

            TestCompilation(compilerResults);
            TestExecution(compilerResults);
        }

        protected void TestFormatting()
        {
            string linkAndTagsLine = null;
            using (var solutionSource = new StringReader(SolutionSource))
            {
                while ((linkAndTagsLine = solutionSource.ReadLine()) != null)
                {
                    if (linkAndTagsLine.Contains(@"https://www.spoj.com/problems/"))
                        break;
                }
            }

            Assert.IsNotNull(linkAndTagsLine);
            Assert.IsTrue(linkAndTagsLine.StartsWith("// https:") || linkAndTagsLine.StartsWith("https:"));

            string[] tags = linkAndTagsLine
                .Split()
                .Where(s => s.StartsWith("#"))
                .ToArray();
            string[] orderedTags = tags
                .OrderBy(t => t)
                .ToArray();

            Assert.IsTrue(tags.All(t => _tags.Contains(t)),
                message: $"Invalid tags: {string.Join(" ", tags.Where(t => !_tags.Contains(t)))}.");
            Assert.IsTrue(tags.SequenceEqual(orderedTags),
                message: $"The tags need to be in alphabetical order, like: {string.Join(" ", orderedTags)}.");
        }

        private void TestCompilation(CompilerResults compilerResults)
        {
            var compilationErrors = compilerResults.Errors
                .Cast<CompilerError>()
                .Select(e => e.ErrorText);

            Assert.IsTrue(!compilationErrors.Any(),
                message: string.Join(Environment.NewLine, compilationErrors));
        }

        private void TestExecution(CompilerResults compilerResults)
        {
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
