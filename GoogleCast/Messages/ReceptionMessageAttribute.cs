using System;

namespace GoogleCast.Messages
{
    /// <summary>
    /// Attribute for received messages
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ReceptionMessageAttribute : Attribute
    {
    }
}
