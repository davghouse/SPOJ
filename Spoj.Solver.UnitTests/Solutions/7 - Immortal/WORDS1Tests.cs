using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._7___Immortal
{
    [TestClass]
    public sealed class WORDS1Tests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.WORDS1;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"3
2
acm
ibm
3
acm
malform
mouse
2
ok
ok
4",
@"1
10
ab
ab
ab
ab
bc
bc
bc
ca
ca
ca"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"The door cannot be opened.
Ordering is possible.
The door cannot be opened.
",
@"Ordering is possible.
"
        };

        [TestMethod]
        public void WORDS1() => TestSolution();
    }
}
