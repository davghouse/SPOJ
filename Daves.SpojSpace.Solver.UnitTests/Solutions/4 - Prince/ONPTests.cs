using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Daves.SpojSpace.Solver.UnitTests.Solutions._4___Prince
{
    [TestClass]
    public sealed class ONPTests : SolutionTestsBase
    {
        public override string SolutionSource => Daves.SpojSpace.Solver.Properties.Resources.ONP;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"3
(a+(b*c))
((a+b)*(z+x))
((a+t)*((b+(a+c))^(c+d)))"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"abc*+
ab+zx+*
at+bac++cd+^*
"
        };

        [TestMethod]
        public void ONP() => TestSolution();
    }
}
