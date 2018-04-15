namespace GoogleCast.Models.Media
{
    /// <summary>
    /// Possible text track types
    /// </summary>
    public enum TextTrackType
    {
        /// <summary>
        /// Transcription or translation of the dialogue, suitable for when the sound is available but not understood
        /// </summary>
        Subtitles,

        /// <summary>
        /// Transcription or translation of the dialogue, sound effects, relevant musical cues, and other relevant audio information, 
        /// suitable for when the soundtrack is unavailable
        /// </summary>
        Captions,

        /// <summary>
        /// Textual descriptions of the video component of the media resource, intended for audio synthesis when 
        /// the visual component is unavailable
        /// </summary>
        Descriptions,

        /// <summary>
        /// Chapter titles, intended to be used for navigating the media resource
        /// </summary>
        Chapters,

        /// <summary>
        /// Tracks intended for use from script
        /// </summary>
        Metadata
    }
}
