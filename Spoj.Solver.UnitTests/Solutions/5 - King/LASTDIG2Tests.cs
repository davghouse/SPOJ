using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._5___King
{
    [TestClass]
    public sealed class LASTDIG2Tests : SolutionTestsBase
    {
        public override string SolutionSource => Spoj.Solver.Properties.Resources.LASTDIG2;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"3
3 10
6 2
150 53"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"9
6
0
"
        };

        [TestMethod]
        public void LASTDIG2() => TestSolution();
    }
}
