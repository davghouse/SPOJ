using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Spoj.Library.Tests
{
    [TestClass]
    public class BigIntegerTests
    {
        [TestMethod]
        public void Addition_AddsIntegersCorrectly()
        {
            BigInteger a = new BigInteger(117);
            BigInteger b = new BigInteger(245425);
            BigInteger c = new BigInteger(331);
            BigInteger abc = new BigInteger(117 + 245425 + 331);

            Assert.AreEqual(abc, a + b + c);
        }
    }
}
