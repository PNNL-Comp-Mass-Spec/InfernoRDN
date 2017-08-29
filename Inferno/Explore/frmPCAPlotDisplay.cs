using System;
using DAnTE.Purgatorio;

namespace DAnTE.Inferno
{
    public partial class frmPCAPlotDisplay : frmPlotDisplay
    {
        private readonly clsPCAplotPar mclsPCAPlotPar;
        private frmDAnTE mfrmDante;

        public frmPCAPlotDisplay(clsPCAplotPar mclsPCA)
        {
            mclsPCAPlotPar = mclsPCA;
            InitializeComponent();
            mnuItemPara.Click += mnuItemPara_Click;
            parametersToolStripMenuItem.Click += mnuItemPara_Click;
            mtBtnParam.Click += mnuItemPara_Click;
        }

        private void mnuItemPara_Click(object sender, EventArgs e)
        {
            mfrmDante.PlotPCA(mclsPCAPlotPar);
        }

        public clsPCAplotPar clsPCAPlotPar => mclsPCAPlotPar;

        public frmDAnTE DAnTEinstance
        {
            set => mfrmDante = value;
        }
    }
}