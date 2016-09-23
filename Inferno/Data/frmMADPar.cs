using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DAnTE.Inferno
{
    public partial class frmMADPar : Form
    {
        readonly Purgatorio.clsMADPar mclsMADPar;

        public frmMADPar(Purgatorio.clsMADPar mclsMAD)
        {
            InitializeComponent();
            mclsMADPar = mclsMAD;
        }

        private void mbtnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void mbtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        #region Properties

        public Purgatorio.clsMADPar clsMADPar
        {
            get
            {
                mclsMADPar.mblMeanAdj = MeanAdj;
                mclsMADPar.mintFactorIndex = FactorIndex;
                mclsMADPar.FactorSelected = Factor;

                return mclsMADPar;
            }
        }

        public string DataSetName
        {
            set { mlblDataName.Text = value; }
        }

        public List<string> PopulateFactorComboBox
        {
            set
            {
                if (value != null)
                {
                    if (!(value[0].Equals("<All>")))
                        value.Insert(0, "<All>");
                    mcmbBoxFactors.DataSource = value;
                }
                else
                    mcmbBoxFactors.Items.Add("<All>");
            }
        }

        //public string Factor
        //{
        //    get { return mcmbBoxFactors.SelectedItem.ToString(); }
        //}

        public string Factor
        {
            get
            {
                if (mcmbBoxFactors.SelectedItem != null)
                {
                    if (mcmbBoxFactors.SelectedItem.ToString().Equals("<All>"))
                        return "AllData";
                    return mcmbBoxFactors.SelectedItem.ToString();
                }
                else
                    return "AllData";
            }
        }

        public int FactorIndex
        {
            get
            {
                int idx = 0;
                if (mcmbBoxFactors.SelectedItem != null)
                {
                    if (mcmbBoxFactors.SelectedItem.ToString().Equals("<All>"))
                        return -1;
                    idx = mcmbBoxFactors.SelectedIndex;
                    return idx;
                }
                else
                    return -1;
            }
        }

        public bool MeanAdj
        {
            get { return mchkBoxMeanAdj.Checked; }
        }

        #endregion
    }
}