using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Spoj.Library.Tests
{
    [TestClass]
    public class Mod10ExponentiatorTests
    {
        [TestMethod]
        public void VerifiesSmallMod10Exponentiation()
        {
            Assert.AreEqual(0, Mod10Exponentiator.Compute(0, 1));
            Assert.AreEqual(0, Mod10Exponentiator.Compute(0, 2));

            for (int b = 1; b <= 9; ++b)
            {
                for (int e = 0; e <= 9; ++e)
                {
                    Assert.AreEqual((int)Math.Pow(b, e) % 10, Mod10Exponentiator.Compute(b, e));
                }
            }
        }

        [TestMethod]
        public void VerifiesBigMod10Exponentiation()
        {
            Assert.AreEqual(0, Mod10Exponentiator.Compute(10, 23409823));
            Assert.AreEqual(1, Mod10Exponentiator.Compute(11, 234094829));
            Assert.AreEqual(8, Mod10Exponentiator.Compute(23242, 993919));
            Assert.AreEqual(3, Mod10Exponentiator.Compute(3, 1341));
            Assert.AreEqual(4, Mod10Exponentiator.Compute(1324314, 1341));
            Assert.AreEqual(5, Mod10Exponentiator.Compute(15345, 1340099));
            Assert.AreEqual(6, Mod10Exponentiator.Compute(611776, 2321));
            Assert.AreEqual(7, Mod10Exponentiator.Compute(62177, 9193));
            Assert.AreEqual(4, Mod10Exponentiator.Compute(698, 1002));
            Assert.AreEqual(9, Mod10Exponentiator.Compute(92349, 23409823));
        }
    }
}
