using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._6___Emperor
{
    [TestClass]
    public sealed class NOTATRITests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.NOTATRI;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"5
2 8 4 1 6
6
2 4 9 1 6 6
7
1 5 5 4 8 7 4
3
4 2 10
3
1 2 3
4
5 2 9 6
0"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"7
12
8
1
0
2
"
        };

        [TestMethod]
        public void NOTATRI() => TestSolution();
    }
}
