using Microsoft.VisualStudio.TestTools.UnitTesting;
using Daves.SpojSpace.Library.Helpers;

namespace Daves.SpojSpace.Library.UnitTests.Helpers
{
    [TestClass]
    public sealed class MathHelperTests
    {
        [TestMethod]
        public void IsPowerOfTwo()
        {
            Assert.IsFalse(MathHelper.IsPowerOfTwo(-1));
            Assert.IsFalse(MathHelper.IsPowerOfTwo(0));
            Assert.IsTrue(MathHelper.IsPowerOfTwo(1));
            Assert.IsTrue(MathHelper.IsPowerOfTwo(2));
            Assert.IsFalse(MathHelper.IsPowerOfTwo(3));
            Assert.IsTrue(MathHelper.IsPowerOfTwo(4));
            Assert.IsFalse(MathHelper.IsPowerOfTwo(5));
            Assert.IsTrue(MathHelper.IsPowerOfTwo(8));
            Assert.IsFalse(MathHelper.IsPowerOfTwo(9));
            Assert.IsTrue(MathHelper.IsPowerOfTwo(256));
            Assert.IsFalse(MathHelper.IsPowerOfTwo(260));
        }

        [TestMethod]
        public void FirstPowerOfTwoEqualOrGreater()
        {
            // Not exactly accurate. Really first integral power of two (non-negative exponent), but kind of implied.
            Assert.AreEqual(1, MathHelper.FirstPowerOfTwoEqualOrGreater(-1));
            Assert.AreEqual(1, MathHelper.FirstPowerOfTwoEqualOrGreater(0));

            Assert.AreEqual(1, MathHelper.FirstPowerOfTwoEqualOrGreater(1));
            Assert.AreEqual(2, MathHelper.FirstPowerOfTwoEqualOrGreater(2));
            Assert.AreEqual(4, MathHelper.FirstPowerOfTwoEqualOrGreater(3));
            Assert.AreEqual(4, MathHelper.FirstPowerOfTwoEqualOrGreater(4));
            Assert.AreEqual(8, MathHelper.FirstPowerOfTwoEqualOrGreater(5));
            Assert.AreEqual(8, MathHelper.FirstPowerOfTwoEqualOrGreater(6));
            Assert.AreEqual(8, MathHelper.FirstPowerOfTwoEqualOrGreater(7));
            Assert.AreEqual(8, MathHelper.FirstPowerOfTwoEqualOrGreater(8));
            Assert.AreEqual(16, MathHelper.FirstPowerOfTwoEqualOrGreater(9));
            Assert.AreEqual(16, MathHelper.FirstPowerOfTwoEqualOrGreater(13));
            Assert.AreEqual(32, MathHelper.FirstPowerOfTwoEqualOrGreater(25));
            Assert.AreEqual(32, MathHelper.FirstPowerOfTwoEqualOrGreater(32));
        }

        [TestMethod]
        public void GreatestPowerOfTwoEqualOrLess()
        {
            Assert.AreEqual(1, MathHelper.GreatestPowerOfTwoEqualOrLess(1));
            Assert.AreEqual(2, MathHelper.GreatestPowerOfTwoEqualOrLess(2));
            Assert.AreEqual(2, MathHelper.GreatestPowerOfTwoEqualOrLess(3));
            Assert.AreEqual(4, MathHelper.GreatestPowerOfTwoEqualOrLess(4));
            Assert.AreEqual(4, MathHelper.GreatestPowerOfTwoEqualOrLess(5));
            Assert.AreEqual(4, MathHelper.GreatestPowerOfTwoEqualOrLess(6));
            Assert.AreEqual(4, MathHelper.GreatestPowerOfTwoEqualOrLess(7));
            Assert.AreEqual(8, MathHelper.GreatestPowerOfTwoEqualOrLess(8));
            Assert.AreEqual(8, MathHelper.GreatestPowerOfTwoEqualOrLess(9));
            Assert.AreEqual(16, MathHelper.GreatestPowerOfTwoEqualOrLess(19));
            Assert.AreEqual(16, MathHelper.GreatestPowerOfTwoEqualOrLess(31));
            Assert.AreEqual(32, MathHelper.GreatestPowerOfTwoEqualOrLess(33));
        }

        [TestMethod]
        public void NumberOfCombinations()
        {
            Assert.AreEqual(1, MathHelper.NumberOfCombinations(1, 0));
            Assert.AreEqual(1, MathHelper.NumberOfCombinations(1, 1));

            Assert.AreEqual(1, MathHelper.NumberOfCombinations(2, 0));
            Assert.AreEqual(2, MathHelper.NumberOfCombinations(2, 1));
            Assert.AreEqual(1, MathHelper.NumberOfCombinations(2, 2));

            Assert.AreEqual(1, MathHelper.NumberOfCombinations(3, 0));
            Assert.AreEqual(3, MathHelper.NumberOfCombinations(3, 1));
            Assert.AreEqual(3, MathHelper.NumberOfCombinations(3, 2));
            Assert.AreEqual(1, MathHelper.NumberOfCombinations(3, 3));

            Assert.AreEqual(1, MathHelper.NumberOfCombinations(4, 0));
            Assert.AreEqual(4, MathHelper.NumberOfCombinations(4, 1));
            Assert.AreEqual(6, MathHelper.NumberOfCombinations(4, 2));
            Assert.AreEqual(4, MathHelper.NumberOfCombinations(4, 3));
            Assert.AreEqual(1, MathHelper.NumberOfCombinations(4, 4));

            Assert.AreEqual(1, MathHelper.NumberOfCombinations(4, 0));
            Assert.AreEqual(4, MathHelper.NumberOfCombinations(4, 1));
            Assert.AreEqual(6, MathHelper.NumberOfCombinations(4, 2));
            Assert.AreEqual(4, MathHelper.NumberOfCombinations(4, 3));
            Assert.AreEqual(1, MathHelper.NumberOfCombinations(4, 4));

            Assert.AreEqual(1, MathHelper.NumberOfCombinations(5, 0));
            Assert.AreEqual(5, MathHelper.NumberOfCombinations(5, 1));
            Assert.AreEqual(10, MathHelper.NumberOfCombinations(5, 2));
            Assert.AreEqual(10, MathHelper.NumberOfCombinations(5, 3));
            Assert.AreEqual(5, MathHelper.NumberOfCombinations(5, 4));
            Assert.AreEqual(1, MathHelper.NumberOfCombinations(5, 5));

            Assert.AreEqual(6188, MathHelper.NumberOfCombinations(17, 5));
            Assert.AreEqual(11716640, MathHelper.NumberOfCombinations(131, 4));
            Assert.AreEqual(20160075, MathHelper.NumberOfCombinations(31, 9));
        }

        [TestMethod]
        public void GreatestCommonDivisor()
        {
            Assert.AreEqual(0, MathHelper.GreatestCommonDivisor(0, 0));
            Assert.AreEqual(1, MathHelper.GreatestCommonDivisor(1, 0));
            Assert.AreEqual(1, MathHelper.GreatestCommonDivisor(0, 1));
            Assert.AreEqual(2, MathHelper.GreatestCommonDivisor(2, 0));
            Assert.AreEqual(2, MathHelper.GreatestCommonDivisor(0, 2));
            Assert.AreEqual(1, MathHelper.GreatestCommonDivisor(1, 1));
            Assert.AreEqual(1, MathHelper.GreatestCommonDivisor(1, 2));
            Assert.AreEqual(1, MathHelper.GreatestCommonDivisor(2, 1));
            Assert.AreEqual(5, MathHelper.GreatestCommonDivisor(25, 15));
            Assert.AreEqual(5, MathHelper.GreatestCommonDivisor(15, 25));
            Assert.AreEqual(105, MathHelper.GreatestCommonDivisor(10290, 945));
            Assert.AreEqual(105, MathHelper.GreatestCommonDivisor(945, 10290));
            Assert.AreEqual(66, MathHelper.GreatestCommonDivisor(7262574, 210763872));
            Assert.AreEqual(66, MathHelper.GreatestCommonDivisor(210763872, 7262574));
            Assert.AreEqual(1, MathHelper.GreatestCommonDivisor(17, 19));
        }
    }
}
