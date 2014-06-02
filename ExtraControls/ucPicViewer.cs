using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DAnTE.ExtraControls
{
    public enum SizeMode
    {
        Scrollable,
        RatioStretch
    }
    /// <summary>
    /// Summary description for Viewer.
    /// </summary>
    public partial class ucPicViewer : System.Windows.Forms.UserControl
    {
        private System.Windows.Forms.PictureBox pictureBox1;
        
        private SizeMode sizeMode;

        public ucPicViewer()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();
            this.ImageSizeMode = SizeMode.RatioStretch;
        }

        public Image Image
        {
            get { return this.pictureBox1.Image; }
            set
            {
                this.pictureBox1.Image = value;
                this.SetLayout();
                //this.ChangeSize();
            }
        }
        public SizeMode ImageSizeMode
        {
            get { return this.sizeMode; }
            set
            {
                this.sizeMode = value;
                this.AutoScroll = (this.sizeMode == SizeMode.Scrollable);
                this.SetLayout();
            }
        }

        private void RatioStretch()
        {
            float pRatio = (float)this.Width / this.Height;
            float imRatio = (float)this.pictureBox1.Image.Width / this.pictureBox1.Image.Height;

            if (this.Width >= this.pictureBox1.Image.Width && this.Height >= this.pictureBox1.Image.Height)
            {
                this.pictureBox1.Width = this.pictureBox1.Image.Width;
                this.pictureBox1.Height = this.pictureBox1.Image.Height;
            }
            else if (this.Width > this.pictureBox1.Image.Width && this.Height < this.pictureBox1.Image.Height)
            {
                this.pictureBox1.Height = this.Height;
                this.pictureBox1.Width = (int)(this.Height * imRatio);
            }
            else if (this.Width < this.pictureBox1.Image.Width && this.Height > this.pictureBox1.Image.Height)
            {
                this.pictureBox1.Width = this.Width;
                this.pictureBox1.Height = (int)(this.Width / imRatio);
            }
            else if (this.Width < this.pictureBox1.Image.Width && this.Height < this.pictureBox1.Image.Height)
            {
                if (this.Width >= this.Height)
                {
                    //width image
                    if (this.pictureBox1.Image.Width >= this.pictureBox1.Image.Height && imRatio >= pRatio)
                    {
                        this.pictureBox1.Width = this.Width;
                        this.pictureBox1.Height = (int)(this.Width / imRatio);
                    }
                    else
                    {
                        this.pictureBox1.Height = this.Height;
                        this.pictureBox1.Width = (int)(this.Height * imRatio);
                    }
                }
                else
                {
                    //width image
                    if (this.pictureBox1.Image.Width < this.pictureBox1.Image.Height && imRatio < pRatio)
                    {
                        this.pictureBox1.Height = this.Height;
                        this.pictureBox1.Width = (int)(this.Height * imRatio);
                    }
                    else // height image
                    {
                        this.pictureBox1.Width = this.Width;
                        this.pictureBox1.Height = (int)(this.Width / imRatio);
                    }
                }
            }
            this.CenterImage();
        }
        private void Scrollable()
        {
            this.pictureBox1.Width = this.pictureBox1.Image.Width;
            this.pictureBox1.Height = this.pictureBox1.Image.Height;
            this.CenterImage();
        }
        internal void SetLayout()
        {
            if (this.pictureBox1.Image == null)
                return;
            if (this.sizeMode == SizeMode.RatioStretch)
                this.RatioStretch();
            else
            {
                this.AutoScroll = false;
                this.Scrollable();
                this.AutoScroll = true;

            }
        }
        private void CenterImage()
        {
            int top = (int)((this.Height - this.pictureBox1.Height) / 2.0);
            int left = (int)((this.Width - this.pictureBox1.Width) / 2.0);
            if (top < 0)
                top = 0;
            if (left < 0)
                left = 0;
            this.pictureBox1.Top = top;
            this.pictureBox1.Left = left;
        }
        
        private void Viewer_Load(object sender, System.EventArgs e)
        {
            this.pictureBox1.Width = 0;
            this.pictureBox1.Height = 0;
            this.SetLayout();
        }

        private void Viewer_Resize(object sender, System.EventArgs e)
        {
            this.SetLayout();
        }
                
    }
}
