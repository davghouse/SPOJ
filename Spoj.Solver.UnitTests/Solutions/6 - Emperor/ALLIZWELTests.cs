using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._6___Emperor
{
    [TestClass]
    public sealed class ALLIZWELTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.ALLIZWEL;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"5
3 6
AWE.QX
LLL.EO
IZZWLL

1 10
ALLIZZWELL

2 9
A.L.Z.E..
.L.I.W.L.

3 3
AEL
LWZ
LIZ

1 10
LLEWZZILLA
"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"YES
YES
NO
NO
YES
"
        };

        [TestMethod]
        public void ALLIZWEL() => TestSolution();
    }
}
