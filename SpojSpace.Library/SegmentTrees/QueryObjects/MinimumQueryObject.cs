using System;

namespace SpojSpace.Library.SegmentTrees.QueryObjects
{
    public sealed class MinimumQueryObject : SegmentTreeQueryObject<MinimumQueryObject, int>
    {
        public override int QueryValue
        {
            get { return Minimum; }
            protected set { Minimum = value; }
        }

        private int Minimum { get; set; }

        public override MinimumQueryObject Combine(MinimumQueryObject rightAdjacentObject)
            => new MinimumQueryObject
            {
                SegmentStartIndex = SegmentStartIndex,
                SegmentEndIndex = rightAdjacentObject.SegmentEndIndex,
                Minimum = Math.Min(Minimum, rightAdjacentObject.Minimum)
            };

        public override void Update(MinimumQueryObject updatedLeftChild, MinimumQueryObject updatedRightChild)
            => Minimum = Math.Min(updatedLeftChild.Minimum, updatedRightChild.Minimum);
    }
}
