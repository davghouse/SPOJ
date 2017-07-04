using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace SpojSpace.Solver.UnitTests.Solutions._7___Immortal
{
    [TestClass]
    public sealed class GSS3Tests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.GSS3;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"4
1 2 3 4
4
1 1 3
0 3 -3
1 2 4
1 3 3"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"6
4
-3
"
        };

        [TestMethod]
        public void GSS3() => TestSolution();
    }
}
