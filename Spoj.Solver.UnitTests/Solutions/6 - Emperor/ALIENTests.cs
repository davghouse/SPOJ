using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Spoj.Solver.UnitTests.Solutions._6___Emperor
{
    [TestClass]
    public sealed class ALIENTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.ALIEN;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"
8
5 100
20 15 30 80 100
6 100
30 25 50 70 20 10
3 100
110 120 130
1 100
90
1 100
110
12 86
12 45 26 32 14 75 26 32 14 21 79 45
5 100
20 15 30 80 100
5 10
1 1 1 1 1"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"65 3
100 3
0 0
90 1
0 0
67 3
65 3
5 5
"
        // For performance reasons FastIO new lines are \n instead of \r\n.
        }.Select(o => o.Replace(Environment.NewLine, "\n")).ToArray();

        [TestMethod]
        public void ALIEN() => TestSolution();
    }
}
