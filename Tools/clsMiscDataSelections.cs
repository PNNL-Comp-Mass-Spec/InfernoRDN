using System;
using System.Collections.Generic;
using System.Text;

namespace DAnTE.Tools
{
    public static class clsMiscDataSelections
    {
        public static string CorrespondingRdataset(string selected)
        {
            string dataset = "Eset";

            switch (selected)
            {
                case ("Expressions"):
                    dataset = "Eset";
                    break;
                case ("Log Expressions"):
                    dataset = "logEset";
                    break;
                case ("LOESS Data"):
                    dataset = "loessData";
                    break;
                case ("Linear Regressed"):
                    dataset = "linregData";
                    break;
                case ("Mean Centered"):
                    dataset = "meanCEset";
                    break;
                case ("MAD Adjusted"):
                    dataset = "madEset";
                    break;
                case ("Imputed"):
                    dataset = "imputedData";
                    break;
                case ("Quantile"):
                    dataset = "quaNormEset";
                    break;
                case ("ScaledData(RRollup)"):
                    dataset = "sData1";
                    break;
                case ("OutliersRemoved(RRollup)"):
                    dataset = "orData1";
                    break;
                case ("Proteins(RRollup)"):
                    dataset = "pData11";
                    break;
                case ("Proteins(ZRollup)"):
                    dataset = "pData22";
                    break;
                case ("QRollup"):
                    dataset = "qrollupP1";
                    break;
                case ("Filtered Data"):
                    dataset = "filteredData";
                    break;
                case ("Merged Data"):
                    dataset = "mergedData";
                    break;
                default:
                    dataset = "Eset";
                    break;
            }
            return dataset;
        }

        public static string RealNameFromRdatasetName(string selected)
        {
            string realname = "Expressions";

            switch (selected)
            {
                case ("Eset"):
                    realname = "Expressions";
                    break;
                case ("logEset"):
                    realname = "Log Expressions";
                    break;
                case ("loessData"):
                    realname = "LOESS Data";
                    break;
                case ("linregData"):
                    realname = "Linear Regressed";
                    break;
                case ("meanCEset"):
                    realname = "Mean Centered";
                    break;
                case ("madEset"):
                    realname = "MAD Adjusted";
                    break;
                case ("imputedData"):
                    realname = "Imputed";
                    break;
                case ("quaNormEset"):
                    realname = "Quantile";
                    break;
                case ("sData1"):
                    realname = "ScaledData(RRollup)";
                    break;
                case ("orData1"):
                    realname = "OutliersRemoved(RRollup)";
                    break;
                case ("pData11"):
                case ("pData1"):
                    realname = "Proteins(RRollup)";
                    break;
                case ("pData22"):
                case ("pData2"):
                    realname = "Proteins(ZRollup)";
                    break;
                case ("qrollupP1"):
                    realname = "QRollup";
                    break;
                case ("filteredData"):
                    realname = "Filtered Data";
                    break;
                case ("mergedData"):
                    realname = "Merged Data";
                    break;
                default:
                    realname = "Expressions";
                    break;
            }
            return realname;
        }


    }
}
