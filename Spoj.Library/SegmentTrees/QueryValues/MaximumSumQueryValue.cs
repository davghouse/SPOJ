using System;

namespace Spoj.Library.SegmentTrees.QueryValues
{
    // Given a query range, this value represents the maximum sum for any contiguous subrange.
    public sealed class MaximumSumQueryValue : ISegmentTreeQueryValue<MaximumSumQueryValue>
    {
        public MaximumSumQueryValue()
        { }

        private int Sum { get; set; }
        public int MaximumSum { get; set; }
        private int MaximumLeftStartingSum { get; set; }
        private int MaximumRightStartingSum { get; set; }

        public void Initialize(int value)
            => Sum = MaximumSum = MaximumLeftStartingSum = MaximumRightStartingSum = value;

        public MaximumSumQueryValue Combine(MaximumSumQueryValue rightAdjacentValue)
            => new MaximumSumQueryValue
            {
                // The sum is just the sum of both.
                Sum = Sum + rightAdjacentValue.Sum,

                // The maximum sum either intersects both segments, or is entirely in one.
                MaximumSum = Math.Max(
                    MaximumRightStartingSum + rightAdjacentValue.MaximumLeftStartingSum,
                    Math.Max(MaximumSum, rightAdjacentValue.MaximumSum)),

                // The maximum left starting sum starts at the left, and may or may not cross into the right.
                MaximumLeftStartingSum = Math.Max(
                    Sum + rightAdjacentValue.MaximumLeftStartingSum,
                    MaximumLeftStartingSum),

                // The maximum right starting sum starts at the right, and may or may not cross into the left.
                MaximumRightStartingSum = Math.Max(
                    rightAdjacentValue.Sum + MaximumRightStartingSum,
                    rightAdjacentValue.MaximumRightStartingSum)
            };
    }
}
