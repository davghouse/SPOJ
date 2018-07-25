using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._7___Immortal
{
    [TestClass]
    public sealed class SHPATHTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.SHPATH;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"2
4
gdansk
2
2 1
3 3
bydgoszcz
3
1 1
3 1
4 4
torun
3
1 3
2 1
4 1
warszawa
2
2 4
3 1
2
gdansk warszawa
bydgoszcz warszawa

4
gdansk
2
2 1
3 3
bydgoszcz
3
1 1
3 1
4 4
torun
3
1 3
2 1
4 1
warszawa
2
2 4
3 1
2
gdansk warszawa
bydgoszcz warszawa",
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"3
2
3
2
"
        };

        [TestMethod]
        public void SHPATH() => TestSolution();
    }
}
