using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._6___Emperor
{
    [TestClass]
    public sealed class ONEZEROTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.ONEZERO;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"8
1
2
3
355
13312
9999
20000
3134"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"1
10
111
100110
10010000000000
111111111111111111111111111111111111
100000
10001111110
"
        };

        [TestMethod]
        public void ONEZERO() => TestSolution();
    }
}
