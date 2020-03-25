using System.Globalization;

namespace DAnTE.Purgatorio
{
    public class clsLogTransformPar
    {
        private string mRCmd;
        private string mstrLogBase;
        //[Tools.clsAnalysisAttribute("Dataset(R)", "Log")]
        public string RDataset;
        private double mdblBias;
        private string mstrBiasOp;

        public clsLogTransformPar()
        {
            mstrLogBase = "Log2";
            mdblBias = 1;
            mstrBiasOp = "Multiplicative";
            RDataset = "Eset";
            DatasetName = "Expressions";
        }

        public string RCommand
        {
            get
            {
                mRCmd = "logEset <- logTransform(" + RDataset + "," + this.LogBase_R + "," + this.Bias_R +
                       "," + this.BiasOp_R + ")";
                return mRCmd;
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

        private string Bias_R => "bias=" + mdblBias.ToString(CultureInfo.InvariantCulture);

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

        [Tools.clsAnalysisAttribute("Source_DataTable", "Log")]
        public string DatasetName { get; set; }

        [Tools.clsAnalysisAttribute("Log_Base", "Log")]
        public string LogBase
        {
            get => mstrLogBase;
            set => mstrLogBase = value;
        }

        [Tools.clsAnalysisAttribute("Bias", "Log")]
        public double LogBias
        {
            get => mdblBias;
            set => mdblBias = value;
        }

        [Tools.clsAnalysisAttribute("Bias_Operation", "Log")]
        public string BiasOp
        {
            get => mstrBiasOp;
            set => mstrBiasOp = value;
        }
    }
}