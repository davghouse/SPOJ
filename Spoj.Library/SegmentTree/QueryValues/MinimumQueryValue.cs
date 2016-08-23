using System;

namespace Spoj.Library.SegmentTree.QueryValues
{
    public sealed class MinimumQueryValue : ISegmentTreeQueryValue<MinimumQueryValue>
    {
        public MinimumQueryValue()
        { }

        private MinimumQueryValue(int value)
        {
            Initialize(value);
        }

        public int Minimum { get; set; }

        public void Initialize(int value)
        {
            Minimum = value;
        }

        public MinimumQueryValue Combine(MinimumQueryValue rightAdjacentValue)
            => new MinimumQueryValue(Math.Min(Minimum, rightAdjacentValue.Minimum));
    }
}
