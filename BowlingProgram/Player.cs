using System;
using System.Collections.Generic;
using System.Linq;

namespace BowlingProgram
{
    public class Player

    {
        public Player()
        {
            for (var i = 0; i < 10; i++)
            {
                Frames.Add(new Frame());
            }
        }
        public List<Frame> Frames { get; } = new List<Frame>();
        public int CurrentFrameIndex => Frames.FindIndex(x => x.Scores.Any(y => y == null));
        public int Score => Frames.Sum(x => x.Score);

        public void AskForScore()
        {
            int score;
            do
            {
                Console.Out.WriteLine("What was your score?");
            } while (!int.TryParse(Console.ReadLine(), out score));

            var index = Frames[CurrentFrameIndex].Scores[0] == null ? 0 : 1;
            var frameIndex = CurrentFrameIndex;
            Frames[CurrentFrameIndex].Scores[index] = score;
            Console.Out.WriteLine($"You scored {score}. Your frame score is {Frames[frameIndex].Score}. Your Total score is {Score}");
        }
    }
}