using System;
using System.Collections.Generic;
using System.Linq;

namespace Spoj.Library.Helpers
{
    public static class IEnumerableHelper
    {
        public static bool SetEqual<T>(this IEnumerable<T> first, IEnumerable<T> second)
            => !first.Except(second).Any()
            && !second.Except(first).Any();

        public static int[] MergeSortedArrays(int[] firstArray, int[] secondArray)
        {
            int[] mergedArray = new int[firstArray.Length + secondArray.Length];
            int mergedArrayIndex = 0;
            int firstArrayIndex = 0;
            int secondArrayIndex = 0;

            while (firstArrayIndex < firstArray.Length)
            {
                if (secondArrayIndex == secondArray.Length)
                {
                    Array.Copy(
                        sourceArray: firstArray,
                        sourceIndex: firstArrayIndex,
                        destinationArray: mergedArray,
                        destinationIndex: mergedArrayIndex,
                        length: firstArray.Length - firstArrayIndex);

                    return mergedArray;
                }

                if (firstArray[firstArrayIndex] < secondArray[secondArrayIndex])
                {
                    mergedArray[mergedArrayIndex++] = firstArray[firstArrayIndex++];
                }
                else
                {
                    mergedArray[mergedArrayIndex++] = secondArray[secondArrayIndex++];
                }
            }

            Array.Copy(
                sourceArray: secondArray,
                sourceIndex: secondArrayIndex,
                destinationArray: mergedArray,
                destinationIndex: mergedArrayIndex,
                length: secondArray.Length - secondArrayIndex);

            return mergedArray;
        }

        // NOTE: doesn't support arrays with duplicate values, because .NET's BinarySearch
        // doesn't guarantee anything about the index of the duplicate found.
        public static int CountElementsBetween(this int[] sortedArray, int lowerBound, int upperBound)
        {
            // The index of the first value >= lowerBound, or array length if all smaller.
            int rangeStartIndex = Array.BinarySearch(sortedArray, lowerBound);
            rangeStartIndex = rangeStartIndex < 0 ? ~rangeStartIndex : rangeStartIndex;
            if (rangeStartIndex == sortedArray.Length)
                return 0;

            // The index of the last value <= upperBound, or -1 if all larger. This index
            // can be at most 1 less than rangeStartIndex, when the value @ rangeStartIndex
            // is greater than both lowerBound and upperBound. 0 is returned as desired then.
            int rangeEndIndex = Array.BinarySearch(
                // Save some work by starting the search at rangeStartIndex.
                sortedArray, rangeStartIndex, sortedArray.Length - rangeStartIndex, upperBound);
            rangeEndIndex = rangeEndIndex < 0 ? ~rangeEndIndex - 1 : rangeEndIndex;

            return rangeEndIndex - rangeStartIndex + 1;
        }
    }
}
