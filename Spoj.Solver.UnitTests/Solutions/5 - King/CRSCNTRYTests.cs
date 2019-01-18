using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._5___King
{
    [TestClass]
    public sealed class CRSCNTRYTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.CRSCNTRY;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"3
1 2 3 4 5 6 7 8 9 0
1 3 8 2 0
2 5 7 8 9 0
1 1 1 1 1 1 2 3 0
1 3 1 3 5 7 8 9 3 4 0
1 2 35 0
0
1 3 5 7 0
3 7 5 1 0
0
1 2 1 1 0
1 1 1 0
0"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"6
2
3
"
        };

        [TestMethod]
        public void CRSCNTRY() => TestSolution();
    }
}
