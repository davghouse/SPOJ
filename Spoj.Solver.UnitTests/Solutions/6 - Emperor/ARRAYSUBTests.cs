using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._6___Emperor
{
    [TestClass]
    public sealed class ARRAYSUBTests : SolutionTestsBase
    {
        public override string SolutionSource => Spoj.Solver.Properties.Resources.ARRAYSUB;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"9
1 2 3 1 4 5 2 3 6
3"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"3 3 4 5 5 5 6
"
        };

        [TestMethod]
        public void ARRAYSUB() => TestSolution();
    }
}
