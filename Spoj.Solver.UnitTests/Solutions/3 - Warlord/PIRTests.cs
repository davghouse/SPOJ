using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._3___Warlord
{
    [TestClass]
    public sealed class PIRTests : SolutionTestsBase
    {
        public override string SolutionSource => Spoj.Solver.Properties.Resources.PIR;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"2
1 1 1 1 1 1
1000 1000 1000 3 4 5"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"0.1179
1999.9937
"
        };

        [TestMethod]
        public void PIR() => TestSolution();
    }
}
