namespace DAnTE.Purgatorio
{
    public class clsRRollupPar
    {
        private string rcmd;
        private string mstrOutFolder;

        //[Tools.clsAnalysisAttribute("Dataset(R)", "RRollup")]
        public string Rdataset;

        [Tools.clsAnalysisAttribute("PlotResults", "RRollup")]
        public bool mblPlot;
        [Tools.clsAnalysisAttribute("Minimum_Presence", "RRollup")]
        public string mstrMinPresence;
        [Tools.clsAnalysisAttribute("Include_OneHitWonders", "RRollup")]
        public bool mblOneHits;
        [Tools.clsAnalysisAttribute("Exclude_threshold", "RRollup")]
        public string mstrOverlap;
        [Tools.clsAnalysisAttribute("MinRequired_for_GrubbsTest", "RRollup")]
        public string mstrGrubsNum;
        [Tools.clsAnalysisAttribute("p_value_for_GrubbsTest", "RRollup")]
        public string mstrGrubsP;
        [Tools.clsAnalysisAttribute("Rollup_as_Mean", "RRollup")]
        public bool mblModeMean;
        [Tools.clsAnalysisAttribute("MeanCenter", "RRollup")]
        public bool mblDoCentering;

        public clsRRollupPar()
        {
            DataSetName = "Eset";
            mblPlot = false;
            mstrOutFolder = @"C:\";
        }

        public string Rcmd
        {
            get
            {
                rcmd = "pScaled1 <- RRollup.proteins(" + Rdataset + ",ProtInfo,minPresence=" + mstrMinPresence +
                                    "," + OneHitWonders + "," + Mode + ",minOverlap=" + mstrOverlap + ",gpvalue=" + mstrGrubsP +
                                    ",gminPCount=" + mstrGrubsNum + "," + OutFolder + "," +
                                    PlotFlag + "," + MeanCenter + ")";
                return rcmd;
            }
        }

        private string MeanCenter
        {
            get
            {
                if (mblDoCentering)
                    return "center=TRUE";
                else
                    return "center=FALSE";
            }
        }

        private string Mode
        {
            get
            {
                if (mblModeMean)
                    return @"Mode=""mean""";
                else
                    return @"Mode=""median""";
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

        private string OutFolder
        {
            get
            {
                return "outfolder=\"" + mstrOutFolder.Replace("\\", "/") + "/\"";
            }
        }

        private string PlotFlag
        {
            get
            {
                if (mblPlot)
                    return "plotflag=TRUE";
                else
                    return "plotflag=FALSE";
            }
        }

        [Tools.clsAnalysisAttribute("Source_DataTable", "RRollup")]
        public string DataSetName { get; set; }

        [Tools.clsAnalysisAttribute("Results_Folder", "RRollup")]
        public string OutFolder_pub
        {
            get
            {
                return mstrOutFolder;
            }
            set
            {
                mstrOutFolder = value;
            }
        }
    }
}
