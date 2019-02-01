namespace DAnTE.Purgatorio
{
    public class clsOneSampleTtestPar
    {
        private string mRCmd;
        public string Rdataset;

        [Tools.clsAnalysisAttribute("Source_DataTable", "OneSample_T_Test")] public string mstrDatasetName;
        [Tools.clsAnalysisAttribute("Minimum_Datapoints_Needed", "OneSample_T_Test")] public int numDatapts;

        public clsOneSampleTtestPar()
        {
            Rdataset = "Eset";
            numDatapts = 3;
        }

        public string RCommand
        {
            get
            {
                mRCmd = "ttest <- Ttest(" + Rdataset + ",thres=" + numDatapts + ")";
                return mRCmd;
            }
        }
    }
}