using Spoj.Library.StringMatchers;
using System.Collections.Generic;
using System.Linq;

namespace Spoj.Library.PerformanceTests.TestSuites
{
    public sealed class StringMatchersTestSuite : ITestSuite
    {
        private readonly string aaPatternLength2;
        private readonly string aaPatternLength5;
        private readonly string aaPatternLength20;
        private readonly string aaPatternLength5000;
        private readonly string aaTextLength10k;
        private readonly string aaTextLength1m;

        private readonly string abPatternLength2;
        private readonly string abPatternLength5;
        private readonly string abPatternLength20;
        private readonly string abPatternLength5000;
        private readonly string abPatternLength100k;
        private readonly string abTextLength10k;
        private readonly string abTextLength1m;

        private readonly string azPatternLength2;
        private readonly string azPatternLength5;
        private readonly string azPatternLength20;
        private readonly string azPatternLength5000;
        private readonly string azTextLength10k;
        private readonly string azTextLength1m;

        public StringMatchersTestSuite()
        {
            aaPatternLength2 = InputGenerator.GenerateRandomString(2, 'a', 'a');
            aaPatternLength5 = InputGenerator.GenerateRandomString(5, 'a', 'a');
            aaPatternLength20 = InputGenerator.GenerateRandomString(20, 'a', 'a');
            aaPatternLength5000 = InputGenerator.GenerateRandomString(5000, 'a', 'a');
            aaTextLength10k = InputGenerator.GenerateRandomString(10000, 'a', 'a');
            aaTextLength1m = InputGenerator.GenerateRandomString(1000000, 'a', 'a');

            abPatternLength2 = InputGenerator.GenerateRandomString(2, 'a', 'b');
            abPatternLength5 = InputGenerator.GenerateRandomString(5, 'a', 'b');
            abPatternLength20 = InputGenerator.GenerateRandomString(20, 'a', 'b');
            abPatternLength5000 = InputGenerator.GenerateRandomString(5000, 'a', 'b');
            abPatternLength100k = InputGenerator.GenerateRandomString(100000, 'a', 'b');
            abTextLength10k = InputGenerator.GenerateRandomString(10000, 'a', 'b');
            abTextLength1m = InputGenerator.GenerateRandomString(1000000, 'a', 'b');

            azPatternLength2 = InputGenerator.GenerateRandomString(2);
            azPatternLength5 = InputGenerator.GenerateRandomString(5);
            azPatternLength20 = InputGenerator.GenerateRandomString(20);
            azPatternLength5000 = InputGenerator.GenerateRandomString(5000);
            azTextLength10k = InputGenerator.GenerateRandomString(10000);
            azTextLength1m = InputGenerator.GenerateRandomString(1000000);
        }

        public IReadOnlyList<TestScenario> TestScenarios => new TestScenario[]
        {
            new TestScenario("a-a text, pattern length 2", new TestCase[]
                {
                    new TestCase("Naive matcher, text length 10k", () => NaiveMatcher(aaTextLength10k, aaPatternLength2)),
                    new TestCase("KMP matcher, text length 10k", () => KmpMatcher(aaTextLength10k, aaPatternLength2)),
                    new TestCase("Naive matcher, text length 1m", () => NaiveMatcher(aaTextLength1m, aaPatternLength2)),
                    new TestCase("KMP matcher, text length 1m", () => KmpMatcher(aaTextLength1m, aaPatternLength2)),
                }),
            new TestScenario("a-a text, pattern length 5", new TestCase[]
                {
                    new TestCase("Naive matcher, text length 10k", () => NaiveMatcher(aaTextLength10k, aaPatternLength5)),
                    new TestCase("KMP matcher, text length 10k", () => KmpMatcher(aaTextLength10k, aaPatternLength5)),
                    new TestCase("Naive matcher, text length 1m", () => NaiveMatcher(aaTextLength1m, aaPatternLength5)),
                    new TestCase("KMP matcher, text length 1m", () => KmpMatcher(aaTextLength1m, aaPatternLength5)),
                }),
            new TestScenario("a-a text, pattern length 10", new TestCase[]
                {
                    new TestCase("Naive matcher, text length 10k", () => NaiveMatcher(aaTextLength10k, aaPatternLength20)),
                    new TestCase("KMP matcher, text length 10k", () => KmpMatcher(aaTextLength10k, aaPatternLength20)),
                    new TestCase("Naive matcher, text length 1m", () => NaiveMatcher(aaTextLength1m, aaPatternLength20)),
                    new TestCase("KMP matcher, text length 1m", () => KmpMatcher(aaTextLength1m, aaPatternLength20)),
                }),
            new TestScenario("a-a text, pattern length 5000", new TestCase[]
                {
                    new TestCase("Naive matcher, text length 10k", () => NaiveMatcher(aaTextLength10k, aaPatternLength5000)),
                    new TestCase("KMP matcher, text length 10k", () => KmpMatcher(aaTextLength10k, aaPatternLength5000)),
                    new TestCase("Naive matcher, text length 1m", () => NaiveMatcher(aaTextLength1m, aaPatternLength5000)),
                    new TestCase("KMP matcher, text length 1m", () => KmpMatcher(aaTextLength1m, aaPatternLength5000)),
                }),

            new TestScenario("a-b text, pattern length 2", new TestCase[]
                {
                    new TestCase("Naive matcher, text length 10k", () => NaiveMatcher(abTextLength10k, abPatternLength2)),
                    new TestCase("KMP matcher, text length 10k", () => KmpMatcher(abTextLength10k, abPatternLength2)),
                    new TestCase("Naive matcher, text length 1m", () => NaiveMatcher(abTextLength1m, abPatternLength2)),
                    new TestCase("KMP matcher, text length 1m", () => KmpMatcher(abTextLength1m, abPatternLength2)),
                }),
            new TestScenario("a-b text, pattern length 5", new TestCase[]
                {
                    new TestCase("Naive matcher, text length 10k", () => NaiveMatcher(abTextLength10k, abPatternLength5)),
                    new TestCase("KMP matcher, text length 10k", () => KmpMatcher(abTextLength10k, abPatternLength5)),
                    new TestCase("Naive matcher, text length 1m", () => NaiveMatcher(abTextLength1m, abPatternLength5)),
                    new TestCase("KMP matcher, text length 1m", () => KmpMatcher(abTextLength1m, abPatternLength5)),
                }),
            new TestScenario("a-b text, pattern length 20", new TestCase[]
                {
                    new TestCase("Naive matcher, text length 10k", () => NaiveMatcher(abTextLength10k, abPatternLength20)),
                    new TestCase("KMP matcher, text length 10k", () => KmpMatcher(abTextLength10k, abPatternLength20)),
                    new TestCase("Naive matcher, text length 1m", () => NaiveMatcher(abTextLength1m, abPatternLength20)),
                    new TestCase("KMP matcher, text length 1m", () => KmpMatcher(abTextLength1m, abPatternLength20)),
                }),
            new TestScenario("a-b text, pattern length 5000", new TestCase[]
                {
                    new TestCase("Naive matcher, text length 10k", () => NaiveMatcher(abTextLength10k, abPatternLength5000)),
                    new TestCase("KMP matcher, text length 10k", () => KmpMatcher(abTextLength10k, abPatternLength5000)),
                    new TestCase("Naive matcher, text length 1m", () => NaiveMatcher(abTextLength1m, abPatternLength5000)),
                    new TestCase("KMP matcher, text length 1m", () => KmpMatcher(abTextLength1m, abPatternLength5000)),
                }),
            new TestScenario("a-b text, pattern length 100k", new TestCase[]
                {
                    new TestCase("Naive matcher, text length 1m", () => NaiveMatcher(abTextLength1m, abPatternLength5000)),
                    new TestCase("KMP matcher, text length 1m", () => KmpMatcher(abTextLength1m, abPatternLength5000)),
                }),

            new TestScenario("a-z text, pattern length 2", new TestCase[]
                {
                    new TestCase("Naive matcher, text length 10k", () => NaiveMatcher(azTextLength10k, azPatternLength2)),
                    new TestCase("KMP matcher, text length 10k", () => KmpMatcher(azTextLength10k, azPatternLength2)),
                    new TestCase("Naive matcher, text length 1m", () => NaiveMatcher(azTextLength1m, azPatternLength2)),
                    new TestCase("KMP matcher, text length 1m", () => KmpMatcher(azTextLength1m, azPatternLength2)),
                }),
            new TestScenario("a-z text, pattern length 5", new TestCase[]
                {
                    new TestCase("Naive matcher, text length 10k", () => NaiveMatcher(azTextLength10k, azPatternLength5)),
                    new TestCase("KMP matcher, text length 10k", () => KmpMatcher(azTextLength10k, azPatternLength5)),
                    new TestCase("Naive matcher, text length 1m", () => NaiveMatcher(azTextLength1m, azPatternLength5)),
                    new TestCase("KMP matcher, text length 1m", () => KmpMatcher(azTextLength1m, azPatternLength5)),
                }),
            new TestScenario("a-z text, pattern length 20", new TestCase[]
                {
                    new TestCase("Naive matcher, text length 10k", () => NaiveMatcher(azTextLength10k, azPatternLength20)),
                    new TestCase("KMP matcher, text length 10k", () => KmpMatcher(azTextLength10k, azPatternLength20)),
                    new TestCase("Naive matcher, text length 1m", () => NaiveMatcher(azTextLength1m, azPatternLength20)),
                    new TestCase("KMP matcher, text length 1m", () => KmpMatcher(azTextLength1m, azPatternLength20)),
                }),
            new TestScenario("a-z text, pattern length 5000", new TestCase[]
                {
                    new TestCase("Naive matcher, text length 10k", () => NaiveMatcher(azTextLength10k, azPatternLength5000)),
                    new TestCase("KMP matcher, text length 10k", () => KmpMatcher(azTextLength10k, azPatternLength5000)),
                    new TestCase("Naive matcher, text length 1m", () => NaiveMatcher(azTextLength1m, azPatternLength5000)),
                    new TestCase("KMP matcher, text length 1m", () => KmpMatcher(azTextLength1m, azPatternLength5000)),
                }),
        };

        private void NaiveMatcher(string text, string pattern)
            => NaiveStringMatcher.GetMatchIndices(text, pattern).ToArray();

        private void KmpMatcher(string text, string pattern)
            => KmpStringMatcher.GetMatchIndices(text, pattern).ToArray();
    }
}
