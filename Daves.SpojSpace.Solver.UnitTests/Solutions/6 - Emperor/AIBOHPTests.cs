using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Daves.SpojSpace.Solver.UnitTests.Solutions._6___Emperor
{
    [TestClass]
    public sealed class AIBOHPTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.AIBOHP;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"1
fft"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"1
"
        };

        [TestMethod]
        public void AIBOHP() => TestSolution();
    }
}
