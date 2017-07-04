using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace SpojSpace.Solver.UnitTests.Solutions._4___Prince
{
    [TestClass]
    public sealed class OFFSIDETests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.OFFSIDE;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"2 3
500 700
700 500 500
2 2
200 400
200 1000
3 4
530 510 490
480 470 50 310
0 0"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"N
Y
N
"
        };

        [TestMethod]
        public void OFFSIDE() => TestSolution();
    }
}
