using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace SpojSpace.Solver.UnitTests.Solutions._7___Immortal
{
    [TestClass]
    public sealed class CSTREETTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.CSTREET;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"1
2
5
7
1 2 1
2 3 2
2 4 6
5 2 1
5 1 3
4 5 2
3 4 3",
@"1
1
9
13
1 2 4
2 3 8
3 4 7
4 5 9
5 6 10
4 6 14
6 7 2
7 8 1
8 9 7
3 9 2
3 6 4
7 9 6
2 8 11
1 8 8",
@"2
2
5
7
1 2 1
2 3 2
2 4 6
5 2 1
5 1 3
4 5 2
3 4 3
2
5
7
1 2 1
2 3 2
3 4 3
4 5 2
1 5 3
2 5 1
2 4 6"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"12
",
@"37
",
@"12
12
",
        };

        [TestMethod]
        public void CSTREET() => TestSolution();
    }
}
