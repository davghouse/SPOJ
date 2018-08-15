using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Spoj.Library.UnitTests.IO
{
    [TestClass]
    public class SlowIOTests : IOTestsBase
    {
        public override string TestSource =>
@"using Spoj.Library.IO;

public static class Program
{
    private static void Main()
    {
        int n = int.Parse(SlowIO.ReadLine());
        for (int i = 0; i < n; ++i)
        {
            SlowIO.WriteLine(int.Parse(SlowIO.ReadLine()));
        }

        SlowIO.Flush();
    }
}";

        [TestMethod]
        public void SlowIO() => Test();
    }
}
