using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._6___Emperor
{
    [TestClass]
    public sealed class ABA12CTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.ABA12C;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"9
10 10
10  100 10 100 10 100 10 100 10 10
6 4
-1 9 5 4 7 1
1 6
2 1 3 -1 4 10
5 4
4 -1 3 -1
3 5
-1 -1 4 5 -1
1 5
5 4 3 2 1
5 5
2 5 -1 -1 -1
1 5
2 5 3 2 6
3 6
2 1 3 -1 4 10",
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"10
4
10
7
-1
1
10
6
3
",
        };

        [TestMethod]
        public void ABA12C() => TestSolution();
    }
}
