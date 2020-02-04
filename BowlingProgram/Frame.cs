using System.Linq;

namespace BowlingProgram
{
    public class Frame
    {
        public Frame NextFrame;
        public int?[] Scores { get; } = new int?[2];
        public int Score
        {
            get
            {
                var total = Scores.Sum(x => x ?? 0);
                if (Scores[0] == 10)
                {
                    if (NextFrame.Scores[0] == 10)
                    {
                        total += NextFrame.Scores[0]??0 + NextFrame.NextFrame.Scores[0]??0;
                    }
                    else
                    {
                        total += NextFrame.Scores[0]??0 + NextFrame.Scores[1]??0;
                    }

                }
                else if (Scores.Sum(x => x ?? 0) == 10)
                    total += NextFrame?.Scores[0]??0;
                return total;
            }
        }
    }
}