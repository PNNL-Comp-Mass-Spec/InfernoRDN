using System;
using DAnTE.Purgatorio;

namespace DAnTE.Inferno
{
    public partial class frmVennDisplay : DAnTE.Inferno.frmPlotDisplay
    {
        private readonly clsVennPar mclsVennPar = new clsVennPar();
        private frmDAnTE mfrmDante;

        public frmVennDisplay(clsVennPar clsVennPar)
        {
            mclsVennPar = clsVennPar;
            InitializeComponent();
            this.mnuItemPara.Click += new System.EventHandler(this.mnuItemPara_Click);
            this.parametersToolStripMenuItem.Click += new System.EventHandler(this.mnuItemPara_Click);
            this.mtBtnParam.Click += new System.EventHandler(this.mnuItemPara_Click);
        }

        private void mnuItemPara_Click(object sender, EventArgs e)
        {
            mfrmDante.PlotVenn(mclsVennPar);
        }

        public clsVennPar clsVennPar
        {
            get
            {
                return mclsVennPar;
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

