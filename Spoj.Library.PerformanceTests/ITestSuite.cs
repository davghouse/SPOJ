using System.Collections.Generic;

namespace Spoj.Library.PerformanceTests
{
    public interface ITestSuite
    {
        IReadOnlyList<TestScenario> TestScenarios { get; }
    }
}
