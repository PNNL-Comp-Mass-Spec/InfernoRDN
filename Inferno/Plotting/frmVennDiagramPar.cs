using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DAnTE.Tools;

namespace DAnTE.Inferno
{
    public partial class frmVennDiagramPar : Form
    {
        private const int MAX_LEVELS = 100;
        private string[] strarrFactors = new string[MAX_LEVELS];
        private ArrayList marrFactors;
        private int numFactors = 0;
        private ArrayList marrListDatasets = new ArrayList();
        private DAnTE.Purgatorio.clsVennPar mclsVennPar = new DAnTE.Purgatorio.clsVennPar();
        private bool mblPlotFactors = false;



        public frmVennDiagramPar(DAnTE.Purgatorio.clsVennPar mclsVPar)
        {
            InitializeComponent();
            mclsVennPar = mclsVPar;
            marrListDatasets = mclsVennPar.marrDatasets;
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
            ArrayList marrFs = new ArrayList();

            fillFactorArray();

            for (int num = 0; num < marrFactors.Count; num++)
                marrFs.Add(((clsFactorInfo)marrFactors[num]).mstrFactor);

            mcmbBoxFactors.DataSource = marrFs;
        }

        private void fillFactorArray()
        {
            for (int num = 0; num < marrFactors.Count; num++)
                strarrFactors[num] = ((clsFactorInfo)marrFactors[num]).mstrFactor;
            numFactors = marrFactors.Count;
        }

        private void mcmbBoxFactors_SelectedIndexChanged(object sender, EventArgs e)
        {
            int nSelected;
            string[] strArrTmp = new string[MAX_LEVELS];
            clsFactorInfo selectedF = new clsFactorInfo();

            if (mcmbBoxFactors.SelectedIndex > -1)
            {
                nSelected = mcmbBoxFactors.SelectedIndex;
                mlistBoxLevels.Items.Clear();
                selectedF = ((clsFactorInfo)marrFactors[nSelected]);
                if (selectedF.vCount > 0)
                {
                    for (int i = 0; i < selectedF.vCount; i++)
                    {
                        mlistBoxLevels.Items.Add(selectedF.marrValues[i].ToString());
                    }
                    mlistBoxLevels.SelectedIndex = -1;
                }
            }
        }

        public void PopulateDataListBox()
        {
            int mintMaxColumns = mclsVennPar.marrDatasets.Count;
            object[] lstBoxEntries = new object[mintMaxColumns];
            mclsVennPar.marrDatasets.CopyTo(lstBoxEntries);
            ListBox.ObjectCollection lboxObjColData = new ListBox.ObjectCollection(mlstBoxDataLists, lstBoxEntries);
            mlstBoxDataLists.Items.Clear();
            mlstBoxDataLists.Items.AddRange(lboxObjColData);
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
            //this.PopulateFactorComboBox = mclsVennPar.marrFactorNames;
            this.FactorInfoArray = mclsVennPar.marrFactors;
            updateFactorForm();
            this.DataSetName = mclsVennPar.mstrDatasetName;
            this.mcmbBoxFactors.SelectedIndex = 0;
        }

        #region Properties

        public DAnTE.Purgatorio.clsVennPar clsVennPar
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

        public ArrayList PopulateFactorComboBox
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
                int idx = 0;
                if (mcmbBoxFactors.SelectedItem.ToString().Equals("<One Color>"))
                    return "1";
                else
                {
                    idx = mcmbBoxFactors.SelectedIndex + 1;
                    return "factors[" + idx.ToString() + ",]";
                }
            }
        }

        public int FactorIdx
        {
            get
            {
                return mcmbBoxFactors.SelectedIndex;
            }
        }

        public ArrayList FactorInfoArray
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