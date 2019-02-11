using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Spoj.Solver.UnitTests.Solutions._7___Immortal
{
    [TestClass]
    public sealed class LITETests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.LITE;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"4 5
0 1 2
0 2 4
1 2 3
0 2 4
1 1 4",
@"1000 20
0 78 767
0 3 1000
0 6 87
1 7 9
1 3 90
0 54 877
0 43 999
0 1 2
0 1 1
0 1 1
0 1 1
1 1 999
1 1 1000
0 1 123
0 98 374
1 450 999
0 87 87
1 98 434
0 65 444
1 4 909"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"1
2
",
@"0
13
135
136
110
251
263
"
        // For performance reasons FastIO new lines are \n instead of \r\n.
        }.Select(o => o.Replace(Environment.NewLine, "\n")).ToArray();

        [TestMethod]
        public void LITE() => TestSolution();
    }
}
