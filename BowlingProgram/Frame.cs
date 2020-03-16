using System.Linq;

namespace BowlingProgram
{
    public class Frame : GameConfig
    {
        public Frame NextFrame;
        public int?[] Scores { get; set; } = new int?[2];
        public int Score
        {
            get
            {
                var total = Scores.Sum(x => x ?? 0);
                if (Scores[0] == MAX_FRAME_SCORE)
                {
                    if (NextFrame?.Scores[0] == MAX_FRAME_SCORE && NextFrame?.NextFrame != null)
                    {
                        total += (NextFrame?.Scores[0]??0 ) + (NextFrame?.NextFrame?.Scores[0]??0);
                    }

                    else
                    {
                        total += (NextFrame?.Scores[0] ?? 0) + (NextFrame?.Scores[1] ?? 0);
                    }

                }
                else if (Scores.Sum(x => x ?? 0) == MAX_FRAME_SCORE)
                    total += NextFrame?.Scores[0]??0;
                return total;
            }
        }
    }
}