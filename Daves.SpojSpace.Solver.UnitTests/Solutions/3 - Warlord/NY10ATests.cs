using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Daves.SpojSpace.Solver.UnitTests.Solutions._3___Warlord
{
    [TestClass]
    public sealed class NY10ATests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.NY10A;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"4
1
HHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH
2
TTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT
3
HHTTTHHTTTHTHHTHHTTHTTTHHHTHTTHTTHTTTHTH
4
HTHTHHHTHHHTHTHHHHTTTHTTTTTHHTTTTHTHHHHT"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"1 0 0 0 0 0 0 0 38
2 38 0 0 0 0 0 0 0
3 4 7 6 4 7 4 5 1
4 6 3 4 5 3 6 5 6
"
        };

        [TestMethod]
        public void NY10A() => TestSolution();
    }
}
