using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._4___Prince
{
    [TestClass]
    public sealed class EASYPROBTests : SolutionTestsBase
    {
        public override string SolutionSource => Spoj.Solver.Properties.Resources.EASYPROB;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"2
137
1315"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"137=2(2(2)+2+2(0))+2(2+2(0))+2(0)
1315=2(2(2+2(0))+2)+2(2(2+2(0)))+2(2(2)+2(0))+2+2(0)
"
        };

        [TestMethod]
        public void EASYPROB() => TestSolution();
    }
}
