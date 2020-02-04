using System;
using System.Collections.Generic;
using System.Linq;

namespace BowlingProgram
{
    public class BowlingGame
    {
        public List<Player> Players { get; } = new List<Player>();

        public Player CurrentPlayer =>
            Players.First(x => x.CurrentFrameIndex == Players.Min(y => y.CurrentFrameIndex));
        //


        public void SetupPlayers()
        {
            int numberOfPlayers;
            do
            {
                Console.Out.WriteLine("How many players?");
            } while (!int.TryParse(Console.ReadLine(), out numberOfPlayers) || numberOfPlayers <= 0);

            for (var i = 0; i < numberOfPlayers; i++)
            {
                Players.Add(new Player());
            }
        }

        public void AskForScore()
        {
            var currentFrame = CurrentPlayer.CurrentFrameIndex;
            CurrentPlayer.AskForScore();
            Console.Out.WriteLine($"Player {Players.IndexOf(CurrentPlayer)+1}");
        }
    }
}