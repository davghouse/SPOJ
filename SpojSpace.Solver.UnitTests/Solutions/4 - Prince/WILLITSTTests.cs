using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace SpojSpace.Solver.UnitTests.Solutions._4___Prince
{
    [TestClass]
    public sealed class WILLITSTTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.WILLITST;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"4"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"TAK
"
        };

        [TestMethod]
        public void WILLITST() => TestSolution();
    }
}
