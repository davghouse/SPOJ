using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spoj.Library.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Spoj.Solver.UnitTests.Solutions
{
    public abstract class SolutionTestsBase
    {
        private static readonly string[] _tags = Solver.Solutions.Tags
            .Split(default(char[]), StringSplitOptions.RemoveEmptyEntries);

        public abstract string SolutionSource { get; }
        public abstract IReadOnlyList<string> TestInputs { get; }
        public abstract IReadOnlyList<string> TestOutputs { get; }

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

        protected void TestSolution([CallerMemberName] string problemName = null)
        {
            TestFormatting();

            var (success, diagnostics) = RuntimeCompiler.CompileToExe(problemName, SolutionSource);
            Assert.IsTrue(success, message: string.Join(Environment.NewLine, diagnostics));

            for (int i = 0; i < TestInputs.Count; ++i)
            {
                var (exitCode, output) = RuntimeCompiler.ExecuteExe(problemName, TestInputs[i]);
                Assert.AreEqual(0, exitCode, message: "Non-zero exit code (runtime error).");
                VerifyOutput(TestOutputs[i], output);
            }
        }

        protected virtual void VerifyOutput(string expectedOutput, string actualOutput)
            => Assert.AreEqual(expectedOutput, actualOutput);
    }
}
