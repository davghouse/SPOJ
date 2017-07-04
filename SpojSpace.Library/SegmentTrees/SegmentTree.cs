using System;
using System.Collections.Generic;

namespace SpojSpace.Library.SegmentTrees
{
    // There are tons of articles about the type of segment tree used here, a good starting point:
    // http://wcipeg.com/wiki/Segment_tree. As far as I can tell this isn't the same as Wikipedia's
    // version of the segment tree (see https://en.wikipedia.org/wiki/Talk:Segment_tree#Rename).
    // Right now this class hierarchy doesn't concern itself with doing lazy updates, as until
    // I get more experience with those I'm not sure I could make a generalizable solution. For
    // now lazy implementations are in the AdHoc namespace, for easy testing/comparing against these.
    // To get a better understanding of how things work, start with NodeBasedSegmentTree.
    public abstract class SegmentTree<TQueryObject, TQueryValue>
        where TQueryObject : SegmentTreeQueryObject<TQueryObject, TQueryValue>, new()
    {
        protected readonly IReadOnlyList<TQueryValue> _sourceArray;

        public SegmentTree(IReadOnlyList<TQueryValue> sourceArray)
        {
            _sourceArray = sourceArray;
        }

        public abstract TQueryValue Query(int queryStartIndex, int queryEndIndex);

        public abstract void Update(int updateIndex, Func<TQueryValue, TQueryValue> updater);

        public abstract void Update(int updateStartIndex, int updateEndIndex, Func<TQueryValue, TQueryValue> updater);
    }
}
