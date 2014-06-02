using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using DAnTE.Properties;

namespace DAnTE.Purgatorio
{
    public class clsHistogramPar
    {
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
        public ArrayList Datasets = new ArrayList();
        public ArrayList CheckedDatasets = new ArrayList();

        public clsHistogramPar()
        {
            ncolumns = 2;
            Bins = @"cells=""Sturges""";
            numBins = 11;
            autoBins = true;
            rcmd = "";
            datasubset = null;
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
