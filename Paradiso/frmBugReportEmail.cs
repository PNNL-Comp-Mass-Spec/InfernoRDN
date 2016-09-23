using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Text;
using DAnTE.Properties;

namespace DAnTE.Paradiso
{
	/// <summary>
	/// Summary description for frmBugReportEmail.
	/// </summary>
	public class frmBugReportEmail : Form
    {
		private GroupBox grpBoxUserInfo;
		private Label lblEmail;
		private Label lblRqstType;
		private Label label1;
		private GroupBox grpBoxFullDes;
		private TextBox mtxtBoxSummary;
		private TextBox mtxtBoxEmail;
		private ComboBox mcmbBoxRequest;
		private Label mlblDisclaimer;
		private RichTextBox mrichTxtBoxFull;
		private Label mlblbtns;
		private Button mbtnSend;
		private Button mbtnCancel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private readonly System.ComponentModel.Container components = null;

		private string mstrUserEmail ;
		private string mstrShortSummary ;
		private string mstrDescription ;
        private ExtraControls.NiceLine niceLine4;
        private Label label2;
        private ExtraControls.NiceLine niceLine1;
        private PictureBox pictureBox1;
		private readonly string version = Application.ProductVersion;

        public frmBugReportEmail()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
			    components?.Dispose();
			}
		    base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBugReportEmail));
            this.grpBoxFullDes = new System.Windows.Forms.GroupBox();
            mrichTxtBoxFull = new System.Windows.Forms.RichTextBox();
            this.grpBoxUserInfo = new System.Windows.Forms.GroupBox();
            mlblDisclaimer = new System.Windows.Forms.Label();
            mcmbBoxRequest = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblRqstType = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            mtxtBoxSummary = new System.Windows.Forms.TextBox();
            mtxtBoxEmail = new System.Windows.Forms.TextBox();
            mbtnCancel = new System.Windows.Forms.Button();
            mbtnSend = new System.Windows.Forms.Button();
            mlblbtns = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.niceLine1 = new DAnTE.ExtraControls.NiceLine();
            this.niceLine4 = new DAnTE.ExtraControls.NiceLine();
            this.grpBoxFullDes.SuspendLayout();
            this.grpBoxUserInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // grpBoxFullDes
            // 
            this.grpBoxFullDes.Controls.Add(mrichTxtBoxFull);
            this.grpBoxFullDes.Location = new System.Drawing.Point(15, 196);
            this.grpBoxFullDes.Name = "grpBoxFullDes";
            this.grpBoxFullDes.Size = new System.Drawing.Size(552, 271);
            this.grpBoxFullDes.TabIndex = 7;
            this.grpBoxFullDes.TabStop = false;
            this.grpBoxFullDes.Text = "Full Description";
            // 
            // mrichTxtBoxFull
            // 
            mrichTxtBoxFull.Location = new System.Drawing.Point(8, 24);
            mrichTxtBoxFull.Name = "mrichTxtBoxFull";
            mrichTxtBoxFull.Size = new System.Drawing.Size(536, 240);
            mrichTxtBoxFull.TabIndex = 3;
            mrichTxtBoxFull.Text = "";
            // 
            // grpBoxUserInfo
            // 
            this.grpBoxUserInfo.Controls.Add(mlblDisclaimer);
            this.grpBoxUserInfo.Controls.Add(mcmbBoxRequest);
            this.grpBoxUserInfo.Controls.Add(this.label1);
            this.grpBoxUserInfo.Controls.Add(this.lblRqstType);
            this.grpBoxUserInfo.Controls.Add(this.lblEmail);
            this.grpBoxUserInfo.Controls.Add(mtxtBoxSummary);
            this.grpBoxUserInfo.Controls.Add(mtxtBoxEmail);
            this.grpBoxUserInfo.Location = new System.Drawing.Point(15, 46);
            this.grpBoxUserInfo.Name = "grpBoxUserInfo";
            this.grpBoxUserInfo.Size = new System.Drawing.Size(552, 144);
            this.grpBoxUserInfo.TabIndex = 6;
            this.grpBoxUserInfo.TabStop = false;
            this.grpBoxUserInfo.Text = "User Information and Summary";
            // 
            // mlblDisclaimer
            // 
            mlblDisclaimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            mlblDisclaimer.Location = new System.Drawing.Point(8, 104);
            mlblDisclaimer.Name = "mlblDisclaimer";
            mlblDisclaimer.Size = new System.Drawing.Size(536, 37);
            mlblDisclaimer.TabIndex = 6;
            mlblDisclaimer.Text = "Your personal information will be used only for the purpose of providing an ident" +
                "ifier for your bug report, as well as providing a contact address for the develo" +
                "per in case of further questions.";
            mlblDisclaimer.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // mcmbBoxRequest
            // 
            mcmbBoxRequest.Items.AddRange(new object[] {
            "Bug Report",
            "Feature Request"});
            mcmbBoxRequest.Location = new System.Drawing.Point(288, 32);
            mcmbBoxRequest.Name = "mcmbBoxRequest";
            mcmbBoxRequest.Size = new System.Drawing.Size(256, 21);
            mcmbBoxRequest.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Short Summary of the Issue";
            // 
            // lblRqstType
            // 
            this.lblRqstType.Location = new System.Drawing.Point(288, 16);
            this.lblRqstType.Name = "lblRqstType";
            this.lblRqstType.Size = new System.Drawing.Size(144, 16);
            this.lblRqstType.TabIndex = 3;
            this.lblRqstType.Text = "Type of Request";
            // 
            // lblEmail
            // 
            this.lblEmail.Location = new System.Drawing.Point(8, 16);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(144, 16);
            this.lblEmail.TabIndex = 2;
            this.lblEmail.Text = "Your Email Address";
            // 
            // mtxtBoxSummary
            // 
            mtxtBoxSummary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            mtxtBoxSummary.Location = new System.Drawing.Point(8, 72);
            mtxtBoxSummary.Name = "mtxtBoxSummary";
            mtxtBoxSummary.Size = new System.Drawing.Size(536, 20);
            mtxtBoxSummary.TabIndex = 2;
            // 
            // mtxtBoxEmail
            // 
            mtxtBoxEmail.Location = new System.Drawing.Point(8, 32);
            mtxtBoxEmail.Name = "mtxtBoxEmail";
            mtxtBoxEmail.Size = new System.Drawing.Size(264, 20);
            mtxtBoxEmail.TabIndex = 0;
            // 
            // mbtnCancel
            // 
            mbtnCancel.Location = new System.Drawing.Point(462, 499);
            mbtnCancel.Name = "mbtnCancel";
            mbtnCancel.Size = new System.Drawing.Size(80, 23);
            mbtnCancel.TabIndex = 5;
            mbtnCancel.Text = "Cancel";
            mbtnCancel.Click += new System.EventHandler(mbtnCancel_Click);
            // 
            // mbtnSend
            // 
            mbtnSend.Location = new System.Drawing.Point(367, 499);
            mbtnSend.Name = "mbtnSend";
            mbtnSend.Size = new System.Drawing.Size(80, 23);
            mbtnSend.TabIndex = 4;
            mbtnSend.Text = "Send Report";
            mbtnSend.Click += new System.EventHandler(mbtnSend_Click);
            // 
            // mlblbtns
            // 
            mlblbtns.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            mlblbtns.Location = new System.Drawing.Point(21, 491);
            mlblbtns.Name = "mlblbtns";
            mlblbtns.Size = new System.Drawing.Size(320, 40);
            mlblbtns.TabIndex = 0;
            mlblbtns.Text = "Clicking \'Send Report\' will open your Email program with your request ticket to b" +
                "e sent to the developer.";
            mlblbtns.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 52;
            this.label2.Text = "Bugs/Features";
            // 
            // pictureBox1
            // 
            this.pictureBox1.ErrorImage = null;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(502, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(53, 47);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 54;
            this.pictureBox1.TabStop = false;
            // 
            // niceLine1
            // 
            this.niceLine1.Location = new System.Drawing.Point(15, 473);
            this.niceLine1.Name = "niceLine1";
            this.niceLine1.Size = new System.Drawing.Size(552, 15);
            this.niceLine1.TabIndex = 54;
            // 
            // niceLine4
            // 
            this.niceLine4.Location = new System.Drawing.Point(12, 25);
            this.niceLine4.Name = "niceLine4";
            this.niceLine4.Size = new System.Drawing.Size(555, 15);
            this.niceLine4.TabIndex = 53;
            // 
            // frmBugReportEmail
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(584, 540);
            this.Controls.Add(this.grpBoxUserInfo);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.niceLine4);
            this.Controls.Add(this.niceLine1);
            this.Controls.Add(mbtnCancel);
            this.Controls.Add(mbtnSend);
            this.Controls.Add(this.grpBoxFullDes);
            this.Controls.Add(mlblbtns);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBugReportEmail";
            this.ShowInTaskbar = false;
            this.Text = "Report Bugs / Features to Developers";
            this.Load += new System.EventHandler(this.frmTracWebBugReport_Load);
            this.grpBoxFullDes.ResumeLayout(false);
            this.grpBoxUserInfo.ResumeLayout(false);
            this.grpBoxUserInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void frmTracWebBugReport_Load(object sender, EventArgs e)
		{
            mstrUserEmail = Settings.Default.email;
			mcmbBoxRequest.Text = "Bug Report";
			mcmbBoxRequest.SelectedIndex = 0 ;
			if (!string.IsNullOrEmpty(mstrUserEmail))
				mtxtBoxEmail.Text = mstrUserEmail ;
		}

		private void mbtnSend_Click(object sender, EventArgs e)
		{
            var emailProcess = new Process();
			var summary = mtxtBoxSummary.Text ;
			if (string.IsNullOrEmpty(summary))
				MessageBox.Show("Specify your problem first!","Problem summary ?") ;
			else
			{
				var tracLink = BuildTracLink();

                emailProcess.StartInfo.FileName = tracLink;
                emailProcess.StartInfo.UseShellExecute = true;
                emailProcess.StartInfo.RedirectStandardOutput = false;
                emailProcess.Start();
				DialogResult = DialogResult.OK;
				Close();
			}
		}

		private void mbtnCancel_Click(object sender, EventArgs e)
		{
            Close();
		}
        
		private string BuildTracLink() 
		{
			var mSBemail = new StringBuilder();
			mstrUserEmail = mtxtBoxEmail.Text;
			mstrShortSummary = mtxtBoxSummary.Text;
			mstrDescription = mrichTxtBoxFull.Text;
            Settings.Default.email = mstrUserEmail;
            Settings.Default.Save();

			mSBemail.Append("mailto:matthew.monroe@pnnl.gov; proteomics@pnnl.gov?");


            mSBemail.Append("subject=InfernoRDN : ");
            mSBemail.Append(mcmbBoxRequest.Text + "&body=");
			
			if (mstrUserEmail.Length > 0)
			{
                mSBemail.Append("Reporter : ");
				//Uri escapeString = new Uri(mstrUserEmail) ;
                mSBemail.Append(mstrUserEmail);
			}

            mSBemail.Append(";    Version " + version + " (" + Inferno.frmDAnTE.PROGRAM_DATE + ")");

			if (mstrShortSummary.Length > 0)
			{
                mSBemail.Append(";    Summary : ");
				//Uri escapeString = new Uri(mstrShortSummary) ;
                mSBemail.Append(mstrShortSummary + Environment.NewLine);
				//optionsSB.Append(System.Uri.EscapeString(mstrShortSummary)) ;
			}

			if (mstrDescription.Length > 0) 
			{
                mSBemail.Append(";    Description : ");
				//Uri escapeString = new Uri(mstrDescription) ;
                mSBemail.Append(mstrDescription);
				//optionsSB.Append(System.Uri.EscapeString(mstrDescription));
			}

			return mSBemail.ToString();
		}
		

		
	}
}
