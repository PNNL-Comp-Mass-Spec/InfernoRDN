using System;
using DAnTE.Purgatorio;

namespace DAnTE.Inferno
{
    public partial class frmBoxPlotDisplay : frmPlotDisplay
    {
        private readonly clsBoxPlotPar mclsBoxPlotPar;
        private frmDAnTE mfrmDante;

        public frmBoxPlotDisplay(clsBoxPlotPar clsBPpar)
        {
            mclsBoxPlotPar = clsBPpar;
            InitializeComponent();
            mnuItemPara.Click += mnuItemPara_Click;
            parametersToolStripMenuItem.Click += mnuItemPara_Click;
            mtBtnParam.Click += mnuItemPara_Click;
        }

        private void mnuItemPara_Click(object sender, EventArgs e)
        {
            mfrmDante.PlotBoxPlots(mclsBoxPlotPar);
        }

        public clsBoxPlotPar clsBoxPlotPar
        {
            get { return mclsBoxPlotPar; }
        }

        public frmDAnTE DAnTEinstance
        {
            set { mfrmDante = value; }
        }
    }
}