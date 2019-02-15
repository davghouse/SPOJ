using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Spoj.Solver.UnitTests.Solutions._7___Immortal
{
    [TestClass]
    public sealed class CHICAGOTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.CHICAGO;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"5 5
1 3 51
3 5 53
2 4 60
4 5 80
1 2 43
5 5
1 3 51
3 5 51
2 4 90
4 5 93
1 2 30
5 7
5 2 100
3 5 80
2 3 70
2 1 50
3 4 90
4 1 85
3 1 70
0"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"27.030000 percent
26.010000 percent
61.200000 percent
"
        };

        [TestMethod]
        public void CHICAGO() => TestSolution();
    }
}
