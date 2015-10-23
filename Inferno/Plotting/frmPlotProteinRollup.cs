using System;
using System.Collections;
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
        private string datasetname, Rdataset = "pData2";
        private string plotCommand = "plotScaleData";
        private string[] proteinList;
        private clsRconnect rConnector;
        private frmDAnTEmdi m_frmDAnTEmdi;
        public frmDAnTE m_frmDAnTE;
        //private frmPlotDisplay mfrmPlotDisplay = new frmPlotDisplay();

        public frmPlotProteinRollup()
        {
            InitializeComponent();
        }

        private Image LoadImage(string tempFile)
        {
            Image currImg = null;
            using (FileStream fs = new FileStream(tempFile, FileMode.Open,
                                FileAccess.Read, FileShare.ReadWrite))
            {
                Image img = Image.FromStream(fs);
                fs.Close();
                currImg = img.Clone() as Image;
                img.Dispose();
                File.Delete(tempFile);
            }
            return currImg;
        }

        private void mbtnPlot_Click(object sender, EventArgs e)
        {
            string rcmd = null;
            string protein = this.Protein2Plot;
            //string Data = Dataset;
            frmPlotDisplay mfrmPlotDisplay = new frmPlotDisplay();

            if (protein != null)
            {
                rcmd = plotCommand + "(" + Rdataset + ", IPI=\"" + protein + "\",";
                rcmd = rcmd + "Data=" + Dataset + "," + ShowDataLabels + @",file=""" + tempFile + @""")";

                try
                {
                    rConnector.EvaluateNoReturn(rcmd);
                    mfrmPlotDisplay.Image = LoadImage(tempFile);
                    mfrmPlotDisplay.EnableParameterMenu = false;
                    mfrmPlotDisplay.MdiParent = m_frmDAnTEmdi;
                    mfrmPlotDisplay.Title = protein;
                    mfrmPlotDisplay.Show();
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
            this.Close();
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
            bool pListOK = false;
            switch (datasetname)
            {
                case ("QRollup"):
                    Rdataset = "qrollupP";
                    if (rConnector.GetRowNamesFromRmatrix(Rdataset))
                    {
                        proteinList = rConnector.RowNames;
                        plotCommand = "plotScaleData.QRup";
                        pListOK = true;
                    }
                    break;
                case ("ZRollup"):
                    Rdataset = "pData2"; // Use this to get the row names
                    if (rConnector.GetRowNamesFromRmatrix(Rdataset))
                    {
                        proteinList = rConnector.RowNames;
                        plotCommand = "plotScaleData";
                        pListOK = true;
                    }
                    Rdataset = "pScaled2"; //Now set to what's used for plotting in R
                    break;
                case ("RRollup"):
                    Rdataset = "pData1"; // Use this to get the row names
                    if (rConnector.GetRowNamesFromRmatrix(Rdataset))
                    {
                        proteinList = rConnector.RowNames;
                        plotCommand = "plotRefRUpData";
                        pListOK = true;
                    }
                    Rdataset = "pScaled1"; //Now set to what's used for plotting in R
                    break;
                case ("None<raw>"):
                    Rdataset = "NULL"; // Use this to get the row names
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
                default:
                    break;
            }
            if (pListOK)
            {
                int mintMaxColumns = proteinList.Length;
                object[] lstBoxEntries = new object[mintMaxColumns];
                proteinList.CopyTo(lstBoxEntries, 0);
                ListBox.ObjectCollection lboxObjColData = new ListBox.ObjectCollection(mlstBoxProteins, lstBoxEntries);
                mlstBoxProteins.Items.Clear();
                mlstBoxProteins.Items.AddRange(lboxObjColData);
                mNiceLineProts.Caption = "Select a Protein to Plot (Total:" + mlstBoxProteins.Items.Count.ToString() + ")";
            }
            else
                MessageBox.Show("No proteins found!", "Error");
        }

        #region Properties

        public string Protein2Plot
        {
            get
            {
                if (mlstBoxProteins.SelectedIndex >= 0)
                    return mlstBoxProteins.SelectedItem.ToString();
                else
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
            set
            {
                tempFile = value;
            }
        }

        public clsRconnect RConnect
        {
            set
            {
                rConnector = value;
            }
        }

        public List<string> PopulateDataComboBox
        {
            set
            {
                mcmbBoxData.DataSource = value;
            }
        }

        public List<string> PopulatePDataComboBox
        {
            set
            {
                mcmbBoxPData.DataSource = value;
            }
        }

        public string Dataset
        {
            get
            {
                var selected = mcmbBoxData.SelectedItem.ToString();
                var dataset = m_frmDAnTE.CorrespondingRdataset(selected);
                return dataset;
            }
        }

        public string ProtDataset
        {
            get
            {
                string selected = mcmbBoxPData.SelectedItem.ToString();
                string dataset = "pData2";
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
                    default:
                        break;
                }
                return dataset;
            }
        }

        public frmDAnTEmdi ParentRef
        {
            set
            {
                m_frmDAnTEmdi = value;
            }
        }

        #endregion
    }
}