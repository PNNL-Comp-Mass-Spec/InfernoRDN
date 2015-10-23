using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using Wizard.UI;
using DAnTE.Properties;

namespace DAnTE.Inferno
{
    public partial class ctlMSMSparaWizPage : Wizard.UI.InternalWizardPage
    {
        private FolderBrowserDialog folderBrwseDlg;
        private string foldername = null;
        private bool mblUseSEQOut = false;

        public ctlMSMSparaWizPage()
        {
            InitializeComponent();
            folderBrwseDlg = new FolderBrowserDialog();
        }

        private void ctlMSMSparaWizPage_SetActive(object sender, CancelEventArgs e)
        {
            SetWizardButtons(WizardButtons.Back | WizardButtons.Next);
            mtxtBoxAnFolder.Text = foldername;
            mtxtBoxAnFolder.Enabled = !mblUseSEQOut;
            mBtnBrowse.Enabled = !mblUseSEQOut;

        }

        private void mBtnBrowse_Click(object sender, EventArgs e)
        {
            folderBrwseDlg.Description = "Select the folder where Sequest *.out files reside.";
            folderBrwseDlg.SelectedPath = foldername;
            DialogResult dresult = folderBrwseDlg.ShowDialog();
            if (dresult == DialogResult.OK)
            {
                foldername = folderBrwseDlg.SelectedPath;
                mtxtBoxAnFolder.Text = foldername;
                Settings.Default.msmsAnalysisFolder = foldername;
                Settings.Default.Save();
            }
        }

        #region Properties
        public int MaxRank
        {
            get
            {
                return decimal.ToInt16(mNumUDXcorRank.Value);
            }
        }

        public string AnalysisFolder
        {
            get
            {
                return mtxtBoxAnFolder.Text;
            }
            set
            {
                foldername = value;
                mtxtBoxAnFolder.Text = foldername;
                Settings.Default.msmsAnalysisFolder = foldername;
                Settings.Default.Save();
            }
        }

        public string TrypticState
        {
            get
            {
                bool none, full, partial;
                none = mchkBoxNone.Checked;
                full = mchkBoxFull.Checked;
                partial = mchkBoxPartial.Checked;

                if (none && partial && full)
                    return @"TrypState='111'";
                if (none && partial && !full)
                    return @"TrypState='110'";
                if (none && !partial && !full)
                    return @"TrypState='100'";
                if (!none && partial && !full)
                    return @"TrypState='010'";
                if (!none && !partial && full)
                    return @"TrypState='001'";
                if (!none && partial && full)
                    return @"TrypState='011'";
                return @"TrypState='111'";
                //if (mrbTSany.Checked)
                //    return @"TrypState='ANY'";
                //else if (mrbTSfully.Checked)
                //    return @"TrypState='FULLY'";
                //else if (mrbTSnone.Checked)
                //    return @"TrypState='NONE'";
                //else
                //    return @"TrypState='PARTIAL'";
            }
        }


        public string DelCn2Threshold
        {
            get
            {
                double delcn2 = 0.1;
                
                try
                {
                    delcn2 = Convert.ToDouble(mtxtBoxDCn2Th.Text, NumberFormatInfo.InvariantInfo);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    delcn2 = 0.1;
                }
                return "DelCn2Th=" + delcn2.ToString(CultureInfo.InvariantCulture);
            }
        }

        public string XCorrThresholds
        {
            get
            {
                double xc1th;
                double xc2th;
                double xc3th;
                double xcOth;
                try
                {
                    xc1th = Convert.ToDouble(mtxtBxXC1Th.Text, NumberFormatInfo.InvariantInfo);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    xc1th = 1.5;
                }
                try
                {
                    xc2th = Convert.ToDouble(mtxtBxXC2Th.Text, NumberFormatInfo.InvariantInfo);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    xc2th = 1.5;
                }
                try
                {
                    xc3th = Convert.ToDouble(mtxtBxXC3Th.Text, NumberFormatInfo.InvariantInfo);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    xc3th = 1.5;
                }
                try
                {
                    xcOth = Convert.ToDouble(mtxtBxXCOTh.Text, NumberFormatInfo.InvariantInfo);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    xcOth = 1.5;
                }
                return "XCorr1Th=" + xc1th.ToString(CultureInfo.InvariantCulture) + ",XCorr2Th=" + xc2th.ToString(CultureInfo.InvariantCulture) +
                    ",XCorr3Th=" + xc3th.ToString(CultureInfo.InvariantCulture) + ",XCorrOTh=" + xcOth.ToString(CultureInfo.InvariantCulture);
            }
        }

        public bool UseSeqOUTFiles
        {
            set
            {
                mblUseSEQOut = value;
            }
        }
        #endregion

    }
}