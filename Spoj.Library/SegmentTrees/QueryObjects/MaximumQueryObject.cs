using System;

namespace Spoj.Library.SegmentTrees.QueryObjects
{
    public sealed class MaximumQueryObject : SegmentTreeQueryObject<MaximumQueryObject, int>
    {
        public override int QueryValue
        {
            get { return Maximum; }
            protected set { Maximum = value; }
        }

        private int Maximum { get; set; }

        public override MaximumQueryObject Combine(MaximumQueryObject rightAdjacentObject)
            => new MaximumQueryObject
            {
                SegmentStartIndex = SegmentStartIndex,
                SegmentEndIndex = rightAdjacentObject.SegmentEndIndex,
                Maximum = Math.Max(Maximum, rightAdjacentObject.Maximum)
            };

        public override void Update(MaximumQueryObject updatedLeftChild, MaximumQueryObject updatedRightChild)
            => Maximum = Math.Max(updatedLeftChild.Maximum, updatedRightChild.Maximum);
    }
}
