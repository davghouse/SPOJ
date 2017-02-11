using System.Collections.Generic;

namespace Daves.SpojSpace.Library.BinaryIndexedTrees
{
    public static class NaiveBinaryIndexedTreeAlternatives
    {
        public static int SumQuery(IReadOnlyList<int> sourceArray, int queryStartIndex, int queryEndIndex)
        {
            int sum = 0;
            for (int i = queryStartIndex; i <= queryEndIndex; ++i)
            {
                sum += sourceArray[i];
            }

            return sum;
        }

        public static void PointUpdate(int[] sourceArray, int updateIndex, int delta)
            => sourceArray[updateIndex] += delta;

        public static int ValueQuery(IReadOnlyList<int> sourceArray, int queryIndex)
            => sourceArray[queryIndex];

        public static void RangeUpdate(int[] sourceArray, int updateStartIndex, int updateEndIndex, int delta)
        {
            for (int i = updateStartIndex; i <= updateEndIndex; ++i)
            {
                sourceArray[i] += delta;
            }
        }
    }
}
