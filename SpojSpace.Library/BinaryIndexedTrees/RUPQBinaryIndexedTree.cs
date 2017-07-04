using System.Collections.Generic;

namespace SpojSpace.Library.BinaryIndexedTrees
{
    // See PURQ before trying to understand this. RUPQ just reinterprets what PURQ is doing without
    // actually changing any of the code. We are bound by what PURQ is doing: when a single value is
    // updated, the queried value from that index onward is affected (increases by the value added).
    // That's because PURQ calculates cumulative sums. Here, we don't want cumulative sums, we want
    // to add (the same) value to a range of elements, and query for the total value that's been added
    // to a single element. Updating a single element doesn't affect the elements after it. But we must
    // find an operation that does, since we're bound by how PURQ works. PURQ is saying "give me an
    // index to update and all queries from that index onward will be increased by the same amount."
    // RUPQ says "okay, here's an index and a value. We interpret this as ALL elements from that index
    // TO THE END of the array get increased by this value, so what you're saying about how the query
    // results are affected makes sense, thanks! Luckily, this interpretation allows us to do range updates."
    public sealed class RUPQBinaryIndexedTree
    {
        private readonly int[] _tree;

        public RUPQBinaryIndexedTree(int arrayLength)
        {
            _tree = new int[arrayLength + 1];
        }

        public RUPQBinaryIndexedTree(IReadOnlyList<int> array)
        {
            _tree = new int[array.Count + 1];

            for (int i = 0; i < array.Count; ++i)
            {
                RangeUpdate(i, i, array[i]);
            }
        }

        private void RangeUpdate(int updateStartIndex, int delta)
        {
            for (++updateStartIndex;
                updateStartIndex < _tree.Length;
                updateStartIndex += updateStartIndex & -updateStartIndex)
            {
                _tree[updateStartIndex] += delta;
            }
        }

        // We know conceptually what update is doing; the second line undoes it for the part of the array
        // after the update range that shouldn't have been affected.
        public void RangeUpdate(int updateStartIndex, int updateEndIndex, int delta)
        {
            RangeUpdate(updateStartIndex, delta);
            RangeUpdate(updateEndIndex + 1, -delta);
        }

        public int ValueQuery(int queryIndex)
        {
            int value = 0;
            for (++queryIndex;
                queryIndex > 0;
                queryIndex -= queryIndex & -queryIndex)
            {
                value += _tree[queryIndex];
            }

            return value;
        }
    }
}
