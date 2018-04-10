using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._4___Prince
{
    [TestClass]
    public sealed class GLJIVETests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.GLJIVE;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"10
20
30
40
50
60
70
80
90
100"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"100
"
        };

        [TestMethod]
        public void GLJIVE() => TestSolution();
    }
}
