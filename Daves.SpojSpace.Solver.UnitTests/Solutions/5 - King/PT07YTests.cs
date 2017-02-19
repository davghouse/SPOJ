using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Daves.SpojSpace.Solver.UnitTests.Solutions._5___King
{
    [TestClass]
    public sealed class PT07YTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.PT07Y;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"3 2
1 2
2 3"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"YES
"
        };

        [TestMethod]
        public void PT07Y() => TestSolution();
    }
}
