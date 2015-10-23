namespace DAnTE.Purgatorio
{
    public class clsCentralTendencyPar
    {
        private string rcmd;

        //[Tools.clsAnalysisAttribute("Dataset(R)", "CentralTendency")]
        public string Rdataset;

        public bool mblUseMeanTend;
        [Tools.clsAnalysisAttribute("Zero_Center_Data", "CentralTendency")]
        public bool mblCenterZero;
        public string mstrmethod;

        public clsCentralTendencyPar()
        {
            DataSetName = "Eset";
            mblUseMeanTend = true;
            mblCenterZero = true;
            mstrmethod = "MeanCenter.Sub";
        }

        public string Rcmd
        {
            get
            {
                if (mblUseMeanTend)
                    rcmd = "meanCEset <- " + mstrmethod + "(" + Rdataset + "," + this.UseTendency + "," + this.ZeroCenter + ")";
                else
                    rcmd = "medianCEset <- " + mstrmethod + "(" + Rdataset + "," + this.UseTendency + "," + this.ZeroCenter + ")";
                return rcmd;
            }
        }

        [Tools.clsAnalysisAttribute("Adjustment_Method", "CentralTendency")]
        public string AdjustmentMethod
        {
            get
            {
                if (mstrmethod.Equals("MeanCenter.Sub"))
                    return "Subtract";
                else
                    return "Divide";
            }
        }

        [Tools.clsAnalysisAttribute("Tendency", "CentralTendency")]
        public string Tendency
        {
            get
            {
                if (mblUseMeanTend)
                    return "Mean";
                else
                    return "Median";
            }
        }

        private string UseTendency
        {
            get
            {
                if (mblUseMeanTend)
                    return "Mean=TRUE";
                else
                    return "Mean=FALSE";
            }
        }

        private string ZeroCenter
        {
            get
            {
                if (mblCenterZero)
                    return "centerZero=TRUE";
                else
                    return "centerZero=FALSE";
            }
        }

        [Tools.clsAnalysisAttribute("Source_DataTable", "CentralTendency")]
        public string DataSetName { get; set; }
    }
}
