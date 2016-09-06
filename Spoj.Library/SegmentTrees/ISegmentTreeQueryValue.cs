namespace Spoj.Library.SegmentTrees
{
    // The generic parameter is kind of confusing here. The interface gets used in the same way IEquatable<T> gets used.
    // I thought about renaming to ISegmentTreeQueryObject and then having a property like int Value { get; }.
    // That would be convenient right now, and if necessary the return type of Value could become a generic parameter.
    public interface ISegmentTreeQueryValue<T> where T : class, new()
    {
        // The segment tree only works for querying over integer arrays right now. Leaf nodes will need to
        // initialize themselves from the integer value at the specific array index their node corresponds to.
        void Initialize(int value);

        // The generic parameter T for SegmentTree requires T : ISegmentTreeQueryValue<T>, which should be thought of
        // as T has to be a SegmentTreeQueryValue. In order to be that, it needs to know how to combine itself with
        // the query value for some segment adjacent and to its right, which this method is trying to convey.
        T Combine(T rightAdjacentValue);
    }
}