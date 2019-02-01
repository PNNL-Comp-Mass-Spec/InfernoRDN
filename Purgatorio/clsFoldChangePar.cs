using System.Collections.Generic;
using DAnTE.Tools;

namespace DAnTE.Purgatorio
{
    public class clsFoldChangePar
    {
        private string mRCmd;
        public string Rdataset;
        [Tools.clsAnalysisAttribute("Source_DataTable", "FoldChange")] public string mstrDatasetName;
        [Tools.clsAnalysisAttribute("Selected_Factor", "FoldChange")] public string selectedFactor;
        [Tools.clsAnalysisAttribute("First_Factor_Value", "FoldChange")] public string selectedfVal1;
        [Tools.clsAnalysisAttribute("Second_Factor_Value", "FoldChange")] public string selectedfVal2;
        [Tools.clsAnalysisAttribute("Data_in_Log_Scale", "FoldChange")] public bool mbllogScale;
        public List<clsFactorInfo> marrFactors;

        public clsFoldChangePar()
        {
            Rdataset = "Eset";
            selectedFactor = "";
            mbllogScale = true;
        }

        public string RCommand
        {
            get
            {
                mRCmd = "foldChanges <- calcFoldChanges(" + Rdataset + "," + this.Factor + "," +
                       this.FactorValue1 + "," + this.FactorValue2 + "," + this.DataScale + ")";
                return mRCmd;
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

        private string FactorValue1 => @"fVal1=""" + selectedfVal1 + @"""";

        private string FactorValue2 => @"fVal2=""" + selectedfVal2 + @"""";
    }
}