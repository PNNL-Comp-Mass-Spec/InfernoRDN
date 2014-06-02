using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using DAnTE.Properties;

namespace DAnTE.Purgatorio
{
    public class clsPatternSearchPar
    {
        private string rcmd;

        //[DAnTE.Tools.clsAnalysisAttribute("Dataset(R)", "PatternSearch")]
        public string Rdataset;
        
        [DAnTE.Tools.clsAnalysisAttribute("Number_of_Patterns", "PatternSearch")]
        public int nPatterns;
        public Hashtable mhtVectorPatterns;
        public string mstrDatasetName;
        public ArrayList Datasets = new ArrayList();

        public clsPatternSearchPar()
        {
            mstrDatasetName = "Eset";
            nPatterns = 2;
        }

        public string Rcmd
        {
            get
            {
                rcmd = "patternData <- patternSearch(" + Rdataset + "," + this.Patterns + ")";
                return rcmd;
            }
        }

        [DAnTE.Tools.clsAnalysisAttribute("Source_DataTable", "PatternSearch")]
        public string DataSetName
        {
            get { return mstrDatasetName; }
            set { mstrDatasetName = value; }
        }

        private string Patterns
        {
            get
            {
                int nDatasets = Datasets.Count;
                string mstrPattern = "c(";
                ArrayList marrPattern = new ArrayList();
                IDictionaryEnumerator en = mhtVectorPatterns.GetEnumerator();
                while (en.MoveNext())
                {
                    marrPattern = (ArrayList)en.Value;
                    for (int i = 0; i < marrPattern.Count; i++)
                    {
                        mstrPattern = mstrPattern + marrPattern[i].ToString() + ",";
                    }
                }
                mstrPattern = mstrPattern.Substring(0, mstrPattern.Length - 1) + ")";
                mstrPattern = "matrix(" + mstrPattern + "," + nDatasets + "," + nPatterns + ")";
                return mstrPattern;
            }
        }
       
    }
}
