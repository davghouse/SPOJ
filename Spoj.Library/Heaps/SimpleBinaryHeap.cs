using System.Collections.Generic;

namespace Spoj.Library.Heaps
{
    public sealed class SimpleBinaryHeap<T> : ISimpleHeap<T>
    {
        private List<T> _values;
        private IComparer<T> _comparer;

        public SimpleBinaryHeap(IEnumerable<T> values = null, IComparer<T> comparer = null)
        {
            values = values ?? new T[0];
            _values = new List<T>((values as ICollection<T>)?.Count + 0 ?? 0);
            _comparer = comparer ?? Comparer<T>.Default;

            foreach (T value in values)
            {
                Add(value);
            }
        }

        public int Size => _values.Count;
        public bool IsEmpty => Size == 0;
        public T Top => _values[0];

        public void Add(T value)
        {
            _values.Add(value);
            SiftUp(_values.Count - 1, value);
        }

        public T Extract()
        {
            T top = _values[0];

            if (_values.Count == 1)
            {
                _values.RemoveAt(0);
            }
            else
            {
                T bottom = _values[_values.Count - 1];
                _values.RemoveAt(_values.Count - 1);
                _values[0] = bottom;
                SiftDown(0, bottom);
            }

            return top;
        }

        public T Replace(T value)
        {
            T top = _values[0];
            _values[0] = value;
            SiftDown(0, value);

            return top;
        }

        private void SiftUp(int index, T value)
        {
            // Stop if we don't have a parent to sift up to.
            if (index == 0) return;

            int parentIndex = (index - 1) / 2;
            T parentValue = _values[parentIndex];

            // If the parent is larger, push the parent down and the value up--small rises to the
            // top. We know this is okay (aka heap-preserving) because parent was smaller than the
            // other child, as only one child gets out of order at a time. So both are larger than value.
            if (_comparer.Compare(parentValue, value) > 0)
            {
                _values[index] = parentValue;
                _values[parentIndex] = value;
                SiftUp(parentIndex, value);
            }
        }

        private void SiftDown(int index, T value)
        {
            int leftChildIndex = 2 * index + 1;
            int rightChildIndex = 2 * index + 2;

            // If both children exist...
            if (rightChildIndex < _values.Count)
            {
                T leftChildValue = _values[leftChildIndex];
                T rightChildValue = _values[rightChildIndex];

                // If the left child is smaller than the right child (so left can move above right)...
                if (_comparer.Compare(leftChildValue, rightChildValue) < 0)
                {
                    // And the value is greater than its left child, push the left child up and
                    // the value down--big falls to the bottom.
                    if (_comparer.Compare(value, leftChildValue) > 0)
                    {
                        _values[index] = leftChildValue;
                        _values[leftChildIndex] = value;
                        SiftDown(leftChildIndex, value);
                    }
                }
                // If the right child is smaller or the same as the left child (so right can move above left)...
                else
                {
                    // And the value is greater than its right child, push the right child up and
                    // the value down--big falls to the bottom.
                    if (_comparer.Compare(value, rightChildValue) > 0)
                    {
                        _values[index] = rightChildValue;
                        _values[rightChildIndex] = value;
                        SiftDown(rightChildIndex, value);
                    }
                }
            }
            // If only the left child exists (and therefore the left child is the last value)...
            else if (leftChildIndex < _values.Count)
            {
                T leftChildValue = _values[leftChildIndex];

                // And the value is greater than its left child, push the left child up and
                // the value down--big falls to the bottom.
                if (_comparer.Compare(value, leftChildValue) > 0)
                {
                    _values[index] = leftChildValue;
                    _values[leftChildIndex] = value;
                }
            }
        }
    }
}
