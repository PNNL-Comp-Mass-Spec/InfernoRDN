using System.Collections.Generic;
using DAnTE.Properties;

namespace DAnTE.Purgatorio
{
    public class clsHistogramPar
    {
        private const string BIN_METHOD_STURGES = "Sturges";

        private string rcmd;
        public string Fcol;
        public string Bcol;
        public string bkground;
        public string addrug;
        public string Rdataset;
        public string mstrDatasetName;
        public string datasubset;
        public int ncolumns;
        public string Bins;
        public int numBins;
        public bool autoBins;
        public bool stamp;
        public string tempFile;
        public List<string> Datasets = new List<string>();
        public List<string> CheckedDatasets = new List<string>();

        public clsHistogramPar()
        {
            ncolumns = 2;

            // Set the method for auto-computing histogram bins to use Sturges' formula:
            // cells="Sturges"
            Bins = GetHistogramBinMethodCode();

            numBins = 11;
            autoBins = true;
            rcmd = "";
            datasubset = null;
            
            // When true, add tick marks
            addrug = "addRug=TRUE";

            Fcol = Settings.Default.histFore;
            Bcol = Settings.Default.histBrdr;
            if (Fcol == "")
                Fcol = "#FFC38A";
            if (Bcol == "") 
                Bcol = "#5FAE27"; 
            bkground = "bkground=\"white\"";
            Rdataset = "";
            mstrDatasetName = "";
            tempFile = "C:/_temp.png";
            stamp = false;
        }

        /// <summary>
        /// Return the code string that instructs R how to bin the data when creating a histogram
        /// </summary>
        /// <param name="binMethod"></param>
        /// <returns></returns>
        public static string GetHistogramBinMethodCode(string binMethod = BIN_METHOD_STURGES)
        {
            return "cells=" + '"' + binMethod + '"';
        }

        public string Rcmd
        {
            get
            {
                rcmd = "plot_hist(" + Rdataset + "[," + datasubset + "],ncols=" + ncolumns.ToString() + ",";
                rcmd = rcmd + @"colF=""" + Fcol + @"""," + @"colB=""" + Bcol + @"""," + bkground + ",";
                rcmd = rcmd + addrug + "," + Bins + "," + Stamp + @",file=""" + tempFile + @""")";
                return rcmd;
            }
        }

        public string Stamp
        {
            get
            {
                if (stamp)
                    return @"stamp=""" + Settings.Default.DataFileName.Replace("\\","\\\\") + @"""";
                else
                    return "stamp=NULL";
            }
        }
    }
}
