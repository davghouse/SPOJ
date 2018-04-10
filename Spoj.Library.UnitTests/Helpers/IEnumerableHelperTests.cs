using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spoj.Library.Helpers;

namespace Spoj.Library.UnitTests.Helpers
{
    [TestClass]
    public class IEnumerableHelperTests
    {
        [TestMethod]
        public void SetEqual()
        {
            Assert.IsTrue(new int[] { 1, 2, 3 }.SetEqual(new int[] { 3, 2, 1 }));
            Assert.IsTrue(new int[] { 1, 2, 3 }.SetEqual(new int[] { 1, 2, 2, 3, 3, 3 }));
            Assert.IsFalse(new int[] { 1, 2, 3 }.SetEqual(new int[] { 1, 2, 4 }));
        }
    }
}
