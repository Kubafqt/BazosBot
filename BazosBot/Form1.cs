using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
//.net maui, corona filter program - respirator
//idea - sort by - price, name (select order to sort)

namespace BazosBot
{
   public partial class Form1 : Form
   {
      string activePanel;
      System.Windows.Forms.Timer timer; //showing progress timer
      System.Windows.Forms.Timer autoTimer; //Bot timer
      Stopwatch sw = new Stopwatch(); //measurement get offers seconds elapsed
      Stopwatch waitingSw = new Stopwatch(); //to not overload the server when downloading
      //string updatedCheckChange;

      public Form1()
      {
         InitializeComponent();
         PrepareUserInterface();
         InitTimers();
         Size = Settings.FormSize["default"];
         Settings.MainPanelLocation("default", Controls);
         bool helpButton;
         DB_Access.LoadSettings(out helpButton);
         cboxHelpButton.Checked = helpButton;
         btnHelpQuickFilter.Visible = helpButton;
      }

      #region timers
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
         autoTimer.Interval = 250;
      }

      /// <summary>
      /// timer switch
      /// </summary> 
      private bool switchBot = false; //Bot get full offers - start stopwatch after getted
      private void switchtimer(bool exception = false)
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
            if (exception)
            {
               lbProgress.Text = string.Empty;
               lbProgress.Hide();
            }
         }
      }

      /// <summary>
      /// showing timer
      /// </summary>
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
         string downloadString = $"Download progress: {Download.count} / {Download.fullCount} - {percent}%";
         string elapsedTimeString = $"\nTime elapsed: {elapsedTime} sec.";
         lbProgress.Text = !Download.getOnlyNewOffers ?
            !Download.downloadDone ? !Download.waiting ? $"{downloadString}{elapsedTimeString}" : //waiting to not overload the server
            $"{downloadString}{elapsedTimeString}\nWaiting to not overload the server - {Math.Round(waitingSw.Elapsed.TotalSeconds, 2)} sec." : //downloading done
            $"{downloadString} - done!\nUpdating data to DB: {DB_Access.i} / {DB_Access.offersCount} - {dbPercent}%{elapsedTimeString}" : //download only new offers 
            !Download.downloadDone ? $"download: in progress\nTime elapsed: {elapsedTime} sec." : $"download: done!\nUpdating data to DB: {DB_Access.i} / {DB_Access.offersCount} - {dbPercent}%{elapsedTimeString}";
         //finished routine:
         if (Download.downloadDone && !Download.isRunning)
         {
            switchtimer();
            Download.downloadDone = false;
            AddOffersToResultLbox(BazosOffers.ListBazosOffers, cmbSelectOffersType.SelectedItem.ToString()); //result lisbox
            elapsedTime = sw.Elapsed.Milliseconds >= 500 ? elapsedTime + 1 : elapsedTime;
            //labels
            string allOffers = $"{Download.fullCount}";
            string newOffers = $"{DB_Access.newOffersList.Count}";
            string updatedOffers = DB_Access.updatedList.Count > 0 ? $"{DB_Access.updatedList.Count}" : "not found";
            string deletedOffers = Download.getOnlyNewOffers ? "not found" : $"{DB_Access.deletedList.Count}";
            lbAllOffers.Text = $"all offers: {allOffers}";
            lbNewOffers.Text = $"new offers: {newOffers}";
            lbUpdatedCount.Text = $"updated: {updatedOffers}";
            lbDeletedCount.Text = $"deleted: {deletedOffers}";
            btnGetBazos.Text = "get offers";
         }
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
      /// autobot timer
      /// </summary>
#nullable enable
      AutoBot? nextBot;
#nullable disable
      int timeToRun;
      private void auto_timer_tick(object s, EventArgs a)
      {
         if (AutoBot.LastBot != null && AutoBot.LastBot.stoppedRunning)
         {
            AutoBot.LastBot.stoppedRunning = false;
            lbBotRunning.Text = "";
         }
         foreach (AutoBot aBot in AutoBot.BotList.ToList()) //not enqueued Bot - wait to interval
         {
            double elapsedSec = Math.Round(aBot.sw.Elapsed.TotalSeconds, 0);
            if (elapsedSec > aBot.interval || !aBot.sw.IsRunning) //switching from botlist to botqueue (and then back)
            {
               nextBot = null;
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
               AutoBot.BotList.Add(AutoBot.LastBot);
               RunBot();
            }
            else if (!AutoBot.LastBot.isRunning) //after lastBot finished run
            {
               AutoBot.BotList.Add(AutoBot.LastBot);
               AutoBot.LastBot = AutoBot.BotQueue.Dequeue();
               RunBot();
            }
         }
         else if (AutoBot.BotList.Count > 0 && !AutoBot.LastBot.isRunning) //report upcoming download
         {
            Dictionary<string, double> NextBotTimeToLeft = new Dictionary<string, double>();
            foreach (var item in AutoBot.BotList)
            {
               double timeLeft = item.interval - Math.Round(item.sw.Elapsed.TotalSeconds, 0);
               NextBotTimeToLeft.Add(item.category, timeLeft);
            }
            string category = NextBotTimeToLeft.Min().Key; //test more minimum value?
            nextBot = AutoBot.BotList.First(p => p.category == category);
            lbBotRunning.Text = $"Bot running: {AutoBot.runningBotName}\nNext downloading category: {nextBot.category} in {timeToRun} sec.";
            //nextBot = nextBot != null ? nextBot : AutoBot.BotList.First(x => x.interval == AutoBot.BotList.Select(p => p.interval - (int)Math.Round(p.sw.Elapsed.TotalSeconds, 0)).Min());
            //timeToRun = nextBot != null ? nextBot.interval - (int)Math.Round(nextBot.sw.Elapsed.TotalSeconds, 0) : 420;
            //AutoBot nextBot = timeToRun.Select(p => p.) //timeToRun.RemoveAll(t => t < 1); //int nextBot = timeToRun.Min();
         }
      }

      /// <summary>
      /// 
      /// </summary>
      private void RunBot()
      {
         if (Download.IsConnectedToInternet())
         {
            bool getOnlyNewOffers = AutoBot.LastBot.fullInterval == 0 || AutoBot.LastBot.timesUsed < AutoBot.LastBot.fullInterval;
            AutoBot.LastBot.timesUsed = getOnlyNewOffers ? AutoBot.LastBot.timesUsed : 0;
            AutoBot.LastBot.isRunning = true;
            //AutoBot nextBot = AutoBot.BotList.Count > 0 ? AutoBot.BotList.Where(p => p.interval - (int)Math.Round(p.sw.Elapsed.TotalSeconds, 0) > 0).Min() : null;
            //int timeToRun = nextBot != null ? nextBot.interval - (int)Math.Round(nextBot.sw.Elapsed.TotalSeconds, 0) : 0;
            lbBotRunning.Text = $"Bot running: {AutoBot.runningBotName}\nDownloading category: {AutoBot.LastBot.category}";
            lbBotRunning.Text = AutoBot.BotQueue.Count > 0 ? $"{lbBotRunning.Text}\nUpcoming category: {AutoBot.BotQueue.Peek().category}" : nextBot != null ? $"{lbBotRunning.Text}\nNext downloading category: {nextBot.category} in {timeToRun} sec." : lbBotRunning.Text;
            if (getOnlyNewOffers)
            {
               AutoBot.LastBot.sw.Start();
               switchtimer();
            }
            else //+ošetøit - nebìží nic jiného (start bot nebo start downloading)
            {
               switchBot = true; //start timing stopwatch after finished full downloading
               switchtimer();
            }
            Thread thread = new Thread(() => Download.DownloadAllFromCategory(AutoBot.LastBot.category, getOnlyNewOffers, true));
            thread.Start();
         }
         else
         {
            autoTimer.Stop();
            MessageBox.Show("Check internet connection! - bot stopped");
         }
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
            bool connection = Download.IsConnectedToInternet();
            if (!Download.isRunning && connection)
            {
               BazosOffers.actualCategoryURL = tbSearchUrl.Text != string.Empty ? tbSearchUrl.Text : cmbSelectOffers.Text; //actual category url id
               if (BazosOffers.actualCategoryURL.Contains("bazos.cz/") || BazosOffers.actualCategoryURL.Contains("bazos.sk/")) //+more contains in one command ?
               {
                  cmbSelectOffersType.SelectedIndex = 0;
                  ResetList(); //reset list of offers and result lisbox
                  DB_Access.notUpdateViewedAndLastChecked = cboxNotUpdateViewedAndLastChecked.Checked;
                  Thread thread = new Thread(() => Download.DownloadAllFromCategory(BazosOffers.actualCategoryURL, cboxDownOnlyLast.Checked));
                  //Download.DownloadAllFromCategory(tbSearchUrl.Text, cboxDownOnlyLast.Checked); //await download
                  thread.Start();
                  if (!QuickFilter.DictActualQuickFilters.ContainsKey(BazosOffers.actualCategoryURL))
                  {
                     QuickFilter.DictActualQuickFilters.Add(BazosOffers.actualCategoryURL, new List<string>());
                  }
                  if (!BlacklistSet.DictActualBlacklistSet.ContainsKey(BazosOffers.actualCategoryURL))
                  {
                     BlacklistSet.DictActualBlacklistSet.Add(BazosOffers.actualCategoryURL, new List<string>());
                  }
                  if (!cmbSelectOffers.Items.Contains(tbSearchUrl.Text) && tbSearchUrl.Text != string.Empty) //+check if it is right url
                  {
                     cmbSelectOffers.Items.Add(tbSearchUrl.Text);
                     cmbBotCategoryUrl.Items.Add(tbSearchUrl.Text);
                  }
                  ChangeCmbSelectQuickFilter(BazosOffers.actualCategoryURL);
                  switchtimer();
               }
               else //+then another method for aukro, etc. servers
               {
                  MessageBox.Show("Zadej URL pro bazoš!");
               }
               btnGetBazos.Text = "stop dowloading";
            }
            else if (!connection)
            {
               MessageBox.Show("Check internet connection!");
            }
            else if (!Download.downloadDone)
            {
               Download.stopped = true;
               btnGetBazos.Text = "stopping";
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

      private void AddOffersToResultLbox(List<BazosOffers> itemList, string offerType, int itemCount = 1)
      {
         foreach (BazosOffers item in itemList.ToList())
         {
            if (offerType == "updated" && DB_Access.showUpdateChanges.Any(p => item.changed.Contains(p)))
            {
               resultLbox.Items.Add($"{itemCount}) {item.nadpis} for {item.cena} - {item.lokace} / {item.psc} - {item.datum} - {item.viewed} x - {item.url}");
               itemCount++;
            }
            else if (offerType != "updated")
            {
               resultLbox.Items.Add($"{itemCount}) {item.nadpis} for {item.cena} - {item.lokace} / {item.psc} - {item.datum} - {item.viewed} x - {item.url}");
               itemCount++;
            }
         }
      }

      /// <summary>
      /// 
      /// </summary>  
      /// <param name="show"></param>
      private void showUpdates(bool show = true)
      {
         offerLbox.Size = show ? new Size(offerLbox.Width, resultLbox.Height - updatesPanel.Height - 20) : new Size(offerLbox.Width, resultLbox.Height);
         MinimumSize = show ? new Size(MinimumSize.Width, 542) : new Size(MinimumSize.Width, 371);
         updatesPanel.Visible = show;
         if (show)
         {
            updatesPanel.Location = new Point(offerLbox.Location.X, offerLbox.Location.Y + offerLbox.Height + 20);
            foreach (Control control in updatesPanel.Controls.OfType<CheckBox>())
            {
               (control as CheckBox).Checked = true;
            }
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
         if (resultLbox.SelectedIndex >= 0)
         {
            try
            {
#nullable enable
               string? result = resultLbox.SelectedItem.ToString();
#nullable disable
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
                     if (Regex.IsMatch(cmbSelectOffersType.Text, "updated", RegexOptions.IgnoreCase) && DB_Access.updateChangeTypes.Any(p => item.changed.Contains(p))) //it is type of updated offers and cotains change other than only datum or price
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
      }

      private void resultLbox_DoubleClick(object sender, EventArgs e)
      {
         openResultLboxUrl();
      }

      private void resultLbox_KeyDown(object sender, KeyEventArgs e)
      {
         if (e.KeyCode == Keys.Enter)
         {
            openResultLboxUrl();
         }
      }

      private void openResultLboxUrl()
      {
         if (resultLbox.SelectedIndex >= 0)
         {
            List<BazosOffers> actualList = !Regex.IsMatch(cmbSelectOffersType.Text, "deleted", RegexOptions.IgnoreCase) ? BazosOffers.ListBazosOffers : DB_Access.deletedList;
            BazosOffers item = actualList.FirstOrDefault(of => of.nadpis == Regex.Replace(resultLbox.SelectedItem.ToString().Split(')', 2)[1], @"\sfor\s", ";").Split(";")[0].Trim());
            Browsers.StartBrowser(item.url);
         }
      }
      #endregion

      #region offers control
      /// <summary>
      /// offerLbox
      /// </summary>
      private void offerLbox_DoubleClick(object sender, EventArgs e)
      {
         openOfferLboxUrl();
      }

      private void offerLbox_KeyDown(object sender, KeyEventArgs e)
      {
         if (e.KeyCode == Keys.Enter)
         {
            openOfferLboxUrl();
         }
      }

      private void openOfferLboxUrl()
      {
         if (offerLbox.Items.Count > 0)
         {
            List<string> items = new List<string>(offerLbox.Items.Cast<string>());
            string url = items.First(p => p.Contains("url:")).Replace("url:", string.Empty).Trim();
            Browsers.StartBrowser(url);
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
      string lastSelectedOfferType;
      private void cmbSelectOffersType_SelectedIndexChanged(object sender, EventArgs e)
      {
         if (DB_Access.downloaded)
         {
            resultLbox.Items.Clear();

            switch (cmbSelectOffersType.Text)
            {
               case "all offers":
                  {
                     AddOffersToResultLbox(BazosOffers.ListBazosOffers, cmbSelectOffersType.SelectedItem.ToString());
                     break;
                  }
               case "new offers":
                  {
                     AddOffersToResultLbox(DB_Access.newOffersList, cmbSelectOffersType.SelectedItem.ToString());
                     break;
                  }
               case "updated":
                  {
                     AddOffersToResultLbox(DB_Access.updatedList, cmbSelectOffersType.SelectedItem.ToString());
                     break;
                  }
               case "deleted":
                  {
                     AddOffersToResultLbox(DB_Access.deletedList, cmbSelectOffersType.SelectedItem.ToString());
                     break;
                  }
               default:
                  break;
            }
            if (lastSelectedOfferType == "updated" || cmbSelectOffersType.Text == "updated")
            {
               bool show = cmbSelectOffersType.Text == "updated" ? true : false;
               showUpdates(show);
            }
            lastSelectedOfferType = cmbSelectOffersType.Text;
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
         if (resultLbox.Items.Count == 0 || lastSelectedItem != cmbSelectOffers.Text && resultLbox.Items.Count > 0 && MessageBox.Show("Nahrát tento seznam vìcí?", "Pøemazat vìci v listboxu?", MessageBoxButtons.YesNo) == DialogResult.Yes)
         {
            BazosOffers.actualCategoryURL = cmbSelectOffers.SelectedItem.ToString();
            if (!cmbSelectOffers.SelectedItem.ToString().Substring(0, "multicategory".Length).Contains("multicategory", StringComparison.OrdinalIgnoreCase))
            {
               tbSearchUrl.Clear();
               resultLbox.Items.Clear();
               offerLbox.Items.Clear();
               BazosOffers.ListBazosOffers = DB_Access.ListActualOffersInDB(cmbSelectOffers.Text);
               AddOffersToResultLbox(BazosOffers.ListBazosOffers, cmbSelectOffersType.SelectedItem.ToString());
               lastSelectedItem = cmbSelectOffers.Text;
               changedCategory = true;
               ChangeCmbSelectQuickFilter(cmbSelectOffers.Text);
            }
            else
            {
               tbSearchUrl.Clear();
               resultLbox.Items.Clear();
               offerLbox.Items.Clear();
               //BazosOffers.ListBazosOffers = AutoBot.ListActualMultiCategoryInDB(cmbSelectOffers.Text);
               lastSelectedItem = cmbSelectOffers.Text;
               changedCategory = true;
               ChangeCmbSelectQuickFilter(cmbSelectOffers.Text);
            }
         }
      }

      private void cmbSelectOffers_KeyDown(object sender, KeyEventArgs e)
      {
         if (cmbSelectOffers.Items.Count > 0 && e.KeyCode == Keys.Delete && MessageBox.Show("Opravdu vymazat kategorii, jeji záznamy a quickfiltery z databází?", "Vymazat kategorii?", MessageBoxButtons.YesNo) == DialogResult.Yes)
         {
            DB_Access.DeleteOffersCategoryFromDB(cmbSelectOffers.SelectedItem.ToString());
            cmbBotCategoryUrl.Items.Remove(cmbSelectOffers.SelectedItem);
            cmbSelectOffers.Items.Remove(cmbSelectOffers.SelectedItem);
            if (cmbSelectOffers.Items.Count > 0)
            {
               cmbSelectOffers.SelectedIndex = 0;
               cmbBotCategoryUrl.SelectedIndex = 0;
            }
            else
            {
               cmbSelectQuickFilter.Items.Clear();
               //cmbBotSelectQuickFilter ? 
            }
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
      /// <summary>
      /// 
      /// </summary>
      private void btnApplyQuickFilter_Click(object sender, EventArgs e)
      {
         QuickFilter.GetQuickFiltersFromTextbox(tbQuickFilter.Text);
         ApplyQuickFilterChanges();
      }

      /// <summary>
      /// 
      /// </summary>
      private void btnCreateQuickFilter_Click(object sender, EventArgs e) //
      {
         if (!string.IsNullOrEmpty(tbQuickFilter.Text) && !string.IsNullOrEmpty(tbSearchUrl.Text) || !string.IsNullOrEmpty(cmbSelectOffers.Text))
         {
            //QuickFilter.GetQuickFiltersFromTextbox(tbQuickFilter.Text);
            //string[] filterSplit = tbQuickFilter.Text.Split(";");
            //QuickFilter.Name = filterSplit[0].Contains(":") ? filterSplit[0].Split(":")[0] : string.Empty;
            quickFilterChanged = false;
            string categoryUrl = tbSearchUrl.Text != string.Empty ? tbSearchUrl.Text : cmbSelectOffers.Text;
            string name;
            QuickFilter.SaveQuickFilterToDB(tbQuickFilter.Text, categoryUrl, out name);
            if (btnCreateQuickFilter.Text != "edit") //create (button name on tbQuickFilter textchanged event)
            {
               string quickFilterText = $"{name}: {tbQuickFilter.Text.Replace($"{name}:", string.Empty).Trim()}";
               cmbSelectQuickFilter.Items.Add(quickFilterText);
               cmbSelectQuickFilter.SelectedIndex = cmbSelectQuickFilter.Items.Count - 1;
               tbQuickFilter.Text = cmbSelectQuickFilter.Text;
            }
            else //edit
            {
               cmbSelectQuickFilter.Items[cmbSelectQuickFilter.SelectedIndex] = tbQuickFilter.Text;
               //QuickFilter.GetQuickFiltersFromTextbox(tbQuickFilter.Text);
               //ApplyQuickFilterChanges();
            }
         }
      }

      bool changedCategory = false; //not apply quick filter changes when changed category
      bool quickFilterChanged = false;
      /// <summary>
      /// 
      /// </summary>
      private void cmbSelectQuickFilter_SelectedIndexChanged(object sender, EventArgs e)
      {
         //tbQuickFilter.Text = cmbSelectQuickFilter.SelectedText;
         //btnApplyQuickFilter.PerformClick();
         if (cmbSelectQuickFilter.SelectedItem.ToString() != "none" && (!quickFilterChanged || MessageBox.Show("Opravdu pøemazat rozdìlaný quickfilter?", "pøemazat quickfilter", MessageBoxButtons.YesNo) == DialogResult.Yes))
         {
            QuickFilter.GetQuickFiltersFromTextbox(cmbSelectQuickFilter.SelectedItem.ToString());
            tbQuickFilter.Text = cmbSelectQuickFilter.SelectedItem.ToString();
            ApplyQuickFilterChanges();
         }
         else if (!changedCategory && (!quickFilterChanged || MessageBox.Show("Opravdu pøemazat rozdìlaný quickfilter?", "pøemazat quickfilter", MessageBoxButtons.YesNo) == DialogResult.Yes)) //quickfilte none
         {
            QuickFilter.QuickFilterList.Clear();
            tbQuickFilter.Clear();
            ApplyQuickFilterChanges();
         }
         else //when changed category
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
         cmbSelectBlacklistSet.Items.Clear();
         if (!actualCategoryUrl.Contains("multicategory", StringComparison.OrdinalIgnoreCase))
         {
            cmbSelectQuickFilter.Items.Add("none");
            if (QuickFilter.DictActualQuickFilters.ContainsKey(actualCategoryUrl))
            {
               cmbSelectQuickFilter.Items.AddRange(QuickFilter.DictActualQuickFilters[actualCategoryUrl].ToArray());
            }
            cmbSelectQuickFilter.SelectedIndex = 0;
            string quickFilterName = tbQuickFilter.Text.Contains(":") ? tbQuickFilter.Text.Split(new[] { ':' }, 2)[0] : string.Empty;
            List<string> selectQuickFilterList = new List<string>();
            //selectQuickFilterList.AddRange(cmbSelectQuickFilter.Items);
            foreach (string item in cmbSelectQuickFilter.Items)
            {
               if (item != "none")
               {
                  selectQuickFilterList.Add(item);
               }
            }
            btnCreateQuickFilter.Text = selectQuickFilterList.Count > 0 && selectQuickFilterList.Any(p => p.Split(":")[0] == quickFilterName) ? "edit" : "create";
            //blacklist set:
            cmbSelectBlacklistSet.Items.Add("none");
            if (BlacklistSet.DictActualBlacklistSet.ContainsKey(actualCategoryUrl))
            {
               cmbSelectBlacklistSet.Items.AddRange(BlacklistSet.DictActualBlacklistSet[actualCategoryUrl].ToArray());
            }
            cmbSelectBlacklistSet.SelectedIndex = 0;
            string blacklistSetName = tbQuickFilter.Text.Contains(":") ? tbQuickFilter.Text.Split(new[] { ':' }, 2)[0] : string.Empty;
            List<string> selectBlacklistSetList = new List<string>();
            foreach (string item in cmbSelectBlacklistSet.Items)
            {
               if (item != "none")
               {
                  selectBlacklistSetList.Add(item);
               }
            }
            btnCreateBlacklistSet.Text = selectBlacklistSetList.Count > 0 && selectBlacklistSetList.Any(p => p.Split(":")[0] == blacklistSetName) ? "edit" : "create";
         }
         else
         {
            cmbSelectQuickFilter.Items.Add("all"); //then show all maybe
         }
      }

      /// <summary>
      /// Delete keydown for delete quickfilter.
      /// </summary>
      private void cmbSelectQuickFilter_KeyDown(object sender, KeyEventArgs e)
      {
         if (e.KeyCode == Keys.Delete && cmbSelectQuickFilter.Text != "none")
         {
            string deleteItem = cmbSelectQuickFilter.Text;
            if (MessageBox.Show("Opravdu smazat quick filter z výbìru (comboboxu)?", "smazat quickfilter z comboboxu", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
               tbQuickFilter.Text = tbQuickFilter.Text.Equals(cmbSelectQuickFilter.SelectedItem.ToString()) ? string.Empty : tbQuickFilter.Text;

               QuickFilter.DeleteQuickFilterFromDB(deleteItem, cmbSelectOffers.SelectedItem.ToString());
               cmbSelectQuickFilter.Items.Remove(deleteItem);
               cmbSelectQuickFilter.SelectedIndex = 0;
            }
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
                     (quickfilter.minCena == 0 || (cena >= 0 && cena >= quickfilter.minCena) || cbDisableQuickFilterPrice.Checked) &&
                     (quickfilter.maxCena == 0 || (cena >= 0 && cena <= quickfilter.maxCena) || cbDisableQuickFilterPrice.Checked) &&
                     !QuickFilter.Blacklist.Any(qfNadpis => TextAdjust.RemoveDiacritics(nadpis).Contains(qfNadpis, StringComparison.OrdinalIgnoreCase)) &&
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
            foreach (BazosOffers item in BazosOffers.ListBazosOffers) //+blacklist lokalita with dot - ? advaced minihelp (pø. Praha;Brno.venkov)
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
            AddOffersToResultLbox(BazosOffers.ListBazosOffers, cmbSelectOffersType.SelectedItem.ToString());
         }
      }

      /// <summary>
      /// Change create quickfilter button text to edit or create when quickfilter name in combobox exist.
      /// </summary>
      private void tbQuickFilter_TextChanged(object sender, EventArgs e)
      {
         string quickFilterName = tbQuickFilter.Text.Contains(":") ? tbQuickFilter.Text.Split(new[] { ':' }, 2)[0] : string.Empty;
         List<string> selectQuickFilterList = new List<string>();
         foreach (string item in cmbSelectQuickFilter.Items)
         {
            if (item != "none")
            {
               selectQuickFilterList.Add(item);
            }
         }
         btnCreateQuickFilter.Text = quickFilterName != string.Empty && selectQuickFilterList.Count > 0 && selectQuickFilterList.Any(p => p.Split(":")[0] == quickFilterName) ? "edit" : "create";
         quickFilterChanged = !selectQuickFilterList.Contains(tbQuickFilter.Text) && !string.IsNullOrWhiteSpace(tbQuickFilter.Text);
      }

      #endregion

      #region blacklistSet
      private void btnAddBlacklistSetToQuickFilter_Click(object sender, EventArgs e)
      {
         if (cmbSelectOffers.SelectedIndex >= 0 && !string.IsNullOrEmpty(cmbSelectBlacklistSet.SelectedItem.ToString()) && (!string.IsNullOrEmpty(tbSearchUrl.Text) || !string.IsNullOrEmpty(cmbSelectOffers.SelectedItem.ToString())) && cmbSelectBlacklistSet.SelectedItem.ToString() != "none")
         {
            string[] filterSplit = cmbSelectBlacklistSet.Text.Split(";");
            string blacklistSetName = $"{filterSplit[0].Split(":")[0]}";
            if (tbQuickFilter.Text.Length > 0 && !tbQuickFilter.Text.Contains($"/{blacklistSetName}"))
            {
               tbQuickFilter.Text = tbQuickFilter.Text.Substring(tbQuickFilter.Text.Length - 1, 1) == ";" ? $"{tbQuickFilter.Text}/{blacklistSetName}" : $"{tbQuickFilter.Text};/{blacklistSetName}";
            }
         }
      }

      private void btnCreateBlacklistSet_Click(object sender, EventArgs e)
      {
         if (!string.IsNullOrEmpty(tbBlackListSet.Text) && !string.IsNullOrEmpty(tbSearchUrl.Text) || !string.IsNullOrEmpty(cmbSelectOffers.Text))
         {
            string categoryUrl = tbSearchUrl.Text != string.Empty ? tbSearchUrl.Text : cmbSelectOffers.Text;
            string name;
            string blacklistSetText = tbBlackListSet.Text.Replace(".", string.Empty);
            BlacklistSet.SaveBlacklistSetToDB(blacklistSetText, categoryUrl, out name);
            if (btnCreateBlacklistSet.Text != "edit") //create (button name on tbQuickFilter textchanged event)
            {
               blacklistSetChanged = false;
               BlacklistSet.DictActualBlacklistSet[categoryUrl].Add(blacklistSetText);
               string blacklistSet = $"{name}: {tbBlackListSet.Text.Replace($"{name}:", string.Empty).Trim()}";
               cmbSelectBlacklistSet.Items.Add(blacklistSet);
               cmbSelectBlacklistSet.SelectedIndex = cmbSelectBlacklistSet.Items.Count - 1;
               tbBlackListSet.Text = cmbSelectBlacklistSet.Text;
            }
            else //edit
            {
               blacklistSetChanged = false;
               int currentIndex = BlacklistSet.DictActualBlacklistSet[categoryUrl].IndexOf(cmbSelectBlacklistSet.SelectedItem.ToString());
               BlacklistSet.DictActualBlacklistSet[categoryUrl][currentIndex] = blacklistSetText;
               cmbSelectBlacklistSet.Items[cmbSelectBlacklistSet.SelectedIndex] = blacklistSetText;
               //BlacklistSet.ChangeBlacklistSet(blacklistSetText);
            }
         }
      }

      bool blacklistSetChanged = false;
      private void cmbSelectBlacklistSet_SelectedIndexChanged(object sender, EventArgs e)
      {
         if (cmbSelectBlacklistSet.SelectedItem.ToString() != "none" && (!blacklistSetChanged || MessageBox.Show("Opravdu pøemazat rozdìlaný blacklistSet?", "pøemazat blacklistSet", MessageBoxButtons.YesNo) == DialogResult.Yes))
         {
            tbBlackListSet.Text = cmbSelectBlacklistSet.SelectedItem.ToString();
         }
         else if (!changedCategory && (!blacklistSetChanged || MessageBox.Show("Opravdu pøemazat rozdìlaný blacklistSet?", "pøemazat blacklistSet", MessageBoxButtons.YesNo) == DialogResult.Yes)) //blacklistSet none
         {
            tbBlackListSet.Clear();
         }
         else //when changed category
         {
            changedCategory = false;
         }
      }

      private void tbBlackListSet_TextChanged(object sender, EventArgs e)
      {
         string blacklistSetName = tbBlackListSet.Text.Contains(":") ? tbBlackListSet.Text.Split(new[] { ':' }, 2)[0] : string.Empty;
         List<string> selectBlacklistSetList = new List<string>();
         foreach (string item in cmbSelectBlacklistSet.Items)
         {
            if (item != "none")
            {
               selectBlacklistSetList.Add(item);
            }
         }
         btnCreateBlacklistSet.Text = blacklistSetName != string.Empty && selectBlacklistSetList.Count > 0 && selectBlacklistSetList.Any(p => p.Split(":")[0] == blacklistSetName) ? "edit" : "create";
         blacklistSetChanged = !selectBlacklistSetList.Contains(tbBlackListSet.Text) && !string.IsNullOrWhiteSpace(tbBlackListSet.Text);
      }

      private void cmbSelectBlacklistSet_KeyDown(object sender, KeyEventArgs e)
      {
         if (e.KeyCode == Keys.Delete && cmbSelectBlacklistSet.Text != "none")
         {
            string deleteItem = cmbSelectBlacklistSet.Text;
            if (MessageBox.Show("Opravdu smazat quick filter z výbìru (comboboxu)?", "smazat quickfilter z comboboxu", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
               tbBlackListSet.Text = tbBlackListSet.Text.Equals(cmbSelectBlacklistSet.SelectedItem.ToString()) ? string.Empty : tbBlackListSet.Text;
               BlacklistSet.DeleteBlacklistSetFromDB(deleteItem, cmbSelectOffers.SelectedItem.ToString());
               cmbSelectBlacklistSet.Items.Remove(deleteItem);
               cmbSelectBlacklistSet.SelectedIndex = 0;
            }
         }
      }

      #endregion

      /// <summary>
      /// Help buttons
      /// </summary>
      private void btnHelpQuickFilter_Click(object sender, EventArgs e)
      {
         MessageBox.Show("name - show offers contained this name in nadpis,\nname! - show offers contained only this name in nadpis (not in word),\nname < or name <= - show offer contained this name till this max price,\n.name - blacklist - dont show offers with this name in nadpis\nUse semicolon - ; - to get more offers, same for more offers locations.\n\nFilters are case and diacritics insensitive.", "QuickFilter help");
      }

      /// <summary>
      /// 
      /// </summary>
      private void btnHelpYouTube_Click(object sender, EventArgs e)
      {

      }

      #endregion

      #region Auto-bot Panel
      bool botChanged = false;
      bool botSaved = false;
      string lastSelectedBotName = string.Empty;
      /// <summary>
      /// Comboboxes:
      /// </summary>
      private void cmbBotName_SelectedIndexChanged(object sender, EventArgs e)
      {
         if (!sortingBotNames)
         {
            if (AutoBot.tempBotList.Count > 0 && botChanged && MessageBox.Show("Uložit rozdìlanou práci?", "uložit rozdìlanou práci", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
               botSaved = true;
               botChanged = false; //comment this and lol trolling
               string botName = tbBotName.Text != string.Empty ? tbBotName.Text : cmbBotName.SelectedIndex > 0 ? cmbBotName.SelectedItem.ToString() : AutoBot.defBotName("unfinished");
               string lastBotName = lastSelectedBotName != "none" ? lastSelectedBotName : string.Empty;
               AutoBot.SaveBotToDB(cboxMultiCategory.Checked, lastBotName, botName); ///
               //bool botNameChanged = cmbBotName.SelectedIndex == 0 || cmbBotName.SelectedItem.ToString() == botName ? false : true;
               MessageBox.Show($"Bot {botName} was successfully saved!");
               if (!cmbBotName.Items.Contains(botName))
               {
                  cmbBotName.Items.Add(botName);
                  if (lastBotName != botName)
                  {
                     cmbBotName.Items.Remove(lastBotName);
                  }
               }
               //SortBotNames();
               //if (botNameChanged)
               //{
               //   cmbBotName.Items.Remove(cmbBotName.SelectedItem);
               //}
               //if (Settings.selectBotAfterCreateOrEdit)
               //{
               //   cmbBotName.SelectedItem = botName;
               //   cmbBotName.SelectedIndex = cmbBotName.SelectedIndex;
               //}
               //else
               //{
               //   cmbBotName.SelectedIndex = 0;
               //}
            }
            if (cmbBotName.SelectedIndex > 0)
            {
               btnCreateBot.Text = "Edit Bot";
            }
            else
            {
               btnCreateBot.Text = "Create Bot";
            }
            //if ()
            // ClearAutoBotUI();
            LoadAutoBotUI(cmbBotName.SelectedItem.ToString());
            lastSelectedBotName = cmbBotName.SelectedItem.ToString();
         }
      }

      bool sortingBotNames = false;
      /// <summary>
      /// 
      /// </summary>
      private void SortBotNames()
      {
         sortingBotNames = true;
         string str = cmbBotName.Items[0].ToString();
         cmbBotName.Items.RemoveAt(0);
         cmbBotName.Sorted = true; // Sort the items
         cmbBotName.Sorted = false; // Disable the sorting
         cmbBotName.Items.Insert(0, str);
         sortingBotNames = false;
      }

      /// <summary>
      /// 
      /// </summary>
      private void cmbCategoryUrlBot_SelectedIndexChanged(object sender, EventArgs e)
      {
         botCategoryChanged = true;
         ChangeCmbSelectBotQuickFilter(cmbBotCategoryUrl.Text);
         //if (cmb)
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="botName"></param>
      private void LoadAutoBotUI(string botName)
      {
         if (botName != "none")
         {
            tbBotName.Text = botName;
            AutoBot.tempBotList = AutoBot.SavedBotList.Where(p => p.botName == botName).ToList(); //functional?
            lboxBotCategory.Items.Clear();
            lboxBotCategory.Items.AddRange(AutoBot.tempBotList.Select(p => p.category).ToArray());
            //cboxMultiCategory.Checked = AutoBot.tempBotList[0].
            //lboxBotCategory.Items.AddRange(AutoBot.tempBotList(p => p.category != string.Empty).);
            //foreach (AutoBot bot in AutoBot.tempBotList)
            //{

            //}
         }
         else if (AutoBotUINotClear() && !botChanged || botSaved || (botChanged && MessageBox.Show("Vymazat rozdìlanýho bota?", "vymazat bota", MessageBoxButtons.YesNo) == DialogResult.Yes))
         {
            botChanged = false;
            botSaved = false;
            ClearAutoBotUI();
         }
      }

      /// <summary>
      /// 
      /// </summary>
      private void ClearAutoBotUI()
      {
         tbBotName.Text = string.Empty;
         tbBotCategoryUrl.Clear();
         tbBotQuickFilter.Clear();
         lboxBotCategory.Items.Clear();
         lboxBotQuickFilter.Items.Clear();
         BotInterval.Value = 1;
         BotFullTime.Value = 0;
         cboxMultiCategory.Checked = false;
         AutoBot.tempBotList.Clear();
      }

      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      private bool AutoBotUINotClear()
      {
         return tbBotName.Text != string.Empty || tbBotCategoryUrl.Text != string.Empty || tbBotQuickFilter.Text != string.Empty || lboxBotCategory.Items.Count > 0 || lboxBotQuickFilter.Items.Count > 0; //bool clear
      }

      #region bot quickfilter

      bool botQuickFilterTextChanged = false;
      /// <summary>
      /// Detect if quickfilter name is contained in quickfilter list.
      /// </summary>
      private void tbBotQuickFilter_TextChanged(object sender, EventArgs e)
      {
         string quickFilterName = tbBotQuickFilter.Text.Contains(":") ? tbBotQuickFilter.Text.Split(new[] { ':' }, 2)[0] : string.Empty;
         List<string> selectQuickFilterList = new List<string>();
         foreach (string item in cmbBotQuickFilter.Items)
         {
            selectQuickFilterList.Add(item);
         }
         botQuickFilterTextChanged = !cmbBotQuickFilter.Items.Contains(tbBotQuickFilter.Text) ? true : false;
         btnAddQuickFilterBot.Text = quickFilterName != string.Empty && selectQuickFilterList.Count > 0 && selectQuickFilterList.Any(p => p.Split(":")[0] == quickFilterName) ? "edit" : "add";
      }

      bool botCategoryChanged = false;
      /// <summary>
      /// When changed combobox of quickfilters in bot panel.
      /// </summary>
      private void cmbQuickFilterBot_SelectedIndexChanged(object sender, EventArgs e)
      {
         if ((tbBotQuickFilter.Text == string.Empty && !botCategoryChanged) || cmbBotQuickFilter.SelectedItem.ToString() == tbBotQuickFilter.Text || !botQuickFilterTextChanged || (!botCategoryChanged && MessageBox.Show("Pøemazat rozepsaný filter?", "Pøemazat filter", MessageBoxButtons.YesNo) == DialogResult.Yes))
         {
            string botQuickFilterText = cmbBotQuickFilter.SelectedItem.ToString() != "none" ? cmbBotQuickFilter.SelectedItem.ToString() : string.Empty;
            tbBotQuickFilter.Text = botQuickFilterText;
         }
         else if (botCategoryChanged)//&& cmbBotQuickFilter.SelectedItem.ToString() != "none")
         {
            botCategoryChanged = false;
         }
      }

      /// <summary>
      /// Button: Create quickfilter to category.
      /// </summary>
      private void btnAddQuickFilterBot_Click(object sender, EventArgs e)
      {
         if (!string.IsNullOrEmpty(tbBotQuickFilter.Text) && (!string.IsNullOrEmpty(tbBotCategoryUrl.Text) || !string.IsNullOrEmpty(cmbBotCategoryUrl.Text)))
         {
            //string[] filterSplit = tbBotQuickFilter.Text.Split(";");
            //QuickFilter.Name = filterSplit[0].Contains(":") ? filterSplit[0].Split(":")[0] : string.Empty;
            string categoryUrl = tbBotCategoryUrl.Text != string.Empty ? tbBotCategoryUrl.Text : cmbBotCategoryUrl.Text;
            string name;
            QuickFilter.SaveQuickFilterToDB(tbBotQuickFilter.Text, categoryUrl, out name);
            if (btnAddQuickFilterBot.Text != "edit") //create (button name on tbQuickFilter textchanged event)
            {
               string quickFilterText = $"{name}: {tbBotQuickFilter.Text.Replace($"{name}:", string.Empty).Trim()}";
               cmbBotQuickFilter.Items.Add(quickFilterText);
               botCategoryChanged = true; //not ask if pøemazat rozdìlaný filter
               cmbBotQuickFilter.SelectedIndex = cmbBotQuickFilter.Items.Count - 1;
               tbBotQuickFilter.Text = cmbBotQuickFilter.SelectedItem.ToString();
               btnAddQuickFilterBot.Text = "edit";
            }
            else //edit
            {
               cmbBotQuickFilter.Items[cmbBotQuickFilter.SelectedIndex] = tbBotQuickFilter.Text;
            }
         }
      }

      string addCategory = string.Empty;
      /// <summary>
      /// Button: Add name, category and quickfilter to bot and listboxes.
      /// </summary>
      private void btnAddItemsToBot_Click(object sender, EventArgs e)
      {
         //bot category url selected - in textbox or in combobox:
         if (!string.IsNullOrEmpty(tbBotCategoryUrl.Text) || !string.IsNullOrEmpty(cmbBotCategoryUrl.Text))
         {
            botCategoryItemDeleted = false;
            addCategory = tbBotCategoryUrl.Text != string.Empty ? tbBotCategoryUrl.Text : cmbBotCategoryUrl.Text; //new category
            string botName = tbBotName.Text != string.Empty ? tbBotName.Text : AutoBot.defBotName("default");
            //rename actual bot:
            if (AutoBot.tempBotList.Count > 0 && AutoBot.tempBotList[0].botName != botName)
            {
               //AutoBot.lastBotName = cmbBotName.SelectedItem.ToString() != "none" ? cmbBotName.SelectedItem.ToString() : string.Empty;
               foreach (var bot in AutoBot.tempBotList)
               {
                  bot.botName = botName;
               }
               botChanged = true;
            }

            //adds quickfilter from textbox or combobox:
            string quickFilterName = !tbBotQuickFilter.Text.Contains(":") ? QuickFilter.GetQuickFilterName(tbBotCategoryUrl.Text, cmbBotCategoryUrl.SelectedItem.ToString()) : tbBotQuickFilter.Text.Split(":")[0];
            string quickFilterText = tbBotQuickFilter.Text != string.Empty ? $"{quickFilterName}: {tbBotQuickFilter.Text.Replace($"{quickFilterName}:", string.Empty).Trim()}" : cmbBotQuickFilter.Items.Count > 0 && !string.IsNullOrEmpty(cmbBotQuickFilter.SelectedItem.ToString()) && cmbBotQuickFilter.Text != "none" ? cmbBotQuickFilter.SelectedItem.ToString() : string.Empty;
            if (string.IsNullOrWhiteSpace(quickFilterText)) { goto NoQuickFilter; }
            if (AutoBot.tempBotList.Any(p => p.category == addCategory) && !AutoBot.tempBotList.First(p => p.category == addCategory).quickFilterTextList.Contains(quickFilterText))
            {
               lboxBotQuickFilter.Items.Add(quickFilterText);
               botChanged = true;
            }
         NoQuickFilter:
            //add quickfilter text to db:
            if (AutoBot.tempBotList.Any(p => p.category == addCategory))
            {
               AutoBot bot = AutoBot.tempBotList.FirstOrDefault(p => p.category == addCategory);
               bot.interval = Convert.ToInt32(BotInterval.Value) > 0 ? (int)BotInterval.Value : 1;
               bot.fullInterval = Convert.ToInt32(BotFullTime.Value);
               if (quickFilterText != string.Empty && !bot.quickFilterTextList.Contains(quickFilterText))
               {
                  bot.quickFilterTextList.Add(quickFilterText);
               }
               botChanged = true;
            }
            //add new category (bot) to tempBotList and its quickfilter:
            else //(create new bot)
            {
               lboxBotQuickFilter.Items.Clear();
               if (quickFilterText != string.Empty)
               {
                  lboxBotQuickFilter.Items.Add(quickFilterText);
               }
               List<string> QuickFilterTextList = lboxBotQuickFilter.Items.Cast<string>().ToList();
               AutoBot.tempBotList.Add(new AutoBot(botName, addCategory, QuickFilterTextList, Convert.ToInt32(BotInterval.Value), cboxMultiCategory.Checked, Convert.ToInt32(BotFullTime.Value)));
               botChanged = true;
            }

            //add category to listbox & select it:
            if (!lboxBotCategory.Items.Contains(addCategory)) //add new category to combobox
            {
               lboxBotCategory.Items.Add(addCategory);
               lboxBotCategory.SelectedItem = addCategory;
               //AutoBot.DictCategoryQuickFilterText.Add(addCategory, new List<string>());
            }
            //select bot category, +string.Empty check?:
            if (lboxBotCategory.SelectedIndex > 0 && lboxBotCategory.SelectedItem.ToString() != addCategory)
            {
               lboxBotCategory.SelectedItem = addCategory;
            }
         }
      }

      /// <summary>
      /// When changing bot quickfilter combobox.
      /// </summary>
      /// <param name="actualCategoryUrl"></param>
      private void ChangeCmbSelectBotQuickFilter(string actualCategoryUrl)
      {
         cmbBotQuickFilter.Items.Clear();
         cmbBotQuickFilter.Items.Add("none");
         //if (QuickFilter.ListActualQuickFilter = ) 
         cmbBotQuickFilter.Items.AddRange(QuickFilter.DictActualQuickFilters[actualCategoryUrl].ToArray());
         if (cmbBotQuickFilter.Items.Count > 0)
         {
            cmbBotQuickFilter.SelectedIndex = 0;
         }
      }

      /// <summary>
      /// Delete quickfilter.
      /// </summary>
      private void cmbBotQuickFilter_KeyDown(object sender, KeyEventArgs e)
      {
         if (e.KeyCode == Keys.Delete && cmbBotQuickFilter.SelectedItem.ToString() != "none")
         {
            string deleteItem = cmbBotQuickFilter.SelectedItem.ToString();
            if (MessageBox.Show("Opravdu smazat quick filter?", "smazat quickfilter", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
               tbBotQuickFilter.Text = tbBotQuickFilter.Text.Equals(cmbBotQuickFilter.SelectedItem.ToString()) ? string.Empty : tbBotQuickFilter.Text;
               QuickFilter.DeleteQuickFilterFromDB(deleteItem, cmbBotCategoryUrl.SelectedItem.ToString());
               cmbBotQuickFilter.Items.Remove(deleteItem);
               cmbBotQuickFilter.SelectedIndex = 0;
            }
         }
      }

      /// <summary>
      /// Delete bot.
      /// </summary>
      private void cmbBotName_KeyDown(object sender, KeyEventArgs e)
      {
         if (e.KeyCode == Keys.Delete && cmbBotName.Text != "none" && MessageBox.Show("Opravdu smazat bota vèetnì všech parametrù?", "smazat bota", MessageBoxButtons.YesNo) == DialogResult.Yes)
         {
            AutoBot.DeleteBotFromDB(cmbBotName.SelectedItem.ToString());
            cmbBotName.Items.Remove(cmbBotName.SelectedItem);
            cmbBotName.SelectedIndex = 0;
         }
      }

      #endregion bot quickfilter

      /// <summary>
      /// listbox category:
      /// </summary>
      bool botCategoryItemDeleted = false;
      private void lboxBotCategory_SelectedIndexChanged(object sender, EventArgs e)
      {
         if (lboxBotCategory.SelectedIndex >= 0)
         {
            lboxBotQuickFilter.Items.Clear();
            if (!botCategoryItemDeleted) //cuz string? and string.IsNullOrEmpty() not working to get null - check stack overflow
            {
               AutoBot bot = AutoBot.tempBotList.First(p => p.category == lboxBotCategory.SelectedItem.ToString());
               BotInterval.Value = (int)bot.interval;
               BotFullTime.Value = (decimal)bot.fullInterval;
#nullable enable
               string? test = lboxBotCategory.SelectedItem.ToString();
#nullable disable
               if (!string.IsNullOrEmpty(test))
               {
                  lboxBotQuickFilter.Items.AddRange(bot.quickFilterTextList.ToArray());
               }
            }
            else
            {
               botCategoryItemDeleted = false;
            }
         }
      }

      /// <summary>
      /// 
      /// </summary>
      private void lboxBotQuickFilter_KeyDown(object sender, KeyEventArgs e)
      {
         if (e.KeyCode == Keys.Delete && lboxBotQuickFilter.SelectedIndex >= 0)
         {
            botChanged = true;
            string quickFilterText = lboxBotQuickFilter.SelectedItem.ToString();
            lboxBotQuickFilter.Items.Remove(quickFilterText);
            AutoBot.tempBotList.First(p => p.category == lboxBotCategory.SelectedItem.ToString()).quickFilterTextList.Remove(quickFilterText);
         }
      }

      /// <summary>
      /// Copy quickfilter text to clipboard on right mouse click.
      /// </summary>
      private void lboxBotQuickFilter_MouseDown(object sender, MouseEventArgs e)
      {
         if (e.Button == MouseButtons.Right && lboxBotQuickFilter.SelectedIndex >= 0)
         {
            Clipboard.SetText(lboxBotQuickFilter.SelectedItem.ToString());
         }
      }

      /// <summary>
      /// 
      /// </summary>
      private void lboxBotCategory_KeyDown(object sender, KeyEventArgs e)
      {
         if (e.KeyCode == Keys.Delete && AutoBot.tempBotList.Any(p => p.category == lboxBotCategory.SelectedItem.ToString()))
         {
            botChanged = true;
            List<string> botQuickFilterList = AutoBot.tempBotList.First(p => p.category == lboxBotCategory.SelectedItem.ToString()).quickFilterTextList;
            if (botQuickFilterList.Count == 0 || (botQuickFilterList.Count > 0 && MessageBox.Show("Opravdu smazat kategorii z bot listu vèetnì pøiøazených quickfilterù"/*\n- Smažou se i pøiøazený quickfiltery ke kategorii u vytváøeného bota.*/, "Smazat kategorii bota?", MessageBoxButtons.YesNo) == DialogResult.Yes))
            {
               addCategory = string.Empty;
               botCategoryItemDeleted = true;
               lboxBotQuickFilter.Items.Clear();
               AutoBot.tempBotList.Remove(AutoBot.tempBotList.First(p => p.category == lboxBotCategory.SelectedItem.ToString()));
               lboxBotCategory.Items.Remove(lboxBotCategory.SelectedItem);
            }
         }
      }

      /// <summary>
      /// Button: Create Bot.
      /// </summary>
      private void btnCreateBot_Click(object sender, EventArgs e)
      {
         if (AutoBot.tempBotList.Count > 0)
         {
            //foreach (AutoBot bot in AutoBot.tempBotList)
            //{
            //   if (bot.interval == 0)
            //   {
            //      MessageBox.Show("Vyplò interval u této kategorie");
            //      lboxBotCategory.SelectedItem = bot.category;
            //   }
            //}

            if (btnCreateBot.Text.Contains("create", StringComparison.OrdinalIgnoreCase))
            {
               AutoBot.SavedBotList.AddRange(AutoBot.tempBotList);
            }
            else
            {
               AutoBot.SavedBotList.RemoveAll(p => AutoBot.tempBotList.Any(g => g.botName == p.botName));
               AutoBot.SavedBotList.AddRange(AutoBot.tempBotList);
            }
            //bool type = btnCreateBot.Text.Substring(0, 4).ToLower() == "edit" ? true : false;
            string lastBotName = cmbBotName.SelectedItem.ToString() != "none" ? cmbBotName.SelectedItem.ToString() : string.Empty;
            string botName = tbBotName.Text != string.Empty ? tbBotName.Text : lastBotName != string.Empty ? cmbBotName.SelectedItem.ToString() : AutoBot.tempBotList[0].botName;
            AutoBot.SaveBotToDB(cboxMultiCategory.Checked, lastBotName, botName);
            botChanged = false;
            MessageBox.Show($"Bot successfully {botName} created!");
            if (!cmbBotName.Items.Contains(botName))
            {
               cmbBotName.Items.Add(botName);
            }
            //SortBotNames();
            bool botNameChanged = cmbBotName.SelectedIndex == 0 || cmbBotName.SelectedItem.ToString() == botName ? false : true;
            if (botNameChanged)
            {
               cmbBotName.Items.Remove(cmbBotName.SelectedItem);
            }
            if (Settings.selectBotAfterCreateOrEdit)
            {
               cmbBotName.SelectedItem = botName;
               cmbBotName.SelectedIndex = cmbBotName.SelectedIndex;
            }
            else
            {
               cmbBotName.SelectedIndex = 0;
            }
            ClearAutoBotUI();
         }
         else
         {
            MessageBox.Show("Není žádný autobot na listu!");
         }
      }

      /// <summary>
      /// 
      /// </summary>
      private void btnStartBot_Click(object sender, EventArgs e)
      {
         if (!Download.isRunning && !AutoBot.botRunning)
         {
            StartBot();
         }
         else if (AutoBot.botRunning)
         {
            StopBot();
         }
         else if (Download.isRunning)
         {
            MessageBox.Show("Wait till download is finished!");
         }
      }

      /// <summary>
      /// 
      /// </summary>
      private void notifyIcon_MouseDown(object sender, MouseEventArgs e)
      {
         if (e.Button == MouseButtons.Left)
         {
            Show();
            notifyIcon.Visible = false;
            WindowState = FormWindowState.Normal;
         }
         else if (e.Button == MouseButtons.Right)
         {
            //- show running bot and progress
            //menu:
            //- show
            //- exit
            //ballon-tip:
            //founded nabídky (+sound - settings, select sound - short || seconds of playing settings)
         }
      }

      /// <summary>
      /// 
      /// </summary>
      private void StartBot()
      {
         if (cboxHideToTray.Checked)
         {
            Hide();
            notifyIcon.Visible = true;
         }
         AutoBot.BotList.Clear();
         AutoBot.BotQueue.Clear();
         if (AutoBot.SavedBotList.Any(p => p.botName == tbBotName.Text)) //cmb -> botName
         {
            AutoBot.runningBotName = tbBotName.Text;
            AutoBot.BotList = AutoBot.SavedBotList.Where(p => p.botName == tbBotName.Text).ToList();
            autoTimer.Enabled = true;
            cmbSelectPanel.SelectedItem = "main panel";
            AutoBot.botRunning = true;
            btnStartBot.Text = "Stop Bot";
            Size = Settings.FormSize["botted"];
            Settings.MainPanelLocation("botted", Controls);
            lbBotRunning.Show();
            lbBotRunning.Text = $"Bot running: {AutoBot.runningBotName}";
         }
         else if (lboxBotCategory.Items.Count > 0) //bot is not saved to SavedBotList
         {
            string botName = cmbBotName.SelectedItem.ToString() != "none" ? cmbBotName.Text : tbBotName.Text != string.Empty ? tbBotName.Text : AutoBot.defBotName("default"); //
            AutoBot.runningBotName = botName;
            foreach (string category in lboxBotCategory.Items)
            {
               List<string> botQuickFilterTextList = AutoBot.tempBotList.FirstOrDefault(a => a.category == category).quickFilterTextList; //give default bot name when it is not in textbox (+number)
               AutoBot.BotList.Add(new AutoBot(botName, category, botQuickFilterTextList, Convert.ToInt32(BotInterval.Value), cboxMultiCategory.Checked, Convert.ToInt32(BotFullTime.Value)));
            }
            autoTimer.Enabled = true;
            cmbSelectPanel.SelectedItem = "main panel";
            AutoBot.botRunning = true;
            btnStartBot.Text = "Stop Bot";
            Size = Settings.FormSize["botted"];
            Settings.MainPanelLocation("botted", Controls);
            lbBotRunning.Show();
            lbBotRunning.Text = $"Bot running: {AutoBot.runningBotName}";
         }
         else
         {
            MessageBox.Show("Any bot is selected!");
         }
         //AutoBot.BotList.AddRange()
      }

      /// <summary>
      /// 
      /// </summary>
      private void StopBot()
      {
         autoTimer.Stop();
         AutoBot.BotList.Clear();
         AutoBot.BotQueue.Clear();
         btnStartBot.Text = "Start Bot";
         Size = Settings.FormSize["default"];
         Settings.MainPanelLocation("default", Controls);
         lbBotRunning.Hide();
      }

      /// <summary>
      /// Numeric updowns:
      /// </summary>
      private void BotInterval_ValueChanged(object sender, EventArgs e)
      {
         string category = lboxBotCategory.SelectedIndex >= 0 ? lboxBotCategory.SelectedItem.ToString() : addCategory;
         if (category != string.Empty && AutoBot.tempBotList.Any(p => p.category == category))
         {
            AutoBot bot = AutoBot.tempBotList.First(p => p.category == category); //functional to change bot in list? - i think so
                                                                                  //int numericValue = (int)BotInterval.Value;
            bot.interval = (int)BotInterval.Value;//numericValue > 0 ? numericValue : 1; //minimum 1 sec interval
         }
      }

      private void BotFullTime_ValueChanged(object sender, EventArgs e)
      {
         string category = lboxBotCategory.SelectedIndex >= 0 ? lboxBotCategory.SelectedItem.ToString() : addCategory;
         if (category != string.Empty && AutoBot.tempBotList.Any(p => p.category == category))
         {
            AutoBot bot = AutoBot.tempBotList.First(p => p.category == category); //functional to change bot in list? - i think so
            bot.fullInterval = (int)BotFullTime.Value;
         }
      }

      #endregion

      #region Control methods
      /// <summary>
      /// 
      /// </summary>
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

         foreach (Control panel in Controls.OfType<Panel>().Where(p => Equals(p.Tag, "mainPanels")))
         {
            cmbSelectPanel.Items.Add(Settings.DictMainPanelsNameValue.FirstOrDefault(c => c.Value == panel.Name).Key); //by value
            panel.Location = Settings.defaultPanelLocation;
            panel.Width = Size.Width - panel.Location.X - 12;
            panel.Height = Size.Height - panel.Location.Y - 42;
         }


         resultLbox.Width = (panelMain.Width - resultLbox.Location.X - 60) / 3 * 2;
         resultLbox.Height = panelMain.Height - resultLbox.Location.Y - 40;
         offerLbox.Location = new Point(resultLbox.Location.X + resultLbox.Width + 20, offerLbox.Location.Y);
         offerLbox.Size = cmbSelectOffersType.Text == "updated" ? new Size((panelMain.Width - resultLbox.Location.X - 60) / 3, resultLbox.Height - updatesPanel.Height - 20) : new Size((panelMain.Width - resultLbox.Location.X - 60) / 3, resultLbox.Height);
         updatesPanel.Location = cmbSelectOffersType.Text == "updated" ? new Point(offerLbox.Location.X, offerLbox.Location.Y + offerLbox.Height + 20) : updatesPanel.Location;

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
         cmbSelectPanel.SelectedItem = "main panel";

         cmbSelectOffers.Items.AddRange(DB_Access.ListActualOffersCategoryURLInDB().ToArray());
         //cmbSelectOffers.Items.AddRange(AutoBot.ListActualMultiCategoryInDB().ToArray());
         cmbBotCategoryUrl.Items.AddRange(cmbSelectOffers.Items.Cast<object>().ToArray());
         cmbSelectQuickFilter.Items.Add("none");
         cmbSelectQuickFilter.SelectedIndex = 0;
         //cmbSelectQuickFilter.Sorted = true;
         cmbBotName.Items.Add("none");
         cmbBotName.Items.AddRange(AutoBot.GetBotNamesFromDB().ToArray());
         SortBotNames();
         //SortBotNames();
         cmbBotName.SelectedIndex = 0;
         cmbSelectOffers.Sorted = true;
      }

      private void btnSelectPanel_Click(object sender, EventArgs e)
      {
         //ChangePanel(Settings.DictPanelNameValue[cmbSelectPanel.SelectedItem.ToString()]);
         //DB_Access.InsertNewOffers("test");
      }

      private void cboxHelpButton_CheckedChanged(object sender, EventArgs e)
      {
         btnHelpQuickFilter.Visible = cboxHelpButton.Checked;
      }

      #endregion

      #region updates
      private void cboxUpdateNadpis_CheckedChanged(object sender, EventArgs e)
      {
         if (cboxUpdateNadpis.Checked)
         {
            DB_Access.showUpdateChanges.Add("nadpis");
         }
         else
         {
            DB_Access.showUpdateChanges.Remove("nadpis");
         }
         resultLbox.Items.Clear();
         AddOffersToResultLbox(DB_Access.updatedList, cmbSelectOffersType.SelectedItem.ToString());
      }

      private void cboxUpdateCena_CheckedChanged(object sender, EventArgs e)
      {
         if (cboxUpdateCena.Checked)
         {
            DB_Access.showUpdateChanges.Add("cena");
         }
         else
         {
            DB_Access.showUpdateChanges.Remove("cena");
         }
         resultLbox.Items.Clear();
         AddOffersToResultLbox(DB_Access.updatedList, cmbSelectOffersType.SelectedItem.ToString());
      }

      private void cboxUpdatePopis_CheckedChanged(object sender, EventArgs e)
      {
         if (cboxUpdatePopis.Checked)
         {
            DB_Access.showUpdateChanges.Add("popis");
         }
         else
         {
            DB_Access.showUpdateChanges.Remove("popis");
         }
         resultLbox.Items.Clear();
         AddOffersToResultLbox(DB_Access.updatedList, cmbSelectOffersType.SelectedItem.ToString());
      }

      private void cboxUpdateDate_CheckedChanged(object sender, EventArgs e)
      {
         if (cboxUpdateDate.Checked)
         {
            DB_Access.showUpdateChanges.Add("datum");
         }
         else
         {
            DB_Access.showUpdateChanges.Remove("datum");
         }
         resultLbox.Items.Clear();
         AddOffersToResultLbox(DB_Access.updatedList, cmbSelectOffersType.SelectedItem.ToString());
      }

      private void cboxUpdateLocation_CheckedChanged(object sender, EventArgs e)
      {
         if (cboxUpdateLocation.Checked)
         {
            DB_Access.showUpdateChanges.Add("lokace");
         }
         else
         {
            DB_Access.showUpdateChanges.Remove("lokace");
         }
         resultLbox.Items.Clear();
         AddOffersToResultLbox(DB_Access.updatedList, cmbSelectOffersType.SelectedItem.ToString());
      }

      #endregion

      private void Form1_FormClosing(object sender, FormClosingEventArgs e)
      {
         DB_Access.SaveSettings(cboxHelpButton.Checked);
      }

      private void Form1_Resize(object sender, EventArgs e)
      {
         if (WindowState == FormWindowState.Minimized)
         {
            Hide();
            notifyIcon.Visible = true;
         }

         foreach (Control panel in Controls.OfType<Panel>().Where(p => Equals(p.Tag, "mainPanels")))
         {
            panel.Width = Size.Width - panel.Location.X - 10;
            panel.Height = Size.Height - panel.Location.Y - 10;
         }
         //ef

         //main panel:
         resultLbox.Width = (panelMain.Width - resultLbox.Location.X - 60) / 3 * 2;
         resultLbox.Height = panelMain.Height - resultLbox.Location.Y - 40;
         offerLbox.Location = new Point(resultLbox.Location.X + resultLbox.Width + 20, offerLbox.Location.Y);
         offerLbox.Size = cmbSelectOffersType.Text == "updated" ? new Size((panelMain.Width - resultLbox.Location.X - 60) / 3, resultLbox.Height - updatesPanel.Height - 20) : new Size((panelMain.Width - resultLbox.Location.X - 60) / 3, resultLbox.Height);
         updatesPanel.Location = cmbSelectOffersType.Text == "updated" ? new Point(offerLbox.Location.X, offerLbox.Location.Y + offerLbox.Height + 20) : updatesPanel.Location;

         tbSearchUrl.Width = (int)(panelMain.Width / 2.6) - 67;
         tbBlackListSet.Width = tbSearchUrl.Width - btnAddBlacklistSetToQuickFilter.Width - 6;
         tbQuickFilter.Width = tbBlackListSet.Width;
         btnAddBlacklistSetToQuickFilter.Location = new Point(tbBlackListSet.Location.X + tbBlackListSet.Width + 6, btnAddBlacklistSetToQuickFilter.Location.Y);
         btnApplyQuickFilter.Location = new Point(btnAddBlacklistSetToQuickFilter.Location.X, btnApplyQuickFilter.Location.Y);
         btnCreateQuickFilter.Location = new Point(btnApplyQuickFilter.Location.X + btnApplyQuickFilter.Width + 6, btnCreateQuickFilter.Location.Y);
         btnCreateBlacklistSet.Location = new Point(btnCreateQuickFilter.Location.X, btnCreateBlacklistSet.Location.Y);
         cmbSelectQuickFilter.Location = new Point(btnCreateQuickFilter.Location.X + btnCreateQuickFilter.Width + 6, cmbSelectQuickFilter.Location.Y);
         cmbSelectBlacklistSet.Location = new Point(cmbSelectQuickFilter.Location.X, cmbSelectBlacklistSet.Location.Y);
         cmbSelectOffersType.Location = new Point(tbSearchUrl.Location.X + tbSearchUrl.Width + 6, cmbSelectOffersType.Location.Y);
         cmbSelectOffers.Location = new Point(cmbSelectOffersType.Location.X + cmbSelectOffersType.Width + 6, cmbSelectOffersType.Location.Y);
         cmbSelectQuickFilter.Width = offerLbox.Location.X + offerLbox.Width - cmbSelectQuickFilter.Location.X; //(int)(panelMain.Width / 2.42) - 7;
         cmbSelectOffers.Width = offerLbox.Location.X + offerLbox.Width - cmbSelectOffers.Location.X; //(int)(panelMain.Width / 3.07) - 3;

         tbLokalita.Width = (int)(panelMain.Width / 8.72);
         cmbSelectBlacklistSet.Width = offerLbox.Location.X + offerLbox.Width - cmbSelectBlacklistSet.Location.X - lbLokace.Width - tbLokalita.Width - 12;
         lbLokace.Location = new Point(cmbSelectBlacklistSet.Location.X + cmbSelectBlacklistSet.Width + 5, lbLokace.Location.Y);
         tbLokalita.Location = new Point(lbLokace.Location.X + lbLokace.Width + 6, tbLokalita.Location.Y);

         //autobot panel:
         lboxBotQuickFilter.Height = panelAutoBot.Height - lboxBotQuickFilter.Location.Y - 32;
         lboxBotCategory.Height = panelAutoBot.Height - lboxBotCategory.Location.Y - 32;



      }


   }
}