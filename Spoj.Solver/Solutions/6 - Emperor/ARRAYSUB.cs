using System;
using System.Collections.Generic;
using System.Linq;

// http://www.spoj.com/problems/ARRAYSUB/ #deque #extrema #window
// Finds the maximum for all contiguous subarrays of a given size in an array.
public static class ARRAYSUB
{
    // Looks like I ended up with the typical sliding-window deque solution. Here's an example for k = 3:
    // 1 3 2 8 4 7 2, initialize left looking dominators like {3, 2}. These dominate everything to their
    // left until a bigger dominator. Leftmost dominator is the to max value for the subarray.
    // [1 3 2] 8 4 7 2, {3, 2}
    // 1 [3 2 8] 4 7 2, {8}, lost 1 but it wasn't a dominator, gained 8 and it dominates everything, ends 3 and 2.
    // 1 3 [2 8 4] 7 2, {8, 4}, lost 3 but wasn't a dominator, gained 4 which dominates until 8.
    // 1 3 2 [8 4 7] 2, {8, 7}, lost 2 but wasn't a dominator, gained 7 which kills 4 and domaintes til 8.
    // 1 3 2 8 [4 7 2], {7, 2}, lost 8 so pop it off, gained 2 which dominates until 7.
    // I say dominate because if you're an element and there's an element in your sliding window to your right
    // that's greater than you (dominating you), you're never going to be the max for any of your remaining subarrays.
    // That dominating element to your right will always be there until you're kicked out of the window.
    // When a big number slides in it can dominate all of the dominators and be the only one left; the existing
    // dominators would be to its left so they'd never be able to be the max for any remaining subarrays.
    // We have to keep track of the dominators to the right of other, larger dominators because those other dominators
    // are going to slide out before the ones to their right, and we need to know where to pick up from. It should be
    // clear the dominators are always sorted, descending, from left to right. Dominators could be thought of recursively
    // as greatest element in subarray, followed by greatest element in subarray to that element's right, etc. O(n)
    // because we add or remove an array element from the dominators at most one time.
    public static int[] Solve(int[] array, int k)
    {
        if (k == array.Length) return new[] { array.Max() };
        if (k == 1) return array;

        // Initializing the dominators for the first subarray. Gotta have the rightmost, then the next to the left that's
        // larger than (or equal to) it, and so on. Equal to because we need the next one after an equal one is popped off.
        var leftLookingDominators = new LinkedList<int>();
        leftLookingDominators.AddFirst(array[k - 1]);

        for (int i = k - 2; i >= 0; --i)
        {
            if (array[i] >= leftLookingDominators.Last.Value)
            {
                leftLookingDominators.AddLast(array[i]);
            }
        }

        // Each iteration we're looking at the next subarray, slid over from the previous. We lose an element during the
        // slide which might've been the leftmost dominator (the leftmost dominator is the only one that can be lost). We
        // gain an element which might start dominating everything, or just some dominators until hitting some dominator to its
        // left that's greater than it, or just itself. Don't have to worry about dominators ever becoming empty, because
        // base case handled above. Even for k = 2, if there's only 1 dominator going in to the next iteration, it must be
        // the rightmost element of the previous subarray, so it's not going to get popped off the end until the next next iteration.
        int subarrayCount = array.Length - k + 1;
        int[] subarrayMaximums = new int[subarrayCount];
        subarrayMaximums[0] = leftLookingDominators.Last.Value;

        for (int i = 1, j = k; i < subarrayCount; ++i, ++j)
        {
            int lostLeftValue = array[i - 1];
            int gainedRightValue = array[j];

            if (lostLeftValue == leftLookingDominators.Last.Value)
            {
                leftLookingDominators.RemoveLast();
            }

            if (gainedRightValue > leftLookingDominators.Last.Value)
            {
                leftLookingDominators.Clear();
                leftLookingDominators.AddFirst(gainedRightValue);
            }
            else
            {
                while (gainedRightValue > leftLookingDominators.First.Value)
                {
                    leftLookingDominators.RemoveFirst();
                }
                leftLookingDominators.AddFirst(gainedRightValue);
            }

            subarrayMaximums[i] = leftLookingDominators.Last.Value;
        }

        return subarrayMaximums;
    }
}

public static class Program
{
    private static void Main()
    {
        int arrayLength = int.Parse(Console.ReadLine());
        int[] array = Array.ConvertAll(
            Console.ReadLine().Split(default(char[]), StringSplitOptions.RemoveEmptyEntries),
            int.Parse);
        int k = int.Parse(Console.ReadLine());

        Console.WriteLine(
            string.Join(" ", ARRAYSUB.Solve(array, k)));
    }
}
