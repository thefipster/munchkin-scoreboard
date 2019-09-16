﻿using System;
using System.Linq;
using LiteDB;
using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.Persistance;

namespace TheFipster.Munchkin.LiteStorage
{
    public class GameStore : IGameStore
    {
        private readonly LiteCollection<Game> _collection;

        public GameStore(IRepository<Game> repository)
        {
            var repo = repository;
            _collection = repo.GetCollection();
        }

        public void Upsert(Game game) =>
            _collection.Upsert(game);

        public Game Get(Guid gameId) => _collection
            .Find(x => x.Id == gameId)
            .First();
    }
}