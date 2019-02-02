using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._6___Emperor
{
    [TestClass]
    public sealed class SPEEDTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.SPEED;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"10
1 5
-1 5
2 5
-5 2
10 7
-7 10
10 5
10 2
1 2
1 -2",
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"4
6
3
7
3
17
1
4
1
3
",
        };

        [TestMethod]
        public void SPEED() => TestSolution();
    }
}
