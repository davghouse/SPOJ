using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._6___Emperor
{
    [TestClass]
    public sealed class BABTWRTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.BABTWR;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"5
31 41 59
26 53 58
97 93 23
84 62 64
33 83 27
1
1 1 1
0",
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"342
1
",
        };

        [TestMethod]
        public void BABTWR() => TestSolution();
    }
}
