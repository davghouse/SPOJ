using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._7___Immortal
{
    [TestClass]
    public sealed class ROOTCIPHTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.ROOTCIPH;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"5
-145 -786 5400
-6 11 -6
-10630 -9389671 1410220240
6 -90000 -540000
-57 1083 -6859"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"22597
14
131776242
180036
1083
"
        };

        [TestMethod]
        public void ROOTCIPH() => TestSolution();
    }
}
