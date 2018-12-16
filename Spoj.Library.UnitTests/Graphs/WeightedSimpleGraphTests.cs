using Spoj.Library.Graphs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Spoj.Library.UnitTests.Graphs
{
    [TestClass]
    public class WeightedSimpleGraphTests
    {
        [TestMethod]
        public void ValidatesAGraph1()
        {
            // This graph is a triangle.
            var graph = WeightedSimpleGraph<int>.CreateFromZeroBasedEdges(3, new[,]
            {
                { 0, 1, 1 }, { 0, 2, 2 }, { 1, 2, 3 }
            });

            Assert.AreEqual(2, graph.Vertices[0].Degree);
            Assert.AreEqual(2, graph.Vertices[1].Degree);
            Assert.AreEqual(2, graph.Vertices[2].Degree);

            Assert.IsTrue(graph.HasEdge(0, 1));
            Assert.IsTrue(graph.HasEdge(1, 0));
            Assert.IsTrue(graph.HasEdge(0, 2));
            Assert.IsTrue(graph.HasEdge(1, 2));
            Assert.IsFalse(graph.HasEdge(1, 1));

            Assert.IsFalse(graph.Vertices[0].HasNeighbor(0));
            Assert.IsTrue(graph.Vertices[0].HasNeighbor(1));
            Assert.IsTrue(graph.Vertices[0].HasNeighbor(2));

            Assert.IsTrue(graph.Vertices[1].HasNeighbor(0));
            Assert.IsFalse(graph.Vertices[1].HasNeighbor(1));
            Assert.IsTrue(graph.Vertices[1].HasNeighbor(2));

            Assert.IsTrue(graph.Vertices[2].HasNeighbor(0));
            Assert.IsTrue(graph.Vertices[2].HasNeighbor(1));
            Assert.IsFalse(graph.Vertices[2].HasNeighbor(2));

            Assert.AreEqual(1, graph.Vertices[0].GetEdgeWeight(1));
            Assert.AreEqual(2, graph.Vertices[0].GetEdgeWeight(2));
            Assert.AreEqual(1, graph.Vertices[1].GetEdgeWeight(0));
            Assert.AreEqual(3, graph.Vertices[1].GetEdgeWeight(2));
            Assert.AreEqual(2, graph.Vertices[2].GetEdgeWeight(0));
            Assert.AreEqual(3, graph.Vertices[2].GetEdgeWeight(1));
        }

        [TestMethod]
        public void ValidatesAGraph2()
        {
            // This graph is two lines and a point.
            var graph = WeightedSimpleGraph<int>.CreateFromOneBasedEdges(5, new[,]
            {
                { 1, 2, 10 }, { 3, 4, 11 }
            });

            Assert.AreEqual(1, graph.Vertices[0].Degree);
            Assert.AreEqual(1, graph.Vertices[1].Degree);
            Assert.AreEqual(1, graph.Vertices[2].Degree);
            Assert.AreEqual(1, graph.Vertices[3].Degree);
            Assert.AreEqual(0, graph.Vertices[4].Degree);

            Assert.IsTrue(graph.HasEdge(0, 1));
            Assert.IsTrue(graph.HasEdge(2, 3));
            Assert.IsFalse(graph.HasEdge(2, 0));
            Assert.IsFalse(graph.HasEdge(0, 3));
            Assert.IsFalse(graph.HasEdge(1, 2));
            Assert.IsFalse(graph.HasEdge(3, 1));

            for (int i = 0; i <= 4; ++i)
            {
                Assert.IsFalse(graph.HasEdge(4, i));
            }

            Assert.IsFalse(graph.Vertices[0].HasNeighbor(0));
            Assert.IsTrue(graph.Vertices[0].HasNeighbor(1));
            Assert.IsFalse(graph.Vertices[0].HasNeighbor(2));
            Assert.IsFalse(graph.Vertices[0].HasNeighbor(3));
            Assert.IsFalse(graph.Vertices[0].HasNeighbor(4));

            Assert.IsTrue(graph.Vertices[1].HasNeighbor(0));
            Assert.IsFalse(graph.Vertices[1].HasNeighbor(1));
            Assert.IsFalse(graph.Vertices[1].HasNeighbor(2));
            Assert.IsFalse(graph.Vertices[1].HasNeighbor(3));
            Assert.IsFalse(graph.Vertices[1].HasNeighbor(4));

            Assert.IsFalse(graph.Vertices[2].HasNeighbor(0));
            Assert.IsFalse(graph.Vertices[2].HasNeighbor(1));
            Assert.IsFalse(graph.Vertices[2].HasNeighbor(2));
            Assert.IsTrue(graph.Vertices[2].HasNeighbor(3));
            Assert.IsFalse(graph.Vertices[2].HasNeighbor(4));

            Assert.IsFalse(graph.Vertices[3].HasNeighbor(0));
            Assert.IsFalse(graph.Vertices[3].HasNeighbor(1));
            Assert.IsTrue(graph.Vertices[3].HasNeighbor(2));
            Assert.IsFalse(graph.Vertices[3].HasNeighbor(3));
            Assert.IsFalse(graph.Vertices[3].HasNeighbor(4));

            Assert.IsFalse(graph.Vertices[4].HasNeighbor(0));
            Assert.IsFalse(graph.Vertices[4].HasNeighbor(1));
            Assert.IsFalse(graph.Vertices[4].HasNeighbor(2));
            Assert.IsFalse(graph.Vertices[4].HasNeighbor(3));
            Assert.IsFalse(graph.Vertices[4].HasNeighbor(4));

            Assert.AreEqual(10, graph.Vertices[0].GetEdgeWeight(1));
            Assert.AreEqual(11, graph.Vertices[2].GetEdgeWeight(3));
        }

        [TestMethod]
        public void AddsEdges()
        {
            var graph = new WeightedSimpleGraph<int>(5);
            Assert.AreEqual(0, graph.Vertices[0].Degree);
            Assert.AreEqual(0, graph.Vertices[1].Degree);

            graph.AddEdge(0, 1, 99);
            Assert.AreEqual(1, graph.Vertices[0].Degree);
            Assert.AreEqual(1, graph.Vertices[1].Degree);
            Assert.AreEqual(1, graph.Vertices[0].Neighbors.Single().ID);
            Assert.AreEqual(0, graph.Vertices[1].Neighbors.Single().ID);
            Assert.AreEqual(99,  graph.Vertices[0].Neighbors.Single().GetEdgeWeight(0));
            Assert.AreEqual(99, graph.Vertices[1].Neighbors.Single().GetEdgeWeight(1));

            graph.AddEdge(1, 4, 100);
            Assert.AreEqual(1, graph.Vertices[0].Degree);
            Assert.AreEqual(2, graph.Vertices[1].Degree);
            Assert.AreEqual(1, graph.Vertices[4].Degree);
            Assert.AreEqual(1, graph.Vertices[0].Neighbors.Single().ID);
            Assert.AreEqual(99, graph.Vertices[0].Neighbors.Single().GetEdgeWeight(0));
            CollectionAssert.AreEquivalent(new[] { 0, 4 }, graph.Vertices[1].Neighbors.Select(n => n.ID).ToArray());
            CollectionAssert.AreEquivalent(new[] { 99, 100 }, graph.Vertices[1].Neighbors.Select(n => n.GetEdgeWeight(1)).ToArray());
            Assert.AreEqual(1, graph.Vertices[4].Neighbors.Single().ID);
            Assert.AreEqual(100, graph.Vertices[4].Neighbors.Single().GetEdgeWeight(4));
        }

        [TestMethod]
        public void TryGetEdgeWeight()
        {
            var graph = new WeightedSimpleGraph<int>(5);
            int value;
            bool hasValue;

            hasValue = graph.Vertices[0].TryGetEdgeWeight(1, out value);
            Assert.AreEqual(0, value);
            Assert.IsFalse(hasValue);

            graph.AddEdge(0, 3, 99);
            hasValue = graph.Vertices[0].TryGetEdgeWeight(3, out value);
            Assert.AreEqual(99, value);
            Assert.IsTrue(hasValue);
            hasValue = graph.Vertices[3].TryGetEdgeWeight(0, out value);
            Assert.AreEqual(99, value);
            Assert.IsTrue(hasValue);
            hasValue = graph.Vertices[0].TryGetEdgeWeight(1, out value);
            Assert.AreEqual(0, value);
            Assert.IsFalse(hasValue);
        }
    }
}
