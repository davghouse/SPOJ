using Spoj.Library.SegmentTrees;
using Spoj.Library.SegmentTrees.AdHoc;
using Spoj.Library.SegmentTrees.QueryObjects;
using System;
using System.Collections.Generic;

namespace Spoj.Library.PerformanceTests.TestSuites
{
    public class SegmentTreesTestSuite : ITestSuite
    {
        private readonly IReadOnlyList<int> _allRangesArray;
        private readonly IReadOnlyList<int> _rightEndingRangesArray;
        private readonly IReadOnlyList<int> _randomRangesArray;
        private const int _randomRangesCount = 10000;

        public SegmentTreesTestSuite()
        {
            _allRangesArray = InputGenerator.GenerateRandomInts(1000, -200, 200);
            _rightEndingRangesArray = InputGenerator.GenerateRandomInts(50000, -300, 300);
            _randomRangesArray = InputGenerator.GenerateRandomInts(50000, -15007, 15007);
        }

        public IReadOnlyList<TestScenario> TestScenarios => new TestScenario[]
        {
            new TestScenario($"All ranges, array size {_allRangesArray.Count}, minimum", new TestCase[]
                {
                    new TestCase("Naive query", () => NaiveQuery(NaiveSegmentTreeAlternatives.MinimumQuery, ArrayMode.AllRanges)),
                    new TestCase("Node-based query", () => SegmentTreeQuery<MinimumQueryObject>(SegmentTreeMode.NodeBased, ArrayMode.AllRanges)),
                    new TestCase("Array-based query", () => SegmentTreeQuery<MinimumQueryObject>(SegmentTreeMode.ArrayBased, ArrayMode.AllRanges)),
                    new TestCase("Non-recursive query", () => SegmentTreeQuery<MinimumQueryObject>(SegmentTreeMode.NonRecursive, ArrayMode.AllRanges)),
                    new TestCase("Node-based update", () => SegmentTreeUpdate<MinimumQueryObject>(SegmentTreeMode.NodeBased, ArrayMode.AllRanges)),
                    new TestCase("Array-based update", () => SegmentTreeUpdate<MinimumQueryObject>(SegmentTreeMode.ArrayBased, ArrayMode.AllRanges)),
                    new TestCase("Non-recursive update", () => SegmentTreeUpdate<MinimumQueryObject>(SegmentTreeMode.NonRecursive, ArrayMode.AllRanges)),
                }),
            new TestScenario($"All ranges, array size {_allRangesArray.Count}, maximum sum query", new TestCase[]
                {
                    new TestCase("Naive", () => NaiveQuery(NaiveSegmentTreeAlternatives.MaximumSumQuery, ArrayMode.AllRanges)),
                    new TestCase("Node-based", () => SegmentTreeQuery<MaximumSumQueryObject>(SegmentTreeMode.NodeBased, ArrayMode.AllRanges)),
                    new TestCase("Array-based", () => SegmentTreeQuery<MaximumSumQueryObject>(SegmentTreeMode.ArrayBased, ArrayMode.AllRanges)),
                    new TestCase("Non-recursive", () => SegmentTreeQuery<MaximumSumQueryObject>(SegmentTreeMode.NonRecursive, ArrayMode.AllRanges)),
                }),
            new TestScenario($"Right-ending ranges, array size {_rightEndingRangesArray.Count}, minimum query", new TestCase[]
                {
                    new TestCase("Naive", () => NaiveQuery(NaiveSegmentTreeAlternatives.MinimumQuery, ArrayMode.RightEndingRanges)),
                    new TestCase("Node-based", () => SegmentTreeQuery<MinimumQueryObject>(SegmentTreeMode.NodeBased, ArrayMode.RightEndingRanges)),
                    new TestCase("Array-based", () => SegmentTreeQuery<MinimumQueryObject>(SegmentTreeMode.ArrayBased, ArrayMode.RightEndingRanges)),
                    new TestCase("Non-recursive", () => SegmentTreeQuery<MinimumQueryObject>(SegmentTreeMode.NonRecursive, ArrayMode.RightEndingRanges)),
                }),
            new TestScenario($"Right-ending ranges, array size {_rightEndingRangesArray.Count}, maximum sum query", new TestCase[]
                {
                    new TestCase("Naive", () => NaiveQuery(NaiveSegmentTreeAlternatives.MaximumSumQuery, ArrayMode.RightEndingRanges)),
                    new TestCase("Node-based", () => SegmentTreeQuery<MaximumSumQueryObject>(SegmentTreeMode.NodeBased, ArrayMode.RightEndingRanges)),
                    new TestCase("Array-based", () => SegmentTreeQuery<MaximumSumQueryObject>(SegmentTreeMode.ArrayBased, ArrayMode.RightEndingRanges)),
                    new TestCase("Non-recursive", () => SegmentTreeQuery<MaximumSumQueryObject>(SegmentTreeMode.NonRecursive, ArrayMode.RightEndingRanges)),
                }),
            new TestScenario($"{_randomRangesCount} random ranges, array size {_randomRangesArray.Count}, minimum query", new TestCase[]
                {
                    new TestCase("Naive", () => NaiveQuery(NaiveSegmentTreeAlternatives.MinimumQuery, ArrayMode.RandomRanges)),
                    new TestCase("Node-based", () => SegmentTreeQuery<MinimumQueryObject>(SegmentTreeMode.NodeBased, ArrayMode.RandomRanges)),
                    new TestCase("Array-based", () => SegmentTreeQuery<MinimumQueryObject>(SegmentTreeMode.ArrayBased, ArrayMode.RandomRanges)),
                    new TestCase("Non-recursive", () => SegmentTreeQuery<MinimumQueryObject>(SegmentTreeMode.NonRecursive, ArrayMode.RandomRanges)),
                }),
            new TestScenario($"{_randomRangesCount} random ranges, array size {_randomRangesArray.Count}, maximum sum query", new TestCase[]
                {
                    new TestCase("Naive", () => NaiveQuery(NaiveSegmentTreeAlternatives.MaximumSumQuery, ArrayMode.RandomRanges)),
                    new TestCase("Node-based", () => SegmentTreeQuery<MaximumSumQueryObject>(SegmentTreeMode.NodeBased, ArrayMode.RandomRanges)),
                    new TestCase("Array-based", () => SegmentTreeQuery<MaximumSumQueryObject>(SegmentTreeMode.ArrayBased, ArrayMode.RandomRanges)),
                    new TestCase("Non-recursive", () => SegmentTreeQuery<MaximumSumQueryObject>(SegmentTreeMode.NonRecursive, ArrayMode.RandomRanges)),
                }),
            new TestScenario($"{_randomRangesCount} random points, array size {_randomRangesArray.Count}, minimum update", new TestCase[]
                {
                    new TestCase("Node-based", () => SegmentTreeUpdate<MinimumQueryObject>(SegmentTreeMode.NodeBased, ArrayMode.RandomRanges, randomPoints: true)),
                    new TestCase("Array-based", () => SegmentTreeUpdate<MinimumQueryObject>(SegmentTreeMode.ArrayBased, ArrayMode.RandomRanges, randomPoints: true)),
                    new TestCase("Non-recursive", () => SegmentTreeUpdate<MinimumQueryObject>(SegmentTreeMode.NonRecursive, ArrayMode.RandomRanges, randomPoints: true)),
                }),
            new TestScenario($"{_randomRangesCount} random ranges, array size {_randomRangesArray.Count}, minimum update", new TestCase[]
                {
                    new TestCase("Node-based", () => SegmentTreeUpdate<MinimumQueryObject>(SegmentTreeMode.NodeBased, ArrayMode.RandomRanges)),
                    new TestCase("Array-based", () => SegmentTreeUpdate<MinimumQueryObject>(SegmentTreeMode.ArrayBased, ArrayMode.RandomRanges)),
                    new TestCase("Non-recursive", () => SegmentTreeUpdate<MinimumQueryObject>(SegmentTreeMode.NonRecursive, ArrayMode.RandomRanges)),
                }),
            new TestScenario($"{_randomRangesCount} random ranges, array size {_randomRangesArray.Count}, sum random operation", new TestCase[]
                {
                    new TestCase("Node-based", () => SegmentTreeSumRandomOperation(SegmentTreeMode.NodeBased)),
                    new TestCase("Array-based", () => SegmentTreeSumRandomOperation(SegmentTreeMode.ArrayBased)),
                    new TestCase("Non-recursive", () => SegmentTreeSumRandomOperation(SegmentTreeMode.NonRecursive)),
                    new TestCase("Ad hoc lazy", () => LazySumSegmentTreeRandomOperation()),
                })
        };

        private enum SegmentTreeMode
        {
            NodeBased,
            ArrayBased,
            NonRecursive,
            AdHocLazy
        };

        private enum ArrayMode
        {
            AllRanges,
            RightEndingRanges,
            RandomRanges
        };

        private void NaiveQuery(Func<IReadOnlyList<int>, int, int, int> naiveQuerier, ArrayMode arrayMode)
        {
            if (arrayMode == ArrayMode.AllRanges)
            {
                for (int i = 0; i < _allRangesArray.Count; ++i)
                {
                    for (int j = i; j < _allRangesArray.Count; ++j)
                    {
                        naiveQuerier(_allRangesArray, i, j);
                    }
                }
            }
            else if (arrayMode == ArrayMode.RightEndingRanges)
            {
                for (int i = 0; i < _rightEndingRangesArray.Count; ++i)
                {
                    naiveQuerier(_rightEndingRangesArray, i, _rightEndingRangesArray.Count - 1);
                }
            }
            else
            {
                var rand = new Random();

                for (int i = 0; i < _randomRangesCount; ++i)
                {
                    int firstIndex = rand.Next(0, _randomRangesArray.Count - 1);
                    int secondIndex = rand.Next(0, _randomRangesArray.Count - 1);

                    naiveQuerier(
                        _randomRangesArray,
                        Math.Min(firstIndex, secondIndex),
                        Math.Max(firstIndex, secondIndex));
                }
            }
        }

        private void SegmentTreeQuery<TQueryObject>(SegmentTreeMode segmentTreeMode, ArrayMode arrayMode)
            where TQueryObject : SegmentTreeQueryObject<TQueryObject, int>, new()
        {
            var array = arrayMode == ArrayMode.AllRanges ? _allRangesArray
                : arrayMode == ArrayMode.RightEndingRanges ? _rightEndingRangesArray
                : _randomRangesArray;
            var segmentTree = segmentTreeMode == SegmentTreeMode.NodeBased ? new NodeBasedSegmentTree<TQueryObject, int>(array)
                : segmentTreeMode == SegmentTreeMode.ArrayBased ? new ArrayBasedSegmentTree<TQueryObject, int>(array)
                : (SegmentTree<TQueryObject, int>)new NonRecursiveSegmentTree<TQueryObject, int>(array);

            if (arrayMode == ArrayMode.AllRanges)
            {
                for (int i = 0; i < array.Count; ++i)
                {
                    for (int j = i; j < array.Count; ++j)
                    {
                        segmentTree.Query(i, j);
                    }
                }
            }
            else if (arrayMode == ArrayMode.RightEndingRanges)
            {
                for (int i = 0; i < array.Count; ++i)
                {
                    segmentTree.Query(i, array.Count - 1);
                }
            }
            else
            {
                var rand = new Random();

                for (int i = 0; i < _randomRangesCount; ++i)
                {
                    int firstIndex = rand.Next(0, array.Count);
                    int secondIndex = rand.Next(0, array.Count);

                    segmentTree.Query(Math.Min(firstIndex, secondIndex), Math.Max(firstIndex, secondIndex));
                }
            }
        }

        private void SegmentTreeUpdate<TQueryObject>(SegmentTreeMode segmentTreeMode, ArrayMode arrayMode, bool randomPoints = false)
            where TQueryObject : SegmentTreeQueryObject<TQueryObject, int>, new()
        {
            Func<int, int> updater = x => x + 1;

            var array = arrayMode == ArrayMode.AllRanges ? _allRangesArray
                : _randomRangesArray;
            var segmentTree = segmentTreeMode == SegmentTreeMode.NodeBased ? new NodeBasedSegmentTree<TQueryObject, int>(array)
                : segmentTreeMode == SegmentTreeMode.ArrayBased ? new ArrayBasedSegmentTree<TQueryObject, int>(array)
                : (SegmentTree<TQueryObject, int>)new NonRecursiveSegmentTree<TQueryObject, int>(array);

            if (arrayMode == ArrayMode.AllRanges)
            {
                for (int i = 0; i < array.Count; ++i)
                {
                    for (int j = i; j < array.Count; ++j)
                    {
                        segmentTree.Update(i, j, updater);
                    }
                }
            }
            else
            {
                var rand = new Random();

                if (randomPoints)
                {
                    for (int i = 0; i < _randomRangesCount; ++i)
                    {
                        int index = rand.Next(0, array.Count);

                        segmentTree.Update(index, updater);
                    }
                }
                else
                {
                    for (int i = 0; i < _randomRangesCount; ++i)
                    {
                        int firstIndex = rand.Next(0, array.Count);
                        int secondIndex = rand.Next(0, array.Count);

                        segmentTree.Update(Math.Min(firstIndex, secondIndex), Math.Max(firstIndex, secondIndex), updater);
                    }
                }
            }
        }

        private void SegmentTreeSumRandomOperation(SegmentTreeMode segmentTreeMode)
        {
            Func<int, int> updater = x => x + 1;

            var segmentTree = segmentTreeMode == SegmentTreeMode.NodeBased ? new NodeBasedSegmentTree<SumQueryObject, int>(_randomRangesArray)
                : segmentTreeMode == SegmentTreeMode.ArrayBased ? new ArrayBasedSegmentTree<SumQueryObject, int>(_randomRangesArray)
                : (SegmentTree<SumQueryObject, int>)new NonRecursiveSegmentTree<SumQueryObject, int>(_randomRangesArray);

            var rand = new Random();

            for (int r = 0; r < _randomRangesCount; ++r)
            {
                int firstIndex = rand.Next(0, _randomRangesArray.Count);
                int secondIndex = rand.Next(0, _randomRangesArray.Count);
                int startIndex = Math.Min(firstIndex, secondIndex);
                int endIndex = Math.Max(firstIndex, secondIndex);
                int mode = rand.Next(2);

                if (mode == 0)
                {
                    segmentTree.Update(startIndex, endIndex, updater);
                }
                else
                {
                    segmentTree.Query(startIndex, endIndex);
                }
            }
        }

        private void LazySumSegmentTreeRandomOperation()
        {
            var segmentTree = new LazySumSegmentTree(_randomRangesArray);

            var rand = new Random();

            for (int r = 0; r < _randomRangesCount; ++r)
            {
                int firstIndex = rand.Next(0, _randomRangesArray.Count);
                int secondIndex = rand.Next(0, _randomRangesArray.Count);
                int startIndex = Math.Min(firstIndex, secondIndex);
                int endIndex = Math.Max(firstIndex, secondIndex);
                int mode = rand.Next(2);

                if (mode == 0)
                {
                    segmentTree.Update(startIndex, endIndex, 1);
                }
                else
                {
                    segmentTree.Query(startIndex, endIndex);
                }
            }
        }
    }
}
