using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace SpojSpace.Solver.UnitTests.Solutions._5___King
{
    [TestClass]
    public sealed class STPARTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.STPAR;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"5
5 1 2 4 3
0"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"yes
"
        };

        [TestMethod]
        public void STPAR() => TestSolution();
    }
}
