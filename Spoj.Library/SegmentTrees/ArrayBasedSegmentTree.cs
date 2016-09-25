using Spoj.Library.Helpers;
using System;
using System.Collections.Generic;

namespace Spoj.Library.SegmentTrees
{
    // Most guides online cover this approach, but here's one good one:
    // https://kartikkukreja.wordpress.com/2014/11/09/a-simple-approach-to-segment-trees/
    public sealed class ArrayBasedSegmentTree<TQueryObject, TQueryValue> : SegmentTree<TQueryObject, TQueryValue>
        where TQueryObject : SegmentTreeQueryObject<TQueryObject, TQueryValue>, new()
    {
        private readonly TQueryObject[] _treeArray;

        public ArrayBasedSegmentTree(IReadOnlyList<TQueryValue> sourceArray)
            : base(sourceArray)
        {
            _treeArray = new TQueryObject[2 * MathHelper.FirstPowerOfTwoAtOrAfter(_sourceArray.Count) - 1];
            Build(0, 0, _sourceArray.Count - 1);
        }

        private void Build(int treeArrayIndex, int segmentStartIndex, int segmentEndIndex)
        {
            if (segmentStartIndex == segmentEndIndex)
            {
                _treeArray[treeArrayIndex] = new TQueryObject();
                _treeArray[treeArrayIndex].Initialize(segmentStartIndex, _sourceArray[segmentStartIndex]);
                return;
            }

            int leftChildTreeArrayIndex = 2 * treeArrayIndex + 1;
            int rightChildTreeArrayIndex = leftChildTreeArrayIndex + 1;

            Build(leftChildTreeArrayIndex, segmentStartIndex, (segmentStartIndex + segmentEndIndex) / 2);
            Build(rightChildTreeArrayIndex, (segmentStartIndex + segmentEndIndex) / 2 + 1, segmentEndIndex);

            _treeArray[treeArrayIndex] = _treeArray[leftChildTreeArrayIndex].Combine(_treeArray[rightChildTreeArrayIndex]);
        }

        public override TQueryValue Query(int queryStartIndex, int queryEndIndex)
            => Query(0, queryStartIndex, queryEndIndex).QueryValue;

        private TQueryObject Query(int treeArrayIndex, int queryStartIndex, int queryEndIndex)
        {
            var queryObject = _treeArray[treeArrayIndex];

            if (queryObject.IsTotallyOverlappedBy(queryStartIndex, queryEndIndex))
                return queryObject;

            bool isLeftHalfOverlapped = queryObject.IsLeftHalfOverlappedBy(queryStartIndex, queryEndIndex);
            bool isRightHalfOverlapped = queryObject.IsRightHalfOverlappedBy(queryStartIndex, queryEndIndex);
            int leftChildTreeArrayIndex = 2 * treeArrayIndex + 1;
            int rightChildTreeArrayIndex = leftChildTreeArrayIndex + 1;

            if (isLeftHalfOverlapped && isRightHalfOverlapped)
                return Query(leftChildTreeArrayIndex, queryStartIndex, queryEndIndex)
                    .Combine(Query(rightChildTreeArrayIndex, queryStartIndex, queryEndIndex));
            else if (isLeftHalfOverlapped)
                return Query(leftChildTreeArrayIndex, queryStartIndex, queryEndIndex);
            else
                return Query(rightChildTreeArrayIndex, queryStartIndex, queryEndIndex);
        }

        public override void Update(int updateIndex, Func<TQueryValue, TQueryValue> updater)
            => Update(updateIndex, updateIndex, updater);

        public override void Update(int updateStartIndex, int updateEndIndex, Func<TQueryValue, TQueryValue> updater)
            => Update(0, updateStartIndex, updateEndIndex, updater);

        private void Update(int treeArrayIndex, int updateStartIndex, int updateEndIndex, Func<TQueryValue, TQueryValue> updater)
        {
            var queryObject = _treeArray[treeArrayIndex];

            if (queryObject.SegmentStartIndex == queryObject.SegmentEndIndex)
            {
                queryObject.Reinitialize(updater);
                return;
            }

            int leftChildTreeArrayIndex = 2 * treeArrayIndex + 1;
            int rightChildTreeArrayIndex = leftChildTreeArrayIndex + 1;

            if (queryObject.IsLeftHalfOverlappedBy(updateStartIndex, updateEndIndex))
            {
                Update(leftChildTreeArrayIndex, updateStartIndex, updateEndIndex, updater);
            }

            if (queryObject.IsRightHalfOverlappedBy(updateStartIndex, updateEndIndex))
            {
                Update(rightChildTreeArrayIndex, updateStartIndex, updateEndIndex, updater);
            }

            queryObject.Update(_treeArray[leftChildTreeArrayIndex], _treeArray[rightChildTreeArrayIndex]);
        }
    }
}
