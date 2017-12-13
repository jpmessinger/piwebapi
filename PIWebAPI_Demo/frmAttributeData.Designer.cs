namespace PIWebAPI_Demo
{
    partial class frmAttributeData
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
            this.btnSearch = new System.Windows.Forms.Button();
            this.cboDatabase = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cboTemplate = new System.Windows.Forms.ComboBox();
            this.chkHierarchy = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNameFilter = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboServers = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lvResults = new System.Windows.Forms.ListView();
            this.cboAttrCategory = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboStreamType = new System.Windows.Forms.ComboBox();
            this.btnData = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cboStreamSetType = new System.Windows.Forms.ComboBox();
            this.btnBulkData = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.btnRealtimeData = new System.Windows.Forms.Button();
            this.lvData = new System.Windows.Forms.ListView();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(620, 25);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(96, 37);
            this.btnSearch.TabIndex = 36;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // cboDatabase
            // 
            this.cboDatabase.FormattingEnabled = true;
            this.cboDatabase.Location = new System.Drawing.Point(160, 25);
            this.cboDatabase.Name = "cboDatabase";
            this.cboDatabase.Size = new System.Drawing.Size(151, 21);
            this.cboDatabase.TabIndex = 35;
            this.cboDatabase.SelectedIndexChanged += new System.EventHandler(this.cboDatabase_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(157, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 34;
            this.label6.Text = "Database:";
            // 
            // cboTemplate
            // 
            this.cboTemplate.FormattingEnabled = true;
            this.cboTemplate.Location = new System.Drawing.Point(397, 79);
            this.cboTemplate.Name = "cboTemplate";
            this.cboTemplate.Size = new System.Drawing.Size(178, 21);
            this.cboTemplate.TabIndex = 33;
            // 
            // chkHierarchy
            // 
            this.chkHierarchy.AutoSize = true;
            this.chkHierarchy.Location = new System.Drawing.Point(594, 83);
            this.chkHierarchy.Name = "chkHierarchy";
            this.chkHierarchy.Size = new System.Drawing.Size(122, 17);
            this.chkHierarchy.TabIndex = 31;
            this.chkHierarchy.Text = "Search full hierarchy";
            this.chkHierarchy.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(394, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 13);
            this.label4.TabIndex = 30;
            this.label4.Text = "Element Template:";
            // 
            // txtNameFilter
            // 
            this.txtNameFilter.Location = new System.Drawing.Point(15, 79);
            this.txtNameFilter.Name = "txtNameFilter";
            this.txtNameFilter.Size = new System.Drawing.Size(183, 20);
            this.txtNameFilter.TabIndex = 28;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "Attribute Name:";
            // 
            // cboServers
            // 
            this.cboServers.FormattingEnabled = true;
            this.cboServers.Location = new System.Drawing.Point(15, 25);
            this.cboServers.Name = "cboServers";
            this.cboServers.Size = new System.Drawing.Size(138, 21);
            this.cboServers.TabIndex = 26;
            this.cboServers.SelectedIndexChanged += new System.EventHandler(this.cboServers_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 25;
            this.label3.Text = "Server:";
            // 
            // lvResults
            // 
            this.lvResults.FullRowSelect = true;
            this.lvResults.Location = new System.Drawing.Point(15, 118);
            this.lvResults.Name = "lvResults";
            this.lvResults.Size = new System.Drawing.Size(800, 193);
            this.lvResults.TabIndex = 37;
            this.lvResults.UseCompatibleStateImageBehavior = false;
            this.lvResults.View = System.Windows.Forms.View.Details;
            // 
            // cboAttrCategory
            // 
            this.cboAttrCategory.FormattingEnabled = true;
            this.cboAttrCategory.Location = new System.Drawing.Point(207, 79);
            this.cboAttrCategory.Name = "cboAttrCategory";
            this.cboAttrCategory.Size = new System.Drawing.Size(178, 21);
            this.cboAttrCategory.TabIndex = 39;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(204, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 38;
            this.label2.Text = "Attribute Category:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.cboStreamType);
            this.groupBox3.Controls.Add(this.btnData);
            this.groupBox3.Location = new System.Drawing.Point(15, 317);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(347, 71);
            this.groupBox3.TabIndex = 40;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Historical Data Access";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Stream Type:";
            // 
            // cboStreamType
            // 
            this.cboStreamType.FormattingEnabled = true;
            this.cboStreamType.Location = new System.Drawing.Point(10, 32);
            this.cboStreamType.Name = "cboStreamType";
            this.cboStreamType.Size = new System.Drawing.Size(190, 21);
            this.cboStreamType.TabIndex = 0;
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cboStreamSetType);
            this.groupBox1.Controls.Add(this.btnBulkData);
            this.groupBox1.Location = new System.Drawing.Point(15, 394);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(347, 71);
            this.groupBox1.TabIndex = 41;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Bulk Data Access";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Stream Type:";
            // 
            // cboStreamSetType
            // 
            this.cboStreamSetType.FormattingEnabled = true;
            this.cboStreamSetType.Location = new System.Drawing.Point(10, 32);
            this.cboStreamSetType.Name = "cboStreamSetType";
            this.cboStreamSetType.Size = new System.Drawing.Size(190, 21);
            this.cboStreamSetType.TabIndex = 0;
            // 
            // btnBulkData
            // 
            this.btnBulkData.Location = new System.Drawing.Point(217, 19);
            this.btnBulkData.Name = "btnBulkData";
            this.btnBulkData.Size = new System.Drawing.Size(113, 35);
            this.btnBulkData.TabIndex = 3;
            this.btnBulkData.Text = "Fetch Data";
            this.btnBulkData.UseVisualStyleBackColor = true;
            this.btnBulkData.Click += new System.EventHandler(this.btnBulkData_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.comboBox2);
            this.groupBox2.Controls.Add(this.btnRealtimeData);
            this.groupBox2.Location = new System.Drawing.Point(15, 471);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(347, 71);
            this.groupBox2.TabIndex = 42;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Real-time Data Access";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Stream Type:";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(10, 32);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(190, 21);
            this.comboBox2.TabIndex = 0;
            // 
            // btnRealtimeData
            // 
            this.btnRealtimeData.Location = new System.Drawing.Point(217, 19);
            this.btnRealtimeData.Name = "btnRealtimeData";
            this.btnRealtimeData.Size = new System.Drawing.Size(113, 35);
            this.btnRealtimeData.TabIndex = 3;
            this.btnRealtimeData.Text = "Fetch Data";
            this.btnRealtimeData.UseVisualStyleBackColor = true;
            this.btnRealtimeData.Click += new System.EventHandler(this.btnRealtimeData_Click);
            // 
            // lvData
            // 
            this.lvData.Location = new System.Drawing.Point(368, 326);
            this.lvData.Name = "lvData";
            this.lvData.Size = new System.Drawing.Size(447, 215);
            this.lvData.TabIndex = 43;
            this.lvData.UseCompatibleStateImageBehavior = false;
            this.lvData.View = System.Windows.Forms.View.Details;
            // 
            // frmAttributeData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(827, 553);
            this.Controls.Add(this.lvData);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.cboAttrCategory);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lvResults);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.cboDatabase);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cboTemplate);
            this.Controls.Add(this.chkHierarchy);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtNameFilter);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboServers);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAttributeData";
            this.Text = "AF Attribute Data";
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ComboBox cboDatabase;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cboTemplate;
        private System.Windows.Forms.CheckBox chkHierarchy;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNameFilter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboServers;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView lvResults;
        private System.Windows.Forms.ComboBox cboAttrCategory;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboStreamType;
        private System.Windows.Forms.Button btnData;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cboStreamSetType;
        private System.Windows.Forms.Button btnBulkData;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Button btnRealtimeData;
        private System.Windows.Forms.ListView lvData;
    }
}