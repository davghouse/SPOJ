using Spoj.Library.Helpers;
using System.Collections.Generic;

namespace Spoj.Library.SegmentTrees.AdHoc
{
    public sealed class LazySumSegmentTree
    {
        private readonly IReadOnlyList<int> _sourceArray;
        private readonly QueryObject[] _treeArray;

        public LazySumSegmentTree(IReadOnlyList<int> sourceArray)
        {
            _sourceArray = sourceArray;
            _treeArray = new QueryObject[2 * MathHelper.FirstPowerOfTwoEqualOrGreater(_sourceArray.Count) - 1];
            Build(0, 0, _sourceArray.Count - 1);
        }

        private void Build(int treeArrayIndex, int segmentStartIndex, int segmentEndIndex)
        {
            if (segmentStartIndex == segmentEndIndex)
            {
                _treeArray[treeArrayIndex] = new QueryObject(segmentStartIndex, _sourceArray[segmentStartIndex]);
                return;
            }

            int leftChildTreeArrayIndex = 2 * treeArrayIndex + 1;
            int rightChildTreeArrayIndex = leftChildTreeArrayIndex + 1;

            Build(leftChildTreeArrayIndex, segmentStartIndex, (segmentStartIndex + segmentEndIndex) / 2);
            Build(rightChildTreeArrayIndex, (segmentStartIndex + segmentEndIndex) / 2 + 1, segmentEndIndex);

            _treeArray[treeArrayIndex] = _treeArray[leftChildTreeArrayIndex].Combine(_treeArray[rightChildTreeArrayIndex]);
        }

        public int SumQuery(int queryStartIndex, int queryEndIndex)
            => SumQuery(0, queryStartIndex, queryEndIndex).Sum;

        // Instead of returning the children object directly, we have to add on the parent's range addition. The
        // children query object knows the subset of the parent segment it intersects with, and everything in
        // there needs the additions that were applied to the parent segment as a whole. It's kind of weird, any
        // pending range additions specifically for the children object gets brought out and added to the sum when
        // we do .Combine or .Sum, but recursively it makes sense: the children object has a sum but still needs
        // to know about the parent's range additions.
        private QueryObject SumQuery(int treeArrayIndex, int queryStartIndex, int queryEndIndex)
        {
            var parentQueryObject = _treeArray[treeArrayIndex];

            if (parentQueryObject.IsTotallyOverlappedBy(queryStartIndex, queryEndIndex))
                return parentQueryObject;

            bool leftHalfOverlaps = parentQueryObject.DoesLeftHalfOverlapWith(queryStartIndex, queryEndIndex);
            bool rightHalfOverlaps = parentQueryObject.DoesRightHalfOverlapWith(queryStartIndex, queryEndIndex);
            int leftChildTreeArrayIndex = 2 * treeArrayIndex + 1;
            int rightChildTreeArrayIndex = leftChildTreeArrayIndex + 1;
            QueryObject childrenQueryObject;

            if (leftHalfOverlaps && rightHalfOverlaps)
            {
                childrenQueryObject = SumQuery(leftChildTreeArrayIndex, queryStartIndex, queryEndIndex)
                    .Combine(SumQuery(rightChildTreeArrayIndex, queryStartIndex, queryEndIndex));
            }
            else if (leftHalfOverlaps)
            {
                childrenQueryObject = SumQuery(leftChildTreeArrayIndex, queryStartIndex, queryEndIndex);
            }
            else
            {
                childrenQueryObject = SumQuery(rightChildTreeArrayIndex, queryStartIndex, queryEndIndex);
            }

            return new QueryObject(
                childrenQueryObject.SegmentStartIndex,
                childrenQueryObject.SegmentEndIndex,
                childrenQueryObject.Sum)
            {
                RangeAddition = parentQueryObject.RangeAddition
            };
        }

        public void Update(int updateIndex, int rangeAddition)
            => Update(updateIndex, updateIndex, rangeAddition);

        public void Update(int updateStartIndex, int updateEndIndex, int rangeAddition)
            => Update(0, updateStartIndex, updateEndIndex, rangeAddition);

        private void Update(int treeArrayIndex, int updateStartIndex, int updateEndIndex, int rangeAddition)
        {
            var queryObject = _treeArray[treeArrayIndex];

            if (queryObject.IsTotallyOverlappedBy(updateStartIndex, updateEndIndex))
            {
                queryObject.Update(rangeAddition);
                return;
            }

            int leftChildTreeArrayIndex = 2 * treeArrayIndex + 1;
            int rightChildTreeArrayIndex = leftChildTreeArrayIndex + 1;

            if (queryObject.DoesLeftHalfOverlapWith(updateStartIndex, updateEndIndex))
            {
                Update(leftChildTreeArrayIndex, updateStartIndex, updateEndIndex, rangeAddition);
            }

            if (queryObject.DoesRightHalfOverlapWith(updateStartIndex, updateEndIndex))
            {
                Update(rightChildTreeArrayIndex, updateStartIndex, updateEndIndex, rangeAddition);
            }

            queryObject.Update(_treeArray[leftChildTreeArrayIndex], _treeArray[rightChildTreeArrayIndex]);
        }

        private sealed class QueryObject
        {
            public int Sum
                => SumWithoutRangeAddition + SumFromRangeAddition;

            public int SumFromRangeAddition
                => RangeAddition * SegmentLength;

            public int SumWithoutRangeAddition { get; internal set; }
            public int RangeAddition { get; internal set; }

            public int SegmentStartIndex { get; }
            public int SegmentEndIndex { get; }
            public int SegmentLength => SegmentEndIndex - SegmentStartIndex + 1;

            public QueryObject(int index, int value)
            {
                SegmentStartIndex = SegmentEndIndex = index;
                SumWithoutRangeAddition = value;
            }

            public QueryObject(int segmentStartIndex, int segmentEndIndex, int sumWithoutRangeAddition)
            {
                SegmentStartIndex = segmentStartIndex;
                SegmentEndIndex = segmentEndIndex;
                SumWithoutRangeAddition = sumWithoutRangeAddition;
            }

            public QueryObject Combine(QueryObject rightAdjacentObject)
                => new QueryObject(
                    segmentStartIndex: SegmentStartIndex,
                    segmentEndIndex: rightAdjacentObject.SegmentEndIndex,
                    sumWithoutRangeAddition: Sum + rightAdjacentObject.Sum);

            public void Update(int rangeAddition)
                => RangeAddition += rangeAddition;

            public void Update(QueryObject updatedLeftChild, QueryObject updatedRightChild)
                => SumWithoutRangeAddition = updatedLeftChild.Sum + updatedRightChild.Sum;

            public bool IsTotallyOverlappedBy(int startIndex, int endIndex)
                => startIndex <= SegmentStartIndex && endIndex >= SegmentEndIndex;

            public bool DoesLeftHalfOverlapWith(int startIndex, int endIndex)
                => startIndex <= (SegmentStartIndex + SegmentEndIndex) / 2;

            public bool DoesRightHalfOverlapWith(int startIndex, int endIndex)
                => endIndex > (SegmentStartIndex + SegmentEndIndex) / 2;
        }
    }
}
