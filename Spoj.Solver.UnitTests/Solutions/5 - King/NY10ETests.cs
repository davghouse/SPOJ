using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._5___King
{
    [TestClass]
    public sealed class NY10ETests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.NY10E;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"16
1 1
2 2
3 3
4 4
5 5
6 6
7 7
8 8
9 9
10 10
11 11
12 12
13 13
14 14
15 15
64 64"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"1 10
2 55
3 220
4 715
5 2002
6 5005
7 11440
8 24310
9 48620
10 92378
11 167960
12 293930
13 497420
14 817190
15 1307504
64 97082021465
"
        };

        [TestMethod]
        public void NY10E() => TestSolution();
    }
}
