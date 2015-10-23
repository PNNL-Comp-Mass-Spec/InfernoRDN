namespace DAnTE.Purgatorio
{
    public class clsQnormPar
    {
        private string rcmd;

        //[Tools.clsAnalysisAttribute("Dataset(R)", "Quantile_Normalization")]
        public string Rdataset;

        public clsQnormPar()
        {
            DataSetName = "Eset";
        }

        public string Rcmd
        {
            get
            {
                rcmd = "quaNormEset <- quantileN(" + Rdataset + ")";
                return rcmd;
            }
        }
        
        [Tools.clsAnalysisAttribute("Source_DataTable", "Quantile_Normalization")]
        public string DataSetName { get; set; }
    }
}
