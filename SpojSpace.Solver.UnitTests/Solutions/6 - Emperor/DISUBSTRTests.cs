using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace SpojSpace.Solver.UnitTests.Solutions._6___Emperor
{
    [TestClass]
    public sealed class DISUBSTRTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.DISUBSTR;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"2
CCCCC
ABABA"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"5
9
"
        };

        [TestMethod]
        public void DISUBSTR() => TestSolution();
    }
}
