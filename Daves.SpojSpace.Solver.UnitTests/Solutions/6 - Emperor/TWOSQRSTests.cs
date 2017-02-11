using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Daves.SpojSpace.Solver.UnitTests.Solutions._6___Emperor
{
    [TestClass]
    public sealed class TWOSQRSTests : SolutionTestsBase
    {
        public override string SolutionSource => Daves.SpojSpace.Solver.Properties.Resources.TWOSQRS;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"10
1
2
7
14
49
9
17
76
2888
27"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"Yes
Yes
No
No
Yes
Yes
Yes
No
Yes
No
"
        };

        [TestMethod]
        public void TWOSQRS() => TestSolution();
    }
}
