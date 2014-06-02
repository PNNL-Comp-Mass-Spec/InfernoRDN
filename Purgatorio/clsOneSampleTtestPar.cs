using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using DAnTE.Properties;

namespace DAnTE.Purgatorio
{
    public class clsOneSampleTtestPar
    {
        private string rcmd;
        public string Rdataset;
        [DAnTE.Tools.clsAnalysisAttribute("Source_DataTable", "OneSample_T_Test")]
        public string mstrDatasetName;
        [DAnTE.Tools.clsAnalysisAttribute("Minimum_Datapoints_Needed", "OneSample_T_Test")]
        public int numDatapts;

        public clsOneSampleTtestPar()
        {
            Rdataset = "Eset";
            numDatapts = 3;
        }

        public string Rcmd
        {
            get
            {
                rcmd = "ttest <- Ttest(" + Rdataset + ",thres=" + numDatapts + ")";
                return rcmd;
            }
        }
    }
}
