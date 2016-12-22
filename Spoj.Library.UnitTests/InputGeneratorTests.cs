using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Spoj.Library.UnitTests
{
    [TestClass]
    public sealed class InputGeneratorTests
    {
        [TestMethod]
        public void DoesntGenerateDistinctRandomInts()
        {
            int[] ints = InputGenerator.GenerateRandomInts(1000, 1, 999);

            Assert.IsTrue(!ints.SequenceEqual(ints.Distinct()));
        }

        [TestMethod]
        public void GeneratesDistinctRandomInts()
        {
            int[] ints = InputGenerator.GenerateDistinctRandomInts(1000, 1, 1000);

            Assert.IsTrue(ints.SequenceEqual(ints.Distinct()));
        }

        [TestMethod]
        public void GeneratesRandomStrings()
        {
            string s = InputGenerator.GenerateRandomString(1000);

            Assert.AreEqual(1000, s.Length);
            Assert.IsTrue(s.All(c => c >= 'a'));
            Assert.IsTrue(s.All(c => c <= 'z'));

            s = InputGenerator.GenerateRandomString(1000, 'a', 'b');

            Assert.AreEqual(1000, s.Length);
            Assert.IsTrue(s.All(c => c >= 'a'));
            Assert.IsTrue(s.All(c => c <= 'b'));
            Assert.IsTrue(s.Any(c => c == 'a'));
            Assert.IsTrue(s.Any(c => c == 'b'));
        }

        [TestMethod]
        public void GeneratesEvenOddPairs()
        {
            int[,] evenOddPairs = InputGenerator.GenerateRandomEvenOddPairs(1000, 1, 100);

            Assert.AreEqual(1000, evenOddPairs.GetLength(0));
            for (int p = 0; p < 1000; ++p)
            {
                int even = evenOddPairs[p, 0];
                int odd = evenOddPairs[p, 1];

                Assert.IsTrue(even % 2 == 0);
                Assert.IsTrue(odd % 2 == 1);
                Assert.IsTrue(even >= 1 && even <= 1000);
                Assert.IsTrue(odd >= 1 && odd <= 1000);
            }

            evenOddPairs = InputGenerator.GenerateRandomEvenOddPairs(1000, 0, 1);

            Assert.AreEqual(1000, evenOddPairs.GetLength(0));
            for (int p = 0; p < 1000; ++p)
            {
                int even = evenOddPairs[p, 0];
                int odd = evenOddPairs[p, 1];

                Assert.AreEqual(0, even);
                Assert.AreEqual(1, odd);
            }
        }

        [TestMethod]
        public void GeneratesMinMaxPairs()
        {
            int[,] minMaxPairs = InputGenerator.GenerateRandomMinMaxPairs(1000, 1, 1000);

            Assert.AreEqual(1000, minMaxPairs.GetLength(0));
            for (int p = 0; p < 1000; ++p)
            {
                int min = minMaxPairs[p, 0];
                int max = minMaxPairs[p, 1];

                Assert.IsTrue(min <= max);
                Assert.IsTrue(min >= 1 && min <= 1000);
                Assert.IsTrue(max >= 1 && max <= 1000);
            }

            minMaxPairs = InputGenerator.GenerateRandomMinMaxPairs(1000, 0, 1);

            Assert.AreEqual(1000, minMaxPairs.GetLength(0));
            for (int p = 0; p < 1000; ++p)
            {
                int min = minMaxPairs[p, 0];
                int max = minMaxPairs[p, 1];

                Assert.IsTrue(min <= max);
                Assert.IsTrue(min >= 0 && min <= 1);
                Assert.IsTrue(max >= 0 && max <= 1);
            }
        }
    }
}
