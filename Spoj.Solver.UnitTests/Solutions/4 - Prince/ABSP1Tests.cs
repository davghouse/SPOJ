using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._4___Prince
{
    [TestClass]
    public sealed class ABSP1Tests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.ABSP1;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"3
4
1 2 3 3
6
1 1 4 4 4 5
4
1 1000000000 1000000000 1000000000"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"7
29
2999999997
"
        };

        [TestMethod]
        public void ABSP1() => TestSolution();
    }
}
