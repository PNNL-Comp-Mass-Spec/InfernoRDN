using System;
using DAnTE.Purgatorio;


namespace DAnTE.Inferno
{
    public partial class frmHeatmapDisplay : DAnTE.Inferno.frmPlotDisplay
    {
        private clsHeatmapPar mclsHmapPar = new clsHeatmapPar();
        private frmDAnTE mfrmDante;

        public frmHeatmapDisplay(clsHeatmapPar mclsHmap)
        {
            InitializeComponent();
            mclsHmapPar = mclsHmap;
            this.mnuItemPara.Click += new EventHandler(mnuItemPara_Click);
            this.parametersToolStripMenuItem.Click += new EventHandler(mnuItemPara_Click);
            this.mtBtnParam.Click += new System.EventHandler(this.mnuItemPara_Click);
        }
                
        void mnuItemPara_Click(object sender, EventArgs e)
        {
            mfrmDante.PlotHeatmap(mclsHmapPar);
        }

        public clsHeatmapPar clsHmapPar
        {
            get
            {
                return mclsHmapPar;
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

