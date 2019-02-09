using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._6___Emperor
{
    [TestClass]
    public sealed class PONTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.PON;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"10
922337203685477580
2073920666
677789936
2102361656
757957974
2097665813
68812861848471
1592123869
1304627679
1000000007"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"NO
NO
NO
NO
NO
YES
NO
NO
NO
YES
"
        };

        [TestMethod]
        public void PON() => TestSolution();
    }
}
