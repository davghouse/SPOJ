using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Spoj.Library.Tests
{
    [TestClass]
    public class BigIntegerTests
    {
        #region Addition

        [TestMethod]
        public void ZeroPlusZeroIsZero()
        {
            BigInteger a = new BigInteger(0);
            BigInteger b = new BigInteger("0");

            Assert.AreEqual(BigInteger.Zero, a + b);
        }

        [TestMethod]
        public void DoesntEqualItselfPlusOne()
        {
            BigInteger a = new BigInteger(117);

            Assert.AreNotEqual(a, a + BigInteger.One);
        }

        [TestMethod]
        public void AddsZero()
        {
            BigInteger a = new BigInteger(117);

            Assert.AreEqual(a, a + BigInteger.Zero);
            Assert.AreEqual(a, BigInteger.Zero + a);
            Assert.AreEqual(a, a + new BigInteger(0));
        }

        [TestMethod]
        public void AddsSomeIntegers()
        {
            BigInteger a = new BigInteger(117);
            BigInteger b = new BigInteger(245425);
            BigInteger c = new BigInteger(331);
            BigInteger abc = new BigInteger(117 + 245425 + 331);

            Assert.AreEqual(abc, a + b + c);
        }

        [TestMethod]
        public void AddsSomeBigIntegers()
        {
            BigInteger a = new BigInteger("2342452452435235098091834098018");
            BigInteger b = new BigInteger("102938209095450980895402985098560727234098123345");
            BigInteger c = new BigInteger("123087049626738452486565420250237462376208456824761033164013640476");
            BigInteger abc = new BigInteger("123087049626738452589503629345688445614063894358556858489945861839");

            Assert.AreEqual(abc, a + b + c);
        }

        [TestMethod]
        public void AdditionIsCommutative()
        {
            BigInteger a = new BigInteger("2342452452435235098091834098018");
            BigInteger b = new BigInteger("102938209095450980895402985098560727234098123345");

            Assert.AreEqual(a + b, b + a);
        }

        [TestMethod]
        public void AdditionIsAssociative()
        {
            BigInteger a = new BigInteger("2342452452435235098091834098018");
            BigInteger b = new BigInteger("102938209095450980895402985098560727234098123345");
            BigInteger c = new BigInteger(435);

            Assert.AreEqual(a + (b + c), (a + b) + c);
        }

        #endregion Addition

        #region Multiplication

        [TestMethod]
        public void IntegerTimesZeroIsZero()
        {
            BigInteger a = new BigInteger(234);

            Assert.AreEqual(BigInteger.Zero, a * BigInteger.Zero);
            Assert.AreEqual(BigInteger.Zero, BigInteger.Zero * a);
        }

        [TestMethod]
        public void BigIntegerTimesZeroIsZero()
        {
            BigInteger a = new BigInteger("2342342345483240982020324524545458");

            Assert.AreEqual(BigInteger.Zero, a * BigInteger.Zero);
            Assert.AreEqual(BigInteger.Zero, BigInteger.Zero * a);
        }

        [TestMethod]
        public void MultipliesByOne()
        {
            BigInteger a = new BigInteger("2342342345483240982020324524545458");

            Assert.AreEqual(a, a * BigInteger.One);
            Assert.AreEqual(a, BigInteger.One * a);
        }

        [TestMethod]
        public void MultipliesSomeIntegers()
        {
            BigInteger a = new BigInteger(117);
            BigInteger b = new BigInteger(24542);
            BigInteger c = new BigInteger(331);
            BigInteger abc = new BigInteger(117 * 24542 * 331);

            Assert.AreEqual(abc, a * b * c);
        }

        [TestMethod]
        public void MultipliesSomeBigIntegers()
        {
            BigInteger a = new BigInteger("35235098091834098018");
            BigInteger b = new BigInteger("402985098560727234098123345");
            BigInteger c = new BigInteger("65654202761033164013640476");
            BigInteger abc = new BigInteger("932238434613340857613513736796192283181771939784559822862931634062779960");

            Assert.AreEqual(abc, a * b * c);
        }

        [TestMethod]
        public void MultiplicationIsCommutative()
        {
            BigInteger a = new BigInteger("2342452452435235098091834098018");
            BigInteger b = new BigInteger("102938209095450980895402985098560727234098123345");

            Assert.AreEqual(a * b, b * a);
        }

        [TestMethod]
        public void MultiplicationIsAssociative()
        {
            BigInteger a = new BigInteger("2342452452435235098091834098018");
            BigInteger b = new BigInteger("102938209095450980895402985098560727234098123345");
            BigInteger c = new BigInteger(435);

            Assert.AreEqual(a * (b * c), (a * b) * c);
        }

        #endregion Multiplication

        #region Subtraction

        [TestMethod]
        public void ZeroMinusZeroIsZero()
        {
            BigInteger a = new BigInteger(0);
            BigInteger b = new BigInteger("0");

            Assert.AreEqual(BigInteger.Zero, a - b);
        }

        [TestMethod]
        public void DoesntEqualItselfMinusOne()
        {
            BigInteger a = new BigInteger(117);

            Assert.AreNotEqual(a, a - BigInteger.One);
        }

        [TestMethod]
        public void SubtractsZero()
        {
            BigInteger a = new BigInteger(117);

            Assert.AreEqual(a, a - BigInteger.Zero);
            Assert.AreEqual(a, a - new BigInteger(0));
        }

        [TestMethod]
        public void SubtractsSomeIntegers1()
        {
            BigInteger a = new BigInteger(245425);
            BigInteger b = new BigInteger(11171);
            BigInteger c = new BigInteger(333);
            BigInteger abc = new BigInteger(245425 - 11171 - 333);

            Assert.AreEqual(abc, a - b - c);
        }

        [TestMethod]
        public void SubtractsSomeIntegers2()
        {
            BigInteger a = new BigInteger(113);
            BigInteger b = new BigInteger(99);
            BigInteger ab = new BigInteger(113 - 99);

            Assert.AreEqual(ab, a - b);
        }

        [TestMethod]
        public void SubtractsSomeIntegers3()
        {
            BigInteger a = new BigInteger(13);
            BigInteger b = new BigInteger(4);
            BigInteger ab = new BigInteger(13 - 4);

            Assert.AreEqual(ab, a - b);
        }

        [TestMethod]
        public void SubtractsSomeBigIntegers()
        {
            BigInteger a = new BigInteger("123087049626738452486565420250237462376208456824761033164013640476");
            BigInteger b = new BigInteger("102938209095450980895402985098560727234098123345");
            BigInteger c = new BigInteger("2342452452435235098091834098018");
            BigInteger abc = new BigInteger("123087049626738452383627211154786479138353019290965207838081419113");

            Assert.AreEqual(abc, a - b - c);
        }

        [TestMethod]
        public void MinusItselfIsZero()
        {
            BigInteger a = new BigInteger("40476");

            Assert.AreEqual(BigInteger.Zero, a - a);
        }

        [TestMethod]
        public void MinusItselfPlusOneIsOne()
        {
            BigInteger a = new BigInteger("40476");

            Assert.AreEqual(BigInteger.One, a - a + BigInteger.One);
        }

        #endregion Subtraction

        #region Division by two

        [TestMethod]
        public void DividesZeroByTwo()
        {
            Assert.AreEqual(BigInteger.Zero, BigInteger.Zero.DivideByTwo());
        }

        [TestMethod]
        public void DividesTwoByTwo()
        {
            Assert.AreEqual(BigInteger.One, (new BigInteger(2)).DivideByTwo());
        }

        [TestMethod]
        public void DividesSomeIntegersByTwo()
        {
            BigInteger a = new BigInteger(2212);
            BigInteger b = new BigInteger(1106);
            BigInteger c = new BigInteger(553);

            Assert.AreEqual(b, a.DivideByTwo());
            Assert.AreEqual(c, b.DivideByTwo());
        }

        [TestMethod]
        public void DividesSomeBigIntegersByTwo()
        {
            BigInteger a = new BigInteger("32149817470458794114082");
            BigInteger b = new BigInteger("912837407413498340938495840576282048");

            Assert.AreEqual(new BigInteger("16074908735229397057041"), a.DivideByTwo());
            Assert.AreEqual(new BigInteger("456418703706749170469247920288141024"), b.DivideByTwo());
        }

        [TestMethod]
        public void DividesPowersOfTwoByTwo()
        {
            BigInteger a = new BigInteger(32);
            BigInteger b = new BigInteger(16);
            BigInteger c = new BigInteger(8);
            BigInteger d = new BigInteger(4);
            BigInteger e = new BigInteger(2);
            BigInteger f = new BigInteger(1);

            Assert.AreEqual(b, a.DivideByTwo());
            Assert.AreEqual(c, b.DivideByTwo());
            Assert.AreEqual(d, c.DivideByTwo());
            Assert.AreEqual(e, d.DivideByTwo());
            Assert.AreEqual(f, e.DivideByTwo());
        }

        #endregion Division by two
    }
}
