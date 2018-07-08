using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._5___King
{
    [TestClass]
    public sealed class ABCPATHTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.ABCPATH;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"4 3
ABE
CFG
BDH
ABC
0 0",
@"4 7
ABKLMXY
CIJNOVZ
FDHPQUZ
EGZRSTA
1 2
AA
1 1
A
1 1
B
4 7
ABKLMWX
CIJNOVY
FDHPQUZ
EGZRSTA
0 0"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"Case 1: 4
",
@"Case 1: 22
Case 2: 1
Case 3: 1
Case 4: 0
Case 5: 26
"
        };

        [TestMethod]
        public void ABCPATH() => TestSolution();
    }
}
