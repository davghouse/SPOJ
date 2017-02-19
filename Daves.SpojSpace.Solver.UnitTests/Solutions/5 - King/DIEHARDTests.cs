using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Daves.SpojSpace.Solver.UnitTests.Solutions._5___King
{
    [TestClass]
    public sealed class DIEHARDTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.DIEHARD;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"3
2 10
4 4
20 8"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"1
1
5
"
        };

        [TestMethod]
        public void DIEHARD() => TestSolution();
    }
}
