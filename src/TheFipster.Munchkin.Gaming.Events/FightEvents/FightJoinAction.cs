﻿using TheFipster.Munchkin.Gaming.Domain;
using TheFipster.Munchkin.Gaming.Domain.Events;
using TheFipster.Munchkin.Gaming.Domain.Exceptions;

namespace TheFipster.Munchkin.Gaming.Events
{
    public class FightJoinAction : GameAction
    {
        public FightJoinAction(GameMessage message, Game game)
            : base(message, game) { }

        public new FightJoinMessage Message => (FightJoinMessage)base.Message;

        public override void Validate()
        {
            base.Validate();

            if (Game.Score.Fight == null)
                throw new InvalidActionException("No luck man, there isn't a fight going on where you could help.");

            if (Game.Score.Fight.Heroes.Count == 2)
                throw new InvalidActionException("Easy... there is already someone that helps.");
        }

        public override Game Do()
        {
            var hero = Game.GetHero(Message.PlayerId);
            Game.Score.Fight.Heroes.Add(hero);
            return base.Do();
        }
    }
}
