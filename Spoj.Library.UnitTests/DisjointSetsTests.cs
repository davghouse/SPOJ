using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Spoj.Library.UnitTests
{
    [TestClass]
    public class DisjointSetsTests
    {
        [TestMethod]
        public void UnionSets1()
        {
            var disjointSets = new DisjointSets(1);
            Assert.AreEqual(1, disjointSets.ElementCount);
            Assert.AreEqual(1, disjointSets.DisjointSetCount);

            disjointSets.UnionSets(0, 0);
            Assert.AreEqual(1, disjointSets.ElementCount);
            Assert.AreEqual(1, disjointSets.DisjointSetCount);
        }

        [TestMethod]
        public void UnionSets2()
        {
            var disjointSets = new DisjointSets(5);
            Assert.AreEqual(5, disjointSets.ElementCount);
            Assert.AreEqual(5, disjointSets.DisjointSetCount);

            disjointSets.UnionSets(0, 4);
            Assert.AreEqual(5, disjointSets.ElementCount);
            Assert.AreEqual(4, disjointSets.DisjointSetCount);

            disjointSets.UnionSets(4, 0);
            Assert.AreEqual(4, disjointSets.DisjointSetCount);

            disjointSets.UnionSets(3, 0);
            Assert.AreEqual(3, disjointSets.DisjointSetCount);

            disjointSets.UnionSets(1, 2);
            Assert.AreEqual(2, disjointSets.DisjointSetCount);

            disjointSets.UnionSets(2, 4);
            Assert.AreEqual(1, disjointSets.DisjointSetCount);

            disjointSets.UnionSets(1, 3);
            Assert.AreEqual(1, disjointSets.DisjointSetCount);

            disjointSets.UnionSets(4, 4);
            Assert.AreEqual(1, disjointSets.DisjointSetCount);

            disjointSets.UnionSets(2, 0);
            Assert.AreEqual(1, disjointSets.DisjointSetCount);
        }

        [TestMethod]
        public void AreInSameSet()
        {
            var disjointSets = new DisjointSets(10);
            for (int i = 0; i < 10; ++i)
            {
                for (int j = 0; j < 10; ++j)
                {
                    if (i != j)
                    {
                        Assert.IsFalse(disjointSets.AreInSameSet(i, j));
                    }
                    else
                    {
                        Assert.IsTrue(disjointSets.AreInSameSet(i, j));
                    }
                }
            }

            disjointSets.UnionSets(0, 3);
            Assert.IsTrue(disjointSets.AreInSameSet(0, 3));
            Assert.IsFalse(disjointSets.AreInSameSet(0, 4));

            disjointSets.UnionSets(1, 4);
            Assert.IsTrue(disjointSets.AreInSameSet(0, 3));
            Assert.IsFalse(disjointSets.AreInSameSet(0, 4));
            Assert.IsTrue(disjointSets.AreInSameSet(1, 4));
            Assert.IsTrue(disjointSets.AreInSameSet(4, 1));

            disjointSets.UnionSets(3, 1);
            Assert.IsTrue(disjointSets.AreInSameSet(0, 4));
            Assert.IsTrue(disjointSets.AreInSameSet(1, 3));
            Assert.IsTrue(disjointSets.AreInSameSet(5, 5));
            Assert.IsFalse(disjointSets.AreInSameSet(1, 5));

            disjointSets.UnionSets(2, 5);
            disjointSets.UnionSets(4, 2);
            disjointSets.UnionSets(6, 7);
            disjointSets.UnionSets(7, 9);
            disjointSets.UnionSets(9, 0);
            disjointSets.UnionSets(2, 6);
            for (int i = 0; i < 10; ++i)
            {
                for (int j = 0; j < 10; ++j)
                {
                    if (i != j)
                    {
                        if (i != 8 && j != 8)
                        {
                            Assert.IsTrue(disjointSets.AreInSameSet(i, j));
                        }
                        else
                        {
                            Assert.IsFalse(disjointSets.AreInSameSet(i, j));
                        }
                    }
                    else
                    {
                        Assert.IsTrue(disjointSets.AreInSameSet(i, j));
                    }
                }
            }
        }
    }
}
