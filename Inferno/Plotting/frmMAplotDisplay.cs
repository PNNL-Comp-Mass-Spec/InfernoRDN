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
    public partial class frmMAplotDisplay : DAnTE.Inferno.frmPlotDisplay
    {
        private clsMAplotsPar mclsMAplotPar = new clsMAplotsPar();
        private frmDAnTE mfrmDante;

        public frmMAplotDisplay(clsMAplotsPar clsMApar)
        {
            mclsMAplotPar = clsMApar;
            InitializeComponent();
            this.mnuItemPara.Click += new System.EventHandler(this.mnuItemPara_Click);
            this.parametersToolStripMenuItem.Click += new System.EventHandler(this.mnuItemPara_Click);
            this.mtBtnParam.Click += new System.EventHandler(this.mnuItemPara_Click);
        }

        private void mnuItemPara_Click(object sender, EventArgs e)
        {
            mfrmDante.PlotMA(mclsMAplotPar);
        }

        public clsMAplotsPar clsMAplotPar
        {
            get
            {
                return mclsMAplotPar;
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

