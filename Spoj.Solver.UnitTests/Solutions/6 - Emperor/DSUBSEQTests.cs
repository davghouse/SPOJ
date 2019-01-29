using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._6___Emperor
{
    [TestClass]
    public sealed class DSUBSEQTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.DSUBSEQ;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"3
AAA
ABCDEFG
CODECRAFT",
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"4
128
496
",
        };

        [TestMethod]
        public void DSUBSEQ() => TestSolution();
    }
}
