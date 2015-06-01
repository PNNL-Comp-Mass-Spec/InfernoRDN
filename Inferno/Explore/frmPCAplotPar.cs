using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DAnTE.Tools;
using DAnTE.Purgatorio;

namespace DAnTE.Inferno
{
    public partial class frmPCAplotPar : Form
    {
        private readonly clsPCAplotPar mclsPCApar;
        private ArrayList marrDatasets = new ArrayList();
        private ArrayList marrFactorList = new ArrayList();

        public frmPCAplotPar(clsPCAplotPar mclsPCA)
        {
            mclsPCApar = mclsPCA;
            InitializeComponent();
        }


        private ArrayList PrincipalComponents()
        {
            ArrayList marrPCs = new ArrayList();
            marrPCs.Add("PC1");
            marrPCs.Add("PC2");
            marrPCs.Add("PC3");
            marrPCs.Add("PC4");
            marrPCs.Add("PC5");

            return marrPCs;
        }

        private void mrbtn3D_CheckedChanged(object sender, EventArgs e)
        {
            if (mrbtn3D.Checked)
            {
                mcmbBoxZ.Enabled = true;
                mchkBoxDropLines.Enabled = !mchkBoxBiPlot.Checked;
                mchkBoxPersp.Enabled = true;
            }
            else
            {
                mcmbBoxZ.Enabled = false;
                mchkBoxDropLines.Enabled = false;
                mchkBoxPersp.Enabled = false;
            }
        }

        private void mbtnOK_Click(object sender, EventArgs e)
        {
            if (mlstViewDataSets.CheckedIndices.Count == 0)
            {
                MessageBox.Show("No datasets selected.", "Select datasets");
                this.DialogResult = DialogResult.None;
            }
            else
                this.DialogResult = DialogResult.OK;
        }

        private void mchkBoxBiPlot_CheckedChanged(object sender, EventArgs e)
        {
            mchkBoxBiLines.Enabled = mchkBoxBiPlot.Checked;
            mchkBoxBiLabels.Enabled = mchkBoxBiPlot.Checked;

            if (mchkBoxBiPlot.Checked)
            {
                mchkBoxDropLines.Enabled = false;
                mchkBoxLabels.Enabled = false;
            }
            else
            {
                mchkBoxDropLines.Enabled = mrbtn3D.Checked;
                mchkBoxPersp.Enabled = mrbtn3D.Checked;
                mchkBoxLabels.Enabled = (mrbtn3D.Checked || mrbtn2D.Checked);
            }
        }

        private void mcmbBoxFactors_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mcmbBoxFactors.SelectedItem.ToString().Equals("<One Color>"))
            {
                mrBtnPLS.Enabled = false;
                mrBtnPCA.Checked = true;
            }
            else
                mrBtnPLS.Enabled = true;
        }

        private void buttonToggleAll_Click(object sender, System.EventArgs e)
        {
            for (int i = 0; i < mlstViewDataSets.Items.Count; i++)
            {
                if (mlstViewDataSets.Items[i].Checked)
                {
                    mlstViewDataSets.Items[i].Checked = false;
                }
                else
                {
                    mlstViewDataSets.Items[i].Checked = true;
                }
            }
        }

        private void frmPCAplotPar_Load(object sender, EventArgs e)
        {
            mcmbBoxX.DataSource = PrincipalComponents();
            mcmbBoxY.DataSource = PrincipalComponents();
            mcmbBoxZ.DataSource = PrincipalComponents();
            mcmbBoxY.SelectedIndex = mclsPCApar.pcy;
            mcmbBoxX.SelectedIndex = mclsPCApar.pcx;
            mcmbBoxZ.SelectedIndex = mclsPCApar.pcz;

            mchkBoxBiLines.Enabled = mchkBoxBiPlot.Checked;
            mchkBoxBiLabels.Enabled = mchkBoxBiPlot.Checked;

            mchkBoxBiLines.Checked = mclsPCApar.arrows;
            mchkBoxBiLabels.Checked = mclsPCApar.biplotL;
            mchkBoxBiPlot.Checked = mclsPCApar.biplot;
            mchkBoxDropLines.Checked = mclsPCApar.dropLines;
            mchkBoxLabels.Checked = mclsPCApar.labels;
            mchkBoxPersp.Checked = mclsPCApar.persp;
            mchkBoxScree.Checked = mclsPCApar.screeplot;
            mrbtn3D.Checked = mclsPCApar.threeD;
            mcmbBoxFactors.SelectedIndex = mclsPCApar.factorIdx;
            mrBtnPCA.Checked = mclsPCApar.pca;
            mrBtnPLS.Checked = !(mclsPCApar.pca);
            this.DataSetName = mclsPCApar.mstrDatasetName;
            mchkBoxStamp.Checked = mclsPCApar.stamp;
            this.PopulateListView = mclsPCApar.Datasets;
            this.SelectedDatasets = mclsPCApar.CheckedDatasets;

            if (marrFactorList.Count == 1)
                mrBtnPLS.Enabled = false;
        }

        #region Properties

        public clsPCAplotPar clsPCApar
        {
            get
            {
                mclsPCApar.datasubset = "c(" + Selected + ")";
                mclsPCApar.arrows = this.BiArrows;
                mclsPCApar.biplot = this.BiPlot;
                mclsPCApar.biplotL = this.BiLabels;
                mclsPCApar.dropLines = this.DropLines;
                mclsPCApar.factor = this.Factor;
                mclsPCApar.factorIdx = this.FactorIdx;
                mclsPCApar.labels = this.ShowLabels;
                mclsPCApar.pcx = this.PCx;
                mclsPCApar.pcy = this.PCy;
                mclsPCApar.pcz = this.PCz;
                mclsPCApar.persp = this.Perspective;
                mclsPCApar.prinComps = this.PrincipalComps;
                mclsPCApar.screeplot = this.Screeplot;
                mclsPCApar.threeD = this.ThreeD;
                mclsPCApar.pca = this.PCA;
                mclsPCApar.stamp = mchkBoxStamp.Checked;
                mclsPCApar.CheckedDatasets = this.SelectedDatasets;

                return mclsPCApar;
            }
        }
        
        public ArrayList PopulateFactorComboBox
        {
            set
            {
                if (value != null)
                {
                    value.Add("<One Color>");
                    mcmbBoxFactors.DataSource = value;
                    marrFactorList = value;
                }
                else
                {
                    mcmbBoxFactors.Items.Add("<One Color>");
                    marrFactorList.Add("<One Color>");
                }
            }
        }

        public ArrayList PopulateListView
        {
            set
            {

                marrDatasets = value;
                ListViewItem[] lstVcolln = new ListViewItem[marrDatasets.Count];
                var countChecked = 0;

                for (int i = 0; i < marrDatasets.Count; i++)
                {
                    ListViewItem lstVItem = new ListViewItem(marrDatasets[i].ToString())
                    {
                        Tag = i
                    };
                    lstVcolln[i] = lstVItem;

                    if (countChecked >= frmDAnTE.MAX_DATASETS_TO_SELECT)
                    {
                        continue;
                    }
                    lstVcolln[i].Checked = true;
                    countChecked++;
                }
                mlstViewDataSets.Items.AddRange(lstVcolln);
            }
        }

        public string Selected
        {
            get
            {
                string selected = null;
                ListView.CheckedIndexCollection indexes = mlstViewDataSets.CheckedIndices;
                if (indexes.Count != 0)
                {
                    int k = 0;
                    foreach (int i in indexes)
                    {
                        if (k == 0)
                            selected = Convert.ToString(Convert.ToInt16(mlstViewDataSets.Items[i].Tag) + 1);
                        else
                            selected = selected + "," + Convert.ToString(Convert.ToInt16(mlstViewDataSets.Items[i].Tag) + 1);
                        k++;
                    }
                }
                return selected;
            }
        }

        public ArrayList SelectedDatasets
        {
            get
            {
                ArrayList selectedDS = new ArrayList();
                ListView.CheckedIndexCollection indexes = mlstViewDataSets.CheckedIndices;
                if (indexes.Count != 0)
                {
                    foreach (int i in indexes)
                    {
                        selectedDS.Add(mlstViewDataSets.Items[i].ToString());
                    }
                }
                return selectedDS;
            }
            set
            {
                ArrayList selectedDS = value;
                for (int i = 0; i < mlstViewDataSets.Items.Count; i++)
                    for (int j = 0; j < selectedDS.Count; j++)
                    {
                        if (selectedDS[j].ToString().Equals(mlstViewDataSets.Items[i].ToString()))
                            mlstViewDataSets.Items[i].Checked = true;
                    }
            }
        }

        public string Factor
        {
            get
            {
                int idx = 0;
                if (mcmbBoxFactors.SelectedItem.ToString().Equals("<One Color>"))
                    return "1";
                else
                {
                    idx = mcmbBoxFactors.SelectedIndex + 1;
                    return "factors[" + idx.ToString() + ",]";
                }
            }
        }

        
        public string PrincipalComps
        {
            get
            {
                string PCs = "PCs=c(";

                PCs = PCs + Convert.ToString(mcmbBoxX.SelectedIndex + 1) + "," +
                    Convert.ToString(mcmbBoxY.SelectedIndex + 1);
                if (mrbtn3D.Checked)
                    PCs = PCs + "," + Convert.ToString(mcmbBoxZ.SelectedIndex + 1);
                PCs = PCs + ")";
                return PCs;
            }
        }

        public bool DropLines
        {
            get
            {
                return mchkBoxDropLines.Checked;
            }
        }

        public bool ShowLabels
        {
            get
            {
                return mchkBoxLabels.Checked;
            }
        }

        public bool Perspective
        {
            get
            {
                return mchkBoxPersp.Checked;
            }
        }

        public bool BiPlot
        {
            get
            {
                return mchkBoxBiPlot.Checked;
            }
        }

        public bool Screeplot
        {
            get
            {
                return mchkBoxScree.Checked;
            }
        }

        public bool BiArrows
        {
            get
            {
                return mchkBoxBiLines.Checked;
            }
        }

        public bool BiLabels
        {
            get
            {
                return mchkBoxBiLabels.Checked;
            }
        }

        public string DataSetName
        {
            set
            {
                mlblDataName.Text = value;
            }
        }

        public int FactorIdx
        {
            get
            {
                return mcmbBoxFactors.SelectedIndex;
            }
        }

        public int PCx
        {
            get
            {
                return mcmbBoxX.SelectedIndex;
            }
        }

        public int PCy
        {
            get
            {
                return mcmbBoxY.SelectedIndex;
            }
        }

        public int PCz
        {
            get
            {
                return mcmbBoxZ.SelectedIndex;
            }
        }

        public bool ThreeD
        {
            get
            {
                return mrbtn3D.Checked;
            }
        }

        public bool PCA
        {
            get
            {
                return mrBtnPCA.Checked;
            }
        }

        #endregion

        
    }
}