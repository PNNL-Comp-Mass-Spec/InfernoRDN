using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using DAnTE.Properties;

namespace DAnTE.Purgatorio
{
    public class clsQnormPar
    {
        private string rcmd;

        //[DAnTE.Tools.clsAnalysisAttribute("Dataset(R)", "Quantile_Normalization")]
        public string Rdataset;
        private string mstrDatasetName;

        public clsQnormPar()
        {
            mstrDatasetName = "Eset";
        }

        public string Rcmd
        {
            get
            {
                rcmd = "quaNormEset <- quantileN(" + Rdataset + ")";
                return rcmd;
            }
        }
        
        [DAnTE.Tools.clsAnalysisAttribute("Source_DataTable", "Quantile_Normalization")]
        public string DataSetName
        {
            get { return mstrDatasetName; }
            set { mstrDatasetName = value; }
        }
                
    }
}
