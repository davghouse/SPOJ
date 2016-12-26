using System;
using System.Collections.Generic;
using System.Linq;

// http://www.spoj.com/problems/FACEFRND/ #ad-hoc #hashset
// Finds friends of someone's friends given their friends and their friends' friends.
public static class FACEFRND
{
    public static int SolveWithHashSets(int friendCount, int[][] friendDefinitions)
    {
        var friends = new HashSet<int>();
        for (int f = 0; f < friendCount; ++f)
        {
            friends.Add(friendDefinitions[f][0]);
        }

        var friendsOfFriends = new HashSet<int>();
        for (int f = 0; f < friendCount; ++f)
        {
            int friendsOfFriendsCount = friendDefinitions[f][1];
            for (int fof = 0; fof < friendsOfFriendsCount; ++fof)
            {
                int friendOfFriendID = friendDefinitions[f][fof + 2];

                if (!friends.Contains(friendOfFriendID))
                {
                    friendsOfFriends.Add(friendOfFriendID);
                }
            }
        }

        return friendsOfFriends.Count;
    }

    public static int SolveWithExcept(int friendCount, int[][] friendDefinitions)
    {
        var friends = new List<int>();
        var friendsOfFriends = new List<int>();

        for (int f = 0; f < friendCount; ++f)
        {
            friends.Add(friendDefinitions[f][0]);

            int friendsOfFriendsCount = friendDefinitions[f][1];
            for (int fof = 0; fof < friendsOfFriendsCount; ++fof)
            {
                friendsOfFriends.Add(friendDefinitions[f][fof + 2]);
            }
        }

        return friendsOfFriends.Except(friends).Count();
    }
}

public static class Program
{
    private static void Main()
    {
        int friendCount = int.Parse(Console.ReadLine());
        var friendDefinitions = new int[friendCount][];
        for (int f = 0; f < friendCount; ++f)
        {
            friendDefinitions[f] = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        }

        Console.WriteLine(
            FACEFRND.SolveWithExcept(friendCount, friendDefinitions));
    }
}
