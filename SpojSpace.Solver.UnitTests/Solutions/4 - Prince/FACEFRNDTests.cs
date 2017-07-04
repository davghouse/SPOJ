using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace SpojSpace.Solver.UnitTests.Solutions._4___Prince
{
    [TestClass]
    public sealed class FACEFRNDTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.FACEFRND;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"3
2334 5 1256 4323 7687 3244 5678
1256 2 2334 7687
4323 5 2334 5678 6547 9766 9543"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"6
"
        };

        [TestMethod]
        public void FACEFRND() => TestSolution();
    }
}
