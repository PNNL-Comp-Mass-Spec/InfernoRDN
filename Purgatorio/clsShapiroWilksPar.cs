namespace DAnTE.Purgatorio
{
    public class clsShapiroWilksPar
    {
        private string mRCmd;
        public string RDataset;

        [Tools.clsAnalysisAttribute("Source_DataTable", "Shapiro-Wilks_Test")]
        public string mstrDatasetName;

        [Tools.clsAnalysisAttribute("Minimum_Datapoints_Needed", "Shapiro-Wilks_Test")]
        public int numDatapts;

        public clsShapiroWilksPar()
        {
            RDataset = "Eset";
            numDatapts = 3;
        }

        public string RCommand
        {
            get
            {
                mRCmd = "swtest <- testShapiroWilks(" + RDataset + ",thres=" + numDatapts + ")";
                return mRCmd;
            }
        }
    }
}