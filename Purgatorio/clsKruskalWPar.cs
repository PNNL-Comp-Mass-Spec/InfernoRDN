using System.Collections.Generic;

namespace DAnTE.Purgatorio
{
    public class clsKruskalWPar
    {
        private string mRCmd;
        //[Tools.clsAnalysisAttribute("Dataset(R)", "Kruskal-Walis Test")]
        public string RDataset;

        [Tools.clsAnalysisAttribute("Source_DataTable", "Kruskal-Walis_Test")]
        public string mstrDatasetName;

        [Tools.clsAnalysisAttribute("Minimum_Datapoints_Needed", "Kruskal-Walis_Test")]
        public int numDatapts;

        [Tools.clsAnalysisAttribute("Selected_Factor", "Kruskal-Walis_Test")]
        public string selectedFactor;

        public int nF;

        public clsKruskalWPar()
        {
            RDataset = "Eset";
            numDatapts = 3;
            selectedFactor = "";
            nF = 0;
        }

        public string RCommand
        {
            get
            {
                mRCmd = "kwtest <- DoNonPara(" + RDataset + @",FixedEffects=""" + selectedFactor +
                       @""",thres=" + numDatapts + @",testType=""KW"")";
                return mRCmd;
            }
        }
    }
}