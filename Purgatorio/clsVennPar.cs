using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using DAnTE.Properties;

namespace DAnTE.Purgatorio
{
    public class clsVennPar
    {
        private string rcmd;
        public bool factorPlot;
        public string x1, x2, x3;
        public string labelA, labelB, labelC;
        public string tempFile;
        public string factor;
        public ArrayList marrFactorNames = new ArrayList();
        public ArrayList marrFactors = new ArrayList();
        public ArrayList marrDatasets;
        public Hashtable mhtDatasets;
        public string mstrDatasetName;
        public string Rdataset;
        public bool mblPlotFac = false;

        public clsVennPar()
        {
            labelA = "A";
            labelB = "B";
            labelC = "C";
            marrDatasets = new ArrayList();
            mhtDatasets = new Hashtable();
            tempFile = "C:/_temp.png";
        }

        public string Rcmd
        {
            get
            {
                if (mblPlotFac)
                    rcmd = "PlotVenn(" + this.FactorLevels + "," + this.ListNames + @",file=""" + 
                        tempFile + @"""," + factor + @",Data=" + Rdataset + @")";
                else
                    rcmd = "PlotVenn(" + DataLists + "," + ListNames + @",file=""" + tempFile + @""")";
                return rcmd;
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
                    return "x1=" + ((DAnTE.Tools.clsDatasetTreeNode)mhtDatasets[x1]).mstrRdatasetName +
                        ", x2=" + ((DAnTE.Tools.clsDatasetTreeNode)mhtDatasets[x2]).mstrRdatasetName +
                        ", x3=" + ((DAnTE.Tools.clsDatasetTreeNode)mhtDatasets[x3]).mstrRdatasetName;
                else
                    return "x1=" + ((DAnTE.Tools.clsDatasetTreeNode)mhtDatasets[x1]).mstrRdatasetName +
                        ", x2=" + ((DAnTE.Tools.clsDatasetTreeNode)mhtDatasets[x2]).mstrRdatasetName;
            }
        }
                
        private string ListNames
        {
            get
            {
                string labl = @"listNames=c(""" + labelA + @""",""" + labelB;
                if (x3 != null)
                    labl += @""",""" + labelC + @""")";
                else
                    labl += @""")";
                return labl;
            }
        }
    }
}
