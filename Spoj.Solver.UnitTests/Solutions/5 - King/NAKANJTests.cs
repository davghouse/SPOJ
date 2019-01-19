using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._5___King
{
    [TestClass]
    public sealed class NAKANJTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.NAKANJ;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"5
a1 h8
a1 c2
h8 c3
a8 h1
a8 h8"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"6
1
4
6
5
"
        };

        [TestMethod]
        public void NAKANJ() => TestSolution();
    }
}
