namespace DAnTE.Purgatorio
{
    public class clsQRollupPar
    {
        private string mRCmd;
        private readonly string mstrOutFolder = null;

        //[Tools.clsAnalysisAttribute("Dataset(R)", "QRollup")]
        public string RDataset;

        [Tools.clsAnalysisAttribute("Minimum_Presence", "QRollup")]
        public string mstrMinPresence;

        [Tools.clsAnalysisAttribute("Include_OneHitWonders", "QRollup")]
        public bool mblOneHits;

        public bool mblModeMean;

        [Tools.clsAnalysisAttribute("Top_percentage", "QRollup")]
        public string mstrTop;

        [Tools.clsAnalysisAttribute("Top_Number_of_peptides", "QRollup")]
        public string mstrTopN;

        [Tools.clsAnalysisAttribute("Use_Top_Number_of_peptides", "QRollup")]
        public bool mblUseTopN;

        public clsQRollupPar()
        {
            DataSetName = "Eset";
        }

        public string RCommand
        {
            get
            {
                mRCmd = "qrollupP <- QRollup.proteins(" + RDataset + ",ProtInfo,minPresence=" + mstrMinPresence + "," +
                       TopPercn + "," + TopN + "," + Mode + "," + OneHitWonders + ")";
                return mRCmd;
            }
        }

        private string TopPercn
        {
            get
            {
                if (mblUseTopN)
                    mstrTop = "0";
                return "Top=" + mstrTop;
            }
        }

        private string TopN
        {
            get
            {
                if (!mblUseTopN)
                    mstrTopN = "0";
                return "topN=" + mstrTopN;
            }
        }

        private string Mode
        {
            get
            {
                if (mblModeMean)
                    return "Mean=TRUE";
                else
                    return "Mean=FALSE";
            }
        }

        [Tools.clsAnalysisAttribute("Rollup_As", "QRollup")]
        public string RollupMode
        {
            get
            {
                if (mblModeMean)
                    return "Mean";
                else
                    return "Median";
            }
        }

        private string OneHitWonders
        {
            get
            {
                if (mblOneHits)
                    return "oneHitWonders=TRUE";
                else
                    return "oneHitWonders=FALSE";
            }
        }

        private string OutFolder => "outfolder=\"" + mstrOutFolder.Replace("\\", "/") + "/\"";

        [Tools.clsAnalysisAttribute("Source_DataTable", "QRollup")]
        public string DataSetName { get; set; }
    }
}