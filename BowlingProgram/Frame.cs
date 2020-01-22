using System.Linq;

namespace BowlingProgram
{
    public class Frame
    {
        public int?[] Scores { get; } = new int?[2];
        public int Score => Scores.Sum(x => x??0);
    }
}