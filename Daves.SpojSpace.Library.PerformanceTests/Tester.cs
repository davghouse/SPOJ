using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Daves.SpojSpace.Library.PerformanceTests
{
    // For background, see http://mattwarren.org/2014/09/19/the-art-of-benchmarking/ and the articles it links to.
    // Mainly interested in coarse-grained relative times rather than absolute times, so the details aren't too important.
    public static class Tester
    {
        private static void Main(string[] args)
        {
            string testSuiteName = args[0];

            PrepareTest(GetTestSuite(testSuiteName));
            var testScenarioResults = RunTest(GetTestSuite(testSuiteName));
            DisplayTestResults(testScenarioResults);
        }

        private static ITestSuite GetTestSuite(string testSuiteName)
            => (ITestSuite)Activator
            .CreateInstance(null, $"Daves.SpojSpace.Library.PerformanceTests.TestSuites.{testSuiteName}TestSuite")
            .Unwrap();

        private static void PrepareTest(ITestSuite testSuite)
        {
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.RealTime;
            Thread.CurrentThread.Priority = ThreadPriority.Highest;

            foreach (TestScenario testScenario in testSuite.TestScenarios)
            {
                foreach (TestCase testCase in testScenario.TestCases)
                {
                    testCase.Run();
                }
            }
        }

        private static IReadOnlyList<TestScenarioResult> RunTest(ITestSuite testSuite)
        {
            var testScenarioResults = new List<TestScenarioResult>();
            foreach (TestScenario testScenario in testSuite.TestScenarios)
            {
                var testCaseResults = new List<TestCaseResult>();
                foreach (TestCase testCase in testScenario.TestCases)
                {
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();

                    Stopwatch stopwatch = Stopwatch.StartNew();
                    testCase.Run();
                    stopwatch.Stop();

                    testCaseResults.Add(new TestCaseResult(testCase, stopwatch.Elapsed));
                }

                testScenarioResults.Add(new TestScenarioResult(testScenario, testCaseResults));
            }

            return testScenarioResults;
        }

        private static void DisplayTestResults(IReadOnlyList<TestScenarioResult> testScenarioResults)
        {
            int maxTestCaseNameLength = testScenarioResults
                .SelectMany(sr => sr.TestCaseResults)
                .Select(cr => cr.TestCase.Name)
                .Max(n => n.Length);
            int maxElapsedTimeMultiplierStringLength = testScenarioResults
                .Select(sr => sr.MaxElapsedTimeMultiplier.ToString("F4"))
                .Max(m => m.Length);
            int maxSecondsStringLength = testScenarioResults
                .SelectMany(sr => sr.TestCaseResults)
                .Select(cr => cr.ElapsedTime.Seconds.ToString())
                .Max(s => s.Length);

            foreach (TestScenarioResult testScenarioResult in testScenarioResults)
            {
                Console.WriteLine($"{testScenarioResult.TestScenario.Name}:");

                foreach (TestCaseResult testCaseResult in testScenarioResult
                    .TestCaseResults
                    .OrderBy(cr => cr.ElapsedTime))
                {
                    // I considered using {0, -maxLength} style format strings along with string interpolation, but it was kind of annoying.
                    string testCaseName = testCaseResult.TestCase.Name;
                    string elapsedTimeMultiplierString = testCaseResult
                        .GetElapsedTimeMultiplier(testScenarioResult.MinElapsedTime)
                        .ToString("F4");
                    string secondsString = testCaseResult.ElapsedTime.Seconds.ToString();
                    string namePadding = new string(' ', maxTestCaseNameLength - testCaseName.Length);
                    string elapsedTimeMultiplierPadding = new string(' ', maxElapsedTimeMultiplierStringLength - elapsedTimeMultiplierString.Length);
                    string secondsPadding = new string(' ', maxSecondsStringLength - secondsString.Length);

                    Console.WriteLine("  {0}: {1}{2}x{3} ({4:%m} minutes {5}{4:s\\.ffff} seconds)",
                        testCaseName,
                        namePadding,
                        elapsedTimeMultiplierPadding,
                        elapsedTimeMultiplierString,
                        testCaseResult.ElapsedTime,
                        secondsPadding);
                }

                Console.WriteLine();
            }
        }
    }
}
