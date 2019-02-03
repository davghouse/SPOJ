using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._4___Prince
{
    [TestClass]
    public sealed class STREETRTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.STREETR;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"4
1
3
7
13",
@"4
2
6
12
18",
@"3
1
3
5",
@"4
1
3
5
9"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
"3",
"5",
"0",
"1"
        };

        [TestMethod]
        public void STREETR() => TestSolution();
    }
}
