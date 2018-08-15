using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Spoj.Solver.UnitTests.Solutions._6___Emperor
{
    [TestClass]
    public sealed class INOUTESTTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.INOUTEST;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"5
-1 -1
1 1
0 999
654 321
39999 -39999"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"1
1
0
209934
-1599920001
"
        // For performance reasons FastIO new lines are \n instead of \r\n.
        }.Select(o => o.Replace(Environment.NewLine, "\n")).ToArray();

        [TestMethod]
        public void INOUTEST() => TestSolution();
    }
}
