using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Spoj.Solver.UnitTests.Solutions._7___Immortal
{
    [TestClass]
    public sealed class GSS1Tests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.GSS1;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"3 
-1 2 3
1
1 2"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"2
"
        // For performance reasons FastIO new lines are \n instead of \r\n.
        }.Select(o => o.Replace(Environment.NewLine, "\n")).ToArray();

        [TestMethod]
        public void GSS1() => TestSolution();
    }
}
