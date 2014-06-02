using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Xml.Serialization;

namespace ExtraControls
{
	/// <summary>
	/// Summary description for PictureBox.
	/// </summary>
	public class PictureBoxEx : System.Windows.Forms.ScrollableControl
	{
		private ContextMenu cmnuPrompt;

		public PictureBoxEx()
		{
			InitComponent();

			base.SetStyle(ControlStyles.DoubleBuffer, true);
			base.SetStyle(ControlStyles.UserPaint, true);
			base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);

			this.Paint += new PaintEventHandler(PictureBox_Paint);
			this.Click += new EventHandler(PictureBox_Click);

			_annotations = new AnnotationCollection(this);

			dragPen = new Pen(Color.Black, 1);
			dragPen.DashStyle = DashStyle.Custom;
			dragPen.DashPattern = dashPattern;

			_annotations.AnnotationsChanged += new EventHandler(OnAnnotationsChanged);
			AllowScrollingChanged			+= new EventHandler(OnAllowScrollingChanged);
			BorderStyleChanged				+= new EventHandler(OnBorderStyleChanged);
			CurrentZoomChanged				+= new EventHandler(OnCurrentZoomChanged);
			DefaultZoomChanged				+= new EventHandler(OnDefaultZoomChanged);
			DoubleClickRestoreChanged		+= new EventHandler(OnDoubleClickRestoreChanged);
			DragOptionsChanged				+= new EventHandler(OnDragOptionsChanged);
			DragWindowColorChanged			+= new EventHandler(OnDragWindowColorChanged);
			DragWindowMinimumChanged		+= new EventHandler(OnDragWindowMinimumChanged);
			DragWindowDashPatternChanged	+= new EventHandler(OnDragWindowDashPatternChanged);
			DrawModeChanged					+= new EventHandler(OnDrawModeChanged);
			ImageChanged					+= new EventHandler(OnImageChanged);
			ImagePortionCopied				+= new ImagePortionCopiedEventHandler(OnImagePortionCopied);
			MaximumZoomChanged				+= new EventHandler(OnMaximumZoomChanged);
			MinimumZoomChanged				+= new EventHandler(OnMinimumZoomChanged);
			ScrollPositionChanged			+= new EventHandler(OnScrollPositionChanged);
		}


		private void InitComponent()
		{
			this.cmnuPrompt = new ContextMenu();
			this.SuspendLayout();
			//
			// cmnuPrompt
			//
			this.cmnuPrompt.MenuItems.Add("&Copy", new EventHandler(PromptMenuItem_Clicked));
			this.cmnuPrompt.MenuItems.Add("&Zoom", new EventHandler(PromptMenuItem_Clicked));
			// 
			// PictureBox
			// 
			this.Controls.Add(this.vScrollBar);
			this.Controls.Add(this.hScrollBar);
			this.ResumeLayout(false);
		}
	

		#region Fields Properties and Events

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
				_allowDrag = value;
			}
		}
		private bool _allowDrag = true;
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

					BorderStyleChanged(this, new EventArgs());

					Invalidate();
				}
			}
		}
		private BorderStyle _borderStyle = BorderStyle.None;
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
				if (value <= 0)
					throw new ArgumentOutOfRangeException("The current zoom cannot be set equal to or less the 0");

				if (value > _maximumZoom && _currentZoom == _maximumZoom)
					return;
				else if (value < _minimumZoom && _currentZoom == _minimumZoom)
					return;

				if (_currentZoom != value)
				{
					if (value > _maximumZoom)
						value = _maximumZoom;
					if (value < _minimumZoom)
						value = _minimumZoom;

					_currentZoom = value;

					if (backup != null)
						GenerateResizedImage();

					Invalidate();

					CurrentZoomChanged(this, new EventArgs());
				}
			}
		}
		private float _currentZoom = 1.0F;
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
				if (_defaultZoom != value)
				{
					_defaultZoom = value;

					DefaultZoomChanged(this, new EventArgs());
				}
			}
		}
		private float _defaultZoom = 1.0F;
		/// <summary>
		/// Fires when the DefaultZoom property has been changed
		/// </summary>
		[Description("Fires when the DefaultZoom property has been changed.")]
		public event EventHandler DefaultZoomChanged;
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

					DoubleClickRestoreChanged(this, new EventArgs());
				}
			}
		}
		private bool _doubleClickRestore = true;
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

					DragOptionsChanged(this, new EventArgs());
				}
			}
		}
		private DragOptions _dragOptions = DragOptions.Prompt;
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

					DragWindowColorChanged(this, new EventArgs());
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

					DragWindowMinimumChanged(this, new EventArgs());
				}
			}
		}
		private Size _dragWindowMinimum = SystemInformation.DragSize;
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

				foreach ( Single number in dashPattern )
				{
					if ( pattern == null )
						pattern += number.ToString();
					else
						pattern += "," + number.ToString();
				}

				return pattern;
			}
			set
			{
				try
				{
					string[] pattern = value.Split(',');

					dashPattern = new Single[pattern.Length];

					for ( int i = 0; i != pattern.Length; i++ )
						dashPattern[i] = Single.Parse(pattern[i].Trim());

					dragPen.DashPattern = dashPattern;

					DragWindowDashPatternChanged(this, new EventArgs());
				}
				catch
				{
					dashPattern = new Single[]{5,2,5,2};
				}
			}
		}
		private Single[] dashPattern = new Single[] { 5, 2, 5, 2 };
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
						GenerateResizedImage();

					DrawModeChanged(this, new EventArgs());

					Invalidate();
				}
			}
		}
		private InterpolationMode _drawMode = InterpolationMode.Default;
		/// <summary>
		/// Fires when the DrawMode property has been changed
		/// </summary>
		[Description("Fires when the DrawMode property has been changed.")]
		public event EventHandler DrawModeChanged;
		/// <summary>
		/// Gets or sets the current scroll position
		/// </summary>
		[Category("Behavior"),
		DefaultValue("0, 0"),
		Description("Controls the current position of the scrollbars."),
		Browsable(true)]
		public Point ScrollPosition
		{
			get
			{
				return new Point(xOffset, yOffset);
			}
			set
			{
				if (resized == null)
				{
					xOffset = 0;
					yOffset = 0;

					hScrollBar.Value = 0;
					vScrollBar.Value = 0;
				}
				else
				{
					xOffset = value.X;
					yOffset = value.Y;

					hScrollBar.Value = Convert.ToInt32(
						((float)xOffset / (float)resized.Width) * (float)hScrollBar.Maximum);
					vScrollBar.Value = Convert.ToInt32(
						((float)yOffset / (float)resized.Height) * (float)vScrollBar.Maximum);
				}
			}
		}
		/// <summary>
		/// Fires when the ScrollPosition property has been changed
		/// </summary>
		[Description("Fires when the ScrollPosition property has been changed."),
		Browsable(false)]
		public event EventHandler ScrollPositionChanged;
		/// <summary>
		/// Gets or sets the Image for the PictureBox (Returns a resized and annotated image if applicable)
		/// </summary>
		[Category("Appearance"),
		DefaultValue(null),
		Description("The Image for the PictureBox."),
		Browsable(true)]
		public Image Image
		{
			get
			{
				return resized;
			}
			set
			{
				DateTime started = DateTime.Now;

				if (value == null)
				{
					backup.Dispose();
					backup = null;

					resized.Dispose();
					resized = null;
				}
				else if (backup != value)
				{
					backup = value;

					if (_currentZoom != _defaultZoom)
					{
						_currentZoom = _defaultZoom;

						CurrentZoomChanged(this, new EventArgs());
					}
					
					_annotations.Clear();

					GenerateResizedImage();
				}

				ImageChanged(this, new EventArgs());

				setTime = (long)DateTime.Now.Subtract(started).TotalMilliseconds;

				Invalidate();

				AdjustScrollBars();
			}
		}
		/// <summary>
		/// Fires when the Image has been changed
		/// </summary>
		[Description("Fires when the Image has been changed.")]
		public event EventHandler ImageChanged;
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
				if (_maximumZoom != value)
				{
					_maximumZoom = value;

					MaximumZoomChanged(this, new EventArgs());

					Invalidate();
				}
			}
		}
		private float _maximumZoom = 3.0F;
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
				if (_minimumZoom != value)
				{
					_minimumZoom = value;

					MinimumZoomChanged(this, new EventArgs());

					Invalidate();
				}
			}
		}
		private float _minimumZoom = 0.10F;
		/// <summary>
		/// Gets or sets true if the picturebox should be placed in diagnostic mode
		/// </summary>
		[Browsable(false)]
		public bool DiagnosticMode
		{
			get
			{
				return _diagnosticMode;
			}
			set
			{
				_diagnosticMode = value;
			}
		}
		private bool _diagnosticMode = false;
		/// <summary>
		/// Fires when the MinimumZoom property has been changed
		/// </summary>
		[Description("Fires when the MinimumZoom property has been changed.")]
		public event EventHandler MinimumZoomChanged;
		/// <summary>
		/// Gets picturebox's collection of annotations
		/// </summary>
		public AnnotationCollection Annotations
		{
			get
			{
				return _annotations;
			}
		}
		private AnnotationCollection _annotations = null;
		/// <summary>
		/// Fires when a portion of the image has been copied
		/// </summary>
		[Description("Fires when a portion of the image has been copied.")]
		public event ImagePortionCopiedEventHandler ImagePortionCopied;

		private int xOffset = 0; // Tracks the scrolled x position
		private int yOffset = 0; // Tracks the scrolled y position
		private Pen dragPen = null; // The drag pen is used to draw the dragwindow
		private Image backup = null; // Holds the original image that is used whenever the image is resized
		private Image resized = null; // Holds the entire image resized to the currentzoom
		private Annotation selectedAnnote = null; // Holds the currently selected annotation

		private const int MK_CONTROL    = 0x0008;
		private const int MK_SHIFT      = 0x0004;
		private const int VK_PRIOR		= 0x0021; // PageDown
		private const int VK_NEXT		= 0x0022; // PageUp
		private const int VK_END		= 0x0023;
		private const int VK_HOME		= 0x0024;
		private const int VK_LEFT		= 0x0025;
		private const int VK_UP			= 0x0026;
		private const int VK_RIGHT      = 0x0027;
		private const int VK_DOWN       = 0x0028;

		private const int WM_KEYDOWN	= 0x0101;
		private const int WM_MOUSEWHEEL = 0x020A;

		private int vScrollWidth
		{
			get
			{
				return ((vScrollBar.Visible) ? vScrollBar.Width : 0);
			}
		}
		private int hScrollHeight
		{
			get
			{
				return ((hScrollBar.Visible) ? hScrollBar.Height : 0);
			}
		}


		private long generateTime;
		private long drawTime;
		private long setTime;

		#endregion

		private void GenerateResizedImage()
		{
			DateTime started = DateTime.Now; // Used as part of the diag mode

			// Get the resized dimentions
			int resizedWidth = Convert.ToInt32(backup.Width * _currentZoom);
			int resizedHeight = Convert.ToInt32(backup.Height * _currentZoom);

			resized = new Bitmap(resizedWidth, resizedHeight);

			// Draw the backup image onto the resized image
			using (Graphics g = Graphics.FromImage(resized))
			{
				g.InterpolationMode = (_currentZoom < 1F) ? _drawMode : InterpolationMode.Default;

				Rectangle srcRect = new Rectangle(0, 0, backup.Width, backup.Height);
				Rectangle destRect = new Rectangle(0, 0, resizedWidth, resizedHeight);

				g.DrawImage(backup, destRect, srcRect, GraphicsUnit.Pixel);

				// Add any annotations to the resized image
				DrawAnnotations(g);
			}

			generateTime = (long)DateTime.Now.Subtract(started).TotalMilliseconds;
		}


		private void PictureBox_Paint(object sender, PaintEventArgs e)
		{
			// Handles the painting of the entire control

			DateTime started = DateTime.Now;

			e.Graphics.FillRectangle(new SolidBrush(this.BackColor), this.ClientRectangle);

			if (backup == null || resized == null)
				return;

			int width = resized.Width - vScrollWidth;
			int height = resized.Height - hScrollHeight;

			e.Graphics.DrawImageUnscaled(resized, -xOffset, -yOffset, width, height);

			if (vScrollBar.Visible && hScrollBar.Visible)
				e.Graphics.FillRectangle(new SolidBrush(BackColor),
					Width - vScrollBar.Width, Height - hScrollBar.Height,
					vScrollBar.Width, hScrollBar.Height);

			DrawSelectedAnnotation(e.Graphics);

			DrawDragWindow(e.Graphics);

			DrawBorder(e.Graphics);

			drawTime = (long)DateTime.Now.Subtract(started).TotalMilliseconds;

			if (_diagnosticMode)
				DrawDiagnostic(e.Graphics);
		}

		private void PictureBox_Click(object sender, EventArgs e)
		{
			Invalidate();

			Select();
		}

		private void DrawBorder(Graphics g)
		{
			switch (_borderStyle)
			{
				case BorderStyle.Fixed3D :
					Pen outsideUpperLeft = new Pen(Color.FromArgb(172, 168, 152), 1);
					g.DrawLine(outsideUpperLeft, new Point(0, Height - 1), new Point(0, 0));
					g.DrawLine(outsideUpperLeft, new Point(0, 0), new Point(Width - 1));

					Pen insideUpperLeft = new Pen(Color.FromArgb(113, 111, 110), 1);
					g.DrawLine(insideUpperLeft, new Point(1, Height - 2), new Point(1, 1));
					g.DrawLine(insideUpperLeft, new Point(1, 1), new Point(Width - 2, 1));

					Pen outsideLowerRight = new Pen(Color.FromArgb(255, 255, 255), 1);
					g.DrawLine(outsideLowerRight, new Point(0, Height - 1 - hScrollHeight), 
						new Point(Width - 1, Height - 1 - hScrollHeight));
					g.DrawLine(outsideLowerRight, new Point(Width - 1 - vScrollWidth, 
						Height - 1), new Point(Width - 1 - vScrollWidth, 0));

					Pen insideLowerRight = new Pen(Color.FromArgb(241, 239, 226), 1);
					g.DrawLine(insideLowerRight, new Point(1, Height - 2 - hScrollHeight), 
						new Point(Width - 2, Height - 2 - hScrollHeight));
					g.DrawLine(insideLowerRight, new Point(Width - 2 - vScrollWidth, 
						Height - 2), new Point(Width - 2 - vScrollWidth, 2));

					break;
				case BorderStyle.FixedSingle :
					Pen fixedSingle = new Pen(Color.FromArgb(127, 157, 185), 1);
					g.DrawRectangle(fixedSingle, 0, 0, Width - 1 - vScrollWidth, 
						Height - 1 - hScrollHeight);
					break;
			}
		}
		
		private void DrawDiagnostic(Graphics g)
		{
			string message = string.Format(
				"Diagnostics Mode: On\r\n" +
				"Generate Time: {0}\r\n" +
				"DrawTime: {1}\r\n" +
				"Set Time: {2}", generateTime, drawTime, setTime);

			g.DrawString(message, new Font("Arial", 16F, GraphicsUnit.Pixel),
				new SolidBrush(Color.Red), 10, 10);
		}


		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged (e);

			AdjustScrollBars();

			// Check to see if there is more room for the image horizontally
			if (resized != null)
			{
				int diff;
				
				diff = resized.Width - xOffset;
				if (diff < Width && resized.Width > Width)
					xOffset -= Math.Max(0, Width - diff);

				diff = resized.Height - yOffset;
				if (diff < Height && resized.Height > Height)
					yOffset -= Math.Max(0, Height - diff);
			}

			Invalidate();
		}

		protected override void OnInvalidated(InvalidateEventArgs e)
		{
			AdjustScrollBars();

			base.OnInvalidated (e);
		}

		protected override void OnDoubleClick(EventArgs e)
		{
			base.OnDoubleClick (e);

			if (_currentZoom != _defaultZoom)
			{
				xOffset = 0;
				yOffset = 0;

				hScrollBar.Value = 0;
				vScrollBar.Value = 0;

				CurrentZoom = _defaultZoom;
			}
		}


		#region Annotation Methods

		public Annotation GetAnnotationAt(int x, int y)
		{
			float ratio = (1 / _currentZoom);

			int adjustedX = Convert.ToInt32((x + xOffset) * ratio);
			int adjustedY = Convert.ToInt32((y + yOffset) * ratio);

			foreach (Annotation annote in _annotations)
				if (annote.Bounds.Contains(adjustedX, adjustedY))
					return annote;

			return null;
		}

		public Annotation GetAnnotationAt(Point location)
		{
			return GetAnnotationAt(location.X, location.Y);
		}

		
		protected virtual void OnAnnotationsChanged(object sender, EventArgs e)
		{
			GenerateResizedImage();

			Invalidate();
		}


		private void DrawSelectedAnnotation(Graphics g)
		{
			if (selectedAnnote != null)
			{
				float fontSize = selectedAnnote.Font.Size * _currentZoom;

				Font font = new Font(selectedAnnote.Font.FontFamily,
					fontSize, selectedAnnote.Font.Style, GraphicsUnit.Pixel);

				Brush brush = new SolidBrush(selectedAnnote.Color);

				SizeF sizeF = g.MeasureString(selectedAnnote.Text, font);

				g.DrawString(selectedAnnote.Text, font, brush, selectedAnnote.Location);

				g.DrawRectangle(dragPen, selectedAnnote.Location.X, selectedAnnote.Location.Y,
					(int)sizeF.Width, (int)sizeF.Height);
			}
		}


		private void DrawAnnotations(Graphics g)
		{
			foreach (Annotation annote in _annotations)
			{
				// float size = (float)annote.Font.Size * (1 / _currentZoom);
				Point location = new Point(Convert.ToInt32(annote.X * _currentZoom), 
					Convert.ToInt32(annote.Y * _currentZoom));

				float fontSize = annote.Font.Size * _currentZoom;

				g.DrawString(annote.Text, new Font(annote.Font.FontFamily, fontSize, 
					annote.Font.Style, GraphicsUnit.Pixel), new SolidBrush(annote.Color), location);
			}
		}


		#endregion

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown (e);

			if (backup == null)
				return; // nothing to do if there is no image

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
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove (e);

			if (selectedAnnote != null)
			{
				selectedAnnote.Location = new Point(e.X, e.Y);

				Invalidate();
			}
			else if (_allowDrag && lmbDown && dragStart != Point.Empty)
			{
				Point dragCurrent = new Point(e.X, e.Y);

				if (ValidateDragWindowSize(dragCurrent))
				{
					dragWindow = GenerateRectangle(dragStart, new Point(e.X, e.Y));

					Invalidate();
				}
			}
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp (e);

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
						case DragOptions.Prompt :
							dragWindowSave = dragWindow;
							cmnuPrompt.Show(this, dragEnd);
							break;
						case DragOptions.Copy :
							Copy();
							break;
						case DragOptions.Zoom :
							Zoom();
							break;
					}
				}

				dragWindow = Rectangle.Empty;
				dragStart = Point.Empty;
				dragEnd = Point.Empty;
			}
		}


		#region Drag Window Methods

		private Point dragStart = Point.Empty;
		private Point dragEnd = Point.Empty;
		private bool lmbDown = false;
		private Rectangle dragWindow = Rectangle.Empty;
		private Rectangle dragWindowSave = Rectangle.Empty;

		private bool ValidateDragWindowSize(Point dragCurrent)
		{
			Rectangle dragWindow = GenerateRectangle(dragStart, dragCurrent);

			if (dragWindow.Width < _dragWindowMinimum.Width || 
				dragWindow.Height < _dragWindowMinimum.Height)
				return false;
			else
				return true;
		}
		private Rectangle GenerateRectangle(Point a, Point b)
		{
			Point topLeft	  = new Point(0,0);
			Point bottomRight = new Point(0,0);
			
			if ( a.X < b.X )
			{	// Dragging to the right.
				if ( a.Y < b.Y )
				{	// Right & Down
					topLeft		= a;
					bottomRight = b;
				}
				else
				{	// Right & Up
					topLeft		= new Point(a.X, b.Y);
					bottomRight = new Point(b.X, a.Y);
				}
			}
			else if ( b.X < a.X )
			{	// Dragging to the left.
				if ( a.Y < b.Y )
				{	// Left & Down
					topLeft		= new Point(b.X, a.Y);
					bottomRight	= new Point(a.X, b.Y);
				}
				else
				{	// Left & Up
					topLeft		= b;
					bottomRight = a;
				}
			}

			int width	= bottomRight.X - topLeft.X;
			int height	= bottomRight.Y - topLeft.Y;

			Rectangle rectangle = new Rectangle(topLeft, new Size(width, height));

			return rectangle;
		}


		private void DrawDragWindow(Graphics g)
		{
			if (dragWindow != Rectangle.Empty)
				g.DrawRectangle(dragPen, dragWindow);
		}


		private void Copy()
		{
			// Up/Down convert drag window size to match
			float multiplier = 1 / _currentZoom;

			int dragWidth = (int)((float)dragWindowSave.Width * multiplier);
			int dragHeight = (int)((float)dragWindowSave.Height * multiplier);

			int dragX = Convert.ToInt32(((float)dragWindowSave.X + xOffset) * multiplier);
			int dragY = Convert.ToInt32(((float)dragWindowSave.Y + yOffset) * multiplier);

			Bitmap copy = new Bitmap(dragWidth, dragHeight);

			Rectangle srcRect = new Rectangle(dragX, dragY, dragWidth, dragHeight);

			using (Graphics g = Graphics.FromImage(copy))
			{
				Rectangle destRect = new Rectangle(0, 0, dragWidth, dragHeight);

				g.DrawImage(backup, destRect, srcRect, GraphicsUnit.Pixel);
			}

			Clipboard.SetDataObject(copy, true);

			ImagePortionCopied(this, 
				new ImagePortionCopiedEventArgs(copy, srcRect));

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
			float xRatio = (float)Width / (float)dragWindowSave.Width;
			float yRatio = (float)Height / (float)dragWindowSave.Height;

			float largerRatio;
			int xAdjust = 0;
			int yAdjust = 0;

			largerRatio = (xRatio < yRatio) ? xRatio : yRatio;

			// The cumulative zoom cannot exceed the maximum zoom;
			if ((largerRatio * _currentZoom) > _maximumZoom)
				largerRatio = _maximumZoom / _currentZoom;

			if (dragWindowSave.Width * largerRatio > Width)
				largerRatio = (float)Width / (float)dragWindowSave.Width;

			if (dragWindowSave.Height * largerRatio > Height)
				largerRatio = (float)Height / (float)dragWindowSave.Height;

			yAdjust = Convert.ToInt32(
				((float)Height - (float)dragWindowSave.Height * largerRatio) / 2F);
			xAdjust = Convert.ToInt32(
				((float)Width - (float)dragWindowSave.Width * largerRatio) / 2F);
			
			CurrentZoom *= largerRatio;

			// Get the new offsets
			xOffset = Math.Max(
				Convert.ToInt32(((float)xOffset + (float)dragWindowSave.X) * largerRatio) - xAdjust, 0);
			yOffset = Math.Max(
				Convert.ToInt32(((float)yOffset + (float)dragWindowSave.Y) * largerRatio) - yAdjust, 0);

			hScrollBar.Value = Convert.ToInt32(
				((float)xOffset / (float)resized.Width) * (float)hScrollBar.Maximum);
			vScrollBar.Value = Convert.ToInt32(
				((float)yOffset / (float)resized.Height) * (float)vScrollBar.Maximum);

			Invalidate();
		}


		#endregion

		#region Scrolling Methods

		private void AdjustScrollBars()
		{
			if (resized != null && _allowScrolling)
			{
				hScrollBar.Visible = (resized.Width > Width - vScrollWidth);
				vScrollBar.Visible = (resized.Height > Height - hScrollHeight);

				// Set the Large changes
				vScrollBar.LargeChange = Math.Max(Height - hScrollHeight, 1);
				hScrollBar.LargeChange = Math.Max(Width - vScrollWidth, 1);

				// If the horizontal scrollbar is needed turn it on
				if (resized.Width > Width)
					hScrollBar.Maximum = 
						resized.Width - (Width - vScrollWidth) + hScrollBar.LargeChange + 1;
				else
				{
					hScrollBar.Value = 0;
					xOffset = 0;
				}

				// If the vertical scrollbar is needed turn it on
				if (resized.Height > Height)
					vScrollBar.Maximum = 
						resized.Height - (Height - hScrollHeight) + vScrollBar.LargeChange + 1;
				else
				{
					vScrollBar.Value = 0;
					yOffset = 0;
				}
			}
			else
			{
				hScrollBar.Visible = false;
				vScrollBar.Visible = false;
			}

			SetScrollBarPositions();	
		}


		private void SetScrollBarPositions()
		{
			// Update the horizontal scrollbar
			hScrollBar.SetBounds(
				0, Height - hScrollBar.Height,
				Width - vScrollWidth, hScrollBar.Height);		

			// Update the vertical scrollbar
			vScrollBar.SetBounds(
				Width - vScrollBar.Width, 0,
				vScrollBar.Width, Height - hScrollHeight);
		}

		/// <summary>
		/// Scroll the form is a specified direction
		/// </summary>
		/// <param name="direction">Directio to scroll the form</param>
		/// <param name="smallChange">True if the change should be small, false if large</param>
		public void Scroll(ScrollDirection direction, bool smallChange)
		{
			ScrollBar sb = null;

			if (direction == ScrollDirection.Down || direction == ScrollDirection.Up)
				sb = vScrollBar;
			else
				sb = hScrollBar;

			int change = (smallChange) ? sb.SmallChange : sb.LargeChange;

			if (direction == ScrollDirection.Left || direction == ScrollDirection.Up)
			{
				if (sb.Value - change < sb.Minimum)
					sb.Value = sb.Minimum;
				else
					sb.Value -= change;
			}
			else
			{
				if (sb.Value + change > sb.Maximum - (sb.LargeChange - 1))
					sb.Value = sb.Maximum - (sb.LargeChange - 1);
				else
					sb.Value += change;
			}

			ScrollBar_Scroll(sb, new ScrollEventArgs(ScrollEventType.EndScroll, sb.Value));
		}

		protected override bool ProcessKeyMessage(ref Message m)
		{
			if (m.Msg == WM_KEYDOWN)
				ProcessKeyDown(ref m);

			return base.ProcessKeyMessage (ref m);
		}
		protected override void WndProc(ref Message m)
		{
			try
			{
				if (m.Msg == WM_MOUSEWHEEL)
					ProcessWheelMovement(ref m);

				base.WndProc (ref m);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void ProcessWheelMovement(ref Message m)
		{
			int wParam = (int)m.WParam;

			if (wParam > 0)
			{
				// The wheel is being scrolled up
				if ((wParam & MK_SHIFT) == MK_SHIFT)
					CurrentZoom += .10F;
				else if ((wParam & MK_CONTROL) == MK_CONTROL)
					Scroll(ScrollDirection.Left, true);
				else
					Scroll(ScrollDirection.Up, true);
			}
			else if (wParam < 0)
			{
				// The wheel is being scrolled down
				if ((wParam & MK_SHIFT) == MK_SHIFT)
					CurrentZoom -= .10F;
				else if ((wParam & MK_CONTROL) == MK_CONTROL)
					Scroll(ScrollDirection.Right, true);
				else
					Scroll(ScrollDirection.Down, true);
			}

			vScrollBar.Update();
		}
		private void ProcessKeyDown(ref Message m)
		{
			int wParam = (int)m.WParam;

			switch (wParam)
			{
				case VK_PRIOR :
					Scroll(ScrollDirection.Up, false);
					break;
				case VK_NEXT :
					Scroll(ScrollDirection.Down, false);
					break;
				case VK_END :
					Scroll(ScrollDirection.Right, false);
					break;
				case VK_HOME :
					Scroll(ScrollDirection.Left, false);
					break;
				case VK_LEFT :
					Scroll(ScrollDirection.Left, true);
					break;
				case VK_UP :
					Scroll(ScrollDirection.Up, true);
					break;
				case VK_RIGHT :
					Scroll(ScrollDirection.Right, true);
					break;
				case VK_DOWN :
					Scroll(ScrollDirection.Down, true);
					break;
			}
		}

		private void ScrollBar_Scroll(object sender, ScrollEventArgs e)
		{
			if (sender.Equals(hScrollBar))
				xOffset = e.NewValue;
			else
				yOffset = e.NewValue;

			Invalidate();
		}

		private void PromptMenuItem_Clicked(object sender, EventArgs e)
		{
			if (((MenuItem)sender).Text == "&Copy")
				Copy();
			else
				Zoom();
		}


		#endregion

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
			float fitHeightZoom = (float)(Height - 1) / (float)backup.Height;

			if (fitHeightZoom < _minimumZoom)
				fitHeightZoom = _minimumZoom;

			CurrentZoom = fitHeightZoom;
		}
		/// <summary>
		/// Will automatically shrink or enlarge the image to match the width of the control
		/// </summary>
		public void FitWidth()
		{
			float fitWidthZoom = (float)(Width - 1) / (float)backup.Width;

			if (fitWidthZoom < _minimumZoom)
				fitWidthZoom = _minimumZoom;

			CurrentZoom = fitWidthZoom;
		}

		/// <summary>
		/// Gets the cursor used by the picturebox to add an annotation
		/// </summary>
		public static Cursor AnnotationCursor
		{
			get
			{
				System.Reflection.Assembly assembly;
				assembly = System.Reflection.Assembly.GetAssembly(typeof(PictureBoxEx));

				return new Cursor(assembly.GetManifestResourceStream("Tpsc.Controls.Annotation.cur"));
			}
		}


		#region Internal Event Methods

		protected virtual void OnAllowScrollingChanged(object sender, EventArgs e)
		{
			AdjustScrollBars();

			Invalidate();
		}
		protected virtual void OnBorderStyleChanged(object sender, EventArgs e)
		{

		}
		protected virtual void OnCurrentZoomChanged(object sender, EventArgs e)
		{

		}
		protected virtual void OnDefaultZoomChanged(object sender, EventArgs e)
		{

		}
		protected virtual void OnDoubleClickRestoreChanged(object sender, EventArgs e)
		{

		}
		protected virtual void OnDragOptionsChanged(object sender, EventArgs e)
		{

		}
		protected virtual void OnDragWindowColorChanged(object sender, EventArgs e)
		{

		}
		protected virtual void OnDragWindowDashPatternChanged(object sender, EventArgs e)
		{

		}
		protected virtual void OnDragWindowMinimumChanged(object sender, EventArgs e)
		{

		}
		protected virtual void OnDrawModeChanged(object sender, EventArgs e)
		{

		}
		protected virtual void OnImageChanged(object sender, EventArgs e)
		{

		}
		protected virtual void OnImagePortionCopied(object sender, ImagePortionCopiedEventArgs e)
		{

		}
		protected virtual void OnMaximumZoomChanged(object sender, EventArgs e)
		{

		}
		protected virtual void OnMinimumZoomChanged(object sender, EventArgs e)
		{

		}

		protected virtual void OnScrollPositionChanged(object sender, EventArgs e)
		{

		}

		#endregion

		public class AnnotationCollection : CollectionBase
		{
			public AnnotationCollection()
			{
				AnnotationsChanged += new EventHandler(OnAnnotationCollection_AnnotationsChanged);
			}
			public AnnotationCollection(PictureBoxEx owner)
			{
				this.owner = owner;

				AnnotationsChanged += new EventHandler(OnAnnotationCollection_AnnotationsChanged);
			}
			public AnnotationCollection(Annotation[] annotations)
			{
				AnnotationsChanged += new EventHandler(OnAnnotationCollection_AnnotationsChanged);

				foreach (Annotation annote in annotations)
					Add(annote, false);
			}
			public AnnotationCollection(PictureBoxEx owner, Annotation[] annotations) : this(annotations)
			{
				this.owner = owner;
			}

			
			private PictureBoxEx owner = null;

			public event EventHandler AnnotationsChanged;

			public int Add(Annotation annotation, bool adjustLocation)
			{
				bool found = Remove(annotation);

				annotation.Changed += new EventHandler(annotation_Changed);

				if (adjustLocation && owner != null)
				{
					float ratio = 1F / owner._currentZoom;

					int adjustedX = Convert.ToInt32((annotation.X + owner.xOffset) * ratio);
					int adjustedY = Convert.ToInt32((annotation.Y + owner.yOffset) * ratio);

					annotation.Location = new Point(adjustedX, adjustedY);
				}

				int index = InnerList.Add(annotation);

				if (!found)
					AnnotationsChanged(this, new EventArgs());

				return index;
			}
			public int Add(string text, Font font, Color color, Point location, bool adjustLocation)
			{
				Annotation newAnnote = new Annotation(text, font, color, location);

				newAnnote.Changed += new EventHandler(annotation_Changed);

				return Add(newAnnote, adjustLocation);
			}
			public void AddRange(Annotation[] annotations)
			{
				foreach (Annotation annotation in annotations)
				{
					bool found = Remove(annotation);

					InnerList.Add(annotation);

					if (!found)
						annotation.Changed += new EventHandler(annotation_Changed);
				}

				AnnotationsChanged(this, new EventArgs());
			}
			public bool Remove(Annotation annotation)
			{
				foreach (Annotation annote in InnerList)
					if (annote.Guid == annotation.Guid)
					{
						annotation.Changed -= new EventHandler(annotation_Changed);
						
						InnerList.Remove(annote);

						AnnotationsChanged(this, new EventArgs());
						
						return true;
					}

				return false;
			}


			protected override void OnClear()
			{
				if (InnerList.Count > 0)
				{
					InnerList.Clear();
					AnnotationsChanged(this, new EventArgs());
				}
			}


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


			public Annotation[] ToArray()
			{
				return (Annotation[])InnerList.ToArray(typeof(Annotation));
			}


			public void Open(string fileName)
			{
				Clear();

				if ( System.IO.File.Exists(fileName) )
				{
					Type type = typeof(Annotation[]);

					using ( FileStream fs = new FileStream(fileName, FileMode.Open) )
					{
						System.Xml.Serialization.XmlSerializer x;
						x = new System.Xml.Serialization.XmlSerializer(type);
						AddRange((Tpsc.Controls.Annotation[])x.Deserialize(fs));
					}
				}

				AnnotationsChanged(this, new EventArgs());
			}

			public void Save(string fileName)
			{
				using ( StreamWriter sw = new StreamWriter(fileName) )
				{
					Type type = typeof(Annotation[]);

					System.Xml.Serialization.XmlSerializer x;
					x = new System.Xml.Serialization.XmlSerializer(type);
					x.Serialize(sw, ToArray());
				}
			}


			private void annotation_Changed(object sender, EventArgs e)
			{
				if (InnerList.Contains(sender))
					AnnotationsChanged(sender, e);
			}


			protected virtual void OnAnnotationCollection_AnnotationsChanged(object sender, EventArgs e)
			{

			}
		}
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
		public ImagePortionCopiedEventArgs(System.Drawing.Bitmap image, Rectangle rectangle)
		{
			_image = image;
			_rectangle = rectangle;
		}


		/// <summary>
		/// The copied portion of the image.
		/// </summary>
		public Bitmap Image
		{
			get { return _image; }
			set { _image = value; }
		}
		private Bitmap _image = null;

		/// <summary>
		/// The drag window info used to copy this portion of the image.
		/// </summary>
		public Rectangle Rectangle
		{
			get { return _rectangle; }
			set { _rectangle = value; }
		}
		private Rectangle _rectangle;
	}
}
