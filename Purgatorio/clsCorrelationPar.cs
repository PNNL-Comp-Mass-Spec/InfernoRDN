using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Drawing;
using DAnTE.Properties;
using DAnTE.Tools;

namespace DAnTE.Purgatorio
{
    public class clsCorrelationPar
    {
        string rcmd;
        public string datasubset;
        public string Rdataset;
        public string mstrDatasetName;
        public bool plotHist;
        public int RplotType;
        public int paletteType;
        public string palettename;
        public string eColor;
        public string customCol;
        public bool showCorr;
        public bool showYXLine;
        public string corrRange;
        public decimal minCorr;
        public decimal maxCorr;
        public string tempFile;
        public bool trBckgrnd;
        public bool stamp;
        public bool mblShowOverlap;
        public bool mblShowLoess;
        public decimal fontScale;
        public ArrayList Datasets = new ArrayList();
        public ArrayList CheckedDatasets = new ArrayList();
        private string lowC, midC, highC;

        public clsCorrelationPar()
        {
            rcmd = ""; 
            datasubset = null;
            Rdataset = "";
            mstrDatasetName = "";
            plotHist = true;
            RplotType = 2;
            
            paletteType = Settings.Default.colorMapType;
            if (paletteType == null)
                paletteType = 4;
            palettename = Settings.Default.colorMap;
            if (palettename == "")
                palettename = "Black-Body";
            
            eColor = Settings.Default.ellipseCol;
            if (eColor == "")
                eColor = clsHexColorUtil.ColorToHex(Color.FromKnownColor(KnownColor.Green));
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
            showCorr = false;
            showYXLine = true;
            corrRange = "corRange=c(0,1)";
            minCorr = 0.0M;
            maxCorr = 1.0M;
            trBckgrnd = false;
            fontScale = 1.0M;
            stamp = false;
            mblShowOverlap = false;
            mblShowLoess = false;
        }

        #region Properties

        public string RplotFunc
        {
            get
            {
                string func = "plotScatterCorr";
                switch (RplotType)
                {
                    case 1: func = "plotScatterCorr"; break;
                    case 2: func = "plotHeatmapCorr"; break;
                    case 3: func = "plotEllipseCorr"; break;
                    case 4: func = "plot2Dmat"; break;
                    default: break;
                }
                return func;
            }
        }

        public string Palette
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

        public string PlotHist
        {
            get
            {
                if (plotHist)
                    return "dHIST = TRUE";
                else 
                    return "dHIST = FALSE";
            }
        }

        public string ShowCorr
        {
            get
            {
                if (showCorr)
                    return "show.vals=TRUE";
                else
                    return "show.vals=FALSE";
            }
        }

        public string EColor
        {
            get
            {
                return @"color=""" + eColor + "\"";
            }
        }

        public string FontScale
        {
            get
            {
                return ("labelscale=" + fontScale.ToString());
            }
        }

        public string Background
        {
            get
            {
                if (trBckgrnd)
                    return "bkground=\"transparent\"";
                else
                    return "bkground=\"white\"";
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

        private string ShowOverlap
        {
            get
            {
                if (mblShowOverlap)
                    return "showOverlap = TRUE";
                else
                    return "showOverlap = FALSE";
            }
        }

        private string ShowLoess
        {
            get
            {
                if (mblShowOverlap)
                    return "showloess = TRUE";
                else
                    return "showloess = FALSE";
            }
        }

        public string Rcmd
        {
            get
            {
                string Rfunction = RplotFunc;

                rcmd = Rfunction + "(" + Rdataset + "[," + datasubset + "]," + this.Background + ",";
                rcmd = rcmd + this.Stamp + @",file=""" + tempFile + "\"";

                if (Rfunction.Equals("plotHeatmapCorr"))
                {
                    rcmd = rcmd + "," + this.FontScale + "," + Palette + "," + customCol + "," + corrRange + @")";
                }
                else if (Rfunction.Equals("plotEllipseCorr"))
                {
                    rcmd = rcmd + "," + this.FontScale + "," + EColor + @")";
                }
                else if (Rfunction.Equals("plot2Dmat"))
                {
                    rcmd = rcmd + "," + ShowCorr + "," + Palette + "," + customCol + "," + corrRange + @")";
                }
                else if (showYXLine)
                    rcmd = rcmd + "," + this.ShowOverlap + "," + this.ShowLoess + "," + 
                        PlotHist + ",regL=TRUE" + @")";
                else
                    rcmd = rcmd + "," + this.ShowOverlap + "," + this.ShowLoess + "," + 
                        PlotHist + @")";
                
                return rcmd;
            }
        }
        

        #endregion
    }
}
