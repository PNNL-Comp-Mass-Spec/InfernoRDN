using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DAnTE.Properties;

namespace DAnTE.Inferno
{
    public partial class frmAnalysisSummary : Form
    {
        private Hashtable mhtSummaries = new Hashtable();
        private ArrayList marrAnalyses = new ArrayList();
        private string mstrFileName = null;
        private DateTime CurrTime = DateTime.Now;
        private string mstrTime = null; 

        public frmAnalysisSummary()
        {
            InitializeComponent();
            mstrTime = CurrTime.ToString("G", System.Globalization.CultureInfo.CreateSpecificCulture("en-us"));
        }


        private void FillSummaryListView()
        {
            mlstViewSummary.Columns.Add("Parameter", 200, HorizontalAlignment.Left);
            mlstViewSummary.Columns.Add("Value", 350, HorizontalAlignment.Left);

            for (int i = 0; i < marrAnalyses.Count; i++)
            {
                object o = ((DAnTE.Tools.clsAnalysisObject)marrAnalyses[i]).AnalysisObject;
                string strKey = ((DAnTE.Tools.clsAnalysisObject)marrAnalyses[i]).Operation;

                ListViewGroup grp = new ListViewGroup(strKey, HorizontalAlignment.Left);
                mlstViewSummary.Groups.Add(grp);

                System.Reflection.PropertyInfo[] props = o.GetType().GetProperties();
                foreach (System.Reflection.PropertyInfo prop in props)
                {
                    try
                    {
                        object[] customAttributes = prop.GetCustomAttributes(typeof(DAnTE.Tools.clsAnalysisAttribute), true);
                        if (customAttributes.Length > 0 && prop.CanRead)
                        {
                            DAnTE.Tools.clsAnalysisAttribute attr = customAttributes[0] as DAnTE.Tools.clsAnalysisAttribute;
                            object objectValue = prop.GetValue(o, System.Reflection.BindingFlags.GetProperty,
                                null, null, null);
                            if (objectValue != null && attr != null)
                            {
                                ListViewItem tmpItem = new ListViewItem(attr.Description, grp);
                                tmpItem.SubItems.Add(objectValue.ToString());
                                mlstViewSummary.Items.Add(tmpItem);
                            }
                        }
                    }
                    catch
                    {
                    }
                }
                foreach (System.Reflection.FieldInfo field in o.GetType().GetFields())
                {
                    try
                    {
                        object[] customAttributes = field.GetCustomAttributes(typeof(DAnTE.Tools.clsAnalysisAttribute), true);
                        if (customAttributes.Length > 0)
                        {
                            DAnTE.Tools.clsAnalysisAttribute attr = customAttributes[0] as DAnTE.Tools.clsAnalysisAttribute;
                            object objectValue = field.GetValue(o);
                            if (objectValue != null && attr != null)
                            {
                                ListViewItem tmpItem = new ListViewItem(attr.Description, grp);
                                tmpItem.SubItems.Add(objectValue.ToString());
                                mlstViewSummary.Items.Add(tmpItem);
                            }
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }

        private DAnTE.Tools.MetaData FillSummaryXML()
        {
            DAnTE.Tools.MetaData metaData = new DAnTE.Tools.MetaData("DAnTE_Analysis");
            metaData.SetValue("DataFile", mstrFileName);
            metaData.SetValue("Time", mstrTime);

            for (int i = 0; i < marrAnalyses.Count; i++)
            {
                object o = ((DAnTE.Tools.clsAnalysisObject)marrAnalyses[i]).AnalysisObject;
                string strKey = ((DAnTE.Tools.clsAnalysisObject)marrAnalyses[i]).Operation;

                DAnTE.Tools.MetaNode metaNode = metaData.OpenChild(strKey);
                
                System.Reflection.PropertyInfo[] props = o.GetType().GetProperties();
                foreach (System.Reflection.PropertyInfo prop in props)
                {
                    try
                    {
                        object[] customAttributes = prop.GetCustomAttributes(typeof(DAnTE.Tools.clsAnalysisAttribute), true);
                        if (customAttributes.Length > 0 && prop.CanRead)
                        {
                            DAnTE.Tools.clsAnalysisAttribute attr = customAttributes[0] as DAnTE.Tools.clsAnalysisAttribute;
                            object objectValue = prop.GetValue(o, System.Reflection.BindingFlags.GetProperty,
                                null, null, null);
                            if (objectValue != null && attr != null)
                            {
                                metaNode.SetValue(attr.Description, objectValue.ToString());
                            }
                        }
                    }
                    catch
                    {
                        return null;
                    }
                }
                foreach (System.Reflection.FieldInfo field in o.GetType().GetFields())
                {
                    try
                    {
                        object[] customAttributes = field.GetCustomAttributes(typeof(DAnTE.Tools.clsAnalysisAttribute), true);
                        if (customAttributes.Length > 0)
                        {
                            DAnTE.Tools.clsAnalysisAttribute attr = customAttributes[0] as DAnTE.Tools.clsAnalysisAttribute;
                            object objectValue = field.GetValue(o);
                            if (objectValue != null && attr != null)
                            {
                                metaNode.SetValue(attr.Description, objectValue.ToString());
                            }
                        }
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
            return metaData;
        }


        private void frmAnalysisSummary_Load(object sender, EventArgs e)
        {
            FillSummaryListView();
            this.mlblTime.Text = mstrTime;
            this.mlblDataFile.Text = mstrFileName;
        }

        

        private void mBtnSave_Click(object sender, EventArgs e)
        {
            string fileName = GetSaveFileName("Select a file to save summary",
                "XML files (*.xml)|*.xml|Tab delimited txt files (*.txt)|*.txt");
            string fExt = System.IO.Path.GetExtension(fileName);
            if (fileName != null)
            {
                if (fExt.Equals(".xml"))
                {
                    DAnTE.Tools.MetaData metaDataXML = FillSummaryXML();
                    if (metaDataXML != null)
                    {
                        metaDataXML.WriteFile(fileName);
                    }
                }
                if (fExt.Equals(".txt"))
                {
                    using (System.IO.TextWriter streamWriter = new System.IO.StreamWriter(fileName))
                    {
                        DAnTE.Tools.CsvWriter.WriteListViewToStream(streamWriter, this.mlstViewSummary, 
                            mstrFileName, false);
                    }
                }
            }
        }

        private string GetSaveFileName(string mstrFldgTitle, string filter)
        {
            string workingFolder = Settings.Default.WorkingFolder;
            string fileName = null;

            SaveFileDialog fdlg = new SaveFileDialog();
            fdlg.Title = mstrFldgTitle;
            if (workingFolder != null)
                fdlg.InitialDirectory = workingFolder;
            else
                fdlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            fdlg.Filter = filter;
            fdlg.FilterIndex = 1;
            fdlg.RestoreDirectory = false;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                fileName = fdlg.FileName;
                workingFolder = System.IO.Path.GetDirectoryName(fileName);
                Settings.Default.WorkingFolder = workingFolder;
                Settings.Default.Save();
            }
            else
                fileName = null;

            return fileName;
        }

        #region Properties

        public Hashtable SummaryHashTable
        {
            set
            {
                mhtSummaries = value;
            }
        }

        public ArrayList SummaryArrayList
        {
            set
            {
                marrAnalyses = value;
            }
        }

        public string DataFileName
        {
            set
            {
                mstrFileName = value;
            }
        }

        #endregion
    }
}