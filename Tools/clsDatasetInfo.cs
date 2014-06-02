using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace DAnTE.Tools
{
    public class clsDatasetInfo : ICloneable
    {
        public string mstrDataSetName;
        public ArrayList marrFactorAssnmnts; // Each element is of type class Factor (see below)
        public bool factorsSET = false;

        public clsDatasetInfo()
        {
            mstrDataSetName = null;
            marrFactorAssnmnts = new ArrayList();
        }

        public clsDatasetInfo(string Name)
        {
            mstrDataSetName = Name;
            marrFactorAssnmnts = new ArrayList();
        }

        #region ICloneable Members

        public object Clone()
        {
            clsDatasetInfo dataset = new clsDatasetInfo(mstrDataSetName);
            dataset.marrFactorAssnmnts = (ArrayList)this.marrFactorAssnmnts.Clone();
            return dataset;
        }

        #endregion
    }
    
    
    /// <summary>
    /// Factor class
    /// </summary>
    public class Factor
    {
        private string name, val;

        public Factor(string Name, string Value)
        {
            name = Name;
            val = Value;
        }
        
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public string Value
        {
            get
            {
                return val;
            }
            set
            {
                val = value;
            }
        }
    }
}
