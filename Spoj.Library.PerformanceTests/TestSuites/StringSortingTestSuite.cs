using System;
using System.Collections.Generic;

namespace Spoj.Library.PerformanceTests.TestSuites
{
    public class StringSortingTestSuite : ITestSuite
    {
        private const int _stringLengthAndCount = 20000;
        private readonly string[] randomStrings1;
        private readonly string[] randomStrings2;
        private readonly string[] binaryStrings;
        private readonly string[] equalStrings;
        private string randomString;

        public StringSortingTestSuite()
        {
            randomStrings1 = new string[_stringLengthAndCount];
            randomStrings2 = new string[_stringLengthAndCount];
            for (int i = 0; i < _stringLengthAndCount; ++i)
            {
                randomStrings1[i] = randomStrings2[i] = InputGenerator.GenerateRandomString(_stringLengthAndCount);
            }

            binaryStrings = new string[_stringLengthAndCount];
            for (int i = 0; i < _stringLengthAndCount; ++i)
            {
                binaryStrings[i] = InputGenerator.GenerateRandomString(_stringLengthAndCount, '0', '1');
            }

            equalStrings = new string[_stringLengthAndCount];
            for (int i = 0; i < _stringLengthAndCount; ++i)
            {
                equalStrings[i] = InputGenerator.GenerateRandomString(_stringLengthAndCount, 'a', 'a');
            }

            randomString = InputGenerator.GenerateRandomString(_stringLengthAndCount);
        }

        public IEnumerable<TestScenario> TestScenarios => new TestScenario[]
        {
            new TestScenario($"Array.Sort, string length {_stringLengthAndCount}", new TestCase[]
                {
                    new TestCase($"{_stringLengthAndCount} random strings, ordinal", SortRandomStringsOrdinal),
                    new TestCase($"{_stringLengthAndCount} random strings, current culture", SortRandomStringsCurrentCulture),
                    new TestCase($"{_stringLengthAndCount} binary strings", SortBinaryStrings),
                    new TestCase($"{_stringLengthAndCount} equal strings", SortEqualStrings),
                    new TestCase($"Random string's {_stringLengthAndCount} suffixes (as indices)", SortBinaryStringSuffixesAsIndices),
                    new TestCase($"Random string's {_stringLengthAndCount} suffixes (as strings)", SortBinaryStringSuffixesAsStrings),
                })
        };

        public void SortRandomStringsOrdinal()
            => Array.Sort(randomStrings1, StringComparer.Ordinal);

        public void SortRandomStringsCurrentCulture()
            => Array.Sort(randomStrings2, StringComparer.CurrentCulture);

        public void SortBinaryStrings()
            => Array.Sort(binaryStrings, StringComparer.Ordinal);

        public void SortEqualStrings()
            => Array.Sort(equalStrings, StringComparer.Ordinal);

        public void SortBinaryStringSuffixesAsIndices()
        {
            int[] randomStringSuffixIndices = new int[_stringLengthAndCount];
            for (int i = 0; i < _stringLengthAndCount; ++i)
            {
                randomStringSuffixIndices[i] = i;
            }
            Array.Sort(randomStringSuffixIndices, (i, j) => string.CompareOrdinal(randomString, i, randomString, j, _stringLengthAndCount));
        }

        public void SortBinaryStringSuffixesAsStrings()
        {
            string[] randomStringSuffixes = new string[_stringLengthAndCount];
            for (int i = 0; i < _stringLengthAndCount; ++i)
            {
                randomStringSuffixes[i] = randomString.Substring(i);
            }
            Array.Sort(randomStringSuffixes, StringComparer.Ordinal);
        }
    }
}
