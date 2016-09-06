using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spoj.Library.Primes;
using System.Linq;

namespace Spoj.Library.Tests.Primes
{
    [TestClass]
    public class PrimeProviderTests
    {
        private static int[] primesUpTo2 = new[] { 2 };
        private static int[] primesUpTo40 = new[] { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37 };

        [TestMethod]
        public void VerifiesSieveOfEratosthenesProvider()
        {
            var provider = new SieveOfEratosthenesProvider(2);
            Assert.IsTrue(primesUpTo2.SequenceEqual(provider.Primes));

            provider = new SieveOfEratosthenesProvider(40);
            Assert.IsTrue(primesUpTo40.SequenceEqual(provider.Primes));
        }
    }
}
