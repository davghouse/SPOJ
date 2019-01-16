using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._6___Emperor
{
    [TestClass]
    public sealed class POUR1Tests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.POUR1;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"26
5
16
1
5
16
2
5
16
3
5
16
4
5
16
5
5
16
6
5
16
7
16
5
8
16
5
9
5
16
10
5
16
11
5
16
12
5
16
13
5
16
14
5
16
15
5
16
16
9
21
1
9
21
2
9
21
3
9
21
4
21
9
6
21
9
9
21
9
11
21
9
12
21
9
15
21
9
14"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"6
14
16
8
1
4
12
20
12
4
2
10
18
14
6
1
-1
-1
4
-1
6
1
-1
2
8
-1
"
        };

        [TestMethod]
        public void POUR1() => TestSolution();
    }
}
