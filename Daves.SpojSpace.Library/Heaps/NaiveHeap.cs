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

        public KeyValuePair<TKey, TValue> Extract()
        {
            int topIndex = FindTopIndex();
            var top = _keyValuePairs[topIndex];
            _keyValuePairs.RemoveAt(topIndex);

            return top;
        }

        public void Insert(KeyValuePair<TKey, TValue> keyValuePair)
            => _keyValuePairs.Add(keyValuePair);

        public KeyValuePair<TKey, TValue> Replace(KeyValuePair<TKey, TValue> keyValuePair)
        {
            var top = Extract();
            Insert(keyValuePair);

            return top;
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
