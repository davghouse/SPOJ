using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Spoj.Solver.UnitTests.Solutions._8___Deity
{
    [TestClass]
    public sealed class KQUERYTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.KQUERY;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"5
5 1 2 3 4
3
2 4 1
4 4 4
1 5 2"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"2
0
3
"
        // For performance reasons FastIO new lines are \n instead of \r\n.
        }.Select(o => o.Replace(Environment.NewLine, "\n")).ToArray();

        [TestMethod]
        public void KQUERY() => TestSolution();
    }
}
