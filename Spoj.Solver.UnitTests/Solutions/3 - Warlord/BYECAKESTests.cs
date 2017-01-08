using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._3___Warlord
{
    [TestClass]
    public sealed class BYECAKESTests : SolutionTestsBase
    {
        public override string SolutionSource => Spoj.Solver.Properties.Resources.BYECAKES;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"2 3 4 5 1 1 1 1
3 6 9 0 1 2 3 4
-1 -1 -1 -1 -1 -1 -1 -1"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"3 2 1 0
0 0 0 12
"
        };

        [TestMethod]
        public void BYECAKES() => TestSolution();
    }
}
