using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._5___King
{
    [TestClass]
    public sealed class CANTONTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.CANTON;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"3
3
14
7"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"TERM 3 IS 2/1
TERM 14 IS 2/4
TERM 7 IS 1/4
"
        };

        [TestMethod]
        public void CANTON() => TestSolution();
    }
}
