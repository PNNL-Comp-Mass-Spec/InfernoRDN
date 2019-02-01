namespace DAnTE.Purgatorio
{
    public class clsQnormPar
    {
        private string mRCmd;

        //[Tools.clsAnalysisAttribute("Dataset(R)", "Quantile_Normalization")]
        public string Rdataset;

        public clsQnormPar()
        {
            DataSetName = "Eset";
        }

        public string RCommand
        {
            get
            {
                mRCmd = "quaNormEset <- quantileN(" + Rdataset + ")";
                return mRCmd;
            }
        }

        [Tools.clsAnalysisAttribute("Source_DataTable", "Quantile_Normalization")]
        public string DataSetName { get; set; }
    }
}