using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Spoj.Solver.UnitTests.Solutions._5___King
{
    [TestClass]
    public sealed class CAM5Tests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.CAM5;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"2

4
2
0 1
1 2

3
0"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"2
3
",
        // For performance reasons FastIO new lines are \n instead of \r\n.
        }.Select(o => o.Replace(Environment.NewLine, "\n")).ToArray();

        [TestMethod]
        public void CAM5() => TestSolution();
    }
}
