using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace SpojSpace.Solver.UnitTests.Solutions._3___Warlord
{
    [TestClass]
    public sealed class CRDSTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.CRDS;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"2
3
7"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"15
77
"
        };

        [TestMethod]
        public void CRDS() => TestSolution();
    }
}
