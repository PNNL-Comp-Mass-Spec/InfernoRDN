namespace DAnTE.Purgatorio
{
    public class clsLoessPar
    {
        private string rcmd;
        private string mstrOutFolder;

        [Tools.clsAnalysisAttribute("Span", "LOESS")] public string span;

        //[Tools.clsAnalysisAttribute("Dataset(R)", "LOESS")]
        public string Rdataset;

        public bool mblPlot;
        public int mintFactorIndex;

        private string mstrReference;

        public clsLoessPar()
        {
            DataSetName = "Eset";
            FactorSelected = "";
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

        private string Span => "span=" + span;

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

        [Tools.clsAnalysisAttribute("Source_DataTable", "LOESS")]
        public string DataSetName { get; set; }

        private string Factors => "factors[" + mintFactorIndex.ToString() + ",]";

        [Tools.clsAnalysisAttribute("Selected_Factor", "LOESS")]
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

        [Tools.clsAnalysisAttribute("Baseline_Criteria", "LOESS")]
        public string Reference_pub
        {
            get => mstrReference;
            set => mstrReference = value;
        }

        [Tools.clsAnalysisAttribute("Save_Diagnostic_Images_Folder", "LOESS")]
        public string OutFolder_pub
        {
            get => mstrOutFolder;
            set => mstrOutFolder = value;
        }

        [Tools.clsAnalysisAttribute("Save_Plots", "LOESS")]
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