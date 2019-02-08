using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._6___Emperor
{
    [TestClass]
    public sealed class NEG2Tests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.NEG2;

        public override IReadOnlyList<string> TestInputs => new[]
        {
"-2",
"-1",
"0",
"1",
"2",
"3",
"-2000000000",
"2000000000"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
"10",
"11",
"0",
"1",
"110",
"111",
"10011001110111111011110000000000",
"110001011010010101001010000000000"
        };

        [TestMethod]
        public void NEG2() => TestSolution();
    }
}
