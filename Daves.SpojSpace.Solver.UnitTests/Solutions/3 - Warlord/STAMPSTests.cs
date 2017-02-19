using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Daves.SpojSpace.Solver.UnitTests.Solutions._3___Warlord
{
    [TestClass]
    public sealed class STAMPSTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.STAMPS;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"3
100 6
13 17 42 9 23 57
99 6
13 17 42 9 23 57
1000 3
314 159 265"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"Scenario #1:
3

Scenario #2:
2

Scenario #3:
impossible

"
        };

        [TestMethod]
        public void STAMPS() => TestSolution();
    }
}
