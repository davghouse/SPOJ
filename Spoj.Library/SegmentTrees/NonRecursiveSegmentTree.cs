using System.Collections.Generic;

namespace Spoj.Library.SegmentTrees
{
    // This implementation is ripped from http://codeforces.com/blog/entry/18051, with slight adjustments
    // to support querying over a closed-interval and using nulls to avoid necessitating sentinel values.
    // Bitwise operators provide a small performance benefit, but the non-bitwise versions are commented nearby.
    public sealed class NonRecursiveSegmentTree<T> : SegmentTree<T> where T : class, ISegmentTreeQueryValue<T>, new()
    {
        private readonly T[] _treeArray;

        public NonRecursiveSegmentTree(IReadOnlyList<int> sourceArray)
            : base(sourceArray)
        {
            _treeArray = new T[2 * sourceArray.Count];
            Build();
        }

        private void Build()
        {
            for (int i = 0; i < _sourceArray.Count; ++i)
            {
                _treeArray[_sourceArray.Count + i] = new T();
                _treeArray[_sourceArray.Count + i].Initialize(_sourceArray[i]);
            }

            for (int i = _sourceArray.Count - 1; i > 0; --i)
            {
                //_treeArray[i] = _treeArray[2 * i].Combine(_treeArray[2 * i + 1]);
                _treeArray[i] = _treeArray[i << 1].Combine(_treeArray[i << 1 | 1]);
            }
        }

        public override T Query(int queryStartIndex, int queryEndIndex)
        {
            if (queryStartIndex == queryEndIndex)
                return _treeArray[queryStartIndex + _sourceArray.Count];

            T leftResult = null, rightResult = null;
            for (queryStartIndex += _sourceArray.Count, queryEndIndex += _sourceArray.Count + 1;
                queryStartIndex < queryEndIndex;
                //queryStartIndex /= 2, queryEndIndex /= 2)
                queryStartIndex >>= 1, queryEndIndex >>= 1)
            {
                //if (queryStartIndex % 2 == 1)
                if ((queryStartIndex & 1) == 1)
                {
                    leftResult = leftResult == null
                        ? _treeArray[queryStartIndex++]
                        : leftResult.Combine(_treeArray[queryStartIndex++]);
                }
                //if (queryEndIndex % 2 == 1)
                if ((queryEndIndex & 1) == 1)
                {
                    rightResult = rightResult == null
                        ? _treeArray[--queryEndIndex]
                        : _treeArray[--queryEndIndex].Combine(rightResult);
                }
            }

            if (leftResult != null && rightResult != null)
                return leftResult.Combine(rightResult);
            else if (leftResult != null)
                return leftResult;
            else
                return rightResult;
        }
    }
}
