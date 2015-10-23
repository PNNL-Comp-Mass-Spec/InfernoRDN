using System;

namespace DAnTE.Tools
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class clsAnalysisAttribute: Attribute
    {
        public clsAnalysisAttribute(string desc, string group)
        {
            Description = desc;
            Group = group;
        }

        public string Description { get; set; }

        public string Group { get; set; }
    }
}
