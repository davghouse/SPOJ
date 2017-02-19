using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Daves.SpojSpace.Solver.UnitTests.Solutions._5___King
{
    [TestClass]
    public sealed class HOTELSTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.HOTELS;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"5 12
2 1 3 4 5",
@"4 9
7 3 5 6"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"12
",
@"8
"
        };

        [TestMethod]
        public void HOTELS() => TestSolution();
    }
}
