using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._5___King
{
    [TestClass]
    public sealed class TDPRIMESTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.TDPRIMES;
        public override IReadOnlyList<string> TestInputs => new[] { "" };
        public override IReadOnlyList<string> TestOutputs => new[] { "" };

        public string OutputStart =>
@"2
547
1229";

        public string OutputEnd =>
@"99995257
99996931
99998953
";

        [TestMethod]
        public void TDPRIMES() => TestSolution();

        protected override void VerifyOutput(string expectedOutput, string actualOutput)
        {
            Assert.IsTrue(actualOutput.StartsWith(OutputStart));
            Assert.IsTrue(actualOutput.EndsWith(OutputEnd));
        }
    }
}
