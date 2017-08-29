using System.Drawing;

namespace DAnTE.ExtraControls
{
    /// <summary>
    /// Annotation color is used because it is xml-serializable and a regular Color is not.
    /// </summary>
    public class AnnotationColor
    {
        /// <summary>
        /// Creates a new AnnotationColor
        /// </summary>
        public AnnotationColor()
        {
        }

        /// <summary>
        /// Creates a new AnnotationColor
        /// </summary>
        /// <param name="red">The red hue of the color</param>
        /// <param name="green">The green hue of the color</param>
        /// <param name="blue">The blue hue of the color</param>
        public AnnotationColor(byte red, byte green, byte blue)
        {
            _red = red;
            _green = green;
            _blue = blue;
        }

        /// <summary>
        /// Gets or sets the red hue of the color
        /// </summary>
        public byte Red
        {
            get => _red;
            set => _red = value;
        }

        private byte _red = 0;

        /// <summary>
        /// Gets or sets the green hue of the color
        /// </summary>
        public byte Green
        {
            get => _green;
            set => _green = value;
        }

        private byte _green = 0;

        /// <summary>
        /// Gets or sets the blue hue of the color
        /// </summary>
        public byte Blue
        {
            get => _blue;
            set => _blue = value;
        }

        private byte _blue = 0;

        /// <summary>
        /// Implecitly convert this Annotation color into a color
        /// </summary>
        /// <param name="color">Annotation color to be converted</param>
        /// <returns>A color object</returns>
        public static implicit operator Color(AnnotationColor color)
        {
            return Color.FromArgb(color.Red, color.Green, color.Blue);
        }

        /// <summary>
        /// Implecitly convert this color into an Annotation  color
        /// </summary>
        /// <param name="color">Color to be converted</param>
        /// <returns>A new annotation color object</returns>
        public static implicit operator AnnotationColor(Color color)
        {
            return new AnnotationColor(color.R, color.G, color.B);
        }
    }
}