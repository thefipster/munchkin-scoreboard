﻿using System;

namespace TheFipster.Munchkin.GameDomain.Exceptions
{
    public class ProtocolEmptyException : Exception
    {
        public ProtocolEmptyException(string message) : base(message) { }
    }
}