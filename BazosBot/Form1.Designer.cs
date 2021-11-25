
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
         this.panelAutoBot = new System.Windows.Forms.Panel();
         this.cmbAutobot = new System.Windows.Forms.ComboBox();
         this.tbCategoryURL = new System.Windows.Forms.TextBox();
         this.tbAutobotQuickFilter = new System.Windows.Forms.TextBox();
         this.tbAutobotName = new System.Windows.Forms.TextBox();
         this.btnAddToQuickFilterAutobot = new System.Windows.Forms.Button();
         this.cmbCategoryUrlAutobot = new System.Windows.Forms.ComboBox();
         this.cmbQuickFilterAutobot = new System.Windows.Forms.ComboBox();
         this.lbAutobotName = new System.Windows.Forms.Label();
         this.lbCategoryUrl = new System.Windows.Forms.Label();
         this.lbAutobotNewQuickFilter = new System.Windows.Forms.Label();
         this.btnCreateAutobot = new System.Windows.Forms.Button();
         this.lboxAutobotQuickFilter = new System.Windows.Forms.ListBox();
         this.AutobotInterval = new System.Windows.Forms.NumericUpDown();
         this.AutoBotFullTime = new System.Windows.Forms.NumericUpDown();
         this.lbAutobotInterval = new System.Windows.Forms.Label();
         this.lbAutobotFullTime = new System.Windows.Forms.Label();
         this.lbAutobotHeading = new System.Windows.Forms.Label();
         this.lbAutobotFT = new System.Windows.Forms.Label();
         this.lbAutobotInt = new System.Windows.Forms.Label();
         this.btnStartAutoBot = new System.Windows.Forms.Button();
         this.panelMain.SuspendLayout();
         this.panelAutoBot.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.AutobotInterval)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.AutoBotFullTime)).BeginInit();
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
         // panelAutoBot
         // 
         this.panelAutoBot.Controls.Add(this.btnStartAutoBot);
         this.panelAutoBot.Controls.Add(this.lbAutobotInt);
         this.panelAutoBot.Controls.Add(this.lbAutobotFT);
         this.panelAutoBot.Controls.Add(this.lbAutobotHeading);
         this.panelAutoBot.Controls.Add(this.lbAutobotFullTime);
         this.panelAutoBot.Controls.Add(this.lbAutobotInterval);
         this.panelAutoBot.Controls.Add(this.AutoBotFullTime);
         this.panelAutoBot.Controls.Add(this.AutobotInterval);
         this.panelAutoBot.Controls.Add(this.lboxAutobotQuickFilter);
         this.panelAutoBot.Controls.Add(this.btnCreateAutobot);
         this.panelAutoBot.Controls.Add(this.lbAutobotNewQuickFilter);
         this.panelAutoBot.Controls.Add(this.lbCategoryUrl);
         this.panelAutoBot.Controls.Add(this.lbAutobotName);
         this.panelAutoBot.Controls.Add(this.cmbQuickFilterAutobot);
         this.panelAutoBot.Controls.Add(this.cmbCategoryUrlAutobot);
         this.panelAutoBot.Controls.Add(this.cmbAutobot);
         this.panelAutoBot.Controls.Add(this.tbCategoryURL);
         this.panelAutoBot.Controls.Add(this.tbAutobotQuickFilter);
         this.panelAutoBot.Controls.Add(this.tbAutobotName);
         this.panelAutoBot.Controls.Add(this.btnAddToQuickFilterAutobot);
         this.panelAutoBot.Location = new System.Drawing.Point(21, 48);
         this.panelAutoBot.Name = "panelAutoBot";
         this.panelAutoBot.Size = new System.Drawing.Size(952, 585);
         this.panelAutoBot.TabIndex = 15;
         // 
         // cmbAutobot
         // 
         this.cmbAutobot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.cmbAutobot.FormattingEnabled = true;
         this.cmbAutobot.Location = new System.Drawing.Point(416, 89);
         this.cmbAutobot.Name = "cmbAutobot";
         this.cmbAutobot.Size = new System.Drawing.Size(318, 23);
         this.cmbAutobot.TabIndex = 6;
         this.cmbAutobot.SelectedIndexChanged += new System.EventHandler(this.cmbAutobot_SelectedIndexChanged);
         // 
         // tbCategoryURL
         // 
         this.tbCategoryURL.Location = new System.Drawing.Point(172, 130);
         this.tbCategoryURL.Name = "tbCategoryURL";
         this.tbCategoryURL.Size = new System.Drawing.Size(220, 23);
         this.tbCategoryURL.TabIndex = 5;
         // 
         // tbAutobotQuickFilter
         // 
         this.tbAutobotQuickFilter.Location = new System.Drawing.Point(172, 170);
         this.tbAutobotQuickFilter.Name = "tbAutobotQuickFilter";
         this.tbAutobotQuickFilter.Size = new System.Drawing.Size(220, 23);
         this.tbAutobotQuickFilter.TabIndex = 4;
         // 
         // tbAutobotName
         // 
         this.tbAutobotName.Location = new System.Drawing.Point(172, 89);
         this.tbAutobotName.Name = "tbAutobotName";
         this.tbAutobotName.Size = new System.Drawing.Size(220, 23);
         this.tbAutobotName.TabIndex = 2;
         // 
         // btnAddToQuickFilterAutobot
         // 
         this.btnAddToQuickFilterAutobot.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
         this.btnAddToQuickFilterAutobot.Location = new System.Drawing.Point(416, 170);
         this.btnAddToQuickFilterAutobot.Name = "btnAddToQuickFilterAutobot";
         this.btnAddToQuickFilterAutobot.Size = new System.Drawing.Size(75, 23);
         this.btnAddToQuickFilterAutobot.TabIndex = 1;
         this.btnAddToQuickFilterAutobot.Text = "add";
         this.btnAddToQuickFilterAutobot.UseVisualStyleBackColor = true;
         this.btnAddToQuickFilterAutobot.Click += new System.EventHandler(this.btnAddToQuickFilterAutobot_Click);
         // 
         // cmbCategoryUrlAutobot
         // 
         this.cmbCategoryUrlAutobot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.cmbCategoryUrlAutobot.FormattingEnabled = true;
         this.cmbCategoryUrlAutobot.Location = new System.Drawing.Point(416, 132);
         this.cmbCategoryUrlAutobot.Name = "cmbCategoryUrlAutobot";
         this.cmbCategoryUrlAutobot.Size = new System.Drawing.Size(318, 23);
         this.cmbCategoryUrlAutobot.TabIndex = 7;
         this.cmbCategoryUrlAutobot.SelectedIndexChanged += new System.EventHandler(this.cmbCategoryUrlAutobot_SelectedIndexChanged);
         // 
         // cmbQuickFilterAutobot
         // 
         this.cmbQuickFilterAutobot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.cmbQuickFilterAutobot.FormattingEnabled = true;
         this.cmbQuickFilterAutobot.Location = new System.Drawing.Point(517, 170);
         this.cmbQuickFilterAutobot.Name = "cmbQuickFilterAutobot";
         this.cmbQuickFilterAutobot.Size = new System.Drawing.Size(217, 23);
         this.cmbQuickFilterAutobot.TabIndex = 8;
         this.cmbQuickFilterAutobot.SelectedIndexChanged += new System.EventHandler(this.cmbQuickFilterAutobot_SelectedIndexChanged);
         // 
         // lbAutobotName
         // 
         this.lbAutobotName.AutoSize = true;
         this.lbAutobotName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
         this.lbAutobotName.Location = new System.Drawing.Point(24, 87);
         this.lbAutobotName.Name = "lbAutobotName";
         this.lbAutobotName.Size = new System.Drawing.Size(142, 21);
         this.lbAutobotName.TabIndex = 9;
         this.lbAutobotName.Text = "name of autobot:";
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
         // lbAutobotNewQuickFilter
         // 
         this.lbAutobotNewQuickFilter.AutoSize = true;
         this.lbAutobotNewQuickFilter.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
         this.lbAutobotNewQuickFilter.Location = new System.Drawing.Point(68, 168);
         this.lbAutobotNewQuickFilter.Name = "lbAutobotNewQuickFilter";
         this.lbAutobotNewQuickFilter.Size = new System.Drawing.Size(98, 21);
         this.lbAutobotNewQuickFilter.TabIndex = 11;
         this.lbAutobotNewQuickFilter.Text = "quick filter:";
         // 
         // btnCreateAutobot
         // 
         this.btnCreateAutobot.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
         this.btnCreateAutobot.Location = new System.Drawing.Point(597, 412);
         this.btnCreateAutobot.Name = "btnCreateAutobot";
         this.btnCreateAutobot.Size = new System.Drawing.Size(137, 69);
         this.btnCreateAutobot.TabIndex = 13;
         this.btnCreateAutobot.Text = "Create Autobot";
         this.btnCreateAutobot.UseVisualStyleBackColor = true;
         this.btnCreateAutobot.Click += new System.EventHandler(this.btnCreateAutobot_Click);
         // 
         // lboxAutobotQuickFilter
         // 
         this.lboxAutobotQuickFilter.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
         this.lboxAutobotQuickFilter.FormattingEnabled = true;
         this.lboxAutobotQuickFilter.ItemHeight = 17;
         this.lboxAutobotQuickFilter.Location = new System.Drawing.Point(58, 222);
         this.lboxAutobotQuickFilter.Name = "lboxAutobotQuickFilter";
         this.lboxAutobotQuickFilter.Size = new System.Drawing.Size(341, 259);
         this.lboxAutobotQuickFilter.TabIndex = 14;
         // 
         // AutobotInterval
         // 
         this.AutobotInterval.Location = new System.Drawing.Point(540, 236);
         this.AutobotInterval.Name = "AutobotInterval";
         this.AutobotInterval.Size = new System.Drawing.Size(105, 23);
         this.AutobotInterval.TabIndex = 17;
         // 
         // AutoBotFullTime
         // 
         this.AutoBotFullTime.Location = new System.Drawing.Point(540, 278);
         this.AutoBotFullTime.Name = "AutoBotFullTime";
         this.AutoBotFullTime.Size = new System.Drawing.Size(105, 23);
         this.AutoBotFullTime.TabIndex = 18;
         // 
         // lbAutobotInterval
         // 
         this.lbAutobotInterval.AutoSize = true;
         this.lbAutobotInterval.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
         this.lbAutobotInterval.Location = new System.Drawing.Point(466, 236);
         this.lbAutobotInterval.Name = "lbAutobotInterval";
         this.lbAutobotInterval.Size = new System.Drawing.Size(60, 17);
         this.lbAutobotInterval.TabIndex = 19;
         this.lbAutobotInterval.Text = "Interval:";
         // 
         // lbAutobotFullTime
         // 
         this.lbAutobotFullTime.AutoSize = true;
         this.lbAutobotFullTime.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
         this.lbAutobotFullTime.Location = new System.Drawing.Point(466, 278);
         this.lbAutobotFullTime.Name = "lbAutobotFullTime";
         this.lbAutobotFullTime.Size = new System.Drawing.Size(66, 17);
         this.lbAutobotFullTime.TabIndex = 20;
         this.lbAutobotFullTime.Text = "FullTime:";
         // 
         // lbAutobotHeading
         // 
         this.lbAutobotHeading.AutoSize = true;
         this.lbAutobotHeading.Font = new System.Drawing.Font("Segoe UI", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point);
         this.lbAutobotHeading.Location = new System.Drawing.Point(334, 26);
         this.lbAutobotHeading.Name = "lbAutobotHeading";
         this.lbAutobotHeading.Size = new System.Drawing.Size(125, 37);
         this.lbAutobotHeading.TabIndex = 21;
         this.lbAutobotHeading.Text = "AutoBot";
         // 
         // lbAutobotFT
         // 
         this.lbAutobotFT.AutoSize = true;
         this.lbAutobotFT.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
         this.lbAutobotFT.Location = new System.Drawing.Point(651, 278);
         this.lbAutobotFT.Name = "lbAutobotFT";
         this.lbAutobotFT.Size = new System.Drawing.Size(75, 17);
         this.lbAutobotFT.TabIndex = 22;
         this.lbAutobotFT.Text = "times used";
         // 
         // lbAutobotInt
         // 
         this.lbAutobotInt.AutoSize = true;
         this.lbAutobotInt.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
         this.lbAutobotInt.Location = new System.Drawing.Point(651, 236);
         this.lbAutobotInt.Name = "lbAutobotInt";
         this.lbAutobotInt.Size = new System.Drawing.Size(57, 17);
         this.lbAutobotInt.TabIndex = 23;
         this.lbAutobotInt.Text = "seconds";
         // 
         // btnStartAutoBot
         // 
         this.btnStartAutoBot.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
         this.btnStartAutoBot.Location = new System.Drawing.Point(428, 412);
         this.btnStartAutoBot.Name = "btnStartAutoBot";
         this.btnStartAutoBot.Size = new System.Drawing.Size(137, 69);
         this.btnStartAutoBot.TabIndex = 24;
         this.btnStartAutoBot.Text = "Start Autobot";
         this.btnStartAutoBot.UseVisualStyleBackColor = true;
         this.btnStartAutoBot.Click += new System.EventHandler(this.btnStartAutoBot_Click);
         // 
         // Form1
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(1049, 645);
         this.Controls.Add(this.panelAutoBot);
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
         this.panelAutoBot.ResumeLayout(false);
         this.panelAutoBot.PerformLayout();
         ((System.ComponentModel.ISupportInitialize)(this.AutobotInterval)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.AutoBotFullTime)).EndInit();
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
      private System.Windows.Forms.Panel panelAutoBot;
      private System.Windows.Forms.ComboBox cmbAutobot;
      private System.Windows.Forms.TextBox tbCategoryURL;
      private System.Windows.Forms.TextBox tbAutobotQuickFilter;
      private System.Windows.Forms.TextBox tbAutobotName;
      private System.Windows.Forms.Button btnAddToQuickFilterAutobot;
      private System.Windows.Forms.ListBox lboxAutobotQuickFilter;
      private System.Windows.Forms.Button btnCreateAutobot;
      private System.Windows.Forms.Label lbAutobotNewQuickFilter;
      private System.Windows.Forms.Label lbCategoryUrl;
      private System.Windows.Forms.Label lbAutobotName;
      private System.Windows.Forms.ComboBox cmbQuickFilterAutobot;
      private System.Windows.Forms.ComboBox cmbCategoryUrlAutobot;
      private System.Windows.Forms.Label lbAutobotInt;
      private System.Windows.Forms.Label lbAutobotFT;
      private System.Windows.Forms.Label lbAutobotHeading;
      private System.Windows.Forms.Label lbAutobotFullTime;
      private System.Windows.Forms.Label lbAutobotInterval;
      private System.Windows.Forms.NumericUpDown AutoBotFullTime;
      private System.Windows.Forms.NumericUpDown AutobotInterval;
      private System.Windows.Forms.Button btnStartAutoBot;
   }
}

