using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Spoj.Solver.UnitTests.Solutions._7___Immortal
{
    [TestClass]
    public sealed class ASSIGNTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.ASSIGN;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"3
3
1 1 1
1 1 1
1 1 1
11
1 0 0 1 0 0 0 0 0 1 1 
1 1 1 1 1 0 1 0 1 0 0 
1 0 0 1 0 0 1 1 0 1 0 
1 0 1 1 1 0 1 1 0 1 1 
0 1 1 1 0 1 0 0 1 1 1 
1 1 1 0 0 1 0 0 0 0 0 
0 0 0 0 1 0 1 0 0 0 1 
1 0 1 1 0 0 0 0 0 0 1 
0 0 1 0 1 1 0 0 0 1 1 
1 1 1 0 0 0 1 0 1 0 1 
1 0 0 0 1 1 1 1 0 0 0 
11
0 1 1 1 0 1 0 0 0 1 0 
0 0 1 1 1 1 1 1 1 1 1 
1 1 0 1 0 0 0 0 0 1 0 
0 1 0 1 0 1 0 1 0 1 1 
1 0 0 1 0 0 0 0 1 0 1 
0 0 1 0 1 1 0 0 0 0 1 
1 0 1 0 1 1 1 0 1 1 0 
1 0 1 1 0 1 1 0 0 1 0 
0 0 1 1 0 1 1 1 1 1 1 
0 1 0 0 0 0 0 0 0 1 1 
0 1 1 0 0 0 0 0 1 0 1"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"6
7588
7426
"
        // For performance reasons FastIO new lines are \n instead of \r\n.
        }.Select(o => o.Replace(Environment.NewLine, "\n")).ToArray();

        [TestMethod]
        public void ASSIGN() => TestSolution();
    }
}
