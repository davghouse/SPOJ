using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Daves.SpojSpace.Solver.UnitTests.Solutions._3___Warlord
{
    [TestClass]
    public sealed class ACPC10ATests : SolutionTestsBase
    {
        public override string SolutionSource => Daves.SpojSpace.Solver.Properties.Resources.ACPC10A;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"4 7 10
2 6 18
0 0 0"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"AP 13
GP 54
"
        };

        [TestMethod]
        public void ACPC10A() => TestSolution();
    }
}
