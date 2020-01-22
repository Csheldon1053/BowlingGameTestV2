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
        public void TestTest()
        {
            // test commit
        }
    }
}