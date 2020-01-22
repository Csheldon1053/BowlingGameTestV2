using System;
using System.IO;
using System.Linq;
using BowlingProgram;
using NUnit.Framework;

namespace BowlingTests
{
    class TwoPlayerBowlingGameTests
    {
        [SetUp]
        public void Setup()
        {
            Game = new BowlingGame();
            var output = new StringWriter();
            Console.SetOut(output);
            var input = new StringReader("2");
            Console.SetIn(input);

            Game.SetupPlayers();
        }

        public BowlingGame Game { get; set; }

        [Test]
        public void EachPlayerHasTenFrames()
        {
            Assert.IsTrue(Game.Players.TrueForAll(x => x.Frames.Count == 10));
        }

        [Test]
        public void StartPlayerIsFirstPlayer()
        {
            Assert.AreSame(Game.CurrentPlayer, Game.Players.First());
        }

        [Test]
        public void FirstPlayerScore2OnFirstBall()
        {
            var output = new StringWriter();
            Console.SetOut(output);
            var input = new StringReader("2");
            Console.SetIn(input);

            Game.AskForScore();
            Assert.AreEqual(2, Game.Players.First().Frames.First().Scores[0]);
        }

        [Test]
        public void FirstPlayerScoreCraigOnFirstBall()
        {
            var output = new StringWriter();
            Console.SetOut(output);
            var input = new StringReader($"Craig{Environment.NewLine}2");
            Console.SetIn(input);

            Game.AskForScore();
            Assert.AreEqual(2,Game.Players.First().Frames.First().Scores[0]);
        }

        [Test]
        public void FirstPlayerScore2OnBothThrows()
        {
            var output = new StringWriter();
            Console.SetOut(output);
            var input = new StringReader("2");
            Console.SetIn(input);

            Game.AskForScore();

            input = new StringReader("2");
            Console.SetIn(input);
            Game.AskForScore();

            Assert.AreEqual(2, Game.Players.First().Frames.First().Scores[0]);
            Assert.AreEqual(2, Game.Players.First().Frames.First().Scores[1]);
            Assert.AreEqual(4, Game.Players.First().Frames.First().Score);
            Assert.AreSame(Game.CurrentPlayer, Game.Players[1]);
        }

        [Test]
        public void Player1PlaysAfterPlayer2()
        {
            var output = new StringWriter();
            Console.SetOut(output);
            var input = new StringReader("2");
            Console.SetIn(input);

            Game.AskForScore();

            input = new StringReader("2");
            Console.SetIn(input);
            Game.AskForScore();

            Assert.AreSame(Game.CurrentPlayer, Game.Players[1]);

            input = new StringReader("3");
            Console.SetIn(input);
            Game.AskForScore();

            input = new StringReader("3");
            Console.SetIn(input);
            Game.AskForScore();

            Assert.AreSame(Game.CurrentPlayer, Game.Players[0]);
        }

        [Test]
        public void ScoresProgressToSecondFrame()
        {
            var output = new StringWriter();
            Console.SetOut(output);
            var input = new StringReader("2");
            Console.SetIn(input);

            Assert.AreEqual(0, Game.Players[0].CurrentFrameIndex);

            Game.AskForScore();

            input = new StringReader("2");
            Console.SetIn(input);
            Game.AskForScore();

            input = new StringReader("3");
            Console.SetIn(input);
            Game.AskForScore();

            input = new StringReader("3");
            Console.SetIn(input);
            Game.AskForScore();

            input = new StringReader("1");
            Console.SetIn(input);
            Game.AskForScore();
            Assert.AreEqual(1, Game.Players[0].CurrentFrameIndex);
            Assert.AreEqual(5, Game.Players[0].Score);
            Assert.AreEqual(6, Game.Players[1].Score);
        }
    }
}
