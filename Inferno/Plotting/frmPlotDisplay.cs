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
        //public delegate void MenuClicked(string plottype);
        //public event MenuClicked meventMenuClicked;
        private Image rPlot;
        private string mstrPlotType;
        private frmDAnTE mfrmDante;

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
                
        private void mnuSave_Click(object sender, System.EventArgs e)
        {
            string workingFolder = Settings.Default.WorkingFolder;
            string strImgName = null;
            try
            {
                /* save the image in the required format. */

                SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                saveFileDialog1.Filter = "PNG files (*.png)|*.png|JPEG files (*.jpg)|*.jpg|TIFF files (*.tif)|*.tif|" +
                    "Bitmaps (*.bmp)|*.bmp|WMF files (*.wmf)|*.wmf";
                saveFileDialog1.FilterIndex = 1;
                saveFileDialog1.RestoreDirectory = true;
                if (workingFolder != null)
                    saveFileDialog1.InitialDirectory = workingFolder;
                else
                    saveFileDialog1.InitialDirectory = 
                        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    this.mpictureBoxEx.RestoreSize();
                    strImgName = saveFileDialog1.FileName;
                    if (strImgName.ToLower().EndsWith("jpg"))
                        mpictureBoxEx.Image.Save(strImgName, ImageFormat.Jpeg);
                    if (strImgName.ToLower().EndsWith("png"))
                        mpictureBoxEx.Image.Save(strImgName, ImageFormat.Png);
                    if (strImgName.ToLower().EndsWith("tif"))
                        mpictureBoxEx.Image.Save(strImgName, ImageFormat.Tiff);
                    if (strImgName.ToLower().EndsWith("bmp"))
                        mpictureBoxEx.Image.Save(strImgName, ImageFormat.Bmp);
                    if (strImgName.ToLower().EndsWith("wmf"))
                        mpictureBoxEx.Image.Save(strImgName, ImageFormat.Wmf);
                    this.mpictureBoxEx.FitHeight();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }

        private void stretchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int x = this.ClientSize.Width;
            int y = this.ClientSize.Height;
            if (x > y)
                this.mpictureBoxEx.FitHeight();
            else
                this.mpictureBoxEx.FitWidth();
        }

        protected override void OnResize(EventArgs e)
        {
            this.Icon = DAnTE.Properties.Resources.dante;
            base.OnResize(e);
            int x = this.ClientSize.Width;
            int y = this.ClientSize.Height;
            if (x > y)
                this.mpictureBoxEx.FitHeight();
            else
                this.mpictureBoxEx.FitWidth();
        } // End OnResize()
                
        private void RestoreSize_event(object sender, EventArgs e)
        {
            mpictureBoxEx.DoRestore();
        }

        private void frmPlotDisplay_Load(object sender, EventArgs e)
        {
            tmrDante.Enabled = true;
            this.TopMost = true;
            this.Icon = DAnTE.Properties.Resources.dante;
        }

        private void frmPlotDisplay_Activated(object sender, EventArgs e)
        {
            if (IsMdiChild)
            {
                menuStrip2.Visible = false;
                mToolStripPlot.Visible = false;

                frmDAnTEmdi mp = (frmDAnTEmdi)Application.OpenForms["frmDAnTEmdi"];
                ToolStripManager.RevertMerge(mp.mtoolStripMDI); //toolstrip refers to parent toolstrip
                ToolStripManager.Merge(this.mToolStripPlot, mp.mtoolStripMDI);
            }
        }

        private void OnClosed_event(object sender, FormClosedEventArgs e)
        {
            ToolStripManager.RevertMerge("mtoolStripMDI");
        }

        private void mnuPrint_Click(object sender, EventArgs e)
        {
            try
            {
                PrintDocument printDoc = new PrintDocument();
                printDoc.PrintPage += new PrintPageEventHandler(printDoc_PrintPage);

                //PrintPreviewDialog dlgPP = new PrintPreviewDialog();
                //dlgPP.Document = printDoc;
                //dlgPP.ShowDialog();

                PrintDialog dlg = new PrintDialog();
                dlg.Document = printDoc;

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
                PrintDocument printDoc = new PrintDocument();
                printDoc.PrintPage += new PrintPageEventHandler(printDoc_PrintPage);

                PrintPreviewDialog dlgPP = new PrintPreviewDialog();
                dlgPP.Document = printDoc;
                
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
            Rectangle destRect = new Rectangle(50, 50, 700, 526);
            e.Graphics.DrawImage(rPlot, destRect);
            e.HasMorePages = false;
        }

        private void tmrDante_Tick(object sender, EventArgs e)
        {
            this.TopMost = false;
            tmrDante.Enabled = false;
        }

        private void addStampToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.mpictureBoxEx.RestoreSize();

            DateTime CurrTime = DateTime.Now;

            string stamp = "DAnTE: " + DateTime.Now.ToString("MM-dd-yyyy hh:mm tt");
            stamp = stamp + " (" + Settings.Default.DataFileName + ")";
                        
            // Create image.
            Image tmp = mpictureBoxEx.Image;
            // Create graphics object for alteration.
            Graphics g = Graphics.FromImage(tmp);

            // Create font and brush.
            Font wmFont = new Font("Trebuchet MS", 8);
            SolidBrush wmBrush = new SolidBrush(Color.Black);
            // Create point for upper-left corner of drawing.
            //PointF wmPoint = new PointF(10.0F, 10.0F);
            PointF wmPoint = new PointF(tmp.Width - stamp.Length*6,tmp.Height-16);
            // Draw string to image.
            g.DrawString(stamp, wmFont, wmBrush, wmPoint);
            //Load the new image to picturebox		
            this.mpictureBoxEx.Image = tmp;
            // Release graphics object.
            g.Dispose();
            this.mpictureBoxEx.FitHeight();
        }



        #region Properties
        public Image Image
        {
            get
            {
                return this.mpictureBoxEx.Image;
            }
            set
            {
                rPlot = value;
                this.mpictureBoxEx.Image = value;
                this.mpictureBoxEx.FitHeight();
                //this.mpictureBoxEx.FitWidth();
                //this.mpictureBoxEx.SetLayout();
                //this.ChangeSize();
            }
        }

        public string PlotName
        {
            set
            {
                mstrPlotType = value;
            }
        }

        public string Title
        {
            set
            {
                this.Text = value;
            }
            get
            {
                return this.Text;
            }
        }

        public frmDAnTE DAnTEinstance
        {
            set
            {
                mfrmDante = value;
            }
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
            this.Icon = DAnTE.Properties.Resources.dante;
        }

        
        
    }
}