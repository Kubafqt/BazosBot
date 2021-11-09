
namespace BazosBot
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
      ///  Required method for Designer support - do not modify
      ///  the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this.btnGetBazos = new System.Windows.Forms.Button();
         this.resultLbox = new System.Windows.Forms.ListBox();
         this.label1 = new System.Windows.Forms.Label();
         this.offerLbox = new System.Windows.Forms.ListBox();
         this.tbSearchUrl = new System.Windows.Forms.TextBox();
         this.panelMain = new System.Windows.Forms.Panel();
         this.cmbApplyFilterSet = new System.Windows.Forms.ComboBox();
         this.btnSelectPanel = new System.Windows.Forms.Button();
         this.cmbSelectPanel = new System.Windows.Forms.ComboBox();
         this.settingsPanel = new System.Windows.Forms.Panel();
         this.panel1 = new System.Windows.Forms.Panel();
         this.selectFilterPanel = new System.Windows.Forms.Panel();
         this.cmbSelectUrl = new System.Windows.Forms.ComboBox();
         this.cmbSelectFilterSet = new System.Windows.Forms.ComboBox();
         this.btnAddBlacklist = new System.Windows.Forms.Button();
         this.cmbAddBlacklist = new System.Windows.Forms.ComboBox();
         this.lboxBlacklistSet = new System.Windows.Forms.ListBox();
         this.btnCreateFilterSet = new System.Windows.Forms.Button();
         this.tbSetName = new System.Windows.Forms.TextBox();
         this.lboxFilterSet = new System.Windows.Forms.ListBox();
         this.btnAddFilter = new System.Windows.Forms.Button();
         this.cmbAddFilter = new System.Windows.Forms.ComboBox();
         this.blacklistPanel = new System.Windows.Forms.Panel();
         this.label7 = new System.Windows.Forms.Label();
         this.label8 = new System.Windows.Forms.Label();
         this.label9 = new System.Windows.Forms.Label();
         this.label10 = new System.Windows.Forms.Label();
         this.label11 = new System.Windows.Forms.Label();
         this.textBox7 = new System.Windows.Forms.TextBox();
         this.textBox8 = new System.Windows.Forms.TextBox();
         this.textBox9 = new System.Windows.Forms.TextBox();
         this.textBox10 = new System.Windows.Forms.TextBox();
         this.textBox11 = new System.Windows.Forms.TextBox();
         this.filterPanel = new System.Windows.Forms.Panel();
         this.btnAddToFilter = new System.Windows.Forms.Button();
         this.lbName = new System.Windows.Forms.Label();
         this.lbMaxCena = new System.Windows.Forms.Label();
         this.lbUrlPage = new System.Windows.Forms.Label();
         this.lbSetName = new System.Windows.Forms.Label();
         this.tbFilterMaxCena = new System.Windows.Forms.TextBox();
         this.tbFilterName = new System.Windows.Forms.TextBox();
         this.tbFilterPageUrl = new System.Windows.Forms.TextBox();
         this.tbFilterSetName = new System.Windows.Forms.TextBox();
         this.cmbSelectFilter = new System.Windows.Forms.ComboBox();
         this.textBox2 = new System.Windows.Forms.TextBox();
         this.panelMain.SuspendLayout();
         this.settingsPanel.SuspendLayout();
         this.selectFilterPanel.SuspendLayout();
         this.blacklistPanel.SuspendLayout();
         this.filterPanel.SuspendLayout();
         this.SuspendLayout();
         // 
         // btnGetBazos
         // 
         this.btnGetBazos.Location = new System.Drawing.Point(9, 65);
         this.btnGetBazos.Name = "btnGetBazos";
         this.btnGetBazos.Size = new System.Drawing.Size(75, 23);
         this.btnGetBazos.TabIndex = 2;
         this.btnGetBazos.Text = "get offers";
         this.btnGetBazos.UseVisualStyleBackColor = true;
         this.btnGetBazos.Click += new System.EventHandler(this.btnGetBazos_Click);
         // 
         // resultLbox
         // 
         this.resultLbox.FormattingEnabled = true;
         this.resultLbox.ItemHeight = 15;
         this.resultLbox.Location = new System.Drawing.Point(98, 65);
         this.resultLbox.Name = "resultLbox";
         this.resultLbox.Size = new System.Drawing.Size(632, 469);
         this.resultLbox.TabIndex = 3;
         this.resultLbox.SelectedIndexChanged += new System.EventHandler(this.resultLbox_SelectedIndexChanged);
         this.resultLbox.DoubleClick += new System.EventHandler(this.resultLbox_DoubleClick);
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(9, 21);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(51, 15);
         this.label1.TabIndex = 1;
         this.label1.Text = "base url:";
         // 
         // offerLbox
         // 
         this.offerLbox.FormattingEnabled = true;
         this.offerLbox.ItemHeight = 15;
         this.offerLbox.Location = new System.Drawing.Point(747, 65);
         this.offerLbox.Name = "offerLbox";
         this.offerLbox.Size = new System.Drawing.Size(244, 469);
         this.offerLbox.TabIndex = 4;
         this.offerLbox.DoubleClick += new System.EventHandler(this.offerLbox_DoubleClick);
         // 
         // tbSearchUrl
         // 
         this.tbSearchUrl.Location = new System.Drawing.Point(66, 18);
         this.tbSearchUrl.Name = "tbSearchUrl";
         this.tbSearchUrl.Size = new System.Drawing.Size(390, 23);
         this.tbSearchUrl.TabIndex = 0;
         // 
         // panelMain
         // 
         this.panelMain.Controls.Add(this.cmbApplyFilterSet);
         this.panelMain.Controls.Add(this.tbSearchUrl);
         this.panelMain.Controls.Add(this.offerLbox);
         this.panelMain.Controls.Add(this.label1);
         this.panelMain.Controls.Add(this.resultLbox);
         this.panelMain.Controls.Add(this.btnGetBazos);
         this.panelMain.Location = new System.Drawing.Point(12, 34);
         this.panelMain.Name = "panelMain";
         this.panelMain.Size = new System.Drawing.Size(1000, 539);
         this.panelMain.TabIndex = 5;
         this.panelMain.Tag = "mainPanels";
         // 
         // cmbApplyFilterSet
         // 
         this.cmbApplyFilterSet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.cmbApplyFilterSet.FormattingEnabled = true;
         this.cmbApplyFilterSet.Location = new System.Drawing.Point(602, 18);
         this.cmbApplyFilterSet.Name = "cmbApplyFilterSet";
         this.cmbApplyFilterSet.Size = new System.Drawing.Size(300, 23);
         this.cmbApplyFilterSet.TabIndex = 9;
         // 
         // btnSelectPanel
         // 
         this.btnSelectPanel.Location = new System.Drawing.Point(21, 5);
         this.btnSelectPanel.Name = "btnSelectPanel";
         this.btnSelectPanel.Size = new System.Drawing.Size(75, 23);
         this.btnSelectPanel.TabIndex = 6;
         this.btnSelectPanel.Text = "Select";
         this.btnSelectPanel.UseVisualStyleBackColor = true;
         this.btnSelectPanel.Click += new System.EventHandler(this.btnSelectPanel_Click);
         // 
         // cmbSelectPanel
         // 
         this.cmbSelectPanel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.cmbSelectPanel.FormattingEnabled = true;
         this.cmbSelectPanel.Location = new System.Drawing.Point(102, 5);
         this.cmbSelectPanel.Name = "cmbSelectPanel";
         this.cmbSelectPanel.Size = new System.Drawing.Size(121, 23);
         this.cmbSelectPanel.TabIndex = 7;
         this.cmbSelectPanel.SelectedIndexChanged += new System.EventHandler(this.cmbSelectPanel_SelectedIndexChanged);
         // 
         // settingsPanel
         // 
         this.settingsPanel.Controls.Add(this.panel1);
         this.settingsPanel.Controls.Add(this.selectFilterPanel);
         this.settingsPanel.Controls.Add(this.blacklistPanel);
         this.settingsPanel.Controls.Add(this.filterPanel);
         this.settingsPanel.Controls.Add(this.cmbSelectFilter);
         this.settingsPanel.Location = new System.Drawing.Point(249, 12);
         this.settingsPanel.Name = "settingsPanel";
         this.settingsPanel.Size = new System.Drawing.Size(599, 495);
         this.settingsPanel.TabIndex = 8;
         this.settingsPanel.Tag = "mainPanels";
         this.settingsPanel.Visible = false;
         // 
         // panel1
         // 
         this.panel1.Location = new System.Drawing.Point(278, 10);
         this.panel1.Name = "panel1";
         this.panel1.Size = new System.Drawing.Size(301, 181);
         this.panel1.TabIndex = 13;
         this.panel1.Tag = "filterPanel";
         // 
         // selectFilterPanel
         // 
         this.selectFilterPanel.Controls.Add(this.cmbSelectUrl);
         this.selectFilterPanel.Controls.Add(this.cmbSelectFilterSet);
         this.selectFilterPanel.Controls.Add(this.btnAddBlacklist);
         this.selectFilterPanel.Controls.Add(this.cmbAddBlacklist);
         this.selectFilterPanel.Controls.Add(this.lboxBlacklistSet);
         this.selectFilterPanel.Controls.Add(this.btnCreateFilterSet);
         this.selectFilterPanel.Controls.Add(this.tbSetName);
         this.selectFilterPanel.Controls.Add(this.lboxFilterSet);
         this.selectFilterPanel.Controls.Add(this.btnAddFilter);
         this.selectFilterPanel.Controls.Add(this.cmbAddFilter);
         this.selectFilterPanel.Location = new System.Drawing.Point(10, 109);
         this.selectFilterPanel.Name = "selectFilterPanel";
         this.selectFilterPanel.Size = new System.Drawing.Size(569, 358);
         this.selectFilterPanel.TabIndex = 12;
         this.selectFilterPanel.Tag = "filterPanel";
         // 
         // cmbSelectUrl
         // 
         this.cmbSelectUrl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.cmbSelectUrl.FormattingEnabled = true;
         this.cmbSelectUrl.Location = new System.Drawing.Point(21, 104);
         this.cmbSelectUrl.Name = "cmbSelectUrl";
         this.cmbSelectUrl.Size = new System.Drawing.Size(204, 23);
         this.cmbSelectUrl.TabIndex = 23;
         this.cmbSelectUrl.SelectedIndexChanged += new System.EventHandler(this.cmbSelectUrl_SelectedIndexChanged);
         // 
         // cmbSelectFilterSet
         // 
         this.cmbSelectFilterSet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.cmbSelectFilterSet.FormattingEnabled = true;
         this.cmbSelectFilterSet.Location = new System.Drawing.Point(21, 133);
         this.cmbSelectFilterSet.Name = "cmbSelectFilterSet";
         this.cmbSelectFilterSet.Size = new System.Drawing.Size(204, 23);
         this.cmbSelectFilterSet.TabIndex = 22;
         this.cmbSelectFilterSet.SelectedIndexChanged += new System.EventHandler(this.cmbSelectFilterSet_SelectedIndexChanged);
         // 
         // btnAddBlacklist
         // 
         this.btnAddBlacklist.Location = new System.Drawing.Point(164, 55);
         this.btnAddBlacklist.Name = "btnAddBlacklist";
         this.btnAddBlacklist.Size = new System.Drawing.Size(58, 23);
         this.btnAddBlacklist.TabIndex = 21;
         this.btnAddBlacklist.Text = "add";
         this.btnAddBlacklist.UseVisualStyleBackColor = true;
         this.btnAddBlacklist.Click += new System.EventHandler(this.btnAddBlacklist_Click);
         // 
         // cmbAddBlacklist
         // 
         this.cmbAddBlacklist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.cmbAddBlacklist.FormattingEnabled = true;
         this.cmbAddBlacklist.Location = new System.Drawing.Point(19, 52);
         this.cmbAddBlacklist.Name = "cmbAddBlacklist";
         this.cmbAddBlacklist.Size = new System.Drawing.Size(139, 23);
         this.cmbAddBlacklist.TabIndex = 20;
         // 
         // lboxBlacklistSet
         // 
         this.lboxBlacklistSet.FormattingEnabled = true;
         this.lboxBlacklistSet.ItemHeight = 15;
         this.lboxBlacklistSet.Location = new System.Drawing.Point(505, 22);
         this.lboxBlacklistSet.Name = "lboxBlacklistSet";
         this.lboxBlacklistSet.Size = new System.Drawing.Size(243, 289);
         this.lboxBlacklistSet.TabIndex = 19;
         // 
         // btnCreateFilterSet
         // 
         this.btnCreateFilterSet.Location = new System.Drawing.Point(23, 203);
         this.btnCreateFilterSet.Name = "btnCreateFilterSet";
         this.btnCreateFilterSet.Size = new System.Drawing.Size(127, 23);
         this.btnCreateFilterSet.TabIndex = 18;
         this.btnCreateFilterSet.Text = "create filterset";
         this.btnCreateFilterSet.UseVisualStyleBackColor = true;
         this.btnCreateFilterSet.Click += new System.EventHandler(this.btnCreateFilterSet_Click);
         // 
         // tbSetName
         // 
         this.tbSetName.Location = new System.Drawing.Point(22, 166);
         this.tbSetName.Name = "tbSetName";
         this.tbSetName.Size = new System.Drawing.Size(139, 23);
         this.tbSetName.TabIndex = 16;
         // 
         // lboxFilterSet
         // 
         this.lboxFilterSet.FormattingEnabled = true;
         this.lboxFilterSet.ItemHeight = 15;
         this.lboxFilterSet.Location = new System.Drawing.Point(256, 20);
         this.lboxFilterSet.Name = "lboxFilterSet";
         this.lboxFilterSet.Size = new System.Drawing.Size(243, 289);
         this.lboxFilterSet.TabIndex = 15;
         // 
         // btnAddFilter
         // 
         this.btnAddFilter.Location = new System.Drawing.Point(164, 21);
         this.btnAddFilter.Name = "btnAddFilter";
         this.btnAddFilter.Size = new System.Drawing.Size(58, 23);
         this.btnAddFilter.TabIndex = 14;
         this.btnAddFilter.Text = "add";
         this.btnAddFilter.UseVisualStyleBackColor = true;
         this.btnAddFilter.Click += new System.EventHandler(this.btnAddFilter_Click);
         // 
         // cmbAddFilter
         // 
         this.cmbAddFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.cmbAddFilter.FormattingEnabled = true;
         this.cmbAddFilter.Location = new System.Drawing.Point(19, 22);
         this.cmbAddFilter.Name = "cmbAddFilter";
         this.cmbAddFilter.Size = new System.Drawing.Size(139, 23);
         this.cmbAddFilter.TabIndex = 13;
         // 
         // blacklistPanel
         // 
         this.blacklistPanel.Controls.Add(this.label7);
         this.blacklistPanel.Controls.Add(this.label8);
         this.blacklistPanel.Controls.Add(this.label9);
         this.blacklistPanel.Controls.Add(this.label10);
         this.blacklistPanel.Controls.Add(this.label11);
         this.blacklistPanel.Controls.Add(this.textBox7);
         this.blacklistPanel.Controls.Add(this.textBox8);
         this.blacklistPanel.Controls.Add(this.textBox9);
         this.blacklistPanel.Controls.Add(this.textBox10);
         this.blacklistPanel.Controls.Add(this.textBox11);
         this.blacklistPanel.Location = new System.Drawing.Point(10, 39);
         this.blacklistPanel.Name = "blacklistPanel";
         this.blacklistPanel.Size = new System.Drawing.Size(111, 49);
         this.blacklistPanel.TabIndex = 11;
         this.blacklistPanel.Tag = "filterPanel";
         // 
         // label7
         // 
         this.label7.AutoSize = true;
         this.label7.Location = new System.Drawing.Point(19, 104);
         this.label7.Name = "label7";
         this.label7.Size = new System.Drawing.Size(38, 15);
         this.label7.TabIndex = 10;
         this.label7.Text = "label7";
         // 
         // label8
         // 
         this.label8.AutoSize = true;
         this.label8.Location = new System.Drawing.Point(19, 134);
         this.label8.Name = "label8";
         this.label8.Size = new System.Drawing.Size(38, 15);
         this.label8.TabIndex = 9;
         this.label8.Text = "label8";
         // 
         // label9
         // 
         this.label9.AutoSize = true;
         this.label9.Location = new System.Drawing.Point(19, 74);
         this.label9.Name = "label9";
         this.label9.Size = new System.Drawing.Size(38, 15);
         this.label9.TabIndex = 8;
         this.label9.Text = "label9";
         // 
         // label10
         // 
         this.label10.AutoSize = true;
         this.label10.Location = new System.Drawing.Point(19, 44);
         this.label10.Name = "label10";
         this.label10.Size = new System.Drawing.Size(44, 15);
         this.label10.TabIndex = 7;
         this.label10.Text = "label10";
         // 
         // label11
         // 
         this.label11.AutoSize = true;
         this.label11.Location = new System.Drawing.Point(19, 14);
         this.label11.Name = "label11";
         this.label11.Size = new System.Drawing.Size(44, 15);
         this.label11.TabIndex = 6;
         this.label11.Text = "label11";
         // 
         // textBox7
         // 
         this.textBox7.Location = new System.Drawing.Point(63, 131);
         this.textBox7.Name = "textBox7";
         this.textBox7.Size = new System.Drawing.Size(187, 23);
         this.textBox7.TabIndex = 5;
         // 
         // textBox8
         // 
         this.textBox8.Location = new System.Drawing.Point(63, 101);
         this.textBox8.Name = "textBox8";
         this.textBox8.Size = new System.Drawing.Size(187, 23);
         this.textBox8.TabIndex = 4;
         // 
         // textBox9
         // 
         this.textBox9.Location = new System.Drawing.Point(63, 71);
         this.textBox9.Name = "textBox9";
         this.textBox9.Size = new System.Drawing.Size(187, 23);
         this.textBox9.TabIndex = 3;
         // 
         // textBox10
         // 
         this.textBox10.Location = new System.Drawing.Point(63, 41);
         this.textBox10.Name = "textBox10";
         this.textBox10.Size = new System.Drawing.Size(187, 23);
         this.textBox10.TabIndex = 2;
         // 
         // textBox11
         // 
         this.textBox11.Location = new System.Drawing.Point(63, 11);
         this.textBox11.Name = "textBox11";
         this.textBox11.Size = new System.Drawing.Size(187, 23);
         this.textBox11.TabIndex = 1;
         // 
         // filterPanel
         // 
         this.filterPanel.Controls.Add(this.btnAddToFilter);
         this.filterPanel.Controls.Add(this.lbName);
         this.filterPanel.Controls.Add(this.lbMaxCena);
         this.filterPanel.Controls.Add(this.lbUrlPage);
         this.filterPanel.Controls.Add(this.lbSetName);
         this.filterPanel.Controls.Add(this.tbFilterMaxCena);
         this.filterPanel.Controls.Add(this.tbFilterName);
         this.filterPanel.Controls.Add(this.tbFilterPageUrl);
         this.filterPanel.Controls.Add(this.tbFilterSetName);
         this.filterPanel.Location = new System.Drawing.Point(155, 10);
         this.filterPanel.Name = "filterPanel";
         this.filterPanel.Size = new System.Drawing.Size(93, 49);
         this.filterPanel.TabIndex = 10;
         this.filterPanel.Tag = "filterPanel";
         // 
         // btnAddToFilter
         // 
         this.btnAddToFilter.Location = new System.Drawing.Point(85, 163);
         this.btnAddToFilter.Name = "btnAddToFilter";
         this.btnAddToFilter.Size = new System.Drawing.Size(75, 23);
         this.btnAddToFilter.TabIndex = 11;
         this.btnAddToFilter.Text = "Add";
         this.btnAddToFilter.UseVisualStyleBackColor = true;
         this.btnAddToFilter.Click += new System.EventHandler(this.btnAddToFilter_Click);
         // 
         // lbName
         // 
         this.lbName.AutoSize = true;
         this.lbName.Location = new System.Drawing.Point(19, 104);
         this.lbName.Name = "lbName";
         this.lbName.Size = new System.Drawing.Size(40, 15);
         this.lbName.TabIndex = 10;
         this.lbName.Text = "name:";
         // 
         // lbMaxCena
         // 
         this.lbMaxCena.AutoSize = true;
         this.lbMaxCena.Location = new System.Drawing.Point(19, 134);
         this.lbMaxCena.Name = "lbMaxCena";
         this.lbMaxCena.Size = new System.Drawing.Size(61, 15);
         this.lbMaxCena.TabIndex = 9;
         this.lbMaxCena.Text = "max cena:";
         // 
         // lbUrlPage
         // 
         this.lbUrlPage.AutoSize = true;
         this.lbUrlPage.Location = new System.Drawing.Point(19, 74);
         this.lbUrlPage.Name = "lbUrlPage";
         this.lbUrlPage.Size = new System.Drawing.Size(53, 15);
         this.lbUrlPage.TabIndex = 8;
         this.lbUrlPage.Text = "page url:";
         // 
         // lbSetName
         // 
         this.lbSetName.AutoSize = true;
         this.lbSetName.Location = new System.Drawing.Point(19, 44);
         this.lbSetName.Name = "lbSetName";
         this.lbSetName.Size = new System.Drawing.Size(58, 15);
         this.lbSetName.TabIndex = 7;
         this.lbSetName.Text = "set name:";
         // 
         // tbFilterMaxCena
         // 
         this.tbFilterMaxCena.Location = new System.Drawing.Point(85, 131);
         this.tbFilterMaxCena.Name = "tbFilterMaxCena";
         this.tbFilterMaxCena.Size = new System.Drawing.Size(187, 23);
         this.tbFilterMaxCena.TabIndex = 5;
         this.tbFilterMaxCena.Tag = "FilterSet";
         // 
         // tbFilterName
         // 
         this.tbFilterName.Location = new System.Drawing.Point(85, 101);
         this.tbFilterName.Name = "tbFilterName";
         this.tbFilterName.Size = new System.Drawing.Size(187, 23);
         this.tbFilterName.TabIndex = 4;
         this.tbFilterName.Tag = "FilterSet";
         // 
         // tbFilterPageUrl
         // 
         this.tbFilterPageUrl.Location = new System.Drawing.Point(85, 71);
         this.tbFilterPageUrl.Name = "tbFilterPageUrl";
         this.tbFilterPageUrl.Size = new System.Drawing.Size(187, 23);
         this.tbFilterPageUrl.TabIndex = 3;
         this.tbFilterPageUrl.Tag = "FilterSet";
         // 
         // tbFilterSetName
         // 
         this.tbFilterSetName.Location = new System.Drawing.Point(85, 41);
         this.tbFilterSetName.Name = "tbFilterSetName";
         this.tbFilterSetName.Size = new System.Drawing.Size(187, 23);
         this.tbFilterSetName.TabIndex = 2;
         this.tbFilterSetName.Tag = "FilterSet";
         // 
         // cmbSelectFilter
         // 
         this.cmbSelectFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.cmbSelectFilter.FormattingEnabled = true;
         this.cmbSelectFilter.Location = new System.Drawing.Point(10, 10);
         this.cmbSelectFilter.Name = "cmbSelectFilter";
         this.cmbSelectFilter.Size = new System.Drawing.Size(121, 23);
         this.cmbSelectFilter.TabIndex = 8;
         this.cmbSelectFilter.SelectedIndexChanged += new System.EventHandler(this.cmbSelectFilter_SelectedIndexChanged);
         // 
         // textBox2
         // 
         this.textBox2.Location = new System.Drawing.Point(91, 186);
         this.textBox2.Name = "textBox2";
         this.textBox2.Size = new System.Drawing.Size(162, 23);
         this.textBox2.TabIndex = 1;
         // 
         // Form1
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(1024, 594);
         this.Controls.Add(this.settingsPanel);
         this.Controls.Add(this.cmbSelectPanel);
         this.Controls.Add(this.btnSelectPanel);
         this.Controls.Add(this.panelMain);
         this.MaximizeBox = false;
         this.Name = "Form1";
         this.Text = "Form1";
         this.panelMain.ResumeLayout(false);
         this.panelMain.PerformLayout();
         this.settingsPanel.ResumeLayout(false);
         this.selectFilterPanel.ResumeLayout(false);
         this.selectFilterPanel.PerformLayout();
         this.blacklistPanel.ResumeLayout(false);
         this.blacklistPanel.PerformLayout();
         this.filterPanel.ResumeLayout(false);
         this.filterPanel.PerformLayout();
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.Button btnGetBazos;
      private System.Windows.Forms.ListBox resultLbox;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.ListBox offerLbox;
      private System.Windows.Forms.TextBox tbSearchUrl;
      private System.Windows.Forms.Panel panelMain;
      private System.Windows.Forms.Button btnSelectPanel;
      private System.Windows.Forms.ComboBox cmbSelectPanel;
      private System.Windows.Forms.Panel settingsPanel;
      private System.Windows.Forms.ComboBox cmbSelectFilter;
      private System.Windows.Forms.Panel blacklistPanel;
      private System.Windows.Forms.Label label7;
      private System.Windows.Forms.Label label8;
      private System.Windows.Forms.Label label9;
      private System.Windows.Forms.Label label10;
      private System.Windows.Forms.Label label11;
      private System.Windows.Forms.TextBox textBox7;
      private System.Windows.Forms.TextBox textBox8;
      private System.Windows.Forms.TextBox textBox9;
      private System.Windows.Forms.TextBox textBox10;
      private System.Windows.Forms.TextBox textBox11;
      private System.Windows.Forms.Panel filterPanel;
      private System.Windows.Forms.Label lbName;
      private System.Windows.Forms.Label lbMaxCena;
      private System.Windows.Forms.Label lbUrlPage;
      private System.Windows.Forms.Label lbSetName;
      private System.Windows.Forms.TextBox tbMaxCena;
      private System.Windows.Forms.TextBox tbFilterName;
      private System.Windows.Forms.TextBox tbFilterPageUrl;
      private System.Windows.Forms.TextBox tbFilterSetName;
      private System.Windows.Forms.TextBox textBox2;
      private System.Windows.Forms.Button btnAddToFilter;
      private System.Windows.Forms.TextBox bF;
      private System.Windows.Forms.TextBox tbFilterMaxCena;
      private System.Windows.Forms.Panel selectFilterPanel;
      private System.Windows.Forms.Button btnAddFilter;
      private System.Windows.Forms.ComboBox cmbAddFilter;
      private System.Windows.Forms.Button btnCreateFilterSet;
      private System.Windows.Forms.TextBox tbSetName;
      private System.Windows.Forms.Button btnAddBlacklist;
      private System.Windows.Forms.ComboBox cmbAddBlacklist;
      private System.Windows.Forms.ListBox lboxBlacklistSet;
      private System.Windows.Forms.ComboBox cmbSelectFilterSet;
      private System.Windows.Forms.Panel panel1;
      private System.Windows.Forms.ComboBox cmbApplyFilterSet;
      private System.Windows.Forms.ListBox lboxFilterSet;
      private System.Windows.Forms.ComboBox cmbSelectUrl;
   }
}

