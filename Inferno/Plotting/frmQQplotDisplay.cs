using System;
using DAnTE.Purgatorio;

namespace DAnTE.Inferno
{
    public partial class frmQQplotDisplay : frmPlotDisplay
    {
        private readonly clsQQPar mclsQQPar;
        private frmDAnTE mfrmDante;

        public frmQQplotDisplay(clsQQPar clsQQPar)
        {
            mclsQQPar = clsQQPar;
            InitializeComponent();
            mnuItemPara.Click += mnuItemPara_Click;
            parametersToolStripMenuItem.Click += mnuItemPara_Click;
            mtBtnParam.Click += mnuItemPara_Click;
        }

        private void mnuItemPara_Click(object sender, EventArgs e)
        {
            mfrmDante.PlotQQ(mclsQQPar);
        }

        public clsQQPar clsQQPar
        {
            get { return mclsQQPar; }
        }

        public frmDAnTE DAnTEinstance
        {
            set { mfrmDante = value; }
        }
    }
}