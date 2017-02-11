using System;

namespace Daves.SpojSpace.Library.SegmentTrees.QueryObjects
{
    public sealed class ProductQueryObject : SegmentTreeQueryObject<ProductQueryObject, int>
    {
        public override int QueryValue
        {
            get { return Product; }
            protected set { Product = value; }
        }

        private int Product { get; set; }

        public override ProductQueryObject Combine(ProductQueryObject rightAdjacentObject)
            => new ProductQueryObject
            {
                SegmentStartIndex = SegmentStartIndex,
                SegmentEndIndex = rightAdjacentObject.SegmentEndIndex,
                Product = Product * rightAdjacentObject.Product
            };

        public override void Update(ProductQueryObject updatedLeftChild, ProductQueryObject updatedRightChild)
            => Product = updatedLeftChild.Product * updatedRightChild.Product;
    }
}
