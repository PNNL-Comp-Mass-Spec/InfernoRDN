using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DAnTE.Tools;

namespace DAnTE.Inferno
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class frmDefFactors : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ListBox lstBoxFactors;
		private System.Windows.Forms.ListBox lstBoxValues;
		private System.Windows.Forms.Button btnValues;
		private System.Windows.Forms.Button btnFactors;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;

		//private static int delta = 5 ;
        //private int itemSelected = -1;
        private IContainer components;

		public const int MAX_LEVELS = 100 ;
		private List<clsFactorInfo> marrFactors;
        //private ArrayList marrFactorsCopy;
		private readonly string [] strarrFactors = new string [MAX_LEVELS] ;
		private int numFactors = 0 ;
		private bool restoreFactors = false;

		private System.Windows.Forms.Button btnFactorDelete;
		private System.Windows.Forms.Button btnValueDelete;
		private System.Windows.Forms.TextBox txtBoxFactors;
        private ExtraControls.NiceLine niceLine1;
        private Label label4;
        private Panel panel2;
        private Label label5;
        private ToolTip mtltipHelp;
		private System.Windows.Forms.TextBox txtBoxValues;
		
		

		public frmDefFactors()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			//
			// TODO: Add any constructor code after InitializeComponent call
			//
            marrFactors = new List<clsFactorInfo>();
			restoreFactors = false;
		}

        public frmDefFactors(List<clsFactorInfo> factorInfoArr)
		{
			marrFactors = factorInfoArr;
			InitializeComponent();
			restoreFactors = true;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose() ;
				}
			}
			base.Dispose( disposing ) ;
		}


		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDefFactors));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lstBoxValues = new System.Windows.Forms.ListBox();
            this.niceLine1 = new DAnTE.ExtraControls.NiceLine();
            this.btnValueDelete = new System.Windows.Forms.Button();
            this.btnFactorDelete = new System.Windows.Forms.Button();
            this.txtBoxValues = new System.Windows.Forms.TextBox();
            this.txtBoxFactors = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnFactors = new System.Windows.Forms.Button();
            this.btnValues = new System.Windows.Forms.Button();
            this.lstBoxFactors = new System.Windows.Forms.ListBox();
            this.mtltipHelp = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.lstBoxValues);
            this.panel1.Controls.Add(this.niceLine1);
            this.panel1.Controls.Add(this.btnValueDelete);
            this.panel1.Controls.Add(this.btnFactorDelete);
            this.panel1.Controls.Add(this.txtBoxValues);
            this.panel1.Controls.Add(this.txtBoxFactors);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnFactors);
            this.panel1.Controls.Add(this.btnValues);
            this.panel1.Controls.Add(this.lstBoxFactors);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(530, 392);
            this.panel1.TabIndex = 10;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(530, 58);
            this.panel2.TabIndex = 17;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(498, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(16, 17);
            this.label5.TabIndex = 16;
            this.label5.Text = "?";
            this.mtltipHelp.SetToolTip(this.label5, resources.GetString("label5.ToolTip"));
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(14, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(245, 18);
            this.label4.TabIndex = 16;
            this.label4.Text = "Change or Define New Factors:";
            // 
            // lstBoxValues
            // 
            this.lstBoxValues.ItemHeight = 16;
            this.lstBoxValues.Location = new System.Drawing.Point(302, 129);
            this.lstBoxValues.Name = "lstBoxValues";
            this.lstBoxValues.Size = new System.Drawing.Size(212, 132);
            this.lstBoxValues.TabIndex = 5;
            this.lstBoxValues.Tag = "Values";
            this.lstBoxValues.SelectedIndexChanged += new System.EventHandler(this.lstBoxValues_SelectedIndexChanged);
            // 
            // niceLine1
            // 
            this.niceLine1.Location = new System.Drawing.Point(14, 310);
            this.niceLine1.Name = "niceLine1";
            this.niceLine1.Size = new System.Drawing.Size(514, 17);
            this.niceLine1.TabIndex = 14;
            // 
            // btnValueDelete
            // 
            this.btnValueDelete.Location = new System.Drawing.Point(376, 276);
            this.btnValueDelete.Name = "btnValueDelete";
            this.btnValueDelete.Size = new System.Drawing.Size(61, 27);
            this.btnValueDelete.TabIndex = 7;
            this.btnValueDelete.Text = "Delete";
            this.btnValueDelete.Click += new System.EventHandler(this.btnValueDelete_Click);
            // 
            // btnFactorDelete
            // 
            this.btnFactorDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnFactorDelete.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnFactorDelete.Location = new System.Drawing.Point(98, 276);
            this.btnFactorDelete.Name = "btnFactorDelete";
            this.btnFactorDelete.Size = new System.Drawing.Size(59, 27);
            this.btnFactorDelete.TabIndex = 6;
            this.btnFactorDelete.Text = "Delete";
            this.btnFactorDelete.Click += new System.EventHandler(this.btnFactorDelete_Click);
            // 
            // txtBoxValues
            // 
            this.txtBoxValues.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxValues.Location = new System.Drawing.Point(302, 92);
            this.txtBoxValues.Name = "txtBoxValues";
            this.txtBoxValues.Size = new System.Drawing.Size(135, 24);
            this.txtBoxValues.TabIndex = 2;
            this.txtBoxValues.Text = "Value1";
            this.txtBoxValues.TextChanged += new System.EventHandler(this.txtBoxValues_txtCh);
            this.txtBoxValues.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBoxValues_KeyPress);
            // 
            // txtBoxFactors
            // 
            this.txtBoxFactors.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxFactors.Location = new System.Drawing.Point(23, 92);
            this.txtBoxFactors.Name = "txtBoxFactors";
            this.txtBoxFactors.Size = new System.Drawing.Size(134, 24);
            this.txtBoxFactors.TabIndex = 0;
            this.txtBoxFactors.Text = "Factor1";
            this.txtBoxFactors.TextChanged += new System.EventHandler(this.txtBoxFactors_textCh);
            this.txtBoxFactors.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBoxFactors_KeyPress);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(148, 335);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(86, 27);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(302, 335);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 27);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 17);
            this.label3.TabIndex = 17;
            this.label3.Text = "Factors:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(302, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(149, 17);
            this.label2.TabIndex = 18;
            this.label2.Text = "Factor Values (levels):";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(241, 180);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 37);
            this.label1.TabIndex = 15;
            this.label1.Text = ">>";
            // 
            // btnFactors
            // 
            this.btnFactors.Location = new System.Drawing.Point(176, 89);
            this.btnFactors.Name = "btnFactors";
            this.btnFactors.Size = new System.Drawing.Size(58, 28);
            this.btnFactors.TabIndex = 1;
            this.btnFactors.Text = "Add";
            this.btnFactors.Click += new System.EventHandler(this.btnAddFactors_Click);
            // 
            // btnValues
            // 
            this.btnValues.Location = new System.Drawing.Point(456, 89);
            this.btnValues.Name = "btnValues";
            this.btnValues.Size = new System.Drawing.Size(58, 28);
            this.btnValues.TabIndex = 3;
            this.btnValues.Text = "Add";
            this.btnValues.Click += new System.EventHandler(this.btnAddValues_Click);
            // 
            // lstBoxFactors
            // 
            this.lstBoxFactors.ItemHeight = 16;
            this.lstBoxFactors.Location = new System.Drawing.Point(23, 129);
            this.lstBoxFactors.Name = "lstBoxFactors";
            this.lstBoxFactors.Size = new System.Drawing.Size(211, 132);
            this.lstBoxFactors.TabIndex = 4;
            this.lstBoxFactors.Tag = "Factors";
            this.lstBoxFactors.SelectedIndexChanged += new System.EventHandler(this.lstBoxFactors_SelectedIndexChanged);
            // 
            // mtltipHelp
            // 
            this.mtltipHelp.AutomaticDelay = 10;
            this.mtltipHelp.AutoPopDelay = 30000;
            this.mtltipHelp.InitialDelay = 10;
            this.mtltipHelp.IsBalloon = true;
            this.mtltipHelp.ReshowDelay = 10;
            this.mtltipHelp.ShowAlways = true;
            this.mtltipHelp.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.mtltipHelp.ToolTipTitle = "Factors";
            // 
            // frmDefFactors
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(530, 392);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(538, 314);
            this.Name = "frmDefFactors";
            this.ShowInTaskbar = false;
            this.Text = "Define Factors";
            this.Load += new System.EventHandler(this.frmDefFactors_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion
        				
		private void frmDefFactors_Load(object sender, System.EventArgs e)
		{
			if (restoreFactors)
				updateFactorForm() ;
        }

		private void updateFactorForm()
		{
			
			fillFactorArray() ;

		    var factorNames = new List<string>();
		    foreach (var item in marrFactors)
		    {
		        factorNames.Add(item.mstrFactor);
		    }

		    lstBoxFactors.DataSource = factorNames;
			txtBoxFactors.Text = "" ;
			txtBoxValues.Text = "" ;
		}

		#region Code for Form buttons

        private void Add_Factor()
        {
            var tmpfactorInfo = new clsFactorInfo();

            if (lstBoxFactors.Items.Count == MAX_LEVELS)
                MessageBox.Show("Maximum number of factors reached!");
            else if (txtBoxFactors.Text != "")
            {
                var strFactor = txtBoxFactors.Text.Replace(" ", "_");
                if (Array.IndexOf(strarrFactors, strFactor) < 0)
                { // no duplicates
                    txtBoxFactors.Text = "";
                    tmpfactorInfo.mstrFactor = strFactor;
                    marrFactors.Add(tmpfactorInfo);
                    lstBoxFactors.Items.Add(strFactor);
                    lstBoxFactors.SelectedIndex = -1;
                    strarrFactors[numFactors] = strFactor;
                    numFactors++;
                }
                else
                {
                    MessageBox.Show("You have already entered this factor!");
                }
            }
        }

        private void Add_Value()
        {
            int nSelected;
            string selectedFactor;
            int nCurrentidx;
            bool duplicateFound = false;

            string strValue = txtBoxValues.Text.Replace(" ", "_");

            if (lstBoxFactors.Items.Count == 1)
                lstBoxFactors.SetSelected(0, true);
            nSelected = lstBoxFactors.SelectedIndex;
            if (nSelected < 0)
                MessageBox.Show("Select a factor first to define their values.");
            else if (lstBoxValues.Items.Count == MAX_LEVELS)
                MessageBox.Show("You have reached the maximum number of values allowed.");
            else if (strValue != "")
            {
                lstBoxValues.SelectedIndex = -1;
                selectedFactor = lstBoxFactors.SelectedItem.ToString();
                fillFactorArray();
                nCurrentidx = Array.IndexOf(strarrFactors, selectedFactor);

                duplicateFound = foundDuplicates(strValue);

                if (!duplicateFound)
                {
                    txtBoxValues.Text = "";
                    lstBoxValues.Items.Add(strValue);
                    marrFactors[nCurrentidx].marrValues.Add(strValue);
                }
                else
                {
                    MessageBox.Show("You have already entered this value earlier!");
                }
            }
        }

		private void btnAddFactors_Click(object sender, System.EventArgs e)
		{
            Add_Factor();
		}

		private void btnAddValues_Click(object sender, System.EventArgs e)
		{
            Add_Value();
		}

        private void txtBoxFactors_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
                Add_Factor();
        }

        private void txtBoxValues_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
                Add_Value();
        }
		
		private void lstBoxFactors_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		    if (lstBoxFactors.SelectedIndex > -1)
			{
				var nSelected = lstBoxFactors.SelectedIndex;
				txtBoxFactors.Text = lstBoxFactors.SelectedItem.ToString() ;
				lstBoxValues.Items.Clear() ;

				var selectedF = marrFactors[nSelected];
				if ( selectedF.vCount > 0 )
				{
					for (var i = 0; i < selectedF.vCount; i++)
					{
						lstBoxValues.Items.Add(selectedF.marrValues[i].ToString());
					}
					txtBoxValues.Text = "" ;
					lstBoxValues.SelectedIndex = -1 ;
				}
				else
					txtBoxValues.Text = "Value1" ;
			}
		}

		private void lstBoxValues_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (lstBoxValues.SelectedIndex != -1)
				txtBoxValues.Text = lstBoxValues.SelectedItem.ToString() ;
		}

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			var correctFactors = FactorCheck() ;
			if (correctFactors)
			{
				DialogResult = DialogResult.OK ;
				this.Close() ;
			}
			else
				MessageBox.Show("Error: Each factor should have at least two values.") ;
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.Cancel ;
			this.Close() ;
		}

		private void btnFactorDelete_Click(object sender, System.EventArgs e)
		{
			int nSelected = lstBoxFactors.SelectedIndex ;

			if (nSelected < 0)
				MessageBox.Show("Select a factor to delete.") ;
			else
			{
				lstBoxFactors.Items.Remove(lstBoxFactors.SelectedItem) ;
				lstBoxFactors.SelectedIndex = -1 ;
				txtBoxFactors.Text = "" ;
				lstBoxValues.Items.Clear() ;
				marrFactors.RemoveAt(nSelected) ;
				fillFactorArray() ;
			}
		}

		private void btnValueDelete_Click(object sender, System.EventArgs e)
		{
			var nSelectedV = lstBoxValues.SelectedIndex ;
			var nSelectedF = lstBoxFactors.SelectedIndex ;

			if (nSelectedV < 0)
				MessageBox.Show("Select a value to delete.") ;
			else
			{
				lstBoxValues.Items.Remove(lstBoxValues.SelectedItem) ;
				lstBoxValues.SelectedIndex = -1 ;
				txtBoxValues.Text = "" ;
				marrFactors[nSelectedF].marrValues.RemoveAt(nSelectedV) ;
			}
		}
		#endregion


		private bool FactorCheck()
		{
            // check if each factor has at least two distinct values.
			var factorCountOK = marrFactors.Count(factor => factor.marrValues.Count > 1);

		    return factorCountOK == marrFactors.Count;
		}


		private void fillFactorArray()
		{
			for (var num = 0; num < marrFactors.Count; num++)
				strarrFactors[num] = marrFactors[num].mstrFactor ;

			numFactors = marrFactors.Count ;
		}

		private bool foundDuplicates(string strValue)
		{
			var found = false ;
		    for (var num = 0; num < numFactors; num++)
			{
			    if (marrFactors[num].vCount <= 0)
			    {
			        continue;
			    }

			    var strarrValues = marrFactors[num].FactorValues;
			    if (Array.IndexOf(strarrValues, strValue) > -1)
			        found = true;
			}

			return found;
		}

		private void txtBoxFactors_textCh(object sender, System.EventArgs e)
		{
			if (txtBoxFactors.Text.Length == 0)
				btnFactors.Enabled = false ;
			else
				btnFactors.Enabled = true ;
		}

		private void txtBoxValues_txtCh(object sender, System.EventArgs e)
		{
			if (txtBoxValues.Text.Length == 0)
				btnValues.Enabled = false ;
			else
				btnValues.Enabled = true ;
		}

		#region Accessors
		public int NumFactors
		{
			get	{return numFactors ;}
		}

        public List<clsFactorInfo> FactorInfoArray
		{
			get {return marrFactors ;}
			set 
			{
				marrFactors = value ;
				restoreFactors = true;
			}
		}
		#endregion

        
	}
}
