using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._3___Warlord
{
    [TestClass]
    public sealed class EC_CONBTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.EC_CONB;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"10
5326216
3347467
3809100
2561960
2570529
5578334
5916487
2714262
7648372
9699839"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"577861
3347467
835095
358969
2570529
4013141
5916487
1725861
1513111
9699839
"
        };

        [TestMethod]
        public void EC_CONB() => TestSolution();
    }
}
