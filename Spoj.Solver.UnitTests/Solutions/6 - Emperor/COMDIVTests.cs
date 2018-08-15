using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Spoj.Solver.UnitTests.Solutions._6___Emperor
{
    [TestClass]
    public sealed class COMDIVTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.COMDIV;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"3
100000 100000
12 24
747794 238336"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"36
6
2
"
        // For performance reasons FastIO new lines are \n instead of \r\n.
        }.Select(o => o.Replace(Environment.NewLine, "\n")).ToArray();

        [TestMethod]
        public void COMDIV() => TestSolution();
    }
}
