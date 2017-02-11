using System;
using System.Collections.Generic;

namespace Daves.SpojSpace.Library.SegmentTrees
{
    // This implementation is taken from http://codeforces.com/blog/entry/18051, with slight adjustments
    // to support querying over a closed-interval and using nulls to avoid necessitating sentinel values.
    // Bitwise operators provide a small performance benefit, but just here to get practice using them.
    public sealed class NonRecursiveSegmentTree<TQueryObject, TQueryValue> : SegmentTree<TQueryObject, TQueryValue>
        where TQueryObject : SegmentTreeQueryObject<TQueryObject, TQueryValue>, new()
    {
        private readonly TQueryObject[] _treeArray;

        public NonRecursiveSegmentTree(IReadOnlyList<TQueryValue> sourceArray)
            : base(sourceArray)
        {
            _treeArray = new TQueryObject[2 * sourceArray.Count];
            Build();
        }

        private void Build()
        {
            for (int i = 0; i < _sourceArray.Count; ++i)
            {
                _treeArray[_sourceArray.Count + i] = new TQueryObject();
                _treeArray[_sourceArray.Count + i].Initialize(i, _sourceArray[i]);
            }

            for (int i = _sourceArray.Count - 1; i > 0; --i)
            {
                _treeArray[i] = _treeArray[i << 1].Combine(_treeArray[i << 1 | 1]);
            }
        }

        public override TQueryValue Query(int queryStartIndex, int queryEndIndex)
        {
            if (queryStartIndex == queryEndIndex)
                return _treeArray[queryStartIndex + _sourceArray.Count].QueryValue;

            TQueryObject leftResult = null, rightResult = null;
            for (queryStartIndex += _sourceArray.Count, queryEndIndex += _sourceArray.Count + 1;
                queryStartIndex < queryEndIndex;
                queryStartIndex >>= 1, queryEndIndex >>= 1)
            {
                if ((queryStartIndex & 1) == 1)
                {
                    leftResult = leftResult == null
                        ? _treeArray[queryStartIndex++]
                        : leftResult.Combine(_treeArray[queryStartIndex++]);
                }

                if ((queryEndIndex & 1) == 1)
                {
                    rightResult = rightResult == null
                        ? _treeArray[--queryEndIndex]
                        : _treeArray[--queryEndIndex].Combine(rightResult);
                }
            }

            if (leftResult != null && rightResult != null)
                return leftResult.Combine(rightResult).QueryValue;
            else if (leftResult != null)
                return leftResult.QueryValue;
            else
                return rightResult.QueryValue;
        }

        public override void Update(int updateIndex, Func<TQueryValue, TQueryValue> updater)
        {
            for (_treeArray[updateIndex += _sourceArray.Count].Reinitialize(updater);
                 updateIndex > 1;
                 updateIndex >>= 1)
            {
                int parentIndex = updateIndex >> 1;

                _treeArray[parentIndex] = _treeArray[parentIndex << 1].Combine(_treeArray[parentIndex << 1 | 1]);
            }
        }

        // This is really slow, I'm not sure if there's a good normal (non-lazy) range update for this segment tree.
        public override void Update(int updateStartIndex, int updateEndIndex, Func<TQueryValue, TQueryValue> updater)
        {
            for (int i = updateStartIndex; i <= updateEndIndex; ++i)
            {
                Update(i, updater);
            }
        }
    }
}
