using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._5___King
{
    [TestClass]
    public sealed class MAJOR_v1Tests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.MAJOR_v1;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"3
4
2 1 2 2
6
1 1 1 2 2 2
5
1 2 4 5 1"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"YES 2
NO
NO
"
        };

        [TestMethod]
        public void MAJOR_v1() => TestSolution();
    }
}
