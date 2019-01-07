using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._3___Warlord
{
    [TestClass]
    public sealed class RPLCTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.RPLC;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"2
5
4 -10 4 4 4
5
1 2 3 4 5"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"Scenario #1: 7
Scenario #2: 1
"
        };

        [TestMethod]
        public void RPLC() => TestSolution();
    }
}
