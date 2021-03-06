﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Spoj.Solver.UnitTests.Solutions._5___King
{
    [TestClass]
    public sealed class ACODE_v1Tests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.ACODE_v1;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"25114
1111111111
3333333333
0"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"6
89
1
"
        };

        [TestMethod]
        public void ACODE_v1() => TestSolution();
    }
}
