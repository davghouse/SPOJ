using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Daves.SpojSpace.Solver.UnitTests.Solutions._5___King
{
    [TestClass]
    public sealed class BITMAPTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.BITMAP;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"1
3 4
0001
0011
0110"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"3 2 1 0
2 1 0 0
1 0 0 1
"
        };

        [TestMethod]
        public void BITMAP() => TestSolution();
    }
}
