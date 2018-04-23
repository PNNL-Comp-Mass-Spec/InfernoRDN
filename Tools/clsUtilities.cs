using System;
using System.Globalization;
using System.Windows.Forms;

namespace DAnTE.Tools
{
    static class clsUtilities
    {
        public enum eCheckState
        {
            checkNone = 0,
            checkDefault = 1,
            checkAll = 2,
        }

        /// <summary>
        /// Parse a string-based number into a double, using InvariantInfo by default
        /// </summary>
        /// <param name="textValue"></param>
        /// <param name="value"></param>
        /// <returns>True if success, false if unable to convert</returns>
        public static bool ParseDouble(string textValue, out double value)
        {
            return double.TryParse(textValue,
                                   NumberStyles.Float | NumberStyles.AllowThousands,
                                   NumberFormatInfo.InvariantInfo,
                                   out value) ||
                   double.TryParse(textValue, out value);
        }

        /// <summary>
        /// Parse a string-based number into a float, using InvariantInfo by default
        /// </summary>
        /// <param name="textValue"></param>
        /// <param name="value"></param>
        /// <returns>True if success, false if unable to convert</returns>
        public static bool ParseFloat(string textValue, out float value)
        {
            return float.TryParse(textValue,
                                   NumberStyles.Float | NumberStyles.AllowThousands,
                                   NumberFormatInfo.InvariantInfo,
                                   out value) ||
                   float.TryParse(textValue, out value);
        }

        public static eCheckState ToggleListViewCheckboxes(ListView lstViewDataSets, int defaultMaxChecked,
                                                           bool allowCheckAll)
        {
            var N = Math.Min(lstViewDataSets.Items.Count, defaultMaxChecked);

            // If none of the items are checked, check N or defaultMaxChecked of them
            // If some of the items are checked, check all of them, but show a warning
            // If all the items are checked, uncheck all of them

            var checkCount = 0;
            eCheckState checkStateNew;

            foreach (ListViewItem item in lstViewDataSets.Items)
            {
                if (item.Checked)
                    checkCount++;
            }

            if (checkCount == 0)
            {
                checkStateNew = eCheckState.checkDefault;
            }
            else if (checkCount < lstViewDataSets.Items.Count)
            {
                checkStateNew = eCheckState.checkAll;
            }
            else
            {
                checkStateNew = eCheckState.checkNone;
            }

            for (var i = 0; i < lstViewDataSets.Items.Count; i++)
            {
                if (checkStateNew == eCheckState.checkNone)
                {
                    lstViewDataSets.Items[i].Checked = false;
                }
                else
                {
                    if (i < N || (checkStateNew == eCheckState.checkAll && allowCheckAll))
                        lstViewDataSets.Items[i].Checked = true;
                    else
                        lstViewDataSets.Items[i].Checked = false;
                }
            }

            return checkStateNew;
        }
    }
}