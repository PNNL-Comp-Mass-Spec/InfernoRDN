using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace DAnTE.Tools
{
    public class clsDatasetInfo : ICloneable
    {
        public string mstrDataSetName;
        public List<Factor> marrFactorAssnmnts; 
        public bool factorsSET = false;

        public clsDatasetInfo()
        {
            mstrDataSetName = null;
            marrFactorAssnmnts = new List<Factor>();
        }

        public clsDatasetInfo(string Name)
        {
            mstrDataSetName = Name;
            marrFactorAssnmnts = new List<Factor>();
        }

        #region ICloneable Members

        public object Clone()
        {
            var dataset = new clsDatasetInfo(mstrDataSetName);

            foreach (var item in marrFactorAssnmnts)
            {
                dataset.marrFactorAssnmnts.Add(new Factor(string.Copy(item.Name), string.Copy(item.Value)));
            }
            
            return dataset;
        }

        #endregion
    }
    
    
    /// <summary>
    /// Factor class
    /// </summary>
    public class Factor
    {
        public Factor(string Name, string Value)
        {
            this.Name = Name;
            this.Value = Value;
        }
        
        public string Name { get; set; }

        public string Value { get; set; }
    }
}
