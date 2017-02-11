using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Daves.SpojSpace.Solver.UnitTests.Solutions._4___Prince
{
    [TestClass]
    public sealed class HPYNOSTests : SolutionTestsBase
    {
        public override string SolutionSource => Daves.SpojSpace.Solver.Properties.Resources.HPYNOS;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"19",
@"204",
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"4
",
@"-1
"
        };

        [TestMethod]
        public void HPYNOS() => TestSolution();
    }
}
