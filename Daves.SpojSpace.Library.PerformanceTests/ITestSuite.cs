using System.Collections.Generic;

namespace Daves.SpojSpace.Library.PerformanceTests
{
    public interface ITestSuite
    {
        IEnumerable<TestScenario> TestScenarios { get; }
    }
}
