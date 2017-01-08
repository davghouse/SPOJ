using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._6___Emperor
{
    [TestClass]
    public sealed class OLOLOTests : SolutionTestsBase
    {
        public override string SolutionSource => Spoj.Solver.Properties.Resources.OLOLO;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"3
1
8
1"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"8
"
        };

        [TestMethod]
        public void OLOLO() => TestSolution();
    }
}
