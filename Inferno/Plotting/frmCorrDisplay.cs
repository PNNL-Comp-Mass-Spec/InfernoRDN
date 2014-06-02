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
    public partial class frmCorrDisplay : DAnTE.Inferno.frmPlotDisplay
    {
        private clsCorrelationPar mclsCorrPar = new clsCorrelationPar();
        private frmDAnTE mfrmDante;

        public frmCorrDisplay(clsCorrelationPar clsCorrPar)
        {
            InitializeComponent();
            mclsCorrPar = clsCorrPar;
            this.mnuItemPara.Click += new System.EventHandler(this.mnuItemPara_Click);
            this.parametersToolStripMenuItem.Click += new System.EventHandler(this.mnuItemPara_Click);
            this.mtBtnParam.Click += new System.EventHandler(this.mnuItemPara_Click);
        }

        private void mnuItemPara_Click(object sender, EventArgs e)
        {
            mfrmDante.PlotCorrelation(mclsCorrPar);
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

