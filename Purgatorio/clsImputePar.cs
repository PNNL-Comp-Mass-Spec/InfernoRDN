using System.Collections.Generic;

namespace DAnTE.Purgatorio
{
    public class clsImputePar
    {
        private string rcmd;

        //[Tools.clsAnalysisAttribute("Dataset(R)", "Imputation")]
        public string Rdataset;

        [Tools.clsAnalysisAttribute("ImputeThreshold", "Imputation")] public string mstrFiltCutoff;
        public string mstrmode;
        public string mstrK;
        public string mstrNPCs;
        public string mstrSVDth;
        public string mstrMaxIter;
        public string mstrSubConst;
        [Tools.clsAnalysisAttribute("NoFilling_Below_Threshold", "Imputation")] public bool mblNoFill;

        private string mstrFactor;
        public int mintFactorIndex;

        public clsImputePar()
        {
            DataSetName = "Eset";
            mstrFiltCutoff = "20";
            mstrmode = "mean";
            mstrK = "10";
            mstrNPCs = "5";
            mstrSVDth = "0.01";
            mstrMaxIter = "100";
            mstrSubConst = "1";
            mstrFactor = "";
            mblNoFill = false;
            mintFactorIndex = 1;
        }

        public string Rcmd
        {
            get
            {
                rcmd = "imputedData <- imputeData(" + Rdataset + "," + mstrFiltCutoff + @",""" + mstrmode + @"""," +
                       mstrK + "," + mstrNPCs + "," + mstrSVDth + "," + mstrMaxIter + "," + mstrSubConst +
                       "," + this.Factor + "," + this.NoFill + ")";
                return rcmd;
            }
        }

        [Tools.clsAnalysisAttribute("Method", "Imputation")]
        public string Mode
        {
            get
            {
                if (mstrmode.Equals("const"))
                    return "Sustitute Constant";
                if (mstrmode.Equals("mean"))
                    return "Dataset Mean";
                if (mstrmode.Equals("median"))
                    return "Dataset Median";
                if (mstrmode.Equals("rowmean"))
                    return "Use Row Mean";
                if (mstrmode.Equals("knn"))
                    return "kNNImpute";
                if (mstrmode.Equals("knnw"))
                    return "Weighted kNNImpute";
                if (mstrmode.Equals("svd"))
                    return "SVDImpute";
                else
                    return "None";
            }
        }

        [Tools.clsAnalysisAttribute("k_in_kNN", "Imputation")]
        public string kNN
        {
            get
            {
                if (mstrmode.Contains("knn"))
                    return mstrK;
                else
                    return "Not applicable";
            }
        }

        [Tools.clsAnalysisAttribute("Number_of_PrincipalComponents", "Imputation")]
        public string nPCS
        {
            get
            {
                if (mstrmode.Contains("svd"))
                    return mstrNPCs;
                else
                    return "Not applicable";
            }
        }

        [Tools.clsAnalysisAttribute("SVDImpute_iterations", "Imputation")]
        public string MaxIter
        {
            get
            {
                if (mstrmode.Contains("svd"))
                    return mstrMaxIter;
                else
                    return "Not applicable";
            }
        }

        [Tools.clsAnalysisAttribute("SVDImpute_threshold", "Imputation")]
        public string SVDthresh
        {
            get
            {
                if (mstrmode.Contains("svd"))
                    return mstrSVDth;
                else
                    return "Not applicable";
            }
        }

        [Tools.clsAnalysisAttribute("Constant_to_Substitute", "Imputation")]
        public string Const2Sub
        {
            get
            {
                if (mstrmode.Contains("const"))
                    return mstrSubConst;
                else
                    return "Not applicable";
            }
        }

        private string NoFill
        {
            get
            {
                if (mblNoFill)
                    return "noFill=TRUE";
                else
                    return "noFill=FALSE";
            }
        }

        [Tools.clsAnalysisAttribute("Source_DataTable", "Imputation")]
        public string DataSetName { get; set; }

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

        public string FactorSelected
        {
            set => mstrFactor = value;
            get => mstrFactor;
        }

        [Tools.clsAnalysisAttribute("Selected_Factor", "Imputation")]
        public string FactorSelectedAttr
        {
            get
            {
                if ((mstrmode.Contains("svd")) || (mstrmode.Contains("knn")) || (mstrmode.Contains("rowmean")))
                    return mstrFactor;
                else
                    return "Not applicable";
            }
        }
    }
}