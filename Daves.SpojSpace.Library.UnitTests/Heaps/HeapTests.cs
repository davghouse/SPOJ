using Daves.SpojSpace.Library.Heaps;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Daves.SpojSpace.Library.UnitTests.Heaps
{
    [TestClass]
    public class HeapTests
    {
        private class Key
        {
            public Key(int id)
                => ID = id;

            public int ID { get; }

            public override int GetHashCode()
                => ID;

            public override bool Equals(object obj)
                => (obj as Key)?.ID == ID;
        }

        private delegate IHeap<Key, int> HeapFactory(IEnumerable<KeyValuePair<Key, int>> keyValuePairs = null, IComparer<int> comparer = null);
        private static readonly HeapFactory naiveHeapFactory = (keyValuePairs, comparer) => new NaiveHeap<Key, int>(keyValuePairs, comparer);
        private static readonly HeapFactory binaryHeapFactory = (keyValuePairs, comparer) => new BinaryHeap<Key, int>(keyValuePairs, comparer);

        private static IReadOnlyList<KeyValuePair<Key, int>> CreateSourceArray(int[] valueArray)
        {
            var sourceArray = new List<KeyValuePair<Key, int>>();
            for (int i = 0; i < valueArray.Length; ++i)
            {
                sourceArray.Add(new KeyValuePair<Key, int>(new Key(i + 1), valueArray[i]));
            }

            return sourceArray;
        }

        //                                                                                       IDs: 1  2  3  4  5   6  7  8    9   10 11  12  13 14 15
        private static IReadOnlyList<KeyValuePair<Key, int>> _sourceArray = CreateSourceArray(new[] { 3, 9, 1, 4, 12, 6, 8, 42, -13, 1, 22, 11, 1, 0, 22 });
        private static Comparer<int> descendingComparer = Comparer<int>.Create((a, b) => b.CompareTo(a));

        [TestMethod]
        public void DefaultConstruction()
        {
            DefaultConstruction(naiveHeapFactory);
            DefaultConstruction(binaryHeapFactory);
        }

        private void DefaultConstruction(HeapFactory heapFactory)
        {
            IHeap<Key, int> heap = heapFactory();
            heap.Insert(new KeyValuePair<Key, int>(new Key(1), 2));
            Assert.AreEqual(1, heap.Top.Key.ID);
            Assert.AreEqual(2, heap.Top.Value);
        }

        [TestMethod]
        public void CollectionCostruction()
        {
            CollectionConstruction(naiveHeapFactory);
            CollectionConstruction(binaryHeapFactory);
        }

        private void CollectionConstruction(HeapFactory heapFactory)
        {
            IHeap<Key, int> heap = heapFactory(_sourceArray);
            Assert.AreEqual(_sourceArray.Count, heap.Size);
            Assert.AreEqual(9, heap.Top.Key.ID);
            Assert.AreEqual(-13, heap.Top.Value);

            heap = heapFactory(CreateSourceArray(new[] { 2, 1 }));
            Assert.AreEqual(2, heap.Size);
            Assert.AreEqual(2, heap.Top.Key.ID);
            Assert.AreEqual(1, heap.Top.Value);

            heap = heapFactory(CreateSourceArray(new[] { 2, 3 }));
            Assert.AreEqual(2, heap.Size);
            Assert.AreEqual(1, heap.Top.Key.ID);
            Assert.AreEqual(2, heap.Top.Value);
        }

        [TestMethod]
        public void ComparerConstruction()
        {
            ComparerConstruction(naiveHeapFactory);
            ComparerConstruction(binaryHeapFactory);
        }

        private void ComparerConstruction(HeapFactory heapFactory)
        {
            IHeap<Key, int> heap = heapFactory(_sourceArray, descendingComparer);
            Assert.AreEqual(_sourceArray.Count, heap.Size);
            Assert.AreEqual(8, heap.Top.Key.ID);
            Assert.AreEqual(42, heap.Top.Value);

            heap = heapFactory(CreateSourceArray(new[] { 2, 1 }), descendingComparer);
            Assert.AreEqual(2, heap.Size);
            Assert.AreEqual(1, heap.Top.Key.ID);
            Assert.AreEqual(2, heap.Top.Value);

            heap = heapFactory(CreateSourceArray(new[] { 2, 3 }), descendingComparer);
            Assert.AreEqual(2, heap.Size);
            Assert.AreEqual(2, heap.Top.Key.ID);
            Assert.AreEqual(3, heap.Top.Value);
        }

        [TestMethod]
        public void Insert1()
        {
            Insert1(naiveHeapFactory);
            Insert1(binaryHeapFactory);
        }

        private void Insert1(HeapFactory heapFactory)
        {
            IHeap<Key, int> heap = heapFactory(_sourceArray, descendingComparer);
            heap.Insert(new KeyValuePair<Key, int>(new Key(16), 13));
            Assert.AreEqual(8, heap.Top.Key.ID);
            Assert.AreEqual(42, heap.Top.Value);
            heap.Insert(new KeyValuePair<Key, int>(new Key(17), 43));
            Assert.AreEqual(17, heap.Top.Key.ID);
            Assert.AreEqual(43, heap.Top.Value);
            heap.Insert(new KeyValuePair<Key, int>(new Key(18), -100));
            Assert.AreEqual(17, heap.Top.Key.ID);
            Assert.AreEqual(43, heap.Top.Value);
            heap.Insert(new KeyValuePair<Key, int>(new Key(19), 50));
            Assert.AreEqual(19, heap.Top.Key.ID);
            Assert.AreEqual(50, heap.Top.Value);
            Assert.AreEqual(_sourceArray.Count + 4, heap.Size);
        }

        [TestMethod]
        public void Insert2()
        {
            Insert2(naiveHeapFactory);
            Insert2(binaryHeapFactory);
        }

        private void Insert2(HeapFactory heapFactory)
        {
            IHeap<Key, int> heap = heapFactory();
            heap.Insert(new KeyValuePair<Key, int>(new Key(1), 43));
            Assert.AreEqual(1, heap.Top.Key.ID);
            Assert.AreEqual(43, heap.Top.Value);
            heap.Insert(new KeyValuePair<Key, int>(new Key(2), -11));
            Assert.AreEqual(2, heap.Top.Key.ID);
            Assert.AreEqual(-11, heap.Top.Value);
            heap.Insert(new KeyValuePair<Key, int>(new Key(3), 0));
            Assert.AreEqual(2, heap.Top.Key.ID);
            Assert.AreEqual(-11, heap.Top.Value);
            heap.Insert(new KeyValuePair<Key, int>(new Key(4), -13));
            Assert.AreEqual(4, heap.Top.Key.ID);
            Assert.AreEqual(-13, heap.Top.Value);
            heap.Insert(new KeyValuePair<Key, int>(new Key(5), 5));
            Assert.AreEqual(4, heap.Top.Key.ID);
            Assert.AreEqual(-13, heap.Top.Value);
            heap.Insert(new KeyValuePair<Key, int>(new Key(6), -50));
            Assert.AreEqual(6, heap.Top.Key.ID);
            Assert.AreEqual(-50, heap.Top.Value);
            heap.Insert(new KeyValuePair<Key, int>(new Key(7), -50));
            Assert.AreEqual(6, heap.Top.Key.ID);
            Assert.AreEqual(-50, heap.Top.Value);
            heap.Insert(new KeyValuePair<Key, int>(new Key(8), 0));
            Assert.AreEqual(6, heap.Top.Key.ID);
            Assert.AreEqual(-50, heap.Top.Value);
            Assert.AreEqual(8, heap.Size);
        }

        [TestMethod]
        public void Extract()
        {
            Extract(naiveHeapFactory);
            Extract(binaryHeapFactory);
        }

        private void Extract(HeapFactory heapFactory)
        {
            IHeap<Key, int> heap = heapFactory(comparer: descendingComparer);
            heap.Insert(new KeyValuePair<Key, int>(new Key(1), 13));
            heap.Insert(new KeyValuePair<Key, int>(new Key(2), 43));
            heap.Insert(new KeyValuePair<Key, int>(new Key(3), -100));
            heap.Insert(new KeyValuePair<Key, int>(new Key(4), 50));
            var top = heap.Extract();
            Assert.AreEqual(4, top.Key.ID);
            Assert.AreEqual(50, top.Value);
            Assert.AreEqual(3, heap.Size);
            top = heap.Extract();
            Assert.AreEqual(2, top.Key.ID);
            Assert.AreEqual(43, top.Value);
            Assert.AreEqual(2, heap.Size);
            top = heap.Extract();
            Assert.AreEqual(1, top.Key.ID);
            Assert.AreEqual(13, top.Value);
            Assert.AreEqual(1, heap.Size);
            top = heap.Extract();
            Assert.AreEqual(3, top.Key.ID);
            Assert.AreEqual(-100, top.Value);
            Assert.AreEqual(0, heap.Size);

            heap.Insert(new KeyValuePair<Key, int>(new Key(5), 10));
            top = heap.Extract();
            Assert.AreEqual(5, top.Key.ID);
            Assert.AreEqual(10, top.Value);
            Assert.AreEqual(0, heap.Size);
        }

        [TestMethod]
        public void Replace()
        {
            Replace(naiveHeapFactory);
            Replace(binaryHeapFactory);
        }

        private void Replace(HeapFactory heapFactory)
        {
            IHeap<Key, int> heap = heapFactory(_sourceArray);
            heap.Replace(new KeyValuePair<Key, int>(new Key(16), 33));
            Assert.AreEqual(_sourceArray.Count, heap.Size);
            Assert.AreEqual(14, heap.Top.Key.ID);
            Assert.AreEqual(0, heap.Top.Value);
            heap.Replace(new KeyValuePair<Key, int>(new Key(17), -33));
            Assert.AreEqual(_sourceArray.Count, heap.Size);
            Assert.AreEqual(17, heap.Top.Key.ID);
            Assert.AreEqual(-33, heap.Top.Value);
        }

        [TestMethod]
        public void VerifyRandomOperationsAgainstEachOther()
        {
            var rand = new Random();
            var naiveHeap = new NaiveHeap<Key, int>();
            var binaryHeap = new BinaryHeap<Key, int>();

            for (int i = 0; i < 10000; ++i)
            {
                int operation = rand.Next(1, 3 + 1);

                if (operation == 1 || naiveHeap.Size <= 1)
                {
                    int value = rand.Next();
                    naiveHeap.Insert(new KeyValuePair<Key, int>(new Key(i + 16), value));
                    binaryHeap.Insert(new KeyValuePair<Key, int>(new Key(i + 16), value));
                }
                else if (operation == 2)
                {
                    var naiveHeapTop = naiveHeap.Extract();
                    var binaryHeapTop = binaryHeap.Extract();
                    Assert.AreEqual(naiveHeapTop.Key.ID, binaryHeapTop.Key.ID);
                    Assert.AreEqual(naiveHeapTop.Value, binaryHeapTop.Value);
                }
                else
                {
                    int value = rand.Next();
                    var naiveHeapTop = naiveHeap.Replace(new KeyValuePair<Key, int>(new Key(i + 16), value));
                    var binaryHeapTop = binaryHeap.Replace(new KeyValuePair<Key, int>(new Key(i + 16), value));
                    Assert.AreEqual(naiveHeapTop.Key.ID, binaryHeapTop.Key.ID);
                    Assert.AreEqual(naiveHeapTop.Value, binaryHeapTop.Value);
                }

                Assert.AreEqual(naiveHeap.Size, binaryHeap.Size);
                Assert.AreEqual(naiveHeap.IsEmpty, binaryHeap.IsEmpty);
                Assert.AreEqual(naiveHeap.Top.Key.ID, binaryHeap.Top.Key.ID);
                Assert.AreEqual(naiveHeap.Top.Value, binaryHeap.Top.Value);
            }
        }
    }
}
