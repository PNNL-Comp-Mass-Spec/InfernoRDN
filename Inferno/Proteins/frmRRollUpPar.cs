using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using DAnTE.Tools;
using DAnTE.Properties;

namespace DAnTE.Inferno
{
    public partial class frmRRollUpPar : Form
    {
        DAnTE.Purgatorio.clsRRollupPar mclsRRollup = new DAnTE.Purgatorio.clsRRollupPar();

        public frmRRollUpPar(DAnTE.Purgatorio.clsRRollupPar mclsRR)
        {
            InitializeComponent();
            mclsRRollup = mclsRR;
        }

        private void mbtnOK_Click(object sender, EventArgs e)
        {
            int minOverlap = 3;
            float minPresence = 50.0f;

            if (mtxtBoxMinOlap.Text.Length == 0 && mtxtBoxMinOlap.Text.Length == 0 &&
                mtxtBoxMinPresent.Text.Length == 0)
            {
                DialogResult = DialogResult.None;
                return;
            }
            else
            {
                try
                {
                    minOverlap = Convert.ToInt16(MinOverlap);
                    minPresence = Convert.ToSingle(MinPresence);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Data type error:" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DialogResult = DialogResult.None;
                    return;
                }
                if (minPresence < 0 || minPresence > 100)
                {
                    DialogResult = DialogResult.None;
                    MessageBox.Show("Minimum presence value is out of allowed range.", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }
                if (minOverlap < 1)
                {
                    DialogResult = DialogResult.None;
                    MessageBox.Show("Overlap value chosen is not allowed.", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }
            }
            if (!Directory.Exists(mtxtBoxFolder.Text))
            {
                MessageBox.Show("Invalid folder.", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                return;
            }

            DialogResult = DialogResult.OK;
        }

        private void mbtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void mbtnDefaults_Click(object sender, EventArgs e)
        {
            mtxtBoxMinOlap.Text = "3";
            mtxtBoxMinPresent.Text = "50";
            mtxtBoxGminP.Text = "5";
            mtxtBoxGpval.Text = "0.05";
            mchkBoxOneHit.Checked = false;
        }

        private void mchkBoxPlot_CheckedChanged(object sender, EventArgs e)
        {
            if (mchkBoxPlot.Checked)
            {
                mtxtBoxFolder.Enabled = true;
                mbtnSelectFolder.Enabled = true;
            }
            else
            {
                mtxtBoxFolder.Enabled = false;
                mbtnSelectFolder.Enabled = false;
            }
        }

        private void mbtnSelectFolder_Click(object sender, EventArgs e)
        {
            string folderName = null;
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                folderName = folderBrowserDialog1.SelectedPath;
                mtxtBoxFolder.Text = folderName;
            }
            else mtxtBoxFolder.Text = Settings.Default.WorkingFolder;
        }

        private void mtxtBoxGminP_TextChanged(object sender, EventArgs e)
        {
            int grubbsMinP = 5;
            try
            {
                grubbsMinP = Convert.ToInt16(mtxtBoxGminP.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Data type error:" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                return;
            }
            if (grubbsMinP < 3)
            {
                MessageBox.Show("Setting the minimum number of peptides for Grubb's test too low may not be a good idea.",
                    "Peptides for Grubb's too low", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mtxtBoxGminP.Text = "5";
            }
        }

        private void frmRRollUpPar_Load(object sender, EventArgs e)
        {
            this.DataSetName = mclsRRollup.DataSetName;
            this.OutFolder = mclsRRollup.OutFolder_pub;
        }

        #region Properties

        public DAnTE.Purgatorio.clsRRollupPar clsRRollupPar
        {
            get
            {
                mclsRRollup.mstrMinPresence = MinPresence;
                mclsRRollup.mblOneHits = OneHitWonders;
                mclsRRollup.mstrOverlap = MinOverlap;
                mclsRRollup.OutFolder_pub = OutFolder.Replace("\\", "/") + "/";
                mclsRRollup.mblPlot = PlotFlag;
                mclsRRollup.mstrGrubsNum = gminPCount;
                mclsRRollup.mstrGrubsP = Gp_value;
                mclsRRollup.mblModeMean = ModeMean;
                mclsRRollup.mblDoCentering = Center;

                return mclsRRollup;
            }
        }

        public string MinPresence
        {
            get
            {
                return mtxtBoxMinPresent.Text;
            }
        }

        public bool OneHitWonders
        {
            get
            {
                return mchkBoxOneHit.Checked;
            }
        }

        public string Gp_value
        {
            get
            {
                return mtxtBoxGpval.Text;
            }
        }

        public string gminPCount
        {
            get
            {
                return mtxtBoxGminP.Text;
            }
        }

        public string MinOverlap
        {
            get
            {
                return mtxtBoxMinOlap.Text;
            }
        }

        public string OutFolder
        {
            get
            {
                return mtxtBoxFolder.Text;
            }
            set
            {
                mtxtBoxFolder.Text = value;
            }
        }

        public bool DoPlotting
        {
            get
            {
                return mchkBoxPlot.Checked;
            }
        }

        public bool PlotFlag
        {
            get
            {
                return DoPlotting;
            }
        }
                
        public string DataSetName
        {
            set
            {
                mlblDataName.Text = value;
            }
        }
        
        public bool ModeMean
        {
            get
            {
                return mchkBoxMode.Checked;
            }
        }

        public bool Center
        {
            get
            {
                return mchkBoxCenter.Checked;
            }
        }

        #endregion

        
    }
}