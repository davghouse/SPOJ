using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Daves.SpojSpace.Library.UnitTests
{
    [TestClass]
    public sealed class BigIntegerTests
    {
        #region Addition

        [TestMethod]
        public void ZeroPlusZeroIsZero()
        {
            var a = new BigInteger(0);
            var b = new BigInteger("0");

            Assert.AreEqual(BigInteger.Zero, a + b);
        }

        [TestMethod]
        public void DoesntEqualItselfPlusOne()
        {
            var a = new BigInteger(117);

            Assert.AreNotEqual(a, a + BigInteger.One);
        }

        [TestMethod]
        public void AddsZero()
        {
            var a = new BigInteger(117);

            Assert.AreEqual(a, a + BigInteger.Zero);
            Assert.AreEqual(a, BigInteger.Zero + a);
            Assert.AreEqual(a, a + new BigInteger(0));
        }

        [TestMethod]
        public void AddsSomeIntegers()
        {
            var a = new BigInteger(117);
            var b = new BigInteger(245425);
            var c = new BigInteger(331);
            var abc = new BigInteger(117 + 245425 + 331);

            Assert.AreEqual(abc, a + b + c);
        }

        [TestMethod]
        public void AddsSomeBigIntegers()
        {
            var a = new BigInteger("2342452452435235098091834098018");
            var b = new BigInteger("102938209095450980895402985098560727234098123345");
            var c = new BigInteger("123087049626738452486565420250237462376208456824761033164013640476");
            var abc = new BigInteger("123087049626738452589503629345688445614063894358556858489945861839");

            Assert.AreEqual(abc, a + b + c);
        }

        [TestMethod]
        public void AdditionIsCommutative()
        {
            var a = new BigInteger("2342452452435235098091834098018");
            var b = new BigInteger("102938209095450980895402985098560727234098123345");

            Assert.AreEqual(a + b, b + a);
        }

        [TestMethod]
        public void AdditionIsAssociative()
        {
            var a = new BigInteger("2342452452435235098091834098018");
            var b = new BigInteger("102938209095450980895402985098560727234098123345");
            var c = new BigInteger(435);

            Assert.AreEqual(a + (b + c), (a + b) + c);
        }

        #endregion Addition

        #region Multiplication

        [TestMethod]
        public void IntegerTimesZeroIsZero()
        {
            var a = new BigInteger(234);

            Assert.AreEqual(BigInteger.Zero, a * BigInteger.Zero);
            Assert.AreEqual(BigInteger.Zero, BigInteger.Zero * a);
        }

        [TestMethod]
        public void BigIntegerTimesZeroIsZero()
        {
            var a = new BigInteger("2342342345483240982020324524545458");

            Assert.AreEqual(BigInteger.Zero, a * BigInteger.Zero);
            Assert.AreEqual(BigInteger.Zero, BigInteger.Zero * a);
        }

        [TestMethod]
        public void MultipliesByOne()
        {
            var a = new BigInteger("2342342345483240982020324524545458");

            Assert.AreEqual(a, a * BigInteger.One);
            Assert.AreEqual(a, BigInteger.One * a);
        }

        [TestMethod]
        public void MultipliesSomeIntegers()
        {
            var a = new BigInteger(117);
            var b = new BigInteger(24542);
            var c = new BigInteger(331);
            var abc = new BigInteger(117 * 24542 * 331);

            Assert.AreEqual(abc, a * b * c);
        }

        [TestMethod]
        public void MultipliesSomeBigIntegers()
        {
            var a = new BigInteger("35235098091834098018");
            var b = new BigInteger("402985098560727234098123345");
            var c = new BigInteger("65654202761033164013640476");
            var abc = new BigInteger("932238434613340857613513736796192283181771939784559822862931634062779960");

            Assert.AreEqual(abc, a * b * c);
        }

        [TestMethod]
        public void MultiplicationIsCommutative()
        {
            var a = new BigInteger("2342452452435235098091834098018");
            var b = new BigInteger("102938209095450980895402985098560727234098123345");

            Assert.AreEqual(a * b, b * a);
        }

        [TestMethod]
        public void MultiplicationIsAssociative()
        {
            var a = new BigInteger("2342452452435235098091834098018");
            var b = new BigInteger("102938209095450980895402985098560727234098123345");
            var c = new BigInteger(435);

            Assert.AreEqual(a * (b * c), (a * b) * c);
        }

        #endregion Multiplication

        #region Subtraction

        [TestMethod]
        public void ZeroMinusZeroIsZero()
        {
            var a = new BigInteger(0);
            var b = new BigInteger("0");

            Assert.AreEqual(BigInteger.Zero, a - b);
        }

        [TestMethod]
        public void DoesntEqualItselfMinusOne()
        {
            var a = new BigInteger(117);

            Assert.AreNotEqual(a, a - BigInteger.One);
        }

        [TestMethod]
        public void SubtractsZero()
        {
            var a = new BigInteger(117);

            Assert.AreEqual(a, a - BigInteger.Zero);
            Assert.AreEqual(a, a - new BigInteger(0));
        }

        [TestMethod]
        public void SubtractsSomeIntegers1()
        {
            var a = new BigInteger(245425);
            var b = new BigInteger(11171);
            var c = new BigInteger(333);
            var abc = new BigInteger(245425 - 11171 - 333);

            Assert.AreEqual(abc, a - b - c);
        }

        [TestMethod]
        public void SubtractsSomeIntegers2()
        {
            var a = new BigInteger(113);
            var b = new BigInteger(99);
            var ab = new BigInteger(113 - 99);

            Assert.AreEqual(ab, a - b);
        }

        [TestMethod]
        public void SubtractsSomeIntegers3()
        {
            var a = new BigInteger(13);
            var b = new BigInteger(4);
            var ab = new BigInteger(13 - 4);

            Assert.AreEqual(ab, a - b);
        }

        [TestMethod]
        public void SubtractsSomeBigIntegers()
        {
            var a = new BigInteger("123087049626738452486565420250237462376208456824761033164013640476");
            var b = new BigInteger("102938209095450980895402985098560727234098123345");
            var c = new BigInteger("2342452452435235098091834098018");
            var abc = new BigInteger("123087049626738452383627211154786479138353019290965207838081419113");

            Assert.AreEqual(abc, a - b - c);
        }

        [TestMethod]
        public void MinusItselfIsZero()
        {
            var a = new BigInteger("40476");

            Assert.AreEqual(BigInteger.Zero, a - a);
        }

        [TestMethod]
        public void MinusItselfPlusOneIsOne()
        {
            var a = new BigInteger("40476");

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
            var a = new BigInteger(2212);
            var b = new BigInteger(1106);
            var c = new BigInteger(553);

            Assert.AreEqual(b, a.DivideByTwo());
            Assert.AreEqual(c, b.DivideByTwo());
        }

        [TestMethod]
        public void DividesSomeBigIntegersByTwo()
        {
            var a = new BigInteger("32149817470458794114082");
            var b = new BigInteger("912837407413498340938495840576282048");

            Assert.AreEqual(new BigInteger("16074908735229397057041"), a.DivideByTwo());
            Assert.AreEqual(new BigInteger("456418703706749170469247920288141024"), b.DivideByTwo());
        }

        [TestMethod]
        public void DividesPowersOfTwoByTwo()
        {
            var a = new BigInteger(32);
            var b = new BigInteger(16);
            var c = new BigInteger(8);
            var d = new BigInteger(4);
            var e = new BigInteger(2);
            var f = new BigInteger(1);

            Assert.AreEqual(b, a.DivideByTwo());
            Assert.AreEqual(c, b.DivideByTwo());
            Assert.AreEqual(d, c.DivideByTwo());
            Assert.AreEqual(e, d.DivideByTwo());
            Assert.AreEqual(f, e.DivideByTwo());
        }

        #endregion Division by two
    }
}
