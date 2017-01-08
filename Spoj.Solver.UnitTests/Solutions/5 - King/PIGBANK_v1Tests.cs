using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._5___King
{
    [TestClass]
    public sealed class PIGBANK_v1Tests : SolutionTestsBase
    {
        public override string SolutionSource => Spoj.Solver.Properties.Resources.PIGBANK_v1;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"3
10 110
2
1 1
30 50
10 110
2
1 1
50 30
1 6
2
10 3
20 4"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"The minimum amount of money in the piggy-bank is 60.
The minimum amount of money in the piggy-bank is 100.
This is impossible.
"
        };

        [TestMethod]
        public void PIGBANK_v1() => TestSolution();
    }
}
