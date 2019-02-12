using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Spoj.Solver.UnitTests.Solutions._7___Immortal
{
    [TestClass]
    public sealed class FREQUENTTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.FREQUENT;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"10 4
-1 -1 1 1 1 1 3 10 10 10
2 3
1 10
5 10
1 1
0"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"1
4
3
1
"
        // For performance reasons FastIO new lines are \n instead of \r\n.
        }.Select(o => o.Replace(Environment.NewLine, "\n")).ToArray();

        [TestMethod]
        public void FREQUENT() => TestSolution();
    }
}
