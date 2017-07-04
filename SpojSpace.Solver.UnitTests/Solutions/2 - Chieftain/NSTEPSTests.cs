using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace SpojSpace.Solver.UnitTests.Solutions._2___Chieftain
{
    [TestClass]
    public sealed class NSTEPSTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.NSTEPS;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"3
4 2
6 6
3 4"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"6
12
No Number
"
        };

        [TestMethod]
        public void NSTEPS() => TestSolution();
    }
}
