using System;
using System.Collections.Generic;

namespace Spoj.Library.SegmentTrees
{
    public static class NaiveSegmentTreeAlternatives
    {
        private static int CombinerQuery(IReadOnlyList<int> sourceArray, int queryStartIndex, int queryEndIndex, Func<int, int, int> combiner)
        {
            int result = sourceArray[queryStartIndex];

            for (int i = queryStartIndex + 1; i <= queryEndIndex; ++i)
            {
                result = combiner(result, sourceArray[i]);
            }

            return result;
        }

        public static int MinimumQuery(IReadOnlyList<int> sourceArray, int queryStartIndex, int queryEndIndex)
            => CombinerQuery(sourceArray, queryStartIndex, queryEndIndex, (result, next) => result < next ? result : next);

        public static int MaximumQuery(IReadOnlyList<int> sourceArray, int queryStartIndex, int queryEndIndex)
            => CombinerQuery(sourceArray, queryStartIndex, queryEndIndex, (result, next) => result > next ? result : next);

        public static int SumQuery(IReadOnlyList<int> sourceArray, int queryStartIndex, int queryEndIndex)
            => CombinerQuery(sourceArray, queryStartIndex, queryEndIndex, (result, next) => result + next);

        public static int ProductQuery(IReadOnlyList<int>sourceArray, int queryStartIndex, int queryEndIndex)
            => CombinerQuery(sourceArray, queryStartIndex, queryEndIndex, (result, next) => result * next);

        // This is the common dynamic programming solution.
        public static int MaximumSumQuery(IReadOnlyList<int> sourceArray, int queryStartIndex, int queryEndIndex)
        {
            var maximumSumEndingAt = new int[sourceArray.Count];
            maximumSumEndingAt[queryStartIndex] = sourceArray[queryStartIndex];

            int maximumSum = maximumSumEndingAt[queryStartIndex];
            for (int i = queryStartIndex + 1; i <= queryEndIndex; ++i)
            {
                maximumSumEndingAt[i] = Math.Max(
                    maximumSumEndingAt[i - 1] + sourceArray[i],
                    sourceArray[i]);
                maximumSum = Math.Max(maximumSum, maximumSumEndingAt[i]);
            }

            return maximumSum;
        }

        public static void Update(int[] sourceArray, int queryIndex, Func<int, int> updater)
            => sourceArray[queryIndex] = updater(sourceArray[queryIndex]);

        public static void Update(int[] sourceArray, int queryStartIndex, int queryEndIndex, Func<int, int> updater)
        {
            for (int i = queryStartIndex; i <= queryEndIndex; ++i)
            {
                sourceArray[i] = updater(sourceArray[i]);
            }
        }
    }
}
