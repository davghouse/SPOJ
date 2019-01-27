using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._6___Emperor
{
    [TestClass]
    public sealed class EKOTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.EKO;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"4 7
20 15 10 17",
@"5 20
4 42 40 26 46",
@"1 8
10",
@"3 6
2 2 2"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
"15",
"36",
"2",
"0"

        };

        [TestMethod]
        public void EKO() => TestSolution();
    }
}
