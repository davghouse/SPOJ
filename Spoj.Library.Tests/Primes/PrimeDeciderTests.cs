using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spoj.Library.Primes;
using System.Linq;

namespace Spoj.Library.Tests.Primes
{
    [TestClass]
    public class PrimeDeciderTests
    {
        private static int[] primesUpTo2 = new[] { 2 };
        private static int[] primesUpTo40 = new[] { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37 };

        [TestMethod]
        public void VerifiesSieveOfEratosthenesDecider()
        {
            var decider = new SieveOfEratosthenesDecider(2);
            for (int n = 0; n <= 2; ++n)
            {
                Assert.AreEqual(primesUpTo2.Contains(n), decider.IsPrime(n));
            }

            decider = new SieveOfEratosthenesDecider(40);
            for (int n = 0; n <= 40; ++n)
            {
                Assert.AreEqual(primesUpTo40.Contains(n), decider.IsPrime(n));
            }
        }

        [TestMethod]
        public void VerifiesTrialDivisionDecider()
        {
            var decider = new TrialDivisionDecider(2);
            for (int n = 0; n <= 2; ++n)
            {
                Assert.AreEqual(primesUpTo2.Contains(n), decider.IsPrime(n));
            }

            decider = new TrialDivisionDecider(40);
            for (int n = 0; n <= 40; ++n)
            {
                Assert.AreEqual(primesUpTo40.Contains(n), decider.IsPrime(n));
            }
        }
    }
}
