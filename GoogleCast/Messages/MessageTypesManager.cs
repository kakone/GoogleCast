using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GoogleCast.Messages
{
    /// <summary>
    /// Message types manager
    /// </summary>
    public class MessageTypesManager : IMessageTypesManager
    {
        /// <summary>
        /// Initializes a new instance of MessageTypesManager class
        /// </summary>
        public MessageTypesManager()
        {
            var messageName = nameof(Message);
            MessageTypes = GetMessagesTypes().ToDictionary(t => t.Name.Substring(0, t.Name.LastIndexOf(messageName)).ToUpper());
        }

        private IDictionary<string, Type> MessageTypes { get; }

        /// <summary>
        /// Gets all the message class types
        /// </summary>
        /// <returns>a collection containing the message class types</returns>
        protected virtual IEnumerable<Type> GetMessagesTypes()
        {
            var messageInterfaceType = typeof(IMessage);
            var messageType = typeof(Message);
            return (from t in GetType().GetTypeInfo().Assembly.GetTypes()
                    where t.GetTypeInfo().IsClass && !t.GetTypeInfo().IsAbstract && t != messageType && messageInterfaceType.IsAssignableFrom(t)
                    select t);
        }

        /// <summary>
        /// Gets the message class type
        /// </summary>
        /// <param name="type">message type</param>
        /// <returns>the message class type</returns>
        public virtual Type GetMessageType(string type)
        {
            return MessageTypes.TryGetValue(type.Replace("_", ""), out Type t) ? t : null;
        }
    }
}
