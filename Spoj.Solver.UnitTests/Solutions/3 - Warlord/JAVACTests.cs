using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._3___Warlord
{
    [TestClass]
    public sealed class JAVACTests : SolutionTestsBase
    {
        public override string SolutionSource => Spoj.Solver.Properties.Resources.JAVAC;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"long_and_mnemonic_identifier
anotherExample
i
bad_Style"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"longAndMnemonicIdentifier
another_example
i
Error!
"
        };

        [TestMethod]
        public void JAVAC() => TestSolution();
    }
}
