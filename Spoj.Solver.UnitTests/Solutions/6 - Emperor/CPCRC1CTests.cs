using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._6___Emperor
{
    [TestClass]
    public sealed class CPCRC1CTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.CPCRC1C;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"1 10
100 777
-1 -1"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"46
8655
"
        };

        [TestMethod]
        public void CPCRC1C() => TestSolution();
    }
}
