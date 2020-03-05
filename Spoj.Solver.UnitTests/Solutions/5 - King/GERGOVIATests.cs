using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._5___King
{
    [TestClass]
    public sealed class GERGOVIATests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.GERGOVIA;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"5
5 -4 1 -3 1
6
-1000 -1000 -1000 1000 1000 1000
0"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"9
9000
"
        };

        [TestMethod]
        public void GERGOVIA() => TestSolution();
    }
}
