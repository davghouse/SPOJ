using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Spoj.Solver.UnitTests.Solutions._7___Immortal
{
    [TestClass]
    public sealed class QTREE2Tests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.QTREE2;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"1

6
1 2 1
2 4 1
2 5 2
1 3 1
3 6 2
DIST 4 6
DIST 4 4
DIST 5 2
KTH 4 6 4
KTH 4 4 1
KTH 1 6 3
KTH 1 1 1
KTH 2 1 1
KTH 2 1 2
DONE
"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"5
0
2
3
4
6
1
2
1
"
        // For performance reasons FastIO new lines are \n instead of \r\n.
        }.Select(o => o.Replace(Environment.NewLine, "\n")).ToArray();

        [TestMethod]
        public void QTREE2() => TestSolution();
    }
}
