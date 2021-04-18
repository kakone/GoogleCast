using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Runtime.Serialization;

namespace GoogleCast.Models.Media
{
    /// <summary>
    /// Describes style information for a text track
    /// </summary>
    [DataContract]
    public class TextTrackStyle
    {
        /// <summary>
        /// Gets or sets the background 32 bit RGBA color
        /// </summary>
        /// <remarks>the alpha channel should be used for transparent backgrounds</remarks>
        [IgnoreDataMember]
        public Color? BackgroundColor { get; set; }

        [DataMember(Name = "backgroundColor", EmitDefaultValue = false)]
        [SuppressMessage("CodeQuality", "IDE0051")]
        private string? BackgroundColorString
        {
            get => BackgroundColor.ToHexString();
            set => BackgroundColor = ColorHelper.FromNullableHexString(value);
        }

        /// <summary>
        /// Gets or sets the RGBA color for the edge
        /// </summary>
        /// <remarks>this value will be ignored if EdgeType is None</remarks>
        [IgnoreDataMember]
        public Color? EdgeColor { get; set; }

        [DataMember(Name = "edgeColor", EmitDefaultValue = false)]
        [SuppressMessage("CodeQuality", "IDE0051")]
        private string? EdgeColorString
        {
            get => EdgeColor.ToHexString();
            set => EdgeColor = ColorHelper.FromNullableHexString(value);
        }

        /// <summary>
        /// Gets or sets the text track edge type
        /// </summary>
        [IgnoreDataMember]
        public TextTrackEdgeType EdgeType { get; set; } = default!;

        [DataMember(Name = "edgeType", EmitDefaultValue = false)]
        [SuppressMessage("CodeQuality", "IDE0051")]
        private string EdgeTypeString
        {
            get => EdgeType.GetName();
            set => EdgeType = EnumHelper.Parse<TextTrackEdgeType>(value);
        }

        /// <summary>
        /// Gets or sets the font family
        /// </summary>
        /// <remarks>if the font is not available in the receiver, the fontGenericFamily will be used</remarks>
        [DataMember(Name = "fontFamily", EmitDefaultValue = false)]
        public string FontFamily { get; set; } = default!;

        /// <summary>
        /// Gets or sets the text track generic family
        /// </summary>
        [IgnoreDataMember]
        public TextTrackFontGenericFamily? FontGenericFamily { get; set; }

        [DataMember(Name = "fontGenericFamily", EmitDefaultValue = false)]
        [SuppressMessage("CodeQuality", "IDE0051")]
        private string? FontGenericFamilyString
        {
            get => FontGenericFamily.GetName();
            set => FontGenericFamily = EnumHelper.ParseNullable<TextTrackFontGenericFamily>(value);
        }

        /// <summary>
        /// Gets or sets font scaling factor for the text track
        /// </summary>
        /// <remarks>the default is 1</remarks>
        [DataMember(Name = "fontScale", EmitDefaultValue = false)]
        public float? FontScale { get; set; }

        /// <summary>
        /// Gets or sets the text track font style
        /// </summary>
        [IgnoreDataMember]
        public TextTrackFontStyle? FontStyle { get; set; }

        [DataMember(Name = "fontStyle", EmitDefaultValue = false)]
        [SuppressMessage("CodeQuality", "IDE0051")]
        private string? FontStyleString
        {
            get => FontStyle.GetName();
            set => FontStyle = EnumHelper.ParseNullable<TextTrackFontStyle>(value);
        }

        /// <summary>
        /// Gets or sets the foreground 32 bit RGBA color
        /// </summary>
        [IgnoreDataMember]
        public Color? ForegroundColor { get; set; }

        [DataMember(Name = "foregroundColor", EmitDefaultValue = false)]
        [SuppressMessage("CodeQuality", "IDE0051")]
        private string? ForegroundColorString
        {
            get => ForegroundColor.ToHexString();
            set => ForegroundColor = ColorHelper.FromNullableHexString(value);
        }

        /// <summary>
        /// Gets or sets the 32 bit RGBA color for the window
        /// </summary>
        /// <remarks>this value will be ignored if WindowType is None</remarks>
        [IgnoreDataMember]
        public Color? WindowColor { get; set; }

        [DataMember(Name = "windowColor", EmitDefaultValue = false)]
        [SuppressMessage("CodeQuality", "IDE0051")]
        private string? WindowColorColorString
        {
            get => WindowColor.ToHexString();
            set => WindowColor = ColorHelper.FromNullableHexString(value);
        }

        /// <summary>
        /// Gets or sets the rounded corner radius absolute value in pixels (px)
        /// </summary>
        /// <remarks>this value will be ignored if windowType is not RoundedCorners</remarks>
        [DataMember(Name = "windowRoundedCornerRadius", EmitDefaultValue = false)]
        public ushort? WindowRoundedCornerRadius { get; set; }

        /// <summary>
        /// Gets or sets the window type
        /// </summary>
        /// <remarks>the window concept is defined in CEA-608 and CEA-708. In WebVTT is called a region</remarks>
        [DataMember(Name = "windowType", EmitDefaultValue = false)]
        public TextTrackWindowType? WindowType { get; set; }
    }
}
