using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._2___Chieftain
{
    [TestClass]
    public sealed class MULTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.MUL;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"6
4 2
123 43
324 342
0 12
9999 12345
1234314314 1433224002314"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"8
5289
110808
0
123437655
1769048901224539322596
"
        };

        [TestMethod]
        public void MUL() => TestSolution();
    }
}
