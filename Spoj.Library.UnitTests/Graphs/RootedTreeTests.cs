using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spoj.Library.Graphs;
using Spoj.Library.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace Spoj.Library.UnitTests.Graphs
{
    [TestClass]
    public class RootedTreeTests
    {
        [TestMethod]
        public void ValidatesATree1()
        {
            // This is the tree pictured here (but zero-based): https://www.spoj.com/problems/LCA/.
            var tree = RootedTree.CreateFromExplicitChildren(13, 0, new[]
            {
                new List<int> { 1, 2, 3 },
                null,
                new List<int> { 4, 5, 6 },
                null,
                null,
                new List<int> { 7, 8 },
                new List<int> { 9, 10 },
                null,
                null,
                new List<int> { 11, 12 },
                null,
                null,
                null
            });
            tree.InitializeDepthsAndSubtreeSizes();

            Assert.AreEqual(13, tree.VertexCount);
            Assert.AreEqual(3, tree.Vertices[0].Children.Count);
            Assert.AreEqual(0, tree.Vertices[1].Children.Count);
            Assert.AreEqual(3, tree.Vertices[2].Children.Count);
            Assert.AreEqual(0, tree.Vertices[3].Children.Count);
            Assert.AreEqual(0, tree.Vertices[4].Children.Count);
            Assert.AreEqual(2, tree.Vertices[5].Children.Count);
            Assert.AreEqual(2, tree.Vertices[6].Children.Count);
            Assert.AreEqual(0, tree.Vertices[7].Children.Count);
            Assert.AreEqual(0, tree.Vertices[8].Children.Count);
            Assert.AreEqual(2, tree.Vertices[9].Children.Count);
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
        }

        [TestMethod]
        public void ValidatesATree2()
        {
            var tree = RootedTree.CreateFromExplicitChildren(1, 0, new List<int>[] { null });
            tree.InitializeDepthsAndSubtreeSizes();

            Assert.AreEqual(1, tree.VertexCount);
            Assert.AreEqual(0, tree.Vertices[0].Children.Count);
            Assert.AreEqual(0, tree.Vertices[0].Depth);
            Assert.AreEqual(1, tree.Vertices[0].SubtreeSize);
        }

        [TestMethod]
        public void ValidatesATree3()
        {
            var tree = RootedTree.CreateFromExplicitChildren(3, 0, new[]
            {
                new List<int> { 1 },
                new List<int> { 2 },
                null
            });
            tree.InitializeDepthsAndSubtreeSizes();

            Assert.AreEqual(3, tree.VertexCount);
            Assert.AreEqual(1, tree.Vertices[0].Children.Count);
            Assert.AreEqual(1, tree.Vertices[1].Children.Count);
            Assert.AreEqual(0, tree.Vertices[2].Children.Count);
            Assert.AreEqual(0, tree.Vertices[0].Depth);
            Assert.AreEqual(1, tree.Vertices[1].Depth);
            Assert.AreEqual(2, tree.Vertices[2].Depth);
            Assert.AreEqual(3, tree.Vertices[0].SubtreeSize);
            Assert.AreEqual(2, tree.Vertices[1].SubtreeSize);
            Assert.AreEqual(1, tree.Vertices[2].SubtreeSize);
        }

        [TestMethod]
        public void GetEulerTour1()
        {
            GetEulerTour1(useStack: true);
            GetEulerTour1(useStack: false);
        }

        private void GetEulerTour1(bool useStack)
        {
            // This is the tree pictured here (but zero-based): https://www.spoj.com/problems/LCA/.
            var tree = RootedTree.CreateFromExplicitChildren(13, 0, new[]
            {
                new List<int> { 1, 2, 3 },
                null,
                new List<int> { 4, 5, 6 },
                null,
                null,
                new List<int> { 7, 8 },
                new List<int> { 9, 10 },
                null,
                null,
                new List<int> { 11, 12 },
                null,
                null,
                null
            });
            var eulerTour = useStack
                ? tree.GetEulerTourUsingStack()
                : tree.GetEulerTourUsingRecursion();

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
            GetEulerTour2(useStack: true);
            GetEulerTour2(useStack: false);
        }

        private void GetEulerTour2(bool useStack)
        {
            var tree = RootedTree.CreateFromExplicitChildren(1, 0, new List<int>[] { null });
            var eulerTour = useStack
                ? tree.GetEulerTourUsingStack()
                : tree.GetEulerTourUsingRecursion();

            CollectionAssert.AreEqual(new[] { 0 }, eulerTour.Select(v => v.ID).ToArray());
            Assert.AreEqual(0, tree.Vertices[0].Depth);
        }

        [TestMethod]
        public void GetEulerTour3()
        {
            GetEulerTour3(useStack: true);
            GetEulerTour3(useStack: false);
        }

        private void GetEulerTour3(bool useStack)
        {
            var tree = RootedTree.CreateFromExplicitChildren(3, 0, new[]
            {
                new List<int> { 1 },
                new List<int> { 2 },
                null
            });
            var eulerTour = useStack
                ? tree.GetEulerTourUsingStack()
                : tree.GetEulerTourUsingRecursion();

            CollectionAssert.AreEqual(new[] { 0, 1, 2, 1, 0 }, eulerTour.Select(v => v.ID).ToArray());
            Assert.AreEqual(0, tree.Vertices[0].Depth);
            Assert.AreEqual(1, tree.Vertices[1].Depth);
            Assert.AreEqual(2, tree.Vertices[2].Depth);
        }

        [TestMethod]
        public void CompareEulerTours()
        {
            var randomTree = InputGenerator.GenerateRandomRootedTree(1000, 1, 3);
            CollectionAssert.AreEqual(
                randomTree.GetEulerTourUsingStack().ToArray(),
                randomTree.GetEulerTourUsingRecursion());
        }
    }
}
