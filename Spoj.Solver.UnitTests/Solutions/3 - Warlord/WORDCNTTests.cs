using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._3___Warlord
{
    [TestClass]
    public sealed class WORDCNTTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.WORDCNT;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"5
a aa bb cc def ghi
a a a a a bb bb bb bb c c	

        
a a a "
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"3
5
0
0
3
"
        };

        [TestMethod]
        public void WORDCNT() => TestSolution();
    }
}
