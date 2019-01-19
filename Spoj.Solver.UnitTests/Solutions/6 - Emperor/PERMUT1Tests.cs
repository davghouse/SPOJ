using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._6___Emperor
{
    [TestClass]
    public sealed class PERMUT1Tests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.PERMUT1;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"19
3 0
3 1
3 2
3 3
3 4
4 0
4 1
4 2
4 3
4 4
4 5
4 6
4 7
4 98
12 98
12 66
1 98
1 0
12 25"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"1
2
2
1
0
1
3
5
6
5
3
1
0
0
0
1
0
1
14664752
"
        };

        [TestMethod]
        public void PERMUT1() => TestSolution();
    }
}
