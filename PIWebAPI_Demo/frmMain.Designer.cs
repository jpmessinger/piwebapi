namespace PIWebAPI_Demo
{
    partial class frmMain
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboAFservers = new System.Windows.Forms.ComboBox();
            this.btnAFServers = new System.Windows.Forms.Button();
            this.cboDAservers = new System.Windows.Forms.ComboBox();
            this.btnPIServers = new System.Windows.Forms.Button();
            this.btnTagSearch = new System.Windows.Forms.Button();
            this.btnElementSearch = new System.Windows.Forms.Button();
            this.btnData = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboStreamType = new System.Windows.Forms.ComboBox();
            this.btnAttrSearch = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboAFservers);
            this.groupBox1.Controls.Add(this.btnAFServers);
            this.groupBox1.Controls.Add(this.cboDAservers);
            this.groupBox1.Controls.Add(this.btnPIServers);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(321, 99);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Servers";
            // 
            // cboAFservers
            // 
            this.cboAFservers.FormattingEnabled = true;
            this.cboAFservers.Location = new System.Drawing.Point(122, 63);
            this.cboAFservers.Name = "cboAFservers";
            this.cboAFservers.Size = new System.Drawing.Size(192, 21);
            this.cboAFservers.TabIndex = 3;
            // 
            // btnAFServers
            // 
            this.btnAFServers.Location = new System.Drawing.Point(6, 62);
            this.btnAFServers.Name = "btnAFServers";
            this.btnAFServers.Size = new System.Drawing.Size(110, 23);
            this.btnAFServers.TabIndex = 2;
            this.btnAFServers.Text = "Get PI AF Servers";
            this.btnAFServers.UseVisualStyleBackColor = true;
            this.btnAFServers.Click += new System.EventHandler(this.btnAFServers_Click);
            // 
            // cboDAservers
            // 
            this.cboDAservers.FormattingEnabled = true;
            this.cboDAservers.Location = new System.Drawing.Point(122, 21);
            this.cboDAservers.Name = "cboDAservers";
            this.cboDAservers.Size = new System.Drawing.Size(192, 21);
            this.cboDAservers.TabIndex = 1;
            // 
            // btnPIServers
            // 
            this.btnPIServers.Location = new System.Drawing.Point(6, 19);
            this.btnPIServers.Name = "btnPIServers";
            this.btnPIServers.Size = new System.Drawing.Size(110, 23);
            this.btnPIServers.TabIndex = 0;
            this.btnPIServers.Text = "Get PI DA Servers";
            this.btnPIServers.UseVisualStyleBackColor = true;
            this.btnPIServers.Click += new System.EventHandler(this.btnPIServers_Click);
            // 
            // btnTagSearch
            // 
            this.btnTagSearch.Location = new System.Drawing.Point(6, 19);
            this.btnTagSearch.Name = "btnTagSearch";
            this.btnTagSearch.Size = new System.Drawing.Size(113, 30);
            this.btnTagSearch.TabIndex = 1;
            this.btnTagSearch.Text = "PI Point Search";
            this.btnTagSearch.UseVisualStyleBackColor = true;
            this.btnTagSearch.Click += new System.EventHandler(this.btnTagSearch_Click);
            // 
            // btnElementSearch
            // 
            this.btnElementSearch.Location = new System.Drawing.Point(6, 60);
            this.btnElementSearch.Name = "btnElementSearch";
            this.btnElementSearch.Size = new System.Drawing.Size(113, 30);
            this.btnElementSearch.TabIndex = 2;
            this.btnElementSearch.Text = "AF Element Search";
            this.btnElementSearch.UseVisualStyleBackColor = true;
            this.btnElementSearch.Click += new System.EventHandler(this.btnElementSearch_Click);
            // 
            // btnData
            // 
            this.btnData.Location = new System.Drawing.Point(217, 19);
            this.btnData.Name = "btnData";
            this.btnData.Size = new System.Drawing.Size(113, 35);
            this.btnData.TabIndex = 3;
            this.btnData.Text = "Fetch Data";
            this.btnData.UseVisualStyleBackColor = true;
            this.btnData.Click += new System.EventHandler(this.btnData_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnTagSearch);
            this.groupBox2.Controls.Add(this.btnElementSearch);
            this.groupBox2.Location = new System.Drawing.Point(339, 11);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(134, 100);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Item Search";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.cboStreamType);
            this.groupBox3.Controls.Add(this.btnData);
            this.groupBox3.Location = new System.Drawing.Point(12, 117);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(347, 71);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Historical Data Access";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Stream Type:";
            // 
            // cboStreamType
            // 
            this.cboStreamType.FormattingEnabled = true;
            this.cboStreamType.Location = new System.Drawing.Point(10, 32);
            this.cboStreamType.Name = "cboStreamType";
            this.cboStreamType.Size = new System.Drawing.Size(190, 21);
            this.cboStreamType.TabIndex = 0;
            // 
            // btnAttrSearch
            // 
            this.btnAttrSearch.Location = new System.Drawing.Point(444, 136);
            this.btnAttrSearch.Name = "btnAttrSearch";
            this.btnAttrSearch.Size = new System.Drawing.Size(113, 30);
            this.btnAttrSearch.TabIndex = 4;
            this.btnAttrSearch.Text = "Data Access";
            this.btnAttrSearch.UseVisualStyleBackColor = true;
            this.btnAttrSearch.Click += new System.EventHandler(this.btnAttrSearch_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(929, 589);
            this.Controls.Add(this.btnAttrSearch);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmMain";
            this.Text = "PI Web API Demo - John Messinger";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cboAFservers;
        private System.Windows.Forms.Button btnAFServers;
        private System.Windows.Forms.ComboBox cboDAservers;
        private System.Windows.Forms.Button btnPIServers;
        private System.Windows.Forms.Button btnTagSearch;
        private System.Windows.Forms.Button btnElementSearch;
        private System.Windows.Forms.Button btnData;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboStreamType;
        private System.Windows.Forms.Button btnAttrSearch;
    }
}

