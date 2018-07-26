using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._6___Emperor
{
    [TestClass]
    public sealed class SUMFOURTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.SUMFOUR;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"6
-45 22 42 -16
-41 -27 56 30
-36 53 -37 77
-36 30 -75 -46
26 -38 -10 62
-32 -54 -6 45",
@"1
0 0 0 0",
@"3
0 0 0 0
0 0 0 0
0 0 0 0"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"5
",
@"1
",
@"81
"
        };

        [TestMethod]
        public void SUMFOUR() => TestSolution();
    }
}
