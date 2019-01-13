using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Spoj.Solver.UnitTests.Solutions._7___Immortal
{
    [TestClass]
    public sealed class UPDATEITTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.UPDATEIT;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"
1
5 3
0 1 7
2 4 6
1 3 2
3
0
3
4"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"7
8
6
"
        // For performance reasons FastIO new lines are \n instead of \r\n.
        }.Select(o => o.Replace(Environment.NewLine, "\n")).ToArray();

        [TestMethod]
        public void UPDATEIT() => TestSolution();
    }
}
