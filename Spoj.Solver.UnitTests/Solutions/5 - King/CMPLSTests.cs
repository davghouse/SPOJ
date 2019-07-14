using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Spoj.Solver.UnitTests.Solutions._5___King
{
    [TestClass]
    public sealed class CMPLSTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.CMPLS;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"4
6 3
1 2 3 4 5 6
8 2
1 2 4 7 11 16 22 29
10 2
1 1 1 1 1 1 1 1 1 2
1 10
3"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"7 8 9 
37 46 
11 56 
3 3 3 3 3 3 3 3 3 3 
"
        // For performance reasons FastIO new lines are \n instead of \r\n.
        }.Select(o => o.Replace(Environment.NewLine, "\n")).ToArray();

        [TestMethod]
        public void CMPLS() => TestSolution();
    }
}
