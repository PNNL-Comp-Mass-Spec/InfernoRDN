using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using DAnTE.Properties;

namespace DAnTE.Purgatorio
{
    public class clsLoessPar
    {
        private string rcmd;
        private string mstrOutFolder;

        [DAnTE.Tools.clsAnalysisAttribute("Span", "LOESS")]
        public string span;

        //[DAnTE.Tools.clsAnalysisAttribute("Dataset(R)", "LOESS")]
        public string Rdataset;
        
        private string mstrDatasetName;
        public bool mblPlot;
        private string mstrFactor;
        public int mintFactorIndex;

        private string mstrReference;
        public ArrayList marrFactors;

        public clsLoessPar()
        {
            mstrDatasetName = "Eset";
            mstrFactor = "";
            mintFactorIndex = 1;
            mblPlot = false;
            mstrReference = "LeastMissing";
            mstrOutFolder = @"C:\";
            span = "span=0.2";
        }

        public string Rcmd
        {
            get
            {
                rcmd = "loessData <- loess_normalize(" + Rdataset + "," + this.Factors +
                            "," + this.Span + "," + this.PlotFlag + "," + this.Reference + "," + this.OutFolder + ")";
                return rcmd;
            }
        }

        private string Span
        {
            get
            {
                return "span=" + span;
            }
        }

        private string OutFolder
        {
            get
            {
                return "folder=\"" + mstrOutFolder.Replace("\\", "/") + "/\""; ;
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

        [DAnTE.Tools.clsAnalysisAttribute("Source_DataTable", "LOESS")]
        public string DataSetName
        {
            get { return mstrDatasetName; }
            set { mstrDatasetName = value; }
        }

        private string Factors
        {
            get { return "factors[" + mintFactorIndex.ToString() + ",]"; }
        }

        [DAnTE.Tools.clsAnalysisAttribute("Selected_Factor", "LOESS")]
        public string FactorSelected
        {
            set { mstrFactor = value; }
            get { return mstrFactor; }
        }

        private string Reference
        {
            get
            {
                if (mstrReference.Equals("FirstDataset"))
                    return "reference=1";
                else if (mstrReference.Equals("MedianData"))
                    return "reference=2";
                else if (mstrReference.Equals("LeastMissing"))
                    return "reference=3";
                else
                    return "reference=3";
            }
        }

        [DAnTE.Tools.clsAnalysisAttribute("Baseline_Criteria", "LOESS")]
        public string Reference_pub
        {
            get { return mstrReference; }
            set { mstrReference = value; }
        }

        [DAnTE.Tools.clsAnalysisAttribute("Save_Diagnostic_Images_Folder", "LOESS")]
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

        [DAnTE.Tools.clsAnalysisAttribute("Save_Plots", "LOESS")]
        public string DoPlot
        {
            get
            {
                if (mblPlot)
                    return "Yes";
                else
                    return "No";
            }
        }
    }
}
