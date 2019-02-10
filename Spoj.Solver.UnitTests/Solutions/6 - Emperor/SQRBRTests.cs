using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._6___Emperor
{
    [TestClass]
    public sealed class SQRBRTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.SQRBR;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"6
1 1
1
1 1
2
2 1
1
3 1
2
4 2
5 7
19 1
1",
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"1
0
2
3
2
1767263190
",
        };

        [TestMethod]
        public void SQRBR() => TestSolution();
    }
}
