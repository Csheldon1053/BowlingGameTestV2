using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using BowlingProgram;
using NUnit.Framework;

namespace BowlingTests
{
    public class ScoringTests
    {

        [SetUp]
        public void Setup()
        {
            Game = new BowlingGame();
            var output = new StringWriter();
            Console.SetOut(output);
            var input = new StringReader("1");
            Console.SetIn(input);

            Game.SetupPlayers();
        }

        public BowlingGame Game { get; set; }

        [Test]
        public void ScoreAddsOnceWithSpare()
        {
            Game.GetCurrentPlayer().AddScore(5);
            Game.GetCurrentPlayer().AddScore(5);
            Game.GetCurrentPlayer().AddScore(5);
            Game.GetCurrentPlayer().AddScore(4);


            Assert.AreEqual(15, Game.Players[0].Frames[0].Score);
            Assert.AreEqual(24, Game.Players[0].Score);
        }

        [Test]
        public void ScoreAddsTwiceWithStrike()
        {
            Game.GetCurrentPlayer().AddScore(10);
            Game.GetCurrentPlayer().AddScore(5);
            Game.GetCurrentPlayer().AddScore(4);


            Assert.AreEqual(19, Game.Players[0].Frames[0].Score);
            Assert.AreEqual(28, Game.Players[0].Score);
        }

        [Test]
        public void ScoreAddsTwiceWith2Strikes()
        {
            Game.GetCurrentPlayer().AddScore(10);
            Game.GetCurrentPlayer().AddScore(10);
            Game.GetCurrentPlayer().AddScore(5);


            Assert.AreEqual(25, Game.Players[0].Frames[0].Score);
            Assert.AreEqual(45, Game.Players[0].Score);
        }

        [Test]
        public void ScoreAddsTwiceWith4Strikes()
        {
            Game.GetCurrentPlayer().AddScore(10); //Player 1 strike 10 + 10 + 10
            Game.GetCurrentPlayer().AddScore(10); //frame 2 10 + 10 + 10
            Game.GetCurrentPlayer().AddScore(10); //frame 3 10 + 10 + 10
            Game.GetCurrentPlayer().AddScore(10); //Frame 4
            Game.GetCurrentPlayer().AddScore(0); //frame 5
            Game.GetCurrentPlayer().AddScore(0);


            Assert.AreEqual(30, Game.Players[0].Frames[0].Score);
            Assert.AreEqual(30, Game.Players[0].Frames[1].Score);
            Assert.AreEqual(20, Game.Players[0].Frames[2].Score);
            Assert.AreEqual(10, Game.Players[0].Frames[3].Score);
            Assert.AreEqual(90, Game.Players[0].Score);
        }

        [Test]
        public void ScoreAddStrikeSpareStrike()
        {
            Game.GetCurrentPlayer().AddScore(10);
            Game.GetCurrentPlayer().AddScore(4);
            Game.GetCurrentPlayer().AddScore(6);
            Game.GetCurrentPlayer().AddScore(10);


            Assert.AreEqual(20, Game.Players[0].Frames[0].Score);
            Assert.AreEqual(20, Game.Players[0].Frames[1].Score);
            Assert.AreEqual(10, Game.Players[0].Frames[2].Score);
            Assert.AreEqual(50, Game.Players[0].Score);
        }


        [Test]
        public void ScoreAddTwoSpares()
        {
            Game.GetCurrentPlayer().AddScore(4);
            Game.GetCurrentPlayer().AddScore(6);
            Game.GetCurrentPlayer().AddScore(3);
            Game.GetCurrentPlayer().AddScore(7);
            Game.GetCurrentPlayer().AddScore(1);

            Assert.AreEqual(13, Game.Players[0].Frames[0].Score);
            Assert.AreEqual(11, Game.Players[0].Frames[1].Score);
            Assert.AreEqual(1, Game.Players[0].Frames[2].Score);
            Assert.AreEqual(25, Game.Players[0].Score);

        }

        [Test]
        public void FrameTenHas3Scores()
        {
            Assert.AreEqual(3, Game.Players[0].Frames[9].Scores.Length);
        }

        [Test]
        public void PerfectGame()
        {
            Game.GetCurrentPlayer().AddScore(10);//30
            Game.GetCurrentPlayer().AddScore(10);//30
            Game.GetCurrentPlayer().AddScore(10);//30
            Game.GetCurrentPlayer().AddScore(10);//30
            Game.GetCurrentPlayer().AddScore(10);//30
            Game.GetCurrentPlayer().AddScore(10);//30
            Game.GetCurrentPlayer().AddScore(10);//30
            Game.GetCurrentPlayer().AddScore(10);//30
            Game.GetCurrentPlayer().AddScore(10);//30
            Game.GetCurrentPlayer().AddScore(10);//10
            Game.GetCurrentPlayer().AddScore(10);//10
            Game.GetCurrentPlayer().AddScore(10);//10

            Assert.AreEqual(300, Game.Players[0].Score);
        }
    }
}
