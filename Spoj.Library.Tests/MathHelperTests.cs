using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Spoj.Library.Tests
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
    }
}
