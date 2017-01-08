using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._5___King
{
    [TestClass]
    public sealed class FCTRLTests : SolutionTestsBase
    {
        public override string SolutionSource => Spoj.Solver.Properties.Resources.FCTRL;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"6
3
60
100
1024
23456
8735373"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"0
14
24
253
5861
2183837
"
        };

        // This test fails sporadically due to running out of memory (allocating an array of size 200 million).
        [TestMethod]
        public void FCTRL() => TestSolution();
    }
}
