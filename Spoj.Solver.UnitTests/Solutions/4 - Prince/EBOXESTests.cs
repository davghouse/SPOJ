using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._4___Prince
{
    [TestClass]
    public sealed class EBOXESTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.EBOXES;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"1
11 8 2 102"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"115
"
        };

        [TestMethod]
        public void EBOXES() => TestSolution();
    }
}
