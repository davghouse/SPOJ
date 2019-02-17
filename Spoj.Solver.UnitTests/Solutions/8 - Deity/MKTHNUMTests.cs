using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Spoj.Solver.UnitTests.Solutions._8___Deity
{
    [TestClass]
    public sealed class MKTHNUMTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.MKTHNUM;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"7 3
1 5 2 6 3 7 4
2 5 3
4 4 1
1 7 3",
@"
5 3
-1 -2 3 4 5
1 5 1
1 5 3
2 3 1",
@"7 3
5 4 3 2 1 0 -1
1 3 2
3 6 1
1 6 4"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"5
6
3
",
@"-2
3
-2
",
@"4
0
3
"
        // For performance reasons FastIO new lines are \n instead of \r\n.
        }.Select(o => o.Replace(Environment.NewLine, "\n")).ToArray();

        [TestMethod]
        public void MKTHNUM() => TestSolution();
    }
}
