using System.Collections;
using System.Collections.Generic;

namespace DAnTE.Purgatorio
{
    public class clsTamuQPar
    {
        private string rcmd;
        [Tools.clsAnalysisAttribute("Check_for_Unbalance_Data", "TAMUimputation")]
        public bool unbalanced;
        public bool randomE;
        [Tools.clsAnalysisAttribute("Use_Restricted_Maximum_Likelihood", "TAMUimputation")]
        public bool useREML;
        [Tools.clsAnalysisAttribute("Check_Interactions", "TAMUimputation")]
        public bool interactions;
        //[Tools.clsAnalysisAttribute("Dataset(R)", "TAMUimputation")]
        public string Rdataset;
        [Tools.clsAnalysisAttribute("Source_DataTable", "TAMUimputation")]
        public string mstrDatasetName;
        public string tempFile;
        [Tools.clsAnalysisAttribute("Minimum_Datapoints_Needed", "TAMUimputation")]
        public int numDatapts;
        public List<string> fixedEff;
        public List<string> randomEff;
        public List<string> marrFactors;

        public clsTamuQPar()
        {
            unbalanced = false;
            randomE = false;
            useREML = false;
            interactions = false;
            Rdataset = "Eset";
            tempFile = "C:/";
            numDatapts = 3;
            fixedEff = new List<string>();
            randomEff = new List<string>();
        }

        public string Rcmd
        {
            get
            {
                rcmd = "tamuQ <- DoTamuQ(" + Rdataset + "," + FixedEffects + ")";
                return rcmd;
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
                    fEff = fEff +  @",""" + fixedEff[i] + @"""";
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
