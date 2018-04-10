using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._5___King
{
    [TestClass]
    public sealed class JULKATests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.JULKA;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"10
2
55
43
12
3
990
98
67
56
1000
12
10000000
9998787
54
34
123
23
578
90"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"6
4
49
6
7
4
544
446
61
5
506
494
9999393
606
44
10
73
50
334
244
"
        };

        [TestMethod]
        public void JULKA() => TestSolution();
    }
}
