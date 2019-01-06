using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spoj.Library.Graphs;
using System.Collections.Generic;
using System.Linq;

namespace Spoj.Library.UnitTests.Graphs
{
    [TestClass]
    public class WeightedRootedTreeTests
    {
        private static KeyValuePair<int, int> KVP(int id, int weight)
            => new KeyValuePair<int, int>(id, weight);

        [TestMethod]
        public void ValidatesATree1()
        {
            // This is the tree pictured here (but zero-based w/ weights): https://www.spoj.com/problems/LCA/.
            var tree = WeightedRootedTree<int>.CreateFromChildren(13, 0, new[]
            {
                new List<KeyValuePair<int, int>> { KVP(1, 1), KVP(2, 2), KVP(3, 3) },
                null,
                new List<KeyValuePair<int, int>> { KVP(4, -1),  KVP(5, -2), KVP(6, -3) },
                null,
                null,
                new List<KeyValuePair<int, int>> { KVP(7, 0), KVP(8, 10) },
                new List<KeyValuePair<int, int>> { KVP(9, 1), KVP(10, 2) },
                null,
                null,
                new List<KeyValuePair<int, int>> { KVP(11, 3), KVP(12, 4) },
                null,
                null,
                null
            });
            tree.InitializeDepthsAndSubtreeSizes();
            ValidatesATree1(tree);

            tree = WeightedRootedTree<int>.CreateFromEdges(13, 0, new[,]
            {
                { 0, 1, 1 }, { 0, 2, 2 }, { 0, 3, 3 },
                { 2, 4, -1 }, { 2, 5, -2 }, { 2, 6, -3 },
                { 9, 6, 1 }, { 10, 6, 2 },
                { 5, 7, 0 }, { 5, 8, 10 },
                { 9, 11, 3 }, { 9, 12, 4 }
            });
            ValidatesATree1(tree);
        }

        private void ValidatesATree1(WeightedRootedTree<int> tree)
        {
            Assert.AreEqual(13, tree.VertexCount);
            CollectionAssert.AreEquivalent(
                new[] { 1, 2, 3 }, tree.Vertices[0].Children.Select(c => c.ID).ToArray());
            Assert.AreEqual(0, tree.Vertices[1].Children.Count);
            CollectionAssert.AreEquivalent(
                new[] { 4, 5, 6 }, tree.Vertices[2].Children.Select(c => c.ID).ToArray());
            Assert.AreEqual(0, tree.Vertices[3].Children.Count);
            Assert.AreEqual(0, tree.Vertices[4].Children.Count);
            CollectionAssert.AreEquivalent(
                new[] { 7, 8 }, tree.Vertices[5].Children.Select(c => c.ID).ToArray());
            CollectionAssert.AreEquivalent(
                new[] { 9, 10 }, tree.Vertices[6].Children.Select(c => c.ID).ToArray());
            Assert.AreEqual(0, tree.Vertices[7].Children.Count);
            Assert.AreEqual(0, tree.Vertices[8].Children.Count);
            CollectionAssert.AreEquivalent(
                new[] { 11, 12 }, tree.Vertices[9].Children.Select(c => c.ID).ToArray());
            Assert.AreEqual(0, tree.Vertices[10].Children.Count);
            Assert.AreEqual(0, tree.Vertices[11].Children.Count);
            Assert.AreEqual(0, tree.Vertices[12].Children.Count);
            Assert.AreEqual(0, tree.Vertices[0].Depth);
            Assert.AreEqual(1, tree.Vertices[1].Depth);
            Assert.AreEqual(1, tree.Vertices[2].Depth);
            Assert.AreEqual(1, tree.Vertices[3].Depth);
            Assert.AreEqual(2, tree.Vertices[4].Depth);
            Assert.AreEqual(2, tree.Vertices[5].Depth);
            Assert.AreEqual(2, tree.Vertices[6].Depth);
            Assert.AreEqual(3, tree.Vertices[7].Depth);
            Assert.AreEqual(3, tree.Vertices[8].Depth);
            Assert.AreEqual(3, tree.Vertices[9].Depth);
            Assert.AreEqual(3, tree.Vertices[10].Depth);
            Assert.AreEqual(4, tree.Vertices[11].Depth);
            Assert.AreEqual(4, tree.Vertices[12].Depth);
            Assert.AreEqual(13, tree.Vertices[0].SubtreeSize);
            Assert.AreEqual(1, tree.Vertices[1].SubtreeSize);
            Assert.AreEqual(10, tree.Vertices[2].SubtreeSize);
            Assert.AreEqual(1, tree.Vertices[3].SubtreeSize);
            Assert.AreEqual(1, tree.Vertices[4].SubtreeSize);
            Assert.AreEqual(3, tree.Vertices[5].SubtreeSize);
            Assert.AreEqual(5, tree.Vertices[6].SubtreeSize);
            Assert.AreEqual(1, tree.Vertices[7].SubtreeSize);
            Assert.AreEqual(1, tree.Vertices[8].SubtreeSize);
            Assert.AreEqual(3, tree.Vertices[9].SubtreeSize);
            Assert.AreEqual(1, tree.Vertices[10].SubtreeSize);
            Assert.AreEqual(1, tree.Vertices[11].SubtreeSize);
            Assert.AreEqual(1, tree.Vertices[12].SubtreeSize);
            Assert.AreEqual(null, tree.Vertices[0].Parent);
            Assert.AreEqual(1, tree.Vertices[1].Weight);
            Assert.AreEqual(2, tree.Vertices[2].Weight);
            Assert.AreEqual(3, tree.Vertices[3].Weight);
            Assert.AreEqual(-1, tree.Vertices[4].Weight);
            Assert.AreEqual(-2, tree.Vertices[5].Weight);
            Assert.AreEqual(-3, tree.Vertices[6].Weight);
            Assert.AreEqual(0, tree.Vertices[7].Weight);
            Assert.AreEqual(10, tree.Vertices[8].Weight);
            Assert.AreEqual(1, tree.Vertices[9].Weight);
            Assert.AreEqual(2, tree.Vertices[10].Weight);
            Assert.AreEqual(3, tree.Vertices[11].Weight);
            Assert.AreEqual(4, tree.Vertices[12].Weight);
        }

        [TestMethod]
        public void ValidatesATree2()
        {
            var tree = WeightedRootedTree<int>.CreateFromChildren(1, 0, new List<KeyValuePair<int, int>>[] { null });
            tree.InitializeDepthsAndSubtreeSizes();
            ValidatesATree2(tree);

            tree = WeightedRootedTree<int>.CreateFromEdges(1, 0, new int[,] { });
            ValidatesATree2(tree);
        }

        private void ValidatesATree2(WeightedRootedTree<int> tree)
        {
            Assert.AreEqual(1, tree.VertexCount);
            Assert.AreEqual(0, tree.Vertices[0].Children.Count);
            Assert.AreEqual(0, tree.Vertices[0].Depth);
            Assert.AreEqual(1, tree.Vertices[0].SubtreeSize);
        }

        [TestMethod]
        public void ValidatesATree3()
        {
            var tree = WeightedRootedTree<int>.CreateFromChildren(3, 0, new[]
            {
                new List<KeyValuePair<int, int>> { KVP(1, 10) },
                new List<KeyValuePair<int, int>> { KVP(2, 11) },
                null
            });
            tree.InitializeDepthsAndSubtreeSizes();
            ValidatesATree3(tree);

            tree = WeightedRootedTree<int>.CreateFromEdges(3, 0, new[,]
            {
                { 2, 1, 11 },
                { 1, 0, 10 }
            });
            ValidatesATree3(tree);
        }

        private void ValidatesATree3(WeightedRootedTree<int> tree)
        {
            Assert.AreEqual(3, tree.VertexCount);
            Assert.AreEqual(1, tree.Vertices[0].Children.Count);
            Assert.AreEqual(1, tree.Vertices[0].Children[0].ID);
            Assert.AreEqual(1, tree.Vertices[1].Children.Count);
            Assert.AreEqual(2, tree.Vertices[1].Children[0].ID);
            Assert.AreEqual(0, tree.Vertices[2].Children.Count);
            Assert.AreEqual(0, tree.Vertices[0].Depth);
            Assert.AreEqual(1, tree.Vertices[1].Depth);
            Assert.AreEqual(2, tree.Vertices[2].Depth);
            Assert.AreEqual(3, tree.Vertices[0].SubtreeSize);
            Assert.AreEqual(2, tree.Vertices[1].SubtreeSize);
            Assert.AreEqual(1, tree.Vertices[2].SubtreeSize);
            Assert.AreEqual(null, tree.Vertices[0].Parent);
            Assert.AreEqual(10, tree.Vertices[1].Weight);
            Assert.AreEqual(11, tree.Vertices[2].Weight);
        }

        [TestMethod]
        public void GetEulerTour1()
        {
            // This is the tree pictured here (but zero-based w/ weights): https://www.spoj.com/problems/LCA/.
            var tree = WeightedRootedTree<int>.CreateFromEdges(13, 0, new[,]
            {
                { 0, 1, 1 }, { 0, 2, 2 }, { 0, 3, 3 },
                { 2, 4, -1 }, { 2, 5, -2 }, { 2, 6, -3 },
                { 9, 6, 1 }, { 10, 6, 2 },
                { 5, 7, 0 }, { 5, 8, 10 },
                { 9, 11, 3 }, { 9, 12, 4 }
            });
            var eulerTour = tree.GetEulerTour();

            CollectionAssert.AreEqual(
                new[] { 0, 1, 0, 2, 4, 2, 5, 7, 5, 8, 5, 2, 6, 9, 11, 9, 12, 9, 6, 10, 6, 2, 0, 3, 0 },
                eulerTour.Select(v => v.ID).ToArray());
            Assert.AreEqual(0, tree.Vertices[0].EulerTourInitialIndex);
            Assert.AreEqual(1, tree.Vertices[1].EulerTourInitialIndex);
            Assert.AreEqual(3, tree.Vertices[2].EulerTourInitialIndex);
            Assert.AreEqual(4, tree.Vertices[4].EulerTourInitialIndex);
            Assert.AreEqual(6, tree.Vertices[5].EulerTourInitialIndex);
            Assert.AreEqual(14, tree.Vertices[11].EulerTourInitialIndex);
            Assert.AreEqual(0, tree.Vertices[0].Depth);
            Assert.AreEqual(1, tree.Vertices[1].Depth);
            Assert.AreEqual(1, tree.Vertices[2].Depth);
            Assert.AreEqual(1, tree.Vertices[3].Depth);
            Assert.AreEqual(2, tree.Vertices[4].Depth);
            Assert.AreEqual(2, tree.Vertices[5].Depth);
            Assert.AreEqual(2, tree.Vertices[6].Depth);
            Assert.AreEqual(3, tree.Vertices[7].Depth);
            Assert.AreEqual(3, tree.Vertices[8].Depth);
            Assert.AreEqual(3, tree.Vertices[9].Depth);
            Assert.AreEqual(3, tree.Vertices[10].Depth);
            Assert.AreEqual(4, tree.Vertices[11].Depth);
            Assert.AreEqual(4, tree.Vertices[12].Depth);
        }

        [TestMethod]
        public void GetEulerTour2()
        {
            var tree = WeightedRootedTree<int>.CreateFromEdges(1, 0, new int[,] { });
            var eulerTour = tree.GetEulerTour();

            CollectionAssert.AreEqual(new[] { 0 }, eulerTour.Select(v => v.ID).ToArray());
            Assert.AreEqual(0, tree.Vertices[0].Depth);
        }

        [TestMethod]
        public void GetEulerTour3()
        {
            var tree = WeightedRootedTree<int>.CreateFromEdges(3, 0, new[,]
            {
                { 2, 1, 11 },
                { 1, 0, 10 }
            });
            var eulerTour = tree.GetEulerTour();

            CollectionAssert.AreEqual(new[] { 0, 1, 2, 1, 0 }, eulerTour.Select(v => v.ID).ToArray());
            Assert.AreEqual(0, tree.Vertices[0].Depth);
            Assert.AreEqual(1, tree.Vertices[1].Depth);
            Assert.AreEqual(2, tree.Vertices[2].Depth);
        }

        [TestMethod]
        public void RunHLD()
        {
            // This is the tree pictured here (but zero-based):
            // https://www.geeksforgeeks.org/heavy-light-decomposition-set-2-implementation/.
            var tree = WeightedRootedTree<int>.CreateFromEdges(11, 0, new[,]
            {
                {0, 1, 13 }, {0, 2, 9 }, {0, 3, 23 },
                {1, 4, 4 }, {1, 5, 25 },
                {2, 6, 29 },
                {5, 7, 5 },
                {6, 8, 30 },
                {7, 9, 1 }, { 7, 10, 6 }
            });
            tree.RunHLD();

            CollectionAssert.AreEqual(new[] { 0, 10, 4, 2, 3 }, tree.HLDChainHeads.Select(v => v.ID).ToArray());
            Assert.AreEqual(0, tree.Vertices[0].HLDChainIndex);
            Assert.AreEqual(0, tree.Vertices[1].HLDChainIndex);
            Assert.AreEqual(0, tree.Vertices[5].HLDChainIndex);
            Assert.AreEqual(0, tree.Vertices[7].HLDChainIndex);
            Assert.AreEqual(0, tree.Vertices[9].HLDChainIndex);
            Assert.AreEqual(1, tree.Vertices[10].HLDChainIndex);
            Assert.AreEqual(2, tree.Vertices[4].HLDChainIndex);
            Assert.AreEqual(3, tree.Vertices[2].HLDChainIndex);
            Assert.AreEqual(3, tree.Vertices[6].HLDChainIndex);
            Assert.AreEqual(3, tree.Vertices[8].HLDChainIndex);
            Assert.AreEqual(4, tree.Vertices[3].HLDChainIndex);

            CollectionAssert.AreEqual(
                new[] { 0, 1, 5, 7, 9, 10, 4, 2, 6, 8, 3},
                tree.HLDBaseArray.Select(v => v.ID).ToArray());
            Assert.AreEqual(0, tree.Vertices[0].HLDBaseArrayIndex);
            Assert.AreEqual(1, tree.Vertices[1].HLDBaseArrayIndex);
            Assert.AreEqual(2, tree.Vertices[5].HLDBaseArrayIndex);
            Assert.AreEqual(3, tree.Vertices[7].HLDBaseArrayIndex);
            Assert.AreEqual(4, tree.Vertices[9].HLDBaseArrayIndex);
            Assert.AreEqual(5, tree.Vertices[10].HLDBaseArrayIndex);
            Assert.AreEqual(6, tree.Vertices[4].HLDBaseArrayIndex);
            Assert.AreEqual(7, tree.Vertices[2].HLDBaseArrayIndex);
            Assert.AreEqual(8, tree.Vertices[6].HLDBaseArrayIndex);
            Assert.AreEqual(9, tree.Vertices[8].HLDBaseArrayIndex);
            Assert.AreEqual(10, tree.Vertices[3].HLDBaseArrayIndex);
        }
    }
}
