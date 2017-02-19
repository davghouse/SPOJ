using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Daves.SpojSpace.Solver.UnitTests.Solutions._5___King
{
    [TestClass]
    public sealed class SUMITRTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.SUMITR;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"2
3
1
2 1
1 2 3
4
1
1 2
4 1 2
2 3 1 1"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"5
9
"
        };

        [TestMethod]
        public void SUMITR() => TestSolution();
    }
}
