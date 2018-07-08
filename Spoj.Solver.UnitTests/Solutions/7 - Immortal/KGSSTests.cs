using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._7___Immortal
{
    [TestClass]
    public sealed class KGSSTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.KGSS;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"5
1 2 3 4 5
6
Q 2 4
Q 2 5
U 1 6
Q 1 5
U 1 7
Q 1 5",
@"2
1 2
5
Q 1 2
U 1 2
Q 1 2
U 2 3
Q 1 2",
@"10
1 2 3 3 5 6 2 8 1 2
11
U 1 4
Q 2 3
U 1 1
Q 2 4
Q 3 6
Q 2 4
U 3 5
Q 9 10
U 1 4
U 5 7
Q 2 7"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"7
9
11
12
",
@"3
4
5
",
@"5
6
11
6
3
13
"
        };

        [TestMethod]
        public void KGSS() => TestSolution();
    }
}
