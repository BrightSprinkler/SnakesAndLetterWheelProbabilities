using System;
using System.Collections.Generic;
using System.Linq;

namespace SnakesAndLadderWheelProbabilities
{
    class Program
    {
        static void Main(string[] args)
        {
            Roll(1, 6, 100000, "D6");
        }

        private static void Roll(int min, int max, int gameCount, string dice)
        {
            Game game = new();

            List<int> rollCount = new();
            for (int i = 0; i < gameCount; i++)
                rollCount.Add(game.Simulate(min, max).Count);

            Console.WriteLine($"{dice} - {gameCount} games");
            PrintRollCountData(rollCount);
        }

        private static void PrintRollCountData(List<int> rolls)
        {
            Console.WriteLine($"Completed after average {rolls.Average()} rolls.");
            Console.WriteLine($"Completed after min {rolls.Min()} rolls.");
            Console.WriteLine($"Completed after max {rolls.Max()} rolls.");
            Console.WriteLine();
            for (int i = 0; i < 100; i += 10) PrintRangeData(i, i + 10, rolls);
            Console.WriteLine();
            for (int i = 0; i <= 1000; i += 100) PrintRangeData(i, i + 100, rolls);
            Console.WriteLine();
        }

        private static void PrintRangeData(int min, int max, List<int> rolls)
        {
            int count = rolls.Where(x => x >= min && x < max).Count();

            Console.WriteLine($"Games between {min} and {max} Rolls: {count} - {((decimal)count / (decimal)rolls.Count()) * 100} %");
        }

    }
}
