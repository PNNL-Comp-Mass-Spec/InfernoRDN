using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using DAnTE.Purgatorio;
using DAnTE.Tools;

namespace DAnTE.Inferno
{
    public partial class frmOneSampleTtestPar : Form
    {
        private clsOneSampleTtestPar mclsOneSampleTtestPar = new clsOneSampleTtestPar();

        public frmOneSampleTtestPar(clsOneSampleTtestPar clsTtest)
        {
            InitializeComponent();
            mclsOneSampleTtestPar = clsTtest;
        }

        private void mbtnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void mbtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }


        private void frmOneSampleTtestPar_Load(object sender, EventArgs e)
        {
            mlblDataName.Text = mclsOneSampleTtestPar.mstrDatasetName;
            mNumUpDthres.Value = (decimal)mclsOneSampleTtestPar.numDatapts;
        }

        #region Properties

        public clsOneSampleTtestPar clsTtestPar
        {
            get
            {
                mclsOneSampleTtestPar.numDatapts = this.NumDataThresh;
                return mclsOneSampleTtestPar;
            }
        }
        
        public int NumDataThresh
        {
            get
            {
                int n = Convert.ToInt16(mNumUpDthres.Value);
                return (n);
            }
        }

        #endregion

        
    }
}