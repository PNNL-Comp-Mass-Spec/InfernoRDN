using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public static eCheckState ToggleListViewCheckboxes(ListView lstViewDataSets, int defaultMaxChecked, bool allowCheckAll)
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
