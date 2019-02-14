using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Spoj.Solver.UnitTests.Solutions._8___Deity
{
    [TestClass]
    public sealed class MULTQ3_v1Tests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.MULTQ3_v1;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"4 7
1 0 3
0 1 2
0 1 3
1 0 0
0 0 3
1 3 3
1 0 3",
@"73 90
0 47 69
0 47 71
0 10 33
0 30 71
1 4 21
1 41 72
0 56 64
1 29 59
0 5 29
1 68 71
0 26 28
1 4 27
1 31 65
1 45 65
1 22 68
1 52 70
0 66 70
0 13 51
1 66 71
1 19 20
1 14 52
1 55 67
1 14 63
1 38 56
1 55 72
1 18 21
1 60 61
0 68 71
1 47 52
0 26 67
0 58 61
0 5 64
0 4 33
0 49 69
0 58 67
1 36 67
0 39 62
0 51 56
1 46 50
0 29 48
0 67 68
1 59 60
0 65 68
0 21 50
0 28 30
1 67 72
0 48 62
1 52 72
1 21 21
0 37 46
1 63 65
1 42 72
0 45 54
1 29 51
0 34 64
0 57 71
1 30 33
0 27 67
0 6 60
0 54 65
0 70 72
0 71 71
0 47 58
1 34 72
1 51 55
0 59 71
0 22 36
0 68 69
0 16 38
1 17 43
1 43 49
0 17 24
0 55 55
1 38 62
0 60 72
0 36 37
0 18 24
0 70 71
0 44 47
0 33 36
0 34 69
1 22 27
0 1 16
1 14 37
0 52 60
0 61 63
1 41 65
0 24 26
0 68 68
1 20 48"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"4
1
0
2
",
@"6
24
9
2
3
10
10
16
9
1
2
18
2
21
4
4
4
0
1
11
0
0
4
9
1
0
10
7
1
18
2
8
4
11
2
7
5
12
"
        // For performance reasons FastIO new lines are \n instead of \r\n.
        }.Select(o => o.Replace(Environment.NewLine, "\n")).ToArray();

        [TestMethod]
        public void MULTQ3_v1() => TestSolution();
    }
}
