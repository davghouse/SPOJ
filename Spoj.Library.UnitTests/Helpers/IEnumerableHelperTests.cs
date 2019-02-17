using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spoj.Library.Helpers;
using System;
using System.Linq;

namespace Spoj.Library.UnitTests.Helpers
{
    [TestClass]
    public class IEnumerableHelperTests
    {
        [TestMethod]
        public void SetEqual()
        {
            Assert.IsTrue(new[] { 1, 2, 3 }.SetEqual(new[] { 3, 2, 1 }));
            Assert.IsTrue(new[] { 1, 2, 3 }.SetEqual(new[] { 1, 2, 2, 3, 3, 3 }));
            Assert.IsFalse(new[] { 1, 2, 3 }.SetEqual(new[] { 1, 2, 4 }));
        }

        [TestMethod]
        public void MergeSortedArrays()
        {
            CollectionAssert.AreEqual(new int[0],
                IEnumerableHelper.MergeSortedArrays(new int[0], new int[0]));

            CollectionAssert.AreEqual(new[] { 1 },
                IEnumerableHelper.MergeSortedArrays(new[] { 1 }, new int[0]));
            CollectionAssert.AreEqual(new[] { 1 },
                IEnumerableHelper.MergeSortedArrays(new int[0], new[] { 1 }));

            CollectionAssert.AreEqual(new[] { 1, 1 },
                IEnumerableHelper.MergeSortedArrays(new int[0], new[] { 1, 1 }));
            CollectionAssert.AreEqual(new[] { 1, 1, 1 },
                IEnumerableHelper.MergeSortedArrays(new[] { 1 }, new[] { 1, 1 }));

            CollectionAssert.AreEqual(new[] { 1, 1, 2, 2, 3, 3, 4, 4 },
                IEnumerableHelper.MergeSortedArrays(new[] { 1, 1, 2, 3, 4, 4 }, new[] { 2, 3 }));

            for (int i = 0; i < 100; ++i)
            {
                for (int j = 0; j < 100; ++j)
                {
                    var firstArray = InputGenerator.GenerateRandomInts(i);
                    var secondArray = InputGenerator.GenerateRandomInts(i);
                    Array.Sort(firstArray);
                    Array.Sort(secondArray);

                    CollectionAssert.AreEqual(firstArray.Concat(secondArray).OrderBy(v => v).ToArray(),
                        IEnumerableHelper.MergeSortedArrays(firstArray, secondArray));
                }
            }
        }

        [TestMethod]
        public void CountElementsBetween()
        {
            Assert.AreEqual(0, new int[0].CountElementsBetween(0, 10));
            Assert.AreEqual(0, new[] { -1 }.CountElementsBetween(0, 10));
            Assert.AreEqual(0, new[] { 11 }.CountElementsBetween(0, 10));
            Assert.AreEqual(1, new[] { 0 }.CountElementsBetween(0, 10));
            Assert.AreEqual(1, new[] { 10 }.CountElementsBetween(0, 10));
            Assert.AreEqual(2, new[] { -1, 0, 10 }.CountElementsBetween(0, 10));
            Assert.AreEqual(2, new[] { 9, 10, 11 }.CountElementsBetween(0, 10));
            Assert.AreEqual(1, new[] { 8, 9, 10 }.CountElementsBetween(10, 10));
            Assert.AreEqual(1, new[] { 8, 9, 10 }.CountElementsBetween(8, 8));
            Assert.AreEqual(1, new[] { 8, 9, 10 }.CountElementsBetween(9, 9));
            Assert.AreEqual(0, new[] { 8, 9, 10 }.CountElementsBetween(6, 7));
            Assert.AreEqual(1, new[] { 8, 9, 10 }.CountElementsBetween(6, 8));
            Assert.AreEqual(0, new[] { 8, 9, 10 }.CountElementsBetween(11, 12));
            Assert.AreEqual(1, new[] { 8, 9, 10 }.CountElementsBetween(10, 12));
            Assert.AreEqual(0, new[] { 7, 8, 90, 100 }.CountElementsBetween(10, 11));
            Assert.AreEqual(2, new[] { 7, 8, 90, 100 }.CountElementsBetween(10, 100));
            Assert.AreEqual(4, new[] { -7, 8, 90, 100 }.CountElementsBetween(-10, 100));
            Assert.AreEqual(2, new[] { -7, -6, 1, 2 }.CountElementsBetween(-7, -6));
            Assert.AreEqual(4, new[] { -7, -6, 1, 2 }.CountElementsBetween(-7, 2));
            Assert.AreEqual(5, new[] { -7, -6, 1, 2, 5 }.CountElementsBetween(-7, 6));

            for (int size = 0; size < 50; ++size)
            {
                int[] array = new int[size];
                for (int i = 0; i < size; ++i)
                {
                    array[i] = InputGenerator.Rand.Next(-100, 101);
                }
                array = array.Distinct().ToArray();
                Array.Sort(array);

                for (int lowerBound = -110; lowerBound <= 110; ++lowerBound)
                {
                    for (int upperBound = lowerBound; upperBound <= 110; ++upperBound)
                    {
                        int bruteForceCount = array.Count(v => v >= lowerBound && v <= upperBound);

                        Assert.AreEqual(bruteForceCount, array.CountElementsBetween(lowerBound, upperBound));
                    }
                }
            }
        }
    }
}
