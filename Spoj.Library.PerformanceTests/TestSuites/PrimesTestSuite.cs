﻿using Spoj.Library.Primes;
using System.Collections.Generic;
using System.Linq;

namespace Spoj.Library.PerformanceTests.TestSuites
{
    public class PrimesTestSuite : ITestSuite
    {
        private const int _tenMillion = 10000000;
        private const int _fiveMillion = 5000000;
        private const int _fiveMillionAnd100Thousand = 5100000;
        private const int _fiveMillionAnd1Thousand = 5001000;
        private const int _sixMillion = 6000000;

        public IEnumerable<TestScenario> TestScenarios => new TestScenario[]
        {
            new TestScenario("Up to ten million, one full-width pass", new TestCase[]
                {
                    new TestCase("Deciding w/ Sieve of Eratosthenes", () => SieveOfEratosthenesDecider(_tenMillion, 1, 0, _tenMillion)),
                    new TestCase("Deciding w/ trial division", () => TrialDivisionDecider(_tenMillion, 1, 0, _tenMillion)),
                    new TestCase("Factoring w/ Sieve of Eratosthenes", () => SieveOfEratosthenesFactorizer(_tenMillion, 1, 0, _tenMillion)),
                    new TestCase("Factoring w/ trial division", () => TrialDivisionFactorizer(_tenMillion, 1, 0, _tenMillion)),
                }),
            new TestScenario("Up to ten million, ten full-width passes", new TestCase[]
                {
                    new TestCase("Deciding w/ Sieve of Eratosthenes", () => SieveOfEratosthenesDecider(_tenMillion, 10, 0, _tenMillion)),
                    new TestCase("Deciding w/ trial division", () => TrialDivisionDecider(_tenMillion, 10, 0, _tenMillion)),
                    new TestCase("Factoring w/ Sieve of Eratosthenes", () => SieveOfEratosthenesFactorizer(_tenMillion, 10, 0, _tenMillion)),
                    new TestCase("Factoring w/ trial division", () => TrialDivisionFactorizer(_tenMillion, 10, 0, _tenMillion)),
                }),
            new TestScenario("Up to ten million, one 1-million-width pass", new TestCase[]
                {
                    new TestCase("Deciding w/ Sieve of Eratosthenes", () => SieveOfEratosthenesDecider(_tenMillion, 1, _fiveMillion, _sixMillion)),
                    new TestCase("Deciding w/ trial division", () => TrialDivisionDecider(_tenMillion, 1, _fiveMillion, _sixMillion)),
                    new TestCase("Factoring w/ Sieve of Eratosthenes", () => SieveOfEratosthenesFactorizer(_tenMillion, 1, _fiveMillion, _sixMillion)),
                    new TestCase("Factoring w/ trial division", () => TrialDivisionFactorizer(_tenMillion, 1, _fiveMillion, _sixMillion)),
                }),
            new TestScenario("Up to ten million, ten 1-million-width passes", new TestCase[]
                {
                    new TestCase("Deciding w/ Sieve of Eratosthenes", () => SieveOfEratosthenesDecider(_tenMillion, 10, _fiveMillion, _sixMillion)),
                    new TestCase("Deciding w/ trial division", () => TrialDivisionDecider(_tenMillion, 10, _fiveMillion, _sixMillion)),
                    new TestCase("Factoring w/ Sieve of Eratosthenes", () => SieveOfEratosthenesFactorizer(_tenMillion, 10, _fiveMillion, _sixMillion)),
                    new TestCase("Factoring w/ trial division", () => TrialDivisionFactorizer(_tenMillion, 10, _fiveMillion, _sixMillion)),
                }),
            new TestScenario("Up to ten million, one 100k-width pass", new TestCase[]
                {
                    new TestCase("Deciding w/ Sieve of Eratosthenes", () => SieveOfEratosthenesDecider(_tenMillion, 1, _fiveMillion, _fiveMillionAnd100Thousand)),
                    new TestCase("Deciding w/ trial division", () => TrialDivisionDecider(_tenMillion, 1, _fiveMillion, _fiveMillionAnd100Thousand)),
                    new TestCase("Factoring w/ Sieve of Eratosthenes", () => SieveOfEratosthenesFactorizer(_tenMillion, 1, _fiveMillion, _fiveMillionAnd100Thousand)),
                    new TestCase("Factoring w/ trial division", () => TrialDivisionFactorizer(_tenMillion, 1, _fiveMillion, _fiveMillionAnd100Thousand)),
                }),
            new TestScenario("Up to ten million, ten 100k-width passes", new TestCase[]
                {
                    new TestCase("Deciding w/ Sieve of Eratosthenes", () => SieveOfEratosthenesDecider(_tenMillion, 10, _fiveMillion, _fiveMillionAnd100Thousand)),
                    new TestCase("Deciding w/ trial division", () => TrialDivisionDecider(_tenMillion, 10, _fiveMillion, _fiveMillionAnd100Thousand)),
                    new TestCase("Factoring w/ Sieve of Eratosthenes", () => SieveOfEratosthenesFactorizer(_tenMillion, 10, _fiveMillion, _fiveMillionAnd100Thousand)),
                    new TestCase("Factoring w/ trial division", () => TrialDivisionFactorizer(_tenMillion, 10, _fiveMillion, _fiveMillionAnd100Thousand)),
                }),
            new TestScenario("Up to ten million, one 1000-width passes", new TestCase[]
                {
                    new TestCase("Deciding w/ Sieve of Eratosthenes", () => SieveOfEratosthenesDecider(_tenMillion, 1, _fiveMillion, _fiveMillionAnd1Thousand)),
                    new TestCase("Deciding w/ trial division", () => TrialDivisionDecider(_tenMillion, 1, _fiveMillion, _fiveMillionAnd1Thousand)),
                    new TestCase("Factoring w/ Sieve of Eratosthenes", () => SieveOfEratosthenesFactorizer(_tenMillion, 1, _fiveMillion, _fiveMillionAnd1Thousand)),
                    new TestCase("Factoring w/ trial division", () => TrialDivisionFactorizer(_tenMillion, 1, _fiveMillion, _fiveMillionAnd1Thousand)),
                }),
            new TestScenario("Up to ten million, ten 1000-width passes", new TestCase[]
                {
                    new TestCase("Deciding w/ Sieve of Eratosthenes", () => SieveOfEratosthenesDecider(_tenMillion, 10, _fiveMillion, _fiveMillionAnd1Thousand)),
                    new TestCase("Deciding w/ trial division", () => TrialDivisionDecider(_tenMillion, 10, _fiveMillion, _fiveMillionAnd1Thousand)),
                    new TestCase("Factoring w/ Sieve of Eratosthenes", () => SieveOfEratosthenesFactorizer(_tenMillion, 10, _fiveMillion, _fiveMillionAnd1Thousand)),
                    new TestCase("Factoring w/ trial division", () => TrialDivisionFactorizer(_tenMillion, 10, _fiveMillion, _fiveMillionAnd1Thousand)),
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

        private void TrialDivisionFactorizer(int limit, int passes, int start, int end)
        {
            var factorizer = new TrialDivisionFactorizer(limit);

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
