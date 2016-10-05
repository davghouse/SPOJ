using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spoj.Library.Primes;
using System.Linq;

namespace Spoj.Library.UnitTests.Primes
{
    [TestClass]
    public class PrimeDeciderTests
    {
        private static int[] primesUpTo2 = new[] { 2 };
        private static int[] primesUpTo49 = new[] { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47 };

        [TestMethod]
        public void VerifiesDecidersAgainstKnownOutput()
        {
            var sieveDecider = new SieveOfEratosthenesDecider(2);
            var sieveProvider = new SieveOfEratosthenesProvider(2);
            var sieveFactorizer = new SieveOfEratosthenesFactorizer(2);
            var trialDivisionDecider = new TrialDivisionDecider(2);
            for (int n = 0; n <= 2; ++n)
            {
                Assert.AreEqual(primesUpTo2.Contains(n), sieveDecider.IsPrime(n));
                Assert.AreEqual(primesUpTo2.Contains(n), sieveProvider.IsPrime(n));
                Assert.AreEqual(primesUpTo2.Contains(n), sieveFactorizer.IsPrime(n));
                Assert.AreEqual(primesUpTo2.Contains(n), trialDivisionDecider.IsPrime(n));
                Assert.AreEqual(primesUpTo2.Contains(n), NaivePrimeDeciderProviderFactorizer.IsPrime(n));
            }

            sieveDecider = new SieveOfEratosthenesDecider(49);
            sieveProvider = new SieveOfEratosthenesProvider(49);
            sieveFactorizer = new SieveOfEratosthenesFactorizer(49);
            trialDivisionDecider = new TrialDivisionDecider(49);
            for (int n = 0; n <= 49; ++n)
            {
                Assert.AreEqual(primesUpTo49.Contains(n), sieveDecider.IsPrime(n));
                Assert.AreEqual(primesUpTo49.Contains(n), sieveProvider.IsPrime(n));
                Assert.AreEqual(primesUpTo49.Contains(n), sieveFactorizer.IsPrime(n));
                Assert.AreEqual(primesUpTo49.Contains(n), trialDivisionDecider.IsPrime(n));
                Assert.AreEqual(primesUpTo49.Contains(n), NaivePrimeDeciderProviderFactorizer.IsPrime(n));
            }
        }

        [TestMethod]
        public void VerifiesDecidersAgainstNaiveDecider()
        {
            for (int i = 1000; i <= 10000; i += 1000)
            {
                var sieveDecider = new SieveOfEratosthenesDecider(i);
                var sieveProvider = new SieveOfEratosthenesProvider(i);
                var sieveFactorizer = new SieveOfEratosthenesFactorizer(i);
                var trialDivisionDecider = new TrialDivisionDecider(i);
                for (int n = 0; n <= i; ++n)
                {
                    Assert.AreEqual(NaivePrimeDeciderProviderFactorizer.IsPrime(n), sieveDecider.IsPrime(n));
                    Assert.AreEqual(NaivePrimeDeciderProviderFactorizer.IsPrime(n), sieveProvider.IsPrime(n));
                    Assert.AreEqual(NaivePrimeDeciderProviderFactorizer.IsPrime(n), sieveFactorizer.IsPrime(n));
                    Assert.AreEqual(NaivePrimeDeciderProviderFactorizer.IsPrime(n), trialDivisionDecider.IsPrime(n));
                }
            }
        }
    }
}
