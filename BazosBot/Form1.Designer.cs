
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
         this.cmbSelectQuickFilter = new System.Windows.Forms.ComboBox();
         this.lbLokace = new System.Windows.Forms.Label();
         this.btnCreateQuickFilter = new System.Windows.Forms.Button();
         this.cboxDownOnlyLast = new System.Windows.Forms.CheckBox();
         this.btnApplyQuickFilter = new System.Windows.Forms.Button();
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
         this.panelMain.SuspendLayout();
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
         this.panelMain.Controls.Add(this.cmbSelectQuickFilter);
         this.panelMain.Controls.Add(this.lbLokace);
         this.panelMain.Controls.Add(this.btnCreateQuickFilter);
         this.panelMain.Controls.Add(this.cboxDownOnlyLast);
         this.panelMain.Controls.Add(this.btnApplyQuickFilter);
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
         this.panelMain.Location = new System.Drawing.Point(21, 49);
         this.panelMain.Name = "panelMain";
         this.panelMain.Size = new System.Drawing.Size(889, 347);
         this.panelMain.TabIndex = 5;
         this.panelMain.Tag = "mainPanels";
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
         // btnApplyQuickFilter
         // 
         this.btnApplyQuickFilter.Location = new System.Drawing.Point(416, 32);
         this.btnApplyQuickFilter.Name = "btnApplyQuickFilter";
         this.btnApplyQuickFilter.Size = new System.Drawing.Size(69, 23);
         this.btnApplyQuickFilter.TabIndex = 13;
         this.btnApplyQuickFilter.Text = "apply";
         this.btnApplyQuickFilter.UseVisualStyleBackColor = true;
         this.btnApplyQuickFilter.Click += new System.EventHandler(this.btnApplyQuickFilter_Click);
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
         this.lbUpdatedCount.Location = new System.Drawing.Point(517, 12);
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
         this.lbNewOffers.Location = new System.Drawing.Point(398, 12);
         this.lbNewOffers.Name = "lbNewOffers";
         this.lbNewOffers.Size = new System.Drawing.Size(65, 15);
         this.lbNewOffers.TabIndex = 11;
         this.lbNewOffers.Text = "new offers:";
         // 
         // lbAllOffers
         // 
         this.lbAllOffers.AutoSize = true;
         this.lbAllOffers.Location = new System.Drawing.Point(275, 12);
         this.lbAllOffers.Name = "lbAllOffers";
         this.lbAllOffers.Size = new System.Drawing.Size(55, 15);
         this.lbAllOffers.TabIndex = 13;
         this.lbAllOffers.Text = "all offers:";
         // 
         // Form1
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(1049, 645);
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
      private System.Windows.Forms.Button btnApplyQuickFilter;
      private System.Windows.Forms.CheckBox cboxDownOnlyLast;
      private System.Windows.Forms.Label lbAllOffers;
      private System.Windows.Forms.Button btnCreateQuickFilter;
      private System.Windows.Forms.Label lbLokace;
      private System.Windows.Forms.ComboBox cmbSelectQuickFilter;
   }
}

