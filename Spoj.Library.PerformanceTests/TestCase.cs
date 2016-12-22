using System;

namespace Spoj.Library.PerformanceTests
{
    public sealed class TestCase
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
