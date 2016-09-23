using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using DAnTE.Properties;

namespace DAnTE.Inferno
{
    public partial class frmPlotDisplay : Form
    {
        private Image rPlot;

        public frmPlotDisplay()
        {
            InitializeComponent();
        }

        private void mbtnOK_Click(object sender, EventArgs e)
        {
            //this.DialogResult = DialogResult.OK;
            tmrDante.Enabled = false;
            this.Close();
        }

        private void mbtnCanel_Click(object sender, EventArgs e)
        {
            //this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void mnuSave_Click(object sender, EventArgs e)
        {
            var workingFolder = Settings.Default.WorkingFolder;
            try
            {
                /* save the image in the required format. */

                var saveFileDialog1 = new SaveFileDialog
                {
                    Filter = "PNG files (*.png)|*.png|JPEG files (*.jpg)|*.jpg|TIFF files (*.tif)|*.tif|" +
                             "Bitmaps (*.bmp)|*.bmp|WMF files (*.wmf)|*.wmf",
                    FilterIndex = 1,
                    RestoreDirectory = true,
                    InitialDirectory = workingFolder ??
                                       Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                };


                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    mPictureBoxEx.RestoreSize();
                    var strImgName = saveFileDialog1.FileName;
                    if (strImgName.ToLower().EndsWith("jpg"))
                        mPictureBoxEx.Image.Save(strImgName, ImageFormat.Jpeg);
                    if (strImgName.ToLower().EndsWith("png"))
                        mPictureBoxEx.Image.Save(strImgName, ImageFormat.Png);
                    if (strImgName.ToLower().EndsWith("tif"))
                        mPictureBoxEx.Image.Save(strImgName, ImageFormat.Tiff);
                    if (strImgName.ToLower().EndsWith("bmp"))
                        mPictureBoxEx.Image.Save(strImgName, ImageFormat.Bmp);
                    if (strImgName.ToLower().EndsWith("wmf"))
                        mPictureBoxEx.Image.Save(strImgName, ImageFormat.Wmf);
                    mPictureBoxEx.FitHeight();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void stretchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var x = ClientSize.Width;
            var y = ClientSize.Height;
            if (x > y)
                mPictureBoxEx.FitHeight();
            else
                mPictureBoxEx.FitWidth();
        }

        protected override void OnResize(EventArgs e)
        {
            Icon = Resources.dante;
            base.OnResize(e);
            var x = ClientSize.Width;
            var y = ClientSize.Height;
            if (x > y)
                mPictureBoxEx.FitHeight();
            else
                mPictureBoxEx.FitWidth();
        } // End OnResize()

        private void RestoreSize_event(object sender, EventArgs e)
        {
            mPictureBoxEx.DoRestore();
        }

        private void frmPlotDisplay_Load(object sender, EventArgs e)
        {
            tmrDante.Enabled = true;
            TopMost = true;
            Icon = Resources.dante;
        }

        private void frmPlotDisplay_Activated(object sender, EventArgs e)
        {
            if (IsMdiChild)
            {
                menuStrip2.Visible = false;
                mToolStripPlot.Visible = false;

                var mp = (frmDAnTEmdi)Application.OpenForms["frmDAnTEmdi"];
                if (mp == null) return;

                ToolStripManager.RevertMerge(mp.mToolStripMDI); //toolstrip refers to parent toolstrip
                ToolStripManager.Merge(mToolStripPlot, mp.mToolStripMDI);
            }
        }

        private void OnClosed_event(object sender, FormClosedEventArgs e)
        {
            ToolStripManager.RevertMerge("mToolStripMDI");
        }

        private void mnuPrint_Click(object sender, EventArgs e)
        {
            try
            {
                var printDoc = new PrintDocument();
                printDoc.PrintPage += printDoc_PrintPage;

                //PrintPreviewDialog dlgPP = new PrintPreviewDialog();
                //dlgPP.Document = printDoc;
                //dlgPP.ShowDialog();

                var dlg = new PrintDialog {Document = printDoc};

                // If the result is OK then print the document.
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    printDoc.Print();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while printing", ex.ToString());
            }
        }

        private void mnuPrintPrvw_Click(object sender, EventArgs e)
        {
            try
            {
                var printDoc = new PrintDocument();
                printDoc.PrintPage += printDoc_PrintPage;

                var dlgPP = new PrintPreviewDialog {Document = printDoc};

                // If the result is OK then print the document.
                if (dlgPP.ShowDialog() == DialogResult.OK)
                {
                    printDoc.Print();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while printing previewing", ex.ToString());
            }
        }

        private void printDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            var destRect = new Rectangle(50, 50, 700, 526);
            e.Graphics.DrawImage(rPlot, destRect);
            e.HasMorePages = false;
        }

        private void tmrDante_Tick(object sender, EventArgs e)
        {
            TopMost = false;
            tmrDante.Enabled = false;
        }

        private void addStampToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mPictureBoxEx.RestoreSize();

            var stamp = "DAnTE: " + DateTime.Now.ToString("MM-dd-yyyy hh:mm tt");
            stamp = stamp + " (" + Settings.Default.DataFileName + ")";

            // Create image.
            var tmp = mPictureBoxEx.Image;
            // Create graphics object for alteration.
            var g = Graphics.FromImage(tmp);

            // Create font and brush.
            var wmFont = new Font("Trebuchet MS", 8);
            var wmBrush = new SolidBrush(Color.Black);
            // Create point for upper-left corner of drawing.
            //PointF wmPoint = new PointF(10.0F, 10.0F);
            var wmPoint = new PointF(tmp.Width - stamp.Length * 6, tmp.Height - 16);
            // Draw string to image.
            g.DrawString(stamp, wmFont, wmBrush, wmPoint);
            //Load the new image to picturebox		
            mPictureBoxEx.Image = tmp;
            // Release graphics object.
            g.Dispose();
            mPictureBoxEx.FitHeight();
        }

        #region Properties

        public Image Image
        {
            get { return mPictureBoxEx.Image; }
            set
            {
                rPlot = value;
                mPictureBoxEx.Image = value;
                mPictureBoxEx.FitHeight();
                //mPictureBoxEx.FitWidth();
                //mPictureBoxEx.SetLayout();
                //this.ChangeSize();
            }
        }

        public string PlotName { set; get; }

        public string Title
        {
            set { Text = value; }
            get { return Text; }
        }

        public bool EnableParameterMenu
        {
            set
            {
                mnuItemPara.Enabled = value;
                parametersToolStripMenuItem.Enabled = value;
            }
        }

        #endregion

        private void frmPlotDisplay_Resize(object sender, EventArgs e)
        {
            Icon = Resources.dante;
        }
    }
}