using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._5___King
{
    [TestClass]
    public sealed class FCTRLTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.FCTRL;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"7
3
60
100
125
1024
23456
8735373"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"0
14
24
31
253
5861
2183837
"
        };

        [TestMethod]
        public void FCTRL() => TestSolution();
    }
}
