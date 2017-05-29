namespace GoogleCast.Channels
{
    /// <summary>
    /// Interface for application channels
    /// </summary>
    public interface IApplicationChannel : IChannel
    {
        /// <summary>
        /// Gets the application identifier
        /// </summary>
        string ApplicationId { get; }
    }
}
