namespace DAnTE.ExtraControls
{
    partial class ucGraphicsDev
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucGraphicsDev));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.axStatConnectorGraphicsDevice1 = new AxSTATCONNECTORCLNTLib.AxStatConnectorGraphicsDevice();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.mbtnSave = new System.Windows.Forms.Button();
            this.mbtnUndock = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axStatConnectorGraphicsDevice1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.axStatConnectorGraphicsDevice1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(519, 554);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Graphics Out";
            // 
            // axStatConnectorGraphicsDevice1
            // 
            this.axStatConnectorGraphicsDevice1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.axStatConnectorGraphicsDevice1.Enabled = true;
            this.axStatConnectorGraphicsDevice1.Location = new System.Drawing.Point(26, 29);
            this.axStatConnectorGraphicsDevice1.Name = "axStatConnectorGraphicsDevice1";
            this.axStatConnectorGraphicsDevice1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axStatConnectorGraphicsDevice1.OcxState")));
            this.axStatConnectorGraphicsDevice1.Size = new System.Drawing.Size(454, 486);
            this.axStatConnectorGraphicsDevice1.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.mbtnUndock);
            this.groupBox2.Controls.Add(this.mbtnSave);
            this.groupBox2.Location = new System.Drawing.Point(525, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(134, 554);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(658, 554);
            this.panel1.TabIndex = 3;
            // 
            // mbtnSave
            // 
            this.mbtnSave.Location = new System.Drawing.Point(28, 29);
            this.mbtnSave.Name = "mbtnSave";
            this.mbtnSave.Size = new System.Drawing.Size(86, 23);
            this.mbtnSave.TabIndex = 0;
            this.mbtnSave.Text = "Save";
            this.mbtnSave.UseVisualStyleBackColor = true;
            // 
            // mbtnUndock
            // 
            this.mbtnUndock.Location = new System.Drawing.Point(28, 73);
            this.mbtnUndock.Name = "mbtnUndock";
            this.mbtnUndock.Size = new System.Drawing.Size(86, 23);
            this.mbtnUndock.TabIndex = 1;
            this.mbtnUndock.Text = "New window";
            this.mbtnUndock.UseVisualStyleBackColor = true;
            // 
            // ucGraphicsDev
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "ucGraphicsDev";
            this.Size = new System.Drawing.Size(658, 554);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axStatConnectorGraphicsDevice1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel1;
        public AxSTATCONNECTORCLNTLib.AxStatConnectorGraphicsDevice axStatConnectorGraphicsDevice1;
        public System.Windows.Forms.Button mbtnSave;
        public System.Windows.Forms.Button mbtnUndock;
    }
}
