﻿using System;
using System.Linq;
using TheFipster.Munchkin.GameEngine.Exceptions;
using TheFipster.Munchkin.GameEngine.Model;

namespace TheFipster.Munchkin.GameEngine.Messages
{
    public class LevelDecreaseMessage : LevelMessage
    {
        public LevelDecreaseMessage() { }

        public LevelDecreaseMessage(Guid playerId, int levelChange, string reason = null)
        {
            PlayerId = playerId;
            Delta = levelChange;
            Reason = reason;
        }

        public override void ApplyTo(GameState state)
        {
            if (!state.Players.Any(player => player.Id == PlayerId))
                throw new UnknownPlayerException(PlayerId);

            state.Players.First(p => p.Id == PlayerId).Level -= Delta;
        }

        public override void Undo(GameState state)
        {
            if (!state.Players.Any(player => player.Id == PlayerId))
                throw new UnknownPlayerException(PlayerId);

            state.Players.First(p => p.Id == PlayerId).Level += Delta;
        }
    }
}
