using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Daves.SpojSpace.Solver.UnitTests.Solutions._6___Emperor
{
    [TestClass]
    public sealed class BUGLIFETests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.BUGLIFE;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"2
3 3
1 2
2 3
1 3
4 2
1 2
3 4"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"Scenario #1:
Suspicious bugs found!
Scenario #2:
No suspicious bugs found!
"
        };

        [TestMethod]
        public void BUGLIFE() => TestSolution();
    }
}
