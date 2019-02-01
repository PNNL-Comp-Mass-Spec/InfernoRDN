using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using DAnTE.Properties;
using DAnTE.Tools;

namespace DAnTE.Purgatorio
{
    public class clsHeatmapPar
    {
        private string mRCmd;
        public string Rdataset;
        [clsAnalysisAttribute("Source_DataTable", "Heatmap_Clustering")] public string mstrDatasetName;
        public string mstrFactor;
        public int mintFactorIndex; // 1,2,3,...
        public string tempFile;
        [clsAnalysisAttribute("Cluster_Rows", "Heatmap_Clustering")] public bool rowClust;
        [clsAnalysisAttribute("Cluster_Datasets", "Heatmap_Clustering")] public bool colClust;
        [clsAnalysisAttribute("Hierarchical_Clustering", "Heatmap_Clustering")] public bool hclust;
        [clsAnalysisAttribute("Scale_Rows", "Heatmap_Clustering")] public bool rowScale;
        public int agglomeration;
        public int distance;
        public int k;
        public bool fixSeed;
        public bool gridSelect;
        public bool customSelect;
        public bool rowsSelected;
        public int rStart;
        public int rEnd;
        public int paletteType;
        public string palettename;
        public bool mblsetColRng;
        public double mdblMinCol;
        public double mdblMaxCol;
        public string customCol;
        public bool noxlab;
        public List<string> Factors = new List<string>();
        public List<string> marrSelRows = new List<string>();


        public clsHeatmapPar()
        {
            var lowC = Settings.Default.colCustLow;
            var midC = Settings.Default.colCustMid;
            var highC = Settings.Default.colCustHigh;

            if ((lowC == "") || (midC == "") || (highC == ""))
            {
                lowC = clsHexColorUtil.ColorToHex(Color.FromKnownColor(KnownColor.Blue));
                midC = clsHexColorUtil.ColorToHex(Color.FromKnownColor(KnownColor.White));
                highC = clsHexColorUtil.ColorToHex(Color.FromKnownColor(KnownColor.Red));
            }
            customCol = @"customColors=c(""" + lowC + @""",""" + midC + @""",""" + highC + @""")";
            Rdataset = "Eset";
            gridSelect = false;
            customSelect = true;
            rowsSelected = false;
            rStart = 1;
            rEnd = 50;

            // Default palette for Heat maps is Blue-White-Red (5)
            paletteType = Settings.Default.colorMapType;
            if (paletteType == 0)
                paletteType = 5;
            palettename = Settings.Default.colorMap;
            if (palettename == "")
                palettename = "Blue-White-Red";
            mstrFactor = "1";
            rowClust = true;
            colClust = false;
            hclust = true;
            agglomeration = 1; //0:single, 1:Complete, 2:Average, 3:McQuitty, 4:Ward 5:Median, 6:Centroid
            distance = 0; //0:Euclidean, 1:Maximum, 2:Manhattan, 3:Canberra, 4:Binary, 5:Pearson, 6:Correlation, 
            //7:Spearman, 8:Kendall
            k = 5;
            fixSeed = false;
            noxlab = false;
            rowScale = true;
            mintFactorIndex = 1;
            mstrFactor = "<NA>";
            mblsetColRng = false;
            mdblMaxCol = 5;
            mdblMinCol = -5;
        }

        public string RCommand
        {
            get
            {
                mRCmd = "clusterResults <- plotHeatmap(" + Rdataset + "[" + this.DataSubset + @",], file=""" +
                       tempFile + @"""," + this.rDendrogram + "," + this.cDendrogram + "," + this.Kmeans +
                       "," + Agglom + "," + Dist + "," + this.RowScale + "," + this.Seed + "," + this.Palette + "," +
                       customCol + "," + this.ColorRange + "," + this.Factor + "," + RowLab + ")";
                return mRCmd;
            }
        }

        private string ColorRange
        {
            get
            {
                var colRange = "colRange=c(";
                if (mblsetColRng)
                    colRange = colRange + mdblMinCol.ToString(CultureInfo.InvariantCulture) + "," +
                               mdblMaxCol.ToString(CultureInfo.InvariantCulture) + ")";
                else
                    colRange = "colRange=NULL";
                return colRange;
            }
        }

        [clsAnalysisAttribute("ColorRange", "Heatmap_Clustering")]
        public string ColorRangeProp
        {
            get
            {
                var colRange = "[";
                if (mblsetColRng)
                    colRange = colRange + mdblMinCol.ToString(CultureInfo.InvariantCulture) + "," +
                               mdblMaxCol.ToString(CultureInfo.InvariantCulture) + "]";
                else
                    colRange = "NA";
                return colRange;
            }
        }

        private string DataSubset
        {
            get
            {
                var subset = "c(";
                if (marrSelRows.Count == 0 || customSelect)
                {
                    subset = subset + rStart + ":" + rEnd + ")";
                    gridSelect = false;
                }
                else
                {
                    foreach (var item in marrSelRows)
                    {
                        subset = subset + @"""" + item + @"""" + ",";
                    }
                    subset = subset.Substring(0, subset.Length - 1) + ")";
                    gridSelect = true;
                }
                return subset;
            }
        }

        private string Factor
        {
            get
            {
                if (mintFactorIndex > -1 && !mstrFactor.Contains("<NA>"))
                    return "Factor=factors[" + mintFactorIndex + ",]";
                else
                    return "Factor=1";
            }
        }

        [clsAnalysisAttribute("Selected_Factor", "Heatmap_Clustering")]
        public string FactorSelected
        {
            set => mstrFactor = value;
            get => mstrFactor;
        }

        [clsAnalysisAttribute("DataSubset", "Heatmap_Clustering")]
        public string DataRowSubset
        {
            get
            {
                var subset = "";
                if (marrSelRows.Count == 0 || customSelect)
                {
                    subset = rStart + ":" + rEnd;
                }
                else
                {
                    foreach (var item in marrSelRows)
                    {
                        subset = item + ",";
                    }
                    subset = subset.Substring(0, subset.Length - 1);
                }
                return subset;
            }
        }

        private string RowScale
        {
            get
            {
                if (rowScale)
                    return "rowscale=TRUE";
                else
                    return "rowscale=FALSE";
            }
        }

        private string rDendrogram
        {
            get
            {
                if (rowClust && hclust)
                    return "rDend=NULL";
                else
                    return "rDend=NA";
            }
        }

        private string Agglom => "agglomeration=" + agglomeration;

        [clsAnalysisAttribute("Agglomeration", "Heatmap_Clustering")]
        public string Agglomeration
        {
            get
            {
                switch (agglomeration)
                {
                    case 0:
                        return "Single";
                    case 1:
                        return "Complete";
                    case 2:
                        return "Average";
                    case 3:
                        return "Mcquitty";
                    case 4:
                        return "Ward";
                    case 5:
                        return "Median";
                    case 6:
                        return "Centroid";
                    default:
                        return "Complete";
                }
            }
        }

        private string Dist => "distance=" + distance;

        [clsAnalysisAttribute("Distance_Metric", "Heatmap_Clustering")]
        public string Distance
        {
            get
            {
                switch (distance)
                {
                    case 0:
                        return "Euclidean";
                    case 1:
                        return "Maximum";
                    case 2:
                        return "Manhattan";
                    case 3:
                        return "Canberra";
                    case 4:
                        return "Binary";
                    case 5:
                        return "Pearson";
                    case 6:
                        return "Correlation";
                    case 7:
                        return "Spearman";
                    case 8:
                        return "Kendall";
                    default:
                        return "Euclidean";
                }
            }
        }

        private string cDendrogram
        {
            get
            {
                if (colClust)
                    return "cDend=NULL";
                else
                    return "cDend=NA";
            }
        }

        private string Kmeans
        {
            get
            {
                if (rowClust && !hclust)
                    return "Kmeans=" + k;
                else
                    return "Kmeans=FALSE";
            }
        }

        [clsAnalysisAttribute("K_for_kmeans", "Heatmap_Clustering")]
        public string K
        {
            get
            {
                if (rowClust && !hclust)
                    return k.ToString();
                else
                    return "Not Applicable";
            }
        }

        [clsAnalysisAttribute("Kmeans_Clustering", "Heatmap_Clustering")]
        public string DoKmeans
        {
            get
            {
                if (hclust)
                    return "False";
                else
                    return "True";
            }
        }

        private string Seed
        {
            get
            {
                if (fixSeed)
                    return "fixSeed=TRUE";
                else
                    return "fixSeed=FALSE";
            }
        }

        private string RowLab
        {
            get
            {
                if (noxlab)
                    return "noxlab=TRUE";
                else
                    return "noxlab=FALSE";
            }
        }

        private string Palette
        {
            get
            {
                string cMap = null;
                switch (paletteType)
                {
                    case 1:
                        cMap = @"cMap=""GreenRed""";
                        palettename = "Green-Red";
                        break;
                    case 2:
                        cMap = @"cMap=""Heat""";
                        palettename = "Heat-Palette";
                        break;
                    case 3:
                        cMap = @"cMap=""Custom""";
                        palettename = "Custom";
                        break;
                    case 4:
                        cMap = @"cMap=""BlackBody""";
                        palettename = "Black-Body";
                        break;
                    case 5:
                        cMap = @"cMap=""BlueWhiteRed""";
                        palettename = "Blue-White-Red";
                        break;
                    default:
                        break;
                }
                return cMap;
            }
        }

        public List<string> SelectedRows
        {
            set
            {
                marrSelRows = value;
                if (marrSelRows.Count > 0)
                {
                    rowsSelected = true;
                    gridSelect = true;
                    //rStart = 0;
                    //rEnd = value.Count;
                }
            }
        }
    }
}