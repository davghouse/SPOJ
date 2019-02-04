using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._7___Immortal
{
    [TestClass]
    public sealed class RPLNTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.RPLN;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"2
5 3
1 2 3 4 5
1 5
1 3
2 4
5 3
1 -2 -4 3 -5
1 5
1 3
2 4"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"Scenario #1:
1
1
2
Scenario #2:
-5
-4
-4
"
        };

        [TestMethod]
        public void RPLN() => TestSolution();
    }
}
