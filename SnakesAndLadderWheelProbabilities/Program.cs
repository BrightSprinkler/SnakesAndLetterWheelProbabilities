using System;
using System.Collections.Generic;
using System.Linq;

namespace SnakesAndLadderWheelProbabilities
{
    class Program
    {
        static void Main(string[] args)
        {
            Roll(1, 6, 1000000, "D6");
        }

        private static void Roll(int min, int max, int gameCount, string dice)
        {
            Game game = new();

            List<MetaData> rolls = new();
            List<int> rollCount = new();
            for (int i = 0; i < gameCount; i++)
            {
                Console.WriteLine($"Game number {i + 1} / {gameCount}");
                List<MetaData> result = game.Simulate(min, max);
                rolls.AddRange(result);
                rollCount.Add(result.Count);
            }

            Console.WriteLine($"{dice} - {gameCount} games");
            PrintRollCountData(rollCount);

            Console.WriteLine($"Rolls in inner ring: {rolls.Where(x => x.Ring == Rings.Inner).Count()}");
            Console.WriteLine($"Rolls in middle ring: {rolls.Where(x => x.Ring == Rings.Middle).Count()}");
            Console.WriteLine($"Rolls in outer ring: {rolls.Where(x => x.Ring == Rings.Outer).Count()}");
            Console.WriteLine();
            Console.WriteLine($"Times reversed: {rolls.Where(x => x.IsReverse).Count()}");
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
