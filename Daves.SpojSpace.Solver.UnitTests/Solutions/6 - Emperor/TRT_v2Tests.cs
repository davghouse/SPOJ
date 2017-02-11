using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Daves.SpojSpace.Solver.UnitTests.Solutions._6___Emperor
{
    [TestClass]
    public sealed class TRT_v2Tests : SolutionTestsBase
    {
        public override string SolutionSource => Daves.SpojSpace.Solver.Properties.Resources.TRT_v2;

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
        public void TRT_v2() => TestSolution();
    }
}
