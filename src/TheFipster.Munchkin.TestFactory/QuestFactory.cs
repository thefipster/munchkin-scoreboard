﻿using System;
using TheFipster.Munchkin.Gaming.Engine;
using TheFipster.Munchkin.Gaming.Events;
using TheFipster.Munchkin.Gaming.Storage;
using TheFipster.Munchkin.Gaming.Storage.Volatile;

namespace TheFipster.Munchkin.TestFactory
{
    public class QuestFactory
    {
        public static Quest Create(IGameStore gameStore)
        {
            var eventInventory = new Inventory();
            return new Quest(gameStore, eventInventory);
        }

        public static Quest Create() => Create(new VolatileGameStore());

        public static Quest CreateStored(out IGameStore gameStore, out Guid gameId)
        {
            gameStore = new VolatileGameStore();
            var quest = Create(gameStore);
            gameId = quest.StartJourney();
            return quest;
        }

        public static Quest CreateStarted(out IGameStore gameStore, out Guid gameId, out Sequence sequence)
        {
            var quest = CreateStored(out gameStore, out gameId);
            sequence = new Sequence();
            startQuest(quest, sequence, gameId);
            return quest;
        }

        public static Quest CreateStartedWithMaleHero(out IGameStore gameStore, out Guid gameId, out Guid playerId, out Sequence sequence)
        {
            var quest = CreateStarted(out gameStore, out gameId, out sequence);
            addMaleHeroToQuest(quest, sequence, gameId, out playerId);
            return quest;
        }

        private static void startQuest(Quest quest, Sequence sequence, Guid gameId)
        {
            var startMsg = StartMessage.Create(sequence.Next);
            quest.AddMessage(gameId, startMsg);
        }

        private static void addMaleHeroToQuest(Quest quest, Sequence sequence, Guid gameId, out Guid playerId)
        {
            var player = PlayerFactory.CreateMale("John Doe");
            var heroAddMsg = PlayerMessage.CreateAdd(sequence.Next, new[] { player });
            quest.AddMessage(gameId, heroAddMsg);
            playerId = player.Id;
        }
    }
}
