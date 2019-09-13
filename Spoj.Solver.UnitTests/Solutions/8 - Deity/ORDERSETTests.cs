using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Spoj.Solver.UnitTests.Solutions._8___Deity
{
    [TestClass]
    public sealed class ORDERSETTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.ORDERSET;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"8
I -1
I -1
I 2
C 0
K 2
D -1
K 1
K 2"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"1
2
2
invalid
"
        };

        [TestMethod]
        public void ORDERSET() => TestSolution();
    }
}
