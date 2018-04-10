using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace SpojSpace.Solver.UnitTests.Solutions._4___Prince
{
    [TestClass]
    public sealed class BUSYMANTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.BUSYMAN;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"3
3
3 9
2 8
6 9
4
1 7
5 8
7 8
1 8
6
7 9
0 10
4 5
8 9
4 10
5 7",
@"1
7
1 4
1 2
2 9
2 3
2 4
3 4
5 6"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"1
2
3
",
@"4
"
        };

        [TestMethod]
        public void BUSYMAN() => TestSolution();
    }
}
