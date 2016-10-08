using System;
using System.Linq;

// Too many problems have improperly formatted input... random whitespace,
// missing newlines... things that C# doesn't deal well with by default.
// It's good to verify the input format on an alt account so you don't get frustrated.
// Currently verifying for: http://www.spoj.com/problems/ABCDEF/
public static class InputVerifier
{
    private static void Main()
    {
        int[] nums = new int[int.Parse(Console.ReadLine())];

        if (nums.Length < 1 || nums.Length > 100)
            throw new FormatException();

        for (int i = 0; i < nums.Length; ++i)
        {
            nums[i] = int.Parse(Console.ReadLine());
        }

        if (nums.Any(n => n < -30000 || n > 30000))
            throw new FormatException();
    }
}
