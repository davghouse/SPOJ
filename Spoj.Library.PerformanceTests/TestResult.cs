using System;

namespace Spoj.Library.PerformanceTests
{
    public sealed class TestResult
    {
        public TestResult(TestCase testCase, TimeSpan elapsedTime)
        {
            TestCase = testCase;
            ElapsedTime = elapsedTime;
        }

        public TestCase TestCase { get; }
        public TimeSpan ElapsedTime { get; }
    }
}
