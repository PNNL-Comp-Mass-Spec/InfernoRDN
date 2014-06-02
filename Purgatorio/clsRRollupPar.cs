using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using DAnTE.Properties;

namespace DAnTE.Purgatorio
{
    public class clsRRollupPar
    {
        private string rcmd;
        private string mstrOutFolder;

        //[DAnTE.Tools.clsAnalysisAttribute("Dataset(R)", "RRollup")]
        public string Rdataset;
        
        private string mstrDatasetName;
        [DAnTE.Tools.clsAnalysisAttribute("PlotResults", "RRollup")]
        public bool mblPlot;
        [DAnTE.Tools.clsAnalysisAttribute("Minimum_Presence", "RRollup")]
        public string mstrMinPresence;
        [DAnTE.Tools.clsAnalysisAttribute("Include_OneHitWonders", "RRollup")]
        public bool mblOneHits;
        [DAnTE.Tools.clsAnalysisAttribute("Exclude_threshold", "RRollup")]
        public string mstrOverlap;
        [DAnTE.Tools.clsAnalysisAttribute("MinRequired_for_GrubbsTest", "RRollup")]
        public string mstrGrubsNum;
        [DAnTE.Tools.clsAnalysisAttribute("p_value_for_GrubbsTest", "RRollup")]
        public string mstrGrubsP;
        [DAnTE.Tools.clsAnalysisAttribute("Rollup_as_Mean", "RRollup")]
        public bool mblModeMean;
        [DAnTE.Tools.clsAnalysisAttribute("MeanCenter", "RRollup")]
        public bool mblDoCentering;

        public clsRRollupPar()
        {
            mstrDatasetName = "Eset";
            mblPlot = false;
            mstrOutFolder = @"C:\";
        }

        public string Rcmd
        {
            get
            {
                rcmd = "pScaled1 <- RRollup.proteins(" + Rdataset + ",ProtInfo,minPresence=" + mstrMinPresence +
                                    "," + this.OneHitWonders + "," + this.Mode + ",minOverlap=" + mstrOverlap + ",gpvalue=" + mstrGrubsP +
                                    ",gminPCount=" + mstrGrubsNum + "," + this.OutFolder + "," +
                                    this.PlotFlag + "," + this.MeanCenter + ")";
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
                return "outfolder=\"" + mstrOutFolder.Replace("\\", "/") + "/\""; ;
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

        [DAnTE.Tools.clsAnalysisAttribute("Source_DataTable", "RRollup")]
        public string DataSetName
        {
            get { return mstrDatasetName; }
            set { mstrDatasetName = value; }
        }

        [DAnTE.Tools.clsAnalysisAttribute("Results_Folder", "RRollup")]
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
