using System;

namespace Daves.SpojSpace.Library.SegmentTrees.QueryObjects
{
    // Given a query range, this value represents the maximum sum for any contiguous subrange.
    public sealed class MaximumSumQueryObject : SegmentTreeQueryObject<MaximumSumQueryObject, int>
    {
        public override void Initialize(int index, int value)
        {
            base.Initialize(index, value);
            Sum = MaximumLeftStartingSum = MaximumRightStartingSum = value;
        }

        public override void Reinitialize(Func<int, int> updater)
            => Sum = MaximumSum = MaximumLeftStartingSum = MaximumRightStartingSum = updater(MaximumSum);

        public override int QueryValue
        {
            get { return MaximumSum; }
            protected set { MaximumSum = value; }
        }

        private int Sum { get; set; }
        private int MaximumSum { get; set; }
        private int MaximumLeftStartingSum { get; set; }  // [-> ... ]
        private int MaximumRightStartingSum { get; set; } // [ ... <-]

        public override MaximumSumQueryObject Combine(MaximumSumQueryObject rightAdjacentObject)
            => new MaximumSumQueryObject
            {
                SegmentStartIndex = SegmentStartIndex,
                SegmentEndIndex = rightAdjacentObject.SegmentEndIndex,
                Sum = GetCombinedSum(this, rightAdjacentObject),
                MaximumSum = GetCombinedMaximumSum(this, rightAdjacentObject),
                MaximumLeftStartingSum = GetCombinedMaximumLeftStartingSum(this, rightAdjacentObject),
                MaximumRightStartingSum = GetCombinedMaximumRightStartingSum(this, rightAdjacentObject)
            };

        public override void Update(MaximumSumQueryObject updatedLeftChild, MaximumSumQueryObject updatedRightChild)
        {
            Sum = GetCombinedSum(updatedLeftChild, updatedRightChild);
            MaximumSum = GetCombinedMaximumSum(updatedLeftChild, updatedRightChild);
            MaximumLeftStartingSum = GetCombinedMaximumLeftStartingSum(updatedLeftChild, updatedRightChild);
            MaximumRightStartingSum = GetCombinedMaximumRightStartingSum(updatedLeftChild, updatedRightChild);
        }

        private static int GetCombinedSum(MaximumSumQueryObject leftAdjacentObject, MaximumSumQueryObject rightAdjacentObject)
            // The sum is just the sum of both.
            => leftAdjacentObject.Sum + rightAdjacentObject.Sum;

        private static int GetCombinedMaximumSum(MaximumSumQueryObject leftAdjacentObject, MaximumSumQueryObject rightAdjacentObject)
            // The maximum sum either intersects both segments, or is entirely in one.
            => Math.Max(
                leftAdjacentObject.MaximumRightStartingSum + rightAdjacentObject.MaximumLeftStartingSum,
                Math.Max(leftAdjacentObject.MaximumSum, rightAdjacentObject.MaximumSum));

        private static int GetCombinedMaximumLeftStartingSum(MaximumSumQueryObject leftAdjacentObject, MaximumSumQueryObject rightAdjacentObject)
            // The maximum left starting sum starts at the left, and may or may not cross into the right.
            => Math.Max(
                leftAdjacentObject.Sum + rightAdjacentObject.MaximumLeftStartingSum,
                leftAdjacentObject.MaximumLeftStartingSum);

        private static int GetCombinedMaximumRightStartingSum(MaximumSumQueryObject leftAdjacentObject, MaximumSumQueryObject rightAdjacentObject)
            // The maximum right starting sum starts at the right, and may or may not cross into the left.
            => Math.Max(
                rightAdjacentObject.Sum + leftAdjacentObject.MaximumRightStartingSum,
                rightAdjacentObject.MaximumRightStartingSum);
    }
}
