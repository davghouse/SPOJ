using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Spoj.Solver.UnitTests.Solutions._2___Chieftain
{
    [TestClass]
    public sealed class GUESSINGTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.GUESSING;

        public override IReadOnlyList<string> TestInputs { get { throw new NotImplementedException(); } }

        public override IReadOnlyList<string> TestOutputs { get { throw new NotImplementedException(); } }

        [TestMethod]
        public void GUESSING() => TestFormatting();
    }
}
