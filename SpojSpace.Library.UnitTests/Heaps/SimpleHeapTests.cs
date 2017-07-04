using SpojSpace.Library.Heaps;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace SpojSpace.Library.UnitTests.Heaps
{
    [TestClass]
    public class SimpleHeapTests
    {
        private delegate ISimpleHeap<int> SimpleHeapFactory(IEnumerable<int> values = null, IComparer<int> comparer = null);
        private static readonly SimpleHeapFactory simpleNaiveHeapFactory = (values, comparer) => new SimpleNaiveHeap<int>(values, comparer);
        private static readonly SimpleHeapFactory simpleBinaryHeapFactory = (values, comparer) => new SimpleBinaryHeap<int>(values, comparer);

        private static IReadOnlyList<int> _sourceArray = new[] { 3, 9, 1, 4, 12, 6, 8, 42, -13, 1, 22, 11, 1, 0, 22 };
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
            heap.Add(1);
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
            ISimpleHeap<int> heap = heapFactory(_sourceArray);
            Assert.AreEqual(_sourceArray.Count, heap.Size);
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
            ISimpleHeap<int> heap = heapFactory(_sourceArray, descendingComparer);
            Assert.AreEqual(_sourceArray.Count, heap.Size);
            Assert.AreEqual(42, heap.Top);

            heap = heapFactory(new[] { 2, 1 }, descendingComparer);
            Assert.AreEqual(2, heap.Size);
            Assert.AreEqual(2, heap.Top);

            heap = heapFactory(new[] { 2, 3 }, descendingComparer);
            Assert.AreEqual(2, heap.Size);
            Assert.AreEqual(3, heap.Top);
        }

        [TestMethod]
        public void Add1()
        {
            Add1(simpleNaiveHeapFactory);
            Add1(simpleBinaryHeapFactory);
        }

        private void Add1(SimpleHeapFactory heapFactory)
        {
            ISimpleHeap<int> heap = heapFactory(_sourceArray, descendingComparer);

            heap.Add(13);
            Assert.AreEqual(42, heap.Top);

            heap.Add(43);
            Assert.AreEqual(43, heap.Top);

            heap.Add(-100);
            Assert.AreEqual(43, heap.Top);

            heap.Add(50);
            Assert.AreEqual(50, heap.Top);
            Assert.AreEqual(_sourceArray.Count + 4, heap.Size);
        }

        [TestMethod]
        public void Add2()
        {
            Add2(simpleNaiveHeapFactory);
            Add2(simpleBinaryHeapFactory);
        }

        private void Add2(SimpleHeapFactory heapFactory)
        {
            ISimpleHeap<int> heap = heapFactory();

            heap.Add(43);
            Assert.AreEqual(43, heap.Top);

            heap.Add(-11);
            Assert.AreEqual(-11, heap.Top);

            heap.Add(0);
            Assert.AreEqual(-11, heap.Top);

            heap.Add(-13);
            Assert.AreEqual(-13, heap.Top);

            heap.Add(5);
            Assert.AreEqual(-13, heap.Top);

            heap.Add(-50);
            Assert.AreEqual(-50, heap.Top);

            heap.Add(-50);
            Assert.AreEqual(-50, heap.Top);

            heap.Add(0);
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
            heap.Add(13);
            heap.Add(43);
            heap.Add(-100);
            heap.Add(50);
            Assert.AreEqual(50, heap.Extract());
            Assert.AreEqual(3, heap.Size);
            Assert.AreEqual(43, heap.Extract());
            Assert.AreEqual(2, heap.Size);
            Assert.AreEqual(13, heap.Extract());
            Assert.AreEqual(1, heap.Size);
            Assert.AreEqual(-100, heap.Extract());
            Assert.AreEqual(0, heap.Size);

            heap.Add(10);
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
            ISimpleHeap<int> heap = heapFactory(_sourceArray);

            heap.Replace(33);
            Assert.AreEqual(_sourceArray.Count, heap.Size);
            Assert.AreEqual(0, heap.Top);

            heap.Replace(-33);
            Assert.AreEqual(_sourceArray.Count, heap.Size);
            Assert.AreEqual(-33, heap.Top);
        }

        [TestMethod]
        public void VerifyRandomOperationsAgainstEachOther()
        {
            var rand = new Random();
            var simpleNaiveHeap = new SimpleNaiveHeap<int>(_sourceArray);
            var simpleBinaryHeap = new SimpleBinaryHeap<int>(_sourceArray);

            for (int i = 0; i < 10000; ++i)
            {
                int operation = rand.Next(1, 3 + 1);

                if (operation == 1 || simpleNaiveHeap.Size <= 1)
                {
                    int value = rand.Next();
                    simpleNaiveHeap.Add(value);
                    simpleBinaryHeap.Add(value);
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
