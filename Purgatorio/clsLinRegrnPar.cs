using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using DAnTE.Properties;

namespace DAnTE.Purgatorio
{
    public class clsLinRegrnPar
    {
        private string rcmd;
        private string mstrOutFolder;

        //[DAnTE.Tools.clsAnalysisAttribute("Dataset(R)", "LinearRegression")]
        public string Rdataset;
        
        private string mstrDatasetName;
        public bool mblPlot;
        private string mstrFactor;
        public int mintFactorIndex;

        [DAnTE.Tools.clsAnalysisAttribute("Reference_Criteria", "LinearRegression")]
        private string mstrReference;
        public ArrayList marrFactors;

        public clsLinRegrnPar()
        {
            mstrDatasetName = "Eset";
            mstrFactor = "";
            mintFactorIndex = 1;
            mblPlot = false;
            mstrReference = "LeastMissing";
            mstrOutFolder = @"C:\";
        }

        public string Rcmd
        {
            get
            {
                rcmd = "linregData <- LinReg_normalize(" + Rdataset + "," + this.Factors +
                            "," + this.PlotFlag + "," + this.Reference + "," + this.OutFolder + ")";
                return rcmd;
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

        [DAnTE.Tools.clsAnalysisAttribute("Source_DataTable", "LinearRegression")]
        public string DataSetName
        {
            get { return mstrDatasetName; }
            set { mstrDatasetName = value; }
        }

        private string Factors
        {
            get { return "factors[" + mintFactorIndex.ToString() + ",]"; }
        }

        [DAnTE.Tools.clsAnalysisAttribute("Selected_Factor", "LinearRegression")]
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

        [DAnTE.Tools.clsAnalysisAttribute("Baseline_Criteria", "LinearRegression")]
        public string Reference_pub
        {
            get { return mstrReference; }
            set { mstrReference = value; }
        }

        [DAnTE.Tools.clsAnalysisAttribute("Save_Diagnostic_Images_Folder", "LinearRegression")]
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

        [DAnTE.Tools.clsAnalysisAttribute("Save_Plots", "LinearRegression")]
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
