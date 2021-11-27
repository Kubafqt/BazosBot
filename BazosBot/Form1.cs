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
//corona filter program - respirator
//idea - sort by - price, name (select order to sort)

namespace BazosBot
{
   public partial class Form1 : Form
   {
      string activePanel;
	   System.Windows.Forms.Timer timer; //showing progress timer
      System.Windows.Forms.Timer autoTimer; //Bot timer
      Stopwatch sw = new Stopwatch();
      Stopwatch waitingSw = new Stopwatch();

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
         cmbBot.Items.AddRange(AutoBot.GetBotNamesFromDB().ToArray());
      }

      #region timers
      //showing:
      private void timer_tick(object s, EventArgs a)
      {
         double percent = 0;
         double dbPercent = 0;
         DB_Access.notUpdateViewedAndLastChecked = cboxNotUpdateViewedAndLastChecked.Checked;
         if (!Download.getOnlyNewOffers)
         {
            percent = Math.Round(100 / (Download.fullCount / Download.count), 1);
            dbPercent = DB_Access.offersCount > 0 && DB_Access.i > 0 ? 100 / (DB_Access.offersCount / DB_Access.i) : 0;
            dbPercent = Math.Round(dbPercent, 1);            
         }
         double elapsedTime = Math.Round(sw.Elapsed.TotalSeconds, 0);
         if (Download.downloadDone && !Download.isRunning) //finished routine
         {
            switchtimer();
            Download.downloadDone = false;
            AddOffersToResultLbox(BazosOffers.ListBazosOffers); //result lisbox
            elapsedTime = sw.Elapsed.Milliseconds >= 50 ? elapsedTime + 1 : elapsedTime;
            //labels
            string allOffers = Download.getOnlyNewOffers ? $"{Download.fullCount}" : $"{BazosOffers.ListBazosOffers.Count}";
            string newOffers = Download.getOnlyNewOffers ? $"{DB_Access.newOffersList.Count}" : $"{DB_Access.newOffersList.Count}";
            string updatedOffers = Download.getOnlyNewOffers ? "not found" : $"{DB_Access.updatedList.Count}";
            string deletedOffers = Download.getOnlyNewOffers ? "not found" : $"{DB_Access.deletedList.Count}";
            lbAllOffers.Text = $"all offers: {allOffers}";
            lbNewOffers.Text = $"new offers: {newOffers}";
            lbUpdatedCount.Text = $"updated: {updatedOffers}";
            lbDeletedCount.Text = $"deleted: {deletedOffers}";
         }
         lbProgress.Text = !Download.getOnlyNewOffers ? 
            !Download.downloadDone ? $"Download progress: {Download.count} / {Download.fullCount} - {percent}%\nTime elapsed: {elapsedTime} sec." : $"Download progress: {Download.count} / {Download.fullCount} - {percent}% - done!\nUpdating data to DB: {DB_Access.i} / {DB_Access.offersCount} - {dbPercent}% \nTime elapsed: {elapsedTime} sec." : 
            !Download.downloadDone ? $"download: in progress\nTime elapsed: {elapsedTime} sec." : $"download: done!\nTime elapsed: {elapsedTime} sec.";
         lbProgress.Text = !Download.waiting ? lbProgress.Text : lbProgress.Text + $"\nWaiting to not overload the server - {Math.Round(waitingSw.Elapsed.TotalSeconds, 2)} sec.";
         //report waiting time:
         if (!Download.waiting && waitingSw.IsRunning)
         {
            waitingSw.Stop();
            waitingSw.Reset();
         }
         else
         {
            waitingSw.Start();
         }
      }

      /// <summary>
      /// timer switch
      /// </summary> 
     private bool switchBot = false; //Bot get full offers - start stopwatch after getted
      private void switchtimer(bool exeption = false)
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
            if (switchBot)
            {
               switchBot = false;
               AutoBot.LastBot.sw.Start();
            }
            if (exeption)
            {
               lbProgress.Text = string.Empty;
               lbProgress.Hide();
            }
			}
		 }

      //Bot timer:
      private void auto_timer_tick(object s, EventArgs a)
      {
         foreach (AutoBot aBot in AutoBot.BotList.ToList()) //not enqueued Bot - wait to interval
         {
            double elapsedSec = Math.Round(aBot.sw.Elapsed.TotalSeconds, 0);
            if (elapsedSec > aBot.interval || !aBot.sw.IsRunning)
            {
               AutoBot.BotList.Remove(aBot);
               AutoBot.BotQueue.Enqueue(aBot);
               aBot.sw.Reset();
            }
         }
         if (AutoBot.BotQueue.Count > 0) //some Bot is ready in queue
         {
            if (AutoBot.LastBot == null) //first Bot run
            {
               AutoBot.LastBot = AutoBot.BotQueue.Dequeue();
               RunBot();
            }
            else if (!AutoBot.LastBot.isRunning) //after lastBot finished run
            {
               AutoBot.BotList.Add(AutoBot.LastBot);
               AutoBot.LastBot = AutoBot.BotQueue.Dequeue();
               RunBot();
            }
         }
      }

      /// <summary>
      /// 
      /// </summary>
      private void RunBot()
      {
         bool getOnlyNewOffers = AutoBot.LastBot.fullInterval == 0 || AutoBot.LastBot.timesUsed < AutoBot.LastBot.fullInterval;
         AutoBot.LastBot.timesUsed = getOnlyNewOffers ? AutoBot.LastBot.timesUsed : 0;
         AutoBot.LastBot.isRunning = true;
         if (getOnlyNewOffers)
         {
            AutoBot.LastBot.sw.Start();        
         }
         else
         {
            switchBot = true;
            switchtimer();
         }
         Thread thread = new Thread(() => Download.DownloadAllFromCategory(AutoBot.LastBot.category, getOnlyNewOffers, true));
         thread.Start();
      }

      /// <summary>
      /// initialize timers
      /// </summary>
      private void InitTimers()
      {
         timer = new System.Windows.Forms.Timer();
         timer.Tick += new EventHandler(timer_tick);
         timer.Interval = 20;
         autoTimer = new System.Windows.Forms.Timer();
         autoTimer.Tick += new EventHandler(auto_timer_tick);
         autoTimer.Interval = 500;
      }

      #endregion

      #region Main Panel
      /// <summary>
      /// Main method to get items from bazos.
      /// </summary>
      private void btnGetBazos_Click(object sender, EventArgs e)
      {
         try
         {
            if (!Download.isRunning)
            {
               cmbSelectOffersType.SelectedIndex = 0;
               ResetList(); //reset list of offers and result lisbox
               BazosOffers.actualCategoryURL = tbSearchUrl.Text != string.Empty ? tbSearchUrl.Text : cmbSelectOffers.Text; //actual category url id
               DB_Access.notUpdateViewedAndLastChecked = cboxNotUpdateViewedAndLastChecked.Checked;
               Thread thread = new Thread(() => Download.DownloadAllFromCategory(BazosOffers.actualCategoryURL, cboxDownOnlyLast.Checked));
               //Download.DownloadAllFromCategory(tbSearchUrl.Text, cboxDownOnlyLast.Checked); //await download
               thread.Start();
               ChangeCmbSelectQuickFilter(BazosOffers.actualCategoryURL);
               switchtimer();
            }
         }
         catch (Exception exeption)
         {
            switchtimer();
            MessageBox.Show($"An error occured when trying to get data from bazos - {exeption.GetType()}");
         }
      }

      #region results lbox
      private void AddItemToResultLbox(int itemCount, BazosOffers item)
      {
         resultLbox.Items.Add($"{itemCount}) {item.nadpis} for {item.cena} - {item.lokace} / {item.psc} - {item.datum} - {item.viewed} x - {item.url}");
      }

      private void AddOffersToResultLbox(List<BazosOffers> itemList, int itemCount = 1)
      {
         foreach (BazosOffers item in itemList.ToList())
         {
            resultLbox.Items.Add($"{itemCount}) {item.nadpis} for {item.cena} - {item.lokace} / {item.psc} - {item.datum} - {item.viewed} x - {item.url}");
            itemCount++;
         }
      }

      //private void AddOffersToResultLbox(List<BazosOffers> offersList, string name = "")
      //{
      //   int itemCount = 1;
      //   foreach (BazosOffers item in offersList.ToList())
      //   {
      //      resultLbox.Items.Add($"{itemCount}) {item.nadpis} for {item.cena} - {item.lokace} / {item.psc} - {item.datum} - {item.viewed} x - {item.url}");
      //      itemCount++;
      //   }
      //}

      private void resultLbox_SelectedIndexChanged(object sender, EventArgs e)
      {
         offerLbox.Items.Clear();
         try
         {
            string? result = resultLbox.SelectedItem.ToString();
            if (!string.IsNullOrEmpty(result))
            {
               List<BazosOffers> actualList = !Regex.IsMatch(cmbSelectOffersType.Text, "deleted", RegexOptions.IgnoreCase) ? BazosOffers.ListBazosOffers : DB_Access.deletedList;
               string nadpis = Regex.Replace(resultLbox.SelectedItem.ToString().Split(')', 2)[1], @"\sfor\s", ";").Split(";")[0].Trim();
               if (actualList.Any(of => of.nadpis == nadpis))
               {
                  BazosOffers item = actualList.FirstOrDefault(of => of.nadpis == nadpis);
                  foreach (var prop in item.GetType().GetProperties())
                  {
                     if (!string.IsNullOrEmpty(prop.GetValue(item, null).ToString()))
                     {
                        offerLbox.Items.Add($"{prop.Name}: {prop.GetValue(item, null)}");
                     }
                  }
                  if (!Regex.IsMatch(cmbSelectOffersType.Text, "updated", RegexOptions.IgnoreCase))
                  {
                     MessageBox.Show(item.changed);
                  }
               }
            }
         }
         catch (Exception exception)
         {
            MessageBox.Show(exception.ToString());
         }
      }

      private void resultLbox_DoubleClick(object sender, EventArgs e)
      {
         List<BazosOffers> actualList = !Regex.IsMatch(cmbSelectOffersType.Text, "deleted", RegexOptions.IgnoreCase) ? BazosOffers.ListBazosOffers : DB_Access.deletedList;
         BazosOffers item = actualList.FirstOrDefault(of => of.nadpis == Regex.Replace(resultLbox.SelectedItem.ToString().Split(')', 2)[1], @"\sfor\s", ";").Split(";")[0].Trim());
         string url = item.url;
         Process process = new Process();
         process.StartInfo = new ProcessStartInfo()
         {
            FileName = Environment.GetEnvironmentVariable("ProgramFiles") + @"\Google\Chrome\Application\chrome.exe",
            Arguments = url
         };
         process.Start();
      }

      #endregion

      #region offers control
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
      /// bugged !
      /// </summary>
      //Dictionary<string, List<BazosOffers>> DictNameOffersList = new Dictionary<string, List<BazosOffers>>()
      //{
      //   { "all offers" , BazosOffers.ListBazosOffers },
      //   { "new offers" , DB_Access.newOffersList },
      //   { "updated" , DB_Access.updatedList },
      //   { "deleted" , DB_Access.deletedList }
      //};

      private void cmbSelectOffersType_SelectedIndexChanged(object sender, EventArgs e)
      {
         if (DB_Access.downloaded)
         {
            resultLbox.Items.Clear();
            switch (cmbSelectOffersType.Text)
            {
               case "all offers":
                  {
                     AddOffersToResultLbox(BazosOffers.ListBazosOffers);
                     break;
                  }
               case "new offers":
                  {
                     AddOffersToResultLbox(DB_Access.newOffersList);
                     break;
                  }
               case "updated":
                  {
                     AddOffersToResultLbox(DB_Access.updatedList);
                     break;
                  }
               case "deleted":
                  {
                     AddOffersToResultLbox(DB_Access.deletedList);
                     break;
                  }
               default:
                  break;
            }
            //Dictionary<string, List<BazosOffers>> innerDictionary = DictNameOffersList;
            //if (cmbSelectOffersType.Text != "updated")
            //{
            //   AddOffersToResultLbox(DictNameOffersList[cmbSelectOffersType.Text], cmbSelectOffersType.Text);
            //}
            //else
            //{
            //   AddOffersToResultLbox(DB_Access.updatedList, "updated");
            //}
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
            changedCategory = true;
            ChangeCmbSelectQuickFilter(cmbSelectOffers.Text);
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

      #endregion

      #region Quick filter
      private void btnApplyQuickFilter_Click(object sender, EventArgs e)
      {
         QuickFilter.GetQuickFiltersFromTextbox(tbQuickFilter.Text);
         ApplyQuickFilterChanges();
      }

      private void btnCreateQuickFilter_Click(object sender, EventArgs e)
      {
         if (!string.IsNullOrEmpty(tbQuickFilter.Text) && !string.IsNullOrEmpty(tbSearchUrl.Text) || !string.IsNullOrEmpty(cmbSelectOffers.Text))
         {
            //QuickFilter.GetQuickFiltersFromTextbox(tbQuickFilter.Text);
            string[] filterSplit = tbQuickFilter.Text.Split(";");
            QuickFilter.Name = filterSplit[0].Contains(":") ? filterSplit[0].Split(":")[0] : string.Empty;
            string categoryUrl = tbSearchUrl.Text != string.Empty ? tbSearchUrl.Text : cmbSelectOffers.Text;
            QuickFilter.SaveQuickFilterToDB(tbQuickFilter.Text, categoryUrl);
            if (btnCreateQuickFilter.Text != "edit") //create (button name on tbQuickFilter textchanged event)
            {
               cmbSelectQuickFilter.Items.Add(tbQuickFilter.Text);
               cmbSelectQuickFilter.SelectedItem = tbQuickFilter.Text;
            }
            else //edit
            {
               cmbSelectQuickFilter.Items[cmbSelectQuickFilter.SelectedIndex] = tbQuickFilter.Text;
            }
         }
      }

      bool changedCategory = false; //not apply quick filter changes when changed category
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
         else if (!changedCategory)
         {
            QuickFilter.QuickFilterList.Clear();
            tbQuickFilter.Clear();
            ApplyQuickFilterChanges();
         }
         else
         {
            changedCategory = false;
         }
      }

      /// <summary>
      /// 
      /// </summary>
      private void ChangeCmbSelectQuickFilter(string actualCategoryUrl)
      {
         cmbSelectQuickFilter.Items.Clear();
         cmbSelectQuickFilter.Items.Add("none");
         cmbSelectQuickFilter.Items.AddRange(QuickFilter.ListActualQuickFiltersInDB(actualCategoryUrl).ToArray());
         cmbSelectQuickFilter.SelectedIndex = 0;
      }

      private void cmbSelectQuickFilter_KeyDown(object sender, KeyEventArgs e)
      {
         if (e.KeyCode == Keys.Delete && cmbSelectQuickFilter.Text != "none" && MessageBox.Show("Opravdu smazat quick filter?", "smazat quickfilter", MessageBoxButtons.YesNo) == DialogResult.Yes)
         {
            QuickFilter.DeleteQuickFilter(cmbSelectQuickFilter.SelectedIndex - 1);
         }
      }

      /// <summary>
      /// 
      /// </summary>
      private void ApplyQuickFilterChanges()
      {
         int itemCount = 1;
         resultLbox.Items.Clear();
         List<QuickFilter> qfList = QuickFilter.QuickFilterList;
         string[] tbLokalitaSplit = tbLokalita.Text.Contains(";") ? tbLokalita.Text.Split(";") : new string[] { tbLokalita.Text }; //more locations
         if (qfList.Count > 0) //quickfilter and lokalita
         {
            foreach (BazosOffers item in BazosOffers.ListBazosOffers)
            {
               int cena = 0;
               string nadpis = TextAdjust.RemoveDiacritics(item.nadpis);
               if (qfList.Any(qf => nadpis.Contains(qf.nadpis, StringComparison.OrdinalIgnoreCase))) //nadpis is matched in quickfilter
               {
                  QuickFilter quickfilter = qfList.FirstOrDefault(qf => nadpis.Contains(qf.nadpis, StringComparison.OrdinalIgnoreCase));
                  int.TryParse(item.cena, out cena);
                  if ((!quickfilter.FullNadpisName || nadpis.Split(' ').Contains(quickfilter.nadpis)) && 
                     (quickfilter.maxCena == 0 || (cena >= 0 && cena <= quickfilter.maxCena) || cbDisableQuickFilterPrice.Checked) && 
                     !QuickFilter.Blacklist.Any(qfNadpis => nadpis.Contains(qfNadpis, StringComparison.OrdinalIgnoreCase)) && 
                     (tbLokalita.Text == string.Empty || tbLokalitaSplit.Any(lokalita => item.lokace.Contains(lokalita, StringComparison.OrdinalIgnoreCase)))) //test if item is matched to max cena
                  {
                     AddItemToResultLbox(itemCount, item);
                     itemCount++; //itemPlus = true;
                  }
               }
            } 
         }
         else if (tbLokalita.Text.Trim() != string.Empty) //lokalita only
         {
            foreach (BazosOffers item in BazosOffers.ListBazosOffers) //+blacklist lokalita with dot - ? advaced minihelp (př. Praha;Brno.venkov)
            {
               if (tbLokalitaSplit.Any(lokalita => item.lokace.Contains(lokalita, StringComparison.OrdinalIgnoreCase)))
               {
                  AddItemToResultLbox(itemCount, item);
                  itemCount++;
               }
            }
         }
         else //everything - without filter
         {
            AddOffersToResultLbox(BazosOffers.ListBazosOffers);
         }
      }

      /// <summary>
      /// Change create quickfilter button text to edit or create when quickfilter name in combobox exist.
      /// </summary>
      private void tbQuickFilter_TextChanged(object sender, EventArgs e)
      {
         string quickFilterName = tbQuickFilter.Text.Contains(":") ? tbQuickFilter.Text.Split(":")[0] : string.Empty;
         List<string> selectQuickFilterList = new List<string>();
         foreach (string item in cmbSelectQuickFilter.Items)
         {
            if (item != "none")
            {
               selectQuickFilterList.Add(item);
            }
         }
         btnCreateQuickFilter.Text = quickFilterName != string.Empty && selectQuickFilterList.Count > 0 && selectQuickFilterList.Any(p => p.Split(":")[0] == quickFilterName) ? "edit" : "create";
      }

      #endregion

      #endregion

      #region Auto-bot Panel
      /// <summary>
      /// Comboboxes:
      /// </summary>
      private void cmbBot_SelectedIndexChanged(object sender, EventArgs e)
      {

      }

      private void cmbCategoryUrlBot_SelectedIndexChanged(object sender, EventArgs e)
      {

      }
      private void cmbQuickFilterBot_SelectedIndexChanged(object sender, EventArgs e)
      {

      }

      /// <summary>
      /// Buttons:
      /// </summary>
      private void btnAddToQuickFilterBot_Click(object sender, EventArgs e)
      {

      }

      private void btnCreateBot_Click(object sender, EventArgs e)
      {

      }

      private void btnStartBot_Click(object sender, EventArgs e)
      {

      }

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

   }
}