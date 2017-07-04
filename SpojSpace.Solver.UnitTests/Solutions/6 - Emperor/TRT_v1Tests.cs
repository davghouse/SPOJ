using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace SpojSpace.Solver.UnitTests.Solutions._6___Emperor
{
    [TestClass]
    public sealed class TRT_v1Tests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.TRT_v1;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"5
1
3
1
5
2"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"43
"
        };

        [TestMethod]
        public void TRT_v1() => TestSolution();
    }
}
