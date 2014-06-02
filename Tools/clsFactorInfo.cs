using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace DAnTE.Tools
{
	/// <summary>
	/// Summary description for clsFactorInfo.
	/// </summary>
	public class clsFactorInfo : ICloneable
	{
		public string mstrFactor;
		public ArrayList marrValues;


		public clsFactorInfo()
		{
			//
			// TODO: Add constructor logic here
			//
			mstrFactor = null;
            marrValues = new ArrayList();
		}

        public clsFactorInfo(string factor)
        {
            mstrFactor = factor;
            marrValues = new ArrayList();
        }

        private ArrayList MakeDeepCopy(ArrayList marrIN) //Deep copy arraylists of classes
        {
            ArrayList copyTo = new ArrayList();
            foreach (object obj in marrIN)
            {
                copyTo.Add(((ICloneable)obj).Clone());
            }
            return copyTo;
        }

        #region ICloneable Members

        public object Clone()
        {
            clsFactorInfo factorInfo = new clsFactorInfo(mstrFactor);
            factorInfo.marrValues = (ArrayList)this.marrValues.Clone();
            return factorInfo;
        }

        #endregion

        #region Properties
        public string[] FactorValues
		{
			get
			{
				string [] values = new string[marrValues.Count];
				if (marrValues.Count == 0)
					return null;
				else
				{
					for (int i = 0; i < marrValues.Count; i++)
						values[i] = marrValues[i].ToString();
					return values;
				}
			}
		}

		public int vCount
		{
			get {return marrValues.Count;}
        }

        public ArrayList SetFvals
        {
            set
            {
                marrValues = MakeDeepCopy(value);
            }
        }

        #endregion
    }
}
