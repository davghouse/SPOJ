using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Daves.SpojSpace.Solver.UnitTests.Solutions._3___Warlord
{
    [TestClass]
    public sealed class CANDY3Tests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.CANDY3;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"2

5
5
2
7
3
8

6
7
11
2
7
3
4"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"YES
NO
"
        };

        [TestMethod]
        public void CANDY3() => TestSolution();
    }
}
