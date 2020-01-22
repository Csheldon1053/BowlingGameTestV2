using System;
using System.Collections.Generic;
using System.Linq;

namespace BowlingProgram
{
    public class BowlingGame
    {
        public List<Player> Players { get; } = new List<Player>();
        public Player CurrentPlayer { get; set; }
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

            CurrentPlayer = Players.First();
        }

        public void AskForScore()
        {
            CurrentPlayer.AskForScore();
            if (CurrentPlayer.Frames.First().Scores.All(x => x != null))
            {
                CurrentPlayer = Players[(Players.IndexOf(CurrentPlayer) + 1) % Players.Count];
                Console.Out.WriteLine($"Player {Players.IndexOf(CurrentPlayer)+1}");
            }
        }
    }
}