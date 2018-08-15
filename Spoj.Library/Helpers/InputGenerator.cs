using Spoj.Library.Graphs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Spoj.Library.Helpers
{
    public static class InputGenerator
    {
        // This isn't thread-safe, but it doesn't matter (yet): http://csharpindepth.com/Articles/Chapter12/Random.aspx.
        public static readonly Random Rand = new Random();

        // Inclusive min and max values.
        public static int GenerateRandomInt(int minValue = 0, int maxValue = int.MaxValue - 1)
        {
            if (maxValue == int.MaxValue)
                throw new NotSupportedException("Random.Next has an exclusive upper bound, so can't include int.MaxValue.");

            return Rand.Next(minValue, maxValue + 1);
        }

        // Inclusive min and max values.
        public static int[] GenerateRandomInts(int count, int minValue = 0, int maxValue = int.MaxValue - 1)
        {
            if (maxValue == int.MaxValue)
                throw new NotSupportedException("Random.Next has an exclusive upper bound, so can't include int.MaxValue.");

            int[] ints = new int[count];
            for (int i = 0; i < count; ++i)
            {
                ints[i] = Rand.Next(minValue, maxValue + 1);
            }

            return ints;
        }

        // Inclusive min and max values.
        public static int[] GenerateDistinctRandomInts(int count, int minValue = 0, int maxValue = int.MaxValue - 1)
        {
            if (maxValue == int.MaxValue)
                throw new NotSupportedException("Random.Next has an exclusive upper bound, so can't include int.MaxValue.");

            var distinctRandomInts = new HashSet<int>();
            int[] ints = new int[count];
            while (distinctRandomInts.Count < count)
            {
                int num = Rand.Next(minValue, maxValue + 1);
                if (distinctRandomInts.Add(num))
                {
                    ints[distinctRandomInts.Count - 1] = num;
                }
            }

            return ints;
        }

        // Inclusive min and max values.
        public static string GenerateRandomString(int length, char minValue = 'a', char maxValue = 'z')
            => new string(GenerateRandomInts(length, minValue, maxValue).Select(i => (char)i).ToArray());

        // Inclusive min and max values.
        public static int[,] GenerateRandomEvenOddPairs(int count, int minValue = 0, int maxValue = int.MaxValue - 1)
        {
            if (maxValue == int.MaxValue)
                throw new NotSupportedException("Random.Next has an exclusive upper bound, so can't include int.MaxValue.");

            if (minValue == maxValue)
                throw new NotSupportedException("Can't generate even and odd numbers from a single number.");

            int[,] evenOddPairs = new int[count, 2];
            for (int i = 0; i < count; ++i)
            {
                int even = Rand.Next(minValue, maxValue + 1);
                int odd = Rand.Next(minValue, maxValue + 1);
                evenOddPairs[i, 0] = even % 2 == 0 ? even
                    : even - 1 >= minValue ? even - 1
                    : even + 1;
                evenOddPairs[i, 1] = odd % 2 == 1 ? odd
                    : odd - 1 >= minValue ? odd - 1
                    : odd + 1;
            }

            return evenOddPairs;
        }

        public static int[,] GenerateRandomMinMaxPairs(int count, int minValue = 0, int maxValue = int.MaxValue - 1)
        {
            int[,] minMaxPairs = new int[count, 2];
            for (int i = 0; i < count; ++i)
            {
                int num1 = Rand.Next(minValue, maxValue + 1);
                int num2 = Rand.Next(minValue, maxValue + 1);
                minMaxPairs[i, 0] = Math.Min(num1, num2);
                minMaxPairs[i, 1] = Math.Max(num1, num2);
            }

            return minMaxPairs;
        }

        public static RootedTree GenerateRandomRootedTree(int vertexCount, int minChildCount, int maxChildCount)
        {
            if (minChildCount < 1 || maxChildCount < 1 || minChildCount > maxChildCount)
                throw new ArgumentException();

            var children = new List<int>[vertexCount];
            // The number of children a vertex has is random, but IDs aren't random. The root is always 0
            // and if ID1 < ID2, ID1's depth <= ID2's depth. Once an ID is added as a child, it gets in
            // line to become a parent. The tree isn't going to be very deep for any maxChildCount except
            // 1, as parents have (minChildCount + maxChildCount) / 2 children on average.
            var availableParentIDs = new Queue<int>();
            availableParentIDs.Enqueue(0);
            var availableChildIDs = new Queue<int>(Enumerable.Range(1, vertexCount - 1));

            while (true)
            {
                int parentID = availableParentIDs.Dequeue();
                var parentsChildren = children[parentID] = new List<int>();
                int childCount = Rand.Next(minChildCount, maxChildCount + 1);

                for (int i = 0; i < childCount; ++i)
                {
                    if (availableChildIDs.Count == 0)
                        return RootedTree.CreateFromExplicitChildren(vertexCount, 0, children);

                    int childID = availableChildIDs.Dequeue();
                    parentsChildren.Add(childID);
                    availableParentIDs.Enqueue(childID);
                }
            }
        }
    }
}
