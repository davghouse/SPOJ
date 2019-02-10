using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._6___Emperor
{
    [TestClass]
    public sealed class ANARC05HTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.ANARC05H;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"00000000000000000000000
1111111324234
1431994300431
0094391054430401354
0
1
2
3
4
5
1340
1304304134
054201348843214
001401040101010
14
1340
001
12040
34013
40
9876543210
bye",
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"1. 4194304
2. 192
3. 21
4. 96
5. 1
6. 1
7. 1
8. 1
9. 1
10. 1
11. 4
12. 29
13. 64
14. 40
15. 2
16. 4
17. 4
18. 6
19. 4
20. 1
21. 4
",
        };

        [TestMethod]
        public void ANARC05H() => TestSolution();
    }
}
