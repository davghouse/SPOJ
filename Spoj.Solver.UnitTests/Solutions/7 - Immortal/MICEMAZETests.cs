using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._7___Immortal
{
    [TestClass]
    public sealed class MICEMAZETests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.MICEMAZE;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"4 
2 
1
8
1 2 1
1 3 1
2 1 1
2 4 1
3 1 1
3 4 1
4 2 1
4 3 1",
@"10
9
3 
10
8 9 2
1 2 1
1 4 2
3 4 1
3 9 1
2 4 1
4 9 1
5 6 1
6 7 1
7 9 1"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"3",
@"9"
        };

        [TestMethod]
        public void MICEMAZE() => TestSolution();
    }
}
