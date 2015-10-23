using System;
using System.Collections;
using System.Collections.Generic;

namespace DAnTE.Tools
{
	/// <summary>
	/// Summary description for clsFactorInfo.
	/// </summary>
	public class clsFactorInfo : ICloneable
	{
		public string mstrFactor;
		public List<string> marrValues;


		public clsFactorInfo()
		{
			//
			// TODO: Add constructor logic here
			//
			mstrFactor = null;
            marrValues = new List<string>();
		}

        public clsFactorInfo(string factor)
        {
            mstrFactor = factor;
            marrValues = new List<string>();
        }

        private List<string> MakeDeepCopy(List<string> sourceList)
        {
            var newList = new List<string>();
            foreach (var item in sourceList)
            {
                newList.Add(string.Copy(item));
            }

            return newList;
        }

        #region ICloneable Members

        public object Clone()
        {
            var factorInfo = new clsFactorInfo(mstrFactor)
            {
                marrValues = MakeDeepCopy(marrValues)
            };

            return factorInfo;
        }

        #endregion

        #region Properties
        public string[] FactorValues
		{
			get
			{
				var values = new string[marrValues.Count];
				if (marrValues.Count == 0)
					return null;
			    
                for (var i = 0; i < marrValues.Count; i++)
			        values[i] = marrValues[i];
			    
                return values;
			}
		}

		public int vCount
		{
			get {return marrValues.Count;}
        }

        public List<string> SetFvals
        {
            set
            {
                marrValues = MakeDeepCopy(value);
            }
        }

        #endregion
    }
}
