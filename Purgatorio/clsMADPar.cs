namespace DAnTE.Purgatorio
{
    public class clsMADPar
    {
        private string mRCmd;

        //[Tools.clsAnalysisAttribute("Dataset(R)", "MedianAbsoluteDeviation_Adjustment")]
        public string Rdataset;

        [Tools.clsAnalysisAttribute("Set_Zero_Mean", "MedianAbsoluteDeviation_Adjustment")] public bool mblMeanAdj;

        public int mintFactorIndex;

        public clsMADPar()
        {
            DataSetName = "Eset";
            FactorSelected = "";
            mintFactorIndex = 1;
            mblMeanAdj = false;
        }

        public string RCommand
        {
            get
            {
                mRCmd = "madEset <- adjustMAD(" + Rdataset + "," + this.Factor + "," + this.MeanAdj + ")";
                return mRCmd;
            }
        }

        [Tools.clsAnalysisAttribute("Source_DataTable", "MedianAbsoluteDeviation_Adjustment")]
        public string DataSetName { get; set; }

        private string Factor
        {
            get
            {
                if (mintFactorIndex > -1)
                    return "Factor=factors[" + mintFactorIndex.ToString() + ",]";
                else
                    return "Factor=1";
            }
        }

        private string MeanAdj
        {
            get
            {
                if (mblMeanAdj)
                    return "meanadjust=TRUE";
                else
                    return "meanadjust=FALSE";
            }
        }

        [Tools.clsAnalysisAttribute("Selected_Factor", "MedianAbsoluteDeviation_Adjustment")]
        public string FactorSelected { set; get; }
    }
}