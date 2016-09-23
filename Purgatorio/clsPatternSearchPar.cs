using System.Collections.Generic;
using System.Text;

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
                var patternCommand = new StringBuilder();
                patternCommand.Append("c(");

                foreach (var pattern in mhtVectorPatterns)
                {
                    var patternValues = pattern.Value;
                    foreach (var value in patternValues)
                    {
                        patternCommand.Append(value + ",");
                    }
                }

                if (patternCommand.Length < 3)
                {
                    // mhtVectorPatterns was empty; add a dummy data point
                    patternCommand.Append("0,");
                }

                // Remove the trailing comma, then add the closing parenthesis
                if (patternCommand.Length > 0 && patternCommand[patternCommand.Length - 1] == ',')
                    patternCommand.Remove(patternCommand.Length - 1, 1);

                patternCommand.Append(")");

                return "matrix(" + patternCommand + "," + nDatasets + "," + nPatterns + ")";
            }
        }

    }
}
