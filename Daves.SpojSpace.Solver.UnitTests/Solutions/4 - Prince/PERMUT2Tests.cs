using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Daves.SpojSpace.Solver.UnitTests.Solutions._4___Prince
{
    [TestClass]
    public sealed class PERMUT2Tests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.PERMUT2;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"4
1 4 3 2
5
2 3 4 5 1
1
1
0"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"ambiguous
not ambiguous
ambiguous
"
        };

        [TestMethod]
        public void PERMUT2() => TestSolution();
    }
}
