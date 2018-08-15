using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Spoj.Library.UnitTests.IO
{
    [TestClass]
    public class SlowestIOTests : IOTestsBase
    {
        public override string TestSource =>
@"using Spoj.Library.IO;

public static class Program
{
    private static void Main()
    {
        int n = int.Parse(SlowestIO.ReadLine());
        for (int i = 0; i < n; ++i)
        {
            SlowestIO.WriteLine(int.Parse(SlowestIO.ReadLine()));
        }
    }
}";

        [TestMethod]
        public void SlowestIO() => Test();
    }
}
