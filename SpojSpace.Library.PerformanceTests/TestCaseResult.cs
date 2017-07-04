using System;

namespace SpojSpace.Library.PerformanceTests
{
    public class TestCaseResult
    {
        public TestCaseResult(TestCase testCase, TimeSpan elapsedTime)
        {
            TestCase = testCase;
            ElapsedTime = elapsedTime;
        }

        public TestCase TestCase { get; }
        public TimeSpan ElapsedTime { get; }

        public double GetElapsedTimeMultiplier(TimeSpan minElapsedTime)
            => ElapsedTime.Ticks / (double)minElapsedTime.Ticks;
    }
}
