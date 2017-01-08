using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._3___Warlord
{
    [TestClass]
    public sealed class AMR12DTests : SolutionTestsBase
    {
        public override string SolutionSource => Spoj.Solver.Properties.Resources.AMR12D;

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
