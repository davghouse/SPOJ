using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spoj.Library.SegmentTrees;
using Spoj.Library.SegmentTrees.QueryValues;

namespace Spoj.Library.Tests.SegmentTrees
{
    [TestClass]
    public class SegmentTreeTests
    {
        private static int[][] _sourceArrays = new int[][] {
                new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 },
                new[] { 3 },
                new[] { -3, -3 },
                new[] { 3, -3 },
                new[] { -3, 3 },
                new[] { 3, 3 },
                new[] { 0, 0, 0 },
                new[] { -3, 2, -2 },
                new[] { 0, 0, 0, 0, 0, 3, 0 },
                new[] { 3, 0, 0, 0, 0, 0, 0, 0 },
                new[] { 3, 0, 0, 0, 0, 0, 0, 0, 0 },
                new[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 3 },
                new[] { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3 },
                new[] { 1, 2, 3, 0, 0, -1, 0, 0, 3, 2, 1 },
                new[] { -1, 1, 2, 3, 4, 5, 6, 7, 0, 19, -23, 14, 0, 167, 123, -1344, 13434 },
                new[] {-3, -2, 1, 3, -2, -4, 6, 7, -3, 2, -1, -1, 4, -6, 4, -1, 3, 5, -6, 7, -1, 0, 0, -3, 4, 2, -1, 2, 9, -7, 10, -1},
                new[]
                {
                    78, 73, 33, 5, -71, 53, 50, -100, -25, 9, -58, 62, 47, -21, 23, -9, -3, -82, -59, -94, -45, -92, 74, -63, -59, -96,
                    50, -50, -37, 1, 59, -85, -18, 36, 0, 29, -12, 6, 69, -48, -89, -61, -94, 62, -2, -41, -31, -45, -61, 89, 87, -46,
                    -72, 60, -63, 65, -64, -47, -64, 61, 45, -45, -87, -49, 27, 77, -50, -84, -25, -9, 82, -87, -83, 33, -52, 29, 99,
                    -45, -78, -20, -94, -10, -99, -49, -84, -25, 29, 100, 31, -34, 42, -51, 24, 94, -29, -85, 91, 37, 94, -37
                }
            };

        [TestMethod]
        public void VerifiesMinimumQueries()
        {
            for (int a = 0; a < _sourceArrays.Length; ++a)
            {
                var sourceArray = _sourceArrays[a];
                var nodeBasedSegmentTree = new NodeBasedSegmentTree<MinimumQueryValue>(sourceArray);
                var arrayBasedSegmentTree = new ArrayBasedSegmentTree<MinimumQueryValue>(sourceArray);
                var nonRecursiveSegmentTree = new NonRecursiveSegmentTree<MinimumQueryValue>(sourceArray);

                for (int i = 0; i < sourceArray.Length; ++i)
                {
                    for (int j = i; j < sourceArray.Length; ++j)
                    {
                        var expected = NaiveSegmentTreeAlternatives.MinimumQuery(sourceArray, i, j);
                        Assert.AreEqual(expected, nodeBasedSegmentTree.Query(i, j).Minimum);
                        Assert.AreEqual(expected, arrayBasedSegmentTree.Query(i, j).Minimum);
                        Assert.AreEqual(expected, nonRecursiveSegmentTree.Query(i, j).Minimum);
                    }
                }
            }
        }

        [TestMethod]
        public void VerifiesMaximumQueries()
        {
            for (int a = 0; a < _sourceArrays.Length; ++a)
            {
                var sourceArray = _sourceArrays[a];
                var nodeBasedSegmentTree = new NodeBasedSegmentTree<MaximumQueryValue>(sourceArray);
                var arrayBasedSegmentTree = new ArrayBasedSegmentTree<MaximumQueryValue>(sourceArray);
                var nonRecursiveSegmentTree = new NonRecursiveSegmentTree<MaximumQueryValue>(sourceArray);

                for (int i = 0; i < sourceArray.Length; ++i)
                {
                    for (int j = i; j < sourceArray.Length; ++j)
                    {
                        var expected = NaiveSegmentTreeAlternatives.MaximumQuery(sourceArray, i, j);
                        Assert.AreEqual(expected, nodeBasedSegmentTree.Query(i, j).Maximum);
                        Assert.AreEqual(expected, arrayBasedSegmentTree.Query(i, j).Maximum);
                        Assert.AreEqual(expected, nonRecursiveSegmentTree.Query(i, j).Maximum);
                    }
                }
            }
        }

        [TestMethod]
        public void VerifiesSumQueries()
        {
            for (int a = 0; a < _sourceArrays.Length; ++a)
            {
                var sourceArray = _sourceArrays[a];
                var nodeBasedSegmentTree = new NodeBasedSegmentTree<SumQueryValue>(sourceArray);
                var arrayBasedSegmentTree = new ArrayBasedSegmentTree<SumQueryValue>(sourceArray);
                var nonRecursiveSegmentTree = new NonRecursiveSegmentTree<SumQueryValue>(sourceArray);

                for (int i = 0; i < sourceArray.Length; ++i)
                {
                    for (int j = i; j < sourceArray.Length; ++j)
                    {
                        var expected = NaiveSegmentTreeAlternatives.SumQuery(sourceArray, i, j);
                        Assert.AreEqual(expected, nodeBasedSegmentTree.Query(i, j).Sum);
                        Assert.AreEqual(expected, arrayBasedSegmentTree.Query(i, j).Sum);
                        Assert.AreEqual(expected, nonRecursiveSegmentTree.Query(i, j).Sum);
                    }
                }
            }
        }

        [TestMethod]
        public void VerifiesProductQueries()
        {
            for (int a = 0; a < _sourceArrays.Length; ++a)
            {
                var sourceArray = _sourceArrays[a];
                var nodeBasedSegmentTree = new NodeBasedSegmentTree<ProductQueryValue>(sourceArray);
                var arrayBasedSegmentTree = new ArrayBasedSegmentTree<ProductQueryValue>(sourceArray);
                var nonRecursiveSegmentTree = new NonRecursiveSegmentTree<ProductQueryValue>(sourceArray);

                for (int i = 0; i < sourceArray.Length; ++i)
                {
                    for (int j = i; j < sourceArray.Length; ++j)
                    {
                        var expected = NaiveSegmentTreeAlternatives.ProductQuery(sourceArray, i, j);
                        Assert.AreEqual(expected, nodeBasedSegmentTree.Query(i, j).Product);
                        Assert.AreEqual(expected, arrayBasedSegmentTree.Query(i, j).Product);
                        Assert.AreEqual(expected, nonRecursiveSegmentTree.Query(i, j).Product);
                    }
                }
            }
        }

        [TestMethod]
        public void VerifiesMaximumSumQueries()
        {
            for (int a = 0; a < _sourceArrays.Length; ++a)
            {
                var sourceArray = _sourceArrays[a];
                var nodeBasedSegmentTree = new NodeBasedSegmentTree<MaximumSumQueryValue>(sourceArray);
                var arrayBasedSegmentTree = new ArrayBasedSegmentTree<MaximumSumQueryValue>(sourceArray);
                var nonRecursiveSegmentTree = new NonRecursiveSegmentTree<MaximumSumQueryValue>(sourceArray);

                for (int i = 0; i < sourceArray.Length; ++i)
                {
                    for (int j = i; j < sourceArray.Length; ++j)
                    {
                        var expected = NaiveSegmentTreeAlternatives.MaximumSumQuery(sourceArray, i, j);
                        Assert.AreEqual(expected, nodeBasedSegmentTree.Query(i, j).MaximumSum);
                        Assert.AreEqual(expected, arrayBasedSegmentTree.Query(i, j).MaximumSum);
                        Assert.AreEqual(expected, nonRecursiveSegmentTree.Query(i, j).MaximumSum);
                    }
                }
            }
        }
    }
}