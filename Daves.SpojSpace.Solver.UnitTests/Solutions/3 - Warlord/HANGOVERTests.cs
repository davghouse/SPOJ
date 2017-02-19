using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Daves.SpojSpace.Solver.UnitTests.Solutions._3___Warlord
{
    [TestClass]
    public sealed class HANGOVERTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.HANGOVER;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"1.00
3.71
0.04
5.19
0.00"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"3 card(s)
61 card(s)
1 card(s)
273 card(s)
"
        };

        [TestMethod]
        public void HANGOVER() => TestSolution();
    }
}
