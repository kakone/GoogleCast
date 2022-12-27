namespace GoogleCast.Channels;

/// <summary>
/// Interface for a status channel
/// </summary>
public interface IStatusChannel : IChannel
{
    /// <summary>
    /// Gets or sets the status
    /// </summary>
    object? Status { get; set; }
}
