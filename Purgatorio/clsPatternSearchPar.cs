using System.Collections.Generic;

namespace DAnTE.Purgatorio
{
    public class clsPatternSearchPar
    {
        private string rcmd;

        //[Tools.clsAnalysisAttribute("Dataset(R)", "PatternSearch")]
        public string Rdataset;
        
        [Tools.clsAnalysisAttribute("Number_of_Patterns", "PatternSearch")]
        public int nPatterns;
        public Dictionary<string, List<double>> mhtVectorPatterns;
        public string mstrDatasetName;
        public List<string> Datasets = new List<string>();

        public clsPatternSearchPar()
        {
            mstrDatasetName = "Eset";
            nPatterns = 2;
        }

        public string Rcmd
        {
            get
            {
                rcmd = "patternData <- patternSearch(" + Rdataset + "," + Patterns + ")";
                return rcmd;
            }
        }

        [Tools.clsAnalysisAttribute("Source_DataTable", "PatternSearch")]
        public string DataSetName
        {
            get { return mstrDatasetName; }
            set { mstrDatasetName = value; }
        }

        private string Patterns
        {
            get
            {
                var nDatasets = Datasets.Count;
                var mstrPattern = "c(";

                foreach (var pattern in mhtVectorPatterns)
                {
                    var marrPattern = pattern.Value;
                    for (var i = 0; i < marrPattern.Count; i++)
                    {
                        mstrPattern = mstrPattern + marrPattern[i] + ",";
                    }
                }
                
                if (mstrPattern.Length < 3)
                {
                    // mhtVectorPatterns was empty; add a dummy data point
                    mstrPattern += "0,";
                }

                // Remove the trailing comma, then add the closing parenthesis
                mstrPattern = mstrPattern.Substring(0, mstrPattern.Length - 1) + ")";
                mstrPattern = "matrix(" + mstrPattern + "," + nDatasets + "," + nPatterns + ")";
                return mstrPattern;
            }
        }
       
    }
}
