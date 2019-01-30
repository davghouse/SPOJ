using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._6___Emperor
{
    [TestClass]
    public sealed class PT07XTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.PT07X;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"3
1 2
1 3",
@"10
1 2
1 3
1 4
1 5
1 6
1 7
2 8
2 9
2 10
2 11
2 12
2 13",
@"4
1 2
2 3
3 4"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
"1",
"2",
"2"
        };

        [TestMethod]
        public void PT07X() => TestSolution();
    }
}
