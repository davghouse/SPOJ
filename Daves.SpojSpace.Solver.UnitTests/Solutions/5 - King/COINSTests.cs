using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Daves.SpojSpace.Solver.UnitTests.Solutions._5___King
{
    [TestClass]
    public sealed class COINSTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.COINS;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"12
2",
@"100
1000
456
78
1
93468
100000000"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"13
2
",
@"120
1370
603
87
1
193063
364906343
"
        };

        [TestMethod]
        public void COINS() => TestSolution();
    }
}
