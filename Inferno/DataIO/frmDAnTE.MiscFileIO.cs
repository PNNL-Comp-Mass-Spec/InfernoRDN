using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DAnTE.Paradiso;
using DAnTE.Properties;
using DAnTE.Tools;

namespace DAnTE.Inferno
{
	partial class frmDAnTE
	{

		private bool DeleteTempFile(string tempfile)
		{
			bool ok = true;
			tempfile = tempfile.Replace("/", "\\");

			if (File.Exists(tempfile))
			{
				try
				{
					rConnector.EvaluateNoReturn("graphics.off()");
					File.Delete(tempfile);
				}
				catch (Exception ex)
				{
					ok = false;
					Console.WriteLine(ex.Message);
				}
			}
			return ok;
		}

		/// <summary>
		/// Get the Image from file "tempFile" and delete the tempFile
		/// </summary>
		private Image LoadImage(string tempFile)
		{
			Image currImg = null;
			using (FileStream fs = new FileStream(tempFile, FileMode.Open,
								FileAccess.Read, FileShare.ReadWrite))
			{
				Image img = Image.FromStream(fs);
				fs.Close();
				currImg = img.Clone() as Image;
				img.Dispose();
				File.Delete(tempFile);
			}
			return currImg;
		}

		/// <summary>
		/// Fireup a file open dialog and get the file name to open.
		/// </summary>
		private void GetOpenFileName(string filter)
		{
			const string WORKING_FOLDER = "Working_Directory";

			string workingFolder = Settings.Default.WorkingFolder;

			if (string.IsNullOrWhiteSpace(workingFolder))
			{
				workingFolder = RegistryAccess.GetStringRegistryValue(WORKING_FOLDER, "");
			}

			if (!string.IsNullOrWhiteSpace(workingFolder))
			{
				try
				{
					var diWorkingFolder = new DirectoryInfo(workingFolder);
					while (!diWorkingFolder.Exists)
					{
						if (diWorkingFolder.Parent == null)
						{
							workingFolder = string.Empty;
							break;
						}
						diWorkingFolder = diWorkingFolder.Parent;
						workingFolder = diWorkingFolder.FullName;
					}
				}
				catch
				{
					workingFolder = string.Empty;
				}			
			}

			OpenFileDialog fdlg = new OpenFileDialog();
			fdlg.Title = mstrFldgTitle;
			if (!string.IsNullOrWhiteSpace(workingFolder))
			{				
				fdlg.InitialDirectory = workingFolder;
			}
			else
			{
				fdlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			}
			fdlg.Filter = filter;
			fdlg.FilterIndex = 1;
			fdlg.RestoreDirectory = false;
			if (fdlg.ShowDialog() == DialogResult.OK)
			{
				mstrLoadedfileName = fdlg.FileName;

				var fiDataFile = new FileInfo(mstrLoadedfileName);
				if (fiDataFile.Exists)
				{
					mstrLoadedfileName = fiDataFile.FullName;

					workingFolder = fiDataFile.Directory.FullName;
					Settings.Default.WorkingFolder = workingFolder;
					Settings.Default.DataFileName = fiDataFile.Name;
					Settings.Default.Save();

					RegistryAccess.SetStringRegistryValue(WORKING_FOLDER, workingFolder);

				}
			}
			else
				mstrLoadedfileName = null;
		}

		private void GetSaveFileName(string filter)
		{
			string workingFolder = Settings.Default.WorkingFolder;

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
				mstrLoadedfileName = fdlg.FileName;
				workingFolder = Path.GetDirectoryName(mstrLoadedfileName);
				Settings.Default.WorkingFolder = workingFolder;
				Settings.Default.Save();
			}
			else
				mstrLoadedfileName = null;
		}

		/// <summary>
		/// Creates a new datatable with first column having unique rowIDs and
		/// multiple columns with data
		/// </summary>
		/// <param name="RowID"></param>
		/// <param name="DataCols"></param>
		/// <returns></returns>
		private DataTable ArrangeDataTable(DataTable mDT, string RowID, string[] DataCols)
		{
			DataTable dtEset = mDT.Copy();
			DataColumnCollection columns = dtEset.Columns;
			Array.Sort(DataCols);
			string[] Cols2Rem = new string[dtEset.Columns.Count -
									DataCols.Length - 1];

			int z = 0;
			foreach (DataColumn column in columns)
			{
				if (Array.BinarySearch(DataCols, column.ColumnName) < 0 &&
					!column.ColumnName.Equals(RowID))
					Cols2Rem[z++] = column.ColumnName;
			}
			foreach (string s in Cols2Rem)
			{
				dtEset.Columns.Remove(s);
			}
			return dtEset;
		}

		/// <summary>
		/// Overloaded method of the above to create a table with only two columns.
		/// </summary>
		/// <param name="RowID"></param>
		/// <param name="Cols"></param>
		/// <returns></returns>
		private DataTable ArrangeDataTable(DataTable mDT, string RowID, string Cols)
		{
			DataTable dtEset = mDT.Copy();
			DataColumnCollection columns = dtEset.Columns;
			string[] Cols2Rem = new string[dtEset.Columns.Count - 2];

			int z = 0;
			foreach (DataColumn column in columns)
			{
				if (!Cols.Equals(column.ColumnName) && !RowID.Equals(column.ColumnName))
					Cols2Rem[z++] = column.ColumnName;
			}
			foreach (string s in Cols2Rem)
			{
				dtEset.Columns.Remove(s);
			}
			return dtEset;
		}


		/// <summary>
		/// Rearrange the protein info columns in a datatable,
		/// then send it to R and 'clean' it.
		/// Finally get the 'cleaned' data back
		/// </summary>
		/// <param name="mDT"></param>
		/// <param name="protIPI"></param>
		/// <param name="rowID"></param>
		/// <returns></returns>
		private DataTable LoadProtColumns(DataTable mDT, string protIPI, string rowID)
		{
			DataTable mDTProts = new DataTable();
			string rcmd = null;

			try
			{
				mDTProts = ArrangeDataTable(mDT, rowID, protIPI);
				mDTProts.TableName = "ProtInfo";
				mDTProts.Columns[0].ColumnName = "Row_ID";
				mstrArrProteins = clsDataTable.DataColumn2strArray(mDT, protIPI);
				mstrArrMassTags = clsDataTable.DataColumn2strArray(mDT, rowID);
				try
				{
					rConnector.SetSymbolCharVector("protIPI", mstrArrProteins);
					rConnector.SetSymbolCharVector("MassTags", mstrArrMassTags);
					rcmd = "ProtInfo<-data.frame(Row_ID=MassTags,ProteinID=protIPI)";
					rConnector.EvaluateNoReturn(rcmd);
					if (mhtDatasets.ContainsKey("Expressions"))
						rcmd = "ProtInfo<-remove.emptyProtInfo(ProtInfo, checkEset=TRUE)";
					else
						rcmd = "ProtInfo<-remove.emptyProtInfo(ProtInfo)";
					rConnector.EvaluateNoReturn(rcmd);

					clsRarray.rowNamesID = "RowID"; // Otherwise this will conflict with 'Row_ID'
					if (rConnector.GetTableFromRProtInfoMatrix("ProtInfo"))
					{
						clsRarray.rowNamesID = "Row_ID";
						mDTProts = rConnector.DataTable.Copy();
						mDTProts.TableName = "ProtInfo";
						if (mDTProts.Columns.CanRemove(mDTProts.Columns[0]))
						{//remove the line number column which comes from R row names.
							mDTProts.Columns.Remove(mDTProts.Columns[0]);
							mDTProts.Columns[0].ColumnName = "Row_ID";
						}
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("Error: " + ex.Message, "Exception while talking to R");
					mDTProts = null;
				}

			}
			catch (Exception ex)
			{
				MessageBox.Show("Error: " + ex.Message,
					"Exception while extracting data columns");
				mDTProts = null;
			}
			return mDTProts;
		}

		private DataTable GetProtInfoMatrix()
		{
			DataTable mDTProtInfo = new DataTable();

			clsRarray.rowNamesID = "RowID"; // Otherwise this will conflict with 'Row_ID'
			if (rConnector.GetTableFromRProtInfoMatrix("ProtInfo"))
			{
				clsRarray.rowNamesID = "Row_ID";
				mDTProtInfo = rConnector.DataTable.Copy();
				mDTProtInfo.TableName = "ProtInfo";
				if (mDTProtInfo.Columns.CanRemove(mDTProtInfo.Columns[0]))
				{//remove the line number column which comes from R row names.
					mDTProtInfo.Columns.Remove(mDTProtInfo.Columns[0]);
					mDTProtInfo.Columns[0].ColumnName = "Row_ID";
				}
			}
			else
				mDTProtInfo = null;
			return mDTProtInfo;
		}

		private DataTable OpenFile_test(string filename)
		{
			string rowID = null;
			string[] dataCols;
			//bool success = true;
			//bool FactorsValid = true;
			string fExt = ".csv";
			DataTable mDTtmp = new DataTable();
			DataTable mDTloaded = new DataTable();
			DataTable mDTselectedEset1 = new DataTable();
			DataTable mDTFactors = new DataTable();

			// check that the file exists before opening it
			if (File.Exists(mstrLoadedfileName))
			{
				fExt = Path.GetExtension(filename);
				if (!(fExt.Equals(".csv") || fExt.Equals(".txt") || fExt.Equals(".xls")))
				{
					MessageBox.Show("Filetype not allowed.", "Error!");
					return null;
				}

				//FactorsValid = true;
				mDTloaded = clsDataTable.LoadFile2DataTableFastCSVReader(mstrLoadedfileName);
				if (mDTloaded == null)
				{
					return null;
				}
				mDTloaded.TableName = "AllEset";
				//Select columns
				frmSelectColumns mfrmSelectCols = new frmSelectColumns();
				mfrmSelectCols.PopulateListBox = clsDataTable.DataTableColumns(mDTloaded, false);
				mfrmSelectCols.Proteins = mhtDatasets.ContainsKey("Protein Info");
				if (mfrmSelectCols.ShowDialog() == DialogResult.OK)
				{
					rowID = mfrmSelectCols.RowIDColumn; //mass tags
					dataCols = mfrmSelectCols.DataColumns; //dataset columns
					try
					{
						mDTselectedEset1 = ArrangeDataTable(mDTloaded, rowID, dataCols); // create the datatable
						mDTselectedEset1.TableName = "Eset";
					}
					catch (Exception ex)
					{
						MessageBox.Show("Error: " + ex.Message,
							"Exception while extracting data columns");
						//success = false;
						return null;
					}
				}
			}
			return mDTselectedEset1;
		}


		/// <summary>
		/// Main routine that opens data files (Expressions, Factors)
		/// in comma or tab delimited CSVs (and Excel as a future possibility).
		/// The main file load routine is in clsDataTable.LoadFile2DataTable(fileName)
		/// Duplicates are handled in C#
		/// </summary>
		private bool OpenFile(string filename)
		{
			string rowID = null, protIPI = null;
			string[] dataCols;
			string rcmd = null;
			bool success = true;
			bool FactorsValid = true;
			string fExt = ".csv";
			DataTable mDTtmp = new DataTable();
			DataTable mDTloaded = new DataTable();
			DataTable mDTselectedEset1 = new DataTable();
			DataTable mDTFactors = new DataTable();

			// check that the file exists before opening it
			if (File.Exists(mstrLoadedfileName))
			{
				fExt = Path.GetExtension(filename);
				if (!(fExt.Equals(".csv") || fExt.Equals(".txt") || fExt.Equals(".xls") || fExt.Equals(".xlsx")))
				{
					MessageBox.Show("Filetype not allowed.", "Error!");
					return false;
				}

				switch (dataSetType)
				{
					case enmDataType.ESET:
						#region Load Expressions
						FactorsValid = true;
						mDTloaded = clsDataTable.LoadFile2DataTableFastCSVReader(mstrLoadedfileName);
						if (mDTloaded == null)
						{
							return false;
						}
						mDTloaded.TableName = "AllEset";
						//Select columns
						frmSelectColumns mfrmSelectCols = new frmSelectColumns();
						mfrmSelectCols.PopulateListBox = clsDataTable.DataTableColumns(mDTloaded, false);
						mfrmSelectCols.Proteins = mhtDatasets.ContainsKey("Protein Info");
						if (mfrmSelectCols.ShowDialog() == DialogResult.OK)
						{
							rowID = mfrmSelectCols.RowIDColumn; //mass tags
							dataCols = mfrmSelectCols.DataColumns; //dataset columns
							try
							{
								mDTselectedEset1 = ArrangeDataTable(mDTloaded, rowID, dataCols); // create the datatable
								mDTselectedEset1.Columns[0].ColumnName = "Row_ID";
								mDTselectedEset1 = clsDataTable.RemoveDuplicateRows2(mDTselectedEset1,
									mDTselectedEset1.Columns[0].ColumnName); // handle duplicate rows
								mDTselectedEset1.TableName = "Eset";
								success = rConnector.SendTable2RmatrixNumeric("Eset", mDTselectedEset1);
								if (mhtDatasets.ContainsKey("Factors"))
								{
									rcmd = "FactorsValid<-identical(as.array(colnames(factors)),as.array(colnames(Eset)))";
									rConnector.EvaluateNoReturn(rcmd);
									FactorsValid = rConnector.GetSymbolAsBool("FactorsValid");
								}
								if (!FactorsValid)
								{
									success = false;
								}
								if (success)
								{
									AddDataset2HashTable(mDTselectedEset1);
									rConnector.EvaluateNoReturn("print(dim(Eset))");
									rConnector.EvaluateNoReturn("cat(\"Expressions loaded.\n\")");
								}
							}
							catch (Exception ex)
							{
								MessageBox.Show("Error: " + ex.Message,
									"Exception while extracting data columns");
								success = false;
								return false;
							}

							if (mfrmSelectCols.Proteins && success) // Protein info needs to be loaded ?
							{
								// loads to mDataTableProtInfo and then sends to R
								DataTable mdtProts = LoadProtColumns(mDTloaded, mfrmSelectCols.ProteinIDColumn, rowID);
								mdtProts.TableName = "ProtInfo";
								AddDataset2HashTable(mdtProts);
							}
						}
						else
						{
							success = false;
						}
						#endregion
						break;
					case enmDataType.PROTINFO:
						#region Load Protein Info
						mDTtmp = clsDataTable.LoadFile2DataTableFastCSVReader(mstrLoadedfileName);
						if (mDTtmp == null)
						{
							return false;
						}
						frmSelectProtInfo mfrmSelectProts = new frmSelectProtInfo();
						mfrmSelectProts.PopulateListBox = clsDataTable.DataTableColumns(mDTtmp, false);
						if (mfrmSelectProts.ShowDialog() == DialogResult.OK)
						{
							rowID = mfrmSelectProts.RowIDColumn; //mass tags
							protIPI = mfrmSelectProts.ProteinIDColumn; //IPI# column
							DataTable mdtProts = LoadProtColumns(mDTtmp, protIPI, rowID);
							mdtProts.TableName = "ProtInfo";
							AddDataset2HashTable(mdtProts);
						}
						else
						{
							success = false;
						}
						#endregion
						break;
					case enmDataType.FACTORS:
						#region Load Factors
						FactorsValid = true;
						mDTFactors = clsDataTable.LoadFile2DataTableFastCSVReader(mstrLoadedfileName);
						if (mDTFactors == null)
						{
							return false;
						}
						if (mDTFactors.Rows.Count > 20)
						{
							return false;
						}
						if (rConnector.SendTable2RmatrixNonNumeric("factors", mDTFactors))
						{
							try
							{
								if (mhtDatasets.ContainsKey("Expressions"))
								{
									rcmd = "FactorsValid<-identical(as.array(colnames(factors)),as.array(colnames(Eset)))";
									rConnector.EvaluateNoReturn(rcmd);
									FactorsValid = rConnector.GetSymbolAsBool("FactorsValid");
								}
								if (!FactorsValid)
								{
									success = false;
								}
								else
								{
									UpdateFactorInfoArray();
									mDTFactors.Columns[0].ColumnName = "Factors";
									mDTFactors.TableName = "factors";
									rConnector.EvaluateNoReturn("print(factors)");
									rConnector.EvaluateNoReturn("cat(\"Factors loaded.\n\")");
								}
							}
							catch (Exception ex)
							{
								MessageBox.Show("Error: " + ex.Message, "Exception while talking to R");
								success = false;
							}
						}
						if (success)
							AddDataset2HashTable(mDTFactors);
						#endregion
						break;
					default:
						break;
				}
			}
			else
				success = false;
			return success;
		}

		/// <summary>
		/// Duplicates are handled in R
		/// </summary>
		/// <param name="filename"></param>
		/// <returns></returns>
		private bool OpenFile2(string filename)
		{
			string rowID = null, protIPI = null;
			string[] dataCols;
			string rcmd = null;
			bool success = true;
			bool FactorsValid = true;
			string fExt = ".csv";
			DataTable mDTtmp = new DataTable();
			DataTable mDTloaded = new DataTable();
			DataTable mDTselectedEset1 = new DataTable();
			DataTable mDTFactors = new DataTable();

			// check that the file exists before opening it
			if (File.Exists(mstrLoadedfileName))
			{
				fExt = Path.GetExtension(filename);
				if (!(fExt.Equals(".csv") || fExt.Equals(".txt") || fExt.Equals(".xls")))
				{
					MessageBox.Show("Filetype not allowed.", "Error!");
					return false;
				}

				switch (dataSetType)
				{
					case enmDataType.ESET:
						#region Load Expressions
						FactorsValid = true;
						mDTloaded = clsDataTable.LoadFile2DataTableFastCSVReader(mstrLoadedfileName);
						if (mDTloaded == null)
						{
							return false;
						}
						mDTloaded.TableName = "AllEset";
						//Select columns
						frmSelectColumns mfrmSelectCols = new frmSelectColumns();
						mfrmSelectCols.PopulateListBox = clsDataTable.DataTableColumns(mDTloaded, false);
						mfrmSelectCols.Proteins = mhtDatasets.ContainsKey("Protein Info");
						if (mfrmSelectCols.ShowDialog() == DialogResult.OK)
						{
							rowID = mfrmSelectCols.RowIDColumn; //mass tags
							dataCols = mfrmSelectCols.DataColumns; //dataset columns
							try
							{
								mDTselectedEset1 = ArrangeDataTable(mDTloaded, rowID, dataCols); // create the datatable
								mDTselectedEset1.TableName = "Eset";
							}
							catch (Exception ex)
							{
								MessageBox.Show("Error: " + ex.Message,
									"Exception while extracting data columns");
								success = false;
								return false;
							}

							//clsRarray.rowNamesID = mDTselectedEset.Columns[0].ToString();
							clsRarray.rowNamesID = "Row_ID";
							if (rConnector.SendTable2RmatrixNumeric("Eset", mDTselectedEset1) && success)
							// Duplicates are handled during the call 'SendTable2RmatrixNumeric'
							{
								try
								{
									if (mhtDatasets.ContainsKey("Factors"))
									{
										rcmd = "FactorsValid<-identical(as.array(colnames(factors)),as.array(colnames(Eset)))";
										rConnector.EvaluateNoReturn(rcmd);
										FactorsValid = rConnector.GetSymbolAsBool("FactorsValid");
									}
									if (!FactorsValid)
									{
										success = false;
									}
									else
									{
										rConnector.GetTableFromRmatrix("Eset"); // Get the cleaned data matrix
										mDTselectedEset1 = rConnector.DataTable.Copy();
										mDTselectedEset1.TableName = "Eset";
										rConnector.EvaluateNoReturn("print(dim(Eset))");
										rConnector.EvaluateNoReturn("cat(\"Expressions loaded.\n\")");
										AddDataset2HashTable(mDTselectedEset1);
									}
								}
								catch (Exception ex)
								{
									MessageBox.Show("Error: " + ex.Message, "Exception while talking to R");
									success = false;
								}
							}
							else
							{
								success = false;
							}
							if (mfrmSelectCols.Proteins && success) // Protein info needs to be loaded ?
							{
								// loads to mDataTableProtInfo and then sends to R
								DataTable mdtProts = LoadProtColumns(mDTloaded, mfrmSelectCols.ProteinIDColumn, rowID);
								mdtProts.TableName = "ProtInfo";
								AddDataset2HashTable(mdtProts);
							}
						}
						else
						{
							success = false;
						}
						#endregion
						break;
					case enmDataType.PROTINFO:
						#region Load Protein Info
						mDTtmp = clsDataTable.LoadFile2DataTableFastCSVReader(mstrLoadedfileName);
						if (mDTtmp == null)
						{
							return false;
						}
						frmSelectProtInfo mfrmSelectProts = new frmSelectProtInfo();
						mfrmSelectProts.PopulateListBox = clsDataTable.DataTableColumns(mDTtmp, false);
						if (mfrmSelectProts.ShowDialog() == DialogResult.OK)
						{
							rowID = mfrmSelectProts.RowIDColumn; //mass tags
							protIPI = mfrmSelectProts.ProteinIDColumn; //IPI# column
							DataTable mdtProts = LoadProtColumns(mDTtmp, protIPI, rowID);
							mdtProts.TableName = "ProtInfo";
							AddDataset2HashTable(mdtProts);
						}
						else
						{
							success = false;
						}
						#endregion
						break;
					case enmDataType.FACTORS:
						#region Load Factors
						FactorsValid = true;
						mDTFactors = clsDataTable.LoadFile2DataTableFastCSVReader(mstrLoadedfileName);
						if (mDTFactors == null)
						{
							return false;
						}
						if (mDTFactors.Rows.Count > 20)
						{
							return false;
						}
						if (rConnector.SendTable2RmatrixNonNumeric("factors", mDTFactors))
						{
							try
							{
								if (mhtDatasets.ContainsKey("Expressions"))
								{
									rcmd = "FactorsValid<-identical(as.array(colnames(factors)),as.array(colnames(Eset)))";
									rConnector.EvaluateNoReturn(rcmd);
									FactorsValid = rConnector.GetSymbolAsBool("FactorsValid");
								}
								if (!FactorsValid)
								{
									success = false;
								}
								else
								{
									UpdateFactorInfoArray();
									mDTFactors.Columns[0].ColumnName = "Factors";
									mDTFactors.TableName = "factors";
									rConnector.EvaluateNoReturn("print(factors)");
									rConnector.EvaluateNoReturn("cat(\"Factors loaded.\n\")");
								}
							}
							catch (Exception ex)
							{
								MessageBox.Show("Error: " + ex.Message, "Exception while talking to R");
								success = false;
							}
						}
						if (success)
							AddDataset2HashTable(mDTFactors);
						#endregion
						break;
					default:
						break;
				}
			}
			else
				success = false;
			return success;
		}


		private void UpdateFactorInfoArray()
		{
			clsFactorInfo currFactorInfo;
			ArrayList marrFV = new ArrayList();
		
			marrFactorInfo.Clear();
			try
			{
				rConnector.EvaluateNoReturn("factorNames <- rownames(factors)");
				var factorNames = rConnector.GetSymbolAsStrings("factorNames");
				string[] factors = factorNames;
				for (int numF = 0; numF < factors.Length; numF++)
				{
					marrFV.Clear();
					rConnector.EvaluateNoReturn("fVals <- unique(factors[" +
						Convert.ToString(numF + 1) + ",])");
					rConnector.EvaluateNoReturn("nfVals <- length(fVals)");
					var fVals = rConnector.GetSymbolAsStrings("fVals");
					var nfVals = rConnector.GetSymbolAsNumbers("nfVals");
					if ((int)nfVals[0] > 1)
					{ //more than one factor value
						for (int i = 0; i < fVals.Length; i++)
							marrFV.Add(fVals[i]);
					}
					else
					{
						marrFV.Add(fVals[0]);
					}
					currFactorInfo = new clsFactorInfo();
					currFactorInfo.SetFvals = marrFV;
					currFactorInfo.mstrFactor = factors[numF];
					marrFactorInfo.Add(currFactorInfo);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error: " + ex.Message, "Exception while talking to R");
			}
		}


		public void OpenSessionThreaded(string mstrFile)
		{
			bool doLoad = true;
			if (mtabControlData.Controls.Count != 0)
			//    if (mblEsetLoaded || mblFactorsLoaded || mblProtInfoLoaded || mblLogEsetOK)
			{
				doLoad = (MessageBox.Show("If you load a saved session, current data will be lost. Continue?",
					"Continue?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes);
			}
			if (doLoad)
			{
				#region Threading
				m_BackgroundWorker.DoWork += new DoWorkEventHandler(m_BackgroundWorker_OpenSession);
				m_BackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
					m_BackgroundWorker_SessionOpenCompleted);
				#endregion

				marrAnalysisObjects.Clear();
				mhtAnalysisObjects.Clear();

				m_BackgroundWorker.RunWorkerAsync(mstrFile);
				mfrmShowProgress.Message = "Loading Saved Session ...";
				mfrmShowProgress.ShowDialog();

				#region Threading
				m_BackgroundWorker.DoWork -= new DoWorkEventHandler(m_BackgroundWorker_OpenSession);
				m_BackgroundWorker.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(
					m_BackgroundWorker_SessionOpenCompleted);
				#endregion
			}
		}

		private string NumericVars2R()
		{
			string vars = "c("; // dummy to make vars an array of strings always

			vars += NumericVars();

			vars = vars.Substring(0, vars.LastIndexOf(","));
			vars = vars + ")";

			return vars;
		}
	}
}
