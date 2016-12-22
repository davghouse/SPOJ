using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spoj.Library.Primes;
using System.Linq;

namespace Spoj.Library.UnitTests.Primes
{
    [TestClass]
    public sealed class PrimeProviderTests
    {
        private static int[] _primesUpTo2 = new[] { 2 };
        private static int[] _primesUpTo49 = new[] { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47 };

        [TestMethod]
        public void VerifiesProvidersAgainstKnownOutput()
        {
            var sieveProvider = new SieveOfEratosthenesProvider(2);
            Assert.IsTrue(_primesUpTo2.SequenceEqual(sieveProvider.Primes));
            Assert.IsTrue(_primesUpTo2.SequenceEqual(NaivePrimeDeciderProviderFactorizer.GetPrimes(2)));

            sieveProvider = new SieveOfEratosthenesProvider(49);
            Assert.IsTrue(_primesUpTo49.SequenceEqual(sieveProvider.Primes));
            Assert.IsTrue(_primesUpTo49.SequenceEqual(NaivePrimeDeciderProviderFactorizer.GetPrimes(49)));
        }

        [TestMethod]
        public void VerifiesProvidersAgainstNaiveProvider()
        {
            for (int n = 1000; n <= 10000; n += 1000)
            {
                var sieveProvider = new SieveOfEratosthenesProvider(n);
                Assert.IsTrue(NaivePrimeDeciderProviderFactorizer.GetPrimes(n).SequenceEqual(sieveProvider.Primes));
            }
        }
    }
}
