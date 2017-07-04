using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace SpojSpace.Solver.UnitTests.Solutions._4___Prince
{
    [TestClass]
    public sealed class AP2Tests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.AP2;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"1
3 8 55"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"10
1 2 3 4 5 6 7 8 9 10
"
        };

        [TestMethod]
        public void AP2() => TestSolution();
    }
}
