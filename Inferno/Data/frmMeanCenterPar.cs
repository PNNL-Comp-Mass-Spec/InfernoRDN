using System;
using System.Windows.Forms;

namespace DAnTE.Inferno
{
    public partial class frmMeanCenterPar : Form
    {
        readonly Purgatorio.clsCentralTendencyPar mclsCentrTendPar;

        public frmMeanCenterPar(Purgatorio.clsCentralTendencyPar mclsCTendPar)
        {
            InitializeComponent();
            mclsCentrTendPar = mclsCTendPar;
        }

        private void mbtnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void mbtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void frmMeanCenterPar_Load(object sender, EventArgs e)
        {
            mlblDesc.Text = "The Central Tendancy of the selected data will be adjusted in terms of Mean or Median." +
                            Environment.NewLine + Environment.NewLine +
                            "You can choose to center all values around zero or otherwise " +
                            "it will select the maximum Mean/Median value in the datasets as the new Mean/Meadian." +
                            Environment.NewLine + Environment.NewLine +
                            "Subtracting is suggested for log transformed data.";
        }

        #region Properties

        public Purgatorio.clsCentralTendencyPar clsCentrTendPar
        {
            get
            {
                mclsCentrTendPar.mblUseMeanTend = Mean;
                mclsCentrTendPar.mblCenterZero = CenterZero;
                mclsCentrTendPar.mstrmethod = Method;

                return mclsCentrTendPar;
            }
        }

        public bool Mean
        {
            get { return mrbtnMean.Checked; }
        }

        public bool CenterZero
        {
            get { return mchkboxCenterZ.Checked; }
        }

        public string Method
        {
            get
            {
                if (mrbtnSubtract.Checked)
                    return "MeanCenter.Sub";
                else
                    return "MeanCenter.Div";
            }
        }

        public string DataSetName
        {
            set { mlblDataName.Text = value; }
        }

        #endregion
    }
}