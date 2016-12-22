using System.Collections.Generic;

namespace Spoj.Library.PerformanceTests
{
    public sealed class TestScenario
    {
        public TestScenario(string name, IReadOnlyList<TestCase> testCases)
        {
            Name = name;
            TestCases = testCases;
        }

        public string Name { get; set; }
        public IReadOnlyList<TestCase> TestCases { get; }
    }
}
