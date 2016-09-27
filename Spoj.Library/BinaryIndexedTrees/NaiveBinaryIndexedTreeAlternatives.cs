using System.Collections.Generic;

namespace Spoj.Library.BinaryIndexedTrees
{
    public static class NaiveBinaryIndexedTreeAlternatives
    {
        public static int CumulativeSumQuery(IReadOnlyList<int> sourceArray, int queryStartIndex, int queryEndIndex)
        {
            int cumulativeSum = 0;
            for (int i = queryStartIndex; i <= queryEndIndex; ++i)
            {
                cumulativeSum += sourceArray[i];
            }

            return cumulativeSum;
        }

        public static void PointUpdate(int[] sourceArray, int updateIndex, int addition)
            => sourceArray[updateIndex] += addition;

        public static int SumQuery(IReadOnlyList<int> sourceArray, int queryIndex)
            => sourceArray[queryIndex];

        public static void RangeUpdate(int[] sourceArray, int updateStartIndex, int updateEndIndex, int addition)
        {
            for (int i = updateStartIndex; i <= updateEndIndex; ++i)
            {
                sourceArray[i] += addition;
            }
        }
    }
}
