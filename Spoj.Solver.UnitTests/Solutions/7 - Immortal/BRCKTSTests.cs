using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._7___Immortal
{
    [TestClass]
    public sealed class BRCKTSTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.BRCKTS;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"8
((()))()
5
0
4
0
4
0
8
((()()()
5
0
4
0
4
0
8
()()))()
5
0
4
0
4
0
8
((()))))
5
0
4
0
4
0
8
(((())()
5
0
4
0
4
0
8
))()))()
5
0
4
0
4
0
8
(())))()
5
0
4
0
4
0
8
((()))()
5
0
4
0
4
0
8
))))))((
5
0
4
0
4
0
8
()()()()
5
0
4
0
4
0"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"Test 1:
YES
NO
YES
Test 2:
NO
NO
NO
Test 3:
NO
YES
NO
Test 4:
NO
YES
NO
Test 5:
NO
YES
NO
Test 6:
NO
NO
NO
Test 7:
NO
YES
NO
Test 8:
YES
NO
YES
Test 9:
NO
NO
NO
Test 10:
YES
NO
YES
"
        };

        [TestMethod]
        public void BRCKTS() => TestSolution();
    }
}
