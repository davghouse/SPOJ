using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace SpojSpace.Solver.UnitTests.Solutions._5___King
{
    [TestClass]
    public sealed class MCOINSTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.MCOINS;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"2 3 5
3 12 113 25714 88888"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"ABAAB
"
        };

        [TestMethod]
        public void MCOINS() => TestSolution();
    }
}
