using Spoj.Library.SegmentTrees;
using Spoj.Library.SegmentTrees.QueryValues;
using System;
using System.Collections.Generic;

namespace Spoj.Library.PerformanceTests.TestSuites
{
    public class SegmentTreeTestSuite : ITestSuite
    {
        private readonly IReadOnlyList<int> _allRangesArray;
        private readonly IReadOnlyList<int> _rightEndingRangesArray;
        private readonly IReadOnlyList<int> _randomRangesArray;
        private const int _randomRangesCount = 50000;

        public SegmentTreeTestSuite()
        {
            _allRangesArray = InputGenerator.GenerateRandomInts(1000, -200, 200);
            _rightEndingRangesArray = InputGenerator.GenerateRandomInts(50000, -300, 300);
            _randomRangesArray = InputGenerator.GenerateRandomInts(50000, -15007, 15007);
        }

        public IReadOnlyList<TestScenario> TestScenarios => new TestScenario[]
        {
            new TestScenario($"All ranges, array size {_allRangesArray.Count}, minimum query", new TestCase[]
                {
                    new TestCase("Naive", NaiveMinimumQuery),
                    new TestCase("Node-based", NodeBasedMinimumQuery),
                    new TestCase("Array-based", ArrayBasedMinimumQuery),
                    new TestCase("Non-recursive", NonRecursiveMinimumQuery),
                }),
            new TestScenario($"All ranges, array size {_allRangesArray.Count}, maximum sum query", new TestCase[]
                {
                    new TestCase("Naive", NaiveMaximumSumQuery),
                    new TestCase("Node-based", NodeBasedMaximumSumQuery),
                    new TestCase("Array-based", ArrayBasedMaximumSumQuery),
                    new TestCase("Non-recursive", NonRecursiveMaximumSumQuery),
                }),
            new TestScenario($"Right-ending ranges, array size {_rightEndingRangesArray.Count}, minimum query", new TestCase[]
                {
                    new TestCase("Naive", NaiveMinimumQueryOnRightEndingRanges),
                    new TestCase("Node-based", NodeBasedMinimumQueryOnRightEndingRanges),
                    new TestCase("Array-based", ArrayBasedMinimumQueryOnRightEndingRanges),
                    new TestCase("Non-recursive", NonRecursiveMinimumQueryOnRightEndingRanges),
                }),
            new TestScenario($"Right-ending ranges, array size {_rightEndingRangesArray.Count}, maximum sum query", new TestCase[]
                {
                    new TestCase("Naive", NaiveMaximumSumQueryOnRightEndingRanges),
                    new TestCase("Node-based", NodeBasedMaximumSumQueryOnRightEndingRanges),
                    new TestCase("Array-based", ArrayBasedMaximumSumQueryOnRightEndingRanges),
                    new TestCase("Non-recursive", NonRecursiveMaximumSumQueryOnRightEndingRanges),
                }),
            new TestScenario($"{_randomRangesCount} random ranges, array size {_randomRangesArray.Count}, minimum query", new TestCase[]
                {
                    new TestCase("Naive", NaiveMinimumQueryOnRandomRanges),
                    new TestCase("Node-based", NodeBasedMinimumQueryOnRandomRanges),
                    new TestCase("Array-based", ArrayBasedMinimumQueryOnRandomRanges),
                    new TestCase("Non-recursive", NonRecursiveMinimumQueryOnRandomRanges),
                }),
            new TestScenario($"{_randomRangesCount} random ranges, array size {_randomRangesArray.Count}, maximum sum query", new TestCase[]
                {
                    new TestCase("Naive", NaiveMaximumSumQueryOnRandomRanges),
                    new TestCase("Node-based", NodeBasedMaximumSumQueryOnRandomRanges),
                    new TestCase("Array-based", ArrayBasedMaximumSumQueryOnRandomRanges),
                    new TestCase("Non-recursive", NonRecursiveMaximumSumQueryOnRandomRanges),
                })
        };

        private void NaiveMinimumQuery()
        {
            for (int i = 0; i < _allRangesArray.Count; ++i)
            {
                for (int j = i; j < _allRangesArray.Count; ++j)
                {
                    NaiveSegmentTreeAlternatives.MinimumQuery(_allRangesArray, i, j);
                }
            }
        }

        private void NodeBasedMinimumQuery()
        {
            var segmentTree = new NodeBasedSegmentTree<MinimumQueryValue>(_allRangesArray);

            for (int i = 0; i < _allRangesArray.Count; ++i)
            {
                for (int j = i; j < _allRangesArray.Count; ++j)
                {
                    segmentTree.Query(i, j);
                }
            }
        }

        private void ArrayBasedMinimumQuery()
        {
            var segmentTree = new ArrayBasedSegmentTree<MinimumQueryValue>(_allRangesArray);

            for (int i = 0; i < _allRangesArray.Count; ++i)
            {
                for (int j = i; j < _allRangesArray.Count; ++j)
                {
                    segmentTree.Query(i, j);
                }
            }
        }

        private void NonRecursiveMinimumQuery()
        {
            var segmentTree = new NonRecursiveSegmentTree<MinimumQueryValue>(_allRangesArray);

            for (int i = 0; i < _allRangesArray.Count; ++i)
            {
                for (int j = i; j < _allRangesArray.Count; ++j)
                {
                    segmentTree.Query(i, j);
                }
            }
        }

        private void NaiveMinimumQueryOnRightEndingRanges()
        {
            for (int i = 0; i < _rightEndingRangesArray.Count; ++i)
            {
                NaiveSegmentTreeAlternatives.MinimumQuery(_rightEndingRangesArray, i, _rightEndingRangesArray.Count - 1);
            }
        }

        private void NodeBasedMinimumQueryOnRightEndingRanges()
        {
            var segmentTree = new NodeBasedSegmentTree<MinimumQueryValue>(_rightEndingRangesArray);

            for (int i = 0; i < _rightEndingRangesArray.Count; ++i)
            {
                segmentTree.Query(i, _rightEndingRangesArray.Count - 1);
            }
        }

        private void ArrayBasedMinimumQueryOnRightEndingRanges()
        {
            var segmentTree = new ArrayBasedSegmentTree<MinimumQueryValue>(_rightEndingRangesArray);

            for (int i = 0; i < _rightEndingRangesArray.Count; ++i)
            {
                segmentTree.Query(i, _rightEndingRangesArray.Count - 1);
            }
        }

        private void NonRecursiveMinimumQueryOnRightEndingRanges()
        {
            var segmentTree = new NonRecursiveSegmentTree<MinimumQueryValue>(_rightEndingRangesArray);

            for (int i = 0; i < _rightEndingRangesArray.Count; ++i)
            {
                segmentTree.Query(i, _rightEndingRangesArray.Count - 1);
            }
        }

        private void NaiveMinimumQueryOnRandomRanges()
        {
            var rand = new Random();

            for (int i = 0; i < _randomRangesCount; ++i)
            {
                int firstIndex = rand.Next(0, _randomRangesArray.Count - 1);
                int secondIndex = rand.Next(0, _randomRangesArray.Count - 1);

                NaiveSegmentTreeAlternatives.MinimumQuery(
                    _randomRangesArray,
                    Math.Min(firstIndex, secondIndex),
                    Math.Max(firstIndex, secondIndex));
            }
        }

        private void NodeBasedMinimumQueryOnRandomRanges()
        {
            var segmentTree = new NodeBasedSegmentTree<MinimumQueryValue>(_randomRangesArray);
            var rand = new Random();

            for (int i = 0; i < _randomRangesCount; ++i)
            {
                int firstIndex = rand.Next(0, _randomRangesArray.Count - 1);
                int secondIndex = rand.Next(0, _randomRangesArray.Count - 1);

                segmentTree.Query(Math.Min(firstIndex, secondIndex), Math.Max(firstIndex, secondIndex));
            }
        }

        private void ArrayBasedMinimumQueryOnRandomRanges()
        {
            var segmentTree = new ArrayBasedSegmentTree<MinimumQueryValue>(_randomRangesArray);
            var rand = new Random();

            for (int i = 0; i < _randomRangesCount; ++i)
            {
                int firstIndex = rand.Next(0, _randomRangesArray.Count - 1);
                int secondIndex = rand.Next(0, _randomRangesArray.Count - 1);

                segmentTree.Query(Math.Min(firstIndex, secondIndex), Math.Max(firstIndex, secondIndex));
            }
        }

        private void NonRecursiveMinimumQueryOnRandomRanges()
        {
            var segmentTree = new NonRecursiveSegmentTree<MinimumQueryValue>(_randomRangesArray);
            var rand = new Random();

            for (int i = 0; i < _randomRangesCount; ++i)
            {
                int firstIndex = rand.Next(0, _randomRangesArray.Count - 1);
                int secondIndex = rand.Next(0, _randomRangesArray.Count - 1);

                segmentTree.Query(Math.Min(firstIndex, secondIndex), Math.Max(firstIndex, secondIndex));
            }
        }

        private void NaiveMaximumSumQuery()
        {
            for (int i = 0; i < _allRangesArray.Count; ++i)
            {
                for (int j = i; j < _allRangesArray.Count; ++j)
                {
                    NaiveSegmentTreeAlternatives.MaximumSumQuery(_allRangesArray, i, j);
                }
            }
        }

        private void NodeBasedMaximumSumQuery()
        {
            var segmentTree = new NodeBasedSegmentTree<MaximumSumQueryValue>(_allRangesArray);

            for (int i = 0; i < _allRangesArray.Count; ++i)
            {
                for (int j = i; j < _allRangesArray.Count; ++j)
                {
                    segmentTree.Query(i, j);
                }
            }
        }

        private void ArrayBasedMaximumSumQuery()
        {
            var segmentTree = new ArrayBasedSegmentTree<MaximumSumQueryValue>(_allRangesArray);

            for (int i = 0; i < _allRangesArray.Count; ++i)
            {
                for (int j = i; j < _allRangesArray.Count; ++j)
                {
                    segmentTree.Query(i, j);
                }
            }
        }

        private void NonRecursiveMaximumSumQuery()
        {
            var segmentTree = new NonRecursiveSegmentTree<MaximumSumQueryValue>(_allRangesArray);

            for (int i = 0; i < _allRangesArray.Count; ++i)
            {
                for (int j = i; j < _allRangesArray.Count; ++j)
                {
                    segmentTree.Query(i, j);
                }
            }
        }

        private void NaiveMaximumSumQueryOnRightEndingRanges()
        {
            for (int i = 0; i < _rightEndingRangesArray.Count; ++i)
            {
                NaiveSegmentTreeAlternatives.MaximumSumQuery(_rightEndingRangesArray, i, _rightEndingRangesArray.Count - 1);
            }
        }

        private void NodeBasedMaximumSumQueryOnRightEndingRanges()
        {
            var segmentTree = new NodeBasedSegmentTree<MaximumSumQueryValue>(_rightEndingRangesArray);

            for (int i = 0; i < _rightEndingRangesArray.Count; ++i)
            {
                segmentTree.Query(i, _rightEndingRangesArray.Count - 1);
            }
        }

        private void ArrayBasedMaximumSumQueryOnRightEndingRanges()
        {
            var segmentTree = new ArrayBasedSegmentTree<MaximumSumQueryValue>(_rightEndingRangesArray);

            for (int i = 0; i < _rightEndingRangesArray.Count; ++i)
            {
                segmentTree.Query(i, _rightEndingRangesArray.Count - 1);
            }
        }

        private void NonRecursiveMaximumSumQueryOnRightEndingRanges()
        {
            var segmentTree = new NonRecursiveSegmentTree<MaximumSumQueryValue>(_rightEndingRangesArray);

            for (int i = 0; i < _rightEndingRangesArray.Count; ++i)
            {
                segmentTree.Query(i, _rightEndingRangesArray.Count - 1);
            }
        }

        private void NaiveMaximumSumQueryOnRandomRanges()
        {
            var rand = new Random();

            for (int i = 0; i < _randomRangesCount; ++i)
            {
                int firstIndex = rand.Next(0, _randomRangesArray.Count - 1);
                int secondIndex = rand.Next(0, _randomRangesArray.Count - 1);

                NaiveSegmentTreeAlternatives.MaximumSumQuery(
                    _randomRangesArray,
                    Math.Min(firstIndex, secondIndex),
                    Math.Max(firstIndex, secondIndex));
            }
        }

        private void NodeBasedMaximumSumQueryOnRandomRanges()
        {
            var segmentTree = new NodeBasedSegmentTree<MaximumSumQueryValue>(_randomRangesArray);
            var rand = new Random();

            for (int i = 0; i < _randomRangesCount; ++i)
            {
                int firstIndex = rand.Next(0, _randomRangesArray.Count - 1);
                int secondIndex = rand.Next(0, _randomRangesArray.Count - 1);

                segmentTree.Query(Math.Min(firstIndex, secondIndex), Math.Max(firstIndex, secondIndex));
            }
        }

        private void ArrayBasedMaximumSumQueryOnRandomRanges()
        {
            var segmentTree = new ArrayBasedSegmentTree<MaximumSumQueryValue>(_randomRangesArray);
            var rand = new Random();

            for (int i = 0; i < _randomRangesCount; ++i)
            {
                int firstIndex = rand.Next(0, _randomRangesArray.Count - 1);
                int secondIndex = rand.Next(0, _randomRangesArray.Count - 1);

                segmentTree.Query(Math.Min(firstIndex, secondIndex), Math.Max(firstIndex, secondIndex));
            }
        }

        private void NonRecursiveMaximumSumQueryOnRandomRanges()
        {
            var segmentTree = new NonRecursiveSegmentTree<MaximumSumQueryValue>(_randomRangesArray);
            var rand = new Random();

            for (int i = 0; i < _randomRangesCount; ++i)
            {
                int firstIndex = rand.Next(0, _randomRangesArray.Count - 1);
                int secondIndex = rand.Next(0, _randomRangesArray.Count - 1);

                segmentTree.Query(Math.Min(firstIndex, secondIndex), Math.Max(firstIndex, secondIndex));
            }
        }
    }
}