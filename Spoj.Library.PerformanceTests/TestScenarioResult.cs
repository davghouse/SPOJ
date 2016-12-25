using System;
using System.Collections.Generic;
using System.Linq;

namespace Spoj.Library.PerformanceTests
{
    public sealed class TestScenarioResult
    {
        public TestScenarioResult(TestScenario testScenario, IReadOnlyList<TestCaseResult> testCaseResults)
        {
            TestScenario = testScenario;
            TestCaseResults = testCaseResults;
            MinElapsedTime = TestCaseResults.Min(cr => cr.ElapsedTime);
            MaxElapsedTimeMultiplier = TestCaseResults
                .Max(cr => cr.GetElapsedTimeMultiplier(MinElapsedTime));
        }

        public TestScenario TestScenario { get; }
        public IReadOnlyList<TestCaseResult> TestCaseResults { get; }
        public TimeSpan MinElapsedTime { get; }
        public double MaxElapsedTimeMultiplier { get; }
    }
}
