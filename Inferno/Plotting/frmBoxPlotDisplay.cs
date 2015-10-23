using System;
using DAnTE.Purgatorio;

namespace DAnTE.Inferno
{
    public partial class frmBoxPlotDisplay : DAnTE.Inferno.frmPlotDisplay
    {
        private clsBoxPlotPar mclsBoxPlotPar = new clsBoxPlotPar();
        private frmDAnTE mfrmDante;

        public frmBoxPlotDisplay(clsBoxPlotPar clsBPpar)
        {
            mclsBoxPlotPar = clsBPpar;
            InitializeComponent();
            this.mnuItemPara.Click += new System.EventHandler(this.mnuItemPara_Click);
            this.parametersToolStripMenuItem.Click += new System.EventHandler(this.mnuItemPara_Click);
            this.mtBtnParam.Click += new System.EventHandler(this.mnuItemPara_Click);
        }

        private void mnuItemPara_Click(object sender, EventArgs e)
        {
            mfrmDante.PlotBoxPlots(mclsBoxPlotPar);
        }

        public clsBoxPlotPar clsBoxPlotPar
        {
            get
            {
                return mclsBoxPlotPar;
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

