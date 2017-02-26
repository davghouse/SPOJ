using System.Collections.Generic;
using System.Linq;

namespace Daves.SpojSpace.Library.Heaps
{
    public sealed class NaiveHeap<TKey, TValue> : IHeap<TKey, TValue>
    {
        private List<KeyValuePair<TKey, TValue>> _keyValuePairs;
        private IComparer<TValue> _comparer;

        public NaiveHeap(IEnumerable<KeyValuePair<TKey, TValue>> keyValuePairs = null, IComparer<TValue> comparer = null)
        {
            _keyValuePairs = keyValuePairs?.ToList() ?? new List<KeyValuePair<TKey, TValue>>();
            _comparer = comparer ?? Comparer<TValue>.Default;
        }

        public int Size => _keyValuePairs.Count;
        public bool IsEmpty => Size == 0;
        public KeyValuePair<TKey, TValue> Top => _keyValuePairs[FindTopIndex()];

        public void Insert(TKey key, TValue value)
            => Insert(new KeyValuePair<TKey, TValue>(key, value));

        public void Insert(KeyValuePair<TKey, TValue> keyValuePair)
            => _keyValuePairs.Add(keyValuePair);

        public KeyValuePair<TKey, TValue> Extract()
        {
            int topIndex = FindTopIndex();
            var top = _keyValuePairs[topIndex];
            _keyValuePairs.RemoveAt(topIndex);

            return top;
        }

        public KeyValuePair<TKey, TValue> Replace(TKey key, TValue value)
            => Replace(new KeyValuePair<TKey, TValue>(key, value));

        public KeyValuePair<TKey, TValue> Replace(KeyValuePair<TKey, TValue> keyValuePair)
        {
            var top = Extract();
            Insert(keyValuePair);

            return top;
        }

        public bool Contains(TKey key)
            => _keyValuePairs.Any(kvp => kvp.Key.Equals(key));

        public TValue GetValue(TKey key)
            => _keyValuePairs.Single(kvp => kvp.Key.Equals(key)).Value;

        public TValue Update(TKey key, TValue value)
            => Update(new KeyValuePair<TKey, TValue>(key, value));

        public TValue Update(KeyValuePair<TKey, TValue> keyValuePair)
        {
            for (int i = 0; i < _keyValuePairs.Count; ++i)
            {
                if (_keyValuePairs[i].Key.Equals(keyValuePair.Key))
                {
                    TValue oldValue = _keyValuePairs[i].Value;
                    _keyValuePairs[i] = keyValuePair;
                    return oldValue;
                }
            }

            throw new KeyNotFoundException();
        }

        private int FindTopIndex()
        {
            var top = _keyValuePairs[0];
            int topIndex = 0;

            for (int i = 1; i < _keyValuePairs.Count; ++i)
            {
                var keyValuePair = _keyValuePairs[i];

                // If top is larger than value, value becomes the new top.
                if (_comparer.Compare(top.Value, keyValuePair.Value) > 0)
                {
                    top = keyValuePair;
                    topIndex = i;
                }
            }

            return topIndex;
        }
    }
}
