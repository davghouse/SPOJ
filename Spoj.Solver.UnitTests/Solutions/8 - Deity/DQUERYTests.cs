using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._8___Deity
{
    [TestClass]
    public sealed class DQUERYTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.DQUERY;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"5
1 1 2 1 3
3
1 5
2 4
3 5"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"3
2
3
"
        };

        [TestMethod]
        public void DQUERY() => TestSolution();
    }
}
