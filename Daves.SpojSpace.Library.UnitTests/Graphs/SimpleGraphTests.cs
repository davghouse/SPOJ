using Daves.SpojSpace.Library.Graphs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Daves.SpojSpace.Library.UnitTests.Graphs
{
    [TestClass]
    public class SimpleGraphTests
    {
        [TestMethod]
        public void ValidatesAGraph1()
        {
            // This graph is a triangle.
            SimpleGraph graph = SimpleGraph.CreateFromZeroBasedEdges(3, new[,]
            {
                { 0, 1 }, { 0, 2 }, { 1, 2 }
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
        }

        [TestMethod]
        public void ValidatesAGraph2()
        {
            // This graph is two lines and a point.
            SimpleGraph graph = SimpleGraph.CreateFromZeroBasedEdges(5, new[,]
            {
                { 0, 1 }, { 2, 3 }
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
        }

        [TestMethod]
        public void ValidatesAGraph3()
        {
            SimpleGraph graph = SimpleGraph.CreateFromOneBasedEdges(3, new[,]
            {
                { 1, 2 }, { 2, 3 }
            });

            Assert.IsTrue(graph.Vertices[0].HasNeighbor(1));
            Assert.IsTrue(graph.Vertices[1].HasNeighbor(0));
            Assert.IsTrue(graph.Vertices[1].HasNeighbor(2));
            Assert.IsTrue(graph.Vertices[2].HasNeighbor(1));

            Assert.IsTrue(graph.HasEdge(0, 1));
            Assert.IsTrue(graph.HasEdge(1, 2));

            Assert.AreEqual(1, graph.Vertices[0].Degree);
            Assert.AreEqual(2, graph.Vertices[1].Degree);
            Assert.AreEqual(1, graph.Vertices[2].Degree);
        }

        [TestMethod]
        public void IsConnected1()
        {
            // This graph is a triangle.
            SimpleGraph graph = SimpleGraph.CreateFromZeroBasedEdges(3, new[,]
            {
                { 0, 1 }, { 0, 2 }, { 1, 2 }
            });

            Assert.IsTrue(graph.IsConnected());
        }

        [TestMethod]
        public void IsConnected2()
        {
            // This graph is two lines and a point.
            SimpleGraph graph = SimpleGraph.CreateFromZeroBasedEdges(5, new[,]
            {
                { 0, 1 }, { 2, 3 }
            });

            Assert.IsFalse(graph.IsConnected());
        }

        [TestMethod]
        public void IsConnected3()
        {
            // This graph is B-shaped.
            SimpleGraph graph = SimpleGraph.CreateFromZeroBasedEdges(6, new[,]
            {
                { 0, 1 }, { 1, 2 }, { 2, 3 }, { 3, 4 }, { 4, 5 }, { 5, 0 }, { 2, 5 }
            });

            Assert.IsTrue(graph.IsConnected());

            SimpleGraph graphWithOneExtra = SimpleGraph.CreateFromZeroBasedEdges(7, new[,]
            {
                { 0, 1 }, { 1, 2 }, { 2, 3 }, { 3, 4 }, { 4, 5 }, { 5, 0 }, { 2, 5 }
            });

            Assert.IsFalse(graphWithOneExtra.IsConnected());
        }

        [TestMethod]
        public void IsConnected4()
        {
            // This graph is a line.
            SimpleGraph graph = SimpleGraph.CreateFromZeroBasedEdges(6, new[,]
            {
                { 0, 1 }, { 1, 2 }, { 2, 3 }, { 3, 4 }, { 4, 5 }
            });

            Assert.IsTrue(graph.IsConnected());

            SimpleGraph graphBrokenInTwo = SimpleGraph.CreateFromZeroBasedEdges(6, new[,]
            {
                { 0, 1 }, { 1, 2 }, { 3, 4 }, { 4, 5 }
            });

            Assert.IsFalse(graphBrokenInTwo.IsConnected());
        }

        [TestMethod]
        public void FindFurthestVertex1()
        {
            // This graph is a point.
            SimpleGraph graph = SimpleGraph.CreateFromZeroBasedEdges(1, new int[,] { });

            Assert.AreEqual(0, graph.FindFurthestVertex(0).Item1.ID);
            Assert.AreEqual(0, graph.FindFurthestVertex(0).Item2);
        }

        [TestMethod]
        public void FindFurthestVertex2()
        {
            // This graph is a line.
            SimpleGraph graph = SimpleGraph.CreateFromZeroBasedEdges(6, new[,]
            {
                { 0, 1 }, { 1, 2 }, { 2, 3 }, { 3, 4 }, { 4, 5 }
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
        public void FindFurthestVertex3()
        {
            // This graph is a line broken in two.
            SimpleGraph graph = SimpleGraph.CreateFromZeroBasedEdges(6, new[,]
            {
                { 0, 1 }, { 1, 2 }, { 3, 4 }, { 4, 5 }
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
        public void FindFurthestVertex4()
        {
            // This graph is complete with four vertices.
            SimpleGraph graph = SimpleGraph.CreateFromZeroBasedEdges(4, new[,]
            {
                { 0, 1 }, { 1, 2 }, { 2, 3 }, { 3, 0 }, { 0, 2 }, { 3, 1 }
            });

            Assert.AreEqual(1, graph.FindFurthestVertex(0).Item2);
            Assert.AreEqual(1, graph.FindFurthestVertex(1).Item2);
            Assert.AreEqual(1, graph.FindFurthestVertex(2).Item2);
            Assert.AreEqual(1, graph.FindFurthestVertex(3).Item2);
        }

        [TestMethod]
        public void FindFurthestVertex5()
        {
            // This graph is a weird mess: http://i.imgur.com/kbeOrqA.png.
            SimpleGraph graph = SimpleGraph.CreateFromZeroBasedEdges(9, new[,]
            {
                { 0, 1 }, { 0, 3 }, { 0, 4 }, { 0, 5 }, { 1, 2 }, { 1, 3 }, { 1, 6 },
                { 2, 3 }, { 2, 4 }, { 3, 4 }, { 4, 5 }, { 4, 8 }, { 5, 7 }
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

        [TestMethod]
        public void IsBipartite_ForSmallGraphs()
        {
            // This graph is empty.
            SimpleGraph graph = SimpleGraph.CreateFromZeroBasedEdges(0, new int[,] { });
            Assert.IsTrue(graph.IsBipartite());

            // This graph has a single vertex.
            graph = SimpleGraph.CreateFromZeroBasedEdges(1, new int[,] { });
            Assert.IsTrue(graph.IsBipartite());

            // This graph has two vertices.
            graph = SimpleGraph.CreateFromZeroBasedEdges(2, new int[,] { });
            Assert.IsTrue(graph.IsBipartite());

            // This graph has two vertices connected by an edge.
            graph = SimpleGraph.CreateFromZeroBasedEdges(2, new[,]
            {
                { 0, 1 }
            });
            Assert.IsTrue(graph.IsBipartite());

            // This graph has 6 vertices, with pairs connected.
            graph = SimpleGraph.CreateFromZeroBasedEdges(6, new[,]
            {
                { 0, 1 }, { 2, 3 }, { 4, 5 }
            });
            Assert.IsTrue(graph.IsBipartite());

            // This graph has 6 vertices, connected in a line.
            graph = SimpleGraph.CreateFromZeroBasedEdges(6, new[,]
            {
                { 0, 1 }, { 1, 2 }, { 2, 3 }, { 3, 4 }, { 4, 5 }
            });
            Assert.IsTrue(graph.IsBipartite());

            // This graph has 6 vertices, connected in a circle.
            graph = SimpleGraph.CreateFromZeroBasedEdges(6, new[,]
            {
                { 0, 1 }, { 1, 2 }, { 2, 3 }, { 3, 4 }, { 4, 5 }, { 5, 0 }
            });
            Assert.IsTrue(graph.IsBipartite());

            // This graph has 7 vertices, connected in a circle.
            graph = SimpleGraph.CreateFromZeroBasedEdges(7, new[,]
            {
                { 0, 1 }, { 1, 2 }, { 2, 3 }, { 3, 4 }, { 4, 5 }, { 5, 6 }, { 6, 0 }
            });
            Assert.IsFalse(graph.IsBipartite());

            // This graph has 9 vertices, 6 connected in a line, 3 in a triangle.
            graph = SimpleGraph.CreateFromZeroBasedEdges(9, new[,]
            {
                { 0, 1 }, { 1, 2 }, { 2, 3 }, { 3, 4 }, { 4, 5 }, { 6, 7 }, { 7, 8 }, { 8, 6 }
            });
            Assert.IsFalse(graph.IsBipartite());

            // This graph has 12 vertices, 6 connected in a line, 4 in a square, 2 in a line.
            graph = SimpleGraph.CreateFromZeroBasedEdges(12, new[,]
            {
                { 0, 1 }, { 1, 2 }, { 2, 3 }, { 3, 4 }, { 4, 5 }, { 6, 7 }, { 7, 8 }, { 8, 9 }, { 9, 6 }, { 10, 11 }
            });
            Assert.IsTrue(graph.IsBipartite());

            // This graph has 7 vertices, 2 connected in a line, 2 in a line, 3 in a triangle
            graph = SimpleGraph.CreateFromZeroBasedEdges(12, new[,]
            {
                { 0, 1 }, { 2, 3 }, { 4, 5 }, { 5, 6 }, { 6, 4 }
            });
            Assert.IsFalse(graph.IsBipartite());
        }

        [TestMethod]
        public void IsBipartite_ForBiggerGraphs()
        {
            // This graph is some bipartite mess. Odds and evens go in their own set, edges between them haphazardly.
            SimpleGraph graph = SimpleGraph.CreateFromOneBasedEdges(22, new int[,]
            {
                { 2, 3 }, { 2, 9 }, { 2, 11 }, { 2, 15 },
                { 4, 1 }, { 4, 7 }, { 4, 11 }, { 4, 17 },
                { 6, 1 }, { 6, 11 }, { 6, 7 }, { 6, 15 },
                { 8, 1 }, { 8, 15 }, { 8, 13 }, { 8, 17 },
                { 10, 1 }, { 10, 3 }, { 10, 13 }, { 10, 15 },
                { 14, 1 }, { 16, 3 }, { 16, 7 }, { 16, 17 },
                { 16, 1 }, { 16, 3 }, { 16, 7 }, { 16, 17 },
                { 16, 9 }, { 16, 11 }, { 16, 5 }, { 16, 19 },
                { 18, 9 }, { 18, 11 }, { 18, 5 }, { 18, 19 },
                { 20, 21 }
            });
            Assert.IsTrue(graph.IsBipartite());

            // This graph is like above, except I threw in an edge between an even pair of vertices.
            graph = SimpleGraph.CreateFromOneBasedEdges(22, new int[,]
            {
                { 2, 3 }, { 2, 9 }, { 2, 11 }, { 2, 15 },
                { 4, 1 }, { 4, 7 }, { 4, 11 }, { 4, 17 },
                { 6, 1 }, { 6, 11 }, { 6, 7 }, { 6, 15 },
                { 8, 1 }, { 8, 15 }, { 8, 13 }, { 8, 17 },
                { 10, 1 }, { 10, 3 }, { 10, 13 }, { 10, 15 },
                { 14, 1 }, { 16, 3 }, { 16, 7 }, { 16, 17 },
                { 16, 1 }, { 16, 3 }, { 16, 7 }, { 16, 17 },
                { 16, 9 }, { 16, 11 }, { 16, 5 }, { 16, 19 },
                { 18, 9 }, { 18, 11 }, { 18, 5 }, { 18, 19 },
                { 20, 21 }, {10, 16 }
            });
            Assert.IsFalse(graph.IsBipartite());
        }

        [TestMethod]
        public void GetShortestPathLength_ForSmallGraphs()
        {
            // This graph has a single vertex.
            SimpleGraph graph = SimpleGraph.CreateFromZeroBasedEdges(1, new int[,] { });
            Assert.AreEqual(0, graph.GetShortestPathLength(0, 0));

            // This graph has two vertices.
            graph = SimpleGraph.CreateFromZeroBasedEdges(2, new int[,] { });
            Assert.AreEqual(0, graph.GetShortestPathLength(0, 0));
            Assert.AreEqual(0, graph.GetShortestPathLength(1, 1));
            Assert.AreEqual(-1, graph.GetShortestPathLength(1, 0));

            // This graph has two vertices connected by an edge.
            graph = SimpleGraph.CreateFromZeroBasedEdges(2, new[,]
            {
                { 0, 1 }
            });
            Assert.AreEqual(0, graph.GetShortestPathLength(1, 1));
            Assert.AreEqual(1, graph.GetShortestPathLength(0, 1));

            // This graph has 6 vertices, with pairs connected.
            graph = SimpleGraph.CreateFromZeroBasedEdges(6, new[,]
            {
                { 0, 1 }, { 2, 3 }, { 4, 5 }
            });
            Assert.AreEqual(1, graph.GetShortestPathLength(0, 1));
            Assert.AreEqual(1, graph.GetShortestPathLength(2, 3));
            Assert.AreEqual(1, graph.GetShortestPathLength(4, 5));
            Assert.AreEqual(-1, graph.GetShortestPathLength(3, 5));

            // This graph has 6 vertices, connected in a line.
            graph = SimpleGraph.CreateFromZeroBasedEdges(6, new[,]
            {
                { 0, 1 }, { 1, 2 }, { 2, 3 }, { 3, 4 }, { 4, 5 }
            });
            for (int i = 0; i <= 5; ++i)
            {
                for (int j = 0; j <= 5; ++j)
                {
                    Assert.AreEqual(Math.Abs(i - j), graph.GetShortestPathLength(i, j));
                }
            }

            // This graph has 6 vertices, connected in a circle.
            graph = SimpleGraph.CreateFromZeroBasedEdges(6, new[,]
            {
                { 0, 1 }, { 1, 2 }, { 2, 3 }, { 3, 4 }, { 4, 5 }, { 5, 0 }
            });
            Assert.AreEqual(1, graph.GetShortestPathLength(3, 2));
            Assert.AreEqual(1, graph.GetShortestPathLength(5, 0));
            Assert.AreEqual(2, graph.GetShortestPathLength(5, 1));
            Assert.AreEqual(2, graph.GetShortestPathLength(2, 4));

            // This graph has 9 vertices, 6 connected in a line, 3 in a triangle.
            graph = SimpleGraph.CreateFromZeroBasedEdges(9, new[,]
            {
                { 0, 1 }, { 1, 2 }, { 2, 3 }, { 3, 4 }, { 4, 5 }, { 6, 7 }, { 7, 8 }, { 8, 6 }
            });
            for (int i = 0; i <= 5; ++i)
            {
                for (int j = 0; j <= 5; ++j)
                {
                    Assert.AreEqual(Math.Abs(i - j), graph.GetShortestPathLength(i, j));
                }
            }
            Assert.AreEqual(-1, graph.GetShortestPathLength(2, 6));
            Assert.AreEqual(1, graph.GetShortestPathLength(6, 7));
            Assert.AreEqual(1, graph.GetShortestPathLength(6, 8));
            Assert.AreEqual(1, graph.GetShortestPathLength(7, 8));

            // This graph has 12 vertices, 6 connected in a line, 4 in a square, 2 in a line.
            graph = SimpleGraph.CreateFromZeroBasedEdges(12, new[,]
            {
                { 0, 1 }, { 1, 2 }, { 2, 3 }, { 3, 4 }, { 4, 5 }, { 6, 7 }, { 7, 8 }, { 8, 9 }, { 9, 6 }, { 10, 11 }
            });
            Assert.AreEqual(1, graph.GetShortestPathLength(10, 11));
            Assert.AreEqual(2, graph.GetShortestPathLength(6, 8));
            Assert.AreEqual(-1, graph.GetShortestPathLength(6, 3));
            Assert.AreEqual(3, graph.GetShortestPathLength(4, 1));
        }

        [TestMethod]
        public void AddsEdges()
        {
            SimpleGraph graph = new SimpleGraph(5);
            Assert.AreEqual(0, graph.Vertices[0].Degree);
            Assert.AreEqual(0, graph.Vertices[1].Degree);

            graph.AddEdge(0, 1);
            Assert.AreEqual(1, graph.Vertices[0].Degree);
            Assert.AreEqual(1, graph.Vertices[1].Degree);
            Assert.AreEqual(1, graph.Vertices[0].Neighbors.Single().ID);
            Assert.AreEqual(0, graph.Vertices[1].Neighbors.Single().ID);

            graph.AddEdge(1, 4);
            Assert.AreEqual(1, graph.Vertices[0].Degree);
            Assert.AreEqual(2, graph.Vertices[1].Degree);
            Assert.AreEqual(1, graph.Vertices[4].Degree);
            Assert.AreEqual(1, graph.Vertices[0].Neighbors.Single().ID);
            CollectionAssert.AreEquivalent(new[] { 0, 4 }, graph.Vertices[1].Neighbors.Select(n => n.ID).ToArray());
            Assert.AreEqual(1, graph.Vertices[4].Neighbors.Single().ID);
        }
    }
}
