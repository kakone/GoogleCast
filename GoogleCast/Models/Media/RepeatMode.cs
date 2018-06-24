namespace GoogleCast.Models.Media
{
    /// <summary>
    /// Possible states of queue repeat mode
    /// </summary>
    public enum RepeatMode
    {
        /// <summary>
        /// Items are played in order, and when the queue is completed (the last item has ended) the media session is terminated
        /// </summary>
        RepeatOff,

        /// <summary>
        /// The items in the queue will be played indefinitely. When the last item has ended, the first item will be played again
        /// </summary>
        RepeatAll,

        /// <summary>
        /// The current item will be repeated indefinitely
        /// </summary>
        RepeatSingle,

        /// <summary>
        /// The items in the queue will be played indefinitely.
        /// When the last item has ended, the list of items will be randomly shuffled by the receiver,
        /// and the queue will continue to play starting from the first item of the shuffled items.
        /// </summary>
        RepeatAllAndShuffle
    }
}
