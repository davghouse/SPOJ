using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Daves.SpojSpace.Solver.UnitTests.Solutions._4___Prince
{
    [TestClass]
    public sealed class ARITH2Tests : SolutionTestsBase
    {
        public override string SolutionSource => Daves.SpojSpace.Solver.Properties.Resources.ARITH2;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"4

1 + 1 * 2 =

29 / 5 =

103 * 103 * 5 =

50 * 40 * 250 + 791 ="
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"4
5
53045
500791
"
        };

        [TestMethod]
        public void ARITH2() => TestSolution();
    }
}
