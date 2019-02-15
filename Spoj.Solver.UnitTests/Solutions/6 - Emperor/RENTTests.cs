using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._6___Emperor
{
    [TestClass]
    public sealed class RENTTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.RENT;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"2
4
0 5 10
3 7 14
5 9 7
6 9 8
3
0 0 1
1 1 2
1 2 3",
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"18
4
",
        };

        [TestMethod]
        public void RENT() => TestSolution();
    }
}
