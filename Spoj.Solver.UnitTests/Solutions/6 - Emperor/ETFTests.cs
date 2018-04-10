using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._6___Emperor
{
    [TestClass]
    public sealed class ETFTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.ETF;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"5
1
2
3
4
5"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"1
1
2
2
4
"
        };

        [TestMethod]
        public void ETF() => TestSolution();
    }
}
