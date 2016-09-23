using System.Collections.Generic;
using System.Drawing;
using System.Text;
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
        public List<string> Datasets = new List<string>();
        public List<string> CheckedDatasets = new List<string>();

        public clsCorrelationPar()
        {
            rcmd = "";
            datasubset = null;
            Rdataset = "";
            mstrDatasetName = "";
            plotHist = true;
            RplotType = 2;

            if (string.IsNullOrEmpty(Settings.Default.colorMap))
            {
                // Default palette for Correlation is Black-Body (4)
                paletteType = 4;
            }
            else
            {
                paletteType = Settings.Default.colorMapType;
            }

            palettename = Settings.Default.colorMap;
            if (palettename == "")
                palettename = "Black-Body";

            eColor = Settings.Default.ellipseCol;
            if (string.IsNullOrWhiteSpace(eColor))
                eColor = clsHexColorUtil.ColorToHex(Color.FromKnownColor(KnownColor.Green));
            var lowC = Settings.Default.colCustLow;
            var midC = Settings.Default.colCustMid;
            var highC = Settings.Default.colCustHigh;
            if (string.IsNullOrWhiteSpace(lowC) || string.IsNullOrWhiteSpace(midC) || string.IsNullOrWhiteSpace(highC))
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
                var func = "plotScatterCorr";
                switch (RplotType)
                {
                    case 1:
                        func = "plotScatterCorr";
                        break;
                    case 2:
                        func = "plotHeatmapCorr";
                        break;
                    case 3:
                        func = "plotEllipseCorr";
                        break;
                    case 4:
                        func = "plot2Dmat";
                        break;
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
            get { return @"color=""" + eColor + "\""; }
        }

        public string FontScale
        {
            get { return ("labelscale=" + fontScale); }
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
                var Rfunction = RplotFunc;
                var commandBuilder = new StringBuilder();

                commandBuilder.Append(Rfunction + "(" + Rdataset + "[," + datasubset + "]," + Background + ",");
                commandBuilder.Append(Stamp + @",file=""" + tempFile + "\"");

                if (Rfunction.Equals("plotHeatmapCorr"))
                {
                    commandBuilder.Append("," + FontScale + "," + Palette + "," + customCol + "," + corrRange + @")");
                }
                else if (Rfunction.Equals("plotEllipseCorr"))
                {
                    commandBuilder.Append("," + FontScale + "," + EColor + @")");
                }
                else if (Rfunction.Equals("plot2Dmat"))
                {
                    commandBuilder.Append("," + ShowCorr + "," + Palette + "," + customCol + "," + corrRange + @")");
                }
                else
                {
                    if (showYXLine)
                        commandBuilder.Append("," + ShowOverlap + "," + ShowLoess + "," + PlotHist + ",regL=TRUE" + @")");
                    else
                        commandBuilder.Append("," + ShowOverlap + "," + ShowLoess + "," + PlotHist + @")");
                }

                rcmd = commandBuilder.ToString();
                return rcmd;
            }
        }

        #endregion
    }
}