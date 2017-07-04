using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace SpojSpace.Solver.UnitTests.Solutions._6___Emperor
{
    [TestClass]
    public sealed class COMDIVTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.COMDIV;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"3
100000 100000
12 24
747794 238336"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"36
6
2
"
        };

        [TestMethod]
        public void COMDIV() => TestSolution();
    }
}
