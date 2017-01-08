using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._7___Immortal
{
    [TestClass]
    public sealed class HORRIBLE_v1Tests : SolutionTestsBase
    {
        public override string SolutionSource => Spoj.Solver.Properties.Resources.HORRIBLE_v1;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"1
8 6
0 2 4 26
0 4 8 80
0 4 5 20
1 8 8
0 5 7 14
1 4 8"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"80
508
"
        };

        [TestMethod]
        public void HORRIBLE_v1() => TestSolution();
    }
}
