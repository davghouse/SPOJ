using System;

// https://www.spoj.com/problems/MISERMAN/ #dynamic-programming-2d #path-optimization #trap
// Finds the cheapest way to take buses between two cities, with constrained movement.
public static class MISERMAN
{
    // Basically the same as BYTESM2. Cheapest way to get to the final city is fare of final bus we use
    // to get there, plus cheapest way to get to that final bus, knowing we can arrive at it by taking
    // any one of the adjacent buses in the previous city (so recursively find the cheapest way to get to
    // those buses). So the DP array will hold cheapest way to get to the next city, using the bus at that
    // position in the array. The hardest part is understanding what we're calculating.
    public static int Solve(int cityCount, int busCount, int[,] busFares)
    {
        int[,] cheapestWayToGetToTheNextCityUsingThisBus = new int[cityCount, busCount];

        for (int b = 0; b < busCount; ++b)
        {
            cheapestWayToGetToTheNextCityUsingThisBus[0, b] = busFares[0, b];
        }

        for (int c = 1; c < cityCount; ++c)
        {
            for (int b = 0; b < busCount; ++b)
            {
                int costToTakeLeftBusHere = b > 0 ? cheapestWayToGetToTheNextCityUsingThisBus[c - 1, b - 1] : int.MaxValue;
                int costToTakeMiddleBusHere = cheapestWayToGetToTheNextCityUsingThisBus[c - 1, b];
                int costToTakeRightBusHere = b < busCount - 1 ? cheapestWayToGetToTheNextCityUsingThisBus[c - 1, b + 1] : int.MaxValue;

                cheapestWayToGetToTheNextCityUsingThisBus[c, b] = busFares[c, b]
                    + Math.Min(Math.Min(costToTakeLeftBusHere, costToTakeMiddleBusHere), costToTakeRightBusHere);
            }
        }

        int cheapestWayToGetToTheDestinationCity = int.MaxValue;
        for (int b = 0; b < busCount; ++b)
        {
            cheapestWayToGetToTheDestinationCity = Math.Min(
                cheapestWayToGetToTheDestinationCity, cheapestWayToGetToTheNextCityUsingThisBus[cityCount - 1, b]);
        }

        return cheapestWayToGetToTheDestinationCity;
    }
}

public static class Program
{
    private static void Main()
    {
        string[] line = Console.ReadLine().Split(default(char[]), StringSplitOptions.RemoveEmptyEntries);
        int cityCount = int.Parse(line[0]);
        int busCount = int.Parse(line[1]);

        int[,] busFares = new int[cityCount, busCount];

        for (int c = 0; c < cityCount; ++c)
        {
            line = Console.ReadLine().Split(default(char[]), StringSplitOptions.RemoveEmptyEntries);

            for (int b = 0; b < busCount; ++b)
            {
                busFares[c, b] = int.Parse(line[b]);
            }
        }

        Console.WriteLine(
            MISERMAN.Solve(cityCount, busCount, busFares));
    }
}
