using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._3___Warlord
{
    [TestClass]
    public sealed class MRECAMANTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.MRECAMAN;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"7
10000
-1"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"20
18658
"
        };

        [TestMethod]
        public void MRECAMAN() => TestSolution();
    }
}
