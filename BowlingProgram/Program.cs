using System;

namespace BowlingProgram
{
    class Program
    {
        static void Main()
        {
            var game = new BowlingGame();
            game.SetupPlayers();
            game.AskForScore();
            game.AskForScore();
            game.AskForScore();
            game.AskForScore();
            game.AskForScore();
            game.AskForScore();
        }
    }
}
