using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace SpojSpace.Solver.UnitTests.Solutions._5___King
{
    [TestClass]
    public sealed class LASTDIGTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.LASTDIG;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"2
3 10
6 2"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"9
6
"
        };

        [TestMethod]
        public void LASTDIG() => TestSolution();
    }
}
