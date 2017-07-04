using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace SpojSpace.Solver.UnitTests.Solutions._6___Emperor
{
    [TestClass]
    public sealed class ABCDEFTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.ABCDEF;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"1
1",
@"2
2
3",
@"2
-1
1",
@"3
5
7
10"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"1
",
@"4
",
@"24
",
@"10
"
        };

        [TestMethod]
        public void ABCDEF() => TestSolution();
    }
}
