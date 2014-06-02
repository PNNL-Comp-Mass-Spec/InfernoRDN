using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using DAnTE.Properties;

namespace DAnTE.Purgatorio
{
    public class clsQRollupPar
    {
        private string rcmd;
        private string mstrOutFolder = null;

        //[DAnTE.Tools.clsAnalysisAttribute("Dataset(R)", "QRollup")]
        public string Rdataset;
        
        private string mstrDatasetName;
        [DAnTE.Tools.clsAnalysisAttribute("Minimum_Presence", "QRollup")]
        public string mstrMinPresence;
        [DAnTE.Tools.clsAnalysisAttribute("Include_OneHitWonders", "QRollup")]
        public bool mblOneHits;
        
        public bool mblModeMean;
        [DAnTE.Tools.clsAnalysisAttribute("Top_percentage", "QRollup")]
        public string mstrTop;

        [DAnTE.Tools.clsAnalysisAttribute("Top_Number_of_peptides", "QRollup")]
        public string mstrTopN;

        [DAnTE.Tools.clsAnalysisAttribute("Use_Top_Number_of_peptides", "QRollup")]
        public bool mblUseTopN;

        public clsQRollupPar()
        {
            mstrDatasetName = "Eset";
        }

        public string Rcmd
        {
            get
            {
                rcmd = "qrollupP <- QRollup.proteins(" + Rdataset + ",ProtInfo,minPresence=" + mstrMinPresence + "," +
                                    this.TopPercn + "," + this.TopN + "," + Mode + "," + OneHitWonders + ")";
                return rcmd;
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

        [DAnTE.Tools.clsAnalysisAttribute("Rollup_As", "QRollup")]
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

        private string OutFolder
        {
            get
            {
                return "outfolder=\"" + mstrOutFolder.Replace("\\", "/") + "/\""; ;
            }
        }

        [DAnTE.Tools.clsAnalysisAttribute("Source_DataTable", "QRollup")]
        public string DataSetName
        {
            get { return mstrDatasetName; }
            set { mstrDatasetName = value; }
        }

    }
}
