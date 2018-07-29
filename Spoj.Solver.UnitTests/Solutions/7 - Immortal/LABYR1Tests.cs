using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._7___Immortal
{
    [TestClass]
    public sealed class LABYR1Tests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.LABYR1;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"6
3 3
###
#.#
###
7 6
#######
#.#.###
#.#.###
#.#.#.#
#.....#
#######
8 1
##.....#
9 1
####..###
1 9
#
#
#
#
.
.
#
#
#
1 3
#
.
#"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"Maximum rope length is 0.
Maximum rope length is 8.
Maximum rope length is 4.
Maximum rope length is 1.
Maximum rope length is 1.
Maximum rope length is 0.
"
        };

        [TestMethod]
        public void LABYR1() => TestSolution();
    }
}
