using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using DAnTE.Properties;

namespace DAnTE.Purgatorio
{
    public class clsCentralTendencyPar
    {
        private string rcmd;

        //[DAnTE.Tools.clsAnalysisAttribute("Dataset(R)", "CentralTendency")]
        public string Rdataset;
        
        private string mstrDatasetName;
        public bool mblUseMeanTend;
        [DAnTE.Tools.clsAnalysisAttribute("Zero_Center_Data", "CentralTendency")]
        public bool mblCenterZero;
        public string mstrmethod;

        public clsCentralTendencyPar()
        {
            mstrDatasetName = "Eset";
            mblUseMeanTend = true;
            mblCenterZero = true;
            mstrmethod = "MeanCenter.Sub";
        }

        public string Rcmd
        {
            get
            {
                if (mblUseMeanTend)
                    rcmd = "meanCEset <- " + mstrmethod + "(" + Rdataset + "," + this.UseTendency + "," + this.ZeroCenter + ")";
                else
                    rcmd = "medianCEset <- " + mstrmethod + "(" + Rdataset + "," + this.UseTendency + "," + this.ZeroCenter + ")";
                return rcmd;
            }
        }

        [DAnTE.Tools.clsAnalysisAttribute("Adjustment_Method", "CentralTendency")]
        public string AdjustmentMethod
        {
            get
            {
                if (mstrmethod.Equals("MeanCenter.Sub"))
                    return "Subtract";
                else
                    return "Divide";
            }
        }

        [DAnTE.Tools.clsAnalysisAttribute("Tendency", "CentralTendency")]
        public string Tendency
        {
            get
            {
                if (mblUseMeanTend)
                    return "Mean";
                else
                    return "Median";
            }
        }

        private string UseTendency
        {
            get
            {
                if (mblUseMeanTend)
                    return "Mean=TRUE";
                else
                    return "Mean=FALSE";
            }
        }

        private string ZeroCenter
        {
            get
            {
                if (mblCenterZero)
                    return "centerZero=TRUE";
                else
                    return "centerZero=FALSE";
            }
        }

        [DAnTE.Tools.clsAnalysisAttribute("Source_DataTable", "CentralTendency")]
        public string DataSetName
        {
            get { return mstrDatasetName; }
            set { mstrDatasetName = value; }
        }
                
    }
}
