using System;
using System.Collections;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Reflection;
using System.Xml.Serialization;

namespace DAnTE.ExtraControls
{
    /// <summary>
    /// The extended picturebox has many more options and features then the original picturebox
    /// </summary>
    public class PictureBoxEx : ScrollableControl
    {
        public delegate void menuclicked();
        //public event menuclicked meventmenuclicked;

        /// <summary>
        /// Constructor: create a new extended picturebox
        /// </summary>
        public PictureBoxEx()
        {
            InitComponent();

            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            base.AutoScroll = true;
            Size = new Size(200, 200);
            Paint += PictureBoxEx_Paint;

            _diagnosticMode = false;
            _doubleClickRestore = true;
            _borderStyle = BorderStyle.None;
            _drawMode = InterpolationMode.Default;

            // Init zoom fields
            _currentZoom = 1.0F;
            _defaultZoom = 1.0F;
            _maximumZoom = 3.0F;
            _minimumZoom = 0.10F;

            // Init drag window fields
            dashPattern = new Single[] { 5, 2, 5, 2 };

            dragWindow = Rectangle.Empty;
            dragStart = Point.Empty;
            dragEnd = Point.Empty;
            dragPen = new Pen(Color.Black, 1)
            {
                DashStyle = DashStyle.Custom,
                DashPattern = dashPattern
            };

            _dragOptions = DragOptions.Prompt;
            _dragWindowMinimum = SystemInformation.DragSize;

            lmbDown = false;

            // Annotation fields
            _annotations = new AnnotationCollection(this);
            _annotations.AnnotationsChanged += Annotations_AnnotationsChanged;
            selectedAnnote = null;

            generateTime = 0L;
            drawTime = 0L;
            setTime = 0L;
        }


        private void InitComponent()
        {
            cmnuPrompt = new ContextMenuStrip();
            CopyToolStripMenuItem = new ToolStripMenuItem();
            ZoomToolStripMenuItem = new ToolStripMenuItem();
            StretchToolStripMenuItem = new ToolStripMenuItem();
            SuspendLayout();
            //
            // cmnuPrompt
            //
            cmnuPrompt.Items.AddRange(new ToolStripItem[] {
            CopyToolStripMenuItem,
            ZoomToolStripMenuItem, StretchToolStripMenuItem});
            cmnuPrompt.Name = "mctxtMenu";

            CopyToolStripMenuItem.Name = "CopyToolStripMenuItem";
            //this.CopyToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            CopyToolStripMenuItem.Text = "&Copy";
            CopyToolStripMenuItem.Click += PromptMenuItem_Clicked;

            ZoomToolStripMenuItem.Name = "ZoomToolStripMenuItem";
            //this.ZoomToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            ZoomToolStripMenuItem.Text = "&Zoom";
            ZoomToolStripMenuItem.Click += PromptMenuItem_Clicked;

            StretchToolStripMenuItem.Name = "stretchToolStripMenuItem";
            //this.StretchToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            StretchToolStripMenuItem.Text = "&Fit Screen";
            StretchToolStripMenuItem.Click += StretchMenuItem_Clicked;

            //this.cmnuPrompt.Items.Add("&Copy", new EventHandler(PromptMenuItem_Clicked));
            //this.cmnuPrompt.Items.Add("&Zoom", new EventHandler(PromptMenuItem_Clicked));
            //this.cmnuPrompt.Items.Add("&Stretch", new EventHandler(StretchMenuItem_Clicked));
            // 
            // PictureBox
            // 
            ResumeLayout(false);
        }


        #region Fields and Properties

        private ContextMenuStrip cmnuPrompt;
        private ToolStripMenuItem CopyToolStripMenuItem, ZoomToolStripMenuItem,
            StretchToolStripMenuItem;

        private Image backup;  // Holds the original image that is used whenever the image is resized
        private Image resized; // Holds the entire image resized to the current zoom

        // Fields used by the drag window
        private Rectangle dragWindow;     // Holds the size and location of the drag window
        private Rectangle dragWindowSave; // Holds the drag window until the prompt has been completed
        private readonly Pen dragPen;     // The drag pen is used to draw the dragwindow
        private Point dragStart;          // The coords where the drag started
        private Point dragEnd;            // The coords where the drag ended
        private bool lmbDown;             // True if the left mouse button is down

        /// <summary>
        /// Gets picturebox's collection of annotations
        /// </summary>
        public AnnotationCollection Annotations
        {
            get { return _annotations; }
        }

        private readonly AnnotationCollection _annotations;
        private Annotation selectedAnnote; // Holds the currently selected annotation

        // Fields used for diagnostics
        readonly long generateTime;            // Time in milliseconds taken to generate resized image
        long drawTime;                // Time in milliseconds taken to draw the controls elements
        long setTime;                 // Time in milliseconds taken to set the image (Inclides generate time)

        /// <summary>
        /// Gets or sets true if the user should be allowed to drag to copy or zoom
        /// </summary>
        [Category("Behavior"),
        DefaultValue(true),
        Description("Indicates whether or not the user should be allowed to drag and copy or zoom"),
        Browsable(true)]
        public bool AllowDrag
        {
            get
            {
                return _allowDrag;
            }
            set
            {
                if (_allowDrag != value)
                {
                    _allowDrag = value;

                    AllowDragChanged?.Invoke(this, new EventArgs());
                }
            }
        }
        private bool _allowDrag = true;
        /// <summary>
        /// Fires when the allow drag property is changed
        /// </summary>
        public event EventHandler AllowDragChanged;

        /// <summary>
        /// Gets or sets what type of border the PictureBox should have
        /// </summary>
        [Category("Appearance"),
        DefaultValue(BorderStyle.None),
        Description("Controls what type of border the PictureBox should have."),
        Browsable(true)]
        public BorderStyle BorderStyle
        {
            get
            {
                return _borderStyle;
            }
            set
            {
                if (_borderStyle != value)
                {
                    _borderStyle = value;

                    Invalidate();

                    BorderStyleChanged?.Invoke(this, new EventArgs());
                }
            }
        }
        private BorderStyle _borderStyle;
        /// <summary>
        /// Fires when the BorderStyle property has been changed
        /// </summary>
        [Description("Fires when the BorderStyle property has been changed.")]
        public event EventHandler BorderStyleChanged;

        /// <summary>
        /// Gets or sets the picturebox's current zoom
        /// </summary>
        [Category("Behavior"),
        DefaultValue(1F),
        Description("Controls the PictureBox's current zoom."),
        Browsable(true)]
        public float CurrentZoom
        {
            get
            {
                return _currentZoom;
            }
            set
            {
                if (Math.Abs(_currentZoom - value) < float.Epsilon)
                    return;

                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "The current zoom cannot be set equal to or less than 0");

                if ((value > _maximumZoom && Math.Abs(_currentZoom - _maximumZoom) < float.Epsilon) || 
                    (value < _minimumZoom && Math.Abs(_currentZoom - _minimumZoom) < float.Epsilon))
                    return;                      // No change

                if (value > _maximumZoom)
                    _currentZoom = _maximumZoom; // Value exceeds max (set as max)
                else if (value < _minimumZoom)
                    _currentZoom = _minimumZoom; // Value exceeds min (set as min)
                else
                    _currentZoom = value;        // Set as specified

                if (backup != null)
                {
                    GenerateResizedImage(); // Regenerate the resized image to the new current zoom

                    AutoScrollPosition = new Point(0, 0);
                    AutoScrollMinSize = resized.Size;
                }

                CurrentZoomChanged?.Invoke(this, new EventArgs());
            }
        }
        private float _currentZoom;
        /// <summary>
        /// Fires when the CurrentZoom property has been changed
        /// </summary>
        [Description("Fires when the CurrentZoom property has been changed.")]
        public event EventHandler CurrentZoomChanged;

        /// <summary>
        /// Gets or sets the picturebox's default zoom
        /// </summary>
        [Category("Behavior"),
        DefaultValue(1F),
        Description("Controls the PictureBox's default zoom."),
        Browsable(true)]
        public float DefaultZoom
        {
            get
            {
                return _defaultZoom;
            }
            set
            {
                if (Math.Abs(_defaultZoom - value) < float.Epsilon)
                    return;

                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "The default zoom cannot be set equal to or less the 0");

                if ((value > _maximumZoom && Math.Abs(_defaultZoom - _maximumZoom) < float.Epsilon) || 
                    (value < _minimumZoom && Math.Abs(_defaultZoom - _minimumZoom) < float.Epsilon))
                    return;                      // No change

                if (value > _maximumZoom)
                    _defaultZoom = _maximumZoom; // Value exceeds max (set as max)
                else if (value < _minimumZoom)
                    _defaultZoom = _minimumZoom; // Value exceeds min (set as min)
                else
                    _defaultZoom = value;        // Set as specified

                DefaultZoomChanged?.Invoke(this, new EventArgs());
            }
        }
        private float _defaultZoom;
        /// <summary>
        /// Fires when the DefaultZoom property has been changed
        /// </summary>
        [Description("Fires when the DefaultZoom property has been changed.")]
        public event EventHandler DefaultZoomChanged;

        /// <summary>
        /// Gets or sets true if the picturebox should be placed in diagnostic mode
        /// </summary>
        [Category("Behavior"),
        DefaultValue(false),
        Description("Set to true to display diagnostics information."),
        Browsable(true)]
        public bool DiagnosticMode
        {
            get
            {
                return _diagnosticMode;
            }
            set
            {
                if (_diagnosticMode != value)
                {
                    _diagnosticMode = value;

                    Invalidate();
                }
            }
        }
        private bool _diagnosticMode;

        /// <summary>
        /// Gets or sets the picturebox's ability to restore the image to the default zoom on double-click
        /// </summary>
        [Category("Behavior"),
        DefaultValue(true),
        Description("Controls the picturebox's ability to restore the image to the default zoom on double-click."),
        Browsable(true)]
        public bool DoubleClickRestore
        {
            get
            {
                return _doubleClickRestore;
            }
            set
            {
                if (_doubleClickRestore != value)
                {
                    _doubleClickRestore = value;

                    DoubleClickRestoreChanged?.Invoke(this, new EventArgs());
                }
            }
        }
        private bool _doubleClickRestore;
        /// <summary>
        /// Fires when the DoubleClickRestore property has been changed
        /// </summary>
        [Description("Fires when the DoubleClickRestore property has been changed.")]
        public event EventHandler DoubleClickRestoreChanged;

        /// <summary>
        /// Gets or sets the picturebox's drag options
        /// </summary>
        [Category("Behavior"),
        DefaultValue(DragOptions.Prompt),
        Description("Controls the PictureBox's drag options. (Copy, Zoom, or Prompt)"),
        Browsable(true)]
        public DragOptions DragOptions
        {
            get
            {
                return _dragOptions;
            }
            set
            {
                if (_dragOptions != value)
                {
                    _dragOptions = value;

                    DragOptionsChanged?.Invoke(this, new EventArgs());
                }
            }
        }
        private DragOptions _dragOptions;
        /// <summary>
        /// Fires when the DragOptions property has been changed
        /// </summary>
        [Description("Fires when the DragOptions property has been changed.")]
        public event EventHandler DragOptionsChanged;

        /// <summary>
        /// Gets or sets the picturebox's drag window color
        /// </summary>
        [Category("Appearance"),
        DefaultValue(typeof(Color), "Black"),
        Description("Controls the picturebox's drag window color."),
        Browsable(true)]
        public Color DragWindowColor
        {
            get
            {
                return dragPen.Color;
            }
            set
            {
                if (!dragPen.Color.Equals(value))
                {
                    dragPen.Color = value;

                    Invalidate();

                    DragWindowColorChanged?.Invoke(this, new EventArgs());
                }
            }
        }
        /// <summary>
        /// Fires when the DragWindowColor property has been changed
        /// </summary>
        [Description("Fires when the DragWindowColor property has been changed.")]
        public event EventHandler DragWindowColorChanged;

        /// <summary>
        /// Gets or sets the picturebox's minimum invokable drag window size
        /// </summary>
        [Category("Behavior"),
        DefaultValue(typeof(Size), "4, 4"),
        Description("Controls the picturebox's minimum invokable drag window size."),
        Browsable(true)]
        public Size DragWindowMinimum
        {
            get
            {
                return _dragWindowMinimum;
            }
            set
            {
                if (_dragWindowMinimum != value)
                {
                    _dragWindowMinimum = value;

                    DragWindowMinimumChanged?.Invoke(this, new EventArgs());
                }
            }
        }
        private Size _dragWindowMinimum;
        /// <summary>
        /// Fires when the DragWindowMinimum property has been changed
        /// </summary>
        [Description("Fires when the DragWindowMinimum property has been changed.")]
        public event EventHandler DragWindowMinimumChanged;

        /// <summary>
        /// Gets or sets the dash pattern that is used by the control when drawing the drag window
        /// </summary>
        [Category("Appearance"),
        DefaultValue("5,2,5,2"),
        Description("The dash pattern that is used by the control when drawing the drag window."),
        Browsable(true)]
        public string DragWindowPattern
        {
            get
            {
                string pattern = null;

                foreach (var number in dashPattern)
                {
                    if (pattern == null)
                        pattern += number.ToString(CultureInfo.InvariantCulture);
                    else
                        pattern += "," + number.ToString(CultureInfo.InvariantCulture);
                }

                return pattern;
            }
            set
            {
                try
                {
                    var pattern = value.Split(',');

                    dashPattern = new Single[pattern.Length];

                    for (var i = 0; i != pattern.Length; i++)
                        dashPattern[i] = Single.Parse(pattern[i].Trim());

                    dragPen.DashPattern = dashPattern;

                    DragWindowDashPatternChanged?.Invoke(this, new EventArgs());
                }
                catch
                {
                    dashPattern = new Single[] { 5, 2, 5, 2 };
                }
            }
        }
        private Single[] dashPattern;
        /// <summary>
        /// Fires when the DragWindowPattern property has been changed
        /// </summary>
        [Description("Fires when the DragWindowPattern property has been changed.")]
        public event EventHandler DragWindowDashPatternChanged;

        /// <summary>
        /// Gets or sets imaging filter that will be applied to the image if it is resized
        /// </summary>
        [Category("Appearance"),
        DefaultValue(InterpolationMode.Default),
        Description("Controls the imaging filter that will be applied to the image if it is resized."),
        Browsable(true)]
        public InterpolationMode DrawMode
        {
            get
            {
                return _drawMode;
            }
            set
            {
                if (_drawMode != value)
                {
                    _drawMode = value;

                    if (backup != null)
                    {
                        GenerateResizedImage();
                    }

                    DrawModeChanged?.Invoke(this, new EventArgs());
                }
            }
        }
        private InterpolationMode _drawMode;
        /// <summary>
        /// Fires when the DrawMode property has been changed
        /// </summary>
        [Description("Fires when the DrawMode property has been changed.")]
        public event EventHandler DrawModeChanged;

        /// <summary>
        /// Gets or sets the Image for the PictureBox (Returns a resized and annotated image if applicable)
        /// </summary>
        [Category("Appearance"),
        DefaultValue(null),
        Description("The Image for the PictureBox."),
        Browsable(true)]
        public Image Image
        {
            get { return resized; }
            set
            {
                var started = DateTime.Now;

                if (backup != value)
                {
                    if (value == null)
                    {
                        // Clear the current images
                        backup.Dispose();
                        backup = null;
                        resized.Dispose();
                        resized = null;
                    }
                    else
                    {
                        backup = value;

                        if (Math.Abs(_currentZoom - _defaultZoom) > float.Epsilon)
                            // Causes image to be resized and repainted
                            CurrentZoom = _defaultZoom;
                        else
                            // Resize and repaint the image manually
                            GenerateResizedImage();

                        AutoScrollMinSize = resized.Size;
                        AutoScrollPosition = new Point(0, 0);
                    }

                    _annotations.Clear();

                    ImageChanged?.Invoke(this, new EventArgs());
                }

                setTime = (long)DateTime.Now.Subtract(started).TotalMilliseconds;
            }
        }

        public Image OrgImage
        {
            get
            {
                if (backup != null)
                    return backup;
                else
                    return resized;
            }
        }

        /// <summary>
        /// Fires when the Image has been changed
        /// </summary>
        [Description("Fires when the Image has been changed.")]
        public event EventHandler ImageChanged;
        /// <summary>
        /// Fires when a portion of the image has been copied
        /// </summary>
        [Description("Fires when a portion of the image has been copied.")]
        public event ImagePortionCopiedEventHandler ImagePortionCopied;

        /// <summary>
        /// Gets or sets minimum allowed zoom
        /// </summary>
        [Category("Appearance"),
        DefaultValue(3.0F),
        Description("Controls the maximum allowed zoom."),
        Browsable(true)]
        public float MaximumZoom
        {
            get
            {
                return _maximumZoom;
            }
            set
            {
                if (Math.Abs(_maximumZoom - value) < float.Epsilon)
                    return;

                _maximumZoom = value;

                MaximumZoomChanged?.Invoke(this, new EventArgs());

                if (_currentZoom > _maximumZoom)
                    CurrentZoom = _maximumZoom;
            }
        }
        private float _maximumZoom;
        /// <summary>
        /// Fires when the MaximumZoom property has been changed
        /// </summary>
        [Description("Fires when the MaximumZoom property has been changed.")]
        public event EventHandler MaximumZoomChanged;

        /// <summary>
        /// Gets or sets maximum allowed zoom
        /// </summary>
        [Category("Appearance"),
        DefaultValue(0.10F),
        Description("Controls the minimum allowed zoom."),
        Browsable(true)]
        public float MinimumZoom
        {
            get
            {
                return _minimumZoom;
            }
            set
            {
                if (Math.Abs(_minimumZoom - value) < float.Epsilon)
                    return;

                _minimumZoom = value;

                MinimumZoomChanged?.Invoke(this, new EventArgs());

                if (_currentZoom < _minimumZoom)
                    CurrentZoom = _minimumZoom;
            }
        }
        private float _minimumZoom;
        /// <summary>
        /// Fires when the MinimumZoom property has been changed
        /// </summary>
        [Description("Fires when the MaximumZoom property has been changed.")]
        public event EventHandler MinimumZoomChanged;

        /// <summary>
        /// Fires when the ScrollPosition property has been changed
        /// </summary>
        [Description("Fires when the ScrollPosition property has been changed."),
        Browsable(false)]
        public event EventHandler ScrollPositionChanged;

        /// <summary>
        /// Hides this property
        /// </summary>
        [Browsable(false)]
        internal new bool AutoScroll
        {
            get { return true; }
            set { }
        }


        #region Constant Windwos Message Codes

        // Key strokes
        private const int MK_CONTROL = 0x0008;
        private const int MK_SHIFT = 0x0004;
        private const int VK_PRIOR = 0x0021; // PageDown
        private const int VK_NEXT = 0x0022; // PageUp
        private const int VK_END = 0x0023;
        private const int VK_HOME = 0x0024;
        private const int VK_LEFT = 0x0025;
        private const int VK_UP = 0x0026;
        private const int VK_RIGHT = 0x0027;
        private const int VK_DOWN = 0x0028;
        // Wheel movement
        private const int WM_KEYDOWN = 0x0101;
        private const int WM_MOUSEWHEEL = 0x020A;
        // User Scrolling
        private const int WM_HSCROLL = 0x0114;
        private const int WM_VSCROLL = 0x0115;

        #endregion

        #endregion

        private void GenerateResizedImage()
        {
            // Used when the picturebox is in diag mode

            var resizedWidth = Convert.ToInt32(backup.Width * _currentZoom);
            var resizedHeight = Convert.ToInt32(backup.Height * _currentZoom);

            resized = new Bitmap(resizedWidth, resizedHeight);

            // Drag the backup image onto the resized image
            using (var g = Graphics.FromImage(resized))
            {
                g.InterpolationMode = (_currentZoom < 1F) ? _drawMode : InterpolationMode.Default;

                var srceRect = new Rectangle(0, 0, backup.Width, backup.Height);
                var destRect = new Rectangle(0, 0, resized.Width, resized.Height);

                g.DrawImage(backup, destRect, srceRect, GraphicsUnit.Pixel);

                // Add any annotations to the resized image
                DrawAnnotations(g);
            }

            Invalidate();
        }


        #region Draw Methods

        private void PictureBoxEx_Paint(object sender, PaintEventArgs e)
        {
            // Handles the painting of the control

            var started = DateTime.Now;

            e.Graphics.FillRectangle(new SolidBrush(BackColor), ClientRectangle);

            if (backup != null && resized != null)
            {
                e.Graphics.DrawImageUnscaled(resized,
                    AutoScrollPosition.X, AutoScrollPosition.Y, resized.Width, resized.Height);

                DrawSelectedAnnotation(e.Graphics);

                DrawDragWindow(e.Graphics);

                DrawBorder(e.Graphics);

                drawTime = (long)DateTime.Now.Subtract(started).TotalMilliseconds;

                if (_diagnosticMode)
                    DrawDiagnostic(e.Graphics);
            }
        }
        private void DrawSelectedAnnotation(Graphics g)
        {
            if (selectedAnnote != null)
            {
                var fontSize = selectedAnnote.Font.Size * _currentZoom;

                var font = new Font(selectedAnnote.Font.FontFamily,
                    fontSize, selectedAnnote.Font.Style, GraphicsUnit.Pixel);

                Brush brush = new SolidBrush(selectedAnnote.Color);

                var sizeF = g.MeasureString(selectedAnnote.Text, font);

                g.DrawString(selectedAnnote.Text, font, brush, selectedAnnote.Location);

                g.DrawRectangle(dragPen, selectedAnnote.Location.X, selectedAnnote.Location.Y,
                    (int)sizeF.Width, (int)sizeF.Height);
            }
        }
        private void DrawDragWindow(Graphics g)
        {
            if (dragWindow != Rectangle.Empty)
                g.DrawRectangle(dragPen, dragWindow);
        }
        private void DrawBorder(Graphics g)
        {
            var hScrollHeight = (VScroll) ? 17 : 0;
            var vScrollWidth = (HScroll) ? 17 : 0;

            switch (_borderStyle)
            {
                case BorderStyle.Fixed3D:
                    var outsideUpperLeft = new Pen(Color.FromArgb(172, 168, 152), 1);
                    g.DrawLine(outsideUpperLeft, new Point(0, Height - 1), new Point(0, 0));
                    g.DrawLine(outsideUpperLeft, new Point(0, 0), new Point(Width - 1));

                    var insideUpperLeft = new Pen(Color.FromArgb(113, 111, 110), 1);
                    g.DrawLine(insideUpperLeft, new Point(1, Height - 2), new Point(1, 1));
                    g.DrawLine(insideUpperLeft, new Point(1, 1), new Point(Width - 2, 1));

                    var outsideLowerRight = new Pen(Color.FromArgb(255, 255, 255), 1);
                    g.DrawLine(outsideLowerRight, new Point(0, Height - 1 - hScrollHeight),
                        new Point(Width - 1, Height - 1 - hScrollHeight));
                    g.DrawLine(outsideLowerRight, new Point(Width - 1 - vScrollWidth,
                        Height - 1), new Point(Width - 1 - vScrollWidth, 0));

                    var insideLowerRight = new Pen(Color.FromArgb(241, 239, 226), 1);
                    g.DrawLine(insideLowerRight, new Point(1, Height - 2 - hScrollHeight),
                        new Point(Width - 2, Height - 2 - hScrollHeight));
                    g.DrawLine(insideLowerRight, new Point(Width - 2 - vScrollWidth,
                        Height - 2), new Point(Width - 2 - vScrollWidth, 2));

                    break;
                case BorderStyle.FixedSingle:
                    var fixedSingle = new Pen(Color.FromArgb(127, 157, 185), 1);
                    g.DrawRectangle(fixedSingle, 0, 0, Width - 1 - vScrollWidth,
                        Height - 1 - hScrollHeight);
                    break;
            }
        }
        private void DrawDiagnostic(Graphics g)
        {
            var message = string.Format(
                "Diagnostics Mode: On\r\n" +
                "Generate Time: {0}\r\n" +
                "DrawTime: {1}\r\n" +
                "Set Time: {2}\r\n" +
                "Auto Scroll Pos: {3}", generateTime, drawTime, setTime, AutoScrollPosition);

            g.DrawString(message, new Font("Arial", 16F, GraphicsUnit.Pixel),
                new SolidBrush(Color.Red), 10, 10);
        }

        private void DrawAnnotations(Graphics g)
        {
            foreach (Annotation annote in _annotations)
            {
                // float size = (float)annote.Font.Size * (1 / _currentZoom);
                var location = new Point(Convert.ToInt32(annote.X * _currentZoom),
                    Convert.ToInt32(annote.Y * _currentZoom));

                var fontSize = annote.Font.Size * _currentZoom;

                g.DrawString(annote.Text, new Font(annote.Font.FontFamily, fontSize,
                    annote.Font.Style, GraphicsUnit.Pixel), new SolidBrush(annote.Color), location);
            }
        }


        #endregion

        #region Drag Methods

        /// <summary>
        /// Used to control dragging options
        /// </summary>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (backup != null && resized != null)
            {
                // Determine if the selected coord is on top of an annotation
                selectedAnnote = GetAnnotationAt(e.X, e.Y);

                if (selectedAnnote != null)
                {
                    _annotations.Remove(selectedAnnote);

                    selectedAnnote.Location = new Point(e.X, e.Y);
                }
                else if (_allowDrag)
                {
                    lmbDown = true;
                    dragStart = new Point(e.X, e.Y);
                }
            }
        }
        /// <summary>
        /// Used to control dragging options
        /// </summary>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (selectedAnnote != null)
            {
                selectedAnnote.Location = new Point(e.X, e.Y);

                Invalidate();
            }
            else if (_allowDrag && lmbDown && dragStart != Point.Empty)
            {
                var dragCurrent = new Point(e.X, e.Y);

                if (ValidateDragWindowSize(dragCurrent))
                {
                    dragWindow = GenerateRectangle(dragStart, new Point(e.X, e.Y));

                    Invalidate();
                }
            }
        }
        /// <summary>
        /// Used to control dragging options
        /// </summary>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (selectedAnnote != null)
            {
                _annotations.Add(selectedAnnote, true);

                selectedAnnote = null;
            }
            else if (lmbDown && e.Button == MouseButtons.Right)
            {
                // Cancel the current drag
                dragWindow = Rectangle.Empty;
                lmbDown = false;
            }
            else if (lmbDown && e.Button == MouseButtons.Left)
            {
                // End the drag
                lmbDown = false;

                if (dragStart == dragEnd)
                    return;

                dragEnd = new Point(e.X, e.Y);

                if (dragStart != Point.Empty && ValidateDragWindowSize(dragEnd))
                {
                    switch (_dragOptions)
                    {
                        case DragOptions.Prompt:
                            dragWindowSave = dragWindow;
                            cmnuPrompt.Show(this, dragEnd);
                            break;
                        case DragOptions.Copy:
                            Copy();
                            break;
                        case DragOptions.Zoom:
                            Zoom();
                            break;
                    }
                }

                dragWindow = Rectangle.Empty;
                dragStart = Point.Empty;
                dragEnd = Point.Empty;
            }
        }


        private bool ValidateDragWindowSize(Point dragCurrent)
        {
            var dragWindowRect = GenerateRectangle(dragStart, dragCurrent);

            return dragWindowRect.Width >= _dragWindowMinimum.Width && dragWindowRect.Height >= _dragWindowMinimum.Height;
        }

        private Rectangle GenerateRectangle(Point a, Point b)
        {
            var topLeft = new Point(0, 0);
            var bottomRight = new Point(0, 0);

            if (a.X < b.X)
            {   // Dragging to the right.
                if (a.Y < b.Y)
                {   // Right & Down
                    topLeft = a;
                    bottomRight = b;
                }
                else
                {   // Right & Up
                    topLeft = new Point(a.X, b.Y);
                    bottomRight = new Point(b.X, a.Y);
                }
            }
            else if (b.X < a.X)
            {   // Dragging to the left.
                if (a.Y < b.Y)
                {   // Left & Down
                    topLeft = new Point(b.X, a.Y);
                    bottomRight = new Point(a.X, b.Y);
                }
                else
                {   // Left & Up
                    topLeft = b;
                    bottomRight = a;
                }
            }

            var width = bottomRight.X - topLeft.X;
            var height = bottomRight.Y - topLeft.Y;

            var rectangle = new Rectangle(topLeft, new Size(width, height));

            return rectangle;
        }


        private void PromptMenuItem_Clicked(object sender, EventArgs e)
        {
            if (((ToolStripMenuItem)sender).Text == "&Copy")
                Copy();
            else
                Zoom();
        }

        private void StretchMenuItem_Clicked(object sender, EventArgs e)
        {
            FitHeight();
            //FitWidth();
        }

        private void Copy()
        {
            // Up/Down convert drag window size to match
            var multiplier = 1 / _currentZoom;

            var dragWidth = (int)(dragWindowSave.Width * multiplier);
            var dragHeight = (int)(dragWindowSave.Height * multiplier);

            var xOffset = -AutoScrollPosition.X;
            var yOffset = -AutoScrollPosition.Y;

            var dragX = Convert.ToInt32(((float)dragWindowSave.X + xOffset) * multiplier);
            var dragY = Convert.ToInt32(((float)dragWindowSave.Y + yOffset) * multiplier);

            var copy = new Bitmap(dragWidth, dragHeight);

            var srcRect = new Rectangle(dragX, dragY, dragWidth, dragHeight);

            using (var g = Graphics.FromImage(copy))
            {
                var destRect = new Rectangle(0, 0, dragWidth, dragHeight);

                g.DrawImage(backup, destRect, srcRect, GraphicsUnit.Pixel);
            }

            Clipboard.SetDataObject(copy, true);

            ImagePortionCopied?.Invoke(this, new ImagePortionCopiedEventArgs(copy, srcRect));

            Invalidate();
        }
        private void Zoom()
        {
            // This version of the picturebox control no longer needs an autoscroll parent

            if (resized == null)
                return; // If there is no image there is nothing to zoom in on

            // The zoom window will never be proportional to the container, so one
            // side will get filled while the other gets centered. The larger % will
            // determine which gets filled, and which gets centered
            var xRatio = Width / (float)dragWindowSave.Width;
            var yRatio = Height / (float)dragWindowSave.Height;

            var xAdjust = 0;
            var yAdjust = 0;

            var largerRatio = (xRatio < yRatio) ? xRatio : yRatio;

            // The cumulative zoom cannot exceed the maximum zoom;
            if ((largerRatio * _currentZoom) > _maximumZoom)
                largerRatio = _maximumZoom / _currentZoom;

            if (dragWindowSave.Width * largerRatio > Width)
                largerRatio = Width / (float)dragWindowSave.Width;

            if (dragWindowSave.Height * largerRatio > Height)
                largerRatio = Height / (float)dragWindowSave.Height;

            yAdjust = Convert.ToInt32(
                (Height - dragWindowSave.Height * largerRatio) / 2F);
            xAdjust = Convert.ToInt32(
                (Width - dragWindowSave.Width * largerRatio) / 2F);

            var xScrollPos = Math.Max(Convert.ToInt32((-AutoScrollPosition.X + (float)dragWindowSave.X) * largerRatio) - xAdjust, 0);
            var yScrollPos = Math.Max(Convert.ToInt32((-AutoScrollPosition.Y + (float)dragWindowSave.Y) * largerRatio) - yAdjust, 0);

            CurrentZoom *= largerRatio;

            AutoScrollPosition = new Point(xScrollPos, yScrollPos);

            Invalidate();
        }

        public void DoRestore()
        {
            if (Math.Abs(_currentZoom - _defaultZoom) > float.Epsilon)
                CurrentZoom = _defaultZoom;
        }

        #endregion

        #region Annotation Methods

        /// <summary>
        /// Gets the annotation at the specified coordinates
        /// </summary>
        public Annotation GetAnnotationAt(int x, int y)
        {
            var ratio = (1 / _currentZoom);

            var xOffset = -AutoScrollPosition.X;
            var yOffset = -AutoScrollPosition.Y;

            var adjustedX = Convert.ToInt32((x + xOffset) * ratio);
            var adjustedY = Convert.ToInt32((y + yOffset) * ratio);

            foreach (Annotation annote in _annotations)
                if (annote.Bounds.Contains(adjustedX, adjustedY))
                    return annote;

            return null;
        }
        /// <summary>
        /// Gets the annotation at the specified coordinates
        /// </summary>
        public Annotation GetAnnotationAt(Point location)
        {
            return GetAnnotationAt(location.X, location.Y);
        }


        private void Annotations_AnnotationsChanged(object sender, EventArgs e)
        {
            GenerateResizedImage(); // Recreate the image with annotations

            Invalidate(); // Redraw the image
        }


        #endregion

        #region Scrolling Methods

        public new void Scroll(ScrollDirection direction, bool smallChange)
        {
            var x = -AutoScrollPosition.X;
            var y = -AutoScrollPosition.Y;

            var change = (smallChange) ? 100 : 250;

            if (direction == ScrollDirection.Left || direction == ScrollDirection.Up)
                if (direction == ScrollDirection.Up)
                    y = y - change;
                else
                    x = x - change;
            else
                if (direction == ScrollDirection.Down)
                y = y + change;
            else
                x = x + change;

            AutoScrollPosition = new Point(x, y);

            Invalidate();
        }


        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == WM_MOUSEWHEEL)
            {
                ProcessWheelMovement(ref m);

                Invalidate();
            }
            else if (m.Msg == WM_HSCROLL || m.Msg == WM_VSCROLL)
            {
                Invalidate();

                ScrollPositionChanged?.Invoke(this, new EventArgs());
            }
        }

        protected override bool ProcessKeyMessage(ref Message m)
        {
            if (m.Msg == WM_KEYDOWN)
                return ProcessKeyDown(ref m);
            else
                return base.ProcessKeyMessage(ref m);
        }

        private void ProcessWheelMovement(ref Message m)
        {
            var wParam = (int)m.WParam;

            if (wParam > 0)
            {
                // The wheel is being scrolled up
                if ((wParam & MK_SHIFT) == MK_SHIFT)
                {
                    if (_currentZoom + .10F < _maximumZoom)
                        CurrentZoom += .10F;
                }
                else if ((wParam & MK_CONTROL) == MK_CONTROL)
                    Scroll(ScrollDirection.Left, true);
            }
            else
            {
                // The wheel is being scrolled down
                if ((wParam & MK_SHIFT) == MK_SHIFT)
                {
                    if (_currentZoom - .10F > _minimumZoom)
                        CurrentZoom -= .10F;
                }
                else if ((wParam & MK_CONTROL) == MK_CONTROL)
                    Scroll(ScrollDirection.Right, true);
            }
        }

        private bool ProcessKeyDown(ref Message m)
        {
            switch ((int)m.WParam)
            {
                case VK_PRIOR:
                    Scroll(ScrollDirection.Up, false);
                    return true;
                case VK_NEXT:
                    Scroll(ScrollDirection.Down, false);
                    return true;
                case VK_END:
                    Scroll(ScrollDirection.Right, false);
                    return true;
                case VK_HOME:
                    Scroll(ScrollDirection.Left, false);
                    return true;
                case VK_LEFT:
                    Scroll(ScrollDirection.Left, true);
                    return true;
                case VK_UP:
                    Scroll(ScrollDirection.Up, true);
                    return true;
                case VK_RIGHT:
                    Scroll(ScrollDirection.Right, true);
                    return true;
                case VK_DOWN:
                    Scroll(ScrollDirection.Down, true);
                    return true;
                default:
                    return false;
            }
        }


        #endregion

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            Select();

            Invalidate(); // Force a redraw
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            Invalidate(); // Force a redraw

        }

        protected override void OnDoubleClick(EventArgs e)
        {
            base.OnDoubleClick(e);

            if (Math.Abs(_currentZoom - _defaultZoom) > float.Epsilon)
                CurrentZoom = _defaultZoom;
        }


        /// <summary>
        /// Rotate or flip the original image
        /// </summary>
        /// <param name="type"></param>
        public void RotateFlip(RotateFlipType type)
        {
            backup.RotateFlip(type);

            GenerateResizedImage();

            Invalidate();
        }
        /// <summary>
        /// Will automatically shrink or enlarge the image to match the height of the control
        /// </summary>
        public void FitHeight()
        {
            if (backup == null)
                return;

            var fitHeightZoom = (Height - 1) / (float)backup.Height;

            if (fitHeightZoom < _minimumZoom)
                fitHeightZoom = _minimumZoom;

            CurrentZoom = fitHeightZoom;
        }
        /// <summary>
        /// Will automatically shrink or enlarge the image to match the width of the control
        /// </summary>
        public void FitWidth()
        {
            if (backup == null)
                return;

            var fitWidthZoom = (Width - 1) / (float)backup.Width;

            if (fitWidthZoom < _minimumZoom)
                fitWidthZoom = _minimumZoom;

            CurrentZoom = fitWidthZoom;
        }

        public void RestoreSize()
        {
            if (Math.Abs(_currentZoom - _defaultZoom) > float.Epsilon)
                CurrentZoom = _defaultZoom;
        }

        /// <summary>
        /// Gets the cursor used by the picturebox to add an annotation
        /// </summary>
        public static Cursor AnnotationCursor
        {
            get
            {
                var assembly = Assembly.GetAssembly(typeof(PictureBoxEx));

                if (assembly != null)
                    return new Cursor(assembly.GetManifestResourceStream("Tpsc.Controls.Annotation.cur"));

                return null;
            }
        }


        #region Nested Classes

        /// <summary>
        /// AnnotationCollection is used to store annotations for the picturebox.
        /// Annotations are designed to be xml serializable
        /// </summary>
        public sealed class AnnotationCollection : CollectionBase
        {
            /// <summary>
            /// Creates a new annotation collection
            /// </summary>
            public AnnotationCollection()
            {
                AnnotationsChanged += OnAnnotationCollection_AnnotationsChanged;
            }
            /// <summary>
            /// Creates a new annotation collection
            /// </summary>
            public AnnotationCollection(PictureBoxEx owner)
            {
                this.owner = owner;

                AnnotationsChanged += OnAnnotationCollection_AnnotationsChanged;
            }
            /// <summary>
            /// Creates a new annotation collection
            /// </summary>
            public AnnotationCollection(Annotation[] annotations)
            {
                AnnotationsChanged += OnAnnotationCollection_AnnotationsChanged;

                foreach (var annote in annotations)
                    Add(annote, false);
            }
            /// <summary>
            /// Creates a new annotation collection
            /// </summary>
            public AnnotationCollection(PictureBoxEx owner, Annotation[] annotations) : this(annotations)
            {
                this.owner = owner;
            }


            private readonly PictureBoxEx owner = null;

            /// <summary>
            /// Fires whenever an annotation is changed, added or removed
            /// </summary>
            public event EventHandler AnnotationsChanged;

            /// <summary>
            /// Add an annotation to the collection
            /// </summary>
            /// <param name="annotation">Annotation to be added</param>
            /// <param name="adjustLocation">True if the annotation should be adjusted for the picturebox's current zoom</param>
            /// <returns>The index of the newly added annotation</returns>
            public int Add(Annotation annotation, bool adjustLocation)
            {
                var found = Remove(annotation);

                annotation.Changed += annotation_Changed;

                if (adjustLocation && owner != null)
                {
                    var ratio = 1F / owner._currentZoom;

                    var adjustedX = Convert.ToInt32((annotation.X + -owner.AutoScrollPosition.X) * ratio);
                    var adjustedY = Convert.ToInt32((annotation.Y + -owner.AutoScrollPosition.Y) * ratio);

                    annotation.Location = new Point(adjustedX, adjustedY);
                }

                var index = InnerList.Add(annotation);

                if (!found)
                {
                    AnnotationsChanged?.Invoke(this, new EventArgs());
                }

                return index;
            }
            /// <summary>
            /// Add an annotation to the collection
            /// </summary>
            /// <param name="text">The annotations desired text</param>
            /// <param name="font">The annotations desired font</param>
            /// <param name="color">The annotations desired color</param>
            /// <param name="location">The annotations desired location</param>
            /// <param name="adjustLocation">True if the annotation should be adjusted for the picturebox's current zoom</param>
            /// <returns>The index of the newly added annotation</returns>
            public int Add(string text, Font font, Color color, Point location, bool adjustLocation)
            {
                var newAnnote = new Annotation(text, font, color, location);

                newAnnote.Changed += annotation_Changed;

                return Add(newAnnote, adjustLocation);
            }
            /// <summary>
            /// Adds one or more annotations to the collection w/o adjustment
            /// </summary>
            /// <param name="annotations"></param>
            public void AddRange(Annotation[] annotations)
            {
                foreach (var annotation in annotations)
                {
                    var found = Remove(annotation);

                    InnerList.Add(annotation);

                    if (!found)
                        annotation.Changed += annotation_Changed;
                }

                AnnotationsChanged?.Invoke(this, new EventArgs());
            }

            /// <summary>
            /// Removes an annotation from the collection
            /// </summary>
            /// <param name="annotation"></param>
            /// <returns></returns>
            public bool Remove(Annotation annotation)
            {
                foreach (Annotation annote in InnerList)
                    if (annote.Guid == annotation.Guid)
                    {
                        annotation.Changed -= annotation_Changed;

                        InnerList.Remove(annote);

                        AnnotationsChanged?.Invoke(this, new EventArgs());

                        return true;
                    }

                return false;
            }


            /// <summary>
            /// Removes all annotations from the collection
            /// </summary>
            protected override void OnClear()
            {
                if (InnerList.Count <= 0)
                    return;

                InnerList.Clear();
                AnnotationsChanged?.Invoke(this, new EventArgs());
            }

            /// <summary>
            /// Returns the annotatinos at the desired index
            /// </summary>
            public Annotation this[int index]
            {
                get
                {
                    return (Annotation)InnerList[index];
                }
                set
                {
                    InnerList[index] = value;
                }
            }

            /// <summary>
            /// Returns the current collection as an array of annotations
            /// </summary>
            /// <returns></returns>
            public Annotation[] ToArray()
            {
                return (Annotation[])InnerList.ToArray(typeof(Annotation));
            }


            /// <summary>
            /// Opens a collection from a file
            /// </summary>
            /// <param name="fileName">File name and path the the file</param>
            public void Open(string fileName)
            {
                Clear();

                if (File.Exists(fileName))
                {
                    var type = typeof(Annotation[]);

                    using (var fs = new FileStream(fileName, FileMode.Open))
                    {
                        System.Xml.Serialization.XmlSerializer x;
                        x = new System.Xml.Serialization.XmlSerializer(type);
                        AddRange((Annotation[])x.Deserialize(fs));
                    }
                }

                AnnotationsChanged?.Invoke(this, new EventArgs());
            }

            /// <summary>
            /// Saves the collection to a file
            /// </summary>
            /// <param name="fileName">File name and path for the desired file</param>
            public void Save(string fileName)
            {
                try
                {

                    using (var sw = new StreamWriter(new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.Read)))
                    {
                        var type = typeof(Annotation[]);

                        var x = new XmlSerializer(type);
                        x.Serialize(sw, ToArray());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Error saving to file {0}: {1}", fileName, ex.Message));
                }
            }


            private void annotation_Changed(object sender, EventArgs e)
            {
                if (InnerList.Contains(sender))
                {
                    AnnotationsChanged?.Invoke(sender, e);
                }
            }


            private void OnAnnotationCollection_AnnotationsChanged(object sender, EventArgs e)
            {

            }
        }

        #endregion
    }

    /// <summary>
    /// The different directions the form can be scrolled
    /// </summary>
    public enum ScrollDirection
    {
        /// <summary>
        /// Scroll Left
        /// </summary>
        Left = 37,
        /// <summary>
        /// Scroll Up
        /// </summary>
        Up = 38,
        /// <summary>
        /// Scroll Right
        /// </summary>
        Right = 39,
        /// <summary>
        /// Scroll Down
        /// </summary>
        Down = 40,
    }

    /// <summary>
    /// Options available after selecting a portion of the image
    /// </summary>
    public enum DragOptions
    {
        /// <summary>
        /// Prompt the user to choose to drag or copy
        /// </summary>
        Prompt = 0,
        /// <summary>
        /// Zoom in on the image
        /// </summary>
        Zoom,
        /// <summary>
        /// Copy the selected portion of the image
        /// </summary>
        Copy
    }

    /// <summary>
    /// Facilitates the copy drag window event
    /// </summary>
    public delegate void ImagePortionCopiedEventHandler(object sender, ImagePortionCopiedEventArgs e);
    /// <summary>
    /// Event argumets that pass the copied portions of the image.
    /// </summary>
    public class ImagePortionCopiedEventArgs : EventArgs
    {
        /// <summary>
        /// Event argumets that pass the copied portions of the image.
        /// </summary>
        public ImagePortionCopiedEventArgs(Bitmap image, Rectangle rectangle)
        {
            Image = image;
            Rectangle = rectangle;
        }


        /// <summary>
        /// The copied portion of the image.
        /// </summary>
        public Bitmap Image { get; set; } = null;

        /// <summary>
        /// The drag window info used to copy this portion of the image.
        /// </summary>
        public Rectangle Rectangle { get; set; }
    }
}