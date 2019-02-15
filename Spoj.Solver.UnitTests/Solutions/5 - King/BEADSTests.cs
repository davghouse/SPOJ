using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._5___King
{
    [TestClass]
    public sealed class BEADSTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.BEADS;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"6
helloworld
amandamanda
dontcallmebfu
aaabaaa
aaa
zzzbbbr"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"10
11
6
5
1
4
"
        };

        [TestMethod]
        public void BEADS() => TestSolution();
    }
}
