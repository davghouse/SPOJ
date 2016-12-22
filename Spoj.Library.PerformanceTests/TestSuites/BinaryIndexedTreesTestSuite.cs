using Spoj.Library.BinaryIndexedTrees;
using Spoj.Library.SegmentTrees.AdHoc;
using System;
using System.Collections.Generic;

namespace Spoj.Library.PerformanceTests.TestSuites
{
    public sealed class BinaryIndexedTreesTestSuite : ITestSuite
    {
        private readonly int[] _array;
        private readonly IReadOnlyList<Tuple<int, int>> _randomRanges;
        private const int _randomRangesCount = 20000;

        public BinaryIndexedTreesTestSuite()
        {
            _array = InputGenerator.GenerateRandomInts(50000, -15007, 15007);
            var randomRanges = new Tuple<int, int>[_randomRangesCount];

            var rand = new Random();

            for (int i = 0; i < _randomRangesCount; ++i)
            {
                int firstIndex = rand.Next(0, _array.Length);
                int secondIndex = rand.Next(0, _array.Length);

                randomRanges[i] = Tuple.Create(
                    Math.Min(firstIndex, secondIndex),
                    Math.Max(firstIndex, secondIndex));
            }

            _randomRanges = Array.AsReadOnly(randomRanges);
        }

        public IReadOnlyList<TestScenario> TestScenarios => new TestScenario[]
        {
            new TestScenario($"{_randomRangesCount} random ranges, array size {_array.Length}, sum query", new TestCase[]
            {
                    new TestCase("Naive", NaiveSumQuery),
                    new TestCase("PURQ", PURQSumQuery),
                    new TestCase("RUPQ", RUPQSumQuery),
                    new TestCase("RURQ", RURQSumQuery),
                    new TestCase("Segment tree", SegmentTreeSumQuery),
            }),
            new TestScenario($"{_randomRangesCount} random ranges, array size {_array.Length}, range update", new TestCase[]
            {
                    new TestCase("Naive", NaiveRangeUpdate),
                    new TestCase("PURQ", PURQRangeUpdate),
                    new TestCase("RUPQ", RUPQRangeUpdate),
                    new TestCase("RURQ", RURQRangeUpdate),
                    new TestCase("Segment tree", SegmentTreeRangeUpdate),
            }),
            new TestScenario($"{_randomRangesCount} random ranges, array size {_array.Length}, random operation", new TestCase[]
            {
                    new TestCase("Naive", NaiveRandomOperation),
                    new TestCase("PURQ", PURQRandomOperation),
                    new TestCase("RUPQ", RUPQRandomOperation),
                    new TestCase("RURQ", RURQRandomOperation),
                    new TestCase("Segment tree", SegmentTreeRandomOperation),
            })
        };

        private void NaiveSumQuery()
        {
            for (int i = 0; i < _randomRangesCount; ++i)
            {
                Tuple<int, int> range = _randomRanges[i];

                NaiveBinaryIndexedTreeAlternatives.SumQuery(_array, range.Item1, range.Item2);
            }
        }

        private void NaiveRangeUpdate()
        {
            for (int i = 0; i < _randomRangesCount; ++i)
            {
                Tuple<int, int> range = _randomRanges[i];

                NaiveBinaryIndexedTreeAlternatives.RangeUpdate(_array, range.Item1, range.Item2, 1);
            }
        }

        private void NaiveRandomOperation()
        {
            for (int i = 0; i < _randomRangesCount; ++i)
            {
                Tuple<int, int> range = _randomRanges[i];

                if (range.Item1 % 2 == 0)
                {
                    NaiveBinaryIndexedTreeAlternatives.SumQuery(_array, range.Item1, range.Item2);
                }
                else
                {
                    NaiveBinaryIndexedTreeAlternatives.RangeUpdate(_array, range.Item1, range.Item2, 1);
                }
            }
        }

        private void PURQSumQuery()
        {
            var purqBinaryIndexedTree = new PURQBinaryIndexedTree(_array);

            for (int i = 0; i < _randomRangesCount; ++i)
            {
                Tuple<int, int> range = _randomRanges[i];

                purqBinaryIndexedTree.SumQuery(range.Item1, range.Item2);
            }
        }

        private void PURQRangeUpdate()
        {
            var purqBinaryIndexedTree = new PURQBinaryIndexedTree(_array);

            for (int i = 0; i < _randomRangesCount; ++i)
            {
                Tuple<int, int> range = _randomRanges[i];

                for (int j = range.Item1; j <= range.Item2; ++j)
                {
                    purqBinaryIndexedTree.PointUpdate(j, 1);
                }
            }
        }

        private void PURQRandomOperation()
        {
            var purqBinaryIndexedTree = new PURQBinaryIndexedTree(_array);

            for (int i = 0; i < _randomRangesCount; ++i)
            {
                Tuple<int, int> range = _randomRanges[i];

                if (range.Item1 % 2 == 0)
                {
                    purqBinaryIndexedTree.SumQuery(range.Item1, range.Item2);
                }
                else
                {
                    for (int j = range.Item1; j <= range.Item2; ++j)
                    {
                        purqBinaryIndexedTree.PointUpdate(j, 1);
                    }
                }
            }
        }

        private void RUPQSumQuery()
        {
            var rupqBinaryIndexedTree = new RUPQBinaryIndexedTree(_array);

            for (int i = 0; i < _randomRangesCount; ++i)
            {
                Tuple<int, int> range = _randomRanges[i];

                int sum = 0;
                for (int j = range.Item1; j <= range.Item2; ++j)
                {
                    sum += rupqBinaryIndexedTree.ValueQuery(j);
                }
            }
        }

        private void RUPQRangeUpdate()
        {
            var rupqBinaryIndexedTree = new RUPQBinaryIndexedTree(_array);

            for (int i = 0; i < _randomRangesCount; ++i)
            {
                Tuple<int, int> range = _randomRanges[i];

                rupqBinaryIndexedTree.RangeUpdate(range.Item1, range.Item2, 1);
            }
        }

        private void RUPQRandomOperation()
        {
            var rupqBinaryIndexedTree = new RUPQBinaryIndexedTree(_array);

            for (int i = 0; i < _randomRangesCount; ++i)
            {
                Tuple<int, int> range = _randomRanges[i];

                if (range.Item1 % 2 == 0)
                {
                    int sum = 0;
                    for (int j = range.Item1; j <= range.Item2; ++j)
                    {
                        sum += rupqBinaryIndexedTree.ValueQuery(j);
                    }
                }
                else
                {
                    rupqBinaryIndexedTree.RangeUpdate(range.Item1, range.Item2, 1);
                }
            }
        }

        private void RURQSumQuery()
        {
            var rurqBinaryIndexedTree = new RURQBinaryIndexedTree(_array);

            for (int i = 0; i < _randomRangesCount; ++i)
            {
                Tuple<int, int> range = _randomRanges[i];

                rurqBinaryIndexedTree.SumQuery(range.Item1, range.Item2);
            }
        }

        private void RURQRangeUpdate()
        {
            var rurqBinaryIndexedTree = new RURQBinaryIndexedTree(_array);

            for (int i = 0; i < _randomRangesCount; ++i)
            {
                Tuple<int, int> range = _randomRanges[i];

                rurqBinaryIndexedTree.RangeUpdate(range.Item1, range.Item2, 1);
            }
        }

        private void RURQRandomOperation()
        {
            var rurqBinaryIndexedTree = new RURQBinaryIndexedTree(_array);

            for (int i = 0; i < _randomRangesCount; ++i)
            {
                Tuple<int, int> range = _randomRanges[i];

                if (range.Item1 % 2 == 0)
                {
                    rurqBinaryIndexedTree.SumQuery(range.Item1, range.Item2);
                }
                else
                {
                    rurqBinaryIndexedTree.RangeUpdate(range.Item1, range.Item2, 1);
                }
            }
        }

        private void SegmentTreeSumQuery()
        {
            var segmentTree = new LazySumSegmentTree(_array);

            for (int i = 0; i < _randomRangesCount; ++i)
            {
                Tuple<int, int> range = _randomRanges[i];

                segmentTree.SumQuery(range.Item1, range.Item2);
            }
        }

        private void SegmentTreeRangeUpdate()
        {
            var segmentTree = new LazySumSegmentTree(_array);

            for (int i = 0; i < _randomRangesCount; ++i)
            {
                Tuple<int, int> range = _randomRanges[i];

                segmentTree.Update(range.Item1, range.Item2, 1);
            }
        }

        private void SegmentTreeRandomOperation()
        {
            var segmentTree = new LazySumSegmentTree(_array);

            for (int i = 0; i < _randomRangesCount; ++i)
            {
                Tuple<int, int> range = _randomRanges[i];

                if (range.Item1 % 2 == 0)
                {
                    segmentTree.SumQuery(range.Item1, range.Item2);
                }
                else
                {
                    segmentTree.Update(range.Item1, range.Item2, 1);
                }
            }
        }
    }
}
