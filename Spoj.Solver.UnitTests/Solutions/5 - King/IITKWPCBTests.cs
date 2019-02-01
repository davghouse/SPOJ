using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._5___King
{
    [TestClass]
    public sealed class IITKWPCBTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.IITKWPCB;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"4
3
4
5
100"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"1
1
2
49
",
        };

        [TestMethod]
        public void IITKWPCB() => TestSolution();
    }
}
