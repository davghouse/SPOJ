using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._5___King
{
    [TestClass]
    public sealed class CEQUTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.CEQU;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"2
2 4 8
3 6 7"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"Case 1: Yes
Case 2: No
"
        };

        [TestMethod]
        public void CEQU() => TestSolution();
    }
}
