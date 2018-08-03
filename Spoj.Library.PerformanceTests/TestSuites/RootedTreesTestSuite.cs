using Spoj.Library.Graphs;
using System.Collections.Generic;

namespace Spoj.Library.PerformanceTests.TestSuites
{
    public class RootedTreesTestSuite : ITestSuite
    {
        private readonly RootedTree[] _randomTrees1ForStack;
        private readonly RootedTree[] _randomTrees1ForRecursion;
        private readonly RootedTree[] _randomTrees2ForStack;
        private readonly RootedTree[] _randomTrees2ForRecursion;

        private static RootedTree[] GetRandomTrees(int count, int vertexCount, int minChildCount, int maxChildCount)
        {
            var randomTrees = new RootedTree[count];
            for (int i = 0; i < count; ++i)
            {
                randomTrees[i] = InputGenerator.GenerateRandomRootedTree(vertexCount, minChildCount, maxChildCount);
            }

            return randomTrees;
        }

        public RootedTreesTestSuite()
        {
            _randomTrees1ForStack = GetRandomTrees(100, 10000, 1, 2);
            _randomTrees1ForRecursion = GetRandomTrees(100, 10000, 1, 2);
            _randomTrees2ForStack = GetRandomTrees(100, 10000, 1, 10);
            _randomTrees2ForRecursion = GetRandomTrees(100, 10000, 1, 10);
        }

        public IEnumerable<TestScenario> TestScenarios => new TestScenario[]
        {
            new TestScenario("Euler tours on 100 trees, size 10000", new TestCase[]
                {
                    new TestCase("Stack, 1 to 2 children", () => EulerTour(_randomTrees1ForStack, useStack: true)),
                    new TestCase("Recursion, 1 to 2 children", () => EulerTour(_randomTrees1ForRecursion, useStack: false)),
                    new TestCase("Stack, 1 to 10 children", () => EulerTour(_randomTrees2ForStack, useStack: true)),
                    new TestCase("Recursion, 1 to 10 children", () => EulerTour(_randomTrees2ForRecursion, useStack: false))
                }),
        };

        private void EulerTour(RootedTree[] randomTrees, bool useStack)
        {
            foreach (var tree in randomTrees)
            {
                var eulerTour = useStack
                    ? tree.GetEulerTourUsingStack()
                    : tree.GetEulerTourUsingRecursion();
            }
        }
    }
}
