using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Daves.SpojSpace.Solver.UnitTests.Solutions._4___Prince
{
    [TestClass]
    public sealed class BISHOPSTests : SolutionTestsBase
    {
        public override string SolutionSource => Daves.SpojSpace.Solver.Properties.Resources.BISHOPS;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"2
3"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"2
4
"
        };

        [TestMethod]
        public void BISHOPS() => TestSolution();
    }
}
