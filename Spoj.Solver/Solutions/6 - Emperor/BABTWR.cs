using System;
using System.Collections.Generic;
using System.Linq;

// https://www.spoj.com/problems/BABTWR/ #ad-hoc #dynamic-programming-1d
// Finds the height of the highest possible tower stack.
public static class BABTWR
{
    public static int Solve(Block[] blocks)
    {
        var effectiveBlocks = blocks
            .SelectMany(b => GetEffectiveBlocks(b))
            .OrderBy(b => b.BaseArea)
            .ToArray();

        // Blocks with smaller area can never have blocks with larger area placed on
        // top. We can build up the highest tower when a block is used as the base
        // by considering the highest tower for all the blocks that can fit on top of it.
        effectiveBlocks[0].HighestSupportingTower = effectiveBlocks[0].Height;
        for (int b = 1; b < effectiveBlocks.Length; ++b)
        {
            var block = effectiveBlocks[b];
            int highestFittingTower = 0;
            for (int t = 0; t < b; ++t)
            {
                var towerBaseBlock = effectiveBlocks[t];
                if (towerBaseBlock.FitsOnTopOf(block)
                    && towerBaseBlock.HighestSupportingTower > highestFittingTower)
                {
                    highestFittingTower = towerBaseBlock.HighestSupportingTower;
                }
            }

            block.HighestSupportingTower = highestFittingTower + block.Height;
        }

        return effectiveBlocks
            .Select(b => b.HighestSupportingTower)
            .Max();
    }

    // Blocks can be rotated, so from one block definition there are effectively 3 different
    // choices for the block's base area (6 faces on a block, divided by 2 as opposite faces
    // have equal area). Don't worry about some of these blocks potentially being the same.
    private static IEnumerable<Block> GetEffectiveBlocks(Block block)
    {
        yield return block;
        yield return new Block(block.Width, block.Height, block.Depth);
        yield return new Block(block.Depth, block.Width, block.Height);
    }
}

public class Block
{
    public Block(int height, int width, int depth)
    {
        Height = height;
        Width = width;
        Depth = depth;
    }

    public int Height { get; }
    public int Width { get; }
    public int Depth { get; }
    public int BaseArea => Width * Depth;

    public int HighestSupportingTower { get; set; }

    // This block fits on top of a block if its base can be rotated (2 options) to fit.
    public bool FitsOnTopOf(Block baseBlock)
        => Width < baseBlock.Width && Depth < baseBlock.Depth
        || Depth < baseBlock.Width && Width < baseBlock.Depth;
}

public static class Program
{
    private static void Main()
    {
        int blockCount;
        while ((blockCount = int.Parse(Console.ReadLine())) != 0)
        {
            var blocks = new Block[blockCount];
            for (int b = 0; b < blockCount; ++b)
            {
                string[] line = Console.ReadLine().Split();
                blocks[b] = new Block(
                    height: int.Parse(line[0]),
                    width: int.Parse(line[1]),
                    depth: int.Parse(line[2]));
            }

            Console.WriteLine(
                BABTWR.Solve(blocks));
        }
    }
}
