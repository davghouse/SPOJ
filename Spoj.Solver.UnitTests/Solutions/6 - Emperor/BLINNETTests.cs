using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._6___Emperor
{
    [TestClass]
    public sealed class BLINNETTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.BLINNET;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"2

4
gdansk
2
2 1
3 3
bydgoszcz
3
1 1
3 1
4 4
torun
3
1 3
2 1
4 1
warszawa
2
2 4
3 1

3
ixowo
2
2 1
3 3
iyekowo
2
1 1
3 7
zetowo
2
1 3 
2 7
"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"3
4
"
        };

        [TestMethod]
        public void BLINNET() => TestSolution();
    }
}
