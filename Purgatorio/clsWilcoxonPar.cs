using System.Collections.Generic;

namespace DAnTE.Purgatorio
{
    public class clsWilcoxonPar
    {
        private string rcmd;
        public string Rdataset;
        [Tools.clsAnalysisAttribute("Source_DataTable", "Wilcoxon_Test")] public string mstrDatasetName;
        public string tempFile;
        [Tools.clsAnalysisAttribute("Minimum_Datapoints_Needed", "Wilcoxon_Test")] public int numDatapts;
        [Tools.clsAnalysisAttribute("Selected_Factor", "Wilcoxon_Test")] public string selectedFactor;
        public int nF;
        public List<string> marrFactors;

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