using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
//.net maui

namespace BazosBot
{
   public partial class Form1 : Form
   {
      string activePanel;
	   System.Windows.Forms.Timer timer; //showing progress timer
      System.Windows.Forms.Timer autoTimer; //autobot timer
      Stopwatch sw = new Stopwatch();

      public Form1()
      {
         InitializeComponent();
         PrepareUserInterface();
		   InitTimers();
      }

      private void Form1_Load(object sender, EventArgs e)
      {
         cmbSelectOffers.Items.AddRange(DB_Access.ListActualOffersCategoryURLInDB().ToArray());
         cmbSelectQuickFilter.Items.Add("none");
         cmbSelectQuickFilter.SelectedIndex = 0;
      }

      #region timers
      //showing:
      private void timer_tick(object s, EventArgs a)
      {
         double percent = 100 / (Download.fullCount / Download.count);
         percent = Math.Round(percent, 1);
         double dbPercent = DB_Access.offersCount > 0 && DB_Access.i > 0 ? 100 / (DB_Access.offersCount / DB_Access.i) : 0;
         double elapsedTime = Math.Round(sw.Elapsed.TotalSeconds, 0);
         dbPercent = Math.Round(dbPercent, 1);

         if (Download.downloadDone && !Download.isRunning)
         {
            switchtimer();
            Download.downloadDone = false;
            AddItemsToResultLbox(BazosOffers.ListBazosOffers); //result lisbox
            elapsedTime = sw.Elapsed.Milliseconds >= 50 ? elapsedTime + 1 : elapsedTime;
            //labels:
            lbAllOffers.Text = $"all offers: {BazosOffers.ListBazosOffers.Count}";
            lbNewOffers.Text = $"new offers: {DB_Access.newOffersList.Count}";
            lbUpdatedCount.Text = $"updated: {DB_Access.updatedList.Count}";
            lbDeletedCount.Text = $"deleted: {DB_Access.deletedList.Count}";
            lastSearched = true;
            cmbSelectQuickFilter.Items.AddRange(QuickFilter.ListActualQuickFiltersInDB(BazosOffers.actualCategoryURL).ToArray());
         }
         lbProgress.Text = !Download.downloadDone ? $"Download progress: {Download.count} / {Download.fullCount} - {percent}%\nTime elapsed: {elapsedTime} sec" : $"Download progress: {Download.count} / {Download.fullCount} - {percent}% - done!\nUpdating data to DB: {DB_Access.i} / {DB_Access.offersCount} - {dbPercent}% \nTime elapsed: {elapsedTime} sec";
      }

      private bool switchAutoBot = false; //autobot get full offers - start stopwatch after getted
		 private void switchtimer()
		 {
			if (!timer.Enabled)
			{
				timer.Start();
				lbProgress.Show();
            sw.Start();
			}
			else
			{
				timer.Stop();
            sw.Stop();
            sw.Reset();
            if (switchAutoBot)
            {
               switchAutoBot = false;
               AutoBot.LastAutoBot.sw.Start();
            }
            //lbProgress.Hide();
			}
		 }

      //autobot timer:
      //double elapsedSec = 0;
      private void auto_timer_tick(object s, EventArgs a)
      {
         foreach (AutoBot aBot in AutoBot.AutoBotList.ToList()) //not enqueued autobot - wait to interval
         {
            double elapsedSec = Math.Round(aBot.sw.Elapsed.TotalSeconds, 0);
            if (elapsedSec > aBot.interval || !aBot.sw.IsRunning)
            {
               AutoBot.AutoBotList.Remove(aBot);
               AutoBot.AutoBotQueue.Enqueue(aBot);
               aBot.sw.Reset();
            }
         }
         if (AutoBot.AutoBotQueue.Count > 0)
         {
            if (AutoBot.LastAutoBot == null) //first autobot run
            {
               AutoBot.LastAutoBot = AutoBot.AutoBotQueue.Dequeue();
               RunAutoBot();
            }
            else if (!AutoBot.LastAutoBot.isRunning) //after lastAutobot finished run
            {
               AutoBot.AutoBotList.Add(AutoBot.LastAutoBot);
               AutoBot.LastAutoBot = AutoBot.AutoBotQueue.Dequeue();
               RunAutoBot();
            }
         }
         //if (AutoBot.LastAutoBot.isRunning)
         //{
         //   elapsedSec = Math.Round(AutoBot.LastAutoBot.sw.Elapsed.TotalSeconds, 0);
         //}
      }

      /// <summary>
      /// 
      /// </summary>
      private void RunAutoBot()
      {
         bool getOnlyNewOffers = AutoBot.LastAutoBot.timesUsed < AutoBot.LastAutoBot.fullInterval;
         AutoBot.LastAutoBot.timesUsed = getOnlyNewOffers ? AutoBot.LastAutoBot.timesUsed : 0;
         AutoBot.LastAutoBot.isRunning = true;
         if (!getOnlyNewOffers)
         {
            switchAutoBot = true;
            switchtimer();
         }
         else
         {
            AutoBot.LastAutoBot.sw.Start();
         }
         Thread thread = new Thread(() => Download.DownloadAllFromCategory(AutoBot.LastAutoBot.category, getOnlyNewOffers, true));
         thread.Start();
      }

      //initialize timers:
      private void InitTimers()
      {
         timer = new System.Windows.Forms.Timer();
         timer.Tick += new EventHandler(timer_tick);
         timer.Interval = 20;
         autoTimer = new System.Windows.Forms.Timer();
         autoTimer.Tick += new EventHandler(timer_tick);
         autoTimer.Interval = 500;
      }

      #endregion

      #region PrepareUserInteface
      private void PrepareUserInterface()
      {
         activePanel = "main";
         resultLbox.HorizontalScrollbar = true;
         offerLbox.HorizontalScrollbar = true;
         PrepareComboboxes();     
      }

      private void PrepareComboboxes()
      {
         string[] cmbSelectOffersTypeString = new string[] { "all offers", "new offers", "updated", "deleted" };
         cmbSelectOffersType.Items.AddRange(cmbSelectOffersTypeString);
         cmbSelectOffersType.SelectedIndex = 0;

         foreach (Control cmb in Controls.OfType<ComboBox>())
         {
            if ((cmb as ComboBox).Items.Count > 0)
            {
               (cmb as ComboBox).SelectedIndex = 0;
            }
         }
         foreach (Control control in Controls.OfType<Panel>())
         {
            cmbSelectPanel.Items.Add(Settings.DictMainPanelsNameValue.FirstOrDefault(c => c.Value == control.Name).Key); //by value
            control.Size = Settings.defaultPanelSize;
            control.Location = Settings.defaultPanelLocation;
         }
         cmbSelectPanel.SelectedItem = "main panel";
     }

      private void btnSelectPanel_Click(object sender, EventArgs e)
      {   
         //ChangePanel(Settings.DictPanelNameValue[cmbSelectPanel.SelectedItem.ToString()]);
         //DB_Access.InsertNewOffers("test");
      }

      #endregion

      #region Main Panel
      /// <summary>
      /// Main method to get items from bazos.
      /// </summary>
      private void btnGetBazos_Click(object sender, EventArgs e)
      {
         if (!Download.isRunning)
         {
            ResetList(); //reset list of offers and result lisbox
            BazosOffers.actualCategoryURL = tbSearchUrl.Text != string.Empty ? tbSearchUrl.Text : cmbSelectOffers.Text; //actual category url id
            //Download.DownloadAllFromCategory(tbSearchUrl.Text, cboxDownOnlyLast.Checked); //await download
            Thread thread = new Thread(() => Download.DownloadAllFromCategory(tbSearchUrl.Text, cboxDownOnlyLast.Checked));
            thread.Start();
            switchtimer();
         }
      }


      /// <summary>
      /// resultLbox
      /// </summary>
      /// <param name="itemCount"></param>
      /// <param name="item"></param>
      private void AddItemToResultLbox(int itemCount, BazosOffers item)
      {
         resultLbox.Items.Add($"{itemCount}) {item.nadpis} for {item.cena} - {item.lokace} / {item.psc} - {item.datum} - {item.viewed} x - {item.url}");
      }

      private void AddItemsToResultLbox(List<BazosOffers> itemList, int itemCount = 1)
      {
         foreach (BazosOffers item in itemList.ToList())
         {
            resultLbox.Items.Add($"{itemCount}) {item.nadpis} for {item.cena} - {item.lokace} / {item.psc} - {item.datum} - {item.viewed} x - {item.url}");
            itemCount++;
         }
      }

      private void AddOffersToResultLbox(List<BazosOffers> offersList, string name = "")
      {
         int itemCount = 1;
         foreach (BazosOffers item in offersList.ToList())
         {
            resultLbox.Items.Add($"{itemCount}) {item.nadpis} for {item.cena} - {item.lokace} / {item.psc} - {item.datum} - {item.viewed} x - {item.url}");
            itemCount++;
         }
      }

      private void resultLbox_SelectedIndexChanged(object sender, EventArgs e)
      {
         offerLbox.Items.Clear();
         if (!Regex.IsMatch(cmbSelectOffersType.Text, "deleted", RegexOptions.IgnoreCase))
         {
            BazosOffers item = BazosOffers.ListBazosOffers.FirstOrDefault(of => of.nadpis == Regex.Replace(resultLbox.SelectedItem.ToString().Split(')', 2)[1], @"\sfor\s", ";").Split(";")[0].Trim());
            foreach (var prop in item.GetType().GetProperties())
            {
               offerLbox.Items.Add($"{prop.Name}: {prop.GetValue(item, null)}");
            }
         }
      }

      private void resultLbox_DoubleClick(object sender, EventArgs e)
      {
         BazosOffers item = BazosOffers.ListBazosOffers.FirstOrDefault(of => of.nadpis == Regex.Replace(resultLbox.SelectedItem.ToString().Split(')', 2)[1], @"\sfor\s", ";").Split(";")[0].Trim());
         string url = item.url;
         Process process = new Process();
         process.StartInfo = new ProcessStartInfo()
         {
            FileName = Environment.GetEnvironmentVariable("ProgramFiles") + @"\Google\Chrome\Application\chrome.exe",
            Arguments = url
         };
         process.Start();
      }


      /// <summary>
      /// offerLbox
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void offerLbox_DoubleClick(object sender, EventArgs e)
      {
         if (offerLbox.SelectedItem.ToString().Contains("url:"))
         {
            string url = offerLbox.SelectedItem.ToString().Replace("url:", string.Empty).Trim();
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo()
            {
               FileName = Environment.GetEnvironmentVariable("ProgramFiles") + @"\Google\Chrome\Application\chrome.exe",
               Arguments = url
            };
            process.Start();
         }
      }


      /// <summary>
      /// reset list
      /// </summary>
      private void ResetList()
      {
         BazosOffers.ListBazosOffers.Clear();
         resultLbox.Items.Clear();
      }


      /// <summary>
      /// 
      /// </summary>
      Dictionary<string, List<BazosOffers>> DictNameOffersList = new Dictionary<string, List<BazosOffers>>()
      {
         { "all offers" , BazosOffers.ListBazosOffers },
         { "new offers" , DB_Access.newOffersList },
         { "updated" , DB_Access.updatedList },
         { "deleted" , DB_Access.deletedList }
      };

      private void cmbSelectOffersType_SelectedIndexChanged(object sender, EventArgs e)
      {
         if (DB_Access.downloaded)
         {
            resultLbox.Items.Clear();
            //Dictionary<string, List<BazosOffers>> innerDictionary = DictNameOffersList;
            if (cmbSelectOffersType.Text != "updated")
            {
               AddOffersToResultLbox(DictNameOffersList[cmbSelectOffersType.Text], cmbSelectOffersType.Text);
            }
            else
            {
               AddOffersToResultLbox(DB_Access.updatedList, "updated");
            }
         }
      }

      string lastSelectedItem;
      private void cmbSelectOffers_SelectedIndexChanged(object sender, EventArgs e)
      {
         if (resultLbox.Items.Count == 0 || lastSelectedItem != cmbSelectOffers.Text && resultLbox.Items.Count > 0 && MessageBox.Show("Nahrát tento seznam věcí?", "Přemazat věci v listboxu?", MessageBoxButtons.YesNo) == DialogResult.Yes)
         {
            resultLbox.Items.Clear();
            BazosOffers.ListBazosOffers = DB_Access.ListActualOffersInDB(cmbSelectOffers.Text);
            AddOffersToResultLbox(BazosOffers.ListBazosOffers);
            lastSelectedItem = cmbSelectOffers.Text;
            cmbSelectQuickFilter.Items.Clear();
            cmbSelectQuickFilter.Items.Add("none");
            cmbSelectQuickFilter.Items.AddRange(QuickFilter.ListActualQuickFiltersInDB(cmbSelectOffers.Text).ToArray());
            lastSearched = false;
         }
      }


      /// <summary>
      /// Quick filter
      /// </summary>
      private void btnApplyQuickFilter_Click(object sender, EventArgs e)
      {
         QuickFilter.GetQuickFiltersFromTextbox(tbQuickFilter.Text);
         ApplyQuickFilterChanges();
      }

      private void cmbSelectQuickFilter_SelectedIndexChanged(object sender, EventArgs e)
      {
         //tbQuickFilter.Text = cmbSelectQuickFilter.SelectedText;
         //btnApplyQuickFilter.PerformClick();
         if (cmbSelectQuickFilter.SelectedItem.ToString() != "none")
         {
            QuickFilter.GetQuickFiltersFromTextbox(cmbSelectQuickFilter.SelectedItem.ToString());
            tbQuickFilter.Text = cmbSelectQuickFilter.SelectedItem.ToString();
            ApplyQuickFilterChanges();
         }
      }

      private void ApplyQuickFilterChanges()
      {
         resultLbox.Items.Clear();
         List<QuickFilter> qfList = QuickFilter.QuickFilterList;
         if (qfList.Count > 0)
         {
            int itemCount = 1;
            //bool itemPlus = false;
            foreach (BazosOffers item in BazosOffers.ListBazosOffers)
            {
               int cena = 0;
               if (qfList.Any(qf => item.nadpis.Contains(qf.nadpis, StringComparison.OrdinalIgnoreCase))) //nadpis is matched in quickfilter
               {
                  QuickFilter quickfilter = qfList.FirstOrDefault(qf => item.nadpis.Contains(qf.nadpis, StringComparison.OrdinalIgnoreCase));
                  int.TryParse(item.cena, out cena);
                  if ((!quickfilter.fullNadpisName || item.nadpis.Split(' ').Contains(quickfilter.nadpis)) && (quickfilter.maxCena == 0 || (cena > 0 && cena <= quickfilter.maxCena) || cena == 0) && !QuickFilter.Blacklist.Any(nadpis => item.nadpis.Contains(nadpis, StringComparison.OrdinalIgnoreCase) && (tbLokalita.Text == string.Empty || item.lokace.Contains(tbLokalita.Text)))) //test if item is matched to max cena
                  {
                     AddItemToResultLbox(itemCount, item);
                     itemCount++; //itemPlus = true;
                  }
               }
            }
         }
         else
         {
            AddItemsToResultLbox(BazosOffers.ListBazosOffers);
         }
      }

      bool lastSearched = false;
      private void btnCreateQuickFilter_Click(object sender, EventArgs e)
      {
         if (!string.IsNullOrEmpty(tbQuickFilter.Text) && !string.IsNullOrEmpty(tbSearchUrl.Text) || !string.IsNullOrEmpty(cmbSelectOffers.Text))
         {
            //QuickFilter.GetQuickFiltersFromTextbox(tbQuickFilter.Text);
            string[] filterSplit = tbQuickFilter.Text.Split(";");
            QuickFilter.Name = filterSplit[0].Contains(":") ? filterSplit[0].Split(":")[0] : string.Empty;
            string categoryUrl = lastSearched ? tbSearchUrl.Text : cmbSelectOffers.Text;
            QuickFilter.SaveQuickFilterToDB(tbQuickFilter.Text, categoryUrl);
            cmbSelectQuickFilter.Items.Add(tbQuickFilter.Text);
            cmbSelectQuickFilter.SelectedItem = tbQuickFilter.Text;
         }      
      }

      #endregion

	   #region Auto-bot Panel
	  
	  
	  #endregion

      #region Control methods
      private void cmbSelectPanel_SelectedIndexChanged(object sender, EventArgs e)
      {
         string key = cmbSelectPanel.SelectedItem.ToString();
         string controlName = Settings.DictMainPanelsNameValue[key];
         foreach (Control control in Controls.OfType<Panel>())
         {
            control.Hide();
         }
         Controls[controlName].Show();
      }

      /// <summary>
      /// Changing panel to selected panel and hide other panels.
      /// </summary>
      /// <param name="panel">selected panel to show</param>
      private void ChangePanel(string panelName)
      {
         Control panel = this.Controls[panelName];
         if (activePanel != panel.Name)
         {
            HideControls<Panel>(); //hide alls panels
            panel.Show();
         }
      }

      /// <summary>
      /// Hide all controls of selected type.
      /// </summary>
      /// <typeparam name="control">type of controls to hide</typeparam>
      private void HideControls<control>()
      {
         foreach (Control c in Controls)
         {
            if (c is control)
            { c.Hide(); }
         }
      }

      private bool ListBoxesNotClear()
      {
         //foreach (GroupBox g in Controls.OfType<GroupBox>())
         //{
         foreach (TextBox textbox in Controls.OfType<TextBox>())//.Where(p => p.Tag == "Filter"))
         {
            if (textbox.Text != string.Empty)
            {
               return true;
            }
         }
         //}
         return false;
      }

      //private void List_RightClick(object sender, MouseEventArgs e)
      //{

      //   if (e.Button == MouseButtons.Right)
      //   {
      //      //int index = lboxFilters.IndexFromPoint(e.Location);
      //      if (index != ListBox.NoMatches)
      //      {
      //         // lboxFilters.Ite[index];
      //      }
      //   }
      //}

      /// <summary>
      /// kill everything on form is closed
      /// </summary>
      protected override void OnFormClosing(FormClosingEventArgs e)
      {
         base.OnFormClosing(e);
         Environment.Exit(0);
      }

      #endregion

   }
}