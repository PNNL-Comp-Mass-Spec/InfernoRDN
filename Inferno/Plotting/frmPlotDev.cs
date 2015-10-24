using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using DAnTE.ExtraControls;
using DAnTE.Properties;

namespace DAnTE.Inferno
{
    public partial class frmPlotDev : Form
    {
        //public delegate void ButtonClicked();
        //public event ButtonClicked meventSaveButtonClicked;

        public frmPlotDev()
        {
            InitializeComponent();
        }

        private void mbtnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
                
        private void mbtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        //private void mbtnSave_Click(object sender, System.EventArgs e)
        //{
        //    if (meventSaveButtonClicked != null)
        //        meventSaveButtonClicked();
        //}

        private void mbtnSave_Click(object sender, System.EventArgs e)
        {
            string strImgName = null;
            try
            {
                /* save the image in the required format. */

                SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                saveFileDialog1.Filter = "JPEG files (*.jpg)|*.jpg|PNG files (*.png)|*.png|TIFF files (*.tif)|*.tif|Bitmaps (*.bmp)|*.bmp";
                saveFileDialog1.FilterIndex = 1;
                saveFileDialog1.RestoreDirectory = true;
                saveFileDialog1.InitialDirectory = Settings.Default.WorkingFolder;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    strImgName = saveFileDialog1.FileName;
                    if (strImgName.ToLower().EndsWith("jpg"))
                        mucPicVwrRPlot.Image.Save(strImgName, ImageFormat.Jpeg);
                    if (strImgName.ToLower().EndsWith("png"))
                        mucPicVwrRPlot.Image.Save(strImgName, ImageFormat.Png);
                    if (strImgName.ToLower().EndsWith("tif"))
                        mucPicVwrRPlot.Image.Save(strImgName, ImageFormat.Tiff);
                    if (strImgName.ToLower().EndsWith("bmp"))
                        mucPicVwrRPlot.Image.Save(strImgName, ImageFormat.Bmp);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void mnuStretch_Click(object sender, EventArgs e)
        {
            this.mucPicVwrRPlot.ImageSizeMode = SizeMode.RatioStretch;
        }

        private void mnuScroll_Click(object sender, EventArgs e)
        {
            this.mucPicVwrRPlot.ImageSizeMode = SizeMode.Scrollable;
        }

        public Image Image
        {
            get 
            {
                return this.mucPicVwrRPlot.Image; 
            }
            set
            {
                this.mucPicVwrRPlot.Image = value;
                this.mucPicVwrRPlot.SetLayout();
                //this.ChangeSize();
            }
        }
        public SizeMode ImageSizeMode
        {
            get 
            { 
                return this.mucPicVwrRPlot.ImageSizeMode; 
            }
            set
            {
                this.mucPicVwrRPlot.ImageSizeMode = value;
                this.AutoScroll = (this.mucPicVwrRPlot.ImageSizeMode == SizeMode.Scrollable);
                this.mucPicVwrRPlot.SetLayout();
            }
        }
    }
}