using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Spoj.Solver.UnitTests.Solutions._7___Immortal
{
    [TestClass]
    public sealed class MATSUMTests : SolutionTestsBase
    {
        public override string SolutionSource => Solver.Solutions.MATSUM;

        public override IReadOnlyList<string> TestInputs => new[]
        {
@"2
4
SET 0 0 1
SUM 0 0 3 3
SET 2 2 12
SET 1 2 5
SET 0 2 7
SET 2 1 -8
SET 3 1 -3
SET 3 2 4
SET 3 3 5
SET 2 3 -2
SET 0 3 4
SET 1 3 -10
SUM 2 2 2 2
SUM 2 2 3 3
SUM 0 0 2 2
SET 2 2 -12
SUM 0 0 2 2
SET 0 0 6
SUM 0 0 2 2
SUM 1 1 2 3
SUM 0 0 2 3
SUM 1 2 3 2 
END
4
SET 0 0 1
SUM 0 0 3 3
SET 2 2 12
SUM 2 2 2 2
SUM 2 2 3 3
SUM 0 0 2 2
END"
        };

        public override IReadOnlyList<string> TestOutputs => new[]
        {
@"1
12
19
17
-7
-2
-27
-10
-3

1
12
12
13

"
        // For performance reasons FastIO new lines are \n instead of \r\n.
        }.Select(o => o.Replace(Environment.NewLine, "\n")).ToArray();

        [TestMethod]
        public void MATSUM() => TestSolution();
    }
}
