using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._5___King
{
    [TestClass]
    public sealed class BEENUMSTests : SolutionTestsBase
    {
        public override string SolutionSource => Spoj.Solver.Properties.Resources.BEENUMS;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"43
1
7
19
15
-1"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"N
Y
Y
Y
N
"
        };

        [TestMethod]
        public void BEENUMS() => TestSolution();
    }
}
