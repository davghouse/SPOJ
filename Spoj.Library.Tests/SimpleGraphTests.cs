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
            var graph = SimpleGraph.CreateFromZeroBasedEdges(3, new[,]
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
            var graph = SimpleGraph.CreateFromZeroBasedEdges(5, new[,]
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
        public void ValidatesAGraph3()
        {
            var graph = SimpleGraph.CreateFromOneBasedEdges(3, new[,]
            {
                {1, 2},
                {2, 3}
            });

            Assert.IsTrue(graph.Vertices[0].HasNeighbor(1));
            Assert.IsTrue(graph.Vertices[1].HasNeighbor(0));
            Assert.IsTrue(graph.Vertices[1].HasNeighbor(2));
            Assert.IsTrue(graph.Vertices[2].HasNeighbor(1));

            Assert.IsTrue(graph.HasEdge(0, 1));
            Assert.IsTrue(graph.HasEdge(1, 2));

            Assert.AreEqual(graph.Vertices[0].Degree, 1);
            Assert.AreEqual(graph.Vertices[1].Degree, 2);
            Assert.AreEqual(graph.Vertices[2].Degree, 1);
        }

        [TestMethod]
        public void ChecksIsConnected1()
        {
            // This graph is a triangle.
            var graph = SimpleGraph.CreateFromZeroBasedEdges(3, new[,]
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
            var graph = SimpleGraph.CreateFromZeroBasedEdges(5, new[,]
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
            var graph = SimpleGraph.CreateFromZeroBasedEdges(6, new[,]
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

            var graphWithOneExtra = SimpleGraph.CreateFromZeroBasedEdges(7, new[,]
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
            var graph = SimpleGraph.CreateFromZeroBasedEdges(6, new[,]
            {
                {0, 1},
                {1, 2},
                {2, 3},
                {3, 4},
                {4, 5}
            });

            Assert.IsTrue(graph.IsConnected());

            var graphBrokenInTwo = SimpleGraph.CreateFromZeroBasedEdges(6, new[,]
            {
                {0, 1},
                {1, 2},
                {3, 4},
                {4, 5}
            });

            Assert.IsFalse(graphBrokenInTwo.IsConnected());
        }

        [TestMethod]
        public void ChecksFurthestVertex1()
        {
            // This graph is a point.
            var graph = SimpleGraph.CreateFromZeroBasedEdges(1, new int[,] { });

            Assert.AreEqual(0, graph.FindFurthestVertex(0).Item1.ID);
            Assert.AreEqual(0, graph.FindFurthestVertex(0).Item2);
        }

        [TestMethod]
        public void ChecksFurthestVertex2()
        {
            // This graph is a line.
            var graph = SimpleGraph.CreateFromZeroBasedEdges(6, new[,]
            {
                {0, 1},
                {1, 2},
                {2, 3},
                {3, 4},
                {4, 5}
            });

            Assert.AreEqual(5, graph.FindFurthestVertex(0).Item1.ID);
            Assert.AreEqual(5, graph.FindFurthestVertex(0).Item2);

            Assert.AreEqual(5, graph.FindFurthestVertex(1).Item1.ID);
            Assert.AreEqual(4, graph.FindFurthestVertex(1).Item2);

            Assert.AreEqual(5, graph.FindFurthestVertex(2).Item1.ID);
            Assert.AreEqual(3, graph.FindFurthestVertex(2).Item2);

            Assert.AreEqual(0, graph.FindFurthestVertex(3).Item1.ID);
            Assert.AreEqual(3, graph.FindFurthestVertex(3).Item2);

            Assert.AreEqual(0, graph.FindFurthestVertex(4).Item1.ID);
            Assert.AreEqual(4, graph.FindFurthestVertex(4).Item2);

            Assert.AreEqual(0, graph.FindFurthestVertex(5).Item1.ID);
            Assert.AreEqual(5, graph.FindFurthestVertex(5).Item2);
        }

        [TestMethod]
        public void ChecksFurthestVertex3()
        {
            // This graph is a line broken in two.
            var graph = SimpleGraph.CreateFromZeroBasedEdges(6, new[,]
            {
                {0, 1},
                {1, 2},
                {3, 4},
                {4, 5}
            });

            Assert.AreEqual(2, graph.FindFurthestVertex(0).Item1.ID);
            Assert.AreEqual(2, graph.FindFurthestVertex(0).Item2);

            Assert.IsTrue(graph.FindFurthestVertex(1).Item1.ID == 2
                || graph.FindFurthestVertex(1).Item1.ID == 0);
            Assert.AreEqual(1, graph.FindFurthestVertex(1).Item2);

            Assert.AreEqual(0, graph.FindFurthestVertex(2).Item1.ID);
            Assert.AreEqual(2, graph.FindFurthestVertex(2).Item2);

            Assert.AreEqual(5, graph.FindFurthestVertex(3).Item1.ID);
            Assert.AreEqual(2, graph.FindFurthestVertex(3).Item2);

            Assert.IsTrue(graph.FindFurthestVertex(4).Item1.ID == 3
                || graph.FindFurthestVertex(4).Item1.ID == 5);
            Assert.AreEqual(1, graph.FindFurthestVertex(4).Item2);

            Assert.AreEqual(3, graph.FindFurthestVertex(5).Item1.ID);
            Assert.AreEqual(2, graph.FindFurthestVertex(5).Item2);
        }

        [TestMethod]
        public void ChecksFurthestVertex4()
        {
            // This graph is complete with four vertices.
            var graph = SimpleGraph.CreateFromZeroBasedEdges(4, new[,]
            {
                {0, 1},
                {1, 2},
                {2, 3},
                {3, 0},
                {0, 2},
                {3, 1}
            });

            Assert.AreEqual(1, graph.FindFurthestVertex(0).Item2);
            Assert.AreEqual(1, graph.FindFurthestVertex(1).Item2);
            Assert.AreEqual(1, graph.FindFurthestVertex(2).Item2);
            Assert.AreEqual(1, graph.FindFurthestVertex(3).Item2);
        }

        [TestMethod]
        public void ChecksFurthestVertex5()
        {
            // This graph is a weird mess: http://i.imgur.com/kbeOrqA.png.
            var graph = SimpleGraph.CreateFromZeroBasedEdges(9, new[,]
            {
                {0, 1},
                {0, 3},
                {0, 4},
                {0, 5},
                {1, 2},
                {1, 3},
                {1, 6},
                {2, 3},
                {2, 4},
                {3, 4},
                {4, 5},
                {4, 8},
                {5, 7},
            });

            Assert.AreEqual(2, graph.FindFurthestVertex(0).Item2);
            Assert.AreEqual(3, graph.FindFurthestVertex(1).Item2);
            Assert.AreEqual(3, graph.FindFurthestVertex(2).Item2);
            Assert.AreEqual(3, graph.FindFurthestVertex(3).Item2);
            Assert.AreEqual(3, graph.FindFurthestVertex(4).Item2);
            Assert.AreEqual(3, graph.FindFurthestVertex(5).Item2);
            Assert.AreEqual(4, graph.FindFurthestVertex(6).Item2);
            Assert.AreEqual(4, graph.FindFurthestVertex(7).Item2);
            Assert.AreEqual(4, graph.FindFurthestVertex(8).Item2);
        }
    }
}
