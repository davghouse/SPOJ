using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._6___Emperor
{
    [TestClass]
    public sealed class INTESTTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.INTEST;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"7 3
1
51
966369
7
9
999996
11"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"4"
        };

        [TestMethod]
        public void INTEST() => TestSolution();
    }
}
