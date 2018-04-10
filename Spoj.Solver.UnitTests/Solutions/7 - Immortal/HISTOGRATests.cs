using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._7___Immortal
{
    [TestClass]
    public sealed class HISTOGRATests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.HISTOGRA;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"7 2 1 4 5 1 3 3
4 1000 1000 1000 1000
0"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"8
4000
"
        };

        [TestMethod]
        public void HISTOGRA() => TestSolution();
    }
}
