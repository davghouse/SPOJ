using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._6___Emperor
{
    [TestClass]
    public sealed class ROCKTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.ROCK;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"2
15
100110001010001
16
0010111101100000"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"9
13
"
        };

        [TestMethod]
        public void ROCK() => TestSolution();
    }
}
