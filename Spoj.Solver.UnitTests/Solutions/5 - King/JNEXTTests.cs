using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Spoj.Solver.UnitTests.Solutions._5___King
{
    [TestClass]
    public sealed class JNEXTTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.JNEXT;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"6
5
1 5 4 8 3
10
1 4 7 4 5 8 4 1 2 6
1
5
3
6 8 4
2
1 9
2
9 1"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"15834
1474584162
-1
846
91
-1
"
        // For performance reasons FastIO new lines are \n instead of \r\n.
        }.Select(o => o.Replace(Environment.NewLine, "\n")).ToArray();

        [TestMethod]
        public void JNEXT() => TestSolution();
    }
}
