using System;

// Too many problems have improperly formatted input... random whitespace,
// missing newlines... things that C# doesn't deal well with by default.
// It's good to verify the input format on an alt account so you don't get frustrated.
// Currently verifying for: http://www.spoj.com/problems/FACEFRND/
public static class InputVerifier
{
    private static void Main()
    {
        int friendCount = int.Parse(Console.ReadLine());

        if (friendCount < 1 || friendCount > 100)
            throw new FormatException();

        for (int i = 0; i < friendCount; ++i)
        {
            int[] line = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            int friendID = line[0];
            int friendFriendCount = line[1];

            if (friendFriendCount < 1 || friendFriendCount > 100)
                throw new FormatException();

            if (line.Length != friendFriendCount + 2)
                throw new FormatException();
        }
    }
}