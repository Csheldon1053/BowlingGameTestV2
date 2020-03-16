using System;
using System.Collections.Generic;
using System.Linq;

namespace BowlingProgram
{
    public class BowlingGame
    {
        public List<Player> Players { get; } = new List<Player>();

        public Player GetCurrentPlayer()
        {
            if(Players.Any(x => x.CurrentFrameIndex >= 0))
                return Players.FirstOrDefault(x => x.CurrentFrameIndex == Players.Where(x => x.CurrentFrameIndex >= 0).Min(y => y.CurrentFrameIndex));
            return null;
        }

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
                Players.Add(new Player(i));
            }
        }

        public void AskForScore()
        {
            var currentFrame = GetCurrentPlayer().CurrentFrameIndex;
            GetCurrentPlayer().AskForScore();
            Console.Clear();
            // ==============================================================================================================
            // | 1      |X  |9/ |5-
            // |        |20 |35 |40
            // 
            Console.WriteLine("==============================================================================================================");
            Players.ForEach(x => Console.Write(x.ToString()));

            if (GetCurrentPlayer() != null)
                Console.Out.WriteLine($"Player {Players.IndexOf(GetCurrentPlayer()) +1}");
        }

        public bool GameRunning => Players.Any(x => x.CurrentFrameIndex != -1);
    }

    public class GameConfig
    {
        protected const int NUMBER_FRAMES = 2;
        protected const int MAX_FRAME_SCORE = 10;
    }
}