namespace DAnTE.Inferno
{
    partial class frmVennDiagramPar
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Button mBtnfA;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVennDiagramPar));
            this.label4 = new System.Windows.Forms.Label();
            this.mbtnCancel = new System.Windows.Forms.Button();
            this.mbtnOK = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.mlstBoxDataLists = new System.Windows.Forms.ListBox();
            this.mtxtBoxA = new System.Windows.Forms.TextBox();
            this.mBtnA = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.mtxtBoxLA = new System.Windows.Forms.TextBox();
            this.mtxtBoxLB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.mBtnB = new System.Windows.Forms.Button();
            this.mtxtBoxB = new System.Windows.Forms.TextBox();
            this.mtxtBoxLC = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.mBtnC = new System.Windows.Forms.Button();
            this.mtxtBoxC = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.mtabControl = new System.Windows.Forms.TabControl();
            this.mtabVennLists = new System.Windows.Forms.TabPage();
            this.mtabFactors = new System.Windows.Forms.TabPage();
            this.mlblDataName = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.mlistBoxLevels = new System.Windows.Forms.ListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.mtxtBoxfC = new System.Windows.Forms.TextBox();
            this.mtxtBoxflA = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.mBtnfC = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.mtxtBoxflC = new System.Windows.Forms.TextBox();
            this.mtxtBoxfA = new System.Windows.Forms.TextBox();
            this.mtxtBoxfB = new System.Windows.Forms.TextBox();
            this.mtxtBoxflB = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.mBtnfB = new System.Windows.Forms.Button();
            this.mcmbBoxFactors = new System.Windows.Forms.ComboBox();
            this.mlblPickFactor = new System.Windows.Forms.Label();
            this.niceLine2 = new DAnTE.ExtraControls.NiceLine();
            this.niceLine1 = new DAnTE.ExtraControls.NiceLine();
            mBtnfA = new System.Windows.Forms.Button();
            this.mtabControl.SuspendLayout();
            this.mtabVennLists.SuspendLayout();
            this.mtabFactors.SuspendLayout();
            this.SuspendLayout();
            // 
            // mBtnfA
            // 
            mBtnfA.Location = new System.Drawing.Point(210, 59);
            mBtnfA.Name = "mBtnfA";
            mBtnfA.Size = new System.Drawing.Size(37, 23);
            mBtnfA.TabIndex = 80;
            mBtnfA.Text = ">>";
            mBtnfA.UseVisualStyleBackColor = true;
            mBtnfA.Click += new System.EventHandler(this.mBtnfA_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(212, 13);
            this.label4.TabIndex = 39;
            this.label4.Text = "Select Parameters for Venn Diagram";
            // 
            // mbtnCancel
            // 
            this.mbtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.mbtnCancel.Location = new System.Drawing.Point(205, 333);
            this.mbtnCancel.Name = "mbtnCancel";
            this.mbtnCancel.Size = new System.Drawing.Size(75, 23);
            this.mbtnCancel.TabIndex = 37;
            this.mbtnCancel.Text = "Cancel";
            this.mbtnCancel.UseVisualStyleBackColor = true;
            this.mbtnCancel.Click += new System.EventHandler(this.mbtnCancel_Click);
            // 
            // mbtnOK
            // 
            this.mbtnOK.Location = new System.Drawing.Point(84, 333);
            this.mbtnOK.Name = "mbtnOK";
            this.mbtnOK.Size = new System.Drawing.Size(75, 23);
            this.mbtnOK.TabIndex = 36;
            this.mbtnOK.Text = "OK";
            this.mbtnOK.UseVisualStyleBackColor = true;
            this.mbtnOK.Click += new System.EventHandler(this.mbtnOK_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 13);
            this.label5.TabIndex = 56;
            this.label5.Text = "Available Data Lists:";
            // 
            // mlstBoxDataLists
            // 
            this.mlstBoxDataLists.FormattingEnabled = true;
            this.mlstBoxDataLists.Location = new System.Drawing.Point(16, 36);
            this.mlstBoxDataLists.Name = "mlstBoxDataLists";
            this.mlstBoxDataLists.Size = new System.Drawing.Size(139, 186);
            this.mlstBoxDataLists.TabIndex = 60;
            // 
            // mtxtBoxA
            // 
            this.mtxtBoxA.Location = new System.Drawing.Point(262, 59);
            this.mtxtBoxA.Name = "mtxtBoxA";
            this.mtxtBoxA.ReadOnly = true;
            this.mtxtBoxA.Size = new System.Drawing.Size(94, 20);
            this.mtxtBoxA.TabIndex = 61;
            // 
            // mBtnA
            // 
            this.mBtnA.Location = new System.Drawing.Point(210, 59);
            this.mBtnA.Name = "mBtnA";
            this.mBtnA.Size = new System.Drawing.Size(37, 23);
            this.mBtnA.TabIndex = 64;
            this.mBtnA.Text = ">>";
            this.mBtnA.UseVisualStyleBackColor = true;
            this.mBtnA.Click += new System.EventHandler(this.mBtnA_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(207, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 66;
            this.label1.Text = "Group A:";
            // 
            // mtxtBoxLA
            // 
            this.mtxtBoxLA.Location = new System.Drawing.Point(262, 36);
            this.mtxtBoxLA.Name = "mtxtBoxLA";
            this.mtxtBoxLA.Size = new System.Drawing.Size(94, 20);
            this.mtxtBoxLA.TabIndex = 69;
            this.mtxtBoxLA.Text = "A";
            // 
            // mtxtBoxLB
            // 
            this.mtxtBoxLB.Location = new System.Drawing.Point(262, 102);
            this.mtxtBoxLB.Name = "mtxtBoxLB";
            this.mtxtBoxLB.Size = new System.Drawing.Size(94, 20);
            this.mtxtBoxLB.TabIndex = 73;
            this.mtxtBoxLB.Text = "B";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(207, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 72;
            this.label2.Text = "Group B:";
            // 
            // mBtnB
            // 
            this.mBtnB.Location = new System.Drawing.Point(210, 125);
            this.mBtnB.Name = "mBtnB";
            this.mBtnB.Size = new System.Drawing.Size(37, 23);
            this.mBtnB.TabIndex = 71;
            this.mBtnB.Text = ">>";
            this.mBtnB.UseVisualStyleBackColor = true;
            this.mBtnB.Click += new System.EventHandler(this.mBtnB_Click);
            // 
            // mtxtBoxB
            // 
            this.mtxtBoxB.Location = new System.Drawing.Point(262, 125);
            this.mtxtBoxB.Name = "mtxtBoxB";
            this.mtxtBoxB.ReadOnly = true;
            this.mtxtBoxB.Size = new System.Drawing.Size(94, 20);
            this.mtxtBoxB.TabIndex = 70;
            // 
            // mtxtBoxLC
            // 
            this.mtxtBoxLC.Location = new System.Drawing.Point(262, 165);
            this.mtxtBoxLC.Name = "mtxtBoxLC";
            this.mtxtBoxLC.Size = new System.Drawing.Size(94, 20);
            this.mtxtBoxLC.TabIndex = 77;
            this.mtxtBoxLC.Text = "C";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(207, 168);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 76;
            this.label3.Text = "Group C:";
            // 
            // mBtnC
            // 
            this.mBtnC.Location = new System.Drawing.Point(210, 188);
            this.mBtnC.Name = "mBtnC";
            this.mBtnC.Size = new System.Drawing.Size(37, 23);
            this.mBtnC.TabIndex = 75;
            this.mBtnC.Text = ">>";
            this.mBtnC.UseVisualStyleBackColor = true;
            this.mBtnC.Click += new System.EventHandler(this.mBtnC_Click);
            // 
            // mtxtBoxC
            // 
            this.mtxtBoxC.Location = new System.Drawing.Point(262, 188);
            this.mtxtBoxC.Name = "mtxtBoxC";
            this.mtxtBoxC.ReadOnly = true;
            this.mtxtBoxC.Size = new System.Drawing.Size(94, 20);
            this.mtxtBoxC.TabIndex = 74;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(280, 211);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 12);
            this.label6.TabIndex = 78;
            this.label6.Text = "(Optional)";
            // 
            // mtabControl
            // 
            this.mtabControl.Controls.Add(this.mtabVennLists);
            this.mtabControl.Controls.Add(this.mtabFactors);
            this.mtabControl.Location = new System.Drawing.Point(12, 46);
            this.mtabControl.Name = "mtabControl";
            this.mtabControl.SelectedIndex = 0;
            this.mtabControl.Size = new System.Drawing.Size(370, 260);
            this.mtabControl.TabIndex = 79;
            // 
            // mtabVennLists
            // 
            this.mtabVennLists.Controls.Add(this.mlstBoxDataLists);
            this.mtabVennLists.Controls.Add(this.label6);
            this.mtabVennLists.Controls.Add(this.label5);
            this.mtabVennLists.Controls.Add(this.mtxtBoxLC);
            this.mtabVennLists.Controls.Add(this.mtxtBoxA);
            this.mtabVennLists.Controls.Add(this.label3);
            this.mtabVennLists.Controls.Add(this.mBtnA);
            this.mtabVennLists.Controls.Add(this.mBtnC);
            this.mtabVennLists.Controls.Add(this.label1);
            this.mtabVennLists.Controls.Add(this.mtxtBoxC);
            this.mtabVennLists.Controls.Add(this.mtxtBoxLA);
            this.mtabVennLists.Controls.Add(this.mtxtBoxLB);
            this.mtabVennLists.Controls.Add(this.mtxtBoxB);
            this.mtabVennLists.Controls.Add(this.label2);
            this.mtabVennLists.Controls.Add(this.mBtnB);
            this.mtabVennLists.Location = new System.Drawing.Point(4, 22);
            this.mtabVennLists.Name = "mtabVennLists";
            this.mtabVennLists.Padding = new System.Windows.Forms.Padding(3);
            this.mtabVennLists.Size = new System.Drawing.Size(362, 234);
            this.mtabVennLists.TabIndex = 1;
            this.mtabVennLists.Text = "Lists";
            this.mtabVennLists.UseVisualStyleBackColor = true;
            // 
            // mtabFactors
            // 
            this.mtabFactors.Controls.Add(this.mlblDataName);
            this.mtabFactors.Controls.Add(this.label11);
            this.mtabFactors.Controls.Add(this.mlistBoxLevels);
            this.mtabFactors.Controls.Add(this.label7);
            this.mtabFactors.Controls.Add(this.mtxtBoxfC);
            this.mtabFactors.Controls.Add(this.mtxtBoxflA);
            this.mtabFactors.Controls.Add(this.label8);
            this.mtabFactors.Controls.Add(mBtnfA);
            this.mtabFactors.Controls.Add(this.mBtnfC);
            this.mtabFactors.Controls.Add(this.label9);
            this.mtabFactors.Controls.Add(this.mtxtBoxflC);
            this.mtabFactors.Controls.Add(this.mtxtBoxfA);
            this.mtabFactors.Controls.Add(this.mtxtBoxfB);
            this.mtabFactors.Controls.Add(this.mtxtBoxflB);
            this.mtabFactors.Controls.Add(this.label10);
            this.mtabFactors.Controls.Add(this.mBtnfB);
            this.mtabFactors.Controls.Add(this.mcmbBoxFactors);
            this.mtabFactors.Controls.Add(this.mlblPickFactor);
            this.mtabFactors.Location = new System.Drawing.Point(4, 22);
            this.mtabFactors.Name = "mtabFactors";
            this.mtabFactors.Padding = new System.Windows.Forms.Padding(3);
            this.mtabFactors.Size = new System.Drawing.Size(362, 234);
            this.mtabFactors.TabIndex = 0;
            this.mtabFactors.Text = "Factors";
            this.mtabFactors.ToolTipText = "Plots a Venn Diagram of detected Proteins across Factor levels";
            this.mtabFactors.UseVisualStyleBackColor = true;
            // 
            // mlblDataName
            // 
            this.mlblDataName.AutoSize = true;
            this.mlblDataName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlblDataName.Location = new System.Drawing.Point(82, 14);
            this.mlblDataName.Name = "mlblDataName";
            this.mlblDataName.Size = new System.Drawing.Size(41, 13);
            this.mlblDataName.TabIndex = 94;
            this.mlblDataName.Text = "label1";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 14);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(70, 13);
            this.label11.TabIndex = 93;
            this.label11.Text = "Data Source:";
            // 
            // mlistBoxLevels
            // 
            this.mlistBoxLevels.FormattingEnabled = true;
            this.mlistBoxLevels.Location = new System.Drawing.Point(6, 88);
            this.mlistBoxLevels.Name = "mlistBoxLevels";
            this.mlistBoxLevels.Size = new System.Drawing.Size(151, 121);
            this.mlistBoxLevels.TabIndex = 92;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(280, 211);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 12);
            this.label7.TabIndex = 91;
            this.label7.Text = "(Optional)";
            // 
            // mtxtBoxfC
            // 
            this.mtxtBoxfC.Location = new System.Drawing.Point(262, 165);
            this.mtxtBoxfC.Name = "mtxtBoxfC";
            this.mtxtBoxfC.Size = new System.Drawing.Size(94, 20);
            this.mtxtBoxfC.TabIndex = 90;
            this.mtxtBoxfC.Text = "C";
            // 
            // mtxtBoxflA
            // 
            this.mtxtBoxflA.Location = new System.Drawing.Point(262, 59);
            this.mtxtBoxflA.Name = "mtxtBoxflA";
            this.mtxtBoxflA.ReadOnly = true;
            this.mtxtBoxflA.Size = new System.Drawing.Size(94, 20);
            this.mtxtBoxflA.TabIndex = 79;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(207, 168);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 13);
            this.label8.TabIndex = 89;
            this.label8.Text = "Group C:";
            // 
            // mBtnfC
            // 
            this.mBtnfC.Location = new System.Drawing.Point(210, 188);
            this.mBtnfC.Name = "mBtnfC";
            this.mBtnfC.Size = new System.Drawing.Size(37, 23);
            this.mBtnfC.TabIndex = 88;
            this.mBtnfC.Text = ">>";
            this.mBtnfC.UseVisualStyleBackColor = true;
            this.mBtnfC.Click += new System.EventHandler(this.mBtnfC_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(207, 39);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(49, 13);
            this.label9.TabIndex = 81;
            this.label9.Text = "Group A:";
            // 
            // mtxtBoxflC
            // 
            this.mtxtBoxflC.Location = new System.Drawing.Point(262, 188);
            this.mtxtBoxflC.Name = "mtxtBoxflC";
            this.mtxtBoxflC.ReadOnly = true;
            this.mtxtBoxflC.Size = new System.Drawing.Size(94, 20);
            this.mtxtBoxflC.TabIndex = 87;
            // 
            // mtxtBoxfA
            // 
            this.mtxtBoxfA.Location = new System.Drawing.Point(262, 36);
            this.mtxtBoxfA.Name = "mtxtBoxfA";
            this.mtxtBoxfA.Size = new System.Drawing.Size(94, 20);
            this.mtxtBoxfA.TabIndex = 82;
            this.mtxtBoxfA.Text = "A";
            // 
            // mtxtBoxfB
            // 
            this.mtxtBoxfB.Location = new System.Drawing.Point(262, 102);
            this.mtxtBoxfB.Name = "mtxtBoxfB";
            this.mtxtBoxfB.Size = new System.Drawing.Size(94, 20);
            this.mtxtBoxfB.TabIndex = 86;
            this.mtxtBoxfB.Text = "B";
            // 
            // mtxtBoxflB
            // 
            this.mtxtBoxflB.Location = new System.Drawing.Point(262, 125);
            this.mtxtBoxflB.Name = "mtxtBoxflB";
            this.mtxtBoxflB.ReadOnly = true;
            this.mtxtBoxflB.Size = new System.Drawing.Size(94, 20);
            this.mtxtBoxflB.TabIndex = 83;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(207, 105);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(49, 13);
            this.label10.TabIndex = 85;
            this.label10.Text = "Group B:";
            // 
            // mBtnfB
            // 
            this.mBtnfB.Location = new System.Drawing.Point(210, 125);
            this.mBtnfB.Name = "mBtnfB";
            this.mBtnfB.Size = new System.Drawing.Size(37, 23);
            this.mBtnfB.TabIndex = 84;
            this.mBtnfB.Text = ">>";
            this.mBtnfB.UseVisualStyleBackColor = true;
            this.mBtnfB.Click += new System.EventHandler(this.mBtnfB_Click);
            // 
            // mcmbBoxFactors
            // 
            this.mcmbBoxFactors.FormattingEnabled = true;
            this.mcmbBoxFactors.Location = new System.Drawing.Point(6, 58);
            this.mcmbBoxFactors.Name = "mcmbBoxFactors";
            this.mcmbBoxFactors.Size = new System.Drawing.Size(151, 21);
            this.mcmbBoxFactors.TabIndex = 52;
            this.mcmbBoxFactors.SelectedIndexChanged += new System.EventHandler(this.mcmbBoxFactors_SelectedIndexChanged);
            // 
            // mlblPickFactor
            // 
            this.mlblPickFactor.AutoSize = true;
            this.mlblPickFactor.Location = new System.Drawing.Point(6, 42);
            this.mlblPickFactor.Name = "mlblPickFactor";
            this.mlblPickFactor.Size = new System.Drawing.Size(73, 13);
            this.mlblPickFactor.TabIndex = 51;
            this.mlblPickFactor.Text = "Select Factor:";
            // 
            // niceLine2
            // 
            this.niceLine2.Location = new System.Drawing.Point(12, 312);
            this.niceLine2.Name = "niceLine2";
            this.niceLine2.Size = new System.Drawing.Size(366, 15);
            this.niceLine2.TabIndex = 43;
            // 
            // niceLine1
            // 
            this.niceLine1.Location = new System.Drawing.Point(12, 25);
            this.niceLine1.Name = "niceLine1";
            this.niceLine1.Size = new System.Drawing.Size(366, 15);
            this.niceLine1.TabIndex = 38;
            // 
            // frmVennDiagramPar
            // 
            this.AcceptButton = this.mbtnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.mbtnCancel;
            this.ClientSize = new System.Drawing.Size(397, 369);
            this.Controls.Add(this.mtabControl);
            this.Controls.Add(this.niceLine2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.niceLine1);
            this.Controls.Add(this.mbtnCancel);
            this.Controls.Add(this.mbtnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmVennDiagramPar";
            this.ShowInTaskbar = false;
            this.Text = "Venn Diagrams";
            this.Load += new System.EventHandler(this.frmVennDiagramPar_Load);
            this.mtabControl.ResumeLayout(false);
            this.mtabVennLists.ResumeLayout(false);
            this.mtabVennLists.PerformLayout();
            this.mtabFactors.ResumeLayout(false);
            this.mtabFactors.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DAnTE.ExtraControls.NiceLine niceLine2;
        private System.Windows.Forms.Label label4;
        private DAnTE.ExtraControls.NiceLine niceLine1;
        private System.Windows.Forms.Button mbtnCancel;
        private System.Windows.Forms.Button mbtnOK;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox mlstBoxDataLists;
        private System.Windows.Forms.TextBox mtxtBoxA;
        private System.Windows.Forms.Button mBtnA;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox mtxtBoxLA;
        private System.Windows.Forms.TextBox mtxtBoxLB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button mBtnB;
        private System.Windows.Forms.TextBox mtxtBoxB;
        private System.Windows.Forms.TextBox mtxtBoxLC;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button mBtnC;
        private System.Windows.Forms.TextBox mtxtBoxC;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TabControl mtabControl;
        private System.Windows.Forms.TabPage mtabFactors;
        private System.Windows.Forms.TabPage mtabVennLists;
        private System.Windows.Forms.ListBox mlistBoxLevels;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox mtxtBoxfC;
        private System.Windows.Forms.TextBox mtxtBoxflA;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button mBtnfC;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox mtxtBoxflC;
        private System.Windows.Forms.TextBox mtxtBoxfA;
        private System.Windows.Forms.TextBox mtxtBoxfB;
        private System.Windows.Forms.TextBox mtxtBoxflB;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button mBtnfB;
        private System.Windows.Forms.ComboBox mcmbBoxFactors;
        private System.Windows.Forms.Label mlblPickFactor;
        private System.Windows.Forms.Label mlblDataName;
        private System.Windows.Forms.Label label11;
    }
}