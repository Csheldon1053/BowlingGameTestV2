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
            Game.CurrentPlayer.AddScore(2);
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
            Game.CurrentPlayer.AddScore(2);
            Game.CurrentPlayer.AddScore(2);

            Assert.AreEqual(2, Game.Players.First().Frames.First().Scores[0]);
            Assert.AreEqual(2, Game.Players.First().Frames.First().Scores[1]);
            Assert.AreEqual(4, Game.Players.First().Frames.First().Score);
            Assert.AreSame(Game.CurrentPlayer, Game.Players[1]);
        }

        [Test]
        public void Player1PlaysAfterPlayer2()
        {
            Game.CurrentPlayer.AddScore(2);
            Game.CurrentPlayer.AddScore(2);
            Game.CurrentPlayer.AddScore(3);
            Game.CurrentPlayer.AddScore(3);

            Assert.AreSame(Game.CurrentPlayer, Game.Players[0]);
        }

        [Test]
        public void ScoresProgressToSecondFrame()
        {
            Assert.AreEqual(0, Game.Players[0].CurrentFrameIndex);

            Game.CurrentPlayer.AddScore(2);
            Game.CurrentPlayer.AddScore(2);
            Game.CurrentPlayer.AddScore(3);
            Game.CurrentPlayer.AddScore(3);
            Game.CurrentPlayer.AddScore(1);

            Assert.AreEqual(1, Game.Players[0].CurrentFrameIndex);
            Assert.AreEqual(5, Game.Players[0].Score);
            Assert.AreEqual(6, Game.Players[1].Score);
        }

        [Test]
        public void SixthThrowShouldBePlayer1IfAllScoresLessThan10()
        {
            Assert.AreEqual(0, Game.Players[0].CurrentFrameIndex);

            Game.CurrentPlayer.AddScore(2);
            Game.CurrentPlayer.AddScore(2);
            Game.CurrentPlayer.AddScore(3);
            Game.CurrentPlayer.AddScore(3);
            Game.CurrentPlayer.AddScore(1);

            Assert.AreEqual(1, Game.Players[0].CurrentFrameIndex);
            Assert.AreEqual(5, Game.Players[0].Score);
            Assert.AreEqual(6, Game.Players[1].Score);
            Assert.AreSame(Game.Players[0], Game.CurrentPlayer);
        }

        [Test]
        public void PlayerWithStrikeOnlyThrowsOnce()
        {
            Game.CurrentPlayer.AddScore(10);

            Assert.AreEqual(10, Game.Players[0].Frames[0].Scores[0]);
            Assert.AreSame(Game.Players[1], Game.CurrentPlayer);
        }

        [Test]
        public void ScoreAddsOnceWithSpare()
        {
            Game.CurrentPlayer.AddScore(5);
            Game.CurrentPlayer.AddScore(5);
            Game.CurrentPlayer.AddScore(0);
            Game.CurrentPlayer.AddScore(0);
            Game.CurrentPlayer.AddScore(5);
            Game.CurrentPlayer.AddScore(4);


            Assert.AreEqual(15, Game.Players[0].Frames[0].Score);
            Assert.AreEqual(24, Game.Players[0].Score);
        }

        [Test]
        public void ScoreAddsTwiceWithStrike()
        {
            Game.CurrentPlayer.AddScore(10);
            Game.CurrentPlayer.AddScore(0);
            Game.CurrentPlayer.AddScore(0);
            Game.CurrentPlayer.AddScore(5);
            Game.CurrentPlayer.AddScore(4);


            Assert.AreEqual(19, Game.Players[0].Frames[0].Score);
            Assert.AreEqual(28, Game.Players[0].Score);
        }

        [Test]
        public void ScoreAddsTwiceWith2Strikes()
        {
            Game.CurrentPlayer.AddScore(10);
            Game.CurrentPlayer.AddScore(0);
            Game.CurrentPlayer.AddScore(0);
            Game.CurrentPlayer.AddScore(10);
            Game.CurrentPlayer.AddScore(0);
            Game.CurrentPlayer.AddScore(0);
            Game.CurrentPlayer.AddScore(5);


            Assert.AreEqual(25, Game.Players[0].Frames[0].Score);
            Assert.AreEqual(45, Game.Players[0].Score);
        }

        [Test]
        public void ScoreAddsTwiceWith4Strikes()
        {
            Game.CurrentPlayer.AddScore(10);
            Game.CurrentPlayer.AddScore(0);
            Game.CurrentPlayer.AddScore(0);
            Game.CurrentPlayer.AddScore(10);
            Game.CurrentPlayer.AddScore(0);
            Game.CurrentPlayer.AddScore(0);
            Game.CurrentPlayer.AddScore(10);
            Game.CurrentPlayer.AddScore(0);
            Game.CurrentPlayer.AddScore(0);
            Game.CurrentPlayer.AddScore(10);
            Game.CurrentPlayer.AddScore(0);
            Game.CurrentPlayer.AddScore(0);
            Game.CurrentPlayer.AddScore(10);
            Game.CurrentPlayer.AddScore(0);
            Game.CurrentPlayer.AddScore(0);
            Game.CurrentPlayer.AddScore(10);


            Assert.AreEqual(30, Game.Players[0].Frames[0].Score);
            Assert.AreEqual(30, Game.Players[0].Frames[1].Score);
            Assert.AreEqual(20, Game.Players[0].Frames[2].Score);
            Assert.AreEqual(10, Game.Players[0].Frames[3].Score);
            Assert.AreEqual(90, Game.Players[0].Score);
        }
    }
}
