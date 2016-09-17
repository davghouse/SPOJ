using Spoj.Library.Primes;
using System.Collections.Generic;
using System.Linq;

namespace Spoj.Library.PerformanceTests.TestSuites
{
    public class PrimesTestSuite : ITestSuite
    {
        private const int _tenMillion = 10000000;

        public IReadOnlyList<TestScenario> TestScenarios => new TestScenario[]
        {
            new TestScenario("Up to ten million, one full-width pass", new TestCase[]
                {
                    new TestCase("Sieve of Eratosthenes decider", () => SieveOfEratosthenesDecider(_tenMillion, 1, 0, _tenMillion)),
                    new TestCase("Trial division decider", () => TrialDivisionDecider(_tenMillion, 1, 0, _tenMillion)),
                    new TestCase("Sieve of Eratosthenes factorizer", () => SieveOfEratosthenesFactorizer(_tenMillion, 1, 0, _tenMillion)),
                }),
            new TestScenario("Up to ten million, ten full-width passes", new TestCase[]
                {
                    new TestCase("Sieve of Eratosthenes decider", () => SieveOfEratosthenesDecider(_tenMillion, 10, 0, _tenMillion)),
                    new TestCase("Trial division decider", () => TrialDivisionDecider(_tenMillion, 10, 0, _tenMillion)),
                    new TestCase("Sieve of Eratosthenes factorizer", () => SieveOfEratosthenesFactorizer(_tenMillion, 10, 0, _tenMillion)),
                }),
            new TestScenario("Up to ten million, one 1-million-width pass", new TestCase[]
                {
                    new TestCase("Sieve of Eratosthenes decider", () => SieveOfEratosthenesDecider(_tenMillion, 1, 5000000, 6000000)),
                    new TestCase("Trial division decider", () => TrialDivisionDecider(_tenMillion, 1, 5000000, 6000000)),
                    new TestCase("Sieve of Eratosthenes factorizer", () => SieveOfEratosthenesFactorizer(_tenMillion, 1, 5000000, 6000000)),
                }),
            new TestScenario("Up to ten million, ten 1-million-width passes", new TestCase[]
                {
                    new TestCase("Sieve of Eratosthenes decider", () => SieveOfEratosthenesDecider(_tenMillion, 10, 5000000, 6000000)),
                    new TestCase("Trial division decider", () => TrialDivisionDecider(_tenMillion, 10, 5000000, 6000000)),
                    new TestCase("Sieve of Eratosthenes factorizer", () => SieveOfEratosthenesFactorizer(_tenMillion, 10, 5000000, 6000000)),
                }),
            new TestScenario("Up to ten million, one 100k-width pass", new TestCase[]
                {
                    new TestCase("Sieve of Eratosthenes decider", () => SieveOfEratosthenesDecider(_tenMillion, 1, 5000000, 5100000)),
                    new TestCase("Trial division decider", () => TrialDivisionDecider(_tenMillion, 1, 5000000, 5100000)),
                    new TestCase("Sieve of Eratosthenes factorizer", () => SieveOfEratosthenesFactorizer(_tenMillion, 1, 5000000, 5100000)),
                }),
            new TestScenario("Up to ten million, ten 100k-million-width passes", new TestCase[]
                {
                    new TestCase("Sieve of Eratosthenes decider", () => SieveOfEratosthenesDecider(_tenMillion, 10, 5000000, 5100000)),
                    new TestCase("Trial division decider", () => TrialDivisionDecider(_tenMillion, 10, 5000000, 5100000)),
                    new TestCase("Sieve of Eratosthenes factorizer", () => SieveOfEratosthenesFactorizer(_tenMillion, 10, 5000000, 5100000)),
                }),
        };

        private void SieveOfEratosthenesDecider(int limit, int passes, int start, int end)
        {
            var decider = new SieveOfEratosthenesDecider(limit);

            for (int p = 0; p < passes; ++p)
            {
                for (int n = start; n <= end; ++n)
                {
                    decider.IsPrime(n);
                }
            }
        }

        private void TrialDivisionDecider(int limit, int passes, int start, int end)
        {
            var decider = new TrialDivisionDecider(limit);

            for (int p = 0; p < passes; ++p)
            {
                for (int n = start; n <= end; ++n)
                {
                    decider.IsPrime(n);
                }
            }
        }

        private void SieveOfEratosthenesFactorizer(int limit, int passes, int start, int end)
        {
            var factorizer = new SieveOfEratosthenesFactorizer(limit);

            for (int p = 0; p < passes; ++p)
            {
                for (int n = start; n <= end; ++n)
                {
                    factorizer.GetPrimeFactors(n).ToArray();
                }
            }
        }
    }
}
