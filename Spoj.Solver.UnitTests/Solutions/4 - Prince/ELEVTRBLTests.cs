using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._4___Prince
{
    [TestClass]
    public sealed class ELEVTRBLTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.ELEVTRBL;

        public override IReadOnlyList<string> TestInputs => new[]
        {
"10 1 10 2 1",
"100 2 1 1 0"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
"6",
"use the stairs"
        };

        [TestMethod]
        public void ELEVTRBL() => TestSolution();
    }
}
