using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._5___King
{
    [TestClass]
    public sealed class GAMESTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.GAMES;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"5
5.5857
30.25
2.3333
30.925
5.5"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"10000
4
10000
40
2
"
        };

        [TestMethod]
        public void GAMES() => TestSolution();
    }
}
