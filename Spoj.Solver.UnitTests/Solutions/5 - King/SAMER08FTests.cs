using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._5___King
{
    [TestClass]
    public sealed class SAMER08FTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.SAMER08F;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"2
1
8
0"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"5
1
204
"
        };

        [TestMethod]
        public void SAMER08F() => TestSolution();
    }
}
