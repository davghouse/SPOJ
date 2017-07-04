using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace SpojSpace.Solver.UnitTests.Solutions._5___King
{
    [TestClass]
    public sealed class BYTESM2Tests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.BYTESM2;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"1
6 5
3 1 7 4 2
2 1 3 1 1
1 2 2 1 8
2 2 1 5 3
2 1 4 4 4
5 2 7 5 1",
@"1
4 5
12 44 8 1 2
1 4 5 2 3
2 5 8 7 6
2 5 8 9 6"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"32
",
@"66
"
        };

        [TestMethod]
        public void BYTESM2() => TestSolution();
    }
}
