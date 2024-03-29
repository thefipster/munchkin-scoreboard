﻿using System;
using System.Collections.Generic;
using System.Linq;
using TheFipster.Munchkin.Gaming.Domain;

namespace TheFipster.Munchkin.Gaming.Storage.Volatile
{
    public class VolatilePlayerStore : IPlayerStore
    {
        private List<GameMaster> _players;

        public VolatilePlayerStore()
        {
            _players = new List<GameMaster>();
        }

        public void Add(GameMaster gameMaster) => _players.Add(gameMaster);

        public GameMaster Get(Guid gameMasterId) => _players.FirstOrDefault(player => player.Id == gameMasterId);

        public GameMaster GetByExternalId(string externalId) => _players.FirstOrDefault(player => player.ExternalId == externalId);

        public GameMaster Register(string name, string externalId, string email = null)
        {
            var gameMaster = new GameMaster(name, externalId, email);
            _players.Add(gameMaster);
            return gameMaster;
        }
    }
}
