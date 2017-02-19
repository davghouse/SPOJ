using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Daves.SpojSpace.Solver.UnitTests.Solutions._6___Emperor
{
    [TestClass]
    public sealed class FAVDICETests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.FAVDICE;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"2
1
12"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"1.00
37.24
"
        };

        [TestMethod]
        public void FAVDICE() => TestSolution();
    }
}
