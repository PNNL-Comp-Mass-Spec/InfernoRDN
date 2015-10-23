using System;
using DAnTE.Purgatorio;

namespace DAnTE.Inferno
{
    public partial class frmPCAPlotDisplay : DAnTE.Inferno.frmPlotDisplay
    {
        private clsPCAplotPar mclsPCAPlotPar = new clsPCAplotPar();
        private frmDAnTE mfrmDante;

        public frmPCAPlotDisplay(clsPCAplotPar mclsPCA)
        {
            mclsPCAPlotPar = (clsPCAplotPar)mclsPCA;
            InitializeComponent();
            this.mnuItemPara.Click += new System.EventHandler(this.mnuItemPara_Click);
            this.parametersToolStripMenuItem.Click += new System.EventHandler(this.mnuItemPara_Click);
            this.mtBtnParam.Click += new System.EventHandler(this.mnuItemPara_Click);
        }
        
        private void mnuItemPara_Click(object sender, EventArgs e)
        {
            mfrmDante.PlotPCA(mclsPCAPlotPar);
        }

        public clsPCAplotPar clsPCAPlotPar
        {
            get
            {
                return mclsPCAPlotPar;
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

