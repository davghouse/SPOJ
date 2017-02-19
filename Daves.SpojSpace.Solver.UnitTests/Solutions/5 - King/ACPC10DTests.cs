using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Daves.SpojSpace.Solver.UnitTests.Solutions._5___King
{
    [TestClass]
    public sealed class ACPC10DTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.ACPC10D;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"4
13 7 5
7 13 6
14 3 12
15 6 16
0"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"1. 22
"
        };

        [TestMethod]
        public void ACPC10D() => TestSolution();
    }
}
