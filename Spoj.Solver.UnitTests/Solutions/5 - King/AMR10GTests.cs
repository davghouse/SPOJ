using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._5___King
{
    [TestClass]
    public sealed class AMR10GTests : SolutionTestsBase
    {
        public override string SolutionSource => Spoj.Solver.Properties.Resources.AMR10G;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"3
3 1
2 5 4
3 2
5 2 4
3 3
2 5 4"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"0
1
3
"
        };

        [TestMethod]
        public void AMR10G() => TestSolution();
    }
}
