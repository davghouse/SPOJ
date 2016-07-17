using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Spoj.Library.Tests
{
    [TestClass]
    public class InputGeneratorTests
    {
        [TestMethod]
        public void DoesntGenerateDistinctRandomInts()
        {
            int[] ints = InputGenerator.GenerateRandomInts(10000, 0, 9999);

            Assert.IsTrue(!ints.SequenceEqual(ints.Distinct()));
        }

        [TestMethod]
        public void GeneratesDistinctRandomInts()
        {
            int[] ints = InputGenerator.GenerateDistinctRandomInts(10000, 0, 10000);

            Assert.IsTrue(ints.SequenceEqual(ints.Distinct()));
        }
    }
}
