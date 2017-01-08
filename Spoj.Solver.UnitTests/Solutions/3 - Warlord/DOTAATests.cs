using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._3___Warlord
{
    [TestClass]
    public sealed class DOTAATests : SolutionTestsBase
    {
        public override string SolutionSource => Spoj.Solver.Properties.Resources.DOTAA;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"3
6 3 400
500
500
500
500
500
500
6 5 400
800
800
801
200
200
200
6 3 400
401
401
400
200
400
200"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"YES
NO
NO
"
        };

        [TestMethod]
        public void DOTAA() => TestSolution();
    }
}
