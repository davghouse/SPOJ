using System;

namespace SpojSpace.Library.PerformanceTests
{
    public class TestCase
    {
        public TestCase(string name, Action run)
        {
            Name = name;
            Run = run;
        }

        public string Name { get; }
        public Action Run { get; }
    }
}
