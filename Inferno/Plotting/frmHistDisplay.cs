using System;
using DAnTE.Purgatorio;

namespace DAnTE.Inferno
{
    public partial class frmHistDisplay : DAnTE.Inferno.frmPlotDisplay
    {
        private clsHistogramPar mclsHistPar = new clsHistogramPar();
        private frmDAnTE mfrmDante;

        public frmHistDisplay(clsHistogramPar clsHistPar)
        {
            mclsHistPar = clsHistPar;
            InitializeComponent();
            this.mnuItemPara.Click += new System.EventHandler(this.mnuItemPara_Click);
            this.parametersToolStripMenuItem.Click += new System.EventHandler(this.mnuItemPara_Click);
            this.mtBtnParam.Click += new System.EventHandler(this.mnuItemPara_Click);
        }

        private void mnuItemPara_Click(object sender, EventArgs e)
        {
            mfrmDante.PlotHistograms(mclsHistPar);
        }

        public clsHistogramPar clsHistPar
        {
            get
            {
                return mclsHistPar;
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

