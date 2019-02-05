using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._6___Emperor
{
    [TestClass]
    public sealed class SUBST1Tests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.SUBST1;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"2
CCCCC
ABABA"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"5
9
"
        };

        [TestMethod]
        public void SUBST1() => TestSolution();
    }
}
