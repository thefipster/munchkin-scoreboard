﻿using TheFipster.Munchkin.GameDomain.Messages;
using Xunit;

namespace TheFipster.Munchkin.GameDomain.UnitTest.Messages
{
    public class EndMessageTest
    {
        [Fact]
        public void TypeNameTest()
        {
            // Arrange
            var endMessage = new EndMessage();
            var expectedType = endMessage.GetType().Name;

            // Act
            var actualType = endMessage.Type;

            // Assert
            Assert.Equal(expectedType, actualType);
        }
    }
}