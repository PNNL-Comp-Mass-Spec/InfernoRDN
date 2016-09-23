using System;
using DAnTE.Purgatorio;

namespace DAnTE.Inferno
{
    public partial class frmHistDisplay : frmPlotDisplay
    {
        private readonly clsHistogramPar mclsHistPar;
        private frmDAnTE mfrmDante;

        public frmHistDisplay(clsHistogramPar clsHistPar)
        {
            mclsHistPar = clsHistPar;
            InitializeComponent();
            mnuItemPara.Click += mnuItemPara_Click;
            parametersToolStripMenuItem.Click += mnuItemPara_Click;
            mtBtnParam.Click += mnuItemPara_Click;
        }

        private void mnuItemPara_Click(object sender, EventArgs e)
        {
            mfrmDante.PlotHistograms(mclsHistPar);
        }

        public clsHistogramPar clsHistPar
        {
            get { return mclsHistPar; }
        }

        public frmDAnTE DAnTEinstance
        {
            set { mfrmDante = value; }
        }
    }
}