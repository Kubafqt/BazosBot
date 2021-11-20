
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
         this.cmbSelectOffersType = new System.Windows.Forms.ComboBox();
         this.cmbSelectOffers = new System.Windows.Forms.ComboBox();
         this.btnSelectPanel = new System.Windows.Forms.Button();
         this.cmbSelectPanel = new System.Windows.Forms.ComboBox();
         this.textBox2 = new System.Windows.Forms.TextBox();
         this.cmbSelectFilter = new System.Windows.Forms.ComboBox();
         this.filterPanel = new System.Windows.Forms.Panel();
         this.btnClearFilterTextboxes = new System.Windows.Forms.Button();
         this.cBoxFullName = new System.Windows.Forms.CheckBox();
         this.btnAddToFilter = new System.Windows.Forms.Button();
         this.lbName = new System.Windows.Forms.Label();
         this.lbMaxCena = new System.Windows.Forms.Label();
         this.lbUrlPage = new System.Windows.Forms.Label();
         this.lbFilterName = new System.Windows.Forms.Label();
         this.tbFilterMaxCena = new System.Windows.Forms.TextBox();
         this.tbFilterNadpis = new System.Windows.Forms.TextBox();
         this.tbFilterPageUrl = new System.Windows.Forms.TextBox();
         this.tbFilterName = new System.Windows.Forms.TextBox();
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
         this.filterSetPanel = new System.Windows.Forms.Panel();
         this.lboxSetDetails = new System.Windows.Forms.ListBox();
         this.cmbSelectUrl = new System.Windows.Forms.ComboBox();
         this.cmbSelectFilterSet = new System.Windows.Forms.ComboBox();
         this.btnAddBlacklist = new System.Windows.Forms.Button();
         this.cmbAddBlacklist = new System.Windows.Forms.ComboBox();
         this.lboxBlacklistSet = new System.Windows.Forms.ListBox();
         this.btnCreateFilterSet = new System.Windows.Forms.Button();
         this.tbFiltersetName = new System.Windows.Forms.TextBox();
         this.lboxFilters = new System.Windows.Forms.ListBox();
         this.btnAddFilter = new System.Windows.Forms.Button();
         this.cmbAddFilter = new System.Windows.Forms.ComboBox();
         this.settingsPanel = new System.Windows.Forms.Panel();
         this.lbUpdatedCount = new System.Windows.Forms.Label();
         this.lbDeletedCount = new System.Windows.Forms.Label();
         this.lbNewOffers = new System.Windows.Forms.Label();
         this.cmbFilterSet = new System.Windows.Forms.ComboBox();
         this.panelMain.SuspendLayout();
         this.filterPanel.SuspendLayout();
         this.blacklistPanel.SuspendLayout();
         this.filterSetPanel.SuspendLayout();
         this.settingsPanel.SuspendLayout();
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
         this.resultLbox.Location = new System.Drawing.Point(99, 65);
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
         this.panelMain.Controls.Add(this.cmbSelectOffersType);
         this.panelMain.Controls.Add(this.cmbSelectOffers);
         this.panelMain.Controls.Add(this.tbSearchUrl);
         this.panelMain.Controls.Add(this.offerLbox);
         this.panelMain.Controls.Add(this.resultLbox);
         this.panelMain.Controls.Add(this.label1);
         this.panelMain.Controls.Add(this.btnGetBazos);
         this.panelMain.Location = new System.Drawing.Point(245, 12);
         this.panelMain.Name = "panelMain";
         this.panelMain.Size = new System.Drawing.Size(46, 33);
         this.panelMain.TabIndex = 5;
         this.panelMain.Tag = "mainPanels";
         // 
         // cmbSelectOffersType
         // 
         this.cmbSelectOffersType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.cmbSelectOffersType.FormattingEnabled = true;
         this.cmbSelectOffersType.Location = new System.Drawing.Point(476, 18);
         this.cmbSelectOffersType.Name = "cmbSelectOffersType";
         this.cmbSelectOffersType.Size = new System.Drawing.Size(164, 23);
         this.cmbSelectOffersType.TabIndex = 10;
         this.cmbSelectOffersType.SelectedIndexChanged += new System.EventHandler(this.cmbSelectOffersType_SelectedIndexChanged);
         // 
         // cmbSelectOffers
         // 
         this.cmbSelectOffers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.cmbSelectOffers.FormattingEnabled = true;
         this.cmbSelectOffers.Location = new System.Drawing.Point(681, 17);
         this.cmbSelectOffers.Name = "cmbSelectOffers";
         this.cmbSelectOffers.Size = new System.Drawing.Size(300, 23);
         this.cmbSelectOffers.TabIndex = 9;
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
         // textBox2
         // 
         this.textBox2.Location = new System.Drawing.Point(91, 186);
         this.textBox2.Name = "textBox2";
         this.textBox2.Size = new System.Drawing.Size(162, 23);
         this.textBox2.TabIndex = 1;
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
         // filterPanel
         // 
         this.filterPanel.Controls.Add(this.btnClearFilterTextboxes);
         this.filterPanel.Controls.Add(this.cBoxFullName);
         this.filterPanel.Controls.Add(this.btnAddToFilter);
         this.filterPanel.Controls.Add(this.lbName);
         this.filterPanel.Controls.Add(this.lbMaxCena);
         this.filterPanel.Controls.Add(this.lbUrlPage);
         this.filterPanel.Controls.Add(this.lbFilterName);
         this.filterPanel.Controls.Add(this.tbFilterMaxCena);
         this.filterPanel.Controls.Add(this.tbFilterNadpis);
         this.filterPanel.Controls.Add(this.tbFilterPageUrl);
         this.filterPanel.Controls.Add(this.tbFilterName);
         this.filterPanel.Location = new System.Drawing.Point(528, 67);
         this.filterPanel.Name = "filterPanel";
         this.filterPanel.Size = new System.Drawing.Size(362, 316);
         this.filterPanel.TabIndex = 10;
         this.filterPanel.Tag = "filterPanel";
         // 
         // btnClearFilterTextboxes
         // 
         this.btnClearFilterTextboxes.Location = new System.Drawing.Point(178, 163);
         this.btnClearFilterTextboxes.Name = "btnClearFilterTextboxes";
         this.btnClearFilterTextboxes.Size = new System.Drawing.Size(75, 23);
         this.btnClearFilterTextboxes.TabIndex = 13;
         this.btnClearFilterTextboxes.Text = "Clear";
         this.btnClearFilterTextboxes.UseVisualStyleBackColor = true;
         this.btnClearFilterTextboxes.Click += new System.EventHandler(this.btnClearFilterTextboxes_Click);
         // 
         // cBoxFullName
         // 
         this.cBoxFullName.AutoSize = true;
         this.cBoxFullName.Location = new System.Drawing.Point(278, 103);
         this.cBoxFullName.Name = "cBoxFullName";
         this.cBoxFullName.Size = new System.Drawing.Size(73, 19);
         this.cBoxFullName.TabIndex = 12;
         this.cBoxFullName.Text = "fullname";
         this.cBoxFullName.UseVisualStyleBackColor = true;
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
         // lbFilterName
         // 
         this.lbFilterName.AutoSize = true;
         this.lbFilterName.Location = new System.Drawing.Point(5, 44);
         this.lbFilterName.Name = "lbFilterName";
         this.lbFilterName.Size = new System.Drawing.Size(67, 15);
         this.lbFilterName.TabIndex = 7;
         this.lbFilterName.Text = "filter name:";
         // 
         // tbFilterMaxCena
         // 
         this.tbFilterMaxCena.Location = new System.Drawing.Point(85, 131);
         this.tbFilterMaxCena.Name = "tbFilterMaxCena";
         this.tbFilterMaxCena.Size = new System.Drawing.Size(187, 23);
         this.tbFilterMaxCena.TabIndex = 5;
         this.tbFilterMaxCena.Tag = "Filter";
         // 
         // tbFilterNadpis
         // 
         this.tbFilterNadpis.Location = new System.Drawing.Point(85, 101);
         this.tbFilterNadpis.Name = "tbFilterNadpis";
         this.tbFilterNadpis.Size = new System.Drawing.Size(187, 23);
         this.tbFilterNadpis.TabIndex = 4;
         this.tbFilterNadpis.Tag = "Filter";
         // 
         // tbFilterPageUrl
         // 
         this.tbFilterPageUrl.Location = new System.Drawing.Point(85, 71);
         this.tbFilterPageUrl.Name = "tbFilterPageUrl";
         this.tbFilterPageUrl.Size = new System.Drawing.Size(187, 23);
         this.tbFilterPageUrl.TabIndex = 3;
         this.tbFilterPageUrl.Tag = "Filter";
         // 
         // tbFilterName
         // 
         this.tbFilterName.Location = new System.Drawing.Point(85, 41);
         this.tbFilterName.Name = "tbFilterName";
         this.tbFilterName.Size = new System.Drawing.Size(187, 23);
         this.tbFilterName.TabIndex = 2;
         this.tbFilterName.Tag = "Filter";
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
         this.blacklistPanel.Location = new System.Drawing.Point(161, 10);
         this.blacklistPanel.Name = "blacklistPanel";
         this.blacklistPanel.Size = new System.Drawing.Size(39, 22);
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
         // filterSetPanel
         // 
         this.filterSetPanel.Controls.Add(this.lboxSetDetails);
         this.filterSetPanel.Controls.Add(this.cmbSelectUrl);
         this.filterSetPanel.Controls.Add(this.cmbSelectFilterSet);
         this.filterSetPanel.Controls.Add(this.filterPanel);
         this.filterSetPanel.Controls.Add(this.btnAddBlacklist);
         this.filterSetPanel.Controls.Add(this.cmbAddBlacklist);
         this.filterSetPanel.Controls.Add(this.lboxBlacklistSet);
         this.filterSetPanel.Controls.Add(this.btnCreateFilterSet);
         this.filterSetPanel.Controls.Add(this.tbFiltersetName);
         this.filterSetPanel.Controls.Add(this.lboxFilters);
         this.filterSetPanel.Controls.Add(this.btnAddFilter);
         this.filterSetPanel.Controls.Add(this.cmbAddFilter);
         this.filterSetPanel.Location = new System.Drawing.Point(10, 47);
         this.filterSetPanel.Name = "filterSetPanel";
         this.filterSetPanel.Size = new System.Drawing.Size(926, 476);
         this.filterSetPanel.TabIndex = 12;
         this.filterSetPanel.Tag = "filterPanel";
         // 
         // lboxSetDetails
         // 
         this.lboxSetDetails.FormattingEnabled = true;
         this.lboxSetDetails.ItemHeight = 15;
         this.lboxSetDetails.Location = new System.Drawing.Point(21, 254);
         this.lboxSetDetails.Name = "lboxSetDetails";
         this.lboxSetDetails.Size = new System.Drawing.Size(204, 169);
         this.lboxSetDetails.TabIndex = 16;
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
         this.lboxBlacklistSet.Location = new System.Drawing.Point(880, 22);
         this.lboxBlacklistSet.Name = "lboxBlacklistSet";
         this.lboxBlacklistSet.Size = new System.Drawing.Size(33, 19);
         this.lboxBlacklistSet.TabIndex = 19;
         this.lboxBlacklistSet.SelectedIndexChanged += new System.EventHandler(this.lboxBlacklistSet_SelectedIndexChanged);
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
         // tbFiltersetName
         // 
         this.tbFiltersetName.Location = new System.Drawing.Point(22, 166);
         this.tbFiltersetName.Name = "tbFiltersetName";
         this.tbFiltersetName.Size = new System.Drawing.Size(139, 23);
         this.tbFiltersetName.TabIndex = 16;
         // 
         // lboxFilters
         // 
         this.lboxFilters.FormattingEnabled = true;
         this.lboxFilters.ItemHeight = 15;
         this.lboxFilters.Location = new System.Drawing.Point(256, 20);
         this.lboxFilters.Name = "lboxFilters";
         this.lboxFilters.Size = new System.Drawing.Size(243, 409);
         this.lboxFilters.TabIndex = 15;
         this.lboxFilters.SelectedIndexChanged += new System.EventHandler(this.lboxFilterSet_SelectedIndexChanged);
         this.lboxFilters.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lboxFilters_KeyDown);
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
         this.cmbAddFilter.SelectedIndexChanged += new System.EventHandler(this.cmbAddFilter_SelectedIndexChanged);
         this.cmbAddFilter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbAddFilter_KeyDown);
         // 
         // settingsPanel
         // 
         this.settingsPanel.Controls.Add(this.blacklistPanel);
         this.settingsPanel.Controls.Add(this.filterSetPanel);
         this.settingsPanel.Controls.Add(this.cmbSelectFilter);
         this.settingsPanel.Location = new System.Drawing.Point(21, 45);
         this.settingsPanel.Name = "settingsPanel";
         this.settingsPanel.Size = new System.Drawing.Size(955, 511);
         this.settingsPanel.TabIndex = 8;
         this.settingsPanel.Tag = "mainPanels";
         this.settingsPanel.Visible = false;
         // 
         // lbUpdatedCount
         // 
         this.lbUpdatedCount.AutoSize = true;
         this.lbUpdatedCount.Location = new System.Drawing.Point(475, 12);
         this.lbUpdatedCount.Name = "lbUpdatedCount";
         this.lbUpdatedCount.Size = new System.Drawing.Size(54, 15);
         this.lbUpdatedCount.TabIndex = 9;
         this.lbUpdatedCount.Text = "updated:";
         // 
         // lbDeletedCount
         // 
         this.lbDeletedCount.AutoSize = true;
         this.lbDeletedCount.Location = new System.Drawing.Point(610, 12);
         this.lbDeletedCount.Name = "lbDeletedCount";
         this.lbDeletedCount.Size = new System.Drawing.Size(49, 15);
         this.lbDeletedCount.TabIndex = 10;
         this.lbDeletedCount.Text = "deleted:";
         // 
         // lbNewOffers
         // 
         this.lbNewOffers.AutoSize = true;
         this.lbNewOffers.Location = new System.Drawing.Point(330, 12);
         this.lbNewOffers.Name = "lbNewOffers";
         this.lbNewOffers.Size = new System.Drawing.Size(65, 15);
         this.lbNewOffers.TabIndex = 11;
         this.lbNewOffers.Text = "new offers:";
         // 
         // cmbFilterSet
         // 
         this.cmbFilterSet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.cmbFilterSet.FormattingEnabled = true;
         this.cmbFilterSet.Location = new System.Drawing.Point(703, 5);
         this.cmbFilterSet.Name = "cmbFilterSet";
         this.cmbFilterSet.Size = new System.Drawing.Size(254, 23);
         this.cmbFilterSet.TabIndex = 12;
         // 
         // Form1
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(1024, 594);
         this.Controls.Add(this.cmbFilterSet);
         this.Controls.Add(this.lbNewOffers);
         this.Controls.Add(this.lbDeletedCount);
         this.Controls.Add(this.lbUpdatedCount);
         this.Controls.Add(this.settingsPanel);
         this.Controls.Add(this.cmbSelectPanel);
         this.Controls.Add(this.btnSelectPanel);
         this.Controls.Add(this.panelMain);
         this.MaximizeBox = false;
         this.Name = "Form1";
         this.Text = "Bazos bot";
         this.panelMain.ResumeLayout(false);
         this.panelMain.PerformLayout();
         this.filterPanel.ResumeLayout(false);
         this.filterPanel.PerformLayout();
         this.blacklistPanel.ResumeLayout(false);
         this.blacklistPanel.PerformLayout();
         this.filterSetPanel.ResumeLayout(false);
         this.filterSetPanel.PerformLayout();
         this.settingsPanel.ResumeLayout(false);
         this.ResumeLayout(false);
         this.PerformLayout();

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
      private System.Windows.Forms.TextBox tbMaxCena;
      private System.Windows.Forms.TextBox textBox2;
      private System.Windows.Forms.TextBox bF;
      private System.Windows.Forms.ComboBox cmbSelectOffers;
      private System.Windows.Forms.ComboBox cmbSelectFilter;
      private System.Windows.Forms.Panel filterPanel;
      private System.Windows.Forms.Button btnAddToFilter;
      private System.Windows.Forms.Label lbName;
      private System.Windows.Forms.Label lbMaxCena;
      private System.Windows.Forms.Label lbUrlPage;
      private System.Windows.Forms.Label lbFilterName;
      private System.Windows.Forms.TextBox tbFilterMaxCena;
      private System.Windows.Forms.TextBox tbFilterNadpis;
      private System.Windows.Forms.TextBox tbFilterPageUrl;
      private System.Windows.Forms.TextBox tbFilterName;
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
      private System.Windows.Forms.Panel filterSetPanel;
      private System.Windows.Forms.ComboBox cmbSelectUrl;
      private System.Windows.Forms.ComboBox cmbSelectFilterSet;
      private System.Windows.Forms.Button btnAddBlacklist;
      private System.Windows.Forms.ComboBox cmbAddBlacklist;
      private System.Windows.Forms.ListBox lboxBlacklistSet;
      private System.Windows.Forms.Button btnCreateFilterSet;
      private System.Windows.Forms.TextBox tbFiltersetName;
      private System.Windows.Forms.ListBox lboxFilters;
      private System.Windows.Forms.Button btnAddFilter;
      private System.Windows.Forms.ComboBox cmbAddFilter;
      private System.Windows.Forms.Panel settingsPanel;
      private System.Windows.Forms.ComboBox cmbSelectOffersType;
      private System.Windows.Forms.Label lbUpdatedCount;
      private System.Windows.Forms.Label lbDeletedCount;
      private System.Windows.Forms.Label lbNewOffers;
      private System.Windows.Forms.CheckBox cBoxFullName;
      private System.Windows.Forms.ComboBox cmbFilterSet;
      private System.Windows.Forms.ListBox lboxSetDetails;
      private System.Windows.Forms.Button btnClearFilterTextboxes;
   }
}

