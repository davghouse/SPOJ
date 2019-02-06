using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._7___Immortal
{
    [TestClass]
    public sealed class SHOPTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.SHOP;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"4 3
X1S3
42X4
X1D2

5 5
S5213
2X2X5
51248
4X4X2
1445D

0 0",
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"4
23
"
        };

        [TestMethod]
        public void SHOP() => TestSolution();
    }
}
