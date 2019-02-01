using System.Collections.Generic;

namespace DAnTE.Purgatorio
{
    public class clsTamuQPar
    {
        private string mRCmd;
        [Tools.clsAnalysisAttribute("Check_for_Unbalance_Data", "TAMUimputation")] public readonly bool unbalanced;
        [Tools.clsAnalysisAttribute("Use_Restricted_Maximum_Likelihood", "TAMUimputation")] public readonly bool useREML;
        [Tools.clsAnalysisAttribute("Check_Interactions", "TAMUimputation")] public readonly bool interactions;
        //[Tools.clsAnalysisAttribute("Dataset(R)", "TAMUimputation")]
        public string Rdataset;
        [Tools.clsAnalysisAttribute("Source_DataTable", "TAMUimputation")] public string mstrDatasetName;
        public List<string> fixedEff;
        private readonly List<string> randomEff;

        public clsTamuQPar()
        {
            unbalanced = false;
            useREML = false;
            interactions = false;
            Rdataset = "Eset";
            fixedEff = new List<string>();
            randomEff = new List<string>();
        }

        public string RCommand
        {
            get
            {
                mRCmd = "tamuQ <- DoTamuQ(" + Rdataset + "," + FixedEffects + ")";
                return mRCmd;
            }
        }

        [Tools.clsAnalysisAttribute("Fixed_Effect_Factors", "TAMUimputation")]
        public string FixedEffectsFactors
        {
            get
            {
                string fEff;

                if (fixedEff.Count == 0)
                    return "None";
                else
                    fEff = fixedEff[0].ToString();

                for (var i = 1; i < fixedEff.Count; i++)
                {
                    fEff = fEff + "," + fixedEff[i];
                }

                return fEff;
            }
        }

        private string FixedEffects
        {
            get
            {
                string fEff;

                if (fixedEff.Count == 0)
                    return "NULL";
                else
                    fEff = @"c(""" + fixedEff[0] + @"""";

                for (var i = 1; i < fixedEff.Count; i++)
                {
                    fEff = fEff + @",""" + fixedEff[i] + @"""";
                }
                fEff = fEff + ")";

                return fEff;
            }
        }

        [Tools.clsAnalysisAttribute("Random_Effect_Factors", "TAMUimputation")]
        public string RandomEffectsFactors
        {
            get
            {
                string rEff;

                if (randomEff.Count == 0)
                    return "None";
                else
                    rEff = randomEff[0].ToString();

                for (var i = 1; i < randomEff.Count; i++)
                {
                    rEff = rEff + "," + randomEff[i];
                }

                return rEff;
            }
        }

        private string RandomEffects
        {
            get
            {
                string rEff;

                if (randomEff.Count == 0)
                    return "NULL";
                else
                    rEff = @"c(""" + randomEff[0] + @"""";

                for (var i = 1; i < randomEff.Count; i++)
                {
                    rEff = rEff + @",""" + randomEff[i] + @"""";
                }
                rEff = rEff + ")";

                return rEff;
            }
        }

        private string UseREML
        {
            get
            {
                if (useREML)
                    return "useREML=TRUE";
                else
                    return "useREML=FALSE";
            }
        }

        private string Interactions
        {
            get
            {
                if (interactions)
                    return "interact=TRUE";
                else
                    return "interact=FALSE";
            }
        }

        private string Unbalanced
        {
            get
            {
                if (unbalanced)
                    return "unbalanced=TRUE";
                else
                    return "unbalanced=FALSE";
            }
        }
    }
}