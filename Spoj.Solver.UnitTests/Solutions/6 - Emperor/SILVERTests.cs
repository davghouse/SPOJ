using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._6___Emperor
{
    [TestClass]
    public sealed class SILVERTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.SILVER;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"1
5
3
0"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"0
2
1
"
        };

        [TestMethod]
        public void SILVER() => TestSolution();
    }
}
