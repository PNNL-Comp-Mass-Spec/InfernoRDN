using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace DAnTE.ExtraControls
{
    public partial class ucTransparentLabel : UserControl
    // A Label that can be translucent to the background
    {
        #region Local Variables

        public enum ShapeBorderStyles
        {
            ShapeBSNone,
            ShapeBSFixedSingle,
        };

        private ShapeBorderStyles _borderStyle = ShapeBorderStyles.ShapeBSNone;
        private System.Drawing.Color _backColor = Color.Black;
        private System.Drawing.Color _borderColor = Color.White;
        private int _radius = 20;
        private int _opacity = 125;
        private string _text = "ucTransparentLabel";
        
        //  Local Variables for text
        public enum TextAlignment
        {
            Left,
            Center,
            Right
        };

        public enum MoveType
        {
            None,
            RightToLeft,
            DownToUp,
            LeftToRight,
            UpToDown
        }

        protected TextAlignment _textAlign = TextAlignment.Center;
        protected MoveType _moving = MoveType.None;
        protected bool _isSelected = false;
        private System.Drawing.Color _dimmedColor = Color.LightGray;

        // Work Variables
        protected int pointX = 0;
        protected int pointY = 0;
        protected Rectangle iRect = new Rectangle();
        protected Rectangle txtRect = new Rectangle();


        #endregion

        #region Constructor
        
        public ucTransparentLabel()
        {
            InitializeComponent();
            base.BackColor = Color.Transparent;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.Opaque, false);
            UpdateStyles();
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            // 
            // timer1
            // 
            this.timer1.Enabled = false;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            this.timer1.Interval = 100;
        }

        #endregion

        #region Properties

        [DefaultValue(typeof(Color), "Black")]
        public new Color BackColor
        // Gets or sets the background color of the control.
        {
            get { return _backColor; }
            set { _backColor = value; Invalidate();}
        }

        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(ShapeBorderStyles.ShapeBSNone),
        Description("Style of border to be drawn around control")
        ]
        public ShapeBorderStyles ShapeBorderStyle
        {
            get { return _borderStyle; }
            set { _borderStyle = value; this.Invalidate(); }
        }

        [DefaultValue(typeof(Color), "White"), Category("Appearance"), Description("The border color of the control.")]
        /// Gets or sets the outer border color of the control.
        public Color BorderColor
        {
            get { return _borderColor; }
            set {_borderColor = value; Invalidate();}
        }

        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(125),
        Description("The alpha value used to blend the control's background. Valid values are 0 through 255.")
        ]
        public int Opacity 
        {
            get { return _opacity; }
            set { _opacity = value; this.Invalidate(); }
        }


        [
        Bindable(true),
        Category("Layout"),
        DefaultValue(20),
        Description("Radius of rounded borders")
        ]
        public int Radius
        {
            get { return _radius; }
            set { _radius = value; this.Invalidate(); }
        }

        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue("ucTransparentLabel"),
        Description("Text in the ucTransparentLabel")
        ]
        public string Caption
        {
            get { return _text; }
            set { _text = value; this.Invalidate(); }
        }

        public override Font Font
        {
            get { return base.Font; }
            set { base.Font = value; this.Invalidate(); }
        }

        public override Color ForeColor
        {
            get { return base.ForeColor; }
            set { base.ForeColor = value; this.Invalidate(); }
        }

        [
        Bindable(true),
        Category("Appearance"),
        Description("Dimmed Color"),
        ]
        public Color DimmedColor
        {
            get { return _dimmedColor; }
            set { _dimmedColor = value; this.Invalidate(); }
        }

        [
        Bindable(true),
        Category("Behavior"),
        Description("Text movement"),
        DefaultValue(MoveType.None)
        ]
        public MoveType Moving
        {
            get { return _moving; }
            set { _moving = value; this.Invalidate(); }
        }

        [
        Bindable(true),
        Category("Appearance"),
        Description("Text alignment (Left, Right or Center), only with Moving None"),
        DefaultValue(TextAlignment.Center)
        ]
        public TextAlignment TextAlign
        {
            get { return _textAlign; }
            set { _textAlign = value; this.Invalidate(); }
        }

        [
        Bindable(true),
        Category("Behavior"),
        Description("Active the text movement"),
        DefaultValue(false)
        ]
        public bool MovingActive
        {
            get { return this.timer1.Enabled; }
            set
            {
                if (value == false) _moving = MoveType.None;
                this.timer1.Enabled = value;
                this.Invalidate();
            }
        }

        #endregion

        #region Methods

        protected override void OnPaint(PaintEventArgs e)
        {
            SmoothingMode sm = e.Graphics.SmoothingMode;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            if (_borderStyle == ShapeBorderStyles.ShapeBSFixedSingle)
                DrawBorder(e.Graphics);
            DrawLabelBackground(e.Graphics);
            DrawText(e);
            e.Graphics.SmoothingMode = sm;
        }

        private void DrawBorder(Graphics g)
        {
            Rectangle rect = this.ClientRectangle;
            rect.Width--;
            rect.Height--;
            using (GraphicsPath bp = GetPath(rect, _radius))
            {
                using (Pen p = new Pen(_borderColor))
                {
                    g.DrawPath(p, bp);
                }
            }
        }

        private void DrawLabelBackground(Graphics g)   
        {
            Rectangle rect = this.ClientRectangle;
            iRect = rect;
            rect.X++;
            rect.Y++;
            rect.Width -= 2;
            rect.Height -= 2;
            using (GraphicsPath bb = GetPath(rect, _radius))
            {
                using (Brush br = new SolidBrush(Color.FromArgb(_opacity, _backColor)))
                {
                    g.FillPath(br, bb);
                }
            }
        }

        protected GraphicsPath GetPath(Rectangle rc, int r)
        //  Build the path with the round corners in the rectangle
        //  r is the radious of rounded corner.
        {
            int x = rc.X, y = rc.Y, w = rc.Width, h = rc.Height;
            r = r << 1;
            GraphicsPath path = new GraphicsPath();
            if (r > 0)
            //  If the radious of rounded corner is greater than one side then
            //  do the side rounded
            {
                if (r > h) { r = h; };                              //Rounded
                if (r > w) { r = w; };                              //Rounded
                path.AddArc(x, y, r, r, 180, 90);				    //Upper left corner
                path.AddArc(x + w - r, y, r, r, 270, 90);			//Upper right corner
                path.AddArc(x + w - r, y + h - r, r, r, 0, 90);		//Lower right corner
                path.AddArc(x, y + h - r, r, r, 90, 90);			//Lower left corner
                path.CloseFigure();
            }
            else
            //  If the radious of rounded corner is zero then the path is a rectangle
            {
                path.AddRectangle(rc);
            }

            return path;
        }


        protected void DrawText(PaintEventArgs pe)
        {
            //This is a workaround to get MeasureString to work properly
            //pe.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            SizeF sz = pe.Graphics.MeasureString(_text, base.Font);
            switch (_moving)
            {
                case MoveType.None:
                    NoMove();
                    break;
                case MoveType.RightToLeft:
                    MoveRightToLeft();
                    break;
                case MoveType.DownToUp:
                    MoveDownToUp();
                    break;
                case MoveType.LeftToRight:
                    MoveLeftToRight();
                    break;
                case MoveType.UpToDown:
                    MoveUpToDown();
                    break;
            }
            //Rectangle bounds for the text
            txtRect = new Rectangle(this.pointX, this.pointY,
                                    (int)sz.Width + 1, (int)sz.Height);

            //If the mouse is passing over the text it is selected and will be dimmed
            //otherwise nothing.
            Brush brText = new SolidBrush(base.ForeColor);
            Brush brTextDimmed = new SolidBrush(_dimmedColor);
            if (_isSelected)
                pe.Graphics.DrawString(_text,
                    base.Font,
                    brTextDimmed,
                    txtRect);
            else
                pe.Graphics.DrawString(_text,
                    base.Font,
                    brText,
                    txtRect);
        }

        protected void NoMove()
        {
            //Align text
            switch (_textAlign)
            {
                case TextAlignment.Left:
                    pointX = (int)this.iRect.X;
                    break;
                case TextAlignment.Center:
                    pointX = (this.iRect.Width - this.txtRect.Width) / 2;
                    break;
                case TextAlignment.Right:
                    pointX = (this.iRect.Width - this.txtRect.Width);
                    break;
            }
            pointY = (this.iRect.Height - this.txtRect.Height) / 2;
        }

        protected void MoveRightToLeft()
        {
            if (pointX < -this.txtRect.Width)
            { pointX = this.iRect.X + this.iRect.Width; }
            else
            { pointX -= 2; }
            pointY = (this.iRect.Height - this.txtRect.Height) / 2;
        }

        protected void MoveDownToUp()
        {
            pointX = (this.iRect.Width - this.txtRect.Width) / 2;
            if (pointY < -this.txtRect.Height)
            { pointY = (int)this.iRect.Y + this.iRect.Height; }
            else
            { pointY -= 2; }
        }

        protected void MoveLeftToRight()
        {
            if (pointX > this.iRect.X + this.iRect.Width)
            { pointX = this.iRect.X - this.txtRect.Width; }
            else
            { pointX += 2; }
            pointY = (this.iRect.Height - this.txtRect.Height) / 2;
        }

        protected void MoveUpToDown()
        {
            pointX = (this.iRect.Width - this.txtRect.Width) / 2;
            if (pointY > this.iRect.Y + this.iRect.Height)
            { pointY = (int)this.iRect.Y - this.iRect.Height; }
            else
            { pointY += 2; }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _isSelected = true;
            this.Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _isSelected = false;
            this.Invalidate();
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            this.Invalidate();
            this.Update();
        }
    }

        #endregion
}