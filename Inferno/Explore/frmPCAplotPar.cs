using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DAnTE.Purgatorio;
using DAnTE.Tools;

namespace DAnTE.Inferno
{
    public partial class frmPCAplotPar : Form
    {
        private const int MAX = frmDAnTE.MAX_DATASETS_TO_SELECT;

        private readonly clsPCAplotPar mPCAOptions;
        private List<string> marrDatasets = new List<string>();
        private List<string> marrFactorList = new List<string>();

        public frmPCAplotPar(clsPCAplotPar pcaOptions)
        {
            mPCAOptions = pcaOptions;
            InitializeComponent();
        }


        private List<string> PrincipalComponents()
        {
            var pcNames = new List<string>
            {
                "PC1",
                "PC2",
                "PC3",
                "PC4",
                "PC5"
            };

            return pcNames;
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
                DialogResult = DialogResult.None;
            }
            else
                DialogResult = DialogResult.OK;
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

        private void buttonToggleAll_Click(object sender, EventArgs e)
        {
            clsUtilities.ToggleListViewCheckboxes(mlstViewDataSets, MAX, true);
        }

        private void frmPCAplotPar_Load(object sender, EventArgs e)
        {
            mcmbBoxX.DataSource = PrincipalComponents();
            mcmbBoxY.DataSource = PrincipalComponents();
            mcmbBoxZ.DataSource = PrincipalComponents();
            mcmbBoxY.SelectedIndex = mPCAOptions.pcy;
            mcmbBoxX.SelectedIndex = mPCAOptions.pcx;
            mcmbBoxZ.SelectedIndex = mPCAOptions.pcz;

            mchkBoxBiLines.Enabled = mchkBoxBiPlot.Checked;
            mchkBoxBiLabels.Enabled = mchkBoxBiPlot.Checked;

            mchkBoxBiLines.Checked = mPCAOptions.arrows;
            mchkBoxBiLabels.Checked = mPCAOptions.biplotL;
            mchkBoxBiPlot.Checked = mPCAOptions.biplot;
            mchkBoxDropLines.Checked = mPCAOptions.dropLines;
            mchkBoxLabels.Checked = mPCAOptions.labels;
            mchkBoxPersp.Checked = mPCAOptions.persp;
            mchkBoxScree.Checked = mPCAOptions.screeplot;
            mrbtn3D.Checked = mPCAOptions.threeD;
            mcmbBoxFactors.SelectedIndex = mPCAOptions.factorIdx;
            mrBtnPCA.Checked = mPCAOptions.pca;
            mrBtnPLS.Checked = !(mPCAOptions.pca);
            DataSetName = mPCAOptions.mstrDatasetName;
            mchkBoxStamp.Checked = mPCAOptions.stamp;
            PopulateListView = mPCAOptions.Datasets;
            SelectedDatasets = mPCAOptions.CheckedDatasets;

            if (marrFactorList.Count == 1)
                mrBtnPLS.Enabled = false;
        }

        #region Properties

        public clsPCAplotPar PCAOptions
        {
            get
            {
                mPCAOptions.datasubset = "c(" + Selected + ")";
                mPCAOptions.arrows = BiArrows;
                mPCAOptions.biplot = BiPlot;
                mPCAOptions.biplotL = BiLabels;
                mPCAOptions.dropLines = DropLines;
                mPCAOptions.factor = Factor;
                mPCAOptions.factorIdx = FactorIdx;
                mPCAOptions.labels = ShowLabels;
                mPCAOptions.pcx = PCx;
                mPCAOptions.pcy = PCy;
                mPCAOptions.pcz = PCz;
                mPCAOptions.persp = Perspective;
                mPCAOptions.prinComps = PrincipalComps;
                mPCAOptions.screeplot = Screeplot;
                mPCAOptions.threeD = ThreeD;
                mPCAOptions.pca = PCA;
                mPCAOptions.stamp = mchkBoxStamp.Checked;
                mPCAOptions.CheckedDatasets = SelectedDatasets;

                return mPCAOptions;
            }
        }

        public List<string> PopulateFactorComboBox
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

        public List<string> PopulateListView
        {
            set
            {
                marrDatasets = value;
                var lstVcolln = new ListViewItem[marrDatasets.Count];
                var countChecked = 0;

                for (var i = 0; i < marrDatasets.Count; i++)
                {
                    var lstVItem = new ListViewItem(marrDatasets[i])
                    {
                        Tag = i
                    };
                    lstVcolln[i] = lstVItem;

                    if (countChecked >= MAX)
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
                var indexes = mlstViewDataSets.CheckedIndices;
                if (indexes.Count != 0)
                {
                    var k = 0;
                    foreach (int i in indexes)
                    {
                        if (k == 0)
                            selected = Convert.ToString(Convert.ToInt16(mlstViewDataSets.Items[i].Tag) + 1);
                        else
                            selected = selected + "," +
                                       Convert.ToString(Convert.ToInt16(mlstViewDataSets.Items[i].Tag) + 1);
                        k++;
                    }
                }
                return selected;
            }
        }

        public List<string> SelectedDatasets
        {
            get
            {
                var selectedDS = new List<string>();
                var indexes = mlstViewDataSets.CheckedIndices;
                if (indexes.Count != 0)
                {
                    selectedDS.AddRange(from int i in indexes select mlstViewDataSets.Items[i].ToString());
                }
                return selectedDS;
            }
            set
            {
                var selectedDS = value;
                for (var i = 0; i < mlstViewDataSets.Items.Count; i++)
                    foreach (var datasetName in selectedDS)
                    {
                        if (datasetName.Equals(mlstViewDataSets.Items[i].ToString()))
                            mlstViewDataSets.Items[i].Checked = true;
                    }
            }
        }

        public string Factor
        {
            get
            {
                if (mcmbBoxFactors.SelectedItem.ToString().Equals("<One Color>"))
                    return "1";

                var idx = mcmbBoxFactors.SelectedIndex + 1;
                return "factors[" + idx.ToString() + ",]";
            }
        }


        public string PrincipalComps
        {
            get
            {
                var PCs = "PCs=c(";

                PCs = PCs + Convert.ToString(mcmbBoxX.SelectedIndex + 1) + "," +
                      Convert.ToString(mcmbBoxY.SelectedIndex + 1);
                if (mrbtn3D.Checked)
                    PCs = PCs + "," + Convert.ToString(mcmbBoxZ.SelectedIndex + 1);
                PCs = PCs + ")";
                return PCs;
            }
        }

        public bool DropLines => mchkBoxDropLines.Checked;

        public bool ShowLabels => mchkBoxLabels.Checked;

        public bool Perspective => mchkBoxPersp.Checked;

        public bool BiPlot => mchkBoxBiPlot.Checked;

        public bool Screeplot => mchkBoxScree.Checked;

        public bool BiArrows => mchkBoxBiLines.Checked;

        public bool BiLabels => mchkBoxBiLabels.Checked;

        public string DataSetName
        {
            set => mlblDataName.Text = value;
        }

        public int FactorIdx => mcmbBoxFactors.SelectedIndex;

        public int PCx => mcmbBoxX.SelectedIndex;

        public int PCy => mcmbBoxY.SelectedIndex;

        public int PCz => mcmbBoxZ.SelectedIndex;

        public bool ThreeD => mrbtn3D.Checked;

        public bool PCA => mrBtnPCA.Checked;

        #endregion
    }
}