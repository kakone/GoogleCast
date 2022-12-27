using System.Net;

namespace GoogleCast;

/// <summary>
/// Interface for a receiver
/// </summary>
public interface IReceiver
{
    /// <summary>
    /// Gets the receiver identifier
    /// </summary>
    string Id { get; }

    /// <summary>
    /// Gets the friendly name
    /// </summary>
    string FriendlyName { get; }

    /// <summary>
    /// Gets the network endpoint
    /// </summary>
    IPEndPoint IPEndPoint { get; }
}