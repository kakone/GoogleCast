using GoogleCast.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GoogleCast
{
    /// <summary>
    /// Dictionary of message types
    /// </summary>
    public class MessageTypes : Dictionary<string, Type>, IMessageTypes
    {
        /// <summary>
        /// Initializes a new instance of <see cref="MessageTypes"/> class
        /// </summary>
        public MessageTypes()
        {
            AddMessageTypes(Assembly.GetExecutingAssembly());
        }

        /// <summary>
        /// Adds all the message types of a given assembly
        /// </summary>
        /// <param name="assembly">assembly</param>
        public void AddMessageTypes(Assembly assembly)
        {
            var messageInterfaceType = typeof(IMessage);
            var receptionMessageAttributeType = typeof(ReceptionMessageAttribute);
            foreach (var type in assembly.GetTypes().Where(t =>
                t.IsClass && !t.IsAbstract && t.IsDefined(receptionMessageAttributeType) && messageInterfaceType.IsAssignableFrom(t)))
            {
                Add(Message.GetMessageType(type), type);
            }
        }
    }
}
