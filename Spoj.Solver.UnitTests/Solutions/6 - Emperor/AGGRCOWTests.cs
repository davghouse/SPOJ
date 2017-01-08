using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._6___Emperor
{
    [TestClass]
    public sealed class AGGRCOWTests : SolutionTestsBase
    {
        public override string SolutionSource => Spoj.Solver.Properties.Resources.AGGRCOW;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"1
5 3
1
2
8
4
9"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"3
"
        };

        [TestMethod]
        public void AGGRCOW() => TestSolution();
    }
}
