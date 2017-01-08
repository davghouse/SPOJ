using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._5___King
{
    [TestClass]
    public sealed class MISERMANTests : SolutionTestsBase
    {
        public override string SolutionSource => Spoj.Solver.Properties.Resources.MISERMAN;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"5 5
1  3  1  2  6
10 2  5  4  15
10 9  6  7  1
2  7  1  5  3
8  2  6  1  9"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"10
"
        };

        [TestMethod]
        public void MISERMAN() => TestSolution();
    }
}
