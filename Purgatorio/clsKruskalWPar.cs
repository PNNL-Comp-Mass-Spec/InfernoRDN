using System.Collections.Generic;

namespace DAnTE.Purgatorio
{
    public class clsKruskalWPar
    {
        private string rcmd;
        //[Tools.clsAnalysisAttribute("Dataset(R)", "Kruskal-Walis Test")]
        public string Rdataset;
        [Tools.clsAnalysisAttribute("Source_DataTable", "Kruskal-Walis_Test")] public string mstrDatasetName;
        public string tempFile;
        [Tools.clsAnalysisAttribute("Minimum_Datapoints_Needed", "Kruskal-Walis_Test")] public int numDatapts;
        [Tools.clsAnalysisAttribute("Selected_Factor", "Kruskal-Walis_Test")] public string selectedFactor;
        public int nF;
        public List<string> marrFactors;

        public clsKruskalWPar()
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
                rcmd = "kwtest <- DoNonPara(" + Rdataset + @",FixedEffects=""" + selectedFactor +
                       @""",thres=" + numDatapts + @",testType=""KW"")";
                return rcmd;
            }
        }
    }
}