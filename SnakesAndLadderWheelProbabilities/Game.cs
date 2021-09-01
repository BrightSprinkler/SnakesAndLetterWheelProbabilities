using System;
using System.Collections.Generic;
using System.Linq;

namespace SnakesAndLadderWheelProbabilities
{
    public enum Rings
    {
        Inner,
        Middle,
        Outer
    }

    public class Game
    {
        public Ring InnerRing { get; private set; }
        public Ring MiddleRing { get; private set; }
        public Ring OuterRing { get; private set; }

        public int DiceMin { get; private set; }
        public int DiceMax { get; private set; }

        private Ring CurrentRing { get; set; }

        private Random Random { get; set; }

        public Game()
        {
            InnerRing = new(0, 8, 0, 4, new() { 2, 6 });
            MiddleRing = new(8, 16, 0, 8, new() { 4, 12 });
            OuterRing = new(24, 24, 0, 12, new() { 6, 18 });
            Random = new();
        }

        public List<MetaData> Simulate(int diceMin, int diceMax)
        {
            DiceMin = diceMin;
            DiceMax = diceMax;

            List<MetaData> metaDatas = new();
            int currentField = 0;
            bool reverse = false;
            CurrentRing = InnerRing;

            while (currentField != OuterRing.FieldCount + OuterRing.StartIndex + 1)
            {
                int roll = RollDice();
                MetaData metaData = new();

                metaData.StartField = currentField;
                metaData.IsCurrentlyReverse = reverse;
                metaData.Roll = roll;

                if (CurrentRing == InnerRing)
                    metaData.Ring = Rings.Inner;
                else if (CurrentRing == OuterRing)
                    metaData.Ring = Rings.Outer;
                else
                    metaData.Ring = Rings.Middle;

                currentField = CurrentRing.Advance(currentField, roll, reverse);
                metaData.StopField = currentField;

                if (CurrentRing.IsLadder(currentField))
                {
                    metaData.IsLadder = true;

                    if (CurrentRing == OuterRing)
                        currentField = OuterRing.FieldCount + OuterRing.StartIndex + 1;
                    else
                    {
                        if (CurrentRing == InnerRing)
                            CurrentRing = MiddleRing;
                        else if (CurrentRing == MiddleRing)
                            CurrentRing = OuterRing;

                        currentField = CurrentRing.Ladder;
                    }
                }

                if (CurrentRing.IsSnake(currentField))
                {
                    metaData.IsSnake = true;

                    if (CurrentRing != InnerRing)
                    {
                        if (CurrentRing == MiddleRing)
                            CurrentRing = InnerRing;
                        else
                            CurrentRing = MiddleRing;

                        currentField = CurrentRing.Snake;
                    }
                }

                if (CurrentRing.IsReverse(currentField))
                {
                    metaData.IsReverse = true;
                    reverse = !reverse;
                }

                metaDatas.Add(metaData);
            }

            metaDatas.Add(new() { IsCurrentlyReverse = reverse, StartField = metaDatas.Last().StopField, StopField = currentField });

            return metaDatas;
        }

        private int RollDice() => Random.Next(DiceMin, DiceMax + 1);

    }
}
