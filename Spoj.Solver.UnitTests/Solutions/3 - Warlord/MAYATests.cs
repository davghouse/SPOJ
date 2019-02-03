using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._3___Warlord
{
    [TestClass]
    public sealed class MAYATests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.MAYA;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"7
... - - -
... - - -
... - - -
... - - -
... - - -
... - - -
... - - -

1
..

5
... -
. - -
S
S
S

0"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"1091368458
2
1231200
"
        };

        [TestMethod]
        public void MAYA() => TestSolution();
    }
}
