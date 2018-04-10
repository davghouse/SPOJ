using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._5___King
{
    [TestClass]
    public sealed class PARTYTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.PARTY;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"50 10
12 3
15 8
16 9
16 6
10 2
21 9
18 4
12 4
17 8
18 9

50 10
13 8
19 10
16 8
12 9
10 2
12 8
13 5
15 5
11 7
16 2

0 0"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"49 26
48 32
"
        };

        [TestMethod]
        public void PARTY() => TestSolution();
    }
}
