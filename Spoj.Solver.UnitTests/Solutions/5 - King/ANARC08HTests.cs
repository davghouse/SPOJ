using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._5___King
{
    [TestClass]
    public sealed class ANARC08HTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.ANARC08H;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"5 3
7 4
0 0"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"5 3 4
7 4 2
"
        };

        [TestMethod]
        public void ANARC08H() => TestSolution();
    }
}
