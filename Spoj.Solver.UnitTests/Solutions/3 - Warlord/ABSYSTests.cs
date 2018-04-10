using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._3___Warlord
{
    [TestClass]
    public sealed class ABSYSTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.ABSYS;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"3

23 + 47 = machula

3247 + 5machula2 = 3749

machula13 + 75425 = 77038"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"23 + 47 = 70
3247 + 502 = 3749
1613 + 75425 = 77038
"
        };

        [TestMethod]
        public void ABSYS() => TestSolution();
    }
}
