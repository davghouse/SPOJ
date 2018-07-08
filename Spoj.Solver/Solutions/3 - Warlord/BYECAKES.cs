using System;
using System.Linq;

// https://www.spoj.com/problems/BYECAKES/ #division #experiment #optimization #trap
// Finds the minimal amount of cake ingredients to buy to use all of them completely up.
public static class BYECAKES
{
    // Find the greatest number of cakes needed to deplete any one of the starting ingredients.
    // This is the total number of cakes to bake, buying (across each ingredient) any missing
    // ingredients necessary to support that number of cakes. Pretty clear that it's minimal.
    // To find the amount to buy, subtract the amount owned from the amount needed.
    public static int[] Solve(
        int eggsOwned, int flourOwned, int sugarOwned, int milkOwned,
        int eggsPerCake, int flourPerCake, int sugarPerCake, int milkPerCake)
    {
        int totalCakesToBake = new[]
        {
            GetNumberOfCakesToDepleteIngredient(eggsOwned, eggsPerCake),
            GetNumberOfCakesToDepleteIngredient(flourOwned, flourPerCake),
            GetNumberOfCakesToDepleteIngredient(sugarOwned, sugarPerCake),
            GetNumberOfCakesToDepleteIngredient(milkOwned, milkPerCake)
        }.Max();

        return new[]
        {
            GetAmountOfIngredientsToBuy(totalCakesToBake, eggsOwned, eggsPerCake),
            GetAmountOfIngredientsToBuy(totalCakesToBake, flourOwned, flourPerCake),
            GetAmountOfIngredientsToBuy(totalCakesToBake, sugarOwned, sugarPerCake),
            GetAmountOfIngredientsToBuy(totalCakesToBake, milkOwned, milkPerCake)
        };
    }

    private static int GetNumberOfCakesToDepleteIngredient(int ingredientsOwned, int ingredientsPerCake)
        => ingredientsOwned % ingredientsPerCake == 0
        ? ingredientsOwned / ingredientsPerCake
        : ingredientsOwned / ingredientsPerCake + 1;

    private static int GetAmountOfIngredientsToBuy(int totalCakesToBake, int ingredientsOwned, int ingredientsPerCake)
        => totalCakesToBake * ingredientsPerCake - ingredientsOwned;
}

public static class Program
{
    private static void Main()
    {
        int[] line;
        while ((line = Array.ConvertAll(
            Console.ReadLine().Split(default(char[]), StringSplitOptions.RemoveEmptyEntries),
            int.Parse))[0] != -1)
        {
            int[] ingredientsToBuy = BYECAKES.Solve(
                line[0], line[1], line[2], line[3],
                line[4], line[5], line[6], line[7]);

            Console.WriteLine(
                string.Join(" ", ingredientsToBuy));
        }
    }
}
