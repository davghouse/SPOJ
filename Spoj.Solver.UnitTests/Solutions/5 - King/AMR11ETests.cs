using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._5___King
{
    [TestClass]
    public sealed class AMR11ETests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.AMR11E;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"5
1
2
3
4
1000"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"30
42
60
66
2664
",
        };

        [TestMethod]
        public void AMR11E() => TestSolution();
    }
}
