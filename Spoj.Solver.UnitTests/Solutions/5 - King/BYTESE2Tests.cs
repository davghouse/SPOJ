using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._5___King
{
    [TestClass]
    public sealed class BYTESE2Tests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.BYTESE2;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"3
5
1 7
2 4
6 9
3 8
5 10
5
1 2
3 4
5 6
7 8
9 10
5
1 100
2 101
3 102
4 103
5 104"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"4
1
5
"
        };

        [TestMethod]
        public void BYTESE2() => TestSolution();
    }
}
