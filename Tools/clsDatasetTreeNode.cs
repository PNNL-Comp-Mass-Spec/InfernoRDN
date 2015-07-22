using System.Windows.Forms;
using System.Data;

namespace DAnTE.Tools
{
    public class clsDatasetTreeNode
    {
        public DataTable mDTable;
        public TreeNode mTNode;
        public string mstrParentNode;
        public string mstrDataText;
        public string mstrMessage;
        public string mstrRdatasetName;
        public string mstrRProtDatasetName;
        public bool mblIsNumeric;
        public bool mblIsPlottable;
        public bool mblRollupPossible;
        public bool mblAddTVCtxtMnu;
        public bool mblAddDGridCtxtMnu;

        public clsDatasetTreeNode()
        {
            mstrParentNode = "DAnTE";
            mstrRProtDatasetName = "";
            mblIsNumeric = true;
            mblIsPlottable = true;
            mblRollupPossible = true;
            mblAddTVCtxtMnu = false;
            mblAddDGridCtxtMnu = false;
        }
    }
}
