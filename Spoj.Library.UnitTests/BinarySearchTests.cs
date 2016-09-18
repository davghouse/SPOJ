using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Spoj.Library.UnitTests
{
    [TestClass]
    public class BinarySearchTests
    {
        [TestMethod]
        public void Searches_FalseToTrue_Unsuccessfully()
        {
            var mode = BinarySearch.Mode.FalseToTrue;
            int[] arr;

            // If x > 3, y > 3 for y >= x. If !(x > 3), !(y > 3) for y <= x.
            arr = new int[0];              Assert.AreEqual(null, BinarySearch.Search(arr, x => x > 3, mode));
            arr = new[] { 0 };             Assert.AreEqual(null, BinarySearch.Search(arr, x => x > 3, mode));
            arr = new[] { 0, 0 };          Assert.AreEqual(null, BinarySearch.Search(arr, x => x > 3, mode));
            arr = new[] { 3 };             Assert.AreEqual(null, BinarySearch.Search(arr, x => x > 3, mode));
            arr = new[] { 3, 3 };          Assert.AreEqual(null, BinarySearch.Search(arr, x => x > 3, mode));
            arr = new[] { 0, 1, 3 };       Assert.AreEqual(null, BinarySearch.Search(arr, x => x > 3, mode));
            arr = new[] { 1, 2, 2 };       Assert.AreEqual(null, BinarySearch.Search(arr, x => x > 3, mode));
            arr = new[] { 0, 1, 2, 3, 3 }; Assert.AreEqual(null, BinarySearch.Search(arr, x => x > 3, mode));
            arr = new[] { 2, 2, 2, 3 };    Assert.AreEqual(null, BinarySearch.Search(arr, x => x > 3, mode));
            arr = new[] { 0, 0, 1, 1, 3 }; Assert.AreEqual(null, BinarySearch.Search(arr, x => x > 3, mode));
            arr = new[] { 3, 3, 3, 3, 3 }; Assert.AreEqual(null, BinarySearch.Search(arr, x => x > 3, mode));

            arr = new[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 2, 2, 2, 2, 2, 3, 3 };
            Assert.AreEqual(null, BinarySearch.Search(arr, x => x > 3, mode));

            // Just looking at truth values; the array doesn't need to be ordered beyond the FalseToTrue implication.
            arr = new[] { 3, 2, 1, 0, 3, 2, 1, 1, 1, 3, 0, 2, 3, 3, 2, 0, 1, 1, 0, 3, 2, 1, 0, 2 };
            Assert.AreEqual(null, BinarySearch.Search(arr, x => x > 3, mode));
        }

        [TestMethod]
        public void Searches_TrueToFalse_Unsuccessfully()
        {
            var mode = BinarySearch.Mode.TrueToFalse;
            int[] arr;

            // If x < 0, y < 0 for y <= x. If !(x < 0), !(y < 0) for y >= x.
            arr = new int[0];              Assert.AreEqual(null, BinarySearch.Search(arr, x => x < 0, mode));
            arr = new[] { 0 };             Assert.AreEqual(null, BinarySearch.Search(arr, x => x < 0, mode));
            arr = new[] { 0, 0 };          Assert.AreEqual(null, BinarySearch.Search(arr, x => x < 0, mode));
            arr = new[] { 3 };             Assert.AreEqual(null, BinarySearch.Search(arr, x => x < 0, mode));
            arr = new[] { 3, 3 };          Assert.AreEqual(null, BinarySearch.Search(arr, x => x < 0, mode));
            arr = new[] { 0, 1, 3 };       Assert.AreEqual(null, BinarySearch.Search(arr, x => x < 0, mode));
            arr = new[] { 1, 2, 2 };       Assert.AreEqual(null, BinarySearch.Search(arr, x => x < 0, mode));
            arr = new[] { 0, 1, 2, 3, 3 }; Assert.AreEqual(null, BinarySearch.Search(arr, x => x < 0, mode));
            arr = new[] { 2, 2, 2, 3 };    Assert.AreEqual(null, BinarySearch.Search(arr, x => x < 0, mode));
            arr = new[] { 0, 0, 1, 1, 3 }; Assert.AreEqual(null, BinarySearch.Search(arr, x => x < 0, mode));
            arr = new[] { 3, 3, 3, 3, 3 }; Assert.AreEqual(null, BinarySearch.Search(arr, x => x < 0, mode));

            arr = new[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 2, 2, 2, 2, 2, 3, 3 };
            Assert.AreEqual(null, BinarySearch.Search(arr, x => x < 0, mode));

            // Just looking at truth values; the array doesn't need to be ordered beyond the TrueToFalse implication.
            arr = new[] { 3, 2, 1, 0, 3, 2, 1, 1, 1, 3, 0, 2, 3, 3, 2, 0, 1, 1, 0, 3, 2, 1, 0, 2 };
            Assert.AreEqual(null, BinarySearch.Search(arr, x => x < 0, mode));
        }

        [TestMethod]
        public void Searches_FalseToTrue_Successfully()
        {
            var mode = BinarySearch.Mode.FalseToTrue;
            int[] arr;

            // If x > 3, y > 3 for y >= x. If !(x > 3), !(y > 3) for y <= x.
            arr = new[] { 4 };                Assert.AreEqual(0, BinarySearch.Search(arr, x => x > 3, mode));
            arr = new[] { 0, 4 };             Assert.AreEqual(1, BinarySearch.Search(arr, x => x > 3, mode));
            arr = new[] { 4, 4 };             Assert.AreEqual(0, BinarySearch.Search(arr, x => x > 3, mode));
            arr = new[] { 3, 3, 4 };          Assert.AreEqual(2, BinarySearch.Search(arr, x => x > 3, mode));
            arr = new[] { 4, 4, 4 };          Assert.AreEqual(0, BinarySearch.Search(arr, x => x > 3, mode));
            arr = new[] { 0, 1, 3, 4 };       Assert.AreEqual(3, BinarySearch.Search(arr, x => x > 3, mode));
            arr = new[] { 1, 2, 2, 4, 5, 6 }; Assert.AreEqual(3, BinarySearch.Search(arr, x => x > 3, mode));
            arr = new[] { 0, 1, 2, 3, 4, 6 }; Assert.AreEqual(4, BinarySearch.Search(arr, x => x > 3, mode));
            arr = new[] { 0, 0, 4, 7 };       Assert.AreEqual(2, BinarySearch.Search(arr, x => x > 3, mode));
            arr = new[] { 0, 0, 4, 5, 6 };    Assert.AreEqual(2, BinarySearch.Search(arr, x => x > 3, mode));
            arr = new[] { 4, 4, 4, 4, 4 };    Assert.AreEqual(0, BinarySearch.Search(arr, x => x > 3, mode));
            arr = new[] { 4, 5, 6, 7, 8, 9 }; Assert.AreEqual(0, BinarySearch.Search(arr, x => x > 3, mode));

            arr = new[] { 0, 0, 0, 1, 2, 2, 2, 2, 3, 3, 3, 4, 4, 4, 4, 5, 6, 7, 7, 8, 9, 9, 9, 9 };
            Assert.AreEqual(11, BinarySearch.Search(arr, x => x > 3, mode));

            // Just looking at truth values; the array doesn't need to be ordered beyond the FalseToTrue implication.
            arr = new[] { 0, 1, 0, 3, 2, 1, 0, 2, 2, 0, 3, 4, 4, 4, 9, 4, 4, 8, 7, 6, 5, 5, 4, 4 };
            Assert.AreEqual(11, BinarySearch.Search(arr, x => x > 3, mode));
        }

        [TestMethod]
        public void Searches_TrueToFalse_Successfully()
        {
            var mode = BinarySearch.Mode.TrueToFalse;
            int[] arr;

            // If x < 4, y < 4 for y <= x. If !(x < 4), !(y < 4) for y >= x.
            arr = new[] { 3 };                   Assert.AreEqual(0, BinarySearch.Search(arr, x => x < 4, mode));
            arr = new[] { 0, 3 };                Assert.AreEqual(1, BinarySearch.Search(arr, x => x < 4, mode));
            arr = new[] { 3, 4 };                Assert.AreEqual(0, BinarySearch.Search(arr, x => x < 4, mode));
            arr = new[] { 3, 3, 4 };             Assert.AreEqual(1, BinarySearch.Search(arr, x => x < 4, mode));
            arr = new[] { 3, 4, 4, 4 };          Assert.AreEqual(0, BinarySearch.Search(arr, x => x < 4, mode));
            arr = new[] { 0, 1, 3, 4 };          Assert.AreEqual(2, BinarySearch.Search(arr, x => x < 4, mode));
            arr = new[] { 1, 2, 2, 4, 5, 6 };    Assert.AreEqual(2, BinarySearch.Search(arr, x => x < 4, mode));
            arr = new[] { 0, 1, 2, 3, 4, 6 };    Assert.AreEqual(3, BinarySearch.Search(arr, x => x < 4, mode));
            arr = new[] { 0, 0, 4, 7 };          Assert.AreEqual(1, BinarySearch.Search(arr, x => x < 4, mode));
            arr = new[] { 0, 0, 4, 5, 6 };       Assert.AreEqual(1, BinarySearch.Search(arr, x => x < 4, mode));
            arr = new[] { 3, 3, 3, 3, 3 };       Assert.AreEqual(4, BinarySearch.Search(arr, x => x < 4, mode));
            arr = new[] { 3, 4, 5, 6, 7, 8, 9 }; Assert.AreEqual(0, BinarySearch.Search(arr, x => x < 4, mode));

            arr = new[] { 0, 0, 0, 1, 2, 2, 2, 2, 3, 3, 3, 4, 4, 4, 4, 5, 6, 7, 7, 8, 9, 9, 9, 9 };
            Assert.AreEqual(10, BinarySearch.Search(arr, x => x < 4, mode));

            // Just looking at truth values; the array doesn't need to be ordered beyond the TrueToFalse implication.
            arr = new[] { 0, 1, 0, 3, 2, 1, 0, 2, 2, 0, 3, 4, 4, 4, 9, 4, 4, 8, 7, 6, 5, 5, 4, 4 };
            Assert.AreEqual(10, BinarySearch.Search(arr, x => x < 4, mode));
        }
    }
}
