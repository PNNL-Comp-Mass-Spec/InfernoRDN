using System;
using DAnTE.Purgatorio;


namespace DAnTE.Inferno
{
    public partial class frmHeatmapDisplay : frmPlotDisplay
    {
        private readonly clsHeatmapPar mclsHmapPar;
        private frmDAnTE mfrmDante;

        public frmHeatmapDisplay(clsHeatmapPar mclsHmap)
        {
            InitializeComponent();
            mclsHmapPar = mclsHmap;
            mnuItemPara.Click += mnuItemPara_Click;
            parametersToolStripMenuItem.Click += mnuItemPara_Click;
            mtBtnParam.Click += mnuItemPara_Click;
        }

        void mnuItemPara_Click(object sender, EventArgs e)
        {
            mfrmDante.PlotHeatmap(mclsHmapPar);
        }

        public clsHeatmapPar clsHmapPar
        {
            get { return mclsHmapPar; }
        }

        public new frmDAnTE DAnTEinstance
        {
            set { mfrmDante = value; }
        }
    }
}