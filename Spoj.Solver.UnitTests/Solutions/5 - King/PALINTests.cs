using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._5___King
{
    [TestClass]
    public sealed class PALINTests : SolutionTestsBase
    {
        public override string SolutionSource => Spoj.Solver.Properties.Resources.PALIN;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"2
808
2133"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"818
2222
"
        };

        [TestMethod]
        public void PALIN() => TestSolution();
    }
}
