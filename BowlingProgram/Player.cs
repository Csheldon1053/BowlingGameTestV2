using System;
using System.Collections.Generic;
using System.Linq;

namespace BowlingProgram
{
    public class Player

    {
        public Player()
        {
            Frames.Add(new Frame());
            for (var i = 1; i < 10; i++)
            {
                var currentFrame = new Frame();
                Frames[i - 1].NextFrame = currentFrame;
                Frames.Add(currentFrame);
            }
        }
        public List<Frame> Frames { get; } = new List<Frame>();
        public int CurrentFrameIndex => Frames.FindIndex(x => x.Scores.Any(y => y == null) && x.Score < 10);
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
            var index = Frames[CurrentFrameIndex].Scores[0] == null ? 0 : 1;
            if (index == 0 && score > 10) score = 10;
            if (index == 1 && Frames[CurrentFrameIndex].Scores[0] + score > 10)
                score = 10 - (int)Frames[CurrentFrameIndex].Scores[0];
            Frames[CurrentFrameIndex].Scores[index] = score;
        }
    }
}