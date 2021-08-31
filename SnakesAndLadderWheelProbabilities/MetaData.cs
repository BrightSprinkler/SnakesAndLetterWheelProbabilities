using System.Text;

namespace SnakesAndLadderWheelProbabilities
{
    public class MetaData
    {
        public int Roll { get; set; }
        public bool IsCurrentlyReverse { get; set; }
        public bool IsReverse { get; set; }
        public bool IsSnake { get; set; }
        public bool IsLadder { get; set; }
        public int StartField { get; set; }
        public int StopField { get; set; }
        public string Ring { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new();

            sb.AppendLine($"Start: {StartField} - Roll: {Roll} - Stop: {StopField}");
            sb.AppendLine($"Is currently reverse: {IsCurrentlyReverse}");
            sb.AppendLine($"Ring: {Ring}");
            sb.AppendLine($"Is snake: {IsSnake}");
            sb.AppendLine($"Is ladder: {IsLadder}");
            sb.AppendLine($"Is reverse: {IsReverse}");

            return sb.ToString();
        }

    }
}
