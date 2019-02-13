using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._6___Emperor
{
    [TestClass]
    public sealed class YODANESSTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.YODANESS;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"2
6
in the force strong you are
you are strong in the force
6
or i will help you not
or i will not help you",
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"11
2
",
        };

        [TestMethod]
        public void YODANESS() => TestSolution();
    }
}
