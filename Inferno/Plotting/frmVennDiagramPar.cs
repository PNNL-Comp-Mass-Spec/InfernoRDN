using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DAnTE.Tools;

namespace DAnTE.Inferno
{
    public partial class frmVennDiagramPar : Form
    {
        private const int MAX_LEVELS = 100;
        private readonly string[] strarrFactors = new string[MAX_LEVELS];
        private List<clsFactorInfo> marrFactors;
        private readonly Purgatorio.clsVennPar mclsVennPar;
        private bool mblPlotFactors = false;



        public frmVennDiagramPar(Purgatorio.clsVennPar mclsVPar)
        {
            InitializeComponent();
            mclsVennPar = mclsVPar;
        }
        
        private void mbtnOK_Click(object sender, EventArgs e)
        {
            bool mblLists = false;
            bool mblFactors = false;

            mblLists = (!mtxtBoxA.Text.Equals("") &&
                !mtxtBoxB.Text.Equals("") &&
                !mtxtBoxA.Text.Equals(mtxtBoxB.Text) &&
                !mtxtBoxLA.Text.Equals(mtxtBoxLB.Text) &&
                !mtxtBoxLA.Text.Equals(mtxtBoxLC.Text) &&
                !mtxtBoxLC.Text.Equals(mtxtBoxLB.Text));
            mblFactors = (!mtxtBoxflA.Text.Equals("") &&
                !mtxtBoxflB.Text.Equals("") &&
                !mtxtBoxflA.Text.Equals(mtxtBoxflB.Text) &&
                !mtxtBoxfA.Text.Equals(mtxtBoxfB.Text) &&
                !mtxtBoxfA.Text.Equals(mtxtBoxfC.Text) &&
                !mtxtBoxfC.Text.Equals(mtxtBoxfB.Text));

            if (mblLists || mblFactors)
            {
                if (mtabControl.SelectedTab.ToString().Contains("Factors"))
                    mblPlotFactors = true;
                DialogResult = DialogResult.OK;
            }
            else
                MessageBox.Show("Error in selection.", "Try again",
                    MessageBoxButtons.OK, MessageBoxIcon.Question);
        }

        private void mbtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void mBtnA_Click(object sender, EventArgs e)
        {
            if (mlstBoxDataLists.SelectedItems.Count == 1)
            {
                mtxtBoxA.Text = mlstBoxDataLists.SelectedItem.ToString();
            }
        }

        private void mBtnB_Click(object sender, EventArgs e)
        {
            if (mlstBoxDataLists.SelectedItems.Count == 1)
            {
                mtxtBoxB.Text = mlstBoxDataLists.SelectedItem.ToString();
            }
        }

        private void mBtnC_Click(object sender, EventArgs e)
        {
            if (mlstBoxDataLists.SelectedItems.Count == 1)
            {
                mtxtBoxC.Text = mlstBoxDataLists.SelectedItem.ToString();
            }
        }

        private void mBtnfA_Click(object sender, EventArgs e)
        {
            if (mlistBoxLevels.SelectedItems.Count == 1)
            {
                mtxtBoxflA.Text = mlistBoxLevels.SelectedItem.ToString();
            }
        }

        private void mBtnfB_Click(object sender, EventArgs e)
        {
            if (mlistBoxLevels.SelectedItems.Count == 1)
            {
                mtxtBoxflB.Text = mlistBoxLevels.SelectedItem.ToString();
            }
        }

        private void mBtnfC_Click(object sender, EventArgs e)
        {
            if (mlistBoxLevels.SelectedItems.Count == 1)
            {
                mtxtBoxflC.Text = mlistBoxLevels.SelectedItem.ToString();
            }
        }

        private void updateFactorForm()
        {
            var factorList = new List<string>();

            fillFactorArray();

            foreach (var factor in marrFactors)
                factorList.Add(factor.mstrFactor);

            mcmbBoxFactors.DataSource = factorList;
        }

        private void fillFactorArray()
        {
            for (var num = 0; num < marrFactors.Count && num < strarrFactors.Length; num++)
                strarrFactors[num] = marrFactors[num].mstrFactor;
        }

        private void mcmbBoxFactors_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mcmbBoxFactors.SelectedIndex <= -1)
            {
                return;
            }

            var nSelected = mcmbBoxFactors.SelectedIndex;
            mlistBoxLevels.Items.Clear();
            var selectedF = marrFactors[nSelected];

            if (selectedF.vCount <= 0)
            {
                return;
            }

            for (var i = 0; i < selectedF.vCount; i++)
            {
                mlistBoxLevels.Items.Add(selectedF.marrValues[i].ToString());
            }
            mlistBoxLevels.SelectedIndex = -1;
        }

        public void PopulateDataListBox()
        {
            mlstBoxDataLists.DataSource = mclsVennPar.marrDatasets;
        }

        private void frmVennDiagramPar_Load(object sender, EventArgs e)
        {
            PopulateDataListBox();
            mtxtBoxLA.Text = mclsVennPar.labelA;
            mtxtBoxLB.Text = mclsVennPar.labelB;
            mtxtBoxLC.Text = mclsVennPar.labelC;
            mtxtBoxA.Text = mclsVennPar.x1;
            mtxtBoxB.Text = mclsVennPar.x2;
            mtxtBoxC.Text = mclsVennPar.x3;

            this.FactorInfoArray = mclsVennPar.marrFactors;
            updateFactorForm();
            this.DataSetName = mclsVennPar.mstrDatasetName;
            this.mcmbBoxFactors.SelectedIndex = 0;
        }

        #region Properties

        public Purgatorio.clsVennPar clsVennPar
        {
            get
            {
                if (mblPlotFactors)
                {
                    mclsVennPar.labelA = mtxtBoxfA.Text;
                    mclsVennPar.labelB = mtxtBoxfB.Text;
                    mclsVennPar.labelC = mtxtBoxfC.Text;
                    mclsVennPar.x1 = mtxtBoxflA.Text;
                    mclsVennPar.x2 = mtxtBoxflB.Text;
                    mclsVennPar.x3 = mtxtBoxflC.Text;
                }
                else
                {
                    mclsVennPar.labelA = mtxtBoxLA.Text;
                    mclsVennPar.labelB = mtxtBoxLB.Text;
                    mclsVennPar.labelC = mtxtBoxLC.Text;
                    mclsVennPar.x1 = mtxtBoxA.Text;
                    mclsVennPar.x2 = mtxtBoxB.Text;
                    mclsVennPar.x3 = mtxtBoxC.Text;
                }
                mclsVennPar.mblPlotFac = mblPlotFactors;
                mclsVennPar.factor = this.SelectedFactor;

                return mclsVennPar;
            }
        }

        public string SelectedFactor
        {
            get
            {
                if (mblPlotFactors)
                {
                    int idx = mcmbBoxFactors.SelectedIndex + 1;
                    return "Factor=factors[" + idx.ToString() + ",]";
                }
                else
                    return "Factor=1";
            }
        }

        public string DataSetName
        {
            set
            {
                mlblDataName.Text = value;
            }
        }

        public string ListA
        {
            get
            {
                return mtxtBoxA.Text;
            }
        }

        public string ListB
        {
            get
            {
                return mtxtBoxB.Text;
            }
        }

        public string ListC
        {
            get
            {
                if (mtxtBoxLC.Text.Length > 0)
                    return mtxtBoxC.Text;
                else
                    return null;
            }
        }

        public string LabelA
        {
            get
            {
                return mtxtBoxLA.Text;
            }
        }
        
        public string LabelB
        {
            get
            {
                return mtxtBoxLB.Text;
            }
        }
        
        public string LabelC
        {
            get
            {
                return mtxtBoxLB.Text;
            }
        }

        public List<string> PopulateFactorComboBox
        {
            set
            {
                if (value != null)
                {
                    if (value.Count == 0)
                    {
                        //this.mtabControl.TabPages[mtabFactors]
                        value.Add("<NA>");
                    }
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
                if (mcmbBoxFactors.SelectedItem.ToString().Equals("<One Color>"))
                    return "1";
                
                var idx = mcmbBoxFactors.SelectedIndex + 1;
                if (idx < 1)
                    idx = 1;

                return "factors[" + idx + ",]";
            }
        }

        public int FactorIdx
        {
            get
            {
                return mcmbBoxFactors.SelectedIndex;
            }
        }

        public List<clsFactorInfo> FactorInfoArray
        {
            set
            {
                marrFactors = value;
            }
        }

        public bool IsFactorPlot
        {
            get
            {
                return mblPlotFactors;
            }
        }

        #endregion

                
        
    }
}