using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Drawing;
using System.Data;
using DAnTE.Properties;
using DAnTE.Tools;

namespace DAnTE.Purgatorio
{
    public class clsHeatmapPar
    {
        private string rcmd;
        public string Rdataset;
        [DAnTE.Tools.clsAnalysisAttribute("Source_DataTable", "Heatmap_Clustering")]
        public string mstrDatasetName;
        public string mstrFactor;
        public int mintFactorIndex; // 1,2,3,...
        public string tempFile;
        [DAnTE.Tools.clsAnalysisAttribute("Cluster_Rows", "Heatmap_Clustering")]
        public bool rowClust;
        [DAnTE.Tools.clsAnalysisAttribute("Cluster_Datasets", "Heatmap_Clustering")]
        public bool colClust;
        [DAnTE.Tools.clsAnalysisAttribute("Hierarchical_Clustering", "Heatmap_Clustering")]
        public bool hclust;
        [DAnTE.Tools.clsAnalysisAttribute("Scale_Rows", "Heatmap_Clustering")]
        public bool rowScale;
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
        private string lowC, midC, highC;
        public bool noxlab;
        public ArrayList Factors = new ArrayList();
        public ArrayList marrSelRows = new ArrayList();


        public clsHeatmapPar()
        {
            lowC = Settings.Default.colCustLow;
            midC = Settings.Default.colCustMid;
            highC = Settings.Default.colCustHigh;
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
            paletteType = Settings.Default.colorMapType;
            if (paletteType == 0)
                paletteType = 4;
            palettename = Settings.Default.colorMap;
            if (palettename == "")
                palettename = "Black-Body";
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

        public string Rcmd
        {
            get
            {
                rcmd = "clusterResults <- plotHeatmap(" + Rdataset + "[" + this.DataSubset + @",], file=""" + 
                    tempFile + @"""," + this.rDendrogram + "," + this.cDendrogram + "," + this.Kmeans +
                    "," + Agglom + "," + Dist + "," + this.RowScale + "," + this.Seed + "," + this.Palette + "," + 
                    customCol + "," + this.ColorRange + "," + this.Factor + "," + RowLab + ")";
                return rcmd;
            }
        }

        private string ColorRange
        {
            get
            {
                string colRange = "colRange=c(";
                if (mblsetColRng)
                    colRange = colRange + mdblMinCol.ToString() + "," + mdblMaxCol.ToString() + ")";
                else
                    colRange = "colRange=NULL";
                return colRange;
            }
        }

        [DAnTE.Tools.clsAnalysisAttribute("ColorRange", "Heatmap_Clustering")]
        public string ColorRangeProp
        {
            get
            {
                string colRange = "[";
                if (mblsetColRng)
                    colRange = colRange + mdblMinCol.ToString() + "," + mdblMaxCol.ToString() + "]";
                else
                    colRange = "NA";
                return colRange;
            }
        }

        private string DataSubset
        {
            get
            {
                string subset = "c(";
                if (marrSelRows.Count == 0 || customSelect)
                {
                    subset = subset + rStart.ToString() + ":" + rEnd.ToString() + ")";
                    gridSelect = false;
                }
                else
                {
                    for (int i = 0; i < marrSelRows.Count; i++)
                    {
                        subset = subset + @"""" + marrSelRows[i].ToString() + @"""" + ",";
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
                    return "Factor=factors[" + mintFactorIndex.ToString() + ",]";
                else
                    return "Factor=1";
            }
        }

        [DAnTE.Tools.clsAnalysisAttribute("Selected_Factor", "Heatmap_Clustering")]
        public string FactorSelected
        {
            set { mstrFactor = value; }
            get { return mstrFactor; }
        }

        [DAnTE.Tools.clsAnalysisAttribute("DataSubset", "Heatmap_Clustering")]
        public string DataRowSubset
        {
            get
            {
                string subset = "";
                if (marrSelRows.Count == 0 || customSelect)
                {
                    subset = rStart.ToString() + ":" + rEnd.ToString();
                }
                else
                {
                    for (int i = 0; i < marrSelRows.Count; i++)
                    {
                        subset = marrSelRows[i].ToString() + ",";
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

        private string Agglom
        {
            get
            {
                return "agglomeration=" + agglomeration.ToString();
            }
        }

        [DAnTE.Tools.clsAnalysisAttribute("Agglomeration", "Heatmap_Clustering")]
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

        private string Dist
        {
            get
            {
                return "distance=" + distance.ToString();
            }
        }

        [DAnTE.Tools.clsAnalysisAttribute("Distance_Metric", "Heatmap_Clustering")]
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
                    return "Kmeans=" + k.ToString();
                else
                    return "Kmeans=FALSE";
            }
        }

        [DAnTE.Tools.clsAnalysisAttribute("K_for_kmeans", "Heatmap_Clustering")]
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

        [DAnTE.Tools.clsAnalysisAttribute("Kmeans_Clustering", "Heatmap_Clustering")]
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

        public ArrayList SelectedRows
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
