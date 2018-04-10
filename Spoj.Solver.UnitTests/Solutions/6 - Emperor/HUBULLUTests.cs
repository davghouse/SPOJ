using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._6___Emperor
{
    [TestClass]
    public sealed class HUBULLUTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.HUBULLU;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"1
1 0"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"Airborne wins.
"
        };

        [TestMethod]
        public void HUBULLU() => TestSolution();
    }
}
