using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Daves.SpojSpace.Solver.UnitTests.Solutions._4___Prince
{
    [TestClass]
    public sealed class EGYPIZZATests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.EGYPIZZA;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"3
1/2
3/4
3/4",
@"5
1/2
3/4
1/2
1/4
1/4"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"4
",
@"4
"
        };

        [TestMethod]
        public void EGYPIZZA() => TestSolution();
    }
}
