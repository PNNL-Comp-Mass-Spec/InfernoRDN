using System;
using DAnTE.Purgatorio;

namespace DAnTE.Inferno.Plotting
{
    public partial class frmMAplotDisplay : frmPlotDisplay
    {
        private readonly clsMAplotsPar mclsMAplotPar;
        private frmDAnTE mfrmDante;

        public frmMAplotDisplay(clsMAplotsPar clsMApar)
        {
            mclsMAplotPar = clsMApar;
            InitializeComponent();
            mnuItemPara.Click += mnuItemPara_Click;
            parametersToolStripMenuItem.Click += mnuItemPara_Click;
            mtBtnParam.Click += mnuItemPara_Click;
        }

        private void mnuItemPara_Click(object sender, EventArgs e)
        {
            mfrmDante.PlotMA(mclsMAplotPar);
        }

        public clsMAplotsPar clsMAplotPar
        {
            get
            {
                return mclsMAplotPar;
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

