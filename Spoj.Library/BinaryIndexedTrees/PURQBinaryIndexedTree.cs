using System.Collections.Generic;

namespace Spoj.Library.BinaryIndexedTrees
{
    // Point update, range query binary indexed tree. This is the original BIT described
    // by Fenwick. There are lots of tutorials online but I'd start with these two videos:
    // https://www.youtube.com/watch?v=v_wj_mOAlig, https://www.youtube.com/watch?v=CWDQJGaN1gY.
    // Those make the querying part clear but don't really describe the update part very well.
    // Then I'd go and read Fenwick's paper, but this is all a lot less intuitive than segment trees.
    public class PURQBinaryIndexedTree
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
        public void PointUpdate(int updateIndex, int addition)
        {
            for (++updateIndex;
                updateIndex < _tree.Length;
                updateIndex += updateIndex & -updateIndex)
            {
                _tree[updateIndex] += addition;
            }
        }

        // Computes the cumulative sum through the query index (by traversing the interrogation tree).
        private int CumulativeSumQuery(int queryEndIndex)
        {
            int cumulativeSum = 0;
            for (++queryEndIndex;
                queryEndIndex > 0;
                queryEndIndex -= queryEndIndex & -queryEndIndex)
            {
                cumulativeSum += _tree[queryEndIndex];
            }

            return cumulativeSum;
        }

        // Computes the cumulative sum from the start through the end query index, by removing the part we
        // shouldn't have counted. Fenwick describes a more efficient way to do this, but it's complicated.
        public int CumulativeSumQuery(int queryStartIndex, int queryEndIndex)
            => CumulativeSumQuery(queryEndIndex) - CumulativeSumQuery(queryStartIndex - 1);
    }
}
