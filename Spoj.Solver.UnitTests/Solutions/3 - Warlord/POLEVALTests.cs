using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._3___Warlord
{
    [TestClass]
    public sealed class POLEVALTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.POLEVAL;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"2
1 -2 -1
5
0 1 -1 2 -2
3
2 1 -2 -1
4
0 -1 2 -2
-1"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"Case 1:
-1
-2
2
-1
7
Case 2:
-1
0
15
-9
"
        };

        [TestMethod]
        public void POLEVAL() => TestSolution();
    }
}
