using System.Collections.Generic;
using DAnTE.Tools;

namespace DAnTE.Purgatorio
{
    public class clsVennPar
    {
        private string mRCmd;
        public string x1, x2, x3;
        public string labelA, labelB, labelC;
        public string tempFile;
        public string factor;
        public List<clsFactorInfo> marrFactors = new List<clsFactorInfo>();
        public List<string> marrDatasets;
        public Dictionary<string, clsDatasetTreeNode> mhtDatasets;
        public string mstrDatasetName;
        public string Rdataset;
        public bool mblPlotFac = false;

        public clsVennPar()
        {
            labelA = "A";
            labelB = "B";
            labelC = "C";
            marrDatasets = new List<string>();
            mhtDatasets = new Dictionary<string, clsDatasetTreeNode>();
            tempFile = "C:/_temp.png";
        }

        public string RCommand
        {
            get
            {
                if (mblPlotFac)
                    mRCmd = "PlotVenn(" + this.FactorLevels + "," + this.ListNames + @",file=""" +
                           tempFile + @"""," + factor + @",Data=" + Rdataset + @")";
                else
                    mRCmd = "PlotVenn(" + DataLists + "," + ListNames + @",file=""" + tempFile + @""")";
                return mRCmd;
            }
        }

        private string FactorLevels
        {
            get
            {
                if (x3 != "")
                    return @"x1=""" + x1 + @""", x2=""" + x2 + @""", x3=""" + x3 + @"""";
                else
                    return @"x1=""" + x1 + @""", x2=""" + x2 + @"""";
            }
        }

        private string DataLists
        {
            get
            {
                if (x3 != "")
                    return "x1=" + mhtDatasets[x1].mstrRdatasetName +
                           ", x2=" + mhtDatasets[x2].mstrRdatasetName +
                           ", x3=" + mhtDatasets[x3].mstrRdatasetName;
                else
                    return "x1=" + mhtDatasets[x1].mstrRdatasetName +
                           ", x2=" + mhtDatasets[x2].mstrRdatasetName;
            }
        }

        private string ListNames
        {
            get
            {
                var labl = @"listNames=c(""" + labelA + @""",""" + labelB;
                if (x3 != null)
                    labl += @""",""" + labelC + @""")";
                else
                    labl += @""")";
                return labl;
            }
        }
    }
}