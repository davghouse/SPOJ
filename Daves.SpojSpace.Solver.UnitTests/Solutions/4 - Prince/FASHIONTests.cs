using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Daves.SpojSpace.Solver.UnitTests.Solutions._4___Prince
{
    [TestClass]
    public sealed class FASHIONTests : SolutionTestsBase
    {
        public override string SolutionSource => Daves.SpojSpace.Solver.Properties.Resources.FASHION;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"2
2
1 1
3 2
3
2 3 2
1 3 2"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"5
15
"
        };

        [TestMethod]
        public void FASHION() => TestSolution();
    }
}
