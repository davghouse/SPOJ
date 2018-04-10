using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._6___Emperor
{
    [TestClass]
    public sealed class FIBOSUMTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.FIBOSUM;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"3
0 3
3 5
10 19"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"4
10
10857
"
        };

        [TestMethod]
        public void FIBOSUM() => TestSolution();
    }
}
