using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._4___Prince
{
    [TestClass]
    public sealed class FARIDATests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.FARIDA;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"2
5
1 2 3 4 5
1
10"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"Case 1: 9
Case 2: 10
"
        };

        [TestMethod]
        public void FARIDA() => TestSolution();
    }
}
