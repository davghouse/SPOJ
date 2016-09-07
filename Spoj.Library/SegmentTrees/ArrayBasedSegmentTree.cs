using Spoj.Library.Helpers;
using System.Collections.Generic;

namespace Spoj.Library.SegmentTrees
{
    // Most guides online cover this approach, but here's one good one:
    // https://kartikkukreja.wordpress.com/2014/11/09/a-simple-approach-to-segment-trees/
    public sealed class ArrayBasedSegmentTree<T> : SegmentTree<T> where T : class, ISegmentTreeQueryValue<T>, new()
    {
        private readonly T[] _treeArray;

        public ArrayBasedSegmentTree(IReadOnlyList<int> sourceArray)
            : base(sourceArray)
        {
            _treeArray = new T[2 * MathHelper.GetFirstPowerOfTwoAtOrAfter(_sourceArray.Count) - 1];
            Build(0, 0, _sourceArray.Count - 1);
        }

        private void Build(int treeArrayIndex, int segmentStartIndex, int segmentEndIndex)
        {
            if (segmentStartIndex == segmentEndIndex)
            {
                _treeArray[treeArrayIndex] = new T();
                _treeArray[treeArrayIndex].Initialize(_sourceArray[segmentStartIndex]);
                return;
            }

            int leftChildTreeArrayIndex = 2 * treeArrayIndex + 1;
            int rightChildTreeArrayIndex = leftChildTreeArrayIndex + 1;

            Build(leftChildTreeArrayIndex, segmentStartIndex, (segmentStartIndex + segmentEndIndex) / 2);
            Build(rightChildTreeArrayIndex, (segmentStartIndex + segmentEndIndex) / 2 + 1, segmentEndIndex);

            _treeArray[treeArrayIndex] = _treeArray[leftChildTreeArrayIndex].Combine(_treeArray[rightChildTreeArrayIndex]);
        }

        public override T Query(int queryStartIndex, int queryEndIndex)
            => Query(0, 0, _sourceArray.Count - 1, queryStartIndex, queryEndIndex);

        // This is where we have to drag around the segment that the value at treeArrayIndex corresponds to.
        private T Query(int treeArrayIndex, int segmentStartIndex, int segmentEndIndex, int queryStartIndex, int queryEndIndex)
        {
            if (queryStartIndex <= segmentStartIndex && queryEndIndex >= segmentEndIndex)
                return _treeArray[treeArrayIndex];

            int leftChildTreeArrayIndex = 2 * treeArrayIndex + 1;
            int rightChildTreeArrayIndex = leftChildTreeArrayIndex + 1;
            int leftChildSegmentEndIndex = (segmentStartIndex + segmentEndIndex) / 2;

            if (queryStartIndex <= leftChildSegmentEndIndex && queryEndIndex > leftChildSegmentEndIndex)
                return Query(leftChildTreeArrayIndex, segmentStartIndex, leftChildSegmentEndIndex, queryStartIndex, queryEndIndex)
                    .Combine(Query(rightChildTreeArrayIndex, leftChildSegmentEndIndex + 1, segmentEndIndex, queryStartIndex, queryEndIndex));
            else if (queryStartIndex <= leftChildSegmentEndIndex)
                return Query(leftChildTreeArrayIndex, segmentStartIndex, leftChildSegmentEndIndex, queryStartIndex, queryEndIndex);
            else
                return Query(rightChildTreeArrayIndex, leftChildSegmentEndIndex + 1, segmentEndIndex, queryStartIndex, queryEndIndex);
        }
    }
}
