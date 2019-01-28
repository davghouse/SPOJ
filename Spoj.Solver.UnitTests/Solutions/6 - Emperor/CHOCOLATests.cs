using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._6___Emperor
{
    [TestClass]
    public sealed class CHOCOLATests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.CHOCOLA;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"2

6 4
2
1
3
1
4
4
1
2

6 5
2
1
3
1
4
4
1
2
9"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"42
62
"
        };

        [TestMethod]
        public void CHOCOLA() => TestSolution();
    }
}
