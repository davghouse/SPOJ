using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spoj.Library.Helpers;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Spoj.Library.UnitTests.IO
{
    public abstract class IOTestsBase
    {
        public abstract string TestSource { get; }

        public IReadOnlyList<string> TestInputs => new[]
        {
@"5
1
-2
3
4
-5",
@"1
0"
        };

        public virtual IReadOnlyList<string> TestOutputs => new[]
        {
@"1
-2
3
4
-5
",
@"0
"
        };

        protected void Test([CallerMemberName] string testName = null)
        {
            var (success, diagnostics) = RuntimeCompiler.CompileToExe(testName, TestSource);
            Assert.IsTrue(success, message: string.Join(Environment.NewLine, diagnostics));

            for (int i = 0; i < TestInputs.Count; ++i)
            {
                var (exitCode, output) = RuntimeCompiler.ExecuteExe(testName, TestInputs[i]);
                Assert.AreEqual(0, exitCode, message: "Non-zero exit code (runtime error).");
                Assert.AreEqual(TestOutputs[i], output);
            }
        }
    }
}
