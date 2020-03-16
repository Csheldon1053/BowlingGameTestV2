using System;
using System.IO;
using System.Linq;
using BowlingProgram;
using NUnit.Framework;

namespace BowlingTests
{
    class TurnTests
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
            Assert.AreSame(Game.GetCurrentPlayer(), Game.Players.First());
        }

        [Test]
        public void FirstPlayerScore2OnFirstBall()
        {
            Game.GetCurrentPlayer().AddScore(2);
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
            Assert.AreEqual(2, Game.Players.First().Frames.First().Scores[0]);
        }

        [Test]
        public void FirstPlayerScore2OnBothThrows()
        {
            Game.GetCurrentPlayer().AddScore(2);
            Game.GetCurrentPlayer().AddScore(2);

            Assert.AreEqual(2, Game.Players.First().Frames.First().Scores[0]);
            Assert.AreEqual(2, Game.Players.First().Frames.First().Scores[1]);
            Assert.AreEqual(4, Game.Players.First().Frames.First().Score);
            Assert.AreSame(Game.GetCurrentPlayer(), Game.Players[1]);
        }

        [Test]
        public void Player1PlaysAfterPlayer2()
        {
            Game.GetCurrentPlayer().AddScore(2);
            Game.GetCurrentPlayer().AddScore(2);
            Game.GetCurrentPlayer().AddScore(3);
            Game.GetCurrentPlayer().AddScore(3);

            Assert.AreSame(Game.GetCurrentPlayer(), Game.Players[0]);
        }

        [Test]
        public void ScoresProgressToSecondFrame()
        {
            Assert.AreEqual(0, Game.Players[0].CurrentFrameIndex);

            Game.GetCurrentPlayer().AddScore(2);
            Game.GetCurrentPlayer().AddScore(2);
            Game.GetCurrentPlayer().AddScore(3);
            Game.GetCurrentPlayer().AddScore(3);
            Game.GetCurrentPlayer().AddScore(1);

            Assert.AreEqual(1, Game.Players[0].CurrentFrameIndex);
            Assert.AreEqual(5, Game.Players[0].Score);
            Assert.AreEqual(6, Game.Players[1].Score);
        }

        [Test]
        public void SixthThrowShouldBePlayer1IfAllScoresLessThan10()
        {
            Assert.AreEqual(0, Game.Players[0].CurrentFrameIndex);

            Game.GetCurrentPlayer().AddScore(2);
            Game.GetCurrentPlayer().AddScore(2);
            Game.GetCurrentPlayer().AddScore(3);
            Game.GetCurrentPlayer().AddScore(3);
            Game.GetCurrentPlayer().AddScore(1);

            Assert.AreEqual(1, Game.Players[0].CurrentFrameIndex);
            Assert.AreEqual(5, Game.Players[0].Score);
            Assert.AreEqual(6, Game.Players[1].Score);
            Assert.AreSame(Game.Players[0], Game.GetCurrentPlayer());
        }

        [Test]
        public void PlayerWithStrikeOnlyThrowsOnce()
        {
            Game.GetCurrentPlayer().AddScore(10);

            Assert.AreEqual(10, Game.Players[0].Frames[0].Scores[0]);
            Assert.AreSame(Game.Players[1], Game.GetCurrentPlayer());
        }        
    }
}
 