using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Spoj.Solver.UnitTests.Solutions._5___King
{
    [TestClass]
    public sealed class ZSUMTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.ZSUM;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"10 3
9 31
83 17
5 2
200000000 1000000
0 0"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"4835897
2118762
2285275
3694
1638201
"
        // For performance reasons FastIO new lines are \n instead of \r\n.
        }.Select(o => o.Replace(Environment.NewLine, "\n")).ToArray();

        [TestMethod]
        public void ZSUM() => TestSolution();
    }
}
