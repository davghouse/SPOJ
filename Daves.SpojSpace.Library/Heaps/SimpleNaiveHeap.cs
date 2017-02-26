using System.Collections.Generic;
using System.Linq;

namespace Daves.SpojSpace.Library.Heaps
{
    public sealed class SimpleNaiveHeap<T> : ISimpleHeap<T>
    {
        private List<T> _values;
        private IComparer<T> _comparer;

        public SimpleNaiveHeap(IEnumerable<T> values = null, IComparer<T> comparer = null)
        {
            _values = values?.ToList() ?? new List<T>();
            _comparer = comparer ?? Comparer<T>.Default;
        }

        public int Size => _values.Count;
        public bool IsEmpty => Size == 0;
        public T Top => _values[FindTopIndex()];

        public void Insert(T value)
            => _values.Add(value);

        public T Extract()
        {
            int topIndex = FindTopIndex();
            T top = _values[topIndex];
            _values.RemoveAt(topIndex);

            return top;
        }

        public T Replace(T value)
        {
            T top = Extract();
            Insert(value);

            return top;
        }

        private int FindTopIndex()
        {
            T top = _values[0];
            int topIndex = 0;

            for (int i = 1; i < _values.Count; ++i)
            {
                T value = _values[i];

                // If top is larger than value, value becomes the new top.
                if (_comparer.Compare(top, value) > 0)
                {
                    top = value;
                    topIndex = i;
                }
            }

            return topIndex;
        }
    }
}
