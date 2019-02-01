using System.Collections.Generic;

namespace DAnTE.Purgatorio
{
    public class clsLinRegrnPar
    {
        private string mRCmd;
        private string mstrOutFolder;

        //[Tools.clsAnalysisAttribute("Dataset(R)", "LinearRegression")]
        public string Rdataset;

        public bool mblPlot;
        public int mintFactorIndex;

        [Tools.clsAnalysisAttribute("Reference_Criteria", "LinearRegression")] private string mstrReference;
        public List<string> marrFactors;

        public clsLinRegrnPar()
        {
            DataSetName = "Eset";
            FactorSelected = "";
            mintFactorIndex = 1;
            mblPlot = false;
            mstrReference = "LeastMissing";
            mstrOutFolder = @"C:\";
        }

        public string RCommand
        {
            get
            {
                mRCmd = "linregData <- LinReg_normalize(" + Rdataset + "," + this.Factors +
                       "," + this.PlotFlag + "," + this.Reference + "," + this.OutFolder + ")";
                return mRCmd;
            }
        }

        private string OutFolder => "folder=\"" + mstrOutFolder.Replace("\\", "/") + "/\"";

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

        [Tools.clsAnalysisAttribute("Source_DataTable", "LinearRegression")]
        public string DataSetName { get; set; }

        private string Factors => "factors[" + mintFactorIndex.ToString() + ",]";

        [Tools.clsAnalysisAttribute("Selected_Factor", "LinearRegression")]
        public string FactorSelected { set; get; }

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

        [Tools.clsAnalysisAttribute("Baseline_Criteria", "LinearRegression")]
        public string Reference_pub
        {
            get => mstrReference;
            set => mstrReference = value;
        }

        [Tools.clsAnalysisAttribute("Save_Diagnostic_Images_Folder", "LinearRegression")]
        public string OutFolder_pub
        {
            get => mstrOutFolder;
            set => mstrOutFolder = value;
        }

        [Tools.clsAnalysisAttribute("Save_Plots", "LinearRegression")]
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