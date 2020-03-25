using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DAnTE.Tools;

namespace DAnTE.Inferno
{
    public partial class frmPlotProteinRollup : Form
    {
        private string tempFile = "deleteme.png";
        private string datasetname, RDataset = "pData2";
        private string plotCommand = "plotScaleData";
        private string[] proteinList;
        private clsRconnect rConnector;
        private frmDAnTEmdi m_frmDAnTEmdi;
        public frmDAnTE m_frmDAnTE;

        public frmPlotProteinRollup()
        {
            InitializeComponent();
        }

        private Image LoadImage(string imageFilePath)
        {
            Image currImg;
            using (var fs = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                var img = Image.FromStream(fs);
                fs.Close();
                currImg = img.Clone() as Image;
                img.Dispose();
                File.Delete(imageFilePath);
            }
            return currImg;
        }

        private void mbtnPlot_Click(object sender, EventArgs e)
        {
            var protein = Protein2Plot;

            var proteinRollupDisplay = new frmPlotDisplay();

            if (protein != null)
            {
                var rcmd = plotCommand + "(" + RDataset + ", IPI=\"" + protein + "\",";
                rcmd = rcmd + "Data=" + Dataset + "," + ShowDataLabels + @",file=""" + tempFile + @""")";

                try
                {
                    rConnector.EvaluateNoReturn(rcmd);
                    proteinRollupDisplay.Image = LoadImage(tempFile);
                    proteinRollupDisplay.EnableParameterMenu = false;
                    proteinRollupDisplay.MdiParent = m_frmDAnTEmdi;
                    proteinRollupDisplay.Title = protein;
                    proteinRollupDisplay.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error in R command: " + rcmd + "\nError: " + ex.Message,
                                    "Exception while talking to R");
                }
            }
            else
                MessageBox.Show("Select a protein first!", "Nothing selected", MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
        }

        private void mbtnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void mcmbBoxData_SelectionChangeCommitted(object sender, EventArgs e)
        {
            datasetname = mcmbBoxPData.SelectedItem.ToString();
            mlstBoxProteins.Items.Clear();
            LoadProteinList();
        }

        private void frmPlotProteinRollup_Load(object sender, EventArgs e)
        {
            datasetname = mcmbBoxPData.SelectedItem.ToString();
            mlstBoxProteins.Items.Clear();
            LoadProteinList();
        }

        private void LoadProteinList()
        {
            var pListOK = false;
            switch (datasetname)
            {
                case ("QRollup"):
                    RDataset = "qrollupP";
                    if (rConnector.GetRowNamesFromRmatrix(RDataset))
                    {
                        proteinList = rConnector.RowNames;
                        plotCommand = "plotScaleData.QRup";
                        pListOK = true;
                    }
                    break;
                case ("ZRollup"):
                    RDataset = "pData2"; // Use this to get the row names
                    if (rConnector.GetRowNamesFromRmatrix(RDataset))
                    {
                        proteinList = rConnector.RowNames;
                        plotCommand = "plotScaleData";
                        pListOK = true;
                    }
                    RDataset = "pScaled2"; //Now set to what's used for plotting in R
                    break;
                case ("RRollup"):
                    RDataset = "pData1"; // Use this to get the row names
                    if (rConnector.GetRowNamesFromRmatrix(RDataset))
                    {
                        proteinList = rConnector.RowNames;
                        plotCommand = "plotRefRUpData";
                        pListOK = true;
                    }
                    RDataset = "pScaled1"; //Now set to what's used for plotting in R
                    break;
                case ("None<raw>"):
                    RDataset = "NULL"; // Use this to get the row names
                    try
                    {
                        rConnector.EvaluateNoReturn("ProtNames<-unique(ProtInfo[,2])");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    if (rConnector.GetRstringVector("ProtNames"))
                    {
                        proteinList = rConnector.Vector;
                        plotCommand = "plotProts.raw";
                        pListOK = true;
                    }
                    break;
            }

            if (pListOK)
            {
                var maxColumns = proteinList.Length;
                var lstBoxEntries = new object[maxColumns];
                proteinList.CopyTo(lstBoxEntries, 0);
                var lboxObjColData = new ListBox.ObjectCollection(mlstBoxProteins, lstBoxEntries);
                mlstBoxProteins.Items.Clear();
                mlstBoxProteins.Items.AddRange(lboxObjColData);
                mNiceLineProts.Caption = "Select a Protein to Plot (Total:" + mlstBoxProteins.Items.Count.ToString() +
                                         ")";
            }
            else
            {
                MessageBox.Show("No proteins found!", "Error");
            }
        }

        #region Properties

        public string Protein2Plot
        {
            get
            {
                if (mlstBoxProteins.SelectedIndex >= 0)
                    return mlstBoxProteins.SelectedItem.ToString();

                return null;
            }
        }

        private string ShowDataLabels
        {
            get
            {
                if (mchkBoxXlabels.Checked)
                    return "datalabels=TRUE";
                else
                    return "datalabels=FALSE";
            }
        }

        public string TempFile
        {
            set => tempFile = value;
        }

        public clsRconnect RConnect
        {
            set => rConnector = value;
        }

        public List<string> PopulateDataComboBox
        {
            set => mcmbBoxData.DataSource = value;
        }

        public List<string> PopulatePDataComboBox
        {
            set => mcmbBoxPData.DataSource = value;
        }

        public string Dataset
        {
            get
            {
                var selected = string.Empty;

                if (mcmbBoxData.SelectedItem != null)
                {
                    selected = mcmbBoxData.SelectedItem.ToString();
                }

                var dataset = m_frmDAnTE.CorrespondingRdataset(selected);
                return dataset;
            }
        }

        public string ProtDataset
        {
            get
            {
                var selected = string.Empty;

                if (mcmbBoxPData.SelectedItem != null)
                {
                    selected = mcmbBoxPData.SelectedItem.ToString();
                }

                var dataset = "pData2";
                switch (selected)
                {
                    case ("RRollup"):
                        dataset = "pScaled1";
                        break;
                    case ("ZRollup"):
                        dataset = "pScaled2";
                        break;
                    case ("QRollup"):
                        dataset = "qrollupP";
                        break;
                }
                return dataset;
            }
        }

        public frmDAnTEmdi ParentRef
        {
            set => m_frmDAnTEmdi = value;
        }

        #endregion
    }
}