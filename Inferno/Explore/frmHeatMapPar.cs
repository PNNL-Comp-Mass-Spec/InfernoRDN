using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using DAnTE.Properties;
using DAnTE.Tools;
using DAnTE.Purgatorio;

namespace DAnTE.Inferno
{
    public partial class frmHeatMapPar : Form
    {
        private clsHeatmapPar mclsHmapPar = new clsHeatmapPar();
        //private frmDAnTE mfrmDante;
        private string mstrPaletteName = "Black-Body", customCol = null;
        private int mintPalette;
        public int maxRowCount = 50;

        public frmHeatMapPar(clsHeatmapPar clsHmapPar)
        {
            InitializeComponent();
            mclsHmapPar = clsHmapPar;
        }

        private ArrayList hclust_agglomerations()
        {
            ArrayList tmp = new ArrayList();
      
            //tmp.Add("Single linkage");
            //tmp.Add("Complete linkage");
            //tmp.Add("Average method");
            //tmp.Add("McQuitty method");
            //tmp.Add("Ward method");
            ////tmp.Add("Median linkage");
            ////tmp.Add("Centroid linkage");
            tmp.Add("Single linkage");
            tmp.Add("Complete linkage");
            tmp.Add("Average method");
            tmp.Add("McQuitty method");
            tmp.Add("Ward method");
            tmp.Add("Median linkage");
            tmp.Add("Centroid linkage");
            return tmp;
        }

        private ArrayList hclust_distances()
        {
            ArrayList tmp = new ArrayList();

            //tmp.Add("Euclidean");
            //tmp.Add("Maximum");
            //tmp.Add("Manhattan");
            //tmp.Add("Canberra");
            //tmp.Add("Binary");
            //tmp.Add("Minkowski");
            tmp.Add("Euclidean");
            tmp.Add("Maximum");
            tmp.Add("Manhattan");
            tmp.Add("Canberra");
            tmp.Add("Binary");
            tmp.Add("Pearson");
            tmp.Add("Correlation");
            tmp.Add("Spearman");
            tmp.Add("Kendall");
            return tmp;
        }

        private void mbtnOK_Click(object sender, EventArgs e)
        {
            bool success = true;
            int start = 0, end = 50, k = 5;
            double maxCol = 0.0, minCol = 0.0;
            try
            {
                start = Convert.ToInt32(mtxtBoxStart.Text);
                end = Convert.ToInt32(mtxtBoxEnd.Text);
                k = Convert.ToInt32(mtxtBoxK.Text);
                minCol = Convert.ToDouble(mtxtBoxMinCol.Text);
                maxCol = Convert.ToDouble(mtxtBoxMaxCol.Text);
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
                this.DialogResult = DialogResult.OK;
        }

        private void mbtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void mbtnHeatmapPalette_Click(object sender, EventArgs e)
        {
            frmColorPalette mfrmColPalette = new frmColorPalette
            {
                SetDefaultPalette = mintPalette
            };

            if (mfrmColPalette.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            mintPalette = mfrmColPalette.ColorPalette;
            customCol = mfrmColPalette.CustomColors;
            mstrPaletteName = mfrmColPalette.ColorPaletteName;
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
            this.DataSetName = mclsHmapPar.mstrDatasetName;
            
            mintPalette = mclsHmapPar.paletteType;
            mstrPaletteName = mclsHmapPar.palettename;
            mlblHeatPalette.Text = mstrPaletteName;
            customCol = mclsHmapPar.customCol;
            mchkBoxScale.Checked = mclsHmapPar.rowScale;
            mchkBoxXlab.Checked = mclsHmapPar.noxlab;
            mtxtBoxMaxCol.Text = mclsHmapPar.mdblMaxCol.ToString();
            mtxtBoxMinCol.Text = mclsHmapPar.mdblMinCol.ToString();
            mchkBoxColRng.Checked = mclsHmapPar.mblsetColRng;
            mtxtBoxMaxCol.Enabled = mchkBoxColRng.Checked;
            mtxtBoxMinCol.Enabled = mchkBoxColRng.Checked;
            
            this.PopulateFactorComboBox = mclsHmapPar.Factors;
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
            this.EndIdx = mclsHmapPar.rEnd;

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
                mclsHmapPar.mdblMaxCol = Convert.ToDouble(mtxtBoxMaxCol.Text);
                mclsHmapPar.mdblMinCol = Convert.ToDouble(mtxtBoxMinCol.Text);

                mclsHmapPar.mintFactorIndex = this.FactorIndex;
                mclsHmapPar.mstrFactor = this.Factor;
                mclsHmapPar.noxlab = this.mchkBoxXlab.Checked;

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
                mclsHmapPar.rEnd = this.EndIdx;

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

        public ArrayList PopulateFactorComboBox
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
                int idx = 0;
                if (mcmbBoxFactors.SelectedItem != null)
                {
                    idx = mcmbBoxFactors.SelectedIndex;
                    return idx+1;
                }
                else
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
                int endidx = Convert.ToInt32(mtxtBoxEnd.Text);
                if (endidx > maxRowCount)
                    return maxRowCount;
                else
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