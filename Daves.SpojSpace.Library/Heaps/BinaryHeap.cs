using System.Collections.Generic;

namespace Daves.SpojSpace.Library.Heaps
{
    public sealed class BinaryHeap<TKey, TValue> : IHeap<TKey, TValue>
    {
        private List<KeyValuePair<TKey, TValue>> _keyValuePairs;
        private IComparer<TValue> _comparer;

        public BinaryHeap(IEnumerable<KeyValuePair<TKey, TValue>> keyValuePairs = null, IComparer<TValue> comparer = null)
        {
            keyValuePairs = keyValuePairs ?? new KeyValuePair<TKey, TValue>[0];
            _keyValuePairs = new List<KeyValuePair<TKey, TValue>>((keyValuePairs as ICollection<KeyValuePair<TKey, TValue>>)?.Count + 0 ?? 0);
            _comparer = comparer ?? Comparer<TValue>.Default;

            foreach (var keyValuePair in keyValuePairs)
            {
                Insert(keyValuePair);
            }
        }

        public int Size => _keyValuePairs.Count;
        public bool IsEmpty => Size == 0;
        public KeyValuePair<TKey, TValue> Top => _keyValuePairs[0];

        public void Insert(KeyValuePair<TKey, TValue> keyValuePair)
        {
            _keyValuePairs.Add(keyValuePair);
            SiftUp(_keyValuePairs.Count - 1, keyValuePair);
        }

        public KeyValuePair<TKey, TValue> Extract()
        {
            var top = _keyValuePairs[0];

            if (_keyValuePairs.Count == 1)
            {
                _keyValuePairs.RemoveAt(0);
            }
            else
            {
                _keyValuePairs[0] = _keyValuePairs[_keyValuePairs.Count - 1];
                _keyValuePairs.RemoveAt(_keyValuePairs.Count - 1);
                SiftDown(0, _keyValuePairs[0]);
            }

            return top;
        }

        public KeyValuePair<TKey, TValue> Replace(KeyValuePair<TKey, TValue> keyValuePair)
        {
            var top = _keyValuePairs[0];
            _keyValuePairs[0] = keyValuePair;
            SiftDown(0, keyValuePair);

            return top;
        }

        private void SiftUp(int index, KeyValuePair<TKey, TValue> keyValuePair)
        {
            // Stop if we don't have a parent to sift up to.
            if (index == 0) return;

            int parentIndex = (index - 1) / 2;
            var parentKeyValuePair = _keyValuePairs[parentIndex];

            // If the parent is larger, push the parent down and the value up--small rises to the top. We know this is okay (aka heap-preserving)
            // because parent was smaller than the other child, as only one child gets out of order at a time. So both are larger than value.
            if (_comparer.Compare(parentKeyValuePair.Value, keyValuePair.Value) > 0)
            {
                _keyValuePairs[index] = parentKeyValuePair;
                _keyValuePairs[parentIndex] = keyValuePair;
                SiftUp(parentIndex, keyValuePair);
            }
        }

        private void SiftDown(int index, KeyValuePair<TKey, TValue> keyValuePair)
        {
            int leftChildIndex = 2 * index + 1;
            int rightChildIndex = 2 * index + 2;

            // If both children exist...
            if (rightChildIndex < _keyValuePairs.Count)
            {
                var leftChildKeyValuePair = _keyValuePairs[leftChildIndex];
                var rightChildKeyValuePair = _keyValuePairs[rightChildIndex];

                // If the left child is smaller than the right child (so left can move above right, no problem)...
                if (_comparer.Compare(leftChildKeyValuePair.Value, rightChildKeyValuePair.Value) < 0)
                {
                    // And the value is greater than its left child, push the left child up and the value down--big falls to the bottom.
                    if (_comparer.Compare(keyValuePair.Value, leftChildKeyValuePair.Value) > 0)
                    {
                        _keyValuePairs[index] = leftChildKeyValuePair;
                        _keyValuePairs[leftChildIndex] = keyValuePair;
                        SiftDown(leftChildIndex, keyValuePair);
                    }
                }
                // If the right child is smaller or the same as the left child (so right can move above left, no problem)...
                else
                {
                    // And the value is greater than its right child, push the right child up and the value down--big falls to the bottom.
                    if (_comparer.Compare(keyValuePair.Value, rightChildKeyValuePair.Value) > 0)
                    {
                        _keyValuePairs[index] = rightChildKeyValuePair;
                        _keyValuePairs[rightChildIndex] = keyValuePair;
                        SiftDown(rightChildIndex, keyValuePair);
                    }
                }
            }
            // If only the left child exists (and therefore the left child is the last value)...
            else if (leftChildIndex < _keyValuePairs.Count)
            {
                var leftChildKeyValuePair = _keyValuePairs[leftChildIndex];

                // And the value is greater than its left child, push the left child up and the value down--big falls to the bottom.
                if (_comparer.Compare(keyValuePair.Value, leftChildKeyValuePair.Value) > 0)
                {
                    _keyValuePairs[index] = leftChildKeyValuePair;
                    _keyValuePairs[leftChildIndex] = keyValuePair;
                }
            }
        }
    }
}
