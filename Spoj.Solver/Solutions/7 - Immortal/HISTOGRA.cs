using System;
using System.Collections.Generic;
using System.Text;

// 1805 http://www.spoj.com/problems/HISTOGRA/ Largest Rectangle in a Histogram
// Finds the largest rectangle (by area) in a histogram (rectangles w/ a common base).
public static class HISTOGRA
{
    // I looked at times after debating (but not implementing) divide and conquer and O(n^2) approaches, and
    // figured there must be an O(n) solution. Not sure if I got lucky thinking of a solution, I could imagine
    // it being a lot more frustrating. Subconsciously at least, the experience of ARRAYSUB and HOTELS helped me.

    // I'll use the word block to mean a maximal rectangle within the histogram. The height of a block is the same
    // as the height of the shortest rectangle it spans. Maximal means both that we can't increase the height
    // (as there's some rectangle we're spanning that's too short), and we can't increase the width (as the rectangle
    // to the left of start and right of end are both too short). Obvious that if you're not a block, you can't be
    // the largest rectangle in the histogram--since we could just increase the height or the width and make a bigger
    // one. The procedure I'm about to outline allows us to compute the area of every block in linear time, and taking
    // the max of those areas gives us the biggest rectangle in the histogram. Maybe it's useful to note that there's
    // indeed a linear number of blocks. Every block has some shortest rectangles. Every rectangle can be the shortest
    // rectangle for only a single block. To see the latter, assume there's a rectangle which is the shortest rectangle
    // for two different blocks. Well, the blocks are the same height and connected through that rectangle, so we could
    // increase the area of either one by grabbing area from the other, contradicting the fact that all blocks are maximal.
    // There's at most n shortest rectangles to go around, so there can be at most n blocks.

    // As an example of the O(n) procedure, consider the following histogram:
    //         |
    //     | | |
    // | | | | | |   |
    // | | | | | | | |
    // 1 2 3 4 5 6 7 8
    // The stack will have the height and the start index of the blocks we're building up while traversing the rectangles.
    // Iterate over the rectangles up to the 5th, pushing the following blocks onto the stack (growing downwards):
    // height 2, index 1
    // height 3, index 3
    // height 4, index 5

    // We're about to look at the 6th rectangle, and we know a height 4 block starts at index 5, until index 5. A height 3
    // block starts at index 3, until index 5. A height 2 block starts at index 1, until index 5. Now we look at the 6th
    // rectangle, and it's of height 2. Looking into the stack, we see the height 4 block. This block is now over, since
    // the 6th rectangle isn't tall enough to continue it. So we pop it off, tally its area as 4 * (death index - start index)
    // = 4 * 1, and look at the next block on the stack. It's height 3, which the 6th rectangle isn't tall enough to continue,
    // so we pop it off and calculate its area as 3 * (death index - start index) = 3 * 3. The next block on the stack is
    // height 2, and the 6th rectangle is high enough to continue it, so we just leave it there and continue onto the next rectangle.
    // The stack now has a single block, height 2, index 1.

    // Now we look at the 7th rectangle. It's height 1, and here we see there's some complication. Looking into the stack, the
    // height 2 block is now over since the 7th rectangle isn't tall enough to continue it, so we pop it off and tally its area as
    // 2 * (death index - start index) = 2 * 6. There's nothing on the stack now, nothing for the 7th rectangle to continue, so
    // we look at what was last popped off (definitely taller than the rectangle we're considering), and continue from that, with a
    // new height gotten from the current rectangle. So now the stack has a single block, height 1, index 1.

    // In summary, when we visit a rectangle we need to:
    // If there's nothing on the stack, push a new block w/ height and start index of the rectangle.
    // Else if the rectangle is the same height as the top of the stack, do nothing.
    // Else if the rectangle is taller than the top of the stack, push a new block w/ height and start index of the rectangle.
    // Else if the rectangle is shorter than the top of the stack, pop blocks and tally areas (now that we know their end index
    // = death index - 1) until it's not. Now that that's done, if it's the same height as the top of the stack, do nothing.
    // If it's taller than the top of the stack, push a new block w/ height same as the rectangle, BUT start index equal to the
    // start index of the last block popped off the stack, since we know everything's tall enough to continue until then.

    // We'll have to simulate death of everything still on the stack once we reach the end of the rectangles array. Just pretend
    // there's an extra 0 height rectangle. And we can push a 0 height block onto the stack at the start, with height 0 and index -1,
    // to get rid of the nothing-on-the-stack checks. This doesn't complicate things because the routine laid out handles height 0 blocks
    // without any special treatment.

    // This process is O(n) because there can be no more blocks pushed onto the stack than there are rectangles in the histogram, and blocks
    // only get pushed onto and popped off of the stack a single time.
    public static long Solve(int rectangleCount, int[] rectangleHeights)
    {
        long maximumArea = 0;

        var blocks = new Stack<Block>();
        blocks.Push(new Block(0, -1));

        for (int index = 0; index <= rectangleCount; ++index)
        {
            int rectangleHeight = index < rectangleCount ? rectangleHeights[index] : 0;
            Block topBlock = blocks.Peek();

            if (rectangleHeight == topBlock.Height)
            {
                continue;
            }
            else if (rectangleHeight > topBlock.Height)
            {
                blocks.Push(new Block(rectangleHeight, index));
            }
            else // rectangleHeight < topBlockHeight
            {
                Block previousTopBlock = null;
                while (rectangleHeight < topBlock.Height)
                {
                    maximumArea = Math.Max(maximumArea, (long)topBlock.Height * (index - topBlock.StartIndex));
                    previousTopBlock = blocks.Pop();
                    topBlock = blocks.Peek();
                }

                if (rectangleHeight == topBlock.Height)
                {
                    continue;
                }
                else if (rectangleHeight > topBlock.Height)
                {
                    blocks.Push(new Block(rectangleHeight, previousTopBlock.StartIndex));
                }
            }
        }

        return maximumArea;
    }

    private class Block
    {
        public Block(int height, int startIndex)
        {
            Height = height;
            StartIndex = startIndex;
        }

        public int Height { get; set; }
        public int StartIndex { get; set; }
    }
}

public static class Program
{
    private static void Main()
    {
        var output = new StringBuilder();

        string[] line;
        while((line = Console.ReadLine().Split())[0] != "0")
        {
            int rectangleCount = int.Parse(line[0]);
            int[] rectangleHeights = new int[rectangleCount];
            for (int i = 0; i < rectangleCount; ++i)
            {
                rectangleHeights[i] = int.Parse(line[i + 1]);
            }

            output.Append(
                HISTOGRA.Solve(rectangleCount, rectangleHeights));
            output.AppendLine();
        }

        Console.Write(output);
    }
}
