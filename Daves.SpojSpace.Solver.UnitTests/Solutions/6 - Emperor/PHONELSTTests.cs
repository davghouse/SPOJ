using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Daves.SpojSpace.Solver.UnitTests.Solutions._6___Emperor
{
    [TestClass]
    public sealed class PHONELSTTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.PHONELST;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"2
3
911
97625999
91125426
5
113
12340
123440
12345
98346"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"NO
YES
"
        };

        [TestMethod]
        public void PHONELST() => TestSolution();
    }
}
