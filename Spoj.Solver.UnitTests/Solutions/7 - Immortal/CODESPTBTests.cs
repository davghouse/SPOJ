using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._7___Immortal
{
    [TestClass]
    public sealed class CODESPTBTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.CODESPTB;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"2
5
1 1 1 2 2
5
2 1 3 1 2"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"0
4
"
        };

        [TestMethod]
        public void CODESPTB() => TestSolution();
    }
}
