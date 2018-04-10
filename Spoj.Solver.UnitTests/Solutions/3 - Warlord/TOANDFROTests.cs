using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._3___Warlord
{
    [TestClass]
    public sealed class TOANDFROTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.TOANDFRO;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"5
toioynnkpheleaigshareconhtomesnlewx
3
ttyohhieneesiaabss
0"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"theresnoplacelikehomeonasnowynightx
thisistheeasyoneab
"
        };

        [TestMethod]
        public void TOANDFRO() => TestSolution();
    }
}
