using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._4___Prince
{
    [TestClass]
    public sealed class CPRMTTests : SolutionTestsBase
    {
        public override string SolutionSource => Spoj.Solver.Properties.Resources.CPRMT;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"pretty
women
walking
down
the
street"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"e
nw
et
"
        };

        [TestMethod]
        public void CPRMT() => TestSolution();
    }
}
