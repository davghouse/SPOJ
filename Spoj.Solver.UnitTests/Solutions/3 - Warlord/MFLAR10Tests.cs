using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._3___Warlord
{
    [TestClass]
    public sealed class MFLAR10Tests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.MFLAR10;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"Flowers Flourish from France
Sam Simmonds speaks softly
Peter pIckEd pePPers
truly tautograms triumph
this is NOT a tautogram
*"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"Y
Y
Y
Y
N
"
        };

        [TestMethod]
        public void MFLAR10() => TestSolution();
    }
}
