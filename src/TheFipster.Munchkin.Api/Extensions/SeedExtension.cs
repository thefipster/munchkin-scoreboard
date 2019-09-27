﻿using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GamePersistance;

namespace TheFipster.Munchkin.Api.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class SeedExtensions
    {
        public static void SynchronizeSeedData(this IHostingEnvironment env, IConfiguration config, ICardStore cardStore)
        {
            sync(config, cardStore, CardCollection.Classes);
            sync(config, cardStore, CardCollection.Curses);
            sync(config, cardStore, CardCollection.Dungeons);
            sync(config, cardStore, CardCollection.Monsters);
            sync(config, cardStore, CardCollection.Races);
            sync(config, cardStore, CardCollection.Genders);
            sync(config, cardStore, CardCollection.FightResults);
            sync(config, cardStore, CardCollection.LevelIncreaseReasons);
            sync(config, cardStore, CardCollection.LevelDecreaseReasons);
            sync(config, cardStore, CardCollection.FightStarters);
        }

        private static void sync(IConfiguration config, ICardStore cardStore, string collectionName)
        {
            var cards = config.GetArray(collectionName);
            cardStore.Sync(collectionName, cards);
        }
    }
}