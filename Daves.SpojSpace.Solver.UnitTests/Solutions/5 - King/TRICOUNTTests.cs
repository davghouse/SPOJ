using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Daves.SpojSpace.Solver.UnitTests.Solutions._5___King
{
    [TestClass]
    public sealed class TRICOUNTTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.TRICOUNT;

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
