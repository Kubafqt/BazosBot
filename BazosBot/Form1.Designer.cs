﻿
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
         this.components = new System.ComponentModel.Container();
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
         this.btnGetBazos = new System.Windows.Forms.Button();
         this.resultLbox = new System.Windows.Forms.ListBox();
         this.lbBaseUrl = new System.Windows.Forms.Label();
         this.offerLbox = new System.Windows.Forms.ListBox();
         this.tbSearchUrl = new System.Windows.Forms.TextBox();
         this.panelMain = new System.Windows.Forms.Panel();
         this.cboxCheckNotFullPopis = new System.Windows.Forms.CheckBox();
         this.btnAddBlacklistSetToQuickFilter = new System.Windows.Forms.Button();
         this.cmbSelectBlacklistSet = new System.Windows.Forms.ComboBox();
         this.btnCreateBlacklistSet = new System.Windows.Forms.Button();
         this.lbBlacklistSet = new System.Windows.Forms.Label();
         this.tbBlackListSet = new System.Windows.Forms.TextBox();
         this.updatesPanel = new System.Windows.Forms.Panel();
         this.lbShowUpdates = new System.Windows.Forms.Label();
         this.cboxUpdateNadpis = new System.Windows.Forms.CheckBox();
         this.cboxUpdateLocation = new System.Windows.Forms.CheckBox();
         this.cboxUpdateCena = new System.Windows.Forms.CheckBox();
         this.cboxUpdateDate = new System.Windows.Forms.CheckBox();
         this.cboxUpdatePopis = new System.Windows.Forms.CheckBox();
         this.btnHelpYouTube = new System.Windows.Forms.Button();
         this.btnHelpQuickFilter = new System.Windows.Forms.Button();
         this.cboxNotUpdateViewedAndLastChecked = new System.Windows.Forms.CheckBox();
         this.btnApplyQuickFilter = new System.Windows.Forms.Button();
         this.cbDisableQuickFilterPrice = new System.Windows.Forms.CheckBox();
         this.cmbSelectQuickFilter = new System.Windows.Forms.ComboBox();
         this.lbLokace = new System.Windows.Forms.Label();
         this.btnCreateQuickFilter = new System.Windows.Forms.Button();
         this.cboxDownOnlyLast = new System.Windows.Forms.CheckBox();
         this.tbLokalita = new System.Windows.Forms.TextBox();
         this.lbQuickFilter = new System.Windows.Forms.Label();
         this.tbQuickFilter = new System.Windows.Forms.TextBox();
         this.cmbSelectOffersType = new System.Windows.Forms.ComboBox();
         this.cmbSelectOffers = new System.Windows.Forms.ComboBox();
         this.lbBotRunning = new System.Windows.Forms.Label();
         this.panelAutoBot = new System.Windows.Forms.Panel();
         this.cboxHideToTray = new System.Windows.Forms.CheckBox();
         this.cboxMultiCategory = new System.Windows.Forms.CheckBox();
         this.lboxBotCategory = new System.Windows.Forms.ListBox();
         this.btnAddItemsToBot = new System.Windows.Forms.Button();
         this.btnStartBot = new System.Windows.Forms.Button();
         this.lbBotInt = new System.Windows.Forms.Label();
         this.lbBotFT = new System.Windows.Forms.Label();
         this.lbBotHeading = new System.Windows.Forms.Label();
         this.lbBotFullTime = new System.Windows.Forms.Label();
         this.lbBotInterval = new System.Windows.Forms.Label();
         this.BotFullTime = new System.Windows.Forms.NumericUpDown();
         this.BotInterval = new System.Windows.Forms.NumericUpDown();
         this.lboxBotQuickFilter = new System.Windows.Forms.ListBox();
         this.btnCreateBot = new System.Windows.Forms.Button();
         this.lbBotQuickFilter = new System.Windows.Forms.Label();
         this.lbBotCategoryUrl = new System.Windows.Forms.Label();
         this.lbBotName = new System.Windows.Forms.Label();
         this.cmbBotQuickFilter = new System.Windows.Forms.ComboBox();
         this.cmbBotCategoryUrl = new System.Windows.Forms.ComboBox();
         this.cmbBotName = new System.Windows.Forms.ComboBox();
         this.tbBotCategoryUrl = new System.Windows.Forms.TextBox();
         this.tbBotQuickFilter = new System.Windows.Forms.TextBox();
         this.tbBotName = new System.Windows.Forms.TextBox();
         this.btnAddQuickFilterBot = new System.Windows.Forms.Button();
         this.btnSelectPanel = new System.Windows.Forms.Button();
         this.cmbSelectPanel = new System.Windows.Forms.ComboBox();
         this.lboxBlacklistSet = new System.Windows.Forms.ListBox();
         this.lbUpdatedCount = new System.Windows.Forms.Label();
         this.lbDeletedCount = new System.Windows.Forms.Label();
         this.lbNewOffers = new System.Windows.Forms.Label();
         this.lbAllOffers = new System.Windows.Forms.Label();
         this.lbProgress = new System.Windows.Forms.Label();
         this.settingsPanel = new System.Windows.Forms.Panel();
         this.cboxHelpButton = new System.Windows.Forms.CheckBox();
         this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
         this.panelMain.SuspendLayout();
         this.updatesPanel.SuspendLayout();
         this.panelAutoBot.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.BotFullTime)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.BotInterval)).BeginInit();
         this.settingsPanel.SuspendLayout();
         this.SuspendLayout();
         // 
         // btnGetBazos
         // 
         this.btnGetBazos.Location = new System.Drawing.Point(4, 136);
         this.btnGetBazos.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
         this.btnGetBazos.Name = "btnGetBazos";
         this.btnGetBazos.Size = new System.Drawing.Size(86, 25);
         this.btnGetBazos.TabIndex = 2;
         this.btnGetBazos.Text = "get offers";
         this.btnGetBazos.UseVisualStyleBackColor = true;
         this.btnGetBazos.Click += new System.EventHandler(this.btnGetBazos_Click);
         // 
         // resultLbox
         // 
         this.resultLbox.FormattingEnabled = true;
         this.resultLbox.ItemHeight = 15;
         this.resultLbox.Location = new System.Drawing.Point(94, 130);
         this.resultLbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
         this.resultLbox.MinimumSize = new System.Drawing.Size(466, 92);
         this.resultLbox.Name = "resultLbox";
         this.resultLbox.Size = new System.Drawing.Size(632, 469);
         this.resultLbox.TabIndex = 3;
         this.resultLbox.SelectedIndexChanged += new System.EventHandler(this.resultLbox_SelectedIndexChanged);
         this.resultLbox.DoubleClick += new System.EventHandler(this.resultLbox_DoubleClick);
         this.resultLbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.resultLbox_KeyDown);
         // 
         // lbBaseUrl
         // 
         this.lbBaseUrl.AutoSize = true;
         this.lbBaseUrl.Location = new System.Drawing.Point(38, 95);
         this.lbBaseUrl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
         this.lbBaseUrl.Name = "lbBaseUrl";
         this.lbBaseUrl.Size = new System.Drawing.Size(51, 15);
         this.lbBaseUrl.TabIndex = 1;
         this.lbBaseUrl.Text = "base url:";
         // 
         // offerLbox
         // 
         this.offerLbox.FormattingEnabled = true;
         this.offerLbox.ItemHeight = 15;
         this.offerLbox.Location = new System.Drawing.Point(747, 130);
         this.offerLbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
         this.offerLbox.MinimumSize = new System.Drawing.Size(186, 184);
         this.offerLbox.Name = "offerLbox";
         this.offerLbox.Size = new System.Drawing.Size(243, 274);
         this.offerLbox.TabIndex = 4;
         this.offerLbox.DoubleClick += new System.EventHandler(this.offerLbox_DoubleClick);
         this.offerLbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.offerLbox_KeyDown);
         // 
         // tbSearchUrl
         // 
         this.tbSearchUrl.Location = new System.Drawing.Point(94, 92);
         this.tbSearchUrl.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
         this.tbSearchUrl.MinimumSize = new System.Drawing.Size(390, 21);
         this.tbSearchUrl.Name = "tbSearchUrl";
         this.tbSearchUrl.Size = new System.Drawing.Size(390, 23);
         this.tbSearchUrl.TabIndex = 0;
         // 
         // panelMain
         // 
         this.panelMain.Controls.Add(this.cboxCheckNotFullPopis);
         this.panelMain.Controls.Add(this.btnAddBlacklistSetToQuickFilter);
         this.panelMain.Controls.Add(this.cmbSelectBlacklistSet);
         this.panelMain.Controls.Add(this.btnCreateBlacklistSet);
         this.panelMain.Controls.Add(this.lbBlacklistSet);
         this.panelMain.Controls.Add(this.tbBlackListSet);
         this.panelMain.Controls.Add(this.updatesPanel);
         this.panelMain.Controls.Add(this.btnHelpYouTube);
         this.panelMain.Controls.Add(this.btnHelpQuickFilter);
         this.panelMain.Controls.Add(this.cboxNotUpdateViewedAndLastChecked);
         this.panelMain.Controls.Add(this.btnApplyQuickFilter);
         this.panelMain.Controls.Add(this.cbDisableQuickFilterPrice);
         this.panelMain.Controls.Add(this.cmbSelectQuickFilter);
         this.panelMain.Controls.Add(this.lbLokace);
         this.panelMain.Controls.Add(this.btnCreateQuickFilter);
         this.panelMain.Controls.Add(this.cboxDownOnlyLast);
         this.panelMain.Controls.Add(this.tbLokalita);
         this.panelMain.Controls.Add(this.lbQuickFilter);
         this.panelMain.Controls.Add(this.tbQuickFilter);
         this.panelMain.Controls.Add(this.cmbSelectOffersType);
         this.panelMain.Controls.Add(this.cmbSelectOffers);
         this.panelMain.Controls.Add(this.tbSearchUrl);
         this.panelMain.Controls.Add(this.offerLbox);
         this.panelMain.Controls.Add(this.resultLbox);
         this.panelMain.Controls.Add(this.lbBaseUrl);
         this.panelMain.Controls.Add(this.btnGetBazos);
         this.panelMain.Location = new System.Drawing.Point(18, 48);
         this.panelMain.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
         this.panelMain.Name = "panelMain";
         this.panelMain.Size = new System.Drawing.Size(1017, 621);
         this.panelMain.TabIndex = 5;
         this.panelMain.Tag = "mainPanels";
         // 
         // cboxCheckNotFullPopis
         // 
         this.cboxCheckNotFullPopis.AutoSize = true;
         this.cboxCheckNotFullPopis.Location = new System.Drawing.Point(750, 7);
         this.cboxCheckNotFullPopis.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
         this.cboxCheckNotFullPopis.Name = "cboxCheckNotFullPopis";
         this.cboxCheckNotFullPopis.Size = new System.Drawing.Size(254, 19);
         this.cboxCheckNotFullPopis.TabIndex = 38;
         this.cboxCheckNotFullPopis.Text = "check all not full popis (when in quickfilter)";
         this.cboxCheckNotFullPopis.UseVisualStyleBackColor = true;
         // 
         // btnAddBlacklistSetToQuickFilter
         // 
         this.btnAddBlacklistSetToQuickFilter.Location = new System.Drawing.Point(416, 61);
         this.btnAddBlacklistSetToQuickFilter.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
         this.btnAddBlacklistSetToQuickFilter.Name = "btnAddBlacklistSetToQuickFilter";
         this.btnAddBlacklistSetToQuickFilter.Size = new System.Drawing.Size(69, 25);
         this.btnAddBlacklistSetToQuickFilter.TabIndex = 37;
         this.btnAddBlacklistSetToQuickFilter.Text = "add to qf";
         this.btnAddBlacklistSetToQuickFilter.UseVisualStyleBackColor = true;
         this.btnAddBlacklistSetToQuickFilter.Click += new System.EventHandler(this.btnAddBlacklistSetToQuickFilter_Click);
         // 
         // cmbSelectBlacklistSet
         // 
         this.cmbSelectBlacklistSet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.cmbSelectBlacklistSet.FormattingEnabled = true;
         this.cmbSelectBlacklistSet.Location = new System.Drawing.Point(572, 62);
         this.cmbSelectBlacklistSet.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
         this.cmbSelectBlacklistSet.Name = "cmbSelectBlacklistSet";
         this.cmbSelectBlacklistSet.Size = new System.Drawing.Size(224, 23);
         this.cmbSelectBlacklistSet.TabIndex = 36;
         this.cmbSelectBlacklistSet.SelectedIndexChanged += new System.EventHandler(this.cmbSelectBlacklistSet_SelectedIndexChanged);
         this.cmbSelectBlacklistSet.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbSelectBlacklistSet_KeyDown);
         // 
         // btnCreateBlacklistSet
         // 
         this.btnCreateBlacklistSet.Location = new System.Drawing.Point(491, 61);
         this.btnCreateBlacklistSet.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
         this.btnCreateBlacklistSet.Name = "btnCreateBlacklistSet";
         this.btnCreateBlacklistSet.Size = new System.Drawing.Size(75, 25);
         this.btnCreateBlacklistSet.TabIndex = 35;
         this.btnCreateBlacklistSet.Text = "create";
         this.btnCreateBlacklistSet.UseVisualStyleBackColor = true;
         this.btnCreateBlacklistSet.Click += new System.EventHandler(this.btnCreateBlacklistSet_Click);
         // 
         // lbBlacklistSet
         // 
         this.lbBlacklistSet.AutoSize = true;
         this.lbBlacklistSet.Location = new System.Drawing.Point(13, 69);
         this.lbBlacklistSet.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
         this.lbBlacklistSet.Name = "lbBlacklistSet";
         this.lbBlacklistSet.Size = new System.Drawing.Size(71, 15);
         this.lbBlacklistSet.TabIndex = 34;
         this.lbBlacklistSet.Text = "blacklist set:";
         // 
         // tbBlackListSet
         // 
         this.tbBlackListSet.Location = new System.Drawing.Point(94, 61);
         this.tbBlackListSet.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
         this.tbBlackListSet.MinimumSize = new System.Drawing.Size(316, 21);
         this.tbBlackListSet.Name = "tbBlackListSet";
         this.tbBlackListSet.Size = new System.Drawing.Size(316, 23);
         this.tbBlackListSet.TabIndex = 33;
         this.tbBlackListSet.TextChanged += new System.EventHandler(this.tbBlackListSet_TextChanged);
         // 
         // updatesPanel
         // 
         this.updatesPanel.Controls.Add(this.lbShowUpdates);
         this.updatesPanel.Controls.Add(this.cboxUpdateNadpis);
         this.updatesPanel.Controls.Add(this.cboxUpdateLocation);
         this.updatesPanel.Controls.Add(this.cboxUpdateCena);
         this.updatesPanel.Controls.Add(this.cboxUpdateDate);
         this.updatesPanel.Controls.Add(this.cboxUpdatePopis);
         this.updatesPanel.Location = new System.Drawing.Point(747, 423);
         this.updatesPanel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
         this.updatesPanel.Name = "updatesPanel";
         this.updatesPanel.Size = new System.Drawing.Size(244, 170);
         this.updatesPanel.TabIndex = 32;
         this.updatesPanel.Visible = false;
         // 
         // lbShowUpdates
         // 
         this.lbShowUpdates.AutoSize = true;
         this.lbShowUpdates.Font = new System.Drawing.Font("Segoe UI", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point);
         this.lbShowUpdates.Location = new System.Drawing.Point(16, 8);
         this.lbShowUpdates.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
         this.lbShowUpdates.Name = "lbShowUpdates";
         this.lbShowUpdates.Size = new System.Drawing.Size(110, 20);
         this.lbShowUpdates.TabIndex = 31;
         this.lbShowUpdates.Tag = "updateCboxes";
         this.lbShowUpdates.Text = "show updates:";
         // 
         // cboxUpdateNadpis
         // 
         this.cboxUpdateNadpis.AutoSize = true;
         this.cboxUpdateNadpis.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
         this.cboxUpdateNadpis.Location = new System.Drawing.Point(31, 37);
         this.cboxUpdateNadpis.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
         this.cboxUpdateNadpis.Name = "cboxUpdateNadpis";
         this.cboxUpdateNadpis.Size = new System.Drawing.Size(68, 21);
         this.cboxUpdateNadpis.TabIndex = 26;
         this.cboxUpdateNadpis.Tag = "updateCboxes";
         this.cboxUpdateNadpis.Text = "nadpis";
         this.cboxUpdateNadpis.UseVisualStyleBackColor = true;
         this.cboxUpdateNadpis.CheckedChanged += new System.EventHandler(this.cboxUpdateNadpis_CheckedChanged);
         // 
         // cboxUpdateLocation
         // 
         this.cboxUpdateLocation.AutoSize = true;
         this.cboxUpdateLocation.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
         this.cboxUpdateLocation.Location = new System.Drawing.Point(31, 140);
         this.cboxUpdateLocation.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
         this.cboxUpdateLocation.Name = "cboxUpdateLocation";
         this.cboxUpdateLocation.Size = new System.Drawing.Size(66, 21);
         this.cboxUpdateLocation.TabIndex = 30;
         this.cboxUpdateLocation.Tag = "updateCboxes";
         this.cboxUpdateLocation.Text = "lokace";
         this.cboxUpdateLocation.UseVisualStyleBackColor = true;
         this.cboxUpdateLocation.CheckedChanged += new System.EventHandler(this.cboxUpdateLocation_CheckedChanged);
         // 
         // cboxUpdateCena
         // 
         this.cboxUpdateCena.AutoSize = true;
         this.cboxUpdateCena.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
         this.cboxUpdateCena.Location = new System.Drawing.Point(31, 65);
         this.cboxUpdateCena.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
         this.cboxUpdateCena.Name = "cboxUpdateCena";
         this.cboxUpdateCena.Size = new System.Drawing.Size(55, 21);
         this.cboxUpdateCena.TabIndex = 27;
         this.cboxUpdateCena.Tag = "updateCboxes";
         this.cboxUpdateCena.Text = "cena";
         this.cboxUpdateCena.UseVisualStyleBackColor = true;
         this.cboxUpdateCena.CheckedChanged += new System.EventHandler(this.cboxUpdateCena_CheckedChanged);
         // 
         // cboxUpdateDate
         // 
         this.cboxUpdateDate.AutoSize = true;
         this.cboxUpdateDate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
         this.cboxUpdateDate.Location = new System.Drawing.Point(31, 115);
         this.cboxUpdateDate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
         this.cboxUpdateDate.Name = "cboxUpdateDate";
         this.cboxUpdateDate.Size = new System.Drawing.Size(67, 21);
         this.cboxUpdateDate.TabIndex = 29;
         this.cboxUpdateDate.Tag = "updateCboxes";
         this.cboxUpdateDate.Text = "datum";
         this.cboxUpdateDate.UseVisualStyleBackColor = true;
         this.cboxUpdateDate.CheckedChanged += new System.EventHandler(this.cboxUpdateDate_CheckedChanged);
         // 
         // cboxUpdatePopis
         // 
         this.cboxUpdatePopis.AutoSize = true;
         this.cboxUpdatePopis.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
         this.cboxUpdatePopis.Location = new System.Drawing.Point(31, 90);
         this.cboxUpdatePopis.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
         this.cboxUpdatePopis.Name = "cboxUpdatePopis";
         this.cboxUpdatePopis.Size = new System.Drawing.Size(61, 21);
         this.cboxUpdatePopis.TabIndex = 28;
         this.cboxUpdatePopis.Tag = "updateCboxes";
         this.cboxUpdatePopis.Text = "popis";
         this.cboxUpdatePopis.UseVisualStyleBackColor = true;
         this.cboxUpdatePopis.CheckedChanged += new System.EventHandler(this.cboxUpdatePopis_CheckedChanged);
         // 
         // btnHelpYouTube
         // 
         this.btnHelpYouTube.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
         this.btnHelpYouTube.Location = new System.Drawing.Point(4, 243);
         this.btnHelpYouTube.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
         this.btnHelpYouTube.Name = "btnHelpYouTube";
         this.btnHelpYouTube.Size = new System.Drawing.Size(86, 51);
         this.btnHelpYouTube.TabIndex = 25;
         this.btnHelpYouTube.Text = "HELP YouTube";
         this.btnHelpYouTube.UseVisualStyleBackColor = true;
         this.btnHelpYouTube.Visible = false;
         this.btnHelpYouTube.Click += new System.EventHandler(this.btnHelpYouTube_Click);
         // 
         // btnHelpQuickFilter
         // 
         this.btnHelpQuickFilter.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
         this.btnHelpQuickFilter.Location = new System.Drawing.Point(4, 187);
         this.btnHelpQuickFilter.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
         this.btnHelpQuickFilter.Name = "btnHelpQuickFilter";
         this.btnHelpQuickFilter.Size = new System.Drawing.Size(86, 50);
         this.btnHelpQuickFilter.TabIndex = 22;
         this.btnHelpQuickFilter.Text = "HELP quickfilter";
         this.btnHelpQuickFilter.UseVisualStyleBackColor = true;
         this.btnHelpQuickFilter.Click += new System.EventHandler(this.btnHelpQuickFilter_Click);
         // 
         // cboxNotUpdateViewedAndLastChecked
         // 
         this.cboxNotUpdateViewedAndLastChecked.AutoSize = true;
         this.cboxNotUpdateViewedAndLastChecked.Location = new System.Drawing.Point(441, 7);
         this.cboxNotUpdateViewedAndLastChecked.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
         this.cboxNotUpdateViewedAndLastChecked.Name = "cboxNotUpdateViewedAndLastChecked";
         this.cboxNotUpdateViewedAndLastChecked.Size = new System.Drawing.Size(301, 19);
         this.cboxNotUpdateViewedAndLastChecked.TabIndex = 21;
         this.cboxNotUpdateViewedAndLastChecked.Text = "dont update viewed and lastchecked to DB (quicker)";
         this.cboxNotUpdateViewedAndLastChecked.UseVisualStyleBackColor = true;
         // 
         // btnApplyQuickFilter
         // 
         this.btnApplyQuickFilter.Location = new System.Drawing.Point(416, 32);
         this.btnApplyQuickFilter.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
         this.btnApplyQuickFilter.Name = "btnApplyQuickFilter";
         this.btnApplyQuickFilter.Size = new System.Drawing.Size(69, 23);
         this.btnApplyQuickFilter.TabIndex = 20;
         this.btnApplyQuickFilter.Text = "apply";
         this.btnApplyQuickFilter.UseVisualStyleBackColor = true;
         this.btnApplyQuickFilter.Click += new System.EventHandler(this.btnApplyQuickFilter_Click);
         // 
         // cbDisableQuickFilterPrice
         // 
         this.cbDisableQuickFilterPrice.AutoSize = true;
         this.cbDisableQuickFilterPrice.Location = new System.Drawing.Point(268, 7);
         this.cbDisableQuickFilterPrice.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
         this.cbDisableQuickFilterPrice.Name = "cbDisableQuickFilterPrice";
         this.cbDisableQuickFilterPrice.Size = new System.Drawing.Size(164, 19);
         this.cbDisableQuickFilterPrice.TabIndex = 19;
         this.cbDisableQuickFilterPrice.Text = "disable price in quick filter";
         this.cbDisableQuickFilterPrice.UseVisualStyleBackColor = true;
         // 
         // cmbSelectQuickFilter
         // 
         this.cmbSelectQuickFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.cmbSelectQuickFilter.FormattingEnabled = true;
         this.cmbSelectQuickFilter.Location = new System.Drawing.Point(572, 33);
         this.cmbSelectQuickFilter.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
         this.cmbSelectQuickFilter.Name = "cmbSelectQuickFilter";
         this.cmbSelectQuickFilter.Size = new System.Drawing.Size(419, 23);
         this.cmbSelectQuickFilter.TabIndex = 18;
         this.cmbSelectQuickFilter.SelectedIndexChanged += new System.EventHandler(this.cmbSelectQuickFilter_SelectedIndexChanged);
         this.cmbSelectQuickFilter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbSelectQuickFilter_KeyDown);
         // 
         // lbLokace
         // 
         this.lbLokace.AutoSize = true;
         this.lbLokace.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
         this.lbLokace.Location = new System.Drawing.Point(803, 62);
         this.lbLokace.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
         this.lbLokace.Name = "lbLokace";
         this.lbLokace.Size = new System.Drawing.Size(57, 21);
         this.lbLokace.TabIndex = 17;
         this.lbLokace.Text = "lokace:";
         // 
         // btnCreateQuickFilter
         // 
         this.btnCreateQuickFilter.Location = new System.Drawing.Point(491, 32);
         this.btnCreateQuickFilter.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
         this.btnCreateQuickFilter.Name = "btnCreateQuickFilter";
         this.btnCreateQuickFilter.Size = new System.Drawing.Size(75, 23);
         this.btnCreateQuickFilter.TabIndex = 16;
         this.btnCreateQuickFilter.Text = "create";
         this.btnCreateQuickFilter.UseVisualStyleBackColor = true;
         this.btnCreateQuickFilter.Click += new System.EventHandler(this.btnCreateQuickFilter_Click);
         // 
         // cboxDownOnlyLast
         // 
         this.cboxDownOnlyLast.AutoSize = true;
         this.cboxDownOnlyLast.Location = new System.Drawing.Point(94, 7);
         this.cboxDownOnlyLast.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
         this.cboxDownOnlyLast.Name = "cboxDownOnlyLast";
         this.cboxDownOnlyLast.Size = new System.Drawing.Size(163, 19);
         this.cboxDownOnlyLast.TabIndex = 15;
         this.cboxDownOnlyLast.Text = "download only new offers";
         this.cboxDownOnlyLast.UseVisualStyleBackColor = true;
         // 
         // tbLokalita
         // 
         this.tbLokalita.Location = new System.Drawing.Point(876, 62);
         this.tbLokalita.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
         this.tbLokalita.Name = "tbLokalita";
         this.tbLokalita.Size = new System.Drawing.Size(116, 23);
         this.tbLokalita.TabIndex = 14;
         // 
         // lbQuickFilter
         // 
         this.lbQuickFilter.AutoSize = true;
         this.lbQuickFilter.Location = new System.Drawing.Point(20, 38);
         this.lbQuickFilter.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
         this.lbQuickFilter.Name = "lbQuickFilter";
         this.lbQuickFilter.Size = new System.Drawing.Size(66, 15);
         this.lbQuickFilter.TabIndex = 13;
         this.lbQuickFilter.Text = "quick filter:";
         // 
         // tbQuickFilter
         // 
         this.tbQuickFilter.Location = new System.Drawing.Point(94, 32);
         this.tbQuickFilter.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
         this.tbQuickFilter.MinimumSize = new System.Drawing.Size(316, 21);
         this.tbQuickFilter.Name = "tbQuickFilter";
         this.tbQuickFilter.Size = new System.Drawing.Size(316, 23);
         this.tbQuickFilter.TabIndex = 11;
         this.tbQuickFilter.TextChanged += new System.EventHandler(this.tbQuickFilter_TextChanged);
         // 
         // cmbSelectOffersType
         // 
         this.cmbSelectOffersType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.cmbSelectOffersType.FormattingEnabled = true;
         this.cmbSelectOffersType.Location = new System.Drawing.Point(491, 92);
         this.cmbSelectOffersType.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
         this.cmbSelectOffersType.Name = "cmbSelectOffersType";
         this.cmbSelectOffersType.Size = new System.Drawing.Size(164, 23);
         this.cmbSelectOffersType.TabIndex = 10;
         this.cmbSelectOffersType.SelectedIndexChanged += new System.EventHandler(this.cmbSelectOffersType_SelectedIndexChanged);
         // 
         // cmbSelectOffers
         // 
         this.cmbSelectOffers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.cmbSelectOffers.FormattingEnabled = true;
         this.cmbSelectOffers.Location = new System.Drawing.Point(662, 92);
         this.cmbSelectOffers.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
         this.cmbSelectOffers.Name = "cmbSelectOffers";
         this.cmbSelectOffers.Size = new System.Drawing.Size(331, 23);
         this.cmbSelectOffers.TabIndex = 9;
         this.cmbSelectOffers.SelectedIndexChanged += new System.EventHandler(this.cmbSelectOffers_SelectedIndexChanged);
         this.cmbSelectOffers.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbSelectOffers_KeyDown);
         // 
         // lbBotRunning
         // 
         this.lbBotRunning.AutoSize = true;
         this.lbBotRunning.Location = new System.Drawing.Point(433, 37);
         this.lbBotRunning.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
         this.lbBotRunning.Name = "lbBotRunning";
         this.lbBotRunning.Size = new System.Drawing.Size(28, 15);
         this.lbBotRunning.TabIndex = 16;
         this.lbBotRunning.Text = "bot:";
         this.lbBotRunning.Visible = false;
         // 
         // panelAutoBot
         // 
         this.panelAutoBot.Controls.Add(this.cboxHideToTray);
         this.panelAutoBot.Controls.Add(this.cboxMultiCategory);
         this.panelAutoBot.Controls.Add(this.lboxBotCategory);
         this.panelAutoBot.Controls.Add(this.btnAddItemsToBot);
         this.panelAutoBot.Controls.Add(this.btnStartBot);
         this.panelAutoBot.Controls.Add(this.lbBotInt);
         this.panelAutoBot.Controls.Add(this.lbBotFT);
         this.panelAutoBot.Controls.Add(this.lbBotHeading);
         this.panelAutoBot.Controls.Add(this.lbBotFullTime);
         this.panelAutoBot.Controls.Add(this.lbBotInterval);
         this.panelAutoBot.Controls.Add(this.BotFullTime);
         this.panelAutoBot.Controls.Add(this.BotInterval);
         this.panelAutoBot.Controls.Add(this.lboxBotQuickFilter);
         this.panelAutoBot.Controls.Add(this.btnCreateBot);
         this.panelAutoBot.Controls.Add(this.lbBotQuickFilter);
         this.panelAutoBot.Controls.Add(this.lbBotCategoryUrl);
         this.panelAutoBot.Controls.Add(this.lbBotName);
         this.panelAutoBot.Controls.Add(this.cmbBotQuickFilter);
         this.panelAutoBot.Controls.Add(this.cmbBotCategoryUrl);
         this.panelAutoBot.Controls.Add(this.cmbBotName);
         this.panelAutoBot.Controls.Add(this.tbBotCategoryUrl);
         this.panelAutoBot.Controls.Add(this.tbBotQuickFilter);
         this.panelAutoBot.Controls.Add(this.tbBotName);
         this.panelAutoBot.Controls.Add(this.btnAddQuickFilterBot);
         this.panelAutoBot.Location = new System.Drawing.Point(18, 5);
         this.panelAutoBot.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
         this.panelAutoBot.Name = "panelAutoBot";
         this.panelAutoBot.Size = new System.Drawing.Size(31, 24);
         this.panelAutoBot.TabIndex = 15;
         // 
         // cboxHideToTray
         // 
         this.cboxHideToTray.AutoSize = true;
         this.cboxHideToTray.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
         this.cboxHideToTray.Location = new System.Drawing.Point(898, 373);
         this.cboxHideToTray.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
         this.cboxHideToTray.Name = "cboxHideToTray";
         this.cboxHideToTray.Size = new System.Drawing.Size(99, 21);
         this.cboxHideToTray.TabIndex = 28;
         this.cboxHideToTray.Text = "hide to tray";
         this.cboxHideToTray.UseVisualStyleBackColor = true;
         // 
         // cboxMultiCategory
         // 
         this.cboxMultiCategory.AutoSize = true;
         this.cboxMultiCategory.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
         this.cboxMultiCategory.Location = new System.Drawing.Point(757, 373);
         this.cboxMultiCategory.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
         this.cboxMultiCategory.Name = "cboxMultiCategory";
         this.cboxMultiCategory.Size = new System.Drawing.Size(114, 21);
         this.cboxMultiCategory.TabIndex = 27;
         this.cboxMultiCategory.Text = "multicategory";
         this.cboxMultiCategory.UseVisualStyleBackColor = true;
         // 
         // lboxBotCategory
         // 
         this.lboxBotCategory.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
         this.lboxBotCategory.FormattingEnabled = true;
         this.lboxBotCategory.HorizontalScrollbar = true;
         this.lboxBotCategory.ItemHeight = 17;
         this.lboxBotCategory.Location = new System.Drawing.Point(398, 222);
         this.lboxBotCategory.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
         this.lboxBotCategory.Name = "lboxBotCategory";
         this.lboxBotCategory.Size = new System.Drawing.Size(325, 395);
         this.lboxBotCategory.TabIndex = 26;
         this.lboxBotCategory.SelectedIndexChanged += new System.EventHandler(this.lboxBotCategory_SelectedIndexChanged);
         this.lboxBotCategory.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lboxBotCategory_KeyDown);
         // 
         // btnAddItemsToBot
         // 
         this.btnAddItemsToBot.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
         this.btnAddItemsToBot.Location = new System.Drawing.Point(602, 170);
         this.btnAddItemsToBot.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
         this.btnAddItemsToBot.Name = "btnAddItemsToBot";
         this.btnAddItemsToBot.Size = new System.Drawing.Size(57, 27);
         this.btnAddItemsToBot.TabIndex = 25;
         this.btnAddItemsToBot.Text = "to bot";
         this.btnAddItemsToBot.UseVisualStyleBackColor = true;
         this.btnAddItemsToBot.Click += new System.EventHandler(this.btnAddItemsToBot_Click);
         // 
         // btnStartBot
         // 
         this.btnStartBot.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
         this.btnStartBot.Location = new System.Drawing.Point(757, 497);
         this.btnStartBot.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
         this.btnStartBot.Name = "btnStartBot";
         this.btnStartBot.Size = new System.Drawing.Size(136, 69);
         this.btnStartBot.TabIndex = 24;
         this.btnStartBot.Text = "Start Bot";
         this.btnStartBot.UseVisualStyleBackColor = true;
         this.btnStartBot.Click += new System.EventHandler(this.btnStartBot_Click);
         // 
         // lbBotInt
         // 
         this.lbBotInt.AutoSize = true;
         this.lbBotInt.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
         this.lbBotInt.Location = new System.Drawing.Point(939, 286);
         this.lbBotInt.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
         this.lbBotInt.Name = "lbBotInt";
         this.lbBotInt.Size = new System.Drawing.Size(57, 17);
         this.lbBotInt.TabIndex = 23;
         this.lbBotInt.Text = "seconds";
         // 
         // lbBotFT
         // 
         this.lbBotFT.AutoSize = true;
         this.lbBotFT.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
         this.lbBotFT.Location = new System.Drawing.Point(939, 329);
         this.lbBotFT.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
         this.lbBotFT.Name = "lbBotFT";
         this.lbBotFT.Size = new System.Drawing.Size(75, 17);
         this.lbBotFT.TabIndex = 22;
         this.lbBotFT.Text = "times used";
         // 
         // lbBotHeading
         // 
         this.lbBotHeading.AutoSize = true;
         this.lbBotHeading.Font = new System.Drawing.Font("Segoe UI", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point);
         this.lbBotHeading.Location = new System.Drawing.Point(334, 27);
         this.lbBotHeading.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
         this.lbBotHeading.Name = "lbBotHeading";
         this.lbBotHeading.Size = new System.Drawing.Size(125, 37);
         this.lbBotHeading.TabIndex = 21;
         this.lbBotHeading.Text = "AutoBot";
         // 
         // lbBotFullTime
         // 
         this.lbBotFullTime.AutoSize = true;
         this.lbBotFullTime.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
         this.lbBotFullTime.Location = new System.Drawing.Point(744, 333);
         this.lbBotFullTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
         this.lbBotFullTime.Name = "lbBotFullTime";
         this.lbBotFullTime.Size = new System.Drawing.Size(66, 17);
         this.lbBotFullTime.TabIndex = 20;
         this.lbBotFullTime.Text = "FullTime:";
         // 
         // lbBotInterval
         // 
         this.lbBotInterval.AutoSize = true;
         this.lbBotInterval.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
         this.lbBotInterval.Location = new System.Drawing.Point(744, 291);
         this.lbBotInterval.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
         this.lbBotInterval.Name = "lbBotInterval";
         this.lbBotInterval.Size = new System.Drawing.Size(60, 17);
         this.lbBotInterval.TabIndex = 19;
         this.lbBotInterval.Text = "Interval:";
         // 
         // BotFullTime
         // 
         this.BotFullTime.Location = new System.Drawing.Point(828, 329);
         this.BotFullTime.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
         this.BotFullTime.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
         this.BotFullTime.Name = "BotFullTime";
         this.BotFullTime.Size = new System.Drawing.Size(106, 23);
         this.BotFullTime.TabIndex = 18;
         this.BotFullTime.ValueChanged += new System.EventHandler(this.BotFullTime_ValueChanged);
         // 
         // BotInterval
         // 
         this.BotInterval.Location = new System.Drawing.Point(828, 286);
         this.BotInterval.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
         this.BotInterval.Maximum = new decimal(new int[] {
            86400,
            0,
            0,
            0});
         this.BotInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
         this.BotInterval.Name = "BotInterval";
         this.BotInterval.Size = new System.Drawing.Size(106, 23);
         this.BotInterval.TabIndex = 17;
         this.BotInterval.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
         this.BotInterval.ValueChanged += new System.EventHandler(this.BotInterval_ValueChanged);
         // 
         // lboxBotQuickFilter
         // 
         this.lboxBotQuickFilter.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
         this.lboxBotQuickFilter.FormattingEnabled = true;
         this.lboxBotQuickFilter.HorizontalScrollbar = true;
         this.lboxBotQuickFilter.ItemHeight = 17;
         this.lboxBotQuickFilter.Location = new System.Drawing.Point(38, 222);
         this.lboxBotQuickFilter.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
         this.lboxBotQuickFilter.Name = "lboxBotQuickFilter";
         this.lboxBotQuickFilter.Size = new System.Drawing.Size(341, 395);
         this.lboxBotQuickFilter.TabIndex = 14;
         this.lboxBotQuickFilter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lboxBotQuickFilter_KeyDown);
         this.lboxBotQuickFilter.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lboxBotQuickFilter_MouseDown);
         // 
         // btnCreateBot
         // 
         this.btnCreateBot.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
         this.btnCreateBot.Location = new System.Drawing.Point(757, 411);
         this.btnCreateBot.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
         this.btnCreateBot.Name = "btnCreateBot";
         this.btnCreateBot.Size = new System.Drawing.Size(136, 69);
         this.btnCreateBot.TabIndex = 13;
         this.btnCreateBot.Text = "Create Bot";
         this.btnCreateBot.UseVisualStyleBackColor = true;
         this.btnCreateBot.Click += new System.EventHandler(this.btnCreateBot_Click);
         // 
         // lbBotQuickFilter
         // 
         this.lbBotQuickFilter.AutoSize = true;
         this.lbBotQuickFilter.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
         this.lbBotQuickFilter.Location = new System.Drawing.Point(29, 166);
         this.lbBotQuickFilter.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
         this.lbBotQuickFilter.Name = "lbBotQuickFilter";
         this.lbBotQuickFilter.Size = new System.Drawing.Size(98, 21);
         this.lbBotQuickFilter.TabIndex = 11;
         this.lbBotQuickFilter.Text = "quick filter:";
         // 
         // lbBotCategoryUrl
         // 
         this.lbBotCategoryUrl.AutoSize = true;
         this.lbBotCategoryUrl.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
         this.lbBotCategoryUrl.Location = new System.Drawing.Point(20, 126);
         this.lbBotCategoryUrl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
         this.lbBotCategoryUrl.Name = "lbBotCategoryUrl";
         this.lbBotCategoryUrl.Size = new System.Drawing.Size(107, 21);
         this.lbBotCategoryUrl.TabIndex = 10;
         this.lbBotCategoryUrl.Text = "category url:";
         // 
         // lbBotName
         // 
         this.lbBotName.AutoSize = true;
         this.lbBotName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
         this.lbBotName.Location = new System.Drawing.Point(20, 89);
         this.lbBotName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
         this.lbBotName.Name = "lbBotName";
         this.lbBotName.Size = new System.Drawing.Size(107, 21);
         this.lbBotName.TabIndex = 9;
         this.lbBotName.Text = "name of Bot:";
         // 
         // cmbBotQuickFilter
         // 
         this.cmbBotQuickFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.cmbBotQuickFilter.FormattingEnabled = true;
         this.cmbBotQuickFilter.Location = new System.Drawing.Point(666, 170);
         this.cmbBotQuickFilter.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
         this.cmbBotQuickFilter.Name = "cmbBotQuickFilter";
         this.cmbBotQuickFilter.Size = new System.Drawing.Size(284, 23);
         this.cmbBotQuickFilter.TabIndex = 8;
         this.cmbBotQuickFilter.SelectedIndexChanged += new System.EventHandler(this.cmbQuickFilterBot_SelectedIndexChanged);
         this.cmbBotQuickFilter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbBotQuickFilter_KeyDown);
         // 
         // cmbBotCategoryUrl
         // 
         this.cmbBotCategoryUrl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.cmbBotCategoryUrl.FormattingEnabled = true;
         this.cmbBotCategoryUrl.Location = new System.Drawing.Point(542, 130);
         this.cmbBotCategoryUrl.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
         this.cmbBotCategoryUrl.Name = "cmbBotCategoryUrl";
         this.cmbBotCategoryUrl.Size = new System.Drawing.Size(406, 23);
         this.cmbBotCategoryUrl.Sorted = true;
         this.cmbBotCategoryUrl.TabIndex = 7;
         this.cmbBotCategoryUrl.SelectedIndexChanged += new System.EventHandler(this.cmbCategoryUrlBot_SelectedIndexChanged);
         // 
         // cmbBotName
         // 
         this.cmbBotName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.cmbBotName.FormattingEnabled = true;
         this.cmbBotName.Location = new System.Drawing.Point(542, 89);
         this.cmbBotName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
         this.cmbBotName.Name = "cmbBotName";
         this.cmbBotName.Size = new System.Drawing.Size(406, 23);
         this.cmbBotName.TabIndex = 6;
         this.cmbBotName.SelectedIndexChanged += new System.EventHandler(this.cmbBotName_SelectedIndexChanged);
         this.cmbBotName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbBotName_KeyDown);
         // 
         // tbBotCategoryUrl
         // 
         this.tbBotCategoryUrl.Location = new System.Drawing.Point(152, 130);
         this.tbBotCategoryUrl.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
         this.tbBotCategoryUrl.Name = "tbBotCategoryUrl";
         this.tbBotCategoryUrl.Size = new System.Drawing.Size(383, 23);
         this.tbBotCategoryUrl.TabIndex = 5;
         // 
         // tbBotQuickFilter
         // 
         this.tbBotQuickFilter.Location = new System.Drawing.Point(152, 170);
         this.tbBotQuickFilter.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
         this.tbBotQuickFilter.Name = "tbBotQuickFilter";
         this.tbBotQuickFilter.Size = new System.Drawing.Size(383, 23);
         this.tbBotQuickFilter.TabIndex = 4;
         this.tbBotQuickFilter.TextChanged += new System.EventHandler(this.tbBotQuickFilter_TextChanged);
         // 
         // tbBotName
         // 
         this.tbBotName.Location = new System.Drawing.Point(152, 89);
         this.tbBotName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
         this.tbBotName.Name = "tbBotName";
         this.tbBotName.Size = new System.Drawing.Size(383, 23);
         this.tbBotName.TabIndex = 2;
         // 
         // btnAddQuickFilterBot
         // 
         this.btnAddQuickFilterBot.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
         this.btnAddQuickFilterBot.Location = new System.Drawing.Point(542, 170);
         this.btnAddQuickFilterBot.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
         this.btnAddQuickFilterBot.Name = "btnAddQuickFilterBot";
         this.btnAddQuickFilterBot.Size = new System.Drawing.Size(54, 27);
         this.btnAddQuickFilterBot.TabIndex = 1;
         this.btnAddQuickFilterBot.Text = "add";
         this.btnAddQuickFilterBot.UseVisualStyleBackColor = true;
         this.btnAddQuickFilterBot.Click += new System.EventHandler(this.btnAddQuickFilterBot_Click);
         // 
         // btnSelectPanel
         // 
         this.btnSelectPanel.Location = new System.Drawing.Point(21, 5);
         this.btnSelectPanel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
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
         this.cmbSelectPanel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
         this.cmbSelectPanel.Name = "cmbSelectPanel";
         this.cmbSelectPanel.Size = new System.Drawing.Size(121, 23);
         this.cmbSelectPanel.TabIndex = 7;
         this.cmbSelectPanel.SelectedIndexChanged += new System.EventHandler(this.cmbSelectPanel_SelectedIndexChanged);
         // 
         // lboxBlacklistSet
         // 
         this.lboxBlacklistSet.Location = new System.Drawing.Point(0, 0);
         this.lboxBlacklistSet.Name = "lboxBlacklistSet";
         this.lboxBlacklistSet.Size = new System.Drawing.Size(120, 96);
         this.lboxBlacklistSet.TabIndex = 0;
         // 
         // lbUpdatedCount
         // 
         this.lbUpdatedCount.AutoSize = true;
         this.lbUpdatedCount.Location = new System.Drawing.Point(486, 12);
         this.lbUpdatedCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
         this.lbUpdatedCount.Name = "lbUpdatedCount";
         this.lbUpdatedCount.Size = new System.Drawing.Size(54, 15);
         this.lbUpdatedCount.TabIndex = 9;
         this.lbUpdatedCount.Text = "updated:";
         // 
         // lbDeletedCount
         // 
         this.lbDeletedCount.AutoSize = true;
         this.lbDeletedCount.Location = new System.Drawing.Point(617, 13);
         this.lbDeletedCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
         this.lbDeletedCount.Name = "lbDeletedCount";
         this.lbDeletedCount.Size = new System.Drawing.Size(49, 15);
         this.lbDeletedCount.TabIndex = 10;
         this.lbDeletedCount.Text = "deleted:";
         // 
         // lbNewOffers
         // 
         this.lbNewOffers.AutoSize = true;
         this.lbNewOffers.Location = new System.Drawing.Point(355, 12);
         this.lbNewOffers.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
         this.lbNewOffers.Name = "lbNewOffers";
         this.lbNewOffers.Size = new System.Drawing.Size(65, 15);
         this.lbNewOffers.TabIndex = 11;
         this.lbNewOffers.Text = "new offers:";
         // 
         // lbAllOffers
         // 
         this.lbAllOffers.AutoSize = true;
         this.lbAllOffers.Location = new System.Drawing.Point(243, 12);
         this.lbAllOffers.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
         this.lbAllOffers.Name = "lbAllOffers";
         this.lbAllOffers.Size = new System.Drawing.Size(55, 15);
         this.lbAllOffers.TabIndex = 13;
         this.lbAllOffers.Text = "all offers:";
         // 
         // lbProgress
         // 
         this.lbProgress.AutoSize = true;
         this.lbProgress.Location = new System.Drawing.Point(768, 12);
         this.lbProgress.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
         this.lbProgress.Name = "lbProgress";
         this.lbProgress.Size = new System.Drawing.Size(55, 15);
         this.lbProgress.TabIndex = 14;
         this.lbProgress.Text = "progress:";
         this.lbProgress.Visible = false;
         // 
         // settingsPanel
         // 
         this.settingsPanel.Controls.Add(this.cboxHelpButton);
         this.settingsPanel.Location = new System.Drawing.Point(561, 13);
         this.settingsPanel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
         this.settingsPanel.Name = "settingsPanel";
         this.settingsPanel.Size = new System.Drawing.Size(22, 29);
         this.settingsPanel.TabIndex = 17;
         // 
         // cboxHelpButton
         // 
         this.cboxHelpButton.AutoSize = true;
         this.cboxHelpButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
         this.cboxHelpButton.Location = new System.Drawing.Point(28, 24);
         this.cboxHelpButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
         this.cboxHelpButton.Name = "cboxHelpButton";
         this.cboxHelpButton.Size = new System.Drawing.Size(100, 21);
         this.cboxHelpButton.TabIndex = 0;
         this.cboxHelpButton.Text = "help button";
         this.cboxHelpButton.UseVisualStyleBackColor = true;
         this.cboxHelpButton.CheckedChanged += new System.EventHandler(this.cboxHelpButton_CheckedChanged);
         // 
         // notifyIcon
         // 
         this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
         this.notifyIcon.Text = "Bazos bot";
         this.notifyIcon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDown);
         // 
         // Form1
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(1059, 681);
         this.Controls.Add(this.settingsPanel);
         this.Controls.Add(this.lbBotRunning);
         this.Controls.Add(this.panelAutoBot);
         this.Controls.Add(this.lbProgress);
         this.Controls.Add(this.lbAllOffers);
         this.Controls.Add(this.lbNewOffers);
         this.Controls.Add(this.lbDeletedCount);
         this.Controls.Add(this.lbUpdatedCount);
         this.Controls.Add(this.cmbSelectPanel);
         this.Controls.Add(this.btnSelectPanel);
         this.Controls.Add(this.panelMain);
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
         this.MinimumSize = new System.Drawing.Size(1066, 422);
         this.Name = "Form1";
         this.Text = "Bazos bot";
         this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
         this.Resize += new System.EventHandler(this.Form1_Resize);
         this.panelMain.ResumeLayout(false);
         this.panelMain.PerformLayout();
         this.updatesPanel.ResumeLayout(false);
         this.updatesPanel.PerformLayout();
         this.panelAutoBot.ResumeLayout(false);
         this.panelAutoBot.PerformLayout();
         ((System.ComponentModel.ISupportInitialize)(this.BotFullTime)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.BotInterval)).EndInit();
         this.settingsPanel.ResumeLayout(false);
         this.settingsPanel.PerformLayout();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Button btnGetBazos;
      private System.Windows.Forms.ListBox resultLbox;
      private System.Windows.Forms.Label lbBaseUrl;
      private System.Windows.Forms.ListBox offerLbox;
      private System.Windows.Forms.TextBox tbSearchUrl;
      private System.Windows.Forms.Panel panelMain;
      private System.Windows.Forms.Button btnSelectPanel;
      private System.Windows.Forms.ComboBox cmbSelectPanel;
      //private System.Windows.Forms.TextBox textBox2;
      private System.Windows.Forms.ComboBox cmbSelectOffers;
      //private System.Windows.Forms.TextBox textBox9;
      private System.Windows.Forms.ListBox lboxBlacklistSet;
      private System.Windows.Forms.ComboBox cmbSelectOffersType;
      private System.Windows.Forms.Label lbUpdatedCount;
      private System.Windows.Forms.Label lbDeletedCount;
      private System.Windows.Forms.Label lbNewOffers;
      private System.Windows.Forms.TextBox tbLokalita;
      private System.Windows.Forms.Label lbQuickFilter;
      private System.Windows.Forms.TextBox tbQuickFilter;
      private System.Windows.Forms.CheckBox cboxDownOnlyLast;
      private System.Windows.Forms.Label lbAllOffers;
      private System.Windows.Forms.Button btnCreateQuickFilter;
      private System.Windows.Forms.Label lbLokace;
      private System.Windows.Forms.ComboBox cmbSelectQuickFilter;
      private System.Windows.Forms.CheckBox cbDisableQuickFilterPrice;
      private System.Windows.Forms.Label lbProgress;
      private System.Windows.Forms.Button btnApplyQuickFilter;
      private System.Windows.Forms.Panel panelAutoBot;
      private System.Windows.Forms.ComboBox cmbBotName;
      private System.Windows.Forms.TextBox tbBotCategoryUrl;
      private System.Windows.Forms.TextBox tbBotQuickFilter;
      private System.Windows.Forms.TextBox tbBotName;
      private System.Windows.Forms.Button btnAddQuickFilterBot;
      private System.Windows.Forms.ListBox lboxBotQuickFilter;
      private System.Windows.Forms.Button btnCreateBot;
      private System.Windows.Forms.Label lbBotQuickFilter;
      private System.Windows.Forms.Label lbBotCategoryUrl;
      private System.Windows.Forms.Label lbBotName;
      private System.Windows.Forms.ComboBox cmbBotQuickFilter;
      private System.Windows.Forms.ComboBox cmbBotCategoryUrl;
      private System.Windows.Forms.Label lbBotInt;
      private System.Windows.Forms.Label lbBotFT;
      private System.Windows.Forms.Label lbBotHeading;
      private System.Windows.Forms.Label lbBotFullTime;
      private System.Windows.Forms.Label lbBotInterval;
      private System.Windows.Forms.NumericUpDown BotFullTime;
      private System.Windows.Forms.NumericUpDown BotInterval;
      private System.Windows.Forms.Button btnStartBot;
      private System.Windows.Forms.CheckBox cboxNotUpdateViewedAndLastChecked;
      private System.Windows.Forms.Button btnHelpYouTube;
      private System.Windows.Forms.Button btnHelpQuickFilter;
      private System.Windows.Forms.Button btnAddItemsToBot;
      private System.Windows.Forms.ListBox lboxBotCategory;
      private System.Windows.Forms.CheckBox cboxMultiCategory;
      private System.Windows.Forms.CheckBox cboxUpdateDate;
      private System.Windows.Forms.CheckBox cboxUpdatePopis;
      private System.Windows.Forms.CheckBox cboxUpdateCena;
      private System.Windows.Forms.CheckBox cboxUpdateNadpis;
      private System.Windows.Forms.CheckBox cboxUpdateLocation;
      private System.Windows.Forms.Label lbShowUpdates;
      private System.Windows.Forms.Panel updatesPanel;
      private System.Windows.Forms.Label lbBotRunning;
      private System.Windows.Forms.Button btnAddBlacklistSetToQuickFilter;
      private System.Windows.Forms.ComboBox cmbSelectBlacklistSet;
      private System.Windows.Forms.Button btnCreateBlacklistSet;
      private System.Windows.Forms.Label lbBlacklistSet;
      private System.Windows.Forms.TextBox tbBlackListSet;
      private System.Windows.Forms.Panel settingsPanel;
      private System.Windows.Forms.CheckBox cboxHideToTray;
      private System.Windows.Forms.NotifyIcon notifyIcon;
      private System.Windows.Forms.CheckBox cboxHelpButton;
      private System.Windows.Forms.CheckBox cboxCheckNotFullPopis;
   }
}

