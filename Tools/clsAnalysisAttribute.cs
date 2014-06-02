using System;
using System.Collections.Generic;
using System.Text;

namespace DAnTE.Tools
{
    [AttributeUsage(System.AttributeTargets.All, AllowMultiple = true)]
    public class clsAnalysisAttribute: System.Attribute
    {
        private string mstrDescription;
        private string mstrGroup;

        public clsAnalysisAttribute(string desc, string group)
        {
            mstrDescription = desc;
            mstrGroup = group;
        }

        public string Description
        {
            get { return mstrDescription; }
            set { mstrDescription = value; }
        }

        public string Group
        {
            get { return mstrGroup; }
            set { mstrGroup = value; }
        }
    }
}
