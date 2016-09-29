using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spoj.Library.BinaryIndexedTrees;
using System;

namespace Spoj.Library.UnitTests.BinaryIndexedTrees
{
    [TestClass]
    public class BinaryIndexedTreeTests
    {
        private int[][] _sourceArrays;

        [TestInitialize]
        public void TestInitialize()
        {
            _sourceArrays = new int[][] {
                new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 },
                new[] { 3 },
                new[] { -3, -2 },
                new[] { 3, -1 },
                new[] { -3, 4 },
                new[] { 3, 2 },
                new[] { 0, 0, 0 },
                new[] { -3, 4, -2 },
                new[] { 0, 4, 0, 0, 0, 3, 0 },
                new[] { 3, 0, 5, 0, 5, 0, 0, 0 },
                new[] { 3, 0, 1, 0, 0, 2, 0, 0, 0 },
                new[] { 0, 9, 0, 0, 2, 0, 0, 0, 0, 3 },
                new[] { 3, 0, 0, 0, 0, 4, 0, 0, 0, 0, 3 },
                new[] { 1, 2, 3, 8, 0, -1, 0, 0, 3, 2, 1 },
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
        }

        [TestMethod]
        public void VerifiesPURQBinaryIndexedTree()
        {
            var rand = new Random();

            for (int a = 0; a < _sourceArrays.Length; ++a)
            {
                var sourceArray = _sourceArrays[a];
                var purqBinaryIndexedTree = new PURQBinaryIndexedTree(sourceArray);

                for (int r = 0; r < 1000; ++r)
                {
                    int firstIndex = rand.Next(0, sourceArray.Length);
                    int secondIndex = rand.Next(0, sourceArray.Length);
                    int mode = rand.Next(2);

                    if (mode == 0)
                    {
                        NaiveBinaryIndexedTreeAlternatives.PointUpdate(sourceArray, firstIndex, delta: r);
                        purqBinaryIndexedTree.PointUpdate(firstIndex, delta: r);
                    }
                    else
                    {
                        int startIndex = Math.Min(firstIndex, secondIndex);
                        int endIndex = Math.Max(firstIndex, secondIndex);

                        var expected = NaiveBinaryIndexedTreeAlternatives.SumQuery(sourceArray, startIndex, endIndex);
                        Assert.AreEqual(expected, purqBinaryIndexedTree.SumQuery(startIndex, endIndex));
                    }
                }
            }
        }

        [TestMethod]
        public void VerifiesRUPQBinaryIndexedTree()
        {
            var rand = new Random();

            for (int a = 0; a < _sourceArrays.Length; ++a)
            {
                var sourceArray = _sourceArrays[a];
                var rupqBinaryIndexedTree = new RUPQBinaryIndexedTree(sourceArray);

                for (int r = 0; r < 1000; ++r)
                {
                    int firstIndex = rand.Next(0, sourceArray.Length);
                    int secondIndex = rand.Next(0, sourceArray.Length);
                    int startIndex = Math.Min(firstIndex, secondIndex);
                    int endIndex = Math.Max(firstIndex, secondIndex);
                    int mode = rand.Next(2);

                    if (mode == 0)
                    {
                        NaiveBinaryIndexedTreeAlternatives.RangeUpdate(sourceArray, startIndex, endIndex, delta: r);
                        rupqBinaryIndexedTree.RangeUpdate(startIndex, endIndex, delta: r);
                    }
                    else
                    {
                        var expected = NaiveBinaryIndexedTreeAlternatives.ValueQuery(sourceArray, firstIndex);
                        Assert.AreEqual(expected, rupqBinaryIndexedTree.ValueQuery(firstIndex));
                    }
                }
            }
        }

        [TestMethod]
        public void VerifiesRURQBinaryIndexedTree()
        {
            var rand = new Random();

            for (int a = 0; a < _sourceArrays.Length; ++a)
            {
                var sourceArray = _sourceArrays[a];
                var rurqBinaryIndexedTree = new RURQBinaryIndexedTree(sourceArray);

                for (int r = 0; r < 1000; ++r)
                {
                    int firstIndex = rand.Next(0, sourceArray.Length);
                    int secondIndex = rand.Next(0, sourceArray.Length);
                    int startIndex = Math.Min(firstIndex, secondIndex);
                    int endIndex = Math.Max(firstIndex, secondIndex);
                    int mode = rand.Next(2);

                    if (mode == 0)
                    {
                        NaiveBinaryIndexedTreeAlternatives.RangeUpdate(sourceArray, startIndex, endIndex, delta: r);
                        rurqBinaryIndexedTree.RangeUpdate(startIndex, endIndex, delta: r);
                    }
                    else
                    {
                        var expected = NaiveBinaryIndexedTreeAlternatives.SumQuery(sourceArray, startIndex, endIndex);
                        Assert.AreEqual(expected, rurqBinaryIndexedTree.SumQuery(startIndex, endIndex));
                    }
                }
            }
        }
    }
}
