
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
         this.textBox9 = new System.Windows.Forms.TextBox();
         this.btnSelectPanel = new System.Windows.Forms.Button();
         this.cmbSelectPanel = new System.Windows.Forms.ComboBox();
         this.textBox2 = new System.Windows.Forms.TextBox();
         this.lboxBlacklistSet = new System.Windows.Forms.ListBox();
         this.lbUpdatedCount = new System.Windows.Forms.Label();
         this.lbDeletedCount = new System.Windows.Forms.Label();
         this.lbNewOffers = new System.Windows.Forms.Label();
         this.lbAllOffers = new System.Windows.Forms.Label();
         this.lbProgress = new System.Windows.Forms.Label();
         this.panelBot = new System.Windows.Forms.Panel();
         this.cmbBot = new System.Windows.Forms.ComboBox();
         this.tbCategoryURL = new System.Windows.Forms.TextBox();
         this.tbBotQuickFilter = new System.Windows.Forms.TextBox();
         this.tbBotName = new System.Windows.Forms.TextBox();
         this.btnAddToQuickFilterBot = new System.Windows.Forms.Button();
         this.cmbCategoryUrlBot = new System.Windows.Forms.ComboBox();
         this.cmbQuickFilterBot = new System.Windows.Forms.ComboBox();
         this.lbBotName = new System.Windows.Forms.Label();
         this.lbCategoryUrl = new System.Windows.Forms.Label();
         this.lbBotNewQuickFilter = new System.Windows.Forms.Label();
         this.btnCreateBot = new System.Windows.Forms.Button();
         this.lboxBotQuickFilter = new System.Windows.Forms.ListBox();
         this.BotInterval = new System.Windows.Forms.NumericUpDown();
         this.BotFullTime = new System.Windows.Forms.NumericUpDown();
         this.lbBotInterval = new System.Windows.Forms.Label();
         this.lbBotFullTime = new System.Windows.Forms.Label();
         this.lbBotHeading = new System.Windows.Forms.Label();
         this.lbBotFT = new System.Windows.Forms.Label();
         this.lbBotInt = new System.Windows.Forms.Label();
         this.btnStartBot = new System.Windows.Forms.Button();
         this.panelMain.SuspendLayout();
         this.panelBot.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.BotInterval)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.BotFullTime)).BeginInit();
         this.SuspendLayout();
         // 
         // btnGetBazos
         // 
         this.btnGetBazos.Location = new System.Drawing.Point(9, 116);
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
         this.resultLbox.Location = new System.Drawing.Point(95, 103);
         this.resultLbox.Name = "resultLbox";
         this.resultLbox.Size = new System.Drawing.Size(632, 469);
         this.resultLbox.TabIndex = 3;
         this.resultLbox.SelectedIndexChanged += new System.EventHandler(this.resultLbox_SelectedIndexChanged);
         this.resultLbox.DoubleClick += new System.EventHandler(this.resultLbox_DoubleClick);
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(38, 68);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(51, 15);
         this.label1.TabIndex = 1;
         this.label1.Text = "base url:";
         // 
         // offerLbox
         // 
         this.offerLbox.FormattingEnabled = true;
         this.offerLbox.ItemHeight = 15;
         this.offerLbox.Location = new System.Drawing.Point(747, 103);
         this.offerLbox.Name = "offerLbox";
         this.offerLbox.Size = new System.Drawing.Size(244, 469);
         this.offerLbox.TabIndex = 4;
         this.offerLbox.DoubleClick += new System.EventHandler(this.offerLbox_DoubleClick);
         // 
         // tbSearchUrl
         // 
         this.tbSearchUrl.Location = new System.Drawing.Point(95, 65);
         this.tbSearchUrl.Name = "tbSearchUrl";
         this.tbSearchUrl.Size = new System.Drawing.Size(390, 23);
         this.tbSearchUrl.TabIndex = 0;
         // 
         // panelMain
         // 
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
         this.panelMain.Controls.Add(this.textBox9);
         this.panelMain.Controls.Add(this.tbSearchUrl);
         this.panelMain.Controls.Add(this.offerLbox);
         this.panelMain.Controls.Add(this.resultLbox);
         this.panelMain.Controls.Add(this.label1);
         this.panelMain.Controls.Add(this.btnGetBazos);
         this.panelMain.Location = new System.Drawing.Point(1015, 614);
         this.panelMain.Name = "panelMain";
         this.panelMain.Size = new System.Drawing.Size(22, 19);
         this.panelMain.TabIndex = 5;
         this.panelMain.Tag = "mainPanels";
         // 
         // btnApplyQuickFilter
         // 
         this.btnApplyQuickFilter.Location = new System.Drawing.Point(416, 32);
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
         this.cbDisableQuickFilterPrice.Location = new System.Drawing.Point(264, 12);
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
         this.cmbSelectQuickFilter.Name = "cmbSelectQuickFilter";
         this.cmbSelectQuickFilter.Size = new System.Drawing.Size(225, 23);
         this.cmbSelectQuickFilter.TabIndex = 18;
         this.cmbSelectQuickFilter.SelectedIndexChanged += new System.EventHandler(this.cmbSelectQuickFilter_SelectedIndexChanged);
         // 
         // lbLokace
         // 
         this.lbLokace.AutoSize = true;
         this.lbLokace.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
         this.lbLokace.Location = new System.Drawing.Point(803, 33);
         this.lbLokace.Name = "lbLokace";
         this.lbLokace.Size = new System.Drawing.Size(57, 21);
         this.lbLokace.TabIndex = 17;
         this.lbLokace.Text = "lokace:";
         // 
         // btnCreateQuickFilter
         // 
         this.btnCreateQuickFilter.Location = new System.Drawing.Point(491, 32);
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
         this.cboxDownOnlyLast.Location = new System.Drawing.Point(95, 12);
         this.cboxDownOnlyLast.Name = "cboxDownOnlyLast";
         this.cboxDownOnlyLast.Size = new System.Drawing.Size(163, 19);
         this.cboxDownOnlyLast.TabIndex = 15;
         this.cboxDownOnlyLast.Text = "download only new offers";
         this.cboxDownOnlyLast.UseVisualStyleBackColor = true;
         // 
         // tbLokalita
         // 
         this.tbLokalita.Location = new System.Drawing.Point(866, 33);
         this.tbLokalita.Name = "tbLokalita";
         this.tbLokalita.Size = new System.Drawing.Size(104, 23);
         this.tbLokalita.TabIndex = 14;
         // 
         // lbQuickFilter
         // 
         this.lbQuickFilter.AutoSize = true;
         this.lbQuickFilter.Location = new System.Drawing.Point(20, 38);
         this.lbQuickFilter.Name = "lbQuickFilter";
         this.lbQuickFilter.Size = new System.Drawing.Size(66, 15);
         this.lbQuickFilter.TabIndex = 13;
         this.lbQuickFilter.Text = "quick filter:";
         // 
         // tbQuickFilter
         // 
         this.tbQuickFilter.Location = new System.Drawing.Point(95, 32);
         this.tbQuickFilter.Name = "tbQuickFilter";
         this.tbQuickFilter.Size = new System.Drawing.Size(315, 23);
         this.tbQuickFilter.TabIndex = 11;
         // 
         // cmbSelectOffersType
         // 
         this.cmbSelectOffersType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.cmbSelectOffersType.FormattingEnabled = true;
         this.cmbSelectOffersType.Location = new System.Drawing.Point(496, 65);
         this.cmbSelectOffersType.Name = "cmbSelectOffersType";
         this.cmbSelectOffersType.Size = new System.Drawing.Size(159, 23);
         this.cmbSelectOffersType.TabIndex = 10;
         this.cmbSelectOffersType.SelectedIndexChanged += new System.EventHandler(this.cmbSelectOffersType_SelectedIndexChanged);
         // 
         // cmbSelectOffers
         // 
         this.cmbSelectOffers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.cmbSelectOffers.FormattingEnabled = true;
         this.cmbSelectOffers.Location = new System.Drawing.Point(670, 65);
         this.cmbSelectOffers.Name = "cmbSelectOffers";
         this.cmbSelectOffers.Size = new System.Drawing.Size(300, 23);
         this.cmbSelectOffers.TabIndex = 9;
         this.cmbSelectOffers.SelectedIndexChanged += new System.EventHandler(this.cmbSelectOffers_SelectedIndexChanged);
         // 
         // textBox9
         // 
         this.textBox9.Location = new System.Drawing.Point(223, -22);
         this.textBox9.Name = "textBox9";
         this.textBox9.Size = new System.Drawing.Size(187, 23);
         this.textBox9.TabIndex = 3;
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
         this.textBox2.Location = new System.Drawing.Point(0, 0);
         this.textBox2.Name = "textBox2";
         this.textBox2.Size = new System.Drawing.Size(100, 23);
         this.textBox2.TabIndex = 0;
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
         this.lbUpdatedCount.Location = new System.Drawing.Point(487, 12);
         this.lbUpdatedCount.Name = "lbUpdatedCount";
         this.lbUpdatedCount.Size = new System.Drawing.Size(54, 15);
         this.lbUpdatedCount.TabIndex = 9;
         this.lbUpdatedCount.Text = "updated:";
         // 
         // lbDeletedCount
         // 
         this.lbDeletedCount.AutoSize = true;
         this.lbDeletedCount.Location = new System.Drawing.Point(617, 13);
         this.lbDeletedCount.Name = "lbDeletedCount";
         this.lbDeletedCount.Size = new System.Drawing.Size(49, 15);
         this.lbDeletedCount.TabIndex = 10;
         this.lbDeletedCount.Text = "deleted:";
         // 
         // lbNewOffers
         // 
         this.lbNewOffers.AutoSize = true;
         this.lbNewOffers.Location = new System.Drawing.Point(355, 12);
         this.lbNewOffers.Name = "lbNewOffers";
         this.lbNewOffers.Size = new System.Drawing.Size(65, 15);
         this.lbNewOffers.TabIndex = 11;
         this.lbNewOffers.Text = "new offers:";
         // 
         // lbAllOffers
         // 
         this.lbAllOffers.AutoSize = true;
         this.lbAllOffers.Location = new System.Drawing.Point(243, 12);
         this.lbAllOffers.Name = "lbAllOffers";
         this.lbAllOffers.Size = new System.Drawing.Size(55, 15);
         this.lbAllOffers.TabIndex = 13;
         this.lbAllOffers.Text = "all offers:";
         // 
         // lbProgress
         // 
         this.lbProgress.AutoSize = true;
         this.lbProgress.Location = new System.Drawing.Point(768, 13);
         this.lbProgress.Name = "lbProgress";
         this.lbProgress.Size = new System.Drawing.Size(55, 15);
         this.lbProgress.TabIndex = 14;
         this.lbProgress.Text = "progress:";
         this.lbProgress.Visible = false;
         // 
         // panelBot
         // 
         this.panelBot.Controls.Add(this.btnStartBot);
         this.panelBot.Controls.Add(this.lbBotInt);
         this.panelBot.Controls.Add(this.lbBotFT);
         this.panelBot.Controls.Add(this.lbBotHeading);
         this.panelBot.Controls.Add(this.lbBotFullTime);
         this.panelBot.Controls.Add(this.lbBotInterval);
         this.panelBot.Controls.Add(this.BotFullTime);
         this.panelBot.Controls.Add(this.BotInterval);
         this.panelBot.Controls.Add(this.lboxBotQuickFilter);
         this.panelBot.Controls.Add(this.btnCreateBot);
         this.panelBot.Controls.Add(this.lbBotNewQuickFilter);
         this.panelBot.Controls.Add(this.lbCategoryUrl);
         this.panelBot.Controls.Add(this.lbBotName);
         this.panelBot.Controls.Add(this.cmbQuickFilterBot);
         this.panelBot.Controls.Add(this.cmbCategoryUrlBot);
         this.panelBot.Controls.Add(this.cmbBot);
         this.panelBot.Controls.Add(this.tbCategoryURL);
         this.panelBot.Controls.Add(this.tbBotQuickFilter);
         this.panelBot.Controls.Add(this.tbBotName);
         this.panelBot.Controls.Add(this.btnAddToQuickFilterBot);
         this.panelBot.Location = new System.Drawing.Point(21, 48);
         this.panelBot.Name = "panelBot";
         this.panelBot.Size = new System.Drawing.Size(952, 585);
         this.panelBot.TabIndex = 15;
         // 
         // cmbBot
         // 
         this.cmbBot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.cmbBot.FormattingEnabled = true;
         this.cmbBot.Location = new System.Drawing.Point(416, 89);
         this.cmbBot.Name = "cmbBot";
         this.cmbBot.Size = new System.Drawing.Size(318, 23);
         this.cmbBot.TabIndex = 6;
         this.cmbBot.SelectedIndexChanged += new System.EventHandler(this.cmbBot_SelectedIndexChanged);
         // 
         // tbCategoryURL
         // 
         this.tbCategoryURL.Location = new System.Drawing.Point(172, 130);
         this.tbCategoryURL.Name = "tbCategoryURL";
         this.tbCategoryURL.Size = new System.Drawing.Size(220, 23);
         this.tbCategoryURL.TabIndex = 5;
         // 
         // tbBotQuickFilter
         // 
         this.tbBotQuickFilter.Location = new System.Drawing.Point(172, 170);
         this.tbBotQuickFilter.Name = "tbBotQuickFilter";
         this.tbBotQuickFilter.Size = new System.Drawing.Size(220, 23);
         this.tbBotQuickFilter.TabIndex = 4;
         // 
         // tbBotName
         // 
         this.tbBotName.Location = new System.Drawing.Point(172, 89);
         this.tbBotName.Name = "tbBotName";
         this.tbBotName.Size = new System.Drawing.Size(220, 23);
         this.tbBotName.TabIndex = 2;
         // 
         // btnAddToQuickFilterBot
         // 
         this.btnAddToQuickFilterBot.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
         this.btnAddToQuickFilterBot.Location = new System.Drawing.Point(416, 170);
         this.btnAddToQuickFilterBot.Name = "btnAddToQuickFilterBot";
         this.btnAddToQuickFilterBot.Size = new System.Drawing.Size(75, 23);
         this.btnAddToQuickFilterBot.TabIndex = 1;
         this.btnAddToQuickFilterBot.Text = "add";
         this.btnAddToQuickFilterBot.UseVisualStyleBackColor = true;
         this.btnAddToQuickFilterBot.Click += new System.EventHandler(this.btnAddToQuickFilterBot_Click);
         // 
         // cmbCategoryUrlBot
         // 
         this.cmbCategoryUrlBot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.cmbCategoryUrlBot.FormattingEnabled = true;
         this.cmbCategoryUrlBot.Location = new System.Drawing.Point(416, 132);
         this.cmbCategoryUrlBot.Name = "cmbCategoryUrlBot";
         this.cmbCategoryUrlBot.Size = new System.Drawing.Size(318, 23);
         this.cmbCategoryUrlBot.TabIndex = 7;
         this.cmbCategoryUrlBot.SelectedIndexChanged += new System.EventHandler(this.cmbCategoryUrlBot_SelectedIndexChanged);
         // 
         // cmbQuickFilterBot
         // 
         this.cmbQuickFilterBot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.cmbQuickFilterBot.FormattingEnabled = true;
         this.cmbQuickFilterBot.Location = new System.Drawing.Point(517, 170);
         this.cmbQuickFilterBot.Name = "cmbQuickFilterBot";
         this.cmbQuickFilterBot.Size = new System.Drawing.Size(217, 23);
         this.cmbQuickFilterBot.TabIndex = 8;
         this.cmbQuickFilterBot.SelectedIndexChanged += new System.EventHandler(this.cmbQuickFilterBot_SelectedIndexChanged);
         // 
         // lbBotName
         // 
         this.lbBotName.AutoSize = true;
         this.lbBotName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
         this.lbBotName.Location = new System.Drawing.Point(24, 87);
         this.lbBotName.Name = "lbBotName";
         this.lbBotName.Size = new System.Drawing.Size(142, 21);
         this.lbBotName.TabIndex = 9;
         this.lbBotName.Text = "name of Bot:";
         // 
         // lbCategoryUrl
         // 
         this.lbCategoryUrl.AutoSize = true;
         this.lbCategoryUrl.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
         this.lbCategoryUrl.Location = new System.Drawing.Point(59, 128);
         this.lbCategoryUrl.Name = "lbCategoryUrl";
         this.lbCategoryUrl.Size = new System.Drawing.Size(107, 21);
         this.lbCategoryUrl.TabIndex = 10;
         this.lbCategoryUrl.Text = "category url:";
         // 
         // lbBotNewQuickFilter
         // 
         this.lbBotNewQuickFilter.AutoSize = true;
         this.lbBotNewQuickFilter.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
         this.lbBotNewQuickFilter.Location = new System.Drawing.Point(68, 168);
         this.lbBotNewQuickFilter.Name = "lbBotNewQuickFilter";
         this.lbBotNewQuickFilter.Size = new System.Drawing.Size(98, 21);
         this.lbBotNewQuickFilter.TabIndex = 11;
         this.lbBotNewQuickFilter.Text = "quick filter:";
         // 
         // btnCreateBot
         // 
         this.btnCreateBot.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
         this.btnCreateBot.Location = new System.Drawing.Point(597, 412);
         this.btnCreateBot.Name = "btnCreateBot";
         this.btnCreateBot.Size = new System.Drawing.Size(137, 69);
         this.btnCreateBot.TabIndex = 13;
         this.btnCreateBot.Text = "Create Bot";
         this.btnCreateBot.UseVisualStyleBackColor = true;
         this.btnCreateBot.Click += new System.EventHandler(this.btnCreateBot_Click);
         // 
         // lboxBotQuickFilter
         // 
         this.lboxBotQuickFilter.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
         this.lboxBotQuickFilter.FormattingEnabled = true;
         this.lboxBotQuickFilter.ItemHeight = 17;
         this.lboxBotQuickFilter.Location = new System.Drawing.Point(58, 222);
         this.lboxBotQuickFilter.Name = "lboxBotQuickFilter";
         this.lboxBotQuickFilter.Size = new System.Drawing.Size(341, 259);
         this.lboxBotQuickFilter.TabIndex = 14;
         // 
         // BotInterval
         // 
         this.BotInterval.Location = new System.Drawing.Point(540, 236);
         this.BotInterval.Name = "BotInterval";
         this.BotInterval.Size = new System.Drawing.Size(105, 23);
         this.BotInterval.TabIndex = 17;
         // 
         // BotFullTime
         // 
         this.BotFullTime.Location = new System.Drawing.Point(540, 278);
         this.BotFullTime.Name = "BotFullTime";
         this.BotFullTime.Size = new System.Drawing.Size(105, 23);
         this.BotFullTime.TabIndex = 18;
         // 
         // lbBotInterval
         // 
         this.lbBotInterval.AutoSize = true;
         this.lbBotInterval.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
         this.lbBotInterval.Location = new System.Drawing.Point(466, 236);
         this.lbBotInterval.Name = "lbBotInterval";
         this.lbBotInterval.Size = new System.Drawing.Size(60, 17);
         this.lbBotInterval.TabIndex = 19;
         this.lbBotInterval.Text = "Interval:";
         // 
         // lbBotFullTime
         // 
         this.lbBotFullTime.AutoSize = true;
         this.lbBotFullTime.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
         this.lbBotFullTime.Location = new System.Drawing.Point(466, 278);
         this.lbBotFullTime.Name = "lbBotFullTime";
         this.lbBotFullTime.Size = new System.Drawing.Size(66, 17);
         this.lbBotFullTime.TabIndex = 20;
         this.lbBotFullTime.Text = "FullTime:";
         // 
         // lbBotHeading
         // 
         this.lbBotHeading.AutoSize = true;
         this.lbBotHeading.Font = new System.Drawing.Font("Segoe UI", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point);
         this.lbBotHeading.Location = new System.Drawing.Point(334, 26);
         this.lbBotHeading.Name = "lbBotHeading";
         this.lbBotHeading.Size = new System.Drawing.Size(125, 37);
         this.lbBotHeading.TabIndex = 21;
         this.lbBotHeading.Text = "Bot";
         // 
         // lbBotFT
         // 
         this.lbBotFT.AutoSize = true;
         this.lbBotFT.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
         this.lbBotFT.Location = new System.Drawing.Point(651, 278);
         this.lbBotFT.Name = "lbBotFT";
         this.lbBotFT.Size = new System.Drawing.Size(75, 17);
         this.lbBotFT.TabIndex = 22;
         this.lbBotFT.Text = "times used";
         // 
         // lbBotInt
         // 
         this.lbBotInt.AutoSize = true;
         this.lbBotInt.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
         this.lbBotInt.Location = new System.Drawing.Point(651, 236);
         this.lbBotInt.Name = "lbBotInt";
         this.lbBotInt.Size = new System.Drawing.Size(57, 17);
         this.lbBotInt.TabIndex = 23;
         this.lbBotInt.Text = "seconds";
         // 
         // btnStartBot
         // 
         this.btnStartBot.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
         this.btnStartBot.Location = new System.Drawing.Point(428, 412);
         this.btnStartBot.Name = "btnStartBot";
         this.btnStartBot.Size = new System.Drawing.Size(137, 69);
         this.btnStartBot.TabIndex = 24;
         this.btnStartBot.Text = "Start Bot";
         this.btnStartBot.UseVisualStyleBackColor = true;
         this.btnStartBot.Click += new System.EventHandler(this.btnStartBot_Click);
         // 
         // Form1
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(1049, 645);
         this.Controls.Add(this.panelBot);
         this.Controls.Add(this.lbProgress);
         this.Controls.Add(this.lbAllOffers);
         this.Controls.Add(this.lbNewOffers);
         this.Controls.Add(this.lbDeletedCount);
         this.Controls.Add(this.lbUpdatedCount);
         this.Controls.Add(this.cmbSelectPanel);
         this.Controls.Add(this.btnSelectPanel);
         this.Controls.Add(this.panelMain);
         this.MaximizeBox = false;
         this.Name = "Form1";
         this.Text = "Bazos bot";
         this.Load += new System.EventHandler(this.Form1_Load);
         this.panelMain.ResumeLayout(false);
         this.panelMain.PerformLayout();
         this.panelBot.ResumeLayout(false);
         this.panelBot.PerformLayout();
         ((System.ComponentModel.ISupportInitialize)(this.BotInterval)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.BotFullTime)).EndInit();
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
      private System.Windows.Forms.TextBox textBox9;
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
      private System.Windows.Forms.Panel panelBot;
      private System.Windows.Forms.ComboBox cmbBot;
      private System.Windows.Forms.TextBox tbCategoryURL;
      private System.Windows.Forms.TextBox tbBotQuickFilter;
      private System.Windows.Forms.TextBox tbBotName;
      private System.Windows.Forms.Button btnAddToQuickFilterBot;
      private System.Windows.Forms.ListBox lboxBotQuickFilter;
      private System.Windows.Forms.Button btnCreateBot;
      private System.Windows.Forms.Label lbBotNewQuickFilter;
      private System.Windows.Forms.Label lbCategoryUrl;
      private System.Windows.Forms.Label lbBotName;
      private System.Windows.Forms.ComboBox cmbQuickFilterBot;
      private System.Windows.Forms.ComboBox cmbCategoryUrlBot;
      private System.Windows.Forms.Label lbBotInt;
      private System.Windows.Forms.Label lbBotFT;
      private System.Windows.Forms.Label lbBotHeading;
      private System.Windows.Forms.Label lbBotFullTime;
      private System.Windows.Forms.Label lbBotInterval;
      private System.Windows.Forms.NumericUpDown BotFullTime;
      private System.Windows.Forms.NumericUpDown BotInterval;
      private System.Windows.Forms.Button btnStartBot;
   }
}

