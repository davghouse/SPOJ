using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Daves.SpojSpace.Solver.UnitTests.Solutions._5___King
{
    [TestClass]
    public sealed class PRIME1Tests : SolutionTestsBase
    {
        public override string SolutionSource => Daves.SpojSpace.Solver.Properties.Resources.PRIME1;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"2
1 10
3 5"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"2
3
5
7

3
5

"
        };

        [TestMethod]
        public void PRIME1() => TestSolution();
    }
}
