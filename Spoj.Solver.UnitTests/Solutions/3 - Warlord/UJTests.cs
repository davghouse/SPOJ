using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._3___Warlord
{
    [TestClass]
    public sealed class UJTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.UJ;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"1 20
3 10
0 0"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"1
59049
"
        };

        [TestMethod]
        public void UJ() => TestSolution();
    }
}
