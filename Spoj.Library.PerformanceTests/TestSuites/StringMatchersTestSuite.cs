using Spoj.Library.Helpers;
using Spoj.Library.StringMatchers;
using System.Collections.Generic;
using System.Linq;

namespace Spoj.Library.PerformanceTests.TestSuites
{
    public class StringMatchersTestSuite : ITestSuite
    {
        private readonly string _aaPatternLength2;
        private readonly string _aaPatternLength5;
        private readonly string _aaPatternLength20;
        private readonly string _aaPatternLength5000;
        private readonly string _aaTextLength10k;
        private readonly string _aaTextLength1m;

        private readonly string _abPatternLength2;
        private readonly string _abPatternLength5;
        private readonly string _abPatternLength20;
        private readonly string _abPatternLength5000;
        private readonly string _abPatternLength100k;
        private readonly string _abTextLength10k;
        private readonly string _abTextLength1m;

        private readonly string _azPatternLength2;
        private readonly string _azPatternLength5;
        private readonly string _azPatternLength20;
        private readonly string _azPatternLength5000;
        private readonly string _azTextLength10k;
        private readonly string _azTextLength1m;

        public StringMatchersTestSuite()
        {
            _aaPatternLength2 = InputGenerator.GenerateRandomString(2, 'a', 'a');
            _aaPatternLength5 = InputGenerator.GenerateRandomString(5, 'a', 'a');
            _aaPatternLength20 = InputGenerator.GenerateRandomString(20, 'a', 'a');
            _aaPatternLength5000 = InputGenerator.GenerateRandomString(5000, 'a', 'a');
            _aaTextLength10k = InputGenerator.GenerateRandomString(10000, 'a', 'a');
            _aaTextLength1m = InputGenerator.GenerateRandomString(1000000, 'a', 'a');

            _abPatternLength2 = InputGenerator.GenerateRandomString(2, 'a', 'b');
            _abPatternLength5 = InputGenerator.GenerateRandomString(5, 'a', 'b');
            _abPatternLength20 = InputGenerator.GenerateRandomString(20, 'a', 'b');
            _abPatternLength5000 = InputGenerator.GenerateRandomString(5000, 'a', 'b');
            _abPatternLength100k = InputGenerator.GenerateRandomString(100000, 'a', 'b');
            _abTextLength10k = InputGenerator.GenerateRandomString(10000, 'a', 'b');
            _abTextLength1m = InputGenerator.GenerateRandomString(1000000, 'a', 'b');

            _azPatternLength2 = InputGenerator.GenerateRandomString(2);
            _azPatternLength5 = InputGenerator.GenerateRandomString(5);
            _azPatternLength20 = InputGenerator.GenerateRandomString(20);
            _azPatternLength5000 = InputGenerator.GenerateRandomString(5000);
            _azTextLength10k = InputGenerator.GenerateRandomString(10000);
            _azTextLength1m = InputGenerator.GenerateRandomString(1000000);
        }

        public IEnumerable<TestScenario> TestScenarios => new TestScenario[]
        {
            new TestScenario("a-a text, pattern length 2", new TestCase[]
                {
                    new TestCase("Naive matcher, text length 10k", () => NaiveMatcher(_aaTextLength10k, _aaPatternLength2)),
                    new TestCase("KMP matcher, text length 10k", () => KmpMatcher(_aaTextLength10k, _aaPatternLength2)),
                    new TestCase("Naive matcher, text length 1m", () => NaiveMatcher(_aaTextLength1m, _aaPatternLength2)),
                    new TestCase("KMP matcher, text length 1m", () => KmpMatcher(_aaTextLength1m, _aaPatternLength2)),
                }),
            new TestScenario("a-a text, pattern length 5", new TestCase[]
                {
                    new TestCase("Naive matcher, text length 10k", () => NaiveMatcher(_aaTextLength10k, _aaPatternLength5)),
                    new TestCase("KMP matcher, text length 10k", () => KmpMatcher(_aaTextLength10k, _aaPatternLength5)),
                    new TestCase("Naive matcher, text length 1m", () => NaiveMatcher(_aaTextLength1m, _aaPatternLength5)),
                    new TestCase("KMP matcher, text length 1m", () => KmpMatcher(_aaTextLength1m, _aaPatternLength5)),
                }),
            new TestScenario("a-a text, pattern length 10", new TestCase[]
                {
                    new TestCase("Naive matcher, text length 10k", () => NaiveMatcher(_aaTextLength10k, _aaPatternLength20)),
                    new TestCase("KMP matcher, text length 10k", () => KmpMatcher(_aaTextLength10k, _aaPatternLength20)),
                    new TestCase("Naive matcher, text length 1m", () => NaiveMatcher(_aaTextLength1m, _aaPatternLength20)),
                    new TestCase("KMP matcher, text length 1m", () => KmpMatcher(_aaTextLength1m, _aaPatternLength20)),
                }),
            new TestScenario("a-a text, pattern length 5000", new TestCase[]
                {
                    new TestCase("Naive matcher, text length 10k", () => NaiveMatcher(_aaTextLength10k, _aaPatternLength5000)),
                    new TestCase("KMP matcher, text length 10k", () => KmpMatcher(_aaTextLength10k, _aaPatternLength5000)),
                    new TestCase("Naive matcher, text length 1m", () => NaiveMatcher(_aaTextLength1m, _aaPatternLength5000)),
                    new TestCase("KMP matcher, text length 1m", () => KmpMatcher(_aaTextLength1m, _aaPatternLength5000)),
                }),

            new TestScenario("a-b text, pattern length 2", new TestCase[]
                {
                    new TestCase("Naive matcher, text length 10k", () => NaiveMatcher(_abTextLength10k, _abPatternLength2)),
                    new TestCase("KMP matcher, text length 10k", () => KmpMatcher(_abTextLength10k, _abPatternLength2)),
                    new TestCase("Naive matcher, text length 1m", () => NaiveMatcher(_abTextLength1m, _abPatternLength2)),
                    new TestCase("KMP matcher, text length 1m", () => KmpMatcher(_abTextLength1m, _abPatternLength2)),
                }),
            new TestScenario("a-b text, pattern length 5", new TestCase[]
                {
                    new TestCase("Naive matcher, text length 10k", () => NaiveMatcher(_abTextLength10k, _abPatternLength5)),
                    new TestCase("KMP matcher, text length 10k", () => KmpMatcher(_abTextLength10k, _abPatternLength5)),
                    new TestCase("Naive matcher, text length 1m", () => NaiveMatcher(_abTextLength1m, _abPatternLength5)),
                    new TestCase("KMP matcher, text length 1m", () => KmpMatcher(_abTextLength1m, _abPatternLength5)),
                }),
            new TestScenario("a-b text, pattern length 20", new TestCase[]
                {
                    new TestCase("Naive matcher, text length 10k", () => NaiveMatcher(_abTextLength10k, _abPatternLength20)),
                    new TestCase("KMP matcher, text length 10k", () => KmpMatcher(_abTextLength10k, _abPatternLength20)),
                    new TestCase("Naive matcher, text length 1m", () => NaiveMatcher(_abTextLength1m, _abPatternLength20)),
                    new TestCase("KMP matcher, text length 1m", () => KmpMatcher(_abTextLength1m, _abPatternLength20)),
                }),
            new TestScenario("a-b text, pattern length 5000", new TestCase[]
                {
                    new TestCase("Naive matcher, text length 10k", () => NaiveMatcher(_abTextLength10k, _abPatternLength5000)),
                    new TestCase("KMP matcher, text length 10k", () => KmpMatcher(_abTextLength10k, _abPatternLength5000)),
                    new TestCase("Naive matcher, text length 1m", () => NaiveMatcher(_abTextLength1m, _abPatternLength5000)),
                    new TestCase("KMP matcher, text length 1m", () => KmpMatcher(_abTextLength1m, _abPatternLength5000)),
                }),
            new TestScenario("a-b text, pattern length 100k", new TestCase[]
                {
                    new TestCase("Naive matcher, text length 1m", () => NaiveMatcher(_abTextLength1m, _abPatternLength5000)),
                    new TestCase("KMP matcher, text length 1m", () => KmpMatcher(_abTextLength1m, _abPatternLength5000)),
                }),

            new TestScenario("a-z text, pattern length 2", new TestCase[]
                {
                    new TestCase("Naive matcher, text length 10k", () => NaiveMatcher(_azTextLength10k, _azPatternLength2)),
                    new TestCase("KMP matcher, text length 10k", () => KmpMatcher(_azTextLength10k, _azPatternLength2)),
                    new TestCase("Naive matcher, text length 1m", () => NaiveMatcher(_azTextLength1m, _azPatternLength2)),
                    new TestCase("KMP matcher, text length 1m", () => KmpMatcher(_azTextLength1m, _azPatternLength2)),
                }),
            new TestScenario("a-z text, pattern length 5", new TestCase[]
                {
                    new TestCase("Naive matcher, text length 10k", () => NaiveMatcher(_azTextLength10k, _azPatternLength5)),
                    new TestCase("KMP matcher, text length 10k", () => KmpMatcher(_azTextLength10k, _azPatternLength5)),
                    new TestCase("Naive matcher, text length 1m", () => NaiveMatcher(_azTextLength1m, _azPatternLength5)),
                    new TestCase("KMP matcher, text length 1m", () => KmpMatcher(_azTextLength1m, _azPatternLength5)),
                }),
            new TestScenario("a-z text, pattern length 20", new TestCase[]
                {
                    new TestCase("Naive matcher, text length 10k", () => NaiveMatcher(_azTextLength10k, _azPatternLength20)),
                    new TestCase("KMP matcher, text length 10k", () => KmpMatcher(_azTextLength10k, _azPatternLength20)),
                    new TestCase("Naive matcher, text length 1m", () => NaiveMatcher(_azTextLength1m, _azPatternLength20)),
                    new TestCase("KMP matcher, text length 1m", () => KmpMatcher(_azTextLength1m, _azPatternLength20)),
                }),
            new TestScenario("a-z text, pattern length 5000", new TestCase[]
                {
                    new TestCase("Naive matcher, text length 10k", () => NaiveMatcher(_azTextLength10k, _azPatternLength5000)),
                    new TestCase("KMP matcher, text length 10k", () => KmpMatcher(_azTextLength10k, _azPatternLength5000)),
                    new TestCase("Naive matcher, text length 1m", () => NaiveMatcher(_azTextLength1m, _azPatternLength5000)),
                    new TestCase("KMP matcher, text length 1m", () => KmpMatcher(_azTextLength1m, _azPatternLength5000)),
                }),
        };

        private void NaiveMatcher(string text, string pattern)
            => NaiveStringMatcher.GetMatchIndices(text, pattern).ToArray();

        private void KmpMatcher(string text, string pattern)
            => KmpStringMatcher.GetMatchIndices(text, pattern).ToArray();
    }
}
