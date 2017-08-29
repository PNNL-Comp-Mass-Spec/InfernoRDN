using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;


namespace DAnTE.ExtraControls
{
    /// <summary>
    /// The ZzzzRangeBar class describes a slide control with two buttons.
    /// A number range is assigned to the control and with the two slide
    /// buttons you can select an interval inside the range. This control can
    /// p.e. used for threshold setting in an image processing tool.
    /// If you push with left mouse button on a slide button it will marked and
    /// while mouse button is pressed you can move the slider left and right.
    /// Otherwise you can use the keys + and - to manipulate the slider position.
    /// The control will throw two events. While left mouse button is pressed and the 
    /// position of one slider has changed the event OnRangeChanging will generate and
    /// if you release mouse button, the event OnRangeChanged signals program that
    /// a new range was selected.
    /// </summary>
    /// <remarks>
    /// Class written by Detlef Neunherz 
    /// and obtained from http://www.codeproject.com/Articles/2275/C-RangeBar-control
    /// </remarks>
    public sealed class ZzzzRangeBar : UserControl
    {
        // delegate to handle range changed
        public delegate void RangeChangedEventHandler(object sender, EventArgs e);

        // delegate to handle range is changing
        public delegate void RangeChangingEventHandler(object sender, EventArgs e);


        /// <summary> 
        /// designer variable
        /// </summary>
        private readonly System.ComponentModel.Container components = null;

        public ZzzzRangeBar()
        {
            // Required by the Windows Form Designer
            InitializeComponent();
        }

        /// <summary> 
        /// Die verwendeten Ressourcen bereinigen.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            // 
            // ZzzzRangeBar
            // 
            Name = "ZzzzRangeBar";
            Size = new System.Drawing.Size(344, 64);
            KeyPress += new System.Windows.Forms.KeyPressEventHandler(OnKeyPress);
            Resize += new System.EventHandler(OnResize);
            Load += new System.EventHandler(OnLoad);
            SizeChanged += new System.EventHandler(OnSizeChanged);
            MouseUp += new System.Windows.Forms.MouseEventHandler(OnMouseUp);
            Paint += new System.Windows.Forms.PaintEventHandler(OnPaint);
            Leave += new System.EventHandler(OnLeave);
            MouseMove += new System.Windows.Forms.MouseEventHandler(OnMouseMove);
            MouseDown += new System.Windows.Forms.MouseEventHandler(OnMouseDown);
        }

        #endregion

        public enum ActiveMarkType
        {
            none,
            left,
            right
        };

        public enum RangeBarOrientation
        {
            horizontal,
            vertical
        };

        public enum TopBottomOrientation
        {
            top,
            bottom,
            both
        };

        private Color colorInner = Color.LightGreen;
        private readonly Color colorRange = Color.FromKnownColor(KnownColor.Control);
        private readonly Color colorShadowLight = Color.FromKnownColor(KnownColor.ControlLightLight);
        private readonly Color colorShadowDark = Color.FromKnownColor(KnownColor.ControlDarkDark);
        private int sizeShadow = 1;
        private double Minimum = 0;
        private double Maximum = 10;
        private double rangeMin = 3;
        private double rangeMax = 5;
        private ActiveMarkType ActiveMark = ActiveMarkType.none;


        private RangeBarOrientation orientationBar = RangeBarOrientation.horizontal; // orientation of range bar
        private TopBottomOrientation orientationScale = TopBottomOrientation.bottom;
        private int BarHeight = 8; // Height of Bar
        private int MarkWidth = 8; // Width of mark knobs
        private int MarkHeight = 24; // total height of mark knobs
        private int TickHeight = 6; // Height of axis tick
        private int numAxisDivision = 10;

        private int PosL, PosR; // Pixel-Position of mark buttons
        private int XPosMin, XPosMax;

        private readonly Point[] LMarkPnt = new Point[5];
        private readonly Point[] RMarkPnt = new Point[5];

        private bool MoveLMark;
        private bool MoveRMark;

        //------------------------------------
        // Properties
        //------------------------------------

        /// <summary>
        /// set or get tick height
        /// </summary>
        public int HeightOfTick
        {
            set
            {
                TickHeight = Math.Min(Math.Max(1, value), BarHeight);
                Invalidate();
                Update();
            }
            get => TickHeight;
        }

        /// <summary>
        /// set or get mark knob height
        /// </summary>
        public int HeightOfMark
        {
            set
            {
                MarkHeight = Math.Max(BarHeight + 2, value);
                Invalidate();
                Update();
            }
            get => MarkHeight;
        }


        /// <summary>
        /// set/get height of mark
        /// </summary>
        public int HeightOfBar
        {
            set
            {
                BarHeight = Math.Min(value, MarkHeight - 2);
                Invalidate();
                Update();
            }
            get => BarHeight;
        }

        /// <summary>
        /// set or get range bar orientation
        /// </summary>
        public RangeBarOrientation Orientation
        {
            set
            {
                orientationBar = value;
                Invalidate();
                Update();
            }
            get => orientationBar;
        }

        /// <summary>
        /// set or get scale orientation
        /// </summary>
        public TopBottomOrientation ScaleOrientation
        {
            set
            {
                orientationScale = value;
                Invalidate();
                Update();
            }
            get => orientationScale;
        }

        /// <summary>
        ///  set or get right side of range
        /// </summary>
        public int RangeMaximum
        {
            set
            {
                rangeMax = value;
                if (rangeMax < Minimum)
                    rangeMax = Minimum;
                else if (rangeMax > Maximum)
                    rangeMax = Maximum;
                if (rangeMax < rangeMin)
                    rangeMax = rangeMin;
                Range2Pos();
                Invalidate(true);
            }
            get => (int)rangeMax;
        }


        /// <summary>
        /// set or get left side of range
        /// </summary>
        public int RangeMinimum
        {
            set
            {
                rangeMin = value;
                if (rangeMin < Minimum)
                    rangeMin = Minimum;
                else if (rangeMin > Maximum)
                    rangeMin = Maximum;
                if (rangeMin > rangeMax)
                    rangeMin = rangeMax;
                Range2Pos();
                Invalidate(true);
            }
            get => (int)rangeMin;
        }


        /// <summary>
        /// set or get right side of total range
        /// </summary>
        public int TotalMaximum
        {
            set
            {
                Maximum = value;
                if (rangeMax > Maximum)
                    rangeMax = Maximum;
                Range2Pos();
                Invalidate(true);
            }
            get => (int)Maximum;
        }


        /// <summary>
        /// set or get left side of total range
        /// </summary>
        public int TotalMinimum
        {
            set
            {
                Minimum = value;
                if (rangeMin < Minimum)
                    rangeMin = Minimum;
                Range2Pos();
                Invalidate(true);
            }
            get => (int)Minimum;
        }


        /// <summary>
        /// set or get number of divisions
        /// </summary>
        public int DivisionNum
        {
            set
            {
                numAxisDivision = value;
                Refresh();
            }
            get => numAxisDivision;
        }


        /// <summary>
        /// set or get color of inner range
        /// </summary>
        public Color InnerColor
        {
            set
            {
                colorInner = value;
                Refresh();
            }
            get => colorInner;
        }


        /// <summary>
        /// set selected range
        /// </summary>
        /// <param name="left">left side of range</param>
        /// <param name="right">right side of range</param>
        public void SelectRange(int left, int right)
        {
            RangeMinimum = left;
            RangeMaximum = right;
            Range2Pos();
            Invalidate(true);
        }


        /// <summary>
        /// set range limits
        /// </summary>
        /// <param name="left">left side of range limit</param>
        /// <param name="right">right side of range limit</param>
        public void SetRangeLimit(double left, double right)
        {
            Minimum = left;
            Maximum = right;
            Range2Pos();
            Invalidate(true);
        }


        // paint event reaction
        private void OnPaint(object sender, PaintEventArgs e)
        {
            var h = Height;
            var w = Width;
            int baryoff, markyoff, tickyoff1, tickyoff2;
            double dtick;
            int tickpos;
            var penShadowLight = new Pen(colorShadowLight);
            var penShadowDark = new Pen(colorShadowDark);
            var brushShadowLight = new SolidBrush(colorShadowLight);
            var brushShadowDark = new SolidBrush(colorShadowDark);
            SolidBrush brushInner;
            var brushRange = new SolidBrush(colorRange);

            if (Enabled)
                brushInner = new SolidBrush(colorInner);
            else
                brushInner = new SolidBrush(Color.FromKnownColor(KnownColor.InactiveCaption));

            // range
            XPosMin = MarkWidth + 1;
            if (orientationBar == RangeBarOrientation.horizontal)
                XPosMax = w - MarkWidth - 1;
            else
                XPosMax = h - MarkWidth - 1;

            // range check
            if (PosL < XPosMin) PosL = XPosMin;
            if (PosL > XPosMax) PosL = XPosMax;
            if (PosR > XPosMax) PosR = XPosMax;
            if (PosR < XPosMin) PosR = XPosMin;

            Range2Pos();


            if (orientationBar == RangeBarOrientation.horizontal)
            {
                baryoff = (h - BarHeight) / 2;
                markyoff = baryoff + (BarHeight - MarkHeight) / 2 - 1;

                // total range bar frame			
                e.Graphics.FillRectangle(brushShadowDark, 0, baryoff, w - 1, sizeShadow); // top
                e.Graphics.FillRectangle(brushShadowDark, 0, baryoff, sizeShadow, BarHeight - 1); // left
                e.Graphics.FillRectangle(brushShadowLight, 0, baryoff + BarHeight - 1 - sizeShadow, w - 1, sizeShadow);
                    // bottom
                e.Graphics.FillRectangle(brushShadowLight, w - 1 - sizeShadow, baryoff, sizeShadow, BarHeight - 1);
                    // right


                // inner region
                e.Graphics.FillRectangle(brushInner, PosL, baryoff + sizeShadow, PosR - PosL,
                                         BarHeight - 1 - 2 * sizeShadow);

                // Skala
                if (orientationScale == TopBottomOrientation.bottom)
                {
                    tickyoff1 = tickyoff2 = baryoff + BarHeight + 2;
                }
                else if (orientationScale == TopBottomOrientation.top)
                {
                    tickyoff1 = tickyoff2 = baryoff - TickHeight - 4;
                }
                else
                {
                    tickyoff1 = baryoff + BarHeight + 2;
                    tickyoff2 = baryoff - TickHeight - 4;
                }

                if (numAxisDivision > 1)
                {
                    dtick = (XPosMax - XPosMin) / (double)numAxisDivision;
                    for (var i = 0; i < numAxisDivision + 1; i++)
                    {
                        tickpos = (int)Math.Round(i * dtick);
                        if (orientationScale == TopBottomOrientation.bottom
                            || orientationScale == TopBottomOrientation.both)
                        {
                            e.Graphics.DrawLine(penShadowDark, MarkWidth + 1 + tickpos,
                                                tickyoff1,
                                                MarkWidth + 1 + tickpos,
                                                tickyoff1 + TickHeight);
                        }
                        if (orientationScale == TopBottomOrientation.top
                            || orientationScale == TopBottomOrientation.both)
                        {
                            e.Graphics.DrawLine(penShadowDark, MarkWidth + 1 + tickpos,
                                                tickyoff2,
                                                MarkWidth + 1 + tickpos,
                                                tickyoff2 + TickHeight);
                        }
                    }
                }

                // left mark knob				
                LMarkPnt[0].X = PosL - MarkWidth;
                LMarkPnt[0].Y = markyoff + MarkHeight / 3;
                LMarkPnt[1].X = PosL;
                LMarkPnt[1].Y = markyoff;
                LMarkPnt[2].X = PosL;
                LMarkPnt[2].Y = markyoff + MarkHeight;
                LMarkPnt[3].X = PosL - MarkWidth;
                LMarkPnt[3].Y = markyoff + 2 * MarkHeight / 3;
                LMarkPnt[4].X = PosL - MarkWidth;
                LMarkPnt[4].Y = markyoff;
                e.Graphics.FillPolygon(brushRange, LMarkPnt);
                e.Graphics.DrawLine(penShadowDark, LMarkPnt[3].X - 1, LMarkPnt[3].Y, LMarkPnt[1].X - 1, LMarkPnt[2].Y);
                    // lower left shadow
                e.Graphics.DrawLine(penShadowLight, LMarkPnt[0].X - 1, LMarkPnt[0].Y, LMarkPnt[0].X - 1, LMarkPnt[3].Y);
                    // left shadow				
                e.Graphics.DrawLine(penShadowLight, LMarkPnt[0].X - 1, LMarkPnt[0].Y, LMarkPnt[1].X - 1, LMarkPnt[1].Y);
                    // upper shadow
                if (PosL < PosR)
                    e.Graphics.DrawLine(penShadowDark, LMarkPnt[1].X, LMarkPnt[1].Y + 1, LMarkPnt[1].X, LMarkPnt[2].Y);
                        // right shadow
                if (ActiveMark == ActiveMarkType.left)
                {
                    e.Graphics.DrawLine(penShadowLight, PosL - MarkWidth / 2 - 1, markyoff + MarkHeight / 3,
                                        PosL - MarkWidth / 2 - 1, markyoff + 2 * MarkHeight / 3); // active mark
                    e.Graphics.DrawLine(penShadowDark, PosL - MarkWidth / 2, markyoff + MarkHeight / 3,
                                        PosL - MarkWidth / 2, markyoff + 2 * MarkHeight / 3); // active mark			
                }


                // right mark knob
                RMarkPnt[0].X = PosR + MarkWidth;
                RMarkPnt[0].Y = markyoff + MarkHeight / 3;
                RMarkPnt[1].X = PosR;
                RMarkPnt[1].Y = markyoff;
                RMarkPnt[2].X = PosR;
                RMarkPnt[2].Y = markyoff + MarkHeight;
                RMarkPnt[3].X = PosR + MarkWidth;
                RMarkPnt[3].Y = markyoff + 2 * MarkHeight / 3;
                RMarkPnt[4].X = PosR + MarkWidth;
                RMarkPnt[4].Y = markyoff;
                e.Graphics.FillPolygon(brushRange, RMarkPnt);
                if (PosL < PosR)
                    e.Graphics.DrawLine(penShadowLight, RMarkPnt[1].X - 1, RMarkPnt[1].Y + 1, RMarkPnt[2].X - 1,
                                        RMarkPnt[2].Y); // left shadow
                e.Graphics.DrawLine(penShadowDark, RMarkPnt[2].X, RMarkPnt[2].Y, RMarkPnt[3].X, RMarkPnt[3].Y);
                    // lower right shadow
                e.Graphics.DrawLine(penShadowDark, RMarkPnt[0].X, RMarkPnt[0].Y, RMarkPnt[1].X, RMarkPnt[1].Y);
                    // upper shadow
                e.Graphics.DrawLine(penShadowDark, RMarkPnt[0].X, RMarkPnt[0].Y + 1, RMarkPnt[3].X, RMarkPnt[3].Y);
                    // right shadow
                if (ActiveMark == ActiveMarkType.right)
                {
                    e.Graphics.DrawLine(penShadowLight, PosR + MarkWidth / 2 - 1, markyoff + MarkHeight / 3,
                                        PosR + MarkWidth / 2 - 1, markyoff + 2 * MarkHeight / 3); // active mark
                    e.Graphics.DrawLine(penShadowDark, PosR + MarkWidth / 2, markyoff + MarkHeight / 3,
                                        PosR + MarkWidth / 2, markyoff + 2 * MarkHeight / 3); // active mark				
                }

                if (MoveLMark)
                {
                    var fontMark = new Font("Arial", MarkWidth);
                    var brushMark = new SolidBrush(colorShadowDark);
                    var strformat = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Near
                    };
                    e.Graphics.DrawString(rangeMin.ToString(CultureInfo.InvariantCulture), fontMark, brushMark, PosL, tickyoff1 + TickHeight,
                                          strformat);
                }

                if (MoveRMark)
                {
                    var fontMark = new Font("Arial", MarkWidth);
                    var brushMark = new SolidBrush(colorShadowDark);
                    var strformat = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Near
                    };
                    e.Graphics.DrawString(rangeMax.ToString(CultureInfo.InvariantCulture), fontMark, brushMark, PosR, tickyoff1 + TickHeight,
                                          strformat);
                }
            }
            else // vertical bar
            {
                baryoff = (w + BarHeight) / 2;
                markyoff = baryoff - BarHeight / 2 - MarkHeight / 2;
                if (orientationScale == TopBottomOrientation.bottom)
                {
                    tickyoff1 = tickyoff2 = baryoff + 2;
                }
                else if (orientationScale == TopBottomOrientation.top)
                {
                    tickyoff1 = tickyoff2 = baryoff - BarHeight - 2 - TickHeight;
                }
                else
                {
                    tickyoff1 = baryoff + 2;
                    tickyoff2 = baryoff - BarHeight - 2 - TickHeight;
                }

                // total range bar frame			
                e.Graphics.FillRectangle(brushShadowDark, baryoff - BarHeight, 0, BarHeight, sizeShadow); // top
                e.Graphics.FillRectangle(brushShadowDark, baryoff - BarHeight, 0, sizeShadow, h - 1); // left				
                e.Graphics.FillRectangle(brushShadowLight, baryoff, 0, sizeShadow, h - 1); // right
                e.Graphics.FillRectangle(brushShadowLight, baryoff - BarHeight, h - sizeShadow, BarHeight, sizeShadow);
                    // bottom

                // inner region
                e.Graphics.FillRectangle(brushInner, baryoff - BarHeight + sizeShadow, PosL, BarHeight - 2 * sizeShadow,
                                         PosR - PosL);

                // Skala
                if (numAxisDivision > 1)
                {
                    dtick = (XPosMax - XPosMin) / (double)numAxisDivision;
                    for (var i = 0; i < numAxisDivision + 1; i++)
                    {
                        tickpos = (int)Math.Round(i * dtick);
                        if (orientationScale == TopBottomOrientation.bottom
                            || orientationScale == TopBottomOrientation.both)
                            e.Graphics.DrawLine(penShadowDark,
                                                tickyoff1,
                                                MarkWidth + 1 + tickpos,
                                                tickyoff1 + TickHeight,
                                                MarkWidth + 1 + tickpos
                                );
                        if (orientationScale == TopBottomOrientation.top
                            || orientationScale == TopBottomOrientation.both)
                            e.Graphics.DrawLine(penShadowDark,
                                                tickyoff2,
                                                MarkWidth + 1 + tickpos,
                                                tickyoff2 + TickHeight,
                                                MarkWidth + 1 + tickpos
                                );
                    }
                }

                // left(upper) mark knob				
                LMarkPnt[0].Y = PosL - MarkWidth;
                LMarkPnt[0].X = markyoff + MarkHeight / 3;
                LMarkPnt[1].Y = PosL;
                LMarkPnt[1].X = markyoff;
                LMarkPnt[2].Y = PosL;
                LMarkPnt[2].X = markyoff + MarkHeight;
                LMarkPnt[3].Y = PosL - MarkWidth;
                LMarkPnt[3].X = markyoff + 2 * MarkHeight / 3;
                LMarkPnt[4].Y = PosL - MarkWidth;
                LMarkPnt[4].X = markyoff;
                e.Graphics.FillPolygon(brushRange, LMarkPnt);
                e.Graphics.DrawLine(penShadowDark, LMarkPnt[3].X, LMarkPnt[3].Y, LMarkPnt[2].X, LMarkPnt[2].Y);
                    // right shadow
                e.Graphics.DrawLine(penShadowLight, LMarkPnt[0].X - 1, LMarkPnt[0].Y, LMarkPnt[3].X - 1, LMarkPnt[3].Y);
                    // top shadow				
                e.Graphics.DrawLine(penShadowLight, LMarkPnt[0].X - 1, LMarkPnt[0].Y, LMarkPnt[1].X - 1, LMarkPnt[1].Y);
                    // left shadow
                if (PosL < PosR)
                    e.Graphics.DrawLine(penShadowDark, LMarkPnt[1].X, LMarkPnt[1].Y, LMarkPnt[2].X, LMarkPnt[2].Y);
                        // lower shadow
                if (ActiveMark == ActiveMarkType.left)
                {
                    e.Graphics.DrawLine(penShadowLight, markyoff + MarkHeight / 3, PosL - MarkWidth / 2,
                                        markyoff + 2 * MarkHeight / 3, PosL - MarkWidth / 2); // active mark
                    e.Graphics.DrawLine(penShadowDark, markyoff + MarkHeight / 3, PosL - MarkWidth / 2 + 1,
                                        markyoff + 2 * MarkHeight / 3, PosL - MarkWidth / 2 + 1); // active mark			
                }

                // right mark knob
                RMarkPnt[0].Y = PosR + MarkWidth;
                RMarkPnt[0].X = markyoff + MarkHeight / 3;
                RMarkPnt[1].Y = PosR;
                RMarkPnt[1].X = markyoff;
                RMarkPnt[2].Y = PosR;
                RMarkPnt[2].X = markyoff + MarkHeight;
                RMarkPnt[3].Y = PosR + MarkWidth;
                RMarkPnt[3].X = markyoff + 2 * MarkHeight / 3;
                RMarkPnt[4].Y = PosR + MarkWidth;
                RMarkPnt[4].X = markyoff;
                e.Graphics.FillPolygon(brushRange, RMarkPnt);
                e.Graphics.DrawLine(penShadowDark, RMarkPnt[2].X, RMarkPnt[2].Y, RMarkPnt[3].X, RMarkPnt[3].Y);
                    // lower right shadow
                e.Graphics.DrawLine(penShadowDark, RMarkPnt[0].X, RMarkPnt[0].Y, RMarkPnt[1].X, RMarkPnt[1].Y);
                    // upper shadow
                e.Graphics.DrawLine(penShadowDark, RMarkPnt[0].X, RMarkPnt[0].Y, RMarkPnt[3].X, RMarkPnt[3].Y);
                    // right shadow
                if (PosL < PosR)
                    e.Graphics.DrawLine(penShadowLight, RMarkPnt[1].X, RMarkPnt[1].Y, RMarkPnt[2].X, RMarkPnt[2].Y);
                        // left shadow
                if (ActiveMark == ActiveMarkType.right)
                {
                    e.Graphics.DrawLine(penShadowLight, markyoff + MarkHeight / 3, PosR + MarkWidth / 2 - 1,
                                        markyoff + 2 * MarkHeight / 3, PosR + MarkWidth / 2 - 1); // active mark
                    e.Graphics.DrawLine(penShadowDark, markyoff + MarkHeight / 3, PosR + MarkWidth / 2,
                                        markyoff + 2 * MarkHeight / 3, PosR + MarkWidth / 2); // active mark				
                }

                if (MoveLMark)
                {
                    var fontMark = new Font("Arial", MarkWidth);
                    var brushMark = new SolidBrush(colorShadowDark);
                    var strformat = new StringFormat
                    {
                        Alignment = StringAlignment.Near,
                        LineAlignment = StringAlignment.Center
                    };
                    e.Graphics.DrawString(rangeMin.ToString(CultureInfo.InvariantCulture), fontMark, brushMark, tickyoff1 + TickHeight + 2, PosL,
                                          strformat);
                }

                if (MoveRMark)
                {
                    var fontMark = new Font("Arial", MarkWidth);
                    var brushMark = new SolidBrush(colorShadowDark);
                    var strformat = new StringFormat
                    {
                        Alignment = StringAlignment.Near,
                        LineAlignment = StringAlignment.Center
                    };
                    e.Graphics.DrawString(rangeMax.ToString(CultureInfo.InvariantCulture), fontMark, brushMark, tickyoff1 + TickHeight, PosR,
                                          strformat);
                }
            }
        }


        // mouse down event
        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (Enabled)
            {
                var LMarkRect = new Rectangle(
                    Math.Min(LMarkPnt[0].X, LMarkPnt[1].X), // X
                    Math.Min(LMarkPnt[0].Y, LMarkPnt[3].Y), // Y
                    Math.Abs(LMarkPnt[2].X - LMarkPnt[0].X), // width
                    Math.Max(Math.Abs(LMarkPnt[0].Y - LMarkPnt[3].Y), Math.Abs(LMarkPnt[0].Y - LMarkPnt[1].Y)));
                    // height
                var RMarkRect = new Rectangle(
                    Math.Min(RMarkPnt[0].X, RMarkPnt[2].X), // X
                    Math.Min(RMarkPnt[0].Y, RMarkPnt[1].Y), // Y
                    Math.Abs(RMarkPnt[0].X - RMarkPnt[2].X), // width
                    Math.Max(Math.Abs(RMarkPnt[2].Y - RMarkPnt[0].Y), Math.Abs(RMarkPnt[1].Y - RMarkPnt[0].Y)));
                    // height

                if (LMarkRect.Contains(e.X, e.Y))
                {
                    Capture = true;
                    MoveLMark = true;
                    ActiveMark = ActiveMarkType.left;
                    Invalidate(true);
                }

                if (RMarkRect.Contains(e.X, e.Y))
                {
                    Capture = true;
                    MoveRMark = true;
                    ActiveMark = ActiveMarkType.right;
                    Invalidate(true);
                }
            }
        }


        // mouse up event
        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            if (Enabled)
            {
                Capture = false;

                MoveLMark = false;
                MoveRMark = false;

                Invalidate();

                OnRangeChanged(EventArgs.Empty);
            }
        }


        // mouse move event
        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (Enabled)
            {
                var LMarkRect = new Rectangle(
                    Math.Min(LMarkPnt[0].X, LMarkPnt[1].X), // X
                    Math.Min(LMarkPnt[0].Y, LMarkPnt[3].Y), // Y
                    Math.Abs(LMarkPnt[2].X - LMarkPnt[0].X), // width
                    Math.Max(Math.Abs(LMarkPnt[0].Y - LMarkPnt[3].Y), Math.Abs(LMarkPnt[0].Y - LMarkPnt[1].Y)));
                    // height
                var RMarkRect = new Rectangle(
                    Math.Min(RMarkPnt[0].X, RMarkPnt[2].X), // X
                    Math.Min(RMarkPnt[0].Y, RMarkPnt[1].Y), // Y
                    Math.Abs(RMarkPnt[0].X - RMarkPnt[2].X), // width
                    Math.Max(Math.Abs(RMarkPnt[2].Y - RMarkPnt[0].Y), Math.Abs(RMarkPnt[1].Y - RMarkPnt[0].Y)));
                    // height

                if (LMarkRect.Contains(e.X, e.Y) || RMarkRect.Contains(e.X, e.Y))
                {
                    if (orientationBar == RangeBarOrientation.horizontal)
                        Cursor = Cursors.SizeWE;
                    else
                        Cursor = Cursors.SizeNS;
                }
                else Cursor = Cursors.Arrow;

                if (MoveLMark)
                {
                    if (orientationBar == RangeBarOrientation.horizontal)
                        Cursor = Cursors.SizeWE;
                    else
                        Cursor = Cursors.SizeNS;
                    if (orientationBar == RangeBarOrientation.horizontal)
                        PosL = e.X;
                    else
                        PosL = e.Y;
                    if (PosL < XPosMin)
                        PosL = XPosMin;
                    if (PosL > XPosMax)
                        PosL = XPosMax;
                    if (PosR < PosL)
                        PosR = PosL;
                    Pos2Range();
                    ActiveMark = ActiveMarkType.left;
                    Invalidate(true);

                    OnRangeChanging(EventArgs.Empty);
                }
                else if (MoveRMark)
                {
                    if (orientationBar == RangeBarOrientation.horizontal)
                        Cursor = Cursors.SizeWE;
                    else
                        Cursor = Cursors.SizeNS;
                    if (orientationBar == RangeBarOrientation.horizontal)
                        PosR = e.X;
                    else
                        PosR = e.Y;
                    if (PosR > XPosMax)
                        PosR = XPosMax;
                    if (PosR < XPosMin)
                        PosR = XPosMin;
                    if (PosL > PosR)
                        PosL = PosR;
                    Pos2Range();
                    ActiveMark = ActiveMarkType.right;
                    Invalidate(true);

                    OnRangeChanging(EventArgs.Empty);
                }
            }
        }


        /// <summary>
        ///  transform pixel position to range position
        /// </summary>
        private void Pos2Range()
        {
            int w;

            if (orientationBar == RangeBarOrientation.horizontal)
                w = Width;
            else
                w = Height;

            var posw = w - 2 * MarkWidth - 2;

            rangeMin = Minimum + (int)Math.Round((Maximum - Minimum) * (PosL - XPosMin) / posw);
            rangeMax = Minimum + (int)Math.Round((Maximum - Minimum) * (PosR - XPosMin) / posw);
        }


        /// <summary>
        ///  transform range position to pixel position
        /// </summary>
        private void Range2Pos()
        {
            int w;

            if (orientationBar == RangeBarOrientation.horizontal)
                w = Width;
            else
                w = Height;

            var posw = w - 2 * MarkWidth - 2;

            PosL = XPosMin + (int)Math.Round(posw * (rangeMin - Minimum) / (Maximum - Minimum));
            PosR = XPosMin + (int)Math.Round(posw * (rangeMax - Minimum) / (Maximum - Minimum));
        }


        /// <summary>
        /// method to handle resize event
        /// </summary>
        /// <param name="sender">object that sends event to resize</param>
        /// <param name="e">event parameter</param>
        private void OnResize(object sender, EventArgs e)
        {
            //Range2Pos();
            Invalidate(true);
        }


        /// <summary>
        /// method to handle key pressed event
        /// </summary>
        /// <param name="sender">object that sends key pressed event</param>
        /// <param name="e">event parameter</param>
        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            if (Enabled)
            {
                if (ActiveMark == ActiveMarkType.left)
                {
                    if (e.KeyChar == '+')
                    {
                        rangeMin++;
                        if (rangeMin > Maximum)
                            rangeMin = Maximum;
                        if (rangeMax < rangeMin)
                            rangeMax = rangeMin;
                        OnRangeChanged(EventArgs.Empty);
                    }
                    else if (e.KeyChar == '-')
                    {
                        rangeMin--;
                        if (rangeMin < Minimum)
                            rangeMin = Minimum;
                        OnRangeChanged(EventArgs.Empty);
                    }
                }
                else if (ActiveMark == ActiveMarkType.right)
                {
                    if (e.KeyChar == '+')
                    {
                        rangeMax++;
                        if (rangeMax > Maximum)
                            rangeMax = Maximum;
                        OnRangeChanged(EventArgs.Empty);
                    }
                    else if (e.KeyChar == '-')
                    {
                        rangeMax--;
                        if (rangeMax < Minimum)
                            rangeMax = Minimum;
                        if (rangeMax < rangeMin)
                            rangeMin = rangeMax;
                        OnRangeChanged(EventArgs.Empty);
                    }
                }
                Invalidate(true);
            }
        }


        private void OnLoad(object sender, EventArgs e)
        {
            // use double buffering
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
        }

        private void OnSizeChanged(object sender, EventArgs e)
        {
            Invalidate(true);
            Update();
        }

        private void OnLeave(object sender, EventArgs e)
        {
            ActiveMark = ActiveMarkType.none;
        }


        public event RangeChangedEventHandler RangeChanged; // event handler for range changed
        public event RangeChangedEventHandler RangeChanging; // event handler for range is changing

        public void OnRangeChanged(EventArgs e)
        {
            RangeChanged?.Invoke(this, e);
        }

        public void OnRangeChanging(EventArgs e)
        {
            RangeChanging?.Invoke(this, e);
        }
    }
}