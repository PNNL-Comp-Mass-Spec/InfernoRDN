using System;
using DAnTE.Purgatorio;

namespace DAnTE.Inferno
{
    public partial class frmVennDisplay : frmPlotDisplay
    {
        private readonly clsVennPar mclsVennPar;
        private frmDAnTE mfrmDante;

        public frmVennDisplay(clsVennPar clsVennPar)
        {
            mclsVennPar = clsVennPar;
            InitializeComponent();
            mnuItemPara.Click += mnuItemPara_Click;
            parametersToolStripMenuItem.Click += mnuItemPara_Click;
            mtBtnParam.Click += mnuItemPara_Click;
        }

        private void mnuItemPara_Click(object sender, EventArgs e)
        {
            mfrmDante.PlotVenn(mclsVennPar);
        }

        public clsVennPar clsVennPar => mclsVennPar;

        public frmDAnTE DAnTEinstance
        {
            set => mfrmDante = value;
        }
    }
}