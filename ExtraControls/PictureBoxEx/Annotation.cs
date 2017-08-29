using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace DAnTE.ExtraControls
{
    /// <summary>
    /// Annotations are used for adding text to the image
    /// </summary>
    [Serializable, XmlInclude(typeof(Annotation))]
    public class Annotation
    {
        /// <summary>
        /// Creates a new pictureboxex annotation
        /// </summary>
        public Annotation()
        {
            Changed += OnAnnotationChanged;

            _guid = Guid.NewGuid();
        }

        /// <summary>
        /// Creates a new pictureboxex annotation
        /// </summary>
        /// <param name="text">Text to be displayed</param>
        /// <param name="font">Font that will be used to write the text</param>
        /// <param name="color">Color of the text</param>
        public Annotation(string text, Font font, Color color) : this(text, font, color, new Point(0, 0))
        {
        }

        /// <summary>
        /// Creates a new pictureboxex annotation
        /// </summary>
        /// <param name="text">Text to be displayed</param>
        /// <param name="font">Font that will be used to write the text</param>
        /// <param name="color">Color of the text</param>
        /// <param name="location">The location where the string should be placed</param>
        public Annotation(string text, Font font, Color color, Point location)
        {
            Changed += OnAnnotationChanged;

            this.Text = text;
            this.Font = font;
            this.Color = color;
            this.Location = location;
            this.Guid = Guid.NewGuid();
        }

        /// <summary>
        /// Gets or sets the font for the annotation
        /// </summary>
        public AnnotationFont Font
        {
            get { return _font; }
            set
            {
                if (_font != value)
                {
                    _font = value;

                    Changed(this, new EventArgs());
                }
            }
        }

        private AnnotationFont _font = null;

        /// <summary>
        /// Gets or sets the text for the annotation
        /// </summary>
        public string Text
        {
            get { return _text; }
            set
            {
                if (_text != value)
                {
                    _text = value;

                    Changed(this, new EventArgs());
                }
            }
        }

        private string _text = null;

        /// <summary>
        /// Gets or sets the color for the annotation
        /// </summary>
        public AnnotationColor Color
        {
            get { return _color; }
            set
            {
                if (_color != value)
                {
                    _color = value;

                    Changed(this, new EventArgs());
                }
            }
        }

        private AnnotationColor _color = null;

        /// <summary>
        /// Gets or sets the location for the annotation
        /// </summary>
        public Point Location
        {
            get { return new Point(x, y); }
            set
            {
                if (x != value.X || y != value.Y)
                {
                    x = value.X;
                    y = value.Y;

                    Changed(this, new EventArgs());
                }
            }
        }

        /// <summary>
        /// Gets or sets the x coordinate for this annotation
        /// </summary>
        [XmlIgnore]
        public int X
        {
            get { return x; }
            set
            {
                if (!x.Equals(value))
                {
                    x = value;

                    Changed(this, new EventArgs());
                }
            }
        }

        private int x = 0;

        /// <summary>
        /// Gets or sets the y coordinate for this annotation
        /// </summary>
        [XmlIgnore]
        public int Y
        {
            get { return y; }
            set
            {
                if (!y.Equals(value))
                {
                    y = value;

                    Changed(this, new EventArgs());
                }
            }
        }

        private int y = 0;

        /// <summary>
        /// Gets the bounds for this particular annotation
        /// </summary>
        [XmlIgnore]
        public Rectangle Bounds
        {
            get
            {
                if (_bounds == Rectangle.Empty)
                {
                    using (Control c = new Control())
                    {
                        Graphics g = c.CreateGraphics();

                        SizeF sizeF = g.MeasureString(_text, _font);

                        g.Dispose();

                        Size size = new Size((int)sizeF.Width, (int)sizeF.Height);

                        _bounds = new Rectangle(Location, size);
                    }
                }

                return _bounds;
            }
        }

        private Rectangle _bounds = Rectangle.Empty;

        /// <summary>
        /// Gets or sets this annotations ID
        /// </summary>
        public Guid Guid
        {
            get
            {
                if (_guid == Guid.Empty)
                    _guid = Guid.NewGuid();

                return _guid;
            }
            set => _guid = value;
        }

        private Guid _guid = Guid.Empty;

        /// <summary>
        /// Gets or sets the orientation of the image
        /// </summary>
        public RotateFlipType Orientation
        {
            get => _orientation;
            set => _orientation = value;
        }

        private RotateFlipType _orientation;

        /// <summary>
        /// Fires when any of this annotations properties are changed
        /// </summary>
        public event EventHandler Changed;

        /// <summary>
        /// The string representation of this annotation object
        /// </summary>
        /// <returns>The text value for this annotation</returns>
        public override string ToString()
        {
            return Text;
        }


        private void OnAnnotationChanged(object sender, EventArgs e)
        {
            _bounds = Rectangle.Empty;
        }
    }
}