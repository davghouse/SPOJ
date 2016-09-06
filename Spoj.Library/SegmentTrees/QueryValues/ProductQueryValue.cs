namespace Spoj.Library.SegmentTrees.QueryValues
{
    public sealed class ProductQueryValue : ISegmentTreeQueryValue<ProductQueryValue>
    {
        public ProductQueryValue()
        { }

        private ProductQueryValue(int value)
        {
            Initialize(value);
        }

        public int Product { get; set; }

        public void Initialize(int value)
        {
            Product = value;
        }

        public ProductQueryValue Combine(ProductQueryValue rightAdjacentValue)
            => new ProductQueryValue(Product * rightAdjacentValue.Product);
    }
}
