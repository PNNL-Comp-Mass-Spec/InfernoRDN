using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using DAnTE.Tools;
using DAnTE.ExtraControls;
using DAnTE.Properties;

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
