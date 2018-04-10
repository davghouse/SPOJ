using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._4___Prince
{
    [TestClass]
    public sealed class DANGERTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.DANGER;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"05e0
01e1
42e0
66e6
00e0"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"3
5
21
64891137
"
        };

        [TestMethod]
        public void DANGER() => TestSolution();
    }
}
