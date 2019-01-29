using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._5___King
{
    [TestClass]
    public sealed class TSHOW1Tests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.TSHOW1;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"8
1000000000000000
5
29
30
65
45
101651241212421
1216321
6561362"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"6655566565666666565655655665556656555555555555556
65
6665
6666
555565
56665
5666555666556665555665555666666556656555555665
55656555666656555565
"
        };

        [TestMethod]
        public void TSHOW1() => TestSolution();
    }
}
