using Daves.SpojSpace.Library.Heaps;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Daves.SpojSpace.Library.UnitTests.Heaps
{
    [TestClass]
    public class SimpleHeapTests
    {
        private delegate ISimpleHeap<int> SimpleHeapFactory(IEnumerable<int> values = null, IComparer<int> comparer = null);
        private static readonly SimpleHeapFactory simpleNaiveHeapFactory = (values, comparer) => new SimpleNaiveHeap<int>(values, comparer);
        private static readonly SimpleHeapFactory simpleBinaryHeapFactory = (values, comparer) => new SimpleBinaryHeap<int>(values, comparer);

        private static IReadOnlyList<int> _array = new[] { 3, 9, 1, 4, 12, 6, 8, 42, -13, 1, 22, 11, 1, 0, 22 };
        private static Comparer<int> descendingComparer = Comparer<int>.Create((a, b) => b.CompareTo(a));

        [TestMethod]
        public void DefaultConstruction()
        {
            DefaultConstruction(simpleNaiveHeapFactory);
            DefaultConstruction(simpleBinaryHeapFactory);
        }

        private void DefaultConstruction(SimpleHeapFactory heapFactory)
        {
            ISimpleHeap<int> heap = heapFactory();
            heap.Insert(1);
            Assert.AreEqual(1, heap.Top);
        }

        [TestMethod]
        public void CollectionCostruction()
        {
            CollectionConstruction(simpleNaiveHeapFactory);
            CollectionConstruction(simpleBinaryHeapFactory);
        }

        private void CollectionConstruction(SimpleHeapFactory heapFactory)
        {
            ISimpleHeap<int> heap = heapFactory(_array);
            Assert.AreEqual(_array.Count, heap.Size);
            Assert.AreEqual(-13, heap.Top);

            heap = heapFactory(new[] { 2, 1 });
            Assert.AreEqual(2, heap.Size);
            Assert.AreEqual(1, heap.Top);

            heap = heapFactory(new[] { 2, 3 });
            Assert.AreEqual(2, heap.Size);
            Assert.AreEqual(2, heap.Top);
        }

        [TestMethod]
        public void ComparerConstruction()
        {
            ComparerConstruction(simpleNaiveHeapFactory);
            ComparerConstruction(simpleBinaryHeapFactory);
        }

        private void ComparerConstruction(SimpleHeapFactory heapFactory)
        {
            ISimpleHeap<int> heap = heapFactory(_array, descendingComparer);
            Assert.AreEqual(_array.Count, heap.Size);
            Assert.AreEqual(42, heap.Top);

            heap = heapFactory(new[] { 2, 1 }, descendingComparer);
            Assert.AreEqual(2, heap.Size);
            Assert.AreEqual(2, heap.Top);

            heap = heapFactory(new[] { 2, 3 }, descendingComparer);
            Assert.AreEqual(2, heap.Size);
            Assert.AreEqual(3, heap.Top);
        }

        [TestMethod]
        public void Insert1()
        {
            Insert1(simpleNaiveHeapFactory);
            Insert1(simpleBinaryHeapFactory);
        }

        private void Insert1(SimpleHeapFactory heapFactory)
        {
            ISimpleHeap<int> heap = heapFactory(_array, descendingComparer);
            heap.Insert(13);
            Assert.AreEqual(42, heap.Top);
            heap.Insert(43);
            Assert.AreEqual(43, heap.Top);
            heap.Insert(-100);
            Assert.AreEqual(43, heap.Top);
            heap.Insert(50);
            Assert.AreEqual(50, heap.Top);
            Assert.AreEqual(_array.Count + 4, heap.Size);
        }

        [TestMethod]
        public void Insert2()
        {
            Insert2(simpleNaiveHeapFactory);
            Insert2(simpleBinaryHeapFactory);
        }

        private void Insert2(SimpleHeapFactory heapFactory)
        {
            ISimpleHeap<int> heap = heapFactory();
            heap.Insert(43);
            Assert.AreEqual(43, heap.Top);
            heap.Insert(-11);
            Assert.AreEqual(-11, heap.Top);
            heap.Insert(0);
            Assert.AreEqual(-11, heap.Top);
            heap.Insert(-13);
            Assert.AreEqual(-13, heap.Top);
            heap.Insert(5);
            Assert.AreEqual(-13, heap.Top);
            heap.Insert(-50);
            Assert.AreEqual(-50, heap.Top);
            heap.Insert(-50);
            Assert.AreEqual(-50, heap.Top);
            heap.Insert(0);
            Assert.AreEqual(-50, heap.Top);
            Assert.AreEqual(8, heap.Size);
        }

        [TestMethod]
        public void Extract()
        {
            Extract(simpleNaiveHeapFactory);
            Extract(simpleBinaryHeapFactory);
        }

        private void Extract(SimpleHeapFactory heapFactory)
        {
            ISimpleHeap<int> heap = heapFactory(comparer: descendingComparer);
            heap.Insert(13);
            heap.Insert(43);
            heap.Insert(-100);
            heap.Insert(50);
            Assert.AreEqual(50, heap.Extract());
            Assert.AreEqual(3, heap.Size);
            Assert.AreEqual(43, heap.Extract());
            Assert.AreEqual(2, heap.Size);
            Assert.AreEqual(13, heap.Extract());
            Assert.AreEqual(1, heap.Size);
            Assert.AreEqual(-100, heap.Extract());
            Assert.AreEqual(0, heap.Size);

            heap.Insert(10);
            Assert.AreEqual(10, heap.Extract());
            Assert.AreEqual(0, heap.Size);
        }

        [TestMethod]
        public void Replace()
        {
            Replace(simpleNaiveHeapFactory);
            Replace(simpleBinaryHeapFactory);
        }

        private void Replace(SimpleHeapFactory heapFactory)
        {
            ISimpleHeap<int> heap = heapFactory(_array);
            heap.Replace(33);
            Assert.AreEqual(_array.Count, heap.Size);
            Assert.AreEqual(0, heap.Top);
            heap.Replace(-33);
            Assert.AreEqual(_array.Count, heap.Size);
            Assert.AreEqual(-33, heap.Top);
        }

        [TestMethod]
        public void VerifyRandomOperationsAgainstEachOther()
        {
            var rand = new Random();
            var simpleNaiveHeap = new SimpleNaiveHeap<int>(_array);
            var simpleBinaryHeap = new SimpleBinaryHeap<int>(_array);

            for (int i = 0; i < 10000; ++i)
            {
                int operation = rand.Next(1, 3 + 1);

                if (operation == 1 || simpleNaiveHeap.Size <= 1)
                {
                    int value = rand.Next();
                    simpleNaiveHeap.Insert(value);
                    simpleBinaryHeap.Insert(value);
                }
                else if (operation == 2)
                {
                    int simpleNaiveHeapTop = simpleNaiveHeap.Extract();
                    int simpleBinaryHeapTop = simpleBinaryHeap.Extract();
                    Assert.AreEqual(simpleNaiveHeapTop, simpleBinaryHeapTop);
                }
                else
                {
                    int value = rand.Next();
                    int simpleNaiveHeapTop = simpleNaiveHeap.Replace(value);
                    int simpleBinaryHeapTop = simpleBinaryHeap.Replace(value);
                    Assert.AreEqual(simpleNaiveHeapTop, simpleBinaryHeapTop);
                }

                Assert.AreEqual(simpleNaiveHeap.Size, simpleBinaryHeap.Size);
                Assert.AreEqual(simpleNaiveHeap.IsEmpty, simpleBinaryHeap.IsEmpty);
                Assert.AreEqual(simpleNaiveHeap.Top, simpleBinaryHeap.Top);
            }
        }
    }
}
