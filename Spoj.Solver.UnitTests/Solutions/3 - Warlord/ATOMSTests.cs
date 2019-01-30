using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._3___Warlord
{
    [TestClass]
    public sealed class ATOMSTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.ATOMS;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"2
2 2 204831321
454112121210521 6132151202008451 451221023547894562"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"26
0
"
        };

        [TestMethod]
        public void ATOMS() => TestSolution();
    }
}
