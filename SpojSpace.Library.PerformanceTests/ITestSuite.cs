using System.Collections.Generic;

namespace SpojSpace.Library.PerformanceTests
{
    public interface ITestSuite
    {
        IEnumerable<TestScenario> TestScenarios { get; }
    }
}
