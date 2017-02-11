using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Daves.SpojSpace.Solver.UnitTests.Solutions._2___Chieftain
{
    [TestClass]
    public sealed class ADDREVTests : SolutionTestsBase
    {
        public override string SolutionSource => Daves.SpojSpace.Solver.Properties.Resources.ADDREV;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"3
24 1
4358 754
305 794"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"34
1998
1
"
        };

        [TestMethod]
        public void ADDREV() => TestSolution();
    }
}
