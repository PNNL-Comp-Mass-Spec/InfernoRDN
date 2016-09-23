namespace DAnTE.Purgatorio
{
    public class clsZRollupPar
    {
        private string rcmd;
        private string mstrOutFolder;

        //[Tools.clsAnalysisAttribute("Dataset(R)", "ZRollup")]
        public string Rdataset;

        [Tools.clsAnalysisAttribute("PlotResults", "ZRollup")] public bool mblPlot;
        [Tools.clsAnalysisAttribute("Minimum_Presence", "ZRollup")] public string mstrMinPresence;
        [Tools.clsAnalysisAttribute("Include_OneHitWonders", "ZRollup")] public bool mblOneHits;
        [Tools.clsAnalysisAttribute("MinRequired_for_GrubbsTest", "ZRollup")] public string mstrGrubsNum;
        [Tools.clsAnalysisAttribute("p_value_for_GrubbsTest", "ZRollup")] public string mstrGrubsP;
        [Tools.clsAnalysisAttribute("Rollup_as_Mean", "ZRollup")] public bool mblModeMean;

        public clsZRollupPar()
        {
            DataSetName = "Eset";
            mblPlot = false;
            mstrOutFolder = @"C:\";
        }

        public string Rcmd
        {
            get
            {
                rcmd = "pScaled2 <- ZRollup.proteins(" + Rdataset + ",ProtInfo,minPresence=" + mstrMinPresence +
                       "," + this.Mode + ",gpvalue=" + mstrGrubsP + ",gminPCount=" + mstrGrubsNum + "," +
                       this.PlotFlag + "," + this.OutFolder + "," + this.OneHitWonders + ")";
                return rcmd;
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
            get { return "outfolder=\"" + mstrOutFolder.Replace("\\", "/") + "/\""; }
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

        [Tools.clsAnalysisAttribute("Source_DataTable", "ZRollup")]
        public string DataSetName { get; set; }

        [Tools.clsAnalysisAttribute("Results_Folder", "ZRollup")]
        public string OutFolder_pub
        {
            get { return mstrOutFolder; }
            set { mstrOutFolder = value; }
        }
    }
}