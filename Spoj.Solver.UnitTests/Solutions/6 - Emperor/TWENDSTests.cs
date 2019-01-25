using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._6___Emperor
{
    [TestClass]
    public sealed class TWENDSTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.TWENDS;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"4 3 2 10 4
8 1 2 3 4 5 6 7 8
8 2 2 1 5 3 8 7 3
4 3 1112 10 114
8 18 21 45 41 15 61 17 18
8 21 21 11 15 13 18 71 3
2 12 8
2 8 12
6 300 100 10 500 1000 100
0"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"In game 1, the greedy strategy might lose by as many as 7 points.
In game 2, the greedy strategy might lose by as many as 4 points.
In game 3, the greedy strategy might lose by as many as 5 points.
In game 4, the greedy strategy might lose by as many as 1213 points.
In game 5, the greedy strategy might lose by as many as 48 points.
In game 6, the greedy strategy might lose by as many as 59 points.
In game 7, the greedy strategy might lose by as many as 4 points.
In game 8, the greedy strategy might lose by as many as 4 points.
In game 9, the greedy strategy might lose by as many as 610 points.
"
        };

        [TestMethod]
        public void TWENDS() => TestSolution();
    }
}
