using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._6___Emperor
{
    [TestClass]
    public sealed class PIETests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.PIE;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"3
3 3
4 3 3
1 24
5
10 5
1 4 2 3 4 5 6 5 4 2"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"25.1327
3.1416
50.2655
"
        };

        [TestMethod]
        public void PIE() => TestSolution();
    }
}
