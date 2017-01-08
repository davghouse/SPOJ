using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._6___Emperor
{
    [TestClass]
    public sealed class NHAYTests : SolutionTestsBase
    {
        public override string SolutionSource => Spoj.Solver.Properties.Resources.NHAY;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"2
na
banananobano
6
foobar
foo
9
foobarfoo
barfoobarfoobarfoobarfoobarfoo"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"2
4

3
9
15
21
"
        };

        [TestMethod]
        public void NHAY() => TestSolution();
    }
}
