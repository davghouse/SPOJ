using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace SpojSpace.Solver.UnitTests.Solutions._3___Warlord
{
    [TestClass]
    public sealed class AMR12DTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.AMR12D;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"2
aba
ab"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"YES
NO
"
        };

        [TestMethod]
        public void AMR12D() => TestSolution();
    }
}
