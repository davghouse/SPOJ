using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Spoj.Solver.UnitTests.Solutions._8___Deity
{
    [TestClass]
    public sealed class MULTQ3Tests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.MULTQ3;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"4 7
1 0 3
0 1 2
0 1 3
1 0 0
0 0 3
1 3 3
1 0 3"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"4
1
0
2
"
        // For performance reasons FastIO new lines are \n instead of \r\n.
        }.Select(o => o.Replace(Environment.NewLine, "\n")).ToArray();

        [TestMethod]
        public void MULTQ3() => TestSolution();
    }
}
