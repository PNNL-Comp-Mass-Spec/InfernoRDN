using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using DAnTE.Properties;

namespace DAnTE.Purgatorio
{
    public class clsLogTransformPar
    {
        private string rcmd;
        private string mstrLogBase;
        //[DAnTE.Tools.clsAnalysisAttribute("Dataset(R)", "Log")]
        public string Rdataset;
        private string mstrDatasetName;
        private double mdblBias;
        private string mstrBiasOp;

        public clsLogTransformPar()
        {
            mstrLogBase = "Log2";
            mdblBias = 1;
            mstrBiasOp = "Multiplicative";
            Rdataset = "Eset";
            mstrDatasetName = "Expressions";
        }

        public string Rcmd
        {
            get
            {
                rcmd = "logEset <- logTransform(" + Rdataset + "," + this.LogBase_R + "," + this.Bias_R +
                                    "," + this.BiasOp_R + ")";
                return rcmd;
            }
        }

        private string LogBase_R
        {
            get
            {
                if (mstrLogBase.Equals("Log10"))
                    return "logBase=10";
                else if (mstrLogBase.Equals("NaturalLog"))
                    return "logBase=exp(1)";
                else
                    return "logBase=2";
            }
        }

        private string Bias_R
        {
            get
            {
                return "bias=" + mdblBias.ToString();
            }
        }

        private string BiasOp_R
        {
            get
            {
                if (mstrBiasOp.Equals("Additive"))
                    return "add=TRUE";
                else
                    return "add=FALSE";
            }
        }

        [DAnTE.Tools.clsAnalysisAttribute("Source_DataTable", "Log")]
        public string DatasetName
        {
            get { return mstrDatasetName; }
            set { mstrDatasetName = value; }
        }

        [DAnTE.Tools.clsAnalysisAttribute("Log_Base", "Log")]
        public string LogBase
        {
            get { return mstrLogBase; }
            set { mstrLogBase = value; }
        }
        
        [DAnTE.Tools.clsAnalysisAttribute("Bias", "Log")]
        public double LogBias
        {
            get { return mdblBias; }
            set { mdblBias = value; }
        }

        [DAnTE.Tools.clsAnalysisAttribute("Bias_Operation", "Log")]
        public string BiasOp
        {
            get { return mstrBiasOp; }
            set { mstrBiasOp = value; }
        }

    }
}
