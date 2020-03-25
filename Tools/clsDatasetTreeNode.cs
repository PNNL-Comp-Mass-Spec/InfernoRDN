using System.Windows.Forms;
using System.Data;

namespace DAnTE.Tools
{
    public class clsDatasetTreeNode
    {
        public readonly DataTable mDTable;
        public TreeNode TNode { get; set; }
        public string ParentNode { get; set; }
        public string DataText { get; set; }
        public string Message { get; set; }
        public string RDatasetName { get; set; }
        public string RProteinDatasetName { get; set; }
        public bool IsNumeric { get; set; }
        public bool IsPlotTable { get; set; }
        public bool RollupPossible { get; set; }
        public bool AddTVContextMenu { get; set; }
        public bool AddDGridContextMenu { get; set; }

        public clsDatasetTreeNode(DataTable dt)
        {
            mDTable = dt;
            ParentNode = "DAnTE";
            RProteinDatasetName = "";
            IsNumeric = true;
            IsPlotTable = true;
            RollupPossible = true;
            AddTVContextMenu = false;
            AddDGridContextMenu = false;
        }
    }
}