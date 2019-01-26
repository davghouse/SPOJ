using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._4___Prince
{
    [TestClass]
    public sealed class CUBEFRTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.CUBEFR;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"6
498
64851
458
12347
48468
4875"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"Case 1: 416
Case 2: 53953
Case 3: 383
Case 4: 10274
Case 5: 40324
Case 6: Not Cube Free
"
        };

        [TestMethod]
        public void CUBEFR() => TestSolution();
    }
}
