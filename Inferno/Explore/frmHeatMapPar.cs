using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using DAnTE.Properties;
using DAnTE.Purgatorio;

namespace DAnTE.Inferno
{
    public partial class frmHeatMapPar : Form
    {
        private readonly clsHeatmapPar mclsHmapPar;

        //private frmDAnTE mfrmDante;
        private string mstrPaletteName = "Black-Body", customCol;
        private int mintPalette;
        public int maxRowCount = 50;

        public frmHeatMapPar(clsHeatmapPar clsHmapPar)
        {
            InitializeComponent();
            mclsHmapPar = clsHmapPar;
        }

        private List<string> hclust_agglomerations()
        {
            var tmp = new List<string>
            {
                "Single linkage",
                "Complete linkage",
                "Average method",
                "McQuitty method",
                "Ward method",
                "Median linkage",
                "Centroid linkage"
            };

            return tmp;
        }

        private List<string> hclust_distances()
        {
            var tmp = new List<string>
            {
                "Euclidean",
                "Maximum",
                "Manhattan",
                "Canberra",
                "Binary",
                "Pearson",
                "Correlation",
                "Spearman",
                "Kendall"
            };
            return tmp;
        }

        private void mbtnOK_Click(object sender, EventArgs e)
        {
            var success = true;
            int start = 0, end = 50, k = 5;
            try
            {
                start = Convert.ToInt32(mtxtBoxStart.Text);
                end = Convert.ToInt32(mtxtBoxEnd.Text);
                k = Convert.ToInt32(mtxtBoxK.Text);
            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine(ex.Message);
                MessageBox.Show("Invalid entries.", "Errors",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if ((end - start) > 6000 || !(end > start + 1))
            {
                MessageBox.Show("Select a number of rows between 2 ~ 6000", "Incorrect selection",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                success = false;
            }
            if ((k < 2) && mrBtnKmeans.Checked)
            {
                MessageBox.Show("Incorrect cluster number was entered", "Incorrect value",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                success = false;
            }

            if (success)
                DialogResult = DialogResult.OK;
        }

        private void mbtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void mbtnHeatmapPalette_Click(object sender, EventArgs e)
        {
            var colorPaletteSelection = new frmColorPalette
            {
                SetDefaultPalette = mintPalette
            };

            if (colorPaletteSelection.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            mintPalette = colorPaletteSelection.ColorPalette;
            customCol = colorPaletteSelection.CustomColors;
            mstrPaletteName = colorPaletteSelection.ColorPaletteName;
            mlblHeatPalette.Text = mstrPaletteName;
            Settings.Default.colorMapType = mintPalette;
            Settings.Default.colorMap = mstrPaletteName;
            Settings.Default.Save();
        }

        private void mrBtnKmeans_CheckedChanged(object sender, EventArgs e)
        {
            mpanelKmeans.Enabled = mrBtnKmeans.Checked;
            mpanelHclust.Enabled = !mrBtnKmeans.Checked;
        }

        private void mrBtnHclust_CheckedChanged(object sender, EventArgs e)
        {
            mpanelKmeans.Enabled = !mrBtnHclust.Checked;
            mpanelHclust.Enabled = mrBtnHclust.Checked;
        }

        private void mchkBoxRows_CheckedChanged(object sender, EventArgs e)
        {
            mtabLayoutRows.Enabled = mchkBoxRows.Checked;
        }

        
        private void frmHeatMapPar_Load(object sender, EventArgs e)
        {
            DataSetName = mclsHmapPar.mstrDatasetName;
            
            mintPalette = mclsHmapPar.paletteType;
            mstrPaletteName = mclsHmapPar.palettename;
            mlblHeatPalette.Text = mstrPaletteName;
            customCol = mclsHmapPar.customCol;
            mchkBoxScale.Checked = mclsHmapPar.rowScale;
            mchkBoxXlab.Checked = mclsHmapPar.noxlab;
            mtxtBoxMaxCol.Text = mclsHmapPar.mdblMaxCol.ToString(CultureInfo.InvariantCulture);
            mtxtBoxMinCol.Text = mclsHmapPar.mdblMinCol.ToString(CultureInfo.InvariantCulture);
            mchkBoxColRng.Checked = mclsHmapPar.mblsetColRng;
            mtxtBoxMaxCol.Enabled = mchkBoxColRng.Checked;
            mtxtBoxMinCol.Enabled = mchkBoxColRng.Checked;
            
            PopulateFactorComboBox = mclsHmapPar.Factors;
            mcmbBoxFactors.SelectedIndex = mclsHmapPar.mintFactorIndex - 1;
            
            mrBtnHclust.Checked = mclsHmapPar.hclust;
            mrBtnKmeans.Checked = !mclsHmapPar.hclust;
            mchkBoxRows.Checked = mclsHmapPar.rowClust;
            mchkBoxCols.Checked = mclsHmapPar.colClust;
            mtxtBoxK.Text = mclsHmapPar.k.ToString();
            mchkBoxSeed.Checked = mclsHmapPar.fixSeed;
            
            mrBtnRowSelection.Checked = mclsHmapPar.gridSelect;
            mrBtnRowSelection.Enabled = mclsHmapPar.rowsSelected;
            mrBtnSelectSubset.Checked = !mclsHmapPar.gridSelect;
            mtxtBoxStart.Text = mclsHmapPar.rStart.ToString();
            EndIdx = mclsHmapPar.rEnd;

            mpanelKmeans.Enabled = mrBtnKmeans.Checked;
            mpanelHclust.Enabled = !mrBtnKmeans.Checked;
            mcmbBoxDist.DataSource = hclust_distances();
            mcmbBoxAggl.DataSource = hclust_agglomerations();
            mcmbBoxAggl.SelectedIndex = mclsHmapPar.agglomeration;
            mcmbBoxDist.SelectedIndex = mclsHmapPar.distance;
        }
        
        #region Properties
        public clsHeatmapPar clsHmapPar
        {
            get
            {
                mclsHmapPar.palettename = mstrPaletteName;
                mclsHmapPar.paletteType = mintPalette;
                mclsHmapPar.customCol = customCol;
                mclsHmapPar.rowScale = mchkBoxScale.Checked;
                mclsHmapPar.mblsetColRng = mchkBoxColRng.Checked;
                mclsHmapPar.mdblMaxCol = Convert.ToDouble(mtxtBoxMaxCol.Text, NumberFormatInfo.InvariantInfo);
                mclsHmapPar.mdblMinCol = Convert.ToDouble(mtxtBoxMinCol.Text, NumberFormatInfo.InvariantInfo);

                mclsHmapPar.mintFactorIndex = FactorIndex;
                mclsHmapPar.mstrFactor = Factor;
                mclsHmapPar.noxlab = mchkBoxXlab.Checked;

                mclsHmapPar.hclust = mrBtnHclust.Checked;
                mclsHmapPar.rowClust = mchkBoxRows.Checked;
                mclsHmapPar.colClust = mchkBoxCols.Checked;
                mclsHmapPar.k = Convert.ToInt32(mtxtBoxK.Text);
                mclsHmapPar.fixSeed = mchkBoxSeed.Checked;
                mclsHmapPar.agglomeration = mcmbBoxAggl.SelectedIndex;
                mclsHmapPar.distance = mcmbBoxDist.SelectedIndex;

                mclsHmapPar.customSelect = mrBtnSelectSubset.Checked;
                mclsHmapPar.gridSelect = !mrBtnSelectSubset.Checked;
                mclsHmapPar.rStart = Convert.ToInt32(mtxtBoxStart.Text);
                mclsHmapPar.rEnd = EndIdx;

                return mclsHmapPar;
            }
        }

        public string DataSetName
        {
            set
            {
                mlblDataName.Text = value;
            }
        }

        public List<string> PopulateFactorComboBox
        {
            set
            {
                if (value != null)
                {
                    if (value.Count == 0)
                        value.Add("<NA>");
                    else if (!(value[value.Count - 1].Equals("<NA>")))
                        value.Add("<NA>");
                    mcmbBoxFactors.DataSource = value;
                }
                else
                    mcmbBoxFactors.Items.Add("<NA>");
            }
        }
        
        public string Factor
        {
            get
            {
                if (mcmbBoxFactors.SelectedItem != null)
                {
                    return mcmbBoxFactors.SelectedItem.ToString();
                }
                else
                    return "<NA>";
            }
        }

        public int FactorIndex
        {
            get
            {
                if (mcmbBoxFactors.SelectedItem != null)
                {
                    var idx = mcmbBoxFactors.SelectedIndex;
                    return idx+1;
                }

                return -1;
            }
        }
                
        public int StartIdx
        {
            get
            {
                return Convert.ToInt32(mtxtBoxStart.Text);
            }
            set
            {
                mtxtBoxStart.Text = value.ToString();
            }
        }

        public int EndIdx
        {
            get
            {
                var endidx = Convert.ToInt32(mtxtBoxEnd.Text);
                if (endidx > maxRowCount)
                    return maxRowCount;
                
                return endidx;
            }
            set
            {
                if (maxRowCount < value)
                    mtxtBoxEnd.Text = maxRowCount.ToString();
                else
                    mtxtBoxEnd.Text = value.ToString();
            }
        }

        public int ColorPalette
        {
            get
            {
                return mintPalette;
            }
        }

        public string ColorPaletteName
        {
            get
            {
                string cMap = null;
                if (mintPalette == 1)
                    cMap = "Green-Red"; //1
                if (mintPalette == 2)
                    cMap = "Heat-Palette"; //2
                if (mintPalette == 3)
                    cMap = "Custom"; //3
                if (mintPalette == 4)
                    cMap = "Black-Body"; //4
                if (mintPalette == 5)
                    cMap = "Blue-White-Red"; //5
                return cMap;
            }
        }

        public bool DoClust
        {
            get
            {
                return mchkBoxRows.Checked;
            }
        }
        #endregion

        private void mchkBoxColRng_CheckedChanged(object sender, EventArgs e)
        {
            mtxtBoxMaxCol.Enabled = mchkBoxColRng.Checked;
            mtxtBoxMinCol.Enabled = mchkBoxColRng.Checked;
        }
        
    }
}