using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._3___Warlord
{
    [TestClass]
    public sealed class CADYDISTTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.CADYDIST;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"4
1 1 1 1
2 2 2 2
5
10 80 37 22 109
6 8 8 20 15
0"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"8
2120
"
        };

        [TestMethod]
        public void CADYDIST() => TestSolution();
    }
}
