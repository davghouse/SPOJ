using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Spoj.Library.UnitTests
{
    [TestClass]
    public class InputGeneratorTests
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
                Assert.IsTrue(evenOddPairs[p, 0] % 2 == 0);
                Assert.IsTrue(evenOddPairs[p, 1] % 2 == 1);
            }

            evenOddPairs = InputGenerator.GenerateRandomEvenOddPairs(1000, 0, 1);
            Assert.AreEqual(1000, evenOddPairs.GetLength(0));
            for (int p = 0; p < 1000; ++p)
            {
                Assert.AreEqual(0, evenOddPairs[p, 0]);
                Assert.AreEqual(1, evenOddPairs[p, 1]);
            }
        }
    }
}
