using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._3___Warlord
{
    [TestClass]
    public sealed class BOMARBLETests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.BOMARBLE;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"1
2
3
4
6
5
1000
0"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"5
12
22
35
70
51
1502501
"
        };

        [TestMethod]
        public void BOMARBLE() => TestSolution();
    }
}
