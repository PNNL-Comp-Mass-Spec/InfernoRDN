namespace DAnTE.Purgatorio
{
    public class clsWilcoxonPar
    {
        private string mRCmd;
        public string RDataset;

        [Tools.clsAnalysisAttribute("Source_DataTable", "Wilcoxon_Test")]
        public string mstrDatasetName;

        [Tools.clsAnalysisAttribute("Minimum_Datapoints_Needed", "Wilcoxon_Test")]
        public int numDatapts;

        [Tools.clsAnalysisAttribute("Selected_Factor", "Wilcoxon_Test")]
        public string selectedFactor;

        public clsWilcoxonPar()
        {
            RDataset = "Eset";
            numDatapts = 3;
            selectedFactor = "";
        }

        public string RCommand
        {
            get
            {
                mRCmd = "wilcoxtest <- DoNonPara(" + RDataset + @",FixedEffects=""" + selectedFactor +
                       @""",thres=" + numDatapts + @",testType=""Wilcox"")";
                return mRCmd;
            }
        }
    }
}