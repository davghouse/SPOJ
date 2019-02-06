using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._6___Emperor
{
    [TestClass]
    public sealed class APSTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.APS;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"1
9999999"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"3203714961607
"
        };

        [TestMethod]
        public void APS() => TestSolution();
    }
}
