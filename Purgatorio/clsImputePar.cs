using System.Collections.Generic;

namespace DAnTE.Purgatorio
{
    public class clsImputePar
    {
        private string mRCmd;

        //[Tools.clsAnalysisAttribute("Dataset(R)", "Imputation")]
        public string mRDataset;

        [Tools.clsAnalysisAttribute("ImputeThreshold", "Imputation")]
        public string mFiltCutoff;

        public string mImputationMode;
        public string mK;

        [Tools.clsAnalysisAttribute("Number_of_PrincipalComponents", "Imputation")]
        public string mNPCs;

        public string mSVDth;
        public string mMaxIterations;
        public string mSubConst;

        [Tools.clsAnalysisAttribute("NoFilling_Below_Threshold", "Imputation")]
        public bool mblNoFill;

        private string mFactor;
        public int mintFactorIndex;

        public clsImputePar()
        {
            DataSetName = "Eset";
            mFiltCutoff = "20";
            mImputationMode = "mean";
            mK = "10";
            mNPCs = "5";
            mSVDth = "0.01";
            mMaxIterations = "100";
            mSubConst = "1";
            mFactor = "";
            mblNoFill = false;
            mintFactorIndex = 1;
        }

        public string RCommand
        {
            get
            {
                mRCmd = "imputedData <- imputeData(" + mRDataset + "," + mFiltCutoff + @",""" + mImputationMode + @"""," +
                       mK + "," + mNPCs + "," + mSVDth + "," + mMaxIterations + "," + mSubConst +
                       "," + this.Factor + "," + this.NoFill + ")";
                return mRCmd;
            }
        }

        [Tools.clsAnalysisAttribute("Method", "Imputation")]
        public string Mode
        {
            get
            {
                if (mImputationMode.Equals("const"))
                    return "Substitute Constant";
                if (mImputationMode.Equals("mean"))
                    return "Dataset Mean";
                if (mImputationMode.Equals("median"))
                    return "Dataset Median";
                if (mImputationMode.Equals("rowmean"))
                    return "Use Row Mean";
                if (mImputationMode.Equals("knn"))
                    return "kNNImpute";
                if (mImputationMode.Equals("knnw"))
                    return "Weighted kNNImpute";
                if (mImputationMode.Equals("svd"))
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
                if (mImputationMode.Contains("knn"))
                    return mK;
                else
                    return "Not applicable";
            }
        }

        [Tools.clsAnalysisAttribute("Number_of_PrincipalComponents", "Imputation")]
        public string nPCS
        {
            get
            {
                if (mImputationMode.Contains("svd"))
                    return mNPCs;
                else
                    return "Not applicable";
            }
        }

        [Tools.clsAnalysisAttribute("SVDImpute_iterations", "Imputation")]
        public string MaxIter
        {
            get
            {
                if (mImputationMode.Contains("svd"))
                    return mMaxIterations;
                else
                    return "Not applicable";
            }
        }

        [Tools.clsAnalysisAttribute("SVDImpute_threshold", "Imputation")]
        public string SVDthresh
        {
            get
            {
                if (mImputationMode.Contains("svd"))
                    return mSVDth;
                else
                    return "Not applicable";
            }
        }

        [Tools.clsAnalysisAttribute("Constant_to_Substitute", "Imputation")]
        public string Const2Sub
        {
            get
            {
                if (mImputationMode.Contains("const"))
                    return mSubConst;
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
            set => mFactor = value;
            get => mFactor;
        }

        [Tools.clsAnalysisAttribute("Selected_Factor", "Imputation")]
        public string FactorSelectedAttr
        {
            get
            {
                if ((mImputationMode.Contains("svd")) || (mImputationMode.Contains("knn")) || (mImputationMode.Contains("rowmean")))
                    return mFactor;
                else
                    return "Not applicable";
            }
        }
    }
}