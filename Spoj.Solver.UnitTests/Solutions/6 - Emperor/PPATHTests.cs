using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._6___Emperor
{
    [TestClass]
    public sealed class PPATHTests : SolutionTestsBase
    {
        public override string SolutionSource => Spoj.Solver.Properties.Resources.PPATH;

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
