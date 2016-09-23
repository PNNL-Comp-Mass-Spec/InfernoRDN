using System;
using DAnTE.Purgatorio;

namespace DAnTE.Inferno
{
    public partial class frmCorrDisplay : frmPlotDisplay
    {
        private readonly clsCorrelationPar mclsCorrPar;
        private frmDAnTE mfrmDante;

        public frmCorrDisplay(clsCorrelationPar clsCorrPar)
        {
            InitializeComponent();
            mclsCorrPar = clsCorrPar;
            mnuItemPara.Click += mnuItemPara_Click;
            parametersToolStripMenuItem.Click += mnuItemPara_Click;
            mtBtnParam.Click += mnuItemPara_Click;
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

