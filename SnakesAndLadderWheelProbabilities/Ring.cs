using System;
using System.Collections.Generic;
using System.Linq;

namespace SnakesAndLadderWheelProbabilities
{
    public class Ring
    {
        public int FieldCount { get; private set; }
        public int StartIndex { get; private set; }
        public int Ladder { get; private set; }
        public int Snake { get; private set; }
        public List<int> Reverse { get; private set; }

        public Ring(int startindex, int fieldCount, int ladderFieldIndex, int snakeFieldIndex, List<int> reverseFieldIndex)
        {
            StartIndex = startindex;
            FieldCount = fieldCount;
            Ladder = ladderFieldIndex + startindex;
            Snake = snakeFieldIndex + startindex;
            Reverse = reverseFieldIndex.Select(x => x + startindex).ToList();
        }

        public bool IsLadder(int index) => Ladder  == index;
        public bool IsSnake(int index) => Snake == index;
        public bool IsReverse(int index) => Reverse.Any(reverseIndex => reverseIndex == index);

        public int Advance(int currentField, int fieldCount, bool reverse)
        {
            if (currentField >= FieldCount + StartIndex) throw new ArgumentException($"This ring has only {FieldCount} fields. Can't advance from field {currentField}");

            for (int i = 0; i < fieldCount; i++)
            {
                currentField = reverse ? currentField - 1 : currentField + 1;

                if (currentField == FieldCount + StartIndex) currentField = StartIndex;
                if (currentField < StartIndex) currentField = FieldCount + StartIndex - 1;
            }

            return currentField;
        }

        public override string ToString()
        {
            return $"{StartIndex} - {FieldCount + StartIndex - 1}";
        }

    }
}
