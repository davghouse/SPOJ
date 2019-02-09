using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._5___King
{
    [TestClass]
    public sealed class CATMTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.CATM;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"5 3
3
2 2 1 1 3 3
2 3 1 3 5 2
3 2 1 2 4 3"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"NO
YES
YES
"
        };

        [TestMethod]
        public void CATM() => TestSolution();
    }
}
