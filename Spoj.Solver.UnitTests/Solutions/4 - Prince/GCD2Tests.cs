using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._4___Prince
{
    [TestClass]
    public sealed class GCD2Tests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.GCD2;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"2
2 6
10 11"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"2
1
"
        };

        [TestMethod]
        public void GCD2() => TestSolution();
    }
}
