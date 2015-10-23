namespace DAnTE.Purgatorio
{
    public class clsShapiroWilksPar
    {
        private string rcmd;
        public string Rdataset;
        [Tools.clsAnalysisAttribute("Source_DataTable", "Shapiro-Wilks_Test")]
        public string mstrDatasetName;
        [Tools.clsAnalysisAttribute("Minimum_Datapoints_Needed", "Shapiro-Wilks_Test")]
        public int numDatapts;

        public clsShapiroWilksPar()
        {
            Rdataset = "Eset";
            numDatapts = 3;
        }

        public string Rcmd
        {
            get
            {
                rcmd = "swtest <- testShapiroWilks(" + Rdataset + ",thres=" + numDatapts + ")";
                return rcmd;
            }
        }
    }
}
