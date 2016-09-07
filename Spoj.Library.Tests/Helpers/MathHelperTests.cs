using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spoj.Library.Helpers;

namespace Spoj.Library.Tests.Helpers
{
    [TestClass]
    public class MathHelperTests
    {
        [TestMethod]
        public void IdentifiesPowersOfTwo()
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
        public void GetsFirstPowerOfTwoAtOrAfter()
        {
            Assert.AreEqual(1, MathHelper.GetFirstPowerOfTwoAtOrAfter(-1));
            Assert.AreEqual(1, MathHelper.GetFirstPowerOfTwoAtOrAfter(0));
            Assert.AreEqual(1, MathHelper.GetFirstPowerOfTwoAtOrAfter(1));
            Assert.AreEqual(2, MathHelper.GetFirstPowerOfTwoAtOrAfter(2));
            Assert.AreEqual(4, MathHelper.GetFirstPowerOfTwoAtOrAfter(3));
            Assert.AreEqual(4, MathHelper.GetFirstPowerOfTwoAtOrAfter(4));
            Assert.AreEqual(8, MathHelper.GetFirstPowerOfTwoAtOrAfter(5));
            Assert.AreEqual(16, MathHelper.GetFirstPowerOfTwoAtOrAfter(13));
            Assert.AreEqual(32, MathHelper.GetFirstPowerOfTwoAtOrAfter(25));
        }
    }
}
