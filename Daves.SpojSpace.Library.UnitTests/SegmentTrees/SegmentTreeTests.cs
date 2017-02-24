using Microsoft.VisualStudio.TestTools.UnitTesting;
using Daves.SpojSpace.Library.SegmentTrees;
using Daves.SpojSpace.Library.SegmentTrees.AdHoc;
using Daves.SpojSpace.Library.SegmentTrees.QueryObjects;
using System;
using System.Collections.Generic;

namespace Daves.SpojSpace.Library.UnitTests.SegmentTrees
{
    [TestClass]
    public class SegmentTreeTests
    {
        private int[][] _sourceArrays;

        [TestInitialize]
        public void TestInitialize()
            => _sourceArrays = new int[][] {
                new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 },
                new[] { 3 },
                new[] { -3, -2 },
                new[] { 3, -1 },
                new[] { -3, 4 },
                new[] { 3, 2 },
                new[] { 0, 0, 0 },
                new[] { -3, 4, -2 },
                new[] { 0, 0, 0, 0, 0, 3, 0 },
                new[] { 3, 0, 0, 0, 5, 0, 0, 0 },
                new[] { 3, 0, 1, 0, 0, 0, 0, 0, 0 },
                new[] { 0, 0, 0, 0, 2, 0, 0, 0, 0, 3 },
                new[] { 3, 0, 0, 0, 0, 4, 0, 0, 0, 0, 3 },
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
        public void MinimumQueries()
            => VerifiesQueries<MinimumQueryObject>(NaiveSegmentTreeAlternatives.MinimumQuery);

        [TestMethod]
        public void MaximumQueries()
            => VerifiesQueries<MaximumQueryObject>(NaiveSegmentTreeAlternatives.MaximumQuery);

        [TestMethod]
        public void SumQueries()
            => VerifiesQueries<SumQueryObject>(NaiveSegmentTreeAlternatives.SumQuery);

        [TestMethod]
        public void ProductQueries()
            => VerifiesQueries<ProductQueryObject>(NaiveSegmentTreeAlternatives.ProductQuery);

        [TestMethod]
        public void MaximumSumQueries()
            => VerifiesQueries<MaximumSumQueryObject>(NaiveSegmentTreeAlternatives.MaximumSumQuery);

        private void VerifiesQueries<TQueryObject>(Func<IReadOnlyList<int>, int, int, int> naiveVerifier)
            where TQueryObject : SegmentTreeQueryObject<TQueryObject, int>, new()
        {
            for (int a = 0; a < _sourceArrays.Length; ++a)
            {
                var sourceArray = _sourceArrays[a];
                var nodeBasedSegmentTree = new NodeBasedSegmentTree<TQueryObject, int>(sourceArray);
                var arrayBasedSegmentTree = new ArrayBasedSegmentTree<TQueryObject, int>(sourceArray);
                var nonRecursiveSegmentTree = new NonRecursiveSegmentTree<TQueryObject, int>(sourceArray);

                for (int i = 0; i < sourceArray.Length; ++i)
                {
                    for (int j = i; j < sourceArray.Length; ++j)
                    {
                        int expected = naiveVerifier(sourceArray, i, j);
                        Assert.AreEqual(expected, nodeBasedSegmentTree.Query(i, j));
                        Assert.AreEqual(expected, arrayBasedSegmentTree.Query(i, j));
                        Assert.AreEqual(expected, nonRecursiveSegmentTree.Query(i, j));
                    }
                }
            }
        }

        [TestMethod]
        public void MinimumUpdates()
            => VerifiesUpdates<MinimumQueryObject>(NaiveSegmentTreeAlternatives.MinimumQuery);

        [TestMethod]
        public void MaximumUpdates()
            => VerifiesUpdates<MaximumQueryObject>(NaiveSegmentTreeAlternatives.MaximumQuery);

        [TestMethod]
        public void SumUpdates()
            => VerifiesUpdates<SumQueryObject>(NaiveSegmentTreeAlternatives.SumQuery);

        [TestMethod]
        public void ProductUpdates()
            => VerifiesUpdates<ProductQueryObject>(NaiveSegmentTreeAlternatives.ProductQuery);

        [TestMethod]
        public void MaximumSumUpdates()
            => VerifiesUpdates<MaximumSumQueryObject>(NaiveSegmentTreeAlternatives.MaximumSumQuery);

        private void VerifiesUpdates<TQueryObject>(Func<IReadOnlyList<int>, int, int, int> naiveVerifier)
            where TQueryObject : SegmentTreeQueryObject<TQueryObject, int>, new()
        {
            Func<int, int> updater = x => x + 1;

            for (int a = 0; a < _sourceArrays.Length; ++a)
            {
                var sourceArray = _sourceArrays[a];
                var nodeBasedSegmentTree = new NodeBasedSegmentTree<TQueryObject, int>(sourceArray);
                var arrayBasedSegmentTree = new ArrayBasedSegmentTree<TQueryObject, int>(sourceArray);
                var nonRecursiveSegmentTree = new NonRecursiveSegmentTree<TQueryObject, int>(sourceArray);

                for (int i = 0; i < sourceArray.Length; ++i)
                {
                    for (int j = i; j < sourceArray.Length; ++j)
                    {
                        NaiveSegmentTreeAlternatives.Update(sourceArray, i, j, updater);
                        nodeBasedSegmentTree.Update(i, j, updater);
                        arrayBasedSegmentTree.Update(i, j, updater);
                        nonRecursiveSegmentTree.Update(i, j, updater);

                        int expected = naiveVerifier(sourceArray, i, j);
                        Assert.AreEqual(expected, nodeBasedSegmentTree.Query(i, j));
                        Assert.AreEqual(expected, arrayBasedSegmentTree.Query(i, j));
                        Assert.AreEqual(expected, nonRecursiveSegmentTree.Query(i, j));
                    }
                }
            }
        }

        [TestMethod]
        public void LazySumSegmentTree_ForRandomOperations()
        {
            var rand = new Random();

            for (int a = 0; a < _sourceArrays.Length; ++a)
            {
                var sourceArray = _sourceArrays[a];
                var lazySumSegmentTree = new LazySumSegmentTree(sourceArray);
                var arrayBasedSegmentTree = new ArrayBasedSegmentTree<SumQueryObject, int>(sourceArray);

                for (int r = 0; r < 1000; ++r)
                {
                    int firstIndex = rand.Next(0, sourceArray.Length);
                    int secondIndex = rand.Next(0, sourceArray.Length);
                    int startIndex = Math.Min(firstIndex, secondIndex);
                    int endIndex = Math.Max(firstIndex, secondIndex);
                    int mode = rand.Next(2);

                    if (mode == 0)
                    {
                        NaiveSegmentTreeAlternatives.Update(sourceArray, startIndex, endIndex, x => x + r);
                        lazySumSegmentTree.Update(startIndex, endIndex, rangeAddition: r);
                        arrayBasedSegmentTree.Update(startIndex, endIndex, x => x + r);
                    }
                    else
                    {
                        int expected = NaiveSegmentTreeAlternatives.SumQuery(sourceArray, startIndex, endIndex);
                        Assert.AreEqual(expected, lazySumSegmentTree.SumQuery(startIndex, endIndex));
                        Assert.AreEqual(expected, arrayBasedSegmentTree.Query(startIndex, endIndex));
                    }
                }
            }
        }
    }
}
