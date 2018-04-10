using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._4___Prince
{
    [TestClass]
    public sealed class ENIGMATHTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.ENIGMATH;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"1
2 3"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"3 2
"
        };

        [TestMethod]
        public void ENIGMATH() => TestSolution();
    }
}
