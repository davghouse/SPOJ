using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._6___Emperor
{
    [TestClass]
    public sealed class IOIPALINTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.IOIPALIN;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"5
Ab3bd",
@"26
00OJKOE234EKaddOTJWX53KSQQ"

        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
"2",
"19"
        };

        [TestMethod]
        public void IOIPALIN() => TestSolution();
    }
}
