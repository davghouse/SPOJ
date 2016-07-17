using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Spoj.Library.Tests
{
    [TestClass]
    public class SimpleGraphTests
    {
        [TestMethod]
        public void ValidatesAGraph1()
        {
            // This graph is a triangle.
            var graph = SimpleGraph.Create(3, new[,]
            {
                {0, 1},
                {0, 2},
                {1, 2}
            });

            Assert.AreEqual(graph.Vertices[0].Degree, 2);
            Assert.AreEqual(graph.Vertices[1].Degree, 2);
            Assert.AreEqual(graph.Vertices[2].Degree, 2);

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
        }

        [TestMethod]
        public void ValidatesAGraph2()
        {
            // This graph is two lines and a point.
            var graph = SimpleGraph.Create(5, new[,]
            {
                {0, 1},
                {2, 3}
            });

            Assert.AreEqual(graph.Vertices[0].Degree, 1);
            Assert.AreEqual(graph.Vertices[1].Degree, 1);
            Assert.AreEqual(graph.Vertices[2].Degree, 1);
            Assert.AreEqual(graph.Vertices[3].Degree, 1);
            Assert.AreEqual(graph.Vertices[4].Degree, 0);

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
        }

        [TestMethod]
        public void ChecksIsConnected1()
        {
            // This graph is a triangle.
            var graph = SimpleGraph.Create(3, new[,]
            {
                {0, 1},
                {0, 2},
                {1, 2}
            });

            Assert.IsTrue(graph.IsConnected());
        }

        [TestMethod]
        public void ChecksIsConnected2()
        {
            // This graph is two lines and a point.
            var graph = SimpleGraph.Create(5, new[,]
            {
                {0, 1},
                {2, 3}
            });

            Assert.IsFalse(graph.IsConnected());
        }

        [TestMethod]
        public void ChecksIsConnected3()
        {
            // This graph is B-shaped.
            var graph = SimpleGraph.Create(6, new[,]
            {
                {0, 1},
                {1, 2},
                {2, 3},
                {3, 4},
                {4, 5},
                {5, 0},
                {2, 5}
            });

            Assert.IsTrue(graph.IsConnected());

            var graphWithOneExtra = SimpleGraph.Create(7, new[,]
            {
                {0, 1},
                {1, 2},
                {2, 3},
                {3, 4},
                {4, 5},
                {5, 0},
                {2, 5}
            });

            Assert.IsFalse(graphWithOneExtra.IsConnected());
        }

        [TestMethod]
        public void ChecksIsConnected4()
        {
            // This graph is a line.
            var graph = SimpleGraph.Create(6, new[,]
            {
                {0, 1},
                {1, 2},
                {2, 3},
                {3, 4},
                {4, 5}
            });

            Assert.IsTrue(graph.IsConnected());

            var graphBrokenInTwo = SimpleGraph.Create(6, new[,]
            {
                {0, 1},
                {1, 2},
                {3, 4},
                {4, 5}
            });

            Assert.IsFalse(graphBrokenInTwo.IsConnected());
        }
    }
}
