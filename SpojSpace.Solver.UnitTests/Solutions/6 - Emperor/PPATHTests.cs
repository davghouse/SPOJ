using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace SpojSpace.Solver.UnitTests.Solutions._6___Emperor
{
    [TestClass]
    public sealed class PPATHTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.PPATH;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"3
1033 8179
1373 8017
1033 1033"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"6
7
0
"
        };

        [TestMethod]
        public void PPATH() => TestSolution();
    }
}
