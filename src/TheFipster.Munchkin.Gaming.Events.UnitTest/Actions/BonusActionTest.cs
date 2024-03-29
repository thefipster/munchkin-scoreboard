﻿using System;
using System.Linq;
using TheFipster.Munchkin.Gaming.Domain.Exceptions;
using TheFipster.Munchkin.TestFactory;
using Xunit;

namespace TheFipster.Munchkin.Gaming.Events.UnitTest.Actions
{
    public class BonusActionTest
    {
        [Fact]
        public void IncreaseBonusWhenGameIsNotStarted_ThrowsException_Test()
        {
            // Arrange
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var increaseBonusMsg = BonusMessage.Create(1, Guid.NewGuid(), 1);

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(gameId, increaseBonusMsg));
        }

        [Fact]
        public void IncreaseBonusOnUnknownHero_ThrowsException_Test()
        {
            // Arrange
            var quest = QuestFactory.CreateStarted(out var gameStore, out var gameId, out var sequence);
            var increaseBonusMsg = BonusMessage.Create(sequence.Next, Guid.NewGuid(), 1);

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(gameId, increaseBonusMsg));
        }

        [Fact]
        public void DecreaseBonusWhenGameIsNotStarted_ThrowsException_Test()
        {
            // Arrange
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var decreaseBonusMsg = BonusMessage.Create(1, Guid.NewGuid(), -1);

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(gameId, decreaseBonusMsg));
        }

        [Fact]
        public void DecreaseBonusOnUnknownHero_ThrowsException_Test()
        {
            // Arrange
            var quest = QuestFactory.CreateStarted(out var gameStore, out var gameId, out var sequence);
            var decreaseBonusMsg = BonusMessage.Create(sequence.Next, Guid.NewGuid(), 1);

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(gameId, decreaseBonusMsg));
        }

        [Fact]
        public void IncreaseBonusOnHero_ResultsInIncreasedBonus_Test()
        {
            // Arrange
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId, out var sequence);
            var increaseBonusMsg = BonusMessage.Create(sequence.Next, playerId, 1);

            // Act
            var game = quest.AddMessage(gameId, increaseBonusMsg);

            // Assert
            Assert.Equal(1, game.Score.Heroes.First().Bonus);
        }

        [Fact]
        public void IncreaseBonusOnHero_ThenUndoIt_ResultsInOriginalBonus_Test()
        {
            // Arrange
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId, out var sequence);
            var increaseBonusMsg = BonusMessage.Create(sequence.Next, playerId, 1);

            // Act
            quest.AddMessage(gameId, increaseBonusMsg);
            var game = quest.Undo(gameId);

            // Assert
            Assert.Equal(0, game.Score.Heroes.First().Bonus);
        }

        [Fact]
        public void IncreaseBonus_ThenDecreaseBonusOnHero_ResultsInNoChange_Test()
        {
            // Arrange
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId, out var sequence);
            var increaseBonusMsg = BonusMessage.Create(sequence.Next, playerId, 1);
            var decreaseBonusMsg = BonusMessage.Create(sequence.Next, playerId, -1);

            // Act
            quest.AddMessage(gameId, increaseBonusMsg);
            var game = quest.AddMessage(gameId, decreaseBonusMsg);

            // Assert
            Assert.Equal(0, game.Score.Heroes.First().Bonus);
        }

        [Fact]
        public void IncreaseBonus_ThenDecreaseBonus_ThenUndoId_ResultsInIncreasedBonus_Test()
        {
            // Arrange
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId, out var sequence);
            var increaseBonusMsg = BonusMessage.Create(sequence.Next, playerId, 1);
            var decreaseBonusMsg = BonusMessage.Create(sequence.Next, playerId, -1);

            // Act
            quest.AddMessage(gameId, increaseBonusMsg);
            quest.AddMessage(gameId, decreaseBonusMsg);
            var game = quest.Undo(gameId);

            // Assert
            Assert.Equal(1, game.Score.Heroes.First().Bonus);
        }
    }
}
