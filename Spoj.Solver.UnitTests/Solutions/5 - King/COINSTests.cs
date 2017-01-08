using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._5___King
{
    [TestClass]
    public sealed class COINSTests : SolutionTestsBase
    {
        public override string SolutionSource => Spoj.Solver.Properties.Resources.COINS;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"12
2"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"13
2
"
        };

        [TestMethod]
        public void COINS() => TestSolution();
    }
}
