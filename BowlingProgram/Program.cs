using System;

namespace BowlingProgram
{
    class Program
    {
        static void Main()
        {
            var game = new BowlingGame();
            game.SetupPlayers();
            while (game.GameRunning)
            {
                game.AskForScore();
            }
        }
    }
}
