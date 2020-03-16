using System;
using System.IO;
using BowlingProgram;
using NUnit.Framework;

namespace BowlingTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            Game = new BowlingGame();
        }

        public BowlingGame Game { get; set; }

        [Test]
        public void CanCreateInstanceOfBowlingGame()
        {
            Assert.IsInstanceOf<BowlingGame>(Game);
        }

        [Test]
        public void HasPropertyPlayers()
        {
            Assert.NotNull(Game.Players);
        }

        [Test]
        public void CanHaveHaveMultiplePlayers()
        {
            var output = new StringWriter();
            Console.SetOut(output);
            var input = new StringReader("2");
            Console.SetIn(input);

            Game.SetupPlayers();

            Assert.That(output.ToString(), Is.EqualTo($"How many players?{Environment.NewLine}"));
            Assert.AreEqual(2, Game.Players.Count);
        }

        [Test]
        public void CanHaveMoreThanTwoPlayers()
        {
            var output = new StringWriter();
            Console.SetOut(output);
            var input = new StringReader("3");
            Console.SetIn(input);

            Game.SetupPlayers();

            Assert.That(output.ToString(), Is.EqualTo($"How many players?{Environment.NewLine}"));
            Assert.AreEqual(3, Game.Players.Count);
        }

        [Test]
        public void MustEnterNumberForPlayers()
        {
            var output = new StringWriter();
            Console.SetOut(output);
            var input = new StringReader($"Craig{Environment.NewLine}2");
            Console.SetIn(input);

            Game.SetupPlayers();

            Assert.AreEqual(output.ToString(), $"How many players?{Environment.NewLine}How many players?{Environment.NewLine}");
        }

        [Test]
        public void MustBePositiveNumberOfPlayers()
        {
            var output = new StringWriter();
            Console.SetOut(output);
            var input = new StringReader($"-1{Environment.NewLine}2");
            Console.SetIn(input);

            Game.SetupPlayers();

            Assert.AreEqual(output.ToString(), $"How many players?{Environment.NewLine}How many players?{Environment.NewLine}");
        }

        [Test]
        public void MustBGreaterThanZeroNumberOfPlayers()
        {
            var output = new StringWriter();
            Console.SetOut(output);
            var input = new StringReader($"0{Environment.NewLine}2");
            Console.SetIn(input);

            Game.SetupPlayers();

            Assert.AreEqual(output.ToString(), $"How many players?{Environment.NewLine}How many players?{Environment.NewLine}");
        }

        [Test]
        public void NoScoreEnteredShouldbeGreaterThan10()
        {
            var output = new StringWriter();
            Console.SetOut(output);
            var input = new StringReader("2");
            Console.SetIn(input);

            Game.SetupPlayers();

            Game.GetCurrentPlayer().AddScore(5);
            Game.GetCurrentPlayer().AddScore(6);

            Assert.AreEqual(5, Game.Players[0].Frames[0].Scores[0]);
            Assert.AreEqual(5, Game.Players[0].Frames[0].Scores[1]);
        }

        [Test]
        public void NoScoreEnteredShouldbeGreaterThan10OnFirstThrow()
        {
            var output = new StringWriter();
            Console.SetOut(output);
            var input = new StringReader("2");
            Console.SetIn(input);

            Game.SetupPlayers();

            Game.GetCurrentPlayer().AddScore(11);

            Assert.AreEqual(10, Game.Players[0].Frames[0].Scores[0]);
        }
    }
}