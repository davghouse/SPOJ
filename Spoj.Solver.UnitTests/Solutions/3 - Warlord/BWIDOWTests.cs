using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._3___Warlord
{
    [TestClass]
    public sealed class BWIDOWTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.BWIDOW;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"2
3
2 3
6 8
3 5
3
4 5
5 8
3 10"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"2
-1
"
        };

        [TestMethod]
        public void BWIDOW() => TestSolution();
    }
}
