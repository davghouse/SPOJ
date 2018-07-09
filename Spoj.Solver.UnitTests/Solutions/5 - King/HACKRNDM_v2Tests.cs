using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._5___King
{
    [TestClass]
    public sealed class HACKRNDM_v2Tests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.HACKRNDM_v2;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"5 2
1
5
3
4
2",
@"10 1
1
3
5
7
9
2
4
6
8
10",
@"1 10
10",
@"4 100
1
2
103
3",
@"15 3
5
8
4
3
1
9
11
16
12
20
17
33
15
23
21",
@"2 1
2
1"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"3",
@"9",
@"0",
@"1",
@"7",
@"1"
        };

        [TestMethod]
        public void HACKRNDM_v2() => TestSolution();
    }
}
