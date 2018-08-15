using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Spoj.Library.UnitTests.IO
{
    [TestClass]
    public class FastIOTests : IOTestsBase
    {
        public override IReadOnlyList<string> TestOutputs
            // For performance reasons FastIO new lines are \n instead of \r\n.
            => base.TestOutputs.Select(o => o.Replace(Environment.NewLine, "\n")).ToArray();

        public override string TestSource =>
@"using Spoj.Library.IO;

public static class Program
{
    private static void Main()
    {
        int n = FastIO.ReadPositiveInt();
        for (int i = 0; i < n; ++i)
        {
            FastIO.WriteLine(FastIO.ReadInt());
        }

        FastIO.Flush();
    }
}";

        [TestMethod]
        public void FastIO() => Test();
    }
}
