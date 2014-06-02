using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using DAnTE.Properties;

namespace DAnTE.Purgatorio
{
    public class clsWilcoxonPar
    {
        private string rcmd;
        public string Rdataset;
        [DAnTE.Tools.clsAnalysisAttribute("Source_DataTable", "Wilcoxon_Test")]
        public string mstrDatasetName;
        public string tempFile;
        [DAnTE.Tools.clsAnalysisAttribute("Minimum_Datapoints_Needed", "Wilcoxon_Test")]
        public int numDatapts;
        [DAnTE.Tools.clsAnalysisAttribute("Selected_Factor", "Wilcoxon_Test")]
        public string selectedFactor;
        public int nF;
        public ArrayList marrFactors;

        public clsWilcoxonPar()
        {
            Rdataset = "Eset";
            tempFile = "C:/";
            numDatapts = 3;
            selectedFactor = "";
            nF = 0; 
        }

        public string Rcmd
        {
            get
            {
                rcmd = "wilcoxtest <- DoNonPara(" + Rdataset + @",FixedEffects=""" + selectedFactor +
                    @""",thres=" + numDatapts + @",testType=""Wilcox"")";
                return rcmd;
            }
        }
    }
}
