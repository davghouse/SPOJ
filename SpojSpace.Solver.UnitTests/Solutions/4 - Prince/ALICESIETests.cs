using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace SpojSpace.Solver.UnitTests.Solutions._4___Prince
{
    [TestClass]
    public sealed class ALICESIETests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.ALICESIE;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"3
2
3
5"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"1
2
3
"
        };

        [TestMethod]
        public void ALICESIE() => TestSolution();
    }
}
