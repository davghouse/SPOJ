using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace SpojSpace.Solver.UnitTests.Solutions._5___King
{
    [TestClass]
    public sealed class MARBLESTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.MARBLES;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"2
10 10
30 7"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"1
475020
"
        };

        [TestMethod]
        public void MARBLES() => TestSolution();
    }
}
