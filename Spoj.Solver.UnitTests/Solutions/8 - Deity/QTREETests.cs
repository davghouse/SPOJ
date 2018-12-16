using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Spoj.Solver.UnitTests.Solutions._8___Deity
{
    [TestClass]
    public sealed class QTREETests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.QTREE;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"2

3
1 2 1
2 3 2
QUERY 1 2
CHANGE 1 3
QUERY 1 2
DONE

3
1 2 1
2 3 2
QUERY 1 2
CHANGE 1 3
QUERY 1 2
DONE"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"1
3
1
3
"
        // For performance reasons FastIO new lines are \n instead of \r\n.
        }.Select(o => o.Replace(Environment.NewLine, "\n")).ToArray();

        [TestMethod]
        public void QTREE() => TestSolution();
    }
}
