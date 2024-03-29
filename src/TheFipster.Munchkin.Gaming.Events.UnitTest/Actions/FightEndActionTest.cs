﻿using TheFipster.Munchkin.Gaming.Domain;
using TheFipster.Munchkin.Gaming.Domain.Exceptions;
using TheFipster.Munchkin.TestFactory;
using Xunit;

namespace TheFipster.Munchkin.Gaming.Events.UnitTest.Actions
{
    public class FightEndActionTest
    {
        private string badThings = "Schlimme Dinge";

        [Fact]
        public void EndingANotStartedFight_ThrowsInvalidActionException_Test()
        {
            // Arrange
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId, out var sequence);
            var hero = new Hero(PlayerFactory.CreateMale("GI Joe"));
            var endFight = FightEndMessage.Create(sequence.Next, badThings);

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(gameId, endFight));
        }
    }
}
