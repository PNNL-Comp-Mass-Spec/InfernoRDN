using System.Collections.Generic;
using DAnTE.Properties;

namespace DAnTE.Purgatorio
{
    public class clsQQPar
    {
        private string rcmd;
        public string Fcol;
        public string Bcol;
        public string Lcol;
        public string bkground;
        public string Rdataset;
        public string mstrDatasetName;
        public string datasubset;
        public int ncolumns;
        public string tempFile;
        public int reference;
        public string wshape;
        public string wscale;
        public string df;
        public string exprate;
        public List<string> Datasets = new List<string>();
        public List<string> CheckedDatasets = new List<string>();

        public clsQQPar()
        {
            ncolumns = 2;
            rcmd = "";
            datasubset = null;
            Fcol = Settings.Default.qqForeCol;
            Bcol = Settings.Default.qqBkCol;
            if (Fcol == "")
                Fcol = "#C0C0C0";
            if (Bcol == "") 
                Bcol = "#000000";
            if (Lcol == "")
                Lcol = "#FF0000"; 
            bkground = "bkground=\"white\"";
            Rdataset = "";
            mstrDatasetName = "";
            tempFile = "C:/_temp.png";
            reference = 0;
            wshape = "2.0";
            wscale = "1.0";
            df = "4";
            exprate = "1.0";
        }

        public string Rcmd
        {
            get
            {
                rcmd = "plot_qq(" + Rdataset + "[," + datasubset + "],ncols=" + ncolumns.ToString() + ",";
                rcmd = rcmd + RefDistr(reference) + ",wshape=" + wshape + ",wscale=" + wscale + ",degfree=" + 
                    df + ",exprate=" + exprate + ",";
                rcmd = rcmd + @"colF=""" + Fcol + @"""," + @"colB=""" + Bcol + @"""," 
                    + @"colL=""" + Lcol + @""",";
                rcmd = rcmd + bkground + @",file=""" + tempFile + @""")";
                return rcmd;
            }
        }

        private string RefDistr(int refd)
        {
            string dist;

            switch (refd)
            {
                case 0:
                    dist = @"reference=""Normal""";
                    break;
                case 1:
                    dist = @"reference=""Exponential""";
                    break;
                case 2:
                    dist = @"reference=""Student""";
                    break;
                case 3:
                    dist = @"reference=""Weibull""";
                    break;
                default:
                    dist = @"reference=""Normal""";
                    break;
            }
            return dist;
        }
    }
}
