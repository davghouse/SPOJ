using System.Collections.Generic;

namespace Spoj.Library.BinaryIndexedTrees
{
    // Point update, range query binary indexed tree. This is the original BIT described
    // by Fenwick. There are lots of tutorials online but I'd start with these two videos:
    // https://www.youtube.com/watch?v=v_wj_mOAlig, https://www.youtube.com/watch?v=CWDQJGaN1gY.
    // Those make the querying part clear but don't really describe the update part very well.
    // For that, I'd go and read Fenwick's paper. This is all a lot less intuitive than segment trees.
    public sealed class PURQBinaryIndexedTree
    {
        private readonly int[] _tree;

        public PURQBinaryIndexedTree(int arraySize)
        {
            _tree = new int[arraySize + 1];
        }

        // There's a way to do this in O(n) instead of O(nlogn), apparently.
        public PURQBinaryIndexedTree(IReadOnlyList<int> array)
        {
            _tree = new int[array.Count + 1];

            for (int i = 0; i < array.Count; ++i)
            {
                PointUpdate(i, array[i]);
            }
        }

        // Updates to reflect an addition at an index of the original array (by traversing the update tree).
        public void PointUpdate(int updateIndex, int delta)
        {
            for (++updateIndex;
                updateIndex < _tree.Length;
                updateIndex += updateIndex & -updateIndex)
            {
                _tree[updateIndex] += delta;
            }
        }

        // Computes the sum from the zeroth index through the query index (by traversing the interrogation tree).
        private int SumQuery(int queryEndIndex)
        {
            int sum = 0;
            for (++queryEndIndex;
                queryEndIndex > 0;
                queryEndIndex -= queryEndIndex & -queryEndIndex)
            {
                sum += _tree[queryEndIndex];
            }

            return sum;
        }

        // Computes the sum from the start through the end query index, by removing the part we
        // shouldn't have counted. Fenwick describes a more efficient way to do this, but it's complicated.
        public int SumQuery(int queryStartIndex, int queryEndIndex)
            => SumQuery(queryEndIndex) - SumQuery(queryStartIndex - 1);
    }
}
