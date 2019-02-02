using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._4___Prince
{
    [TestClass]
    public sealed class ANARC09BTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.ANARC09B;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"21 24
22 24
22 23
1000 324
101 323
323 101
2 4
8 24
9 24
1 4
1 9
6 5
8 6
3 7
30 8
49 14
49 7
48 9
0 0"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"56
132
506
20250
32623
32623
2
3
24
4
9
30
12
21
60
14
7
48
"
        };

        [TestMethod]
        public void ANARC09B() => TestSolution();
    }
}
