using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Daves.SpojSpace.Solver.UnitTests.Solutions._5___King
{
    [TestClass]
    public sealed class ANARC05BTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.ANARC05B;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"13 3 5 7 9 20 25 30 40 55 56 57 60 62
11 1 4 7 11 14 25 44 47 55 57 100
4 -5 100 1000 1005
3 -12 1000 1001
0"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"450
2100
"
        };

        [TestMethod]
        public void ANARC05B() => TestSolution();
    }
}
