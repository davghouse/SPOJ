using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._7___Immortal
{
    [TestClass]
    public sealed class LCATests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.LCA;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"2
17
3 2 3 4
0
3 5 6 7
0
0
2 8 9
2 10 11
0
0
2 12 13
2 14 15
0
0
0
2 16 17
0
0
7
11 12
14 17
16 17
2 5
3 16
5 9
2 14
7
3 2 3 4
0
3 5 6 7
0
0
0
0
2
5 7
2 7"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"Case 1:
7
11
15
1
3
3
1
Case 2:
3
1
"
        };

        [TestMethod]
        public void LCA() => TestSolution();
    }
}
