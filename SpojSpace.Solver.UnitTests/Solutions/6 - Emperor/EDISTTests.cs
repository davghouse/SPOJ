using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace SpojSpace.Solver.UnitTests.Solutions._6___Emperor
{
    [TestClass]
    public sealed class EDISTTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.EDIST;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"1
FOOD
MONEY"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"4
"
        };

        [TestMethod]
        public void EDIST() => TestSolution();
    }
}
