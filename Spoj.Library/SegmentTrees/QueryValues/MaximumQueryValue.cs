using System;

namespace Spoj.Library.SegmentTrees.QueryValues
{
    public sealed class MaximumQueryValue : ISegmentTreeQueryValue<MaximumQueryValue>
    {
        public MaximumQueryValue()
        { }

        private MaximumQueryValue(int value)
        {
            Initialize(value);
        }

        public int Maximum { get; set; }

        public void Initialize(int value)
        {
            Maximum = value;
        }

        public MaximumQueryValue Combine(MaximumQueryValue rightAdjacentValue)
            => new MaximumQueryValue(Math.Max(Maximum, rightAdjacentValue.Maximum));
    }
}
