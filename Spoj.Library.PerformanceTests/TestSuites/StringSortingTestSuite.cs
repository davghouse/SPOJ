using Spoj.Library.Helpers;
using System;
using System.Collections.Generic;

namespace Spoj.Library.PerformanceTests.TestSuites
{
    public class StringSortingTestSuite : ITestSuite
    {
        private const int _stringLengthAndCount = 20000;
        private readonly string[] _randomStrings1;
        private readonly string[] _randomStrings2;
        private readonly string[] _binaryStrings;
        private readonly string[] _equalStrings;
        private string _randomString;

        public StringSortingTestSuite()
        {
            _randomStrings1 = new string[_stringLengthAndCount];
            _randomStrings2 = new string[_stringLengthAndCount];
            for (int i = 0; i < _stringLengthAndCount; ++i)
            {
                _randomStrings1[i] = _randomStrings2[i] = InputGenerator.GenerateRandomString(_stringLengthAndCount);
            }

            _binaryStrings = new string[_stringLengthAndCount];
            for (int i = 0; i < _stringLengthAndCount; ++i)
            {
                _binaryStrings[i] = InputGenerator.GenerateRandomString(_stringLengthAndCount, '0', '1');
            }

            _equalStrings = new string[_stringLengthAndCount];
            for (int i = 0; i < _stringLengthAndCount; ++i)
            {
                _equalStrings[i] = InputGenerator.GenerateRandomString(_stringLengthAndCount, 'a', 'a');
            }

            _randomString = InputGenerator.GenerateRandomString(_stringLengthAndCount);
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
            => Array.Sort(_randomStrings1, StringComparer.Ordinal);

        public void SortRandomStringsCurrentCulture()
            => Array.Sort(_randomStrings2, StringComparer.CurrentCulture);

        public void SortBinaryStrings()
            => Array.Sort(_binaryStrings, StringComparer.Ordinal);

        public void SortEqualStrings()
            => Array.Sort(_equalStrings, StringComparer.Ordinal);

        public void SortBinaryStringSuffixesAsIndices()
        {
            int[] randomStringSuffixIndices = new int[_stringLengthAndCount];
            for (int i = 0; i < _stringLengthAndCount; ++i)
            {
                randomStringSuffixIndices[i] = i;
            }
            Array.Sort(randomStringSuffixIndices, (i, j) => string.CompareOrdinal(_randomString, i, _randomString, j, _stringLengthAndCount));
        }

        public void SortBinaryStringSuffixesAsStrings()
        {
            string[] randomStringSuffixes = new string[_stringLengthAndCount];
            for (int i = 0; i < _stringLengthAndCount; ++i)
            {
                randomStringSuffixes[i] = _randomString.Substring(i);
            }
            Array.Sort(randomStringSuffixes, StringComparer.Ordinal);
        }
    }
}
