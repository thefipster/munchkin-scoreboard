﻿using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameDomain.Exceptions;
using TheFipster.Munchkin.GameDomain.Messages;

namespace TheFipster.Munchkin.GameEngine.Actions
{
    public class DungeonAction : MessageAction, IGameAction
    {
        public DungeonAction(DungeonMessage message, Game game)
            : base(message, game) { }

        public new DungeonMessage Message => (DungeonMessage)base.Message;

        public Game Do()
        {
            switch (Message.Modifier)
            {
                case Modifier.Add:
                    return addDungeon();
                case Modifier.Remove:
                    return removeDungeon();
                default:
                    throw new InvalidModifierException();
            }
        }

        public Game Undo()
        {
            switch(Message.Modifier)
            {
                case Modifier.Add:
                    return removeDungeon();
                case Modifier.Remove:
                    return addDungeon();
                default:
                    throw new InvalidModifierException();
            }
        }

        public void Validate()
        {
            if (Message.Modifier == Modifier.Add && dungeonExists())
                throw new InvalidActionException("You are already in this dungeon.");

            if (Message.Modifier == Modifier.Remove && !dungeonExists())
                throw new InvalidActionException("Can't leave a dungeon that you have not entered.");
        }

        private bool dungeonExists() =>
            Game.Score.Dungeons.Contains(Message.Dungeon);

        private Game addDungeon()
        {
            Game.Score.Dungeons.Add(Message.Dungeon);
            return Game;
        }

        private Game removeDungeon()
        {
            Game.Score.Dungeons.Remove(Message.Dungeon);
            return Game;
        }
    }
}