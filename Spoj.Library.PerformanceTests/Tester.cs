using Spoj.Library.PerformanceTests.TestSuites;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Spoj.Library.PerformanceTests
{
    public static class Tester
    {
        private static void Main(string[] args)
        {
            // Replace with other test suites as needed.
            ITestSuite testSuite = new PrimeDeciderAndFactorizerTestSuite();

            foreach (TestScenario testScenario in testSuite.TestScenarios)
            {
                var testResults = new List<TestResult>();

                foreach (TestCase testCase in testScenario.TestCases)
                {
                    Stopwatch stopwatch = Stopwatch.StartNew();
                    testCase.Run();
                    stopwatch.Stop();

                    testResults.Add(new TestResult(testCase, stopwatch.Elapsed));
                }

                int maxNameLength = testResults.Max(r => r.TestCase.Name.Length);
                TimeSpan minElapsedTime = testResults.Min(r => r.ElapsedTime);

                Console.WriteLine($"{testScenario.Name}:");
                foreach (TestResult testResult in testResults.OrderBy(r => r.ElapsedTime))
                {
                    Console.WriteLine("  {0}: {1}x{2:F5} ({3:%m} minutes {3:s\\.fffff} seconds)",
                        testResult.TestCase.Name,
                        new string(' ', maxNameLength - testResult.TestCase.Name.Length),
                        testResult.ElapsedTime.Ticks / (double)minElapsedTime.Ticks,
                        testResult.ElapsedTime);
                }
                Console.WriteLine();
            }
        }
    }
}
