using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._4___Prince
{
    [TestClass]
    public sealed class LENGFACTTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.LENGFACT;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"4
1
10
100
5000000000"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"1
7
158
46323377618
"
        };

        [TestMethod]
        public void LENGFACT() => TestSolution();
    }
}
