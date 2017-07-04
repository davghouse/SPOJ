using SpojSpace.Library.Tries;
using System;
using System.Collections.Generic;

namespace SpojSpace.Library.PerformanceTests.TestSuites
{
    public sealed class TriesTestSuite : ITestSuite
    {
        private const int _stringCount = 100000;
        private const int _stringMaxLength = 20;
        private const int _prefixGroupCount = 1000;
        private const int _prefixGroupSize = _stringCount / _prefixGroupCount;
        private const int _prefixLength = 4;
        private readonly string[] _seedRandomStrings;
        private readonly string[] _searchRandomStrings;
        private readonly string[] _searchRandomPrefixGroupedStrings;
        private HashSet<string> _caseSensitiveHashSet;
        private HashSet<string> _caseInsensitiveHashSet;
        private Trie _caseSensitiveTrie;
        private Trie _caseInsensitiveTrie;

        public TriesTestSuite()
        {
            var rand = InputGenerator.Rand;

            _seedRandomStrings = new string[_stringCount];
            for (int i = 0; i < _stringCount; ++i)
            {
                _seedRandomStrings[i] = InputGenerator.GenerateRandomString(rand.Next(1, _stringMaxLength + 1), 'A', 'Z');
            }

            _searchRandomStrings = new string[_stringCount];
            for (int i = 0; i < _stringCount; ++i)
            {
                _searchRandomStrings[i] = InputGenerator.GenerateRandomString(rand.Next(1, _stringMaxLength + 1), 'A', 'Z');
            }

            _searchRandomPrefixGroupedStrings = new string[_stringCount];
            for (int i = 0; i < _prefixGroupCount; ++i)
            {
                string prefix = InputGenerator.GenerateRandomString(_prefixLength, 'A', 'Z');
                for (int j = 0; j < _prefixGroupSize; ++j)
                {
                    _searchRandomPrefixGroupedStrings[i * _prefixGroupSize + j] = prefix + InputGenerator.GenerateRandomString(rand.Next(1, _stringMaxLength + 1 - _prefixLength), 'A', 'z');
                }
            }
        }

        public IEnumerable<TestScenario> TestScenarios => new TestScenario[]
        {
            new TestScenario($"Adding {_stringCount} random strings up to length {_stringMaxLength}", new TestCase[]
                {
                    new TestCase("Case-sensitive hash set", CaseSensitiveHashSetAdd),
                    new TestCase("Case-insensitive hash set", CaseInsensitiveHashSetAdd),
                    new TestCase("Case-sensitive trie", CaseSensitiveTrieAdd),
                    new TestCase("Case-insensitive trie", CaseInsensitiveTrieAdd),
                }),
            new TestScenario($"Searching for {_stringCount} random strings up to length {_stringMaxLength}", new TestCase[]
                {
                    new TestCase("Case-sensitive hash set", CaseSensitiveHashSetSearch),
                    new TestCase("Case-insensitive hash set", CaseInsensitiveHashSetSearch),
                    new TestCase("Case-sensitive trie", CaseSensitiveTrieSearch),
                    new TestCase("Case-insensitive trie", CaseInsensitiveTrieSearch),
                }),
            new TestScenario($"Searching for {_stringCount} strings up to length {_stringMaxLength}, in {_prefixGroupCount} prefix groups of size {_prefixGroupSize} with prefix length {_prefixLength}", new TestCase[]
                {
                    new TestCase("Case-sensitive hash set", CaseSensitiveHashSetPrefixedSearch),
                    new TestCase("Case-insensitive hash set", CaseInsensitiveHashSetPrefixedSearch),
                    new TestCase("Case-sensitive trie", CaseSensitiveTriePrefixedSearch),
                    new TestCase("Case-insensitive trie", CaseInsensitiveTriePrefixedSearch),
                })
        };

        private void CaseSensitiveHashSetAdd()
        {
            _caseSensitiveHashSet = new HashSet<string>(StringComparer.Ordinal);
            for (int i = 0; i < _stringCount; ++i)
            {
                _caseSensitiveHashSet.Add(_seedRandomStrings[i]);
            }
        }

        private void CaseInsensitiveHashSetAdd()
        {
            _caseInsensitiveHashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            for (int i = 0; i < _stringCount; ++i)
            {
                _caseInsensitiveHashSet.Add(_seedRandomStrings[i]);
            }
        }

        private void CaseSensitiveTrieAdd()
        {
            _caseSensitiveTrie = new Trie(EqualityComparer<char>.Default);
            for (int i = 0; i < _stringCount; ++i)
            {
                _caseSensitiveTrie.Add(_seedRandomStrings[i]);
            }
        }

        private void CaseInsensitiveTrieAdd()
        {
            _caseInsensitiveTrie = new Trie(new CaseInsensitiveCharEqualityComparer());
            for (int i = 0; i < _stringCount; ++i)
            {
                _caseInsensitiveTrie.Add(_seedRandomStrings[i]);
            }
        }

        private void CaseSensitiveHashSetSearch()
        {
            for (int i = 0; i < _stringCount; ++i)
            {
                _caseSensitiveHashSet.Contains(_searchRandomStrings[i]);
            }
        }

        private void CaseInsensitiveHashSetSearch()
        {
            for (int i = 0; i < _stringCount; ++i)
            {
                _caseInsensitiveHashSet.Contains(_searchRandomStrings[i]);
            }
        }

        private void CaseSensitiveTrieSearch()
        {
            for (int i = 0; i < _stringCount; ++i)
            {
                _caseSensitiveTrie.ContainsWord(_searchRandomStrings[i]);
            }
        }

        private void CaseInsensitiveTrieSearch()
        {
            for (int i = 0; i < _stringCount; ++i)
            {
                _caseInsensitiveTrie.ContainsWord(_searchRandomStrings[i]);
            }
        }

        private void CaseSensitiveHashSetPrefixedSearch()
        {
            for (int i = 0; i < _stringCount; ++i)
            {
                _caseSensitiveHashSet.Contains(_searchRandomPrefixGroupedStrings[i]);
            }
        }

        private void CaseInsensitiveHashSetPrefixedSearch()
        {
            for (int i = 0; i < _stringCount; ++i)
            {
                _caseInsensitiveHashSet.Contains(_searchRandomPrefixGroupedStrings[i]);
            }
        }

        private void CaseSensitiveTriePrefixedSearch()
        {
            for (int i = 0; i < _prefixGroupCount; ++i)
            {
                string prefix = _searchRandomPrefixGroupedStrings[i * _prefixGroupSize].Substring(0, _prefixLength);
                if (!_caseSensitiveTrie.ContainsPrefix(prefix)) continue;

                for (int j = 1; j < _prefixGroupSize; ++j)
                {
                    _caseSensitiveTrie.ContainsWord(_searchRandomPrefixGroupedStrings[i * _prefixGroupSize + j]);
                }
            }
        }

        private void CaseInsensitiveTriePrefixedSearch()
        {
            for (int i = 0; i < _prefixGroupCount; ++i)
            {
                string prefix = _searchRandomPrefixGroupedStrings[i * _prefixGroupSize].Substring(0, _prefixLength);
                if (!_caseInsensitiveTrie.ContainsPrefix(prefix)) continue;

                for (int j = 1; j < _prefixGroupSize; ++j)
                {
                    _caseInsensitiveTrie.ContainsWord(_searchRandomPrefixGroupedStrings[i * _prefixGroupSize + j]);
                }
            }
        }
    }
}
