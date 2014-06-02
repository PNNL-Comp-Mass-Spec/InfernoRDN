using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
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
		private ArrayList marrFactors;// = new ArrayList() ;
        //private ArrayList marrFactorsCopy;
		private string [] strarrFactors = new string [MAX_LEVELS] ;
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
            marrFactors = new ArrayList();
			restoreFactors = false;
		}

		public frmDefFactors(ArrayList factorInfoArr)
		{
            marrFactors = new ArrayList();
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
            this.panel1.Size = new System.Drawing.Size(452, 327);
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
            this.panel2.Size = new System.Drawing.Size(452, 50);
            this.panel2.TabIndex = 17;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(415, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(13, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "?";
            this.mtltipHelp.SetToolTip(this.label5, resources.GetString("label5.ToolTip"));
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(206, 15);
            this.label4.TabIndex = 16;
            this.label4.Text = "Change or Define New Factors:";
            // 
            // lstBoxValues
            // 
            this.lstBoxValues.Location = new System.Drawing.Point(252, 112);
            this.lstBoxValues.Name = "lstBoxValues";
            this.lstBoxValues.Size = new System.Drawing.Size(176, 121);
            this.lstBoxValues.TabIndex = 5;
            this.lstBoxValues.Tag = "Values";
            this.lstBoxValues.SelectedIndexChanged += new System.EventHandler(this.lstBoxValues_SelectedIndexChanged);
            // 
            // niceLine1
            // 
            this.niceLine1.Location = new System.Drawing.Point(12, 269);
            this.niceLine1.Name = "niceLine1";
            this.niceLine1.Size = new System.Drawing.Size(428, 15);
            this.niceLine1.TabIndex = 14;
            // 
            // btnValueDelete
            // 
            this.btnValueDelete.Location = new System.Drawing.Point(313, 239);
            this.btnValueDelete.Name = "btnValueDelete";
            this.btnValueDelete.Size = new System.Drawing.Size(51, 24);
            this.btnValueDelete.TabIndex = 7;
            this.btnValueDelete.Text = "Delete";
            this.btnValueDelete.Click += new System.EventHandler(this.btnValueDelete_Click);
            // 
            // btnFactorDelete
            // 
            this.btnFactorDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnFactorDelete.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnFactorDelete.Location = new System.Drawing.Point(82, 239);
            this.btnFactorDelete.Name = "btnFactorDelete";
            this.btnFactorDelete.Size = new System.Drawing.Size(49, 24);
            this.btnFactorDelete.TabIndex = 6;
            this.btnFactorDelete.Text = "Delete";
            this.btnFactorDelete.Click += new System.EventHandler(this.btnFactorDelete_Click);
            // 
            // txtBoxValues
            // 
            this.txtBoxValues.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxValues.Location = new System.Drawing.Point(252, 80);
            this.txtBoxValues.Name = "txtBoxValues";
            this.txtBoxValues.Size = new System.Drawing.Size(112, 21);
            this.txtBoxValues.TabIndex = 2;
            this.txtBoxValues.Text = "Value1";
            this.txtBoxValues.TextChanged += new System.EventHandler(this.txtBoxValues_txtCh);
            this.txtBoxValues.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBoxValues_KeyPress);
            // 
            // txtBoxFactors
            // 
            this.txtBoxFactors.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxFactors.Location = new System.Drawing.Point(19, 80);
            this.txtBoxFactors.Name = "txtBoxFactors";
            this.txtBoxFactors.Size = new System.Drawing.Size(112, 21);
            this.txtBoxFactors.TabIndex = 0;
            this.txtBoxFactors.Text = "Factor1";
            this.txtBoxFactors.TextChanged += new System.EventHandler(this.txtBoxFactors_textCh);
            this.txtBoxFactors.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBoxFactors_KeyPress);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(123, 290);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(72, 24);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(252, 290);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 24);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Factors:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(252, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Factor Values (levels):";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(201, 156);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 32);
            this.label1.TabIndex = 15;
            this.label1.Text = ">>";
            // 
            // btnFactors
            // 
            this.btnFactors.Location = new System.Drawing.Point(147, 77);
            this.btnFactors.Name = "btnFactors";
            this.btnFactors.Size = new System.Drawing.Size(48, 24);
            this.btnFactors.TabIndex = 1;
            this.btnFactors.Text = "Add";
            this.btnFactors.Click += new System.EventHandler(this.btnAddFactors_Click);
            // 
            // btnValues
            // 
            this.btnValues.Location = new System.Drawing.Point(380, 77);
            this.btnValues.Name = "btnValues";
            this.btnValues.Size = new System.Drawing.Size(48, 24);
            this.btnValues.TabIndex = 3;
            this.btnValues.Text = "Add";
            this.btnValues.Click += new System.EventHandler(this.btnAddValues_Click);
            // 
            // lstBoxFactors
            // 
            this.lstBoxFactors.Location = new System.Drawing.Point(19, 112);
            this.lstBoxFactors.Name = "lstBoxFactors";
            this.lstBoxFactors.Size = new System.Drawing.Size(176, 121);
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
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(452, 327);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(448, 272);
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
			System.Object[] ItemObject = new System.Object[marrFactors.Count] ;

			fillFactorArray() ;

			for (int num = 0 ; num < marrFactors.Count ; num++)
				ItemObject[num] = ((clsFactorInfo)marrFactors[num]).mstrFactor ;

			lstBoxFactors.Items.AddRange(ItemObject) ;
			txtBoxFactors.Text = "" ;
			txtBoxValues.Text = "" ;
		}

		#region Code for Form buttons

        private void Add_Factor()
        {
            clsFactorInfo tmpfactorInfo = new clsFactorInfo();

            if (lstBoxFactors.Items.Count == MAX_LEVELS)
                MessageBox.Show("Maximum number of factors reached!");
            else if (txtBoxFactors.Text != "")
            {
                string strFactor = txtBoxFactors.Text.Replace(" ", "_");
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
                    ((clsFactorInfo)marrFactors[nCurrentidx]).marrValues.Add(strValue);
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
			int nSelected ;
			string [] strArrTmp = new string [MAX_LEVELS] ;
			clsFactorInfo selectedF = new clsFactorInfo() ;
			
			if (lstBoxFactors.SelectedIndex > -1)
			{
				nSelected = lstBoxFactors.SelectedIndex ;
				txtBoxFactors.Text = lstBoxFactors.SelectedItem.ToString() ;
				lstBoxValues.Items.Clear() ;
				selectedF = ((clsFactorInfo)marrFactors[nSelected]) ;
				if ( selectedF.vCount > 0 )
				{
					for (int i = 0; i < selectedF.vCount; i++)
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
			bool correctFactors = FactorCheck() ;
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
			int nSelectedV = lstBoxValues.SelectedIndex ;
			int nSelectedF = lstBoxFactors.SelectedIndex ;

			if (nSelectedV < 0)
				MessageBox.Show("Select a value to delete.") ;
			else
			{
				lstBoxValues.Items.Remove(lstBoxValues.SelectedItem) ;
				lstBoxValues.SelectedIndex = -1 ;
				txtBoxValues.Text = "" ;
				((clsFactorInfo)marrFactors[nSelectedF]).marrValues.RemoveAt(nSelectedV) ;
			}
		}
		#endregion


		private bool FactorCheck()
		{// check if each factor has at least two distinct values.
			bool factorsOK = false ;

			for (int num = 0 ; num < marrFactors.Count ; num++)
			{
				if (((clsFactorInfo)marrFactors[num]).marrValues.Count > 1)
					factorsOK = true ;
			}
			return factorsOK ;
		}


		private void fillFactorArray()
		{
			for (int num = 0; num < marrFactors.Count; num++)
				strarrFactors[num] = ((clsFactorInfo)marrFactors[num]).mstrFactor ;
			numFactors = marrFactors.Count ;
		}

		private bool foundDuplicates(string strValue)
		{
			bool found = false ;
			string [] strarrValues ;
			for (int num = 0; num < numFactors; num++)
			{
				if (((clsFactorInfo)marrFactors[num]).vCount > 0)
				{
					strarrValues = ((clsFactorInfo)marrFactors[num]).FactorValues ;
					if (Array.IndexOf(strarrValues,strValue) > -1)
						found = true ;
				}
			}
			return found ;
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

		public ArrayList FactorInfoArray
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
