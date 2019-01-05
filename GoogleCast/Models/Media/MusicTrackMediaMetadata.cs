using System.Runtime.Serialization;

namespace GoogleCast.Models.Media
{
    /// <summary>
    /// Music metadata
    /// </summary>
    [DataContract]
    public class MusicTrackMediaMetadata : GenericMediaMetadata
    {
        /// <summary>
        /// Initializes a new instance of <see cref="MusicTrackMediaMetadata"/> class
        /// </summary>
        public MusicTrackMediaMetadata()
        {
            MetadataType = MetadataType.Music;
        }

        [DataMember(Name = "albumArtist", EmitDefaultValue = false)]
        public string AlbumArtist { get; set; }

        [DataMember(Name = "albumName", EmitDefaultValue = false)]
        public string AlbumName { get; set; }

        [DataMember(Name = "artist", EmitDefaultValue = false)]
        public string Artist { get; set; }

        [DataMember(Name = "composer", EmitDefaultValue = false)]
        public string Composer { get; set; }

        [DataMember(Name = "discNumber", EmitDefaultValue = false)]
        public int? DiscNumber { get; set; }

        [DataMember(Name = "releaseDate", EmitDefaultValue = false)]
        public string ReleaseDate { get; set; }

        [DataMember(Name = "songName", EmitDefaultValue = false)]
        public string SongName { get; set; }

        [DataMember(Name = "trackNumber", EmitDefaultValue = false)]
        public int? TrackNumber { get; set; }
    }
}
