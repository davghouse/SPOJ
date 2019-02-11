using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._6___Emperor
{
    [TestClass]
    public sealed class SCUBADIVTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.SCUBADIV;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"1
5 60
5
3 36 120
10 25 129
5 50 250
1 45 130
4 20 119"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"249
"
        };

        [TestMethod]
        public void SCUBADIV() => TestSolution();
    }
}
