using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._5___King
{
    [TestClass]
    public sealed class MAXLNTests : SolutionTestsBase
    {
        public override string SolutionSource => Spoj.Solver.Properties.Resources.MAXLN;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"1
1"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"Case 1: 4.25
"
        };

        [TestMethod]
        public void MAXLN() => TestSolution();
    }
}
