using System;
using System.Drawing;
using System.Xml.Serialization;

namespace DAnTE.ExtraControls
{
    /// <summary>
    /// Annotation font is used because it is xml-serializable and a regular Font is not.
    /// </summary>
    [Serializable, XmlInclude(typeof(AnnotationFont))]
    public class AnnotationFont
    {
        /// <summary>
        /// Creates a new annotation font
        /// </summary>
        public AnnotationFont()
        {
        }

        /// <summary>
        /// Creates a new annotation font
        /// </summary>
        /// <param name="fontFamily">The family name of the font</param>
        /// <param name="size">The size of the font</param>
        /// <param name="style">The style to be used for the font</param>
        public AnnotationFont(string fontFamily, float size, FontStyle style)
        {
            _fontFamily = fontFamily;
            _size = size;
            _style = style;
        }

        /// <summary>
        /// Gets or sets the family name of the font
        /// </summary>
        public string FontFamily
        {
            get { return _fontFamily; }
            set { _fontFamily = value; }
        }

        private string _fontFamily = "Arial";

        /// <summary>
        /// Gets or sets the size of the font
        /// </summary>
        public float Size
        {
            get { return _size; }
            set { _size = value; }
        }

        private float _size = 9.0F;

        /// <summary>
        /// Gets or sets the style to be used for the font
        /// </summary>
        public FontStyle Style
        {
            get { return _style; }
            set { _style = value; }
        }

        private FontStyle _style = FontStyle.Regular;

        /// <summary>
        /// Implicitly converts an annotation font which is xml-serializable into a font which is not.
        /// </summary>
        /// <param name="font">Annotation font to be converted</param>
        /// <returns>The System.Drawing.Font equivelent</returns>
        public static implicit operator Font(AnnotationFont font)
        {
            return new Font(font.FontFamily, font.Size, font.Style);
        }

        /// <summary>
        /// Implicitly converts a Font into an annotation font
        /// </summary>
        /// <param name="font">Font to be converted</param>
        /// <returns>The AnnotationFont equivelent</returns>
        public static implicit operator AnnotationFont(Font font)
        {
            return new AnnotationFont(font.FontFamily.Name, font.Size, font.Style);
        }
    }
}