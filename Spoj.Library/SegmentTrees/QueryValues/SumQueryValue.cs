namespace Spoj.Library.SegmentTrees.QueryValues
{
    public sealed class SumQueryValue : ISegmentTreeQueryValue<SumQueryValue>
    {
        public SumQueryValue()
        { }

        private SumQueryValue(int value)
        {
            Initialize(value);
        }

        public int Sum { get; set; }

        public void Initialize(int value)
        {
            Sum = value;
        }

        public SumQueryValue Combine(SumQueryValue rightAdjacentValue)
            => new SumQueryValue(Sum + rightAdjacentValue.Sum);
    }
}
