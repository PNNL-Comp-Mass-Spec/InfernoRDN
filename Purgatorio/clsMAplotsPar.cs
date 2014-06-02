using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using DAnTE.Properties;

namespace DAnTE.Purgatorio
{
    public class clsMAplotsPar
    {
        private string rcmd;
        public string datasubset;
        public string dCol;
        public string lCol;
        public bool trBkground;
        public string Rdataset;
        public string mstrDatasetName;
        public string tempFile;
        public bool stamp;
        public ArrayList Datasets = new ArrayList();
        public ArrayList CheckedDatasets = new ArrayList();

        public clsMAplotsPar()
        {
            datasubset = null;
            dCol = Settings.Default.maplotDcol;
            lCol = Settings.Default.maplotLcol;
            if (dCol == "")
            {
                dCol = "#00FF00";
                Settings.Default.boxplotCol = dCol;
                Settings.Default.Save();
            }
            if (lCol == "")
            {
                lCol = "#FF0000";
                Settings.Default.boxplotCol = lCol;
                Settings.Default.Save();
            }
            trBkground = false;
            stamp = false;
        }

        public string Rcmd
        {
            get
            {
                rcmd = "MApairs(" + Rdataset + "[," + datasubset + "]," + this.Stamp + ",";
                rcmd = rcmd + @"dCol=""" + dCol + @"""," + @"lCol=""" + lCol + @"""," + this.Background + ",";
                rcmd = rcmd + @"file=""" + tempFile + @""")";
                return rcmd;
            }
        }
        
        public string Background
        {
            get
            {
                if (trBkground)
                    return "bkground=\"transparent\"";
                else
                    return "bkground=\"white\"";
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
