using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._3___Warlord
{
    [TestClass]
    public sealed class CANDYTests : SolutionTestsBase
    {
        public override string SolutionSource => Spoj.Solver.Properties.Resources.CANDY;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"5
1
1
1
1
6
2
3
4
-1"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"4
-1
"
        };

        [TestMethod]
        public void CANDY() => TestSolution();
    }
}
