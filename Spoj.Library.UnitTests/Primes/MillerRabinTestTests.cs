using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spoj.Library.Primes;
using System.Linq;

namespace Spoj.Library.UnitTests.Primes
{
    [TestClass]
    public class MillerRabinTestTests
    {
        private static int[] _primesUpTo49 = new[] { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47 };

        [TestMethod]
        public void MillerRabinTestForSmallNumbers()
        {
            for (int n = 0; n <= 49; ++n)
            {
                Assert.AreEqual(_primesUpTo49.Contains(n), MillerRabinTest.Run((ulong)n));
            }
        }

        [TestMethod]
        public void MillerRabinTestForBigNumbers()
        {
            Assert.AreEqual(false, MillerRabinTest.Run(922337203685477580));
            Assert.AreEqual(false, MillerRabinTest.Run(4294967296));
            Assert.AreEqual(false, MillerRabinTest.Run(2073920666));
            Assert.AreEqual(false, MillerRabinTest.Run(825265));
            Assert.AreEqual(false, MillerRabinTest.Run(677789936));
            Assert.AreEqual(false, MillerRabinTest.Run(8911));
            Assert.AreEqual(false, MillerRabinTest.Run(5394826801));
            Assert.AreEqual(true, MillerRabinTest.Run(2147483629));
            Assert.AreEqual(true, MillerRabinTest.Run(2147483647));
            Assert.AreEqual(false, MillerRabinTest.Run(2102361656));
            Assert.AreEqual(false, MillerRabinTest.Run(757957974));
            Assert.AreEqual(true, MillerRabinTest.Run(2097665813));
            Assert.AreEqual(false, MillerRabinTest.Run(68812861848471));
            Assert.AreEqual(false, MillerRabinTest.Run(1592123869));
            Assert.AreEqual(false, MillerRabinTest.Run(1304627679));
            Assert.AreEqual(true, MillerRabinTest.Run(1000000007));
            Assert.AreEqual(true, MillerRabinTest.Run(104707));
            Assert.AreEqual(true, MillerRabinTest.Run(104711));
            Assert.AreEqual(true, MillerRabinTest.Run(104717));
            Assert.AreEqual(true, MillerRabinTest.Run(961748941));
            Assert.AreEqual(true, MillerRabinTest.Run(982451653));
            Assert.AreEqual(false, MillerRabinTest.Run(2455921));
            Assert.AreEqual(false, MillerRabinTest.Run(512461));
            Assert.AreEqual(true, MillerRabinTest.Run(175292000011));
            Assert.AreEqual(false, MillerRabinTest.Run(175292000065));
            Assert.AreEqual(true, MillerRabinTest.Run(175292000069));
        }
    }
}
