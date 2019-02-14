using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._5___King
{
    [TestClass]
    public sealed class GNYR09FTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.GNYR09F;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"10
1 5 2
2 20 8
3 30 17
4 40 24
5 50 37
6 60 52
7 70 59
8 80 73
9 90 84
10 100 90"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"1 6
2 63426
3 1861225
4 168212501
5 44874764
6 160916
7 22937308
8 99167
9 15476
10 23076518
"
        };

        [TestMethod]
        public void GNYR09F() => TestSolution();
    }
}
