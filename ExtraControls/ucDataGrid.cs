using System.Data;

namespace DAnTE.ExtraControls
{
    /// <summary>
    /// Summary description for ucDataGrid.
    /// </summary>
    public abstract class ucDataGrid : System.Windows.Forms.UserControl
    {
        private System.Windows.Forms.DataGrid dataGrid1;

        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private readonly System.ComponentModel.Container components = null;

        protected ucDataGrid()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

        }


        public ucDataGrid(DataTable dataT)
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            dataGrid1.DataSource = null;
            dataGrid1.DataSource = dataT;
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGrid1
            // 
            this.dataGrid1.DataMember = "";
            this.dataGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGrid1.Location = new System.Drawing.Point(0, 0);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.ReadOnly = true;
            this.dataGrid1.Size = new System.Drawing.Size(584, 584);
            this.dataGrid1.TabIndex = 0;
            // 
            // ucDataGrid
            // 
            this.Controls.Add(this.dataGrid1);
            this.Name = "ucDataGrid";
            this.Size = new System.Drawing.Size(584, 584);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        public DataTable SetDataSource
        {
            set
            {
                dataGrid1.DataSource = null;
                dataGrid1.DataSource = value;
            }
            get { return ((DataTable)dataGrid1.DataSource); }
        }

        //public DataGridViewSelectedRowCollection
    }
}