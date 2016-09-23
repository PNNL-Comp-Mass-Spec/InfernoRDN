using System;
using System.Collections.Generic;
using DAnTE.Properties;

namespace DAnTE.Purgatorio
{
    public class clsPCAplotPar : ICloneable
    {
        private string rcmd;
        public string factor;
        public int factorIdx;
        public string prinComps;
        public int pcx, pcy, pcz;
        public bool dropLines;
        public bool persp;
        public bool biplot;
        public bool labels;
        public bool screeplot;
        public bool arrows;
        public bool biplotL;
        public bool threeD;
        public bool pca;
        public bool stamp;
        public string Rdataset;
        public string mstrDatasetName;
        public string tempFile;
        public string datasubset;
        public List<string> Datasets = new List<string>();
        public List<string> CheckedDatasets = new List<string>();

        public clsPCAplotPar()
        {
            datasubset = null;
            factor = "1";
            factorIdx = 0;
            prinComps = "PCs=c(1,2)";
            dropLines = true;
            persp = true;
            biplot = false;
            labels = true;
            screeplot = true;
            arrows = true;
            biplotL = false;
            threeD = false;
            pca = true;
            pcx = 0;
            pcy = 1;
            pcz = 2;
            stamp = false;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public string Rcmd
        {
            get
            {
                rcmd = "weights <- plotPCA(" + Rdataset + "[," + datasubset + "]," + "Factor=" +
                       factor + "," + prinComps + ",";
                rcmd = rcmd + DropLines + "," + Perspective + "," + BiPlot + "," + ShowLabels + "," +
                       Screeplot + "," + BiArrows + "," + BiLabels + "," + Type + "," + Stamp +
                       @",file=""" + tempFile + @""")";

                return rcmd;
            }
        }

        public string DropLines
        {
            get
            {
                if (dropLines)
                    return "Lines=TRUE";
                else
                    return "Lines=FALSE";
            }
        }

        public string ShowLabels
        {
            get
            {
                if (labels)
                    return "Labels=TRUE";
                else
                    return "Labels=FALSE";
            }
        }

        public string Perspective
        {
            get
            {
                if (persp)
                    return "Persp=TRUE";
                else
                    return "Persp=FALSE";
            }
        }

        public string BiPlot
        {
            get
            {
                if (biplot)
                    return "biplotting=TRUE";
                else
                    return "biplotting=FALSE";
            }
        }

        public string Screeplot
        {
            get
            {
                if (screeplot)
                    return "scree=TRUE";
                else
                    return "scree=FALSE";
            }
        }

        public string BiArrows
        {
            get
            {
                if (arrows)
                    return "Arrows=TRUE";
                else
                    return "Arrows=FALSE";
            }
        }

        public string BiLabels
        {
            get
            {
                if (biplotL)
                    return "biplotL=TRUE";
                else
                    return "biplotL=FALSE";
            }
        }

        public string Type
        {
            get
            {
                if (pca)
                    return @"Type=""PCA""";
                else
                    return @"Type=""PLS""";
            }
        }

        public string Stamp
        {
            get
            {
                if (stamp)
                    return @"stamp=""" + Settings.Default.DataFileName.Replace("\\", "\\\\") + @"""";
                else
                    return "stamp=NULL";
            }
        }
    }
}