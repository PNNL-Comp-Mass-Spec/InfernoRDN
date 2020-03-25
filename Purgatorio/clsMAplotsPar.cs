using System.Collections.Generic;
using DAnTE.Properties;

namespace DAnTE.Purgatorio
{
    public class clsMAplotsPar
    {
        private string mRCmd;
        public string datasubset;
        public string dCol;
        public string lCol;
        public bool trBkground;
        public string RDataset;
        public string mstrDatasetName;
        public string tempFile;
        public bool stamp;
        public List<string> Datasets = new List<string>();
        public List<string> CheckedDatasets = new List<string>();

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

        public string RCommand
        {
            get
            {
                mRCmd = "MApairs(" + RDataset + "[," + datasubset + "]," + this.Stamp + ",";
                mRCmd = mRCmd + @"dCol=""" + dCol + @"""," + @"lCol=""" + lCol + @"""," + this.Background + ",";
                mRCmd = mRCmd + @"file=""" + tempFile + @""")";
                return mRCmd;
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