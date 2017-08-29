using System;
using System.Windows.Forms;

namespace DAnTE.Inferno
{
    public partial class frmMissingFilter : Form
    {
        public frmMissingFilter()
        {
            InitializeComponent();
        }

        public string CutOff => "cutoff=" + mtxtBoxFthres.Text;

        public string DataSetName
        {
            set => mlblDataName.Text = value;
        }

        private void mbtnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void mbtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}