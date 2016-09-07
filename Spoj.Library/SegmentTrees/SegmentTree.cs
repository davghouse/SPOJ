using System.Collections.Generic;

namespace Spoj.Library.SegmentTrees
{
    // There are tons of articles about the type of segment tree used here, a good starting point:
    // http://wcipeg.com/wiki/Segment_tree. As far as I can tell this isn't the same as Wikipedia's
    // version of the segment tree (see https://en.wikipedia.org/wiki/Talk:Segment_tree#Rename).
    // So far the tree is static, without value/range updates to the source array.
    public abstract class SegmentTree<T> where T : class, ISegmentTreeQueryValue<T>, new()
    {
        protected readonly IReadOnlyList<int> _sourceArray;

        public SegmentTree(IReadOnlyList<int> sourceArray)
        {
            _sourceArray = sourceArray;
        }

        public abstract T Query(int queryStartIndex, int queryEndIndex);
    }
}
