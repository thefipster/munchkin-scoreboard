﻿using System;

namespace TheFipster.Munchkin.GameDomain.Messages
{
    public class StartMessage : GameMessage
    {
        public DateTime Begin { get; set; }
    }
}