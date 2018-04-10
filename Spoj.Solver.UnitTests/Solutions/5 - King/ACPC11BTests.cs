using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._5___King
{
    [TestClass]
    public sealed class ACPC11BTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.ACPC11B;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"2
8 1 3 5 7 9 7 3 1
8 2 4 6 8 10 8 6 2
8 2 3 5 10 9 3 2 1
7 1 2 6 12 13 3 2"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"1
0
"
        };

        [TestMethod]
        public void ACPC11B() => TestSolution();
    }
}
