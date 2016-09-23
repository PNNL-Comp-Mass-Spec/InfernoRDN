namespace DAnTE.Tools
{
    public class clsAnalysisObject
    {
        public clsAnalysisObject(string analysisStep, object o)
        {
            Operation = analysisStep;
            AnalysisObject = o;
        }

        public string Operation { get; }

        public object AnalysisObject { get; }
    }
}