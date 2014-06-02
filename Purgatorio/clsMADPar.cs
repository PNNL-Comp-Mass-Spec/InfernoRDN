using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using DAnTE.Properties;

namespace DAnTE.Purgatorio
{
    public class clsMADPar
    {
        private string rcmd;

        //[DAnTE.Tools.clsAnalysisAttribute("Dataset(R)", "MedianAbsoluteDeviation_Adjustment")]
        public string Rdataset;
        
        private string mstrDatasetName;
        [DAnTE.Tools.clsAnalysisAttribute("Set_Zero_Mean", "MedianAbsoluteDeviation_Adjustment")]
        public bool mblMeanAdj;
        private string mstrFactor;
        public int mintFactorIndex;
                
        public ArrayList marrFactors;

        public clsMADPar()
        {
            mstrDatasetName = "Eset";
            mstrFactor = "";
            mintFactorIndex = 1;
            mblMeanAdj = false;
        }

        public string Rcmd
        {
            get
            {
                rcmd = "madEset <- adjustMAD(" + Rdataset + "," + this.Factor + "," + this.MeanAdj + ")";
                return rcmd;
            }
        }

        [DAnTE.Tools.clsAnalysisAttribute("Source_DataTable", "MedianAbsoluteDeviation_Adjustment")]
        public string DataSetName
        {
            get { return mstrDatasetName; }
            set { mstrDatasetName = value; }
        }

        private string Factor
        {
            get 
            {
                if (mintFactorIndex > -1)
                    return "Factor=factors[" + mintFactorIndex.ToString() + ",]";
                else
                    return "Factor=1";
            }
        }

        private string MeanAdj
        {
            get
            {
                if (mblMeanAdj)
                    return "meanadjust=TRUE";
                else
                    return "meanadjust=FALSE";
            }
        }

        [DAnTE.Tools.clsAnalysisAttribute("Selected_Factor", "MedianAbsoluteDeviation_Adjustment")]
        public string FactorSelected
        {
            set { mstrFactor = value; }
            get { return mstrFactor; }
        }
    }
}
