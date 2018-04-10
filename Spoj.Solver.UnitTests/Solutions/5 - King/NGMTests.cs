using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._5___King
{
    [TestClass]
    public sealed class NGMTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.NGM;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"14"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"1
4
"
        };

        [TestMethod]
        public void NGM() => TestSolution();
    }
}
