namespace DAnTE.Tools
{
    public class clsAnalysisObject
    {
        private readonly string _operation;
        private readonly object _o;

        public clsAnalysisObject(string analysisStep, object o)
        {
            _operation = analysisStep;
            _o = o;
        }

        public string Operation
        {
            get { return _operation; }
        }

        public object AnalysisObject
        {
            get { return _o; }
        }
    }
}