using System.Collections.Generic;

namespace DAnTE.Tools
{
    class clsProteinInfo
    {
        public List<string> Metadata { get; }

        public string ProteinName { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="proteinName">Protein name</param>
        public clsProteinInfo(string proteinName)
        {
            ProteinName = proteinName;
            Metadata = new List<string>();
        }

        public void AppendMetadata(string value)
        {
            Metadata.Add(string.IsNullOrWhiteSpace(value) ? string.Empty : value);
        }

        public override string ToString()
        {
            return ProteinName;
        }
    }
}