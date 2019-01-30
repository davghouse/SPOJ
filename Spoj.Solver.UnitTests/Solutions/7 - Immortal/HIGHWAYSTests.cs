using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._7___Immortal
{
    [TestClass]
    public sealed class HIGHWAYSTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.HIGHWAYS;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"2
4 2 1 4
1 2 5
3 4 5
4 5 1 4
1 2 900
2 3 5
3 4 5
1 2 5
4 2 6",
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"NONE
11
",
        };

        [TestMethod]
        public void HIGHWAYS() => TestSolution();
    }
}
