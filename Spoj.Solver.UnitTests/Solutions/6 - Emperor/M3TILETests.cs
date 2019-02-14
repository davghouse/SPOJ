using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._6___Emperor
{
    [TestClass]
    public sealed class M3TILETests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.M3TILE;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"2
8
12
13
30
-1
"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"3
153
2131
0
299303201
"
        };

        [TestMethod]
        public void M3TILE() => TestSolution();
    }
}
