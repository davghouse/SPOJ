using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._3___Warlord
{
    [TestClass]
    public sealed class SCPC11BTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.SCPC11B;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"2
0
900
8
1400
1200
1000
800
600
400
200
0
8
0
200
400
600
800
1000
1200
1322
8
0
200
400
600
800
1000
1200
1321
0"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"IMPOSSIBLE
POSSIBLE
POSSIBLE
IMPOSSIBLE
"
        };

        [TestMethod]
        public void SCPC11B() => TestSolution();
    }
}
