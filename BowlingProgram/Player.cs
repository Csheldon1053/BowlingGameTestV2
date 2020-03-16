using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BowlingProgram
{
    public class Player: GameConfig
    {
        private readonly int playerNumber;

        public Player(int playerNumber)
        {
            Frames.Add(new Frame());
            for (var i = 1; i < NUMBER_FRAMES; i++)
            {
                var currentFrame = new Frame();
                Frames[i - 1].NextFrame = currentFrame;
                Frames.Add(currentFrame);
            }
            Frames.Last().Scores = new int?[3];
            this.playerNumber = playerNumber;
        }
        public List<Frame> Frames { get; } = new List<Frame>();
        public int CurrentFrameIndex => Frames.FindIndex(x => x.Scores.Any(y => y == null) && (x.Score < MAX_FRAME_SCORE || x.NextFrame == null));
        public int Score => Frames.Sum(x => x.Score);

        public void AskForScore()
        {
            int score;
            do
            {
                Console.Out.WriteLine("What was your score?");
            } while (!int.TryParse(Console.ReadLine(), out score));
            var frameIndex = CurrentFrameIndex;
            AddScore(score);
            Console.Out.WriteLine($"You scored {score}. Your frame score is {Frames[frameIndex].Score}. Your Total score is {Score}");
        }

        public void AddScore(int score)
        {
            var index = 0;
            while (Frames[CurrentFrameIndex].Scores[index] != null && index < Frames[CurrentFrameIndex].Scores.Length)
            {
                index++;
            }

            if (Frames[CurrentFrameIndex].Equals(Frames.Last()))
            {
                if (score > MAX_FRAME_SCORE) score = MAX_FRAME_SCORE;
            }
            else
            {
                if (index == 0 && score > MAX_FRAME_SCORE) score = MAX_FRAME_SCORE;
                if (index == 1 && Frames[CurrentFrameIndex].Scores[0] + score > MAX_FRAME_SCORE)
                    score = MAX_FRAME_SCORE - (int)Frames[CurrentFrameIndex].Scores[0];
            }
            Frames[CurrentFrameIndex].Scores[index] = score;
        }

        public override string ToString()
        {
            // | 1      |X  |9/ |5-
            // |        |20 |35 |40
            // 
            var sb = new StringBuilder();
            sb.Append($"| {playerNumber} |");
            foreach(var frame in Frames)
            {
                string frameOutput = string.Empty;
                foreach(var score in frame.Scores)
                {
                    var output = string.Empty;
                    switch (score)
                    {
                        case null:
                            output = " ";
                            break;
                        case 10:
                            output = "X";
                            break;
                        case 0:
                            output = "-";
                            break;
                        default:
                            output = score.ToString();
                            break;
                    }
                    frameOutput += output;
                }
                if (frame.Scores[0] > 0 && (frame.Scores[0] + frame.Scores[1]) == MAX_FRAME_SCORE)
                {
                    var result = frameOutput.ToCharArray();
                    result[1] = '/';
                    frameOutput = new string(result);
                }
                sb.Append(frameOutput.PadRight(3));
                sb.Append("|");
            }
            sb.Append(Environment.NewLine);
            sb.Append($"|   |");
            foreach (var frame in Frames)
            {
                if (frame.Scores.All(x => x == null))
                    sb.Append("   |");
                sb.Append(Frames.Take(Frames.IndexOf(frame)+1).Sum(x => x.Score).ToString().PadRight(3));
                sb.Append("|");
            }
            sb.Append(Environment.NewLine);
            sb.Append("==============================================================================================================");
            sb.Append(Environment.NewLine);
            return sb.ToString();
        }
    }
}