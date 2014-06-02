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
    public partial class frmShapiroWilksPar : Form
    {
        private clsShapiroWilksPar mclsShapiroWilksPar = new clsShapiroWilksPar();

        public frmShapiroWilksPar(clsShapiroWilksPar clsKW)
        {
            InitializeComponent();
            mclsShapiroWilksPar = clsKW;
        }

        private void mbtnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void mbtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        
        private void frmShapiroWilkspar_Load(object sender, EventArgs e)
        {
            mlblDataName.Text = mclsShapiroWilksPar.mstrDatasetName;
            mNumUpDthres.Value = (decimal)mclsShapiroWilksPar.numDatapts;
        }

        #region Properties

        public clsShapiroWilksPar clsShapiroWilksPar
        {
            get
            {
                mclsShapiroWilksPar.numDatapts = this.NumDataThresh;
                return mclsShapiroWilksPar;
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