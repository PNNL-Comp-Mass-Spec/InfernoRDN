using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DAnTE.Purgatorio;

namespace DAnTE.Inferno
{
    public partial class frmQQplotDisplay : DAnTE.Inferno.frmPlotDisplay
    {
        private clsQQPar mclsQQPar = new clsQQPar();
        private frmDAnTE mfrmDante;

        public frmQQplotDisplay(clsQQPar clsQQPar)
        {
            mclsQQPar = clsQQPar;
            InitializeComponent();
            this.mnuItemPara.Click += new System.EventHandler(this.mnuItemPara_Click);
            this.parametersToolStripMenuItem.Click += new System.EventHandler(this.mnuItemPara_Click);
            this.mtBtnParam.Click += new System.EventHandler(this.mnuItemPara_Click);
        }

        private void mnuItemPara_Click(object sender, EventArgs e)
        {
            mfrmDante.PlotQQ(mclsQQPar);
        }

        public clsQQPar clsQQPar
        {
            get
            {
                return mclsQQPar;
            }
        }

        public new frmDAnTE DAnTEinstance
        {
            set
            {
                mfrmDante = value;
            }
        }
    }
}

