using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DAnTE.Tools;

namespace DAnTE.Inferno
{
    public partial class frmLogPar : Form
    {
        DAnTE.Purgatorio.clsLogTransformPar mclsLogParam = new DAnTE.Purgatorio.clsLogTransformPar();

        public frmLogPar(DAnTE.Purgatorio.clsLogTransformPar clsLogPar)
        {
            InitializeComponent();
            mclsLogParam = clsLogPar;
        }

        private void mbtnOK_Click(object sender, EventArgs e)
        {
            float bias = 10.0f;

            if (mtxtBoxBias.Text.Length == 0)
            {
                MessageBox.Show("Empty bias not valid!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                return;
            }
            else
            {
                try
                {
                    bias = Convert.ToSingle(mtxtBoxBias.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Data type error:" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DialogResult = DialogResult.None;
                    return;
                }
            }
            DialogResult = DialogResult.OK;
        }

        private void mbtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void frmLogPar_Load(object sender, EventArgs e)
        {
            this.DataSetName = mclsLogParam.DatasetName;
            this.LogBase = mclsLogParam.LogBase;
            this.Bias = mclsLogParam.LogBias;
            this.BiasOp = mclsLogParam.BiasOp;
        }

        #region Properties
        public DAnTE.Purgatorio.clsLogTransformPar clsLogPar
        {
            get
            {
                mclsLogParam.LogBias = this.Bias;
                mclsLogParam.BiasOp = (mrBtnAdd.Checked) ? "Additive" : "Multiplicative";
                mclsLogParam.DatasetName = mlblDataName.Text;
                mclsLogParam.LogBase = this.LogBase;

                return mclsLogParam;
            }
        }

        private string DataSetName
        {
            set
            {
                mlblDataName.Text = value;
            }
        }

        private string LogBase
        {
            get
            {
                if (mrBtnLog10.Checked)
                    return "Log10";
                else if (mrBtnLogn.Checked)
                    return "NaturalLog";
                else
                    return "Log2";
            }
            set
            {
                if (value.Equals("Log10"))
                    mrBtnLog10.Checked = true;
                else if (value.Equals("NaturalLog"))
                    mrBtnLogn.Checked = true;
                else
                    mrBtnLog2.Checked = true;
            }
        }

        private double Bias
        {
            get
            {
                return Convert.ToDouble(mtxtBoxBias.Text);
            }
            set
            {
                mtxtBoxBias.Text = value.ToString();
            }
        }

        private string BiasOp
        {
            get
            {
                if (mrBtnAdd.Checked)
                    return "Additive";
                else
                    return "Multiplicative";
            }
            set
            {
                if (value.Equals("Additive"))
                    mrBtnAdd.Checked = true;
                else
                    mrBtnMult.Checked = true;

            }
        }

        #endregion

        
    }
}