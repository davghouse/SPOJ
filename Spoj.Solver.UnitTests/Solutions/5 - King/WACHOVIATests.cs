using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._5___King
{
    [TestClass]
    public sealed class WACHOVIATests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.WACHOVIA;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"3
34 5
178 12
30 1
13 7
34 8
87 6
900 1
900 25
100 10
27 16
131 9
132 17
6 5
6 23
56 21
100 25
1 25
25 25
100 2"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"Hey stupid robber, you can get 8.
Hey stupid robber, you can get 25.
Hey stupid robber, you can get 99.
"
        };

        [TestMethod]
        public void WACHOVIA() => TestSolution();
    }
}
