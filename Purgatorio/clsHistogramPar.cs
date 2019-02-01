using System.Collections.Generic;
using DAnTE.Properties;

namespace DAnTE.Purgatorio
{
    public class clsHistogramPar
    {
        /// <summary>
        /// Sturges' formula is used to auto-define the bin ranges used in a histogram
        /// </summary>
        private const string BIN_METHOD_STURGES = "Sturges";

        private string mRCmd;
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
            mRCmd = "";
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

        public string RCommand
        {
            get
            {
                mRCmd = "plot_hist(" + Rdataset + "[," + datasubset + "],ncols=" + ncolumns.ToString() + ",";
                mRCmd = mRCmd + @"colF=""" + Fcol + @"""," + @"colB=""" + Bcol + @"""," + bkground + ",";
                mRCmd = mRCmd + addrug + "," + Bins + "," + Stamp + @",file=""" + tempFile + @""")";
                return mRCmd;
            }
        }

        public string Stamp
        {
            get
            {
                if (stamp)
                    return @"stamp=""" + Settings.Default.DataFileName.Replace("\\", "\\\\") + @"""";
                else
                    return "stamp=NULL";
            }
        }
    }
}