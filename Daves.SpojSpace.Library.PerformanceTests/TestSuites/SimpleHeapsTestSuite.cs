using Daves.SpojSpace.Library.Heaps;
using System.Collections.Generic;

namespace Daves.SpojSpace.Library.PerformanceTests.TestSuites
{
    public class SimpleHeapsTestSuite : ITestSuite
    {
        private const int _startingValuesCount = 10000;
        private const int _randomOperationsCount = 100000;
        private readonly IReadOnlyList<int> _startingValues;
        private readonly IReadOnlyList<int> _randomOperations;
        private readonly IReadOnlyList<int> _randomValues;

        public SimpleHeapsTestSuite()
        {
            _startingValues = InputGenerator.GenerateRandomInts(_startingValuesCount);
            _randomOperations = InputGenerator.GenerateRandomInts(_randomOperationsCount, 1, 4);
            _randomValues = InputGenerator.GenerateRandomInts(_randomOperationsCount);
        }

        public IEnumerable<TestScenario> TestScenarios => new TestScenario[]
        {
            new TestScenario($"{_randomOperationsCount} random operations, starting size of 0", new TestCase[]
            {
                new TestCase("Simple naive heap", () => SimpleNaiveHeapRandomOperations(new int[0])),
                new TestCase("Simple binary heap", () => SimpleBinaryHeapRandomOperations(new int[0]))
            }),
            new TestScenario($"{_randomOperationsCount} random operations, starting size of {_startingValuesCount}", new TestCase[]
            {
                new TestCase("Simple naive heap", () => SimpleNaiveHeapRandomOperations(_startingValues)),
                new TestCase("Simple binary heap", () => SimpleBinaryHeapRandomOperations(_startingValues))
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
                    simpleNaiveHeap.Insert(_randomValues[randomValuesIndex++]);
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
                    simpleBinaryHeap.Insert(_randomValues[randomValuesIndex++]);
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
    }
}
