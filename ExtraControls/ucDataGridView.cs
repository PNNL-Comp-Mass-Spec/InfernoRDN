using System.Data;
using System.Windows.Forms;

namespace DAnTE.ExtraControls
{
    public partial class ucDataGridView : UserControl
    {
        public ucDataGridView()
        {
            InitializeComponent();
        }

        public ucDataGridView(DataTable mDT)
        {
            InitializeComponent();
            dAnTEdatagridview1.DataSource = null;
            dAnTEdatagridview1.DataSource = mDT;
        }

        private void dAnTEdatagridview1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Right) && (e.RowIndex >= 0))
            {
                if (!this.dAnTEdatagridview1.Rows[e.RowIndex].Selected)
                    this.dAnTEdatagridview1.ClearSelection();
                this.dAnTEdatagridview1.Rows[e.RowIndex].Selected = true;
            }
        }

        public DataTable SetDataSource
        {
            set
            {
                dAnTEdatagridview1.DataSource = null;
                dAnTEdatagridview1.DataSource = value;
            }
            get
            {
                return ((DataTable)dAnTEdatagridview1.DataSource);
            }
        }

        public DataGridViewSelectedRowCollection SelectedRows
        {
            get
            {
                return dAnTEdatagridview1.SelectedRows;
            }
        }

        public ContextMenuStrip CxMenu
        {
            set
            {
                dAnTEdatagridview1.ContextMenuStrip = value;
            }
        }

        public DataGridView TableGrid
        {
            get
            {
                return dAnTEdatagridview1;
            }
        }

        
    }
}
