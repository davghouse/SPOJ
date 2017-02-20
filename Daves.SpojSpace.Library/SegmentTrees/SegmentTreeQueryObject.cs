using System;

namespace Daves.SpojSpace.Library.SegmentTrees
{
    // Generic TQueryObject is kind of like the familiar EquatableObject : IEquatable<EquatableObject>.
    // For more information see: https://blogs.msdn.microsoft.com/ericlippert/2011/02/03/curiouser-and-curiouser/
    // Query objects know about their segment, but don't know about their 'children'--that's a segment tree's job.
    public abstract class SegmentTreeQueryObject<TQueryObject, TQueryValue>
        where TQueryObject : SegmentTreeQueryObject<TQueryObject, TQueryValue>, new()
    {
        // A query object wraps a query value, of the same type as in the source array and
        // the data type of the answer we're looking for when querying.
        public abstract TQueryValue QueryValue { get; protected set; }

        // 'Readonly' property for the start index of the array range this query object corresponds to.
        public int SegmentStartIndex { get; protected set; }

        // 'Readonly' property for the end index of the array range this query object corresponds to.
        public int SegmentEndIndex { get; protected set; }

        // For constructing leaves, given an index of the source array and the corresponding value.
        // It would be nice if we could have some actual constructors but we're using these objects
        // as generic type parameters so it'd be annoying to support that (activator or factory).
        public virtual void Initialize(int index, TQueryValue value)
        {
            SegmentStartIndex = SegmentEndIndex = index;
            QueryValue = value;
        }

        // When the source array is updated, it's necessary for leaf nodes to reinitialize themselves.
        // The updater is provided in case the new query value is defined in terms of its current value.
        public virtual void Reinitialize(Func<TQueryValue, TQueryValue> updater)
            => QueryValue = updater(QueryValue);

        // A query object can combine into a new object with query objects from segments adjacent and to the right.
        public abstract TQueryObject Combine(TQueryObject rightAdjacentObject);

        // A query object needs to update itself when the source array gets updated, given its children
        // after they've been updated. Like Combine, but from a different perspective. It's easy to use
        // Update to implement Combine; see MaximumSumQueryObject.
        public abstract void Update(TQueryObject updatedLeftChild, TQueryObject updatedRightChild);

        // The given range starts before the segment starts and ends after the segment ends.
        public bool IsTotallyOverlappedBy(int startIndex, int endIndex)
            => startIndex <= SegmentStartIndex && endIndex >= SegmentEndIndex;

        // Assumed that some overlap exists, just not necessarily over the left half.
        public bool IsLeftHalfOverlappedBy(int startIndex, int endIndex)
            => startIndex <= (SegmentStartIndex + SegmentEndIndex) / 2;

        // Assumed that some overlap exists, just not necessarily over the right half.
        public bool IsRightHalfOverlappedBy(int startIndex, int endIndex)
            => endIndex > (SegmentStartIndex + SegmentEndIndex) / 2;
    }
}
