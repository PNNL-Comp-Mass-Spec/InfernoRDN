using System.Collections;
using System.Collections.Generic;
using DAnTE.Tools;

namespace DAnTE.Purgatorio
{
    public class clsFoldChangePar
    {
        private string rcmd;
        public string Rdataset;
        [Tools.clsAnalysisAttribute("Source_DataTable", "FoldChange")]
        public string mstrDatasetName;
        public string tempFile;
        [Tools.clsAnalysisAttribute("Selected_Factor", "FoldChange")]
        public string selectedFactor;
        [Tools.clsAnalysisAttribute("First_Factor_Value", "FoldChange")]
        public string selectedfVal1;
        [Tools.clsAnalysisAttribute("Second_Factor_Value", "FoldChange")]
        public string selectedfVal2;
        [Tools.clsAnalysisAttribute("Data_in_Log_Scale", "FoldChange")]
        public bool mbllogScale;
        public List<clsFactorInfo> marrFactors;

        public clsFoldChangePar()
        {
            Rdataset = "Eset";
            tempFile = "C:/";
            selectedFactor = "";
            mbllogScale = true;
        }

        public string Rcmd
        {
            get
            {
                rcmd = "foldChanges <- calcFoldChanges(" + Rdataset + "," + this.Factor + "," +
                        this.FactorValue1 + "," + this.FactorValue2 + "," + this.DataScale + ")";
                return rcmd;
            }
        }

        private string Factor
        {
            get
            {
                if (selectedFactor != null)
                {
                    return @"Factor=factors[""" + selectedFactor + @""",]";
                }
                else
                    return "Factor=factors[1,]";
            }
        }

        private string DataScale
        {
            get
            {
                if (mbllogScale)
                    return @"logScale=TRUE";
                else
                    return @"logScale=FALSE";
            }
        }

        private string FactorValue1
        {
            get
            {
                return @"fVal1=""" + selectedfVal1 + @"""";
            }
        }

        private string FactorValue2
        {
            get
            {
                return @"fVal2=""" + selectedfVal2 + @"""";
            }
        }
    }
}
