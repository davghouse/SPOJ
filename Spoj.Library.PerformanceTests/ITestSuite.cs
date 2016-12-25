using System.Collections.Generic;

namespace Spoj.Library.PerformanceTests
{
    public interface ITestSuite
    {
        IEnumerable<TestScenario> TestScenarios { get; }
    }
}
