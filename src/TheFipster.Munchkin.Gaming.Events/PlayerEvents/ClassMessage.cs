﻿using System;
using System.Collections.Generic;
using TheFipster.Munchkin.Gaming.Domain.Events;

namespace TheFipster.Munchkin.Gaming.Events
{
    public class ClassMessage : GameSwitchMessage<string>
    {
        public static ClassMessage CreateAdd(int sequence, Guid playerId, IList<string> add)
        {
            return new ClassMessage
            {
                Sequence = sequence,
                PlayerId = playerId,
                Add = add
            };
        }

        public static ClassMessage CreateRemove(int sequence, Guid playerId, IList<string> remove)
        {
            return new ClassMessage
            {
                Sequence = sequence,
                PlayerId = playerId,
                Remove = remove
            };
        }

        public static ClassMessage Create(int sequence, Guid playerId, IList<string> add, IList<string> remove)
        {
            return new ClassMessage
            {
                Sequence = sequence,
                PlayerId = playerId,
                Add = add,
                Remove = remove
            };
        }

        public Guid PlayerId { get; set; }
    }
}
