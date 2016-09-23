namespace DAnTE.Purgatorio
{
    public class clsWilcoxonPar
    {
        private string rcmd;
        public string Rdataset;
        [Tools.clsAnalysisAttribute("Source_DataTable", "Wilcoxon_Test")] public string mstrDatasetName;
        [Tools.clsAnalysisAttribute("Minimum_Datapoints_Needed", "Wilcoxon_Test")] public int numDatapts;
        [Tools.clsAnalysisAttribute("Selected_Factor", "Wilcoxon_Test")] public string selectedFactor;

        public clsWilcoxonPar()
        {
            Rdataset = "Eset";
            numDatapts = 3;
            selectedFactor = "";
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