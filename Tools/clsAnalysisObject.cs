using System;
using System.Collections.Generic;
using System.Text;

namespace DAnTE.Tools
{
    public class clsAnalysisObject
    {
        private string _operation;
        private object _o;

        public clsAnalysisObject(string analysisStep, object o)
        {
            _operation = analysisStep;
            _o = o;
        }

        public string Operation
        {
            get { return _operation; }
        }

        public object AnalysisObject
        {
            get { return _o; }
        }
    }
}
