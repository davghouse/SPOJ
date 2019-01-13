using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._5___King
{
    [TestClass]
    public sealed class TRGRIDTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.TRGRID;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"5
1 1
2 2
3 1
3 3
5 4"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"R
L
D
R
U
"
        };

        [TestMethod]
        public void TRGRID() => TestSolution();
    }
}
