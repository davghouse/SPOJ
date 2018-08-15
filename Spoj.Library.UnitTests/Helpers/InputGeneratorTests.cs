using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spoj.Library.Helpers;
using System.Linq;

namespace Spoj.Library.UnitTests.Helpers
{
    [TestClass]
    public class InputGeneratorTests
    {
        [TestMethod]
        public void DoesntGenerateDistinctRandomInts()
        {
            int[] ints = InputGenerator.GenerateRandomInts(1000, 1, 999);

            Assert.IsTrue(!ints.SequenceEqual(ints.Distinct()));
        }

        [TestMethod]
        public void GenerateDistinctRandomInts()
        {
            int[] ints = InputGenerator.GenerateDistinctRandomInts(1000, 1, 1000);

            Assert.IsTrue(ints.SequenceEqual(ints.Distinct()));
        }

        [TestMethod]
        public void GenerateRandomStrings()
        {
            string s = InputGenerator.GenerateRandomString(1000);

            Assert.AreEqual(1000, s.Length);
            Assert.IsTrue(s.All(c => c >= 'a'));
            Assert.IsTrue(s.All(c => c <= 'z'));

            s = InputGenerator.GenerateRandomString(1000, 'a', 'b');

            Assert.AreEqual(1000, s.Length);
            Assert.IsTrue(s.All(c => c >= 'a'));
            Assert.IsTrue(s.All(c => c <= 'b'));
            Assert.IsTrue(s.Any(c => c == 'a'));
            Assert.IsTrue(s.Any(c => c == 'b'));
        }

        [TestMethod]
        public void GenerateEvenOddPairs()
        {
            int[,] evenOddPairs = InputGenerator.GenerateRandomEvenOddPairs(1000, 1, 100);

            Assert.AreEqual(1000, evenOddPairs.GetLength(0));
            for (int p = 0; p < 1000; ++p)
            {
                int even = evenOddPairs[p, 0];
                int odd = evenOddPairs[p, 1];

                Assert.IsTrue(even % 2 == 0);
                Assert.IsTrue(odd % 2 == 1);
                Assert.IsTrue(even >= 1 && even <= 1000);
                Assert.IsTrue(odd >= 1 && odd <= 1000);
            }

            evenOddPairs = InputGenerator.GenerateRandomEvenOddPairs(1000, 0, 1);

            Assert.AreEqual(1000, evenOddPairs.GetLength(0));
            for (int p = 0; p < 1000; ++p)
            {
                int even = evenOddPairs[p, 0];
                int odd = evenOddPairs[p, 1];

                Assert.AreEqual(0, even);
                Assert.AreEqual(1, odd);
            }
        }

        [TestMethod]
        public void GenerateMinMaxPairs()
        {
            int[,] minMaxPairs = InputGenerator.GenerateRandomMinMaxPairs(1000, 1, 1000);

            Assert.AreEqual(1000, minMaxPairs.GetLength(0));
            for (int p = 0; p < 1000; ++p)
            {
                int min = minMaxPairs[p, 0];
                int max = minMaxPairs[p, 1];

                Assert.IsTrue(min <= max);
                Assert.IsTrue(min >= 1 && min <= 1000);
                Assert.IsTrue(max >= 1 && max <= 1000);
            }

            minMaxPairs = InputGenerator.GenerateRandomMinMaxPairs(1000, 0, 1);

            Assert.AreEqual(1000, minMaxPairs.GetLength(0));
            for (int p = 0; p < 1000; ++p)
            {
                int min = minMaxPairs[p, 0];
                int max = minMaxPairs[p, 1];

                Assert.IsTrue(min <= max);
                Assert.IsTrue(min >= 0 && min <= 1);
                Assert.IsTrue(max >= 0 && max <= 1);
            }
        }

        [TestMethod]
        public void GenerateRandomRootedTrees()
        {
            var lineTree = InputGenerator.GenerateRandomRootedTree(5, 1, 1);
            Assert.AreEqual(0, lineTree.Root.ID);
            Assert.AreEqual(5, lineTree.VertexCount);
            Assert.AreEqual(1, lineTree.Vertices[0].Children.Count);
            Assert.AreEqual(1, lineTree.Vertices[1].Children.Count);
            Assert.AreEqual(1, lineTree.Vertices[2].Children.Count);
            Assert.AreEqual(1, lineTree.Vertices[3].Children.Count);
            Assert.AreEqual(0, lineTree.Vertices[4].Children.Count);
            Assert.AreEqual(3, lineTree.Vertices[2].Children[0].ID);

            var binaryTree = InputGenerator.GenerateRandomRootedTree(6, 2, 2);
            Assert.AreEqual(0, binaryTree.Root.ID);
            Assert.AreEqual(6, binaryTree.VertexCount);
            Assert.AreEqual(2, binaryTree.Vertices[0].Children.Count);
            Assert.AreEqual(2, binaryTree.Vertices[1].Children.Count);
            Assert.AreEqual(1, binaryTree.Vertices[2].Children.Count);
            Assert.AreEqual(0, binaryTree.Vertices[3].Children.Count);
            Assert.AreEqual(0, binaryTree.Vertices[4].Children.Count);
            Assert.AreEqual(0, binaryTree.Vertices[5].Children.Count);
            Assert.AreEqual(5, binaryTree.Vertices[2].Children[0].ID);

            var starTree = InputGenerator.GenerateRandomRootedTree(6, 5, 10);
            Assert.AreEqual(0, starTree.Root.ID);
            Assert.AreEqual(6, starTree.VertexCount);
            Assert.AreEqual(5, starTree.Vertices[0].Children.Count);
            Assert.AreEqual(0, starTree.Vertices[1].Children.Count);
            Assert.AreEqual(0, starTree.Vertices[2].Children.Count);
            Assert.AreEqual(0, starTree.Vertices[3].Children.Count);
            Assert.AreEqual(0, starTree.Vertices[4].Children.Count);
            Assert.AreEqual(0, starTree.Vertices[5].Children.Count);

            var randomTree = InputGenerator.GenerateRandomRootedTree(100, 1, 3);
            randomTree.InitializeDepthsAndSubtreeSizes();
            Assert.IsTrue(randomTree.Vertices
                .All(v => v.Children.All(c => c.Depth == v.Depth + 1)));
            Assert.IsTrue(randomTree.Vertices
                .All(v => randomTree.Vertices.Where(v2 => v2.ID > v.ID).All(v2 => v2.Depth >= v.Depth)));
        }
    }
}
