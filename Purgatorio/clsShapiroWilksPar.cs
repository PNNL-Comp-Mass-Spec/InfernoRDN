namespace DAnTE.Purgatorio
{
    public class clsShapiroWilksPar
    {
        private string mRCmd;
        public string Rdataset;
        [Tools.clsAnalysisAttribute("Source_DataTable", "Shapiro-Wilks_Test")] public string mstrDatasetName;
        [Tools.clsAnalysisAttribute("Minimum_Datapoints_Needed", "Shapiro-Wilks_Test")] public int numDatapts;

        public clsShapiroWilksPar()
        {
            Rdataset = "Eset";
            numDatapts = 3;
        }

        public string RCommand
        {
            get
            {
                mRCmd = "swtest <- testShapiroWilks(" + Rdataset + ",thres=" + numDatapts + ")";
                return mRCmd;
            }
        }
    }
}