using Daves.SpojSpace.Library.Heaps;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Daves.SpojSpace.Library.PerformanceTests.TestSuites
{
    public class HeapsTestSuite : ITestSuite
    {
        private const int _startingValuesCountSmall = 10;
        private const int _startingValuesCountBig = 10000;
        private const int _randomOperationsCount = 200000;
        private readonly IReadOnlyList<int> _startingValuesSmall;
        private readonly IReadOnlyList<int> _startingValuesBig;
        private readonly IReadOnlyList<KeyValuePair<int, int>> _startingKeyValuePairsSmall;
        private readonly IReadOnlyList<KeyValuePair<int, int>> _startingKeyValuePairsBig;
        private readonly IReadOnlyList<int> _randomOperations;
        private readonly IReadOnlyList<int> _randomValues;
        private readonly IReadOnlyList<Tuple<int, int>> _randomPrimOperations;
        private int _key = 0;

        public HeapsTestSuite()
        {
            _startingValuesSmall = InputGenerator.GenerateRandomInts(_startingValuesCountSmall);
            _startingKeyValuePairsSmall = _startingValuesSmall
                .Select(v => new KeyValuePair<int, int>(++_key, v))
                .ToList();
            _key = 0;
            _startingValuesBig = InputGenerator.GenerateRandomInts(_startingValuesCountBig);
            _startingKeyValuePairsBig = _startingValuesBig
                .Select(v => new KeyValuePair<int, int>(++_key, v))
                .ToList();
            _randomOperations = InputGenerator.GenerateRandomInts(_randomOperationsCount, 1, 4);
            _randomValues = InputGenerator.GenerateRandomInts(_randomOperationsCount);

            var randomPrimOperations = new List<Tuple<int, int>>();
            int extractOperationCount = 0;
            var rand = new Random();
            while (extractOperationCount != _startingValuesCountBig)
            {
                int operation = rand.Next(1, 6 + 1);
                if (operation == 1)
                {
                    ++extractOperationCount;
                    randomPrimOperations.Add(null);
                }
                else
                {
                    randomPrimOperations.Add(Tuple.Create(rand.Next(1, _startingValuesCountBig + 1), rand.Next()));
                }
            }
            _randomPrimOperations = randomPrimOperations;
        }

        public IEnumerable<TestScenario> TestScenarios => new TestScenario[]
        {
            new TestScenario($"{_randomOperationsCount} random operations, starting size of {_startingValuesCountSmall}", new TestCase[]
            {
                new TestCase("Simple naive heap", () => SimpleNaiveHeapRandomOperations(_startingValuesSmall)),
                new TestCase("Simple binary heap", () => SimpleBinaryHeapRandomOperations(_startingValuesSmall)),
                new TestCase("Naive heap", () => NaiveHeapRandomOperations(_startingKeyValuePairsSmall)),
                new TestCase("Binary heap", () => BinaryHeapRandomOperations(_startingKeyValuePairsSmall))
            }),
            new TestScenario($"{_randomOperationsCount} random operations, starting size of {_startingValuesCountBig}", new TestCase[]
            {
                new TestCase("Simple naive heap", () => SimpleNaiveHeapRandomOperations(_startingValuesBig)),
                new TestCase("Simple binary heap", () => SimpleBinaryHeapRandomOperations(_startingValuesBig)),
                new TestCase("Naive heap", () => NaiveHeapRandomOperations(_startingKeyValuePairsBig)),
                new TestCase("Binary heap", () => BinaryHeapRandomOperations(_startingKeyValuePairsBig))
            }),
            new TestScenario($"5:1 update/extract operations, starting size of {_startingValuesCountBig} (Prim simulation)", new TestCase[]
            {
                new TestCase("Naive heap", NaiveHeapPrimSimulation),
                new TestCase("Binary heap", BinaryHeapPrimSimulation)
            })
        };

        private void SimpleNaiveHeapRandomOperations(IReadOnlyList<int> startingValues)
        {
            var simpleNaiveHeap = new SimpleNaiveHeap<int>(startingValues);
            int randomValuesIndex = 0;

            foreach (int operation in _randomOperations)
            {
                if (operation == 1 || simpleNaiveHeap.Size <= 1)
                {
                    simpleNaiveHeap.Add(_randomValues[randomValuesIndex++]);
                }
                else if (operation == 2)
                {
                    simpleNaiveHeap.Extract();
                }
                else if (operation == 3)
                {
                    simpleNaiveHeap.Replace(_randomValues[randomValuesIndex++]);
                }
                else
                {
                    int top = simpleNaiveHeap.Top;
                }
            }
        }

        private void SimpleBinaryHeapRandomOperations(IReadOnlyList<int> startingValues)
        {
            var simpleBinaryHeap = new SimpleBinaryHeap<int>(startingValues);
            int randomValuesIndex = 0;

            foreach (int operation in _randomOperations)
            {
                if (operation == 1 || simpleBinaryHeap.Size <= 1)
                {
                    simpleBinaryHeap.Add(_randomValues[randomValuesIndex++]);
                }
                else if (operation == 2)
                {
                    simpleBinaryHeap.Extract();
                }
                else if (operation == 3)
                {
                    simpleBinaryHeap.Replace(_randomValues[randomValuesIndex++]);
                }
                else
                {
                    int top = simpleBinaryHeap.Top;
                }
            }
        }

        private void NaiveHeapRandomOperations(IReadOnlyList<KeyValuePair<int, int>> startingKeyValuePairs)
        {
            var naiveHeap = new NaiveHeap<int, int>(startingKeyValuePairs);
            int randomValuesIndex = 0;

            foreach (int operation in _randomOperations)
            {
                if (operation == 1 || naiveHeap.Size <= 1)
                {
                    naiveHeap.Add(++_key, _randomValues[randomValuesIndex++]);
                }
                else if (operation == 2)
                {
                    naiveHeap.Extract();
                }
                else if (operation == 3)
                {
                    naiveHeap.Replace(++_key, _randomValues[randomValuesIndex++]);
                }
                else
                {
                    var top = naiveHeap.Top;
                }
            }
        }

        private void BinaryHeapRandomOperations(IReadOnlyList<KeyValuePair<int, int>> startingKeyValuePairs)
        {
            var binaryHeap = new BinaryHeap<int, int>(startingKeyValuePairs);
            int randomValuesIndex = 0;

            foreach (int operation in _randomOperations)
            {
                if (operation == 1 || binaryHeap.Size <= 1)
                {
                    binaryHeap.Add(++_key, _randomValues[randomValuesIndex++]);
                }
                else if (operation == 2)
                {
                    binaryHeap.Extract();
                }
                else if (operation == 3)
                {
                    binaryHeap.Replace(++_key, _randomValues[randomValuesIndex++]);
                }
                else
                {
                    var top = binaryHeap.Top;
                }
            }
        }

        private void NaiveHeapPrimSimulation()
        {
            var naiveHeap = new NaiveHeap<int, int>(_startingKeyValuePairsBig);

            foreach (Tuple<int, int> randomOperation in _randomPrimOperations)
            {
                if (randomOperation == null)
                {
                    naiveHeap.Extract();
                }
                else
                {
                    if (naiveHeap.Contains(randomOperation.Item1))
                    {
                        naiveHeap.Update(randomOperation.Item1, randomOperation.Item2);
                    }
                }
            }
        }

        private void BinaryHeapPrimSimulation()
        {
            var binaryHeap = new BinaryHeap<int, int>(_startingKeyValuePairsBig);

            foreach (Tuple<int, int> randomOperation in _randomPrimOperations)
            {
                if (randomOperation == null)
                {
                    binaryHeap.Extract();
                }
                else
                {
                    if (binaryHeap.Contains(randomOperation.Item1))
                    {
                        binaryHeap.Update(randomOperation.Item1, randomOperation.Item2);
                    }
                }
            }
        }
    }
}
