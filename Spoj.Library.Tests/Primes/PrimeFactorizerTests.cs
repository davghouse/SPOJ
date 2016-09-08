using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spoj.Library.Helpers;
using Spoj.Library.Primes;
using System;
using System.Linq;

namespace Spoj.Library.Tests.Primes
{
    [TestClass]
    public class PrimeFactorizerTests
    {
        private static Tuple<int, int[]>[] numberPrimeFactorsPairs = new[]
        {
            Tuple.Create(0, new int[] { }),
            Tuple.Create(1, new int[] { }),
            Tuple.Create(2, new[] { 2 }),
            Tuple.Create(3, new[] { 3 }),
            Tuple.Create(4, new[] { 2, 2 }),
            Tuple.Create(5, new[] { 5 }),
            Tuple.Create(56, new[] { 2, 2, 2, 7 }),
            Tuple.Create(68, new[] { 2, 2, 17 }),
            Tuple.Create(81, new[] { 3, 3, 3, 3 }),
            Tuple.Create(90, new[] { 2, 3, 3, 5 }),
            Tuple.Create(679, new[] { 7, 97 }),
            Tuple.Create(97, new[] { 97 }),
            Tuple.Create(428, new[] { 2, 2, 107 }),
            Tuple.Create(840, new[] { 2, 2, 2, 3, 5, 7}),
            Tuple.Create(971, new[] { 971 }),
            Tuple.Create(1000, new[] { 2, 2, 2, 5, 5, 5 }),
        };

        [TestMethod]
        public void VerifiesPrimeFactors()
        {
            var factorizer = new SieveOfEratosthenesFactorizer(1000);

            foreach (var numberPrimeFactorsPair in numberPrimeFactorsPairs)
            {
                int number = numberPrimeFactorsPair.Item1;
                int[] primeFactors = numberPrimeFactorsPair.Item2;

                CollectionAssert.AreEquivalent(primeFactors, factorizer.GetPrimeFactors(number).ToArray());
            }
        }

        [TestMethod]
        public void VerifiesDistinctPrimeFactors()
        {
            var factorizer = new SieveOfEratosthenesFactorizer(1000);

            foreach (var numberPrimeFactorsPair in numberPrimeFactorsPairs)
            {
                int number = numberPrimeFactorsPair.Item1;
                int[] distinctPrimeFactors = numberPrimeFactorsPair.Item2.Distinct().ToArray();

                CollectionAssert.AreEquivalent(distinctPrimeFactors, factorizer.GetDistinctPrimeFactors(number).ToArray());
            }
        }
    }
}
