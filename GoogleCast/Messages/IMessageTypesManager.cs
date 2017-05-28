using System;

namespace GoogleCast.Messages
{
    /// <summary>
    /// Interface for message types manager
    /// </summary>
    public interface IMessageTypesManager
    {
        /// <summary>
        /// Gets the message class type
        /// </summary>
        /// <param name="type">message type</param>
        /// <returns>the message class type</returns>
        Type GetMessageType(string type);
    }
}
