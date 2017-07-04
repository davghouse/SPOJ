using SpojSpace.Library.SegmentTrees;
using SpojSpace.Library.SegmentTrees.AdHoc;
using SpojSpace.Library.SegmentTrees.QueryObjects;
using System;
using System.Collections.Generic;

namespace SpojSpace.Library.PerformanceTests.TestSuites
{
    public class SegmentTreesTestSuite : ITestSuite
    {
        private const int _allRangesArraySize = 1000;
        private const int _rightEndingRangesArraySize = 50000;
        private const int _randomRangesArraySize = 50000;
        private const int _randomRangesCount = 10000;
        private readonly IReadOnlyList<int> _allRangesArray;
        private readonly IReadOnlyList<int> _rightEndingRangesArray;
        private readonly IReadOnlyList<int> _randomRangesArray;
        private readonly IReadOnlyList<Tuple<int, int>> _randomRanges;

        public SegmentTreesTestSuite()
        {
            _allRangesArray = InputGenerator.GenerateRandomInts(_allRangesArraySize, -1000, 1000);
            _rightEndingRangesArray = InputGenerator.GenerateRandomInts(_rightEndingRangesArraySize, -1000, 1000);
            _randomRangesArray = InputGenerator.GenerateRandomInts(_randomRangesArraySize, -1000, 1000);

            var rand = InputGenerator.Rand;
            var randomRanges = new Tuple<int, int>[_randomRangesCount];
            for (int i = 0; i < _randomRangesCount; ++i)
            {
                int firstIndex = rand.Next(0, _randomRangesArraySize);
                int secondIndex = rand.Next(0, _randomRangesArraySize);

                randomRanges[i] = Tuple.Create(
                    Math.Min(firstIndex, secondIndex),
                    Math.Max(firstIndex, secondIndex));
            }

            _randomRanges = randomRanges;
        }

        public IEnumerable<TestScenario> TestScenarios => new TestScenario[]
        {
            new TestScenario($"All ranges, array size {_allRangesArraySize}, minimum operations", new TestCase[]
                {
                    new TestCase("Naive query", () => NaiveQuery(NaiveSegmentTreeAlternatives.MinimumQuery, ArrayMode.AllRanges)),
                    new TestCase("Node-based query", () => SegmentTreeQuery<MinimumQueryObject>(SegmentTreeMode.NodeBased, ArrayMode.AllRanges)),
                    new TestCase("Array-based query", () => SegmentTreeQuery<MinimumQueryObject>(SegmentTreeMode.ArrayBased, ArrayMode.AllRanges)),
                    new TestCase("Non-recursive query", () => SegmentTreeQuery<MinimumQueryObject>(SegmentTreeMode.NonRecursive, ArrayMode.AllRanges)),
                    new TestCase("Node-based update", () => SegmentTreeUpdate<MinimumQueryObject>(SegmentTreeMode.NodeBased, ArrayMode.AllRanges)),
                    new TestCase("Array-based update", () => SegmentTreeUpdate<MinimumQueryObject>(SegmentTreeMode.ArrayBased, ArrayMode.AllRanges)),
                    new TestCase("Non-recursive update", () => SegmentTreeUpdate<MinimumQueryObject>(SegmentTreeMode.NonRecursive, ArrayMode.AllRanges)),
                }),
            new TestScenario($"All ranges, array size {_allRangesArraySize}, maximum sum query", new TestCase[]
                {
                    new TestCase("Naive", () => NaiveQuery(NaiveSegmentTreeAlternatives.MaximumSumQuery, ArrayMode.AllRanges)),
                    new TestCase("Node-based", () => SegmentTreeQuery<MaximumSumQueryObject>(SegmentTreeMode.NodeBased, ArrayMode.AllRanges)),
                    new TestCase("Array-based", () => SegmentTreeQuery<MaximumSumQueryObject>(SegmentTreeMode.ArrayBased, ArrayMode.AllRanges)),
                    new TestCase("Non-recursive", () => SegmentTreeQuery<MaximumSumQueryObject>(SegmentTreeMode.NonRecursive, ArrayMode.AllRanges)),
                }),
            new TestScenario($"Right-ending ranges, array size {_rightEndingRangesArraySize}, minimum query", new TestCase[]
                {
                    new TestCase("Naive", () => NaiveQuery(NaiveSegmentTreeAlternatives.MinimumQuery, ArrayMode.RightEndingRanges)),
                    new TestCase("Node-based", () => SegmentTreeQuery<MinimumQueryObject>(SegmentTreeMode.NodeBased, ArrayMode.RightEndingRanges)),
                    new TestCase("Array-based", () => SegmentTreeQuery<MinimumQueryObject>(SegmentTreeMode.ArrayBased, ArrayMode.RightEndingRanges)),
                    new TestCase("Non-recursive", () => SegmentTreeQuery<MinimumQueryObject>(SegmentTreeMode.NonRecursive, ArrayMode.RightEndingRanges)),
                }),
            new TestScenario($"Right-ending ranges, array size {_rightEndingRangesArraySize}, maximum sum query", new TestCase[]
                {
                    new TestCase("Naive", () => NaiveQuery(NaiveSegmentTreeAlternatives.MaximumSumQuery, ArrayMode.RightEndingRanges)),
                    new TestCase("Node-based", () => SegmentTreeQuery<MaximumSumQueryObject>(SegmentTreeMode.NodeBased, ArrayMode.RightEndingRanges)),
                    new TestCase("Array-based", () => SegmentTreeQuery<MaximumSumQueryObject>(SegmentTreeMode.ArrayBased, ArrayMode.RightEndingRanges)),
                    new TestCase("Non-recursive", () => SegmentTreeQuery<MaximumSumQueryObject>(SegmentTreeMode.NonRecursive, ArrayMode.RightEndingRanges)),
                }),
            new TestScenario($"{_randomRangesCount} random ranges, array size {_randomRangesArraySize}, minimum query", new TestCase[]
                {
                    new TestCase("Naive", () => NaiveQuery(NaiveSegmentTreeAlternatives.MinimumQuery, ArrayMode.RandomRanges)),
                    new TestCase("Node-based", () => SegmentTreeQuery<MinimumQueryObject>(SegmentTreeMode.NodeBased, ArrayMode.RandomRanges)),
                    new TestCase("Array-based", () => SegmentTreeQuery<MinimumQueryObject>(SegmentTreeMode.ArrayBased, ArrayMode.RandomRanges)),
                    new TestCase("Non-recursive", () => SegmentTreeQuery<MinimumQueryObject>(SegmentTreeMode.NonRecursive, ArrayMode.RandomRanges)),
                }),
            new TestScenario($"{_randomRangesCount} random ranges, array size {_randomRangesArraySize}, maximum sum query", new TestCase[]
                {
                    new TestCase("Naive", () => NaiveQuery(NaiveSegmentTreeAlternatives.MaximumSumQuery, ArrayMode.RandomRanges)),
                    new TestCase("Node-based", () => SegmentTreeQuery<MaximumSumQueryObject>(SegmentTreeMode.NodeBased, ArrayMode.RandomRanges)),
                    new TestCase("Array-based", () => SegmentTreeQuery<MaximumSumQueryObject>(SegmentTreeMode.ArrayBased, ArrayMode.RandomRanges)),
                    new TestCase("Non-recursive", () => SegmentTreeQuery<MaximumSumQueryObject>(SegmentTreeMode.NonRecursive, ArrayMode.RandomRanges)),
                }),
            new TestScenario($"{_randomRangesCount} random points, array size {_randomRangesArraySize}, minimum update", new TestCase[]
                {
                    new TestCase("Node-based", () => SegmentTreeUpdate<MinimumQueryObject>(SegmentTreeMode.NodeBased, ArrayMode.RandomRanges, randomPoints: true)),
                    new TestCase("Array-based", () => SegmentTreeUpdate<MinimumQueryObject>(SegmentTreeMode.ArrayBased, ArrayMode.RandomRanges, randomPoints: true)),
                    new TestCase("Non-recursive", () => SegmentTreeUpdate<MinimumQueryObject>(SegmentTreeMode.NonRecursive, ArrayMode.RandomRanges, randomPoints: true)),
                }),
            new TestScenario($"{_randomRangesCount} random ranges, array size {_randomRangesArraySize}, minimum update", new TestCase[]
                {
                    new TestCase("Node-based", () => SegmentTreeUpdate<MinimumQueryObject>(SegmentTreeMode.NodeBased, ArrayMode.RandomRanges)),
                    new TestCase("Array-based", () => SegmentTreeUpdate<MinimumQueryObject>(SegmentTreeMode.ArrayBased, ArrayMode.RandomRanges)),
                    new TestCase("Non-recursive", () => SegmentTreeUpdate<MinimumQueryObject>(SegmentTreeMode.NonRecursive, ArrayMode.RandomRanges)),
                }),
            new TestScenario($"{_randomRangesCount} random ranges, array size {_randomRangesArraySize}, sum random operation", new TestCase[]
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
                for (int i = 0; i < _allRangesArraySize; ++i)
                {
                    for (int j = i; j < _allRangesArraySize; ++j)
                    {
                        naiveQuerier(_allRangesArray, i, j);
                    }
                }
            }
            else if (arrayMode == ArrayMode.RightEndingRanges)
            {
                for (int i = 0; i < _rightEndingRangesArraySize; ++i)
                {
                    naiveQuerier(_rightEndingRangesArray, i, _rightEndingRangesArraySize - 1);
                }
            }
            else
            {
                for (int i = 0; i < _randomRangesCount; ++i)
                {
                    Tuple<int, int> range = _randomRanges[i];

                    naiveQuerier(_randomRangesArray, range.Item1, range.Item2);
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
                for (int i = 0; i < _randomRangesCount; ++i)
                {
                    Tuple<int, int> range = _randomRanges[i];

                    segmentTree.Query(range.Item1, range.Item2);
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
                if (randomPoints)
                {
                    for (int i = 0; i < _randomRangesCount; ++i)
                    {
                        Tuple<int, int> range = _randomRanges[i];

                        segmentTree.Update(range.Item1, updater);
                    }
                }
                else
                {
                    for (int i = 0; i < _randomRangesCount; ++i)
                    {
                        Tuple<int, int> range = _randomRanges[i];

                        segmentTree.Update(range.Item1, range.Item2, updater);
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

            for (int r = 0; r < _randomRangesCount; ++r)
            {
                Tuple<int, int> range = _randomRanges[r];

                if (range.Item1 % 2 == 0)
                {
                    segmentTree.Update(range.Item1, range.Item2, updater);
                }
                else
                {
                    segmentTree.Query(range.Item1, range.Item2);
                }
            }
        }

        private void LazySumSegmentTreeRandomOperation()
        {
            var segmentTree = new LazySumSegmentTree(_randomRangesArray);

            for (int r = 0; r < _randomRangesCount; ++r)
            {
                Tuple<int, int> range = _randomRanges[r];

                if (range.Item1 % 2 == 0)
                {
                    segmentTree.Update(range.Item1, range.Item2, 1);
                }
                else
                {
                    segmentTree.SumQuery(range.Item1, range.Item2);
                }
            }
        }
    }
}
