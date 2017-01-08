using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._5___King
{
    [TestClass]
    public sealed class TRICOUNTTests : SolutionTestsBase
    {
        public override string SolutionSource => Spoj.Solver.Properties.Resources.TRICOUNT;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"3
1
2
3"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"1
5
13
"
        };

        [TestMethod]
        public void TRICOUNT() => TestSolution();
    }
}
