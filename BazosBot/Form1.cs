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
      Stopwatch sw = new Stopwatch(); //measurement get offers seconds elapsed
      Stopwatch waitingSw = new Stopwatch(); //to not overload the server when downloading

      public Form1()
      {
         InitializeComponent();
         PrepareUserInterface();
		   InitTimers();
      }

      /// <summary>
      /// 
      /// </summary>
      private void Form1_Load(object sender, EventArgs e)
      {
         cmbSelectOffers.Items.AddRange(DB_Access.ListActualOffersCategoryURLInDB().ToArray());
         //cmbSelectOffers.Items.AddRange(AutoBot.ListActualMultiCategoryInDB().ToArray());
         cmbBotCategoryUrl.Items.AddRange(cmbSelectOffers.Items.Cast<object>().ToArray());
         cmbSelectQuickFilter.Items.Add("none");
         cmbSelectQuickFilter.SelectedIndex = 0;
         //cmbSelectQuickFilter.Sorted = true;
         cmbBotName.Items.Add("none");
         cmbBotName.Items.AddRange(AutoBot.GetBotNamesFromDB().ToArray());
         //SortBotNames();
         cmbBotName.SelectedIndex = 0;
         cmbSelectOffers.Sorted = true;
      }

      #region timers
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
         string downloadString = $"Download progress: {Download.count} / {Download.fullCount} - {percent}%";
         string elapsedTimeString = $"\nTime elapsed: {elapsedTime} sec.";
         lbProgress.Text = !Download.getOnlyNewOffers ? 
            !Download.downloadDone ? !Download.waiting ? $"{downloadString}{elapsedTimeString}": //waiting to not overload the server
            $"{downloadString}{elapsedTimeString}\nWaiting to not overload the server - {Math.Round(waitingSw.Elapsed.TotalSeconds, 2)} sec." : //downloading done
            $"{downloadString} - done!\nUpdating data to DB: {DB_Access.i} / {DB_Access.offersCount} - {dbPercent}%{elapsedTimeString}" : //download only new offers 
            !Download.downloadDone ? $"download: in progress\nTime elapsed: {elapsedTime} sec." : $"download: done!{elapsedTimeString}";
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
      /// autobot timer
      /// </summary>
      private void auto_timer_tick(object s, EventArgs a)
      {
         foreach (AutoBot aBot in AutoBot.BotList.ToList()) //not enqueued Bot - wait to interval
         {
            double elapsedSec = Math.Round(aBot.sw.Elapsed.TotalSeconds, 0);
            if (elapsedSec > aBot.interval || !aBot.sw.IsRunning) //switching from botlist to botqueue (and then back)
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
         else //+ošetřit - neběží nic jiného (start bot nebo start downloading)
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
               BazosOffers.actualCategoryURL = tbSearchUrl.Text != string.Empty ? tbSearchUrl.Text : cmbSelectOffers.Text; //actual category url id
               if (BazosOffers.actualCategoryURL.Contains("bazos.cz/") || BazosOffers.actualCategoryURL.Contains("bazos.sk/")) //+more contains in one command ?
               {
                  cmbSelectOffersType.SelectedIndex = 0;
                  ResetList(); //reset list of offers and result lisbox
                  DB_Access.notUpdateViewedAndLastChecked = cboxNotUpdateViewedAndLastChecked.Checked;
                  Thread thread = new Thread(() => Download.DownloadAllFromCategory(BazosOffers.actualCategoryURL, cboxDownOnlyLast.Checked));
                  //Download.DownloadAllFromCategory(tbSearchUrl.Text, cboxDownOnlyLast.Checked); //await download
                  thread.Start();
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
         if (resultLbox.SelectedIndex >= 0)
         {
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
            if (!cmbSelectOffers.SelectedItem.ToString().Substring(0, "multicategory".Length).Contains("multicategory", StringComparison.OrdinalIgnoreCase))
            {
               tbSearchUrl.Clear();
               resultLbox.Items.Clear();
               BazosOffers.ListBazosOffers = DB_Access.ListActualOffersInDB(cmbSelectOffers.Text);
               AddOffersToResultLbox(BazosOffers.ListBazosOffers);
               lastSelectedItem = cmbSelectOffers.Text;
               changedCategory = true;
               ChangeCmbSelectQuickFilter(cmbSelectOffers.Text);
            }
            else
            {
               tbSearchUrl.Clear();
               resultLbox.Items.Clear();
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
      private void btnCreateQuickFilter_Click(object sender, EventArgs e)
      {
         if (!string.IsNullOrEmpty(tbQuickFilter.Text) && !string.IsNullOrEmpty(tbSearchUrl.Text) || !string.IsNullOrEmpty(cmbSelectOffers.Text))
         {
            //QuickFilter.GetQuickFiltersFromTextbox(tbQuickFilter.Text);
            //string[] filterSplit = tbQuickFilter.Text.Split(";");
            //QuickFilter.Name = filterSplit[0].Contains(":") ? filterSplit[0].Split(":")[0] : string.Empty;
            string categoryUrl = tbSearchUrl.Text != string.Empty ? tbSearchUrl.Text : cmbSelectOffers.Text;
            string name;
            QuickFilter.SaveQuickFilterToDB(tbQuickFilter.Text, categoryUrl, out name);
            if (btnCreateQuickFilter.Text != "edit") //create (button name on tbQuickFilter textchanged event)
            {
               cmbSelectQuickFilter.Items.Add($"{name}: {tbQuickFilter.Text}");
               cmbSelectQuickFilter.SelectedItem = tbQuickFilter.Text;
            }
            else //edit
            {
               cmbSelectQuickFilter.Items[cmbSelectQuickFilter.SelectedIndex] = tbQuickFilter.Text;
            }
         }
      }

      bool changedCategory = false; //not apply quick filter changes when changed category
      /// <summary>
      /// 
      /// </summary>
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
         if (!actualCategoryUrl.Contains("multicategory", StringComparison.OrdinalIgnoreCase))
         {
            cmbSelectQuickFilter.Items.Add("none");
            cmbSelectQuickFilter.Items.AddRange(QuickFilter.ListActualQuickFiltersInDB(actualCategoryUrl).ToArray());
            cmbSelectQuickFilter.SelectedIndex = 0;
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
         if (e.KeyCode == Keys.Delete && cmbSelectQuickFilter.Text != "none" && MessageBox.Show("Opravdu smazat quick filter z výběru (comboboxu)?", "smazat quickfilter z comboboxu", MessageBoxButtons.YesNo) == DialogResult.Yes)
         {
            tbQuickFilter.Text = tbQuickFilter.Text.Equals(cmbSelectQuickFilter.SelectedItem.ToString()) ? string.Empty : tbQuickFilter.Text;
            QuickFilter.DeleteQuickFilterFromDB(cmbSelectQuickFilter.SelectedItem.ToString());
            cmbSelectQuickFilter.Items.Remove(cmbSelectQuickFilter.SelectedItem);
            cmbSelectQuickFilter.SelectedIndex = 0;
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
            if (AutoBot.tempBotList.Count > 0 && botChanged && MessageBox.Show("Uložit rozdělanou práci?", "uložit rozdělanou práci", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
               botSaved = true;
               botChanged = false; //comment this and lol trolling
               string botName = tbBotName.Text != string.Empty ? tbBotName.Text : cmbBotName.SelectedIndex > 0 ? cmbBotName.SelectedItem.ToString() : AutoBot.defBotName("unfinished");
               string lastBotName = lastSelectedBotName != "none" ? lastSelectedBotName : string.Empty;
               AutoBot.SaveBotToDB(cboxMultiCategory.Checked, lastBotName, botName); ///
               bool botNameChanged = cmbBotName.SelectedIndex == 0 || cmbBotName.SelectedItem.ToString() == botName ? false : true;
               MessageBox.Show($"Bot {botName} was successfully saved!");
               if (!cmbBotName.Items.Contains(botName))
               {
                  cmbBotName.Items.Add(botName);
               }
               //SortBotNames();
               if (botNameChanged)
               {
                  cmbBotName.Items.Remove(cmbBotName.SelectedItem);
               }
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
         else if (AutoBotUINotClear() && !botChanged || botSaved || (botChanged && MessageBox.Show("Vymazat rozdělanýho bota?", "vymazat bota", MessageBoxButtons.YesNo) == DialogResult.Yes))
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
         if ((tbBotQuickFilter.Text == string.Empty && !botCategoryChanged) || cmbBotQuickFilter.SelectedItem.ToString() == tbBotQuickFilter.Text || !botQuickFilterTextChanged || (!botCategoryChanged && MessageBox.Show("Přemazat rozepsaný filter?", "Přemazat filter", MessageBoxButtons.YesNo) == DialogResult.Yes))
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
               cmbBotQuickFilter.Items.Add($"{name}: {tbBotQuickFilter.Text}");
               cmbBotQuickFilter.SelectedItem = tbBotQuickFilter.Text;
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
            string quickFilterText = tbBotQuickFilter.Text != string.Empty ? tbBotQuickFilter.Text : cmbBotQuickFilter.Items.Count > 0 && !string.IsNullOrEmpty(cmbBotQuickFilter.SelectedItem.ToString()) && cmbBotQuickFilter.Text != "none" ? cmbBotQuickFilter.SelectedItem.ToString() : string.Empty;
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
         cmbBotQuickFilter.Items.AddRange(QuickFilter.ListActualQuickFiltersInDB(actualCategoryUrl).ToArray());
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
         if (e.KeyCode == Keys.Delete && cmbBotQuickFilter.Text != "none" && MessageBox.Show("Opravdu smazat quick filter?", "smazat quickfilter", MessageBoxButtons.YesNo) == DialogResult.Yes)
         {
            tbBotQuickFilter.Text = tbBotQuickFilter.Text.Equals(cmbBotQuickFilter.SelectedItem.ToString()) ? string.Empty : tbBotQuickFilter.Text;
            QuickFilter.DeleteQuickFilterFromDB(cmbBotQuickFilter.SelectedItem.ToString());
            cmbBotQuickFilter.Items.Remove(cmbBotQuickFilter.SelectedItem);
            cmbBotQuickFilter.SelectedIndex = 0;
         }
      }

      /// <summary>
      /// Delete bot.
      /// </summary>
      private void cmbBotName_KeyDown(object sender, KeyEventArgs e)
      {
         if (e.KeyCode == Keys.Delete && cmbBotName.Text != "none" && MessageBox.Show("Opravdu smazat bota včetně všech parametrů?", "smazat bota", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
               BotInterval.Value = bot.interval;
               BotFullTime.Value = (decimal)bot.fullInterval;
               string? test = lboxBotCategory.SelectedItem.ToString();
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
            if (botQuickFilterList.Count == 0 || (botQuickFilterList.Count > 0 && MessageBox.Show("Opravdu smazat kategorii z bot listu včetně přiřazených quickfilterů"/*\n- Smažou se i přiřazený quickfiltery ke kategorii u vytvářeného bota.*/, "Smazat kategorii bota?", MessageBoxButtons.YesNo) == DialogResult.Yes))
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
            //      MessageBox.Show("Vyplň interval u této kategorie");
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
         if (!Download.isRunning)
         {
            AutoBot.BotList.Clear();
            AutoBot.BotQueue.Clear();
            if (AutoBot.SavedBotList.Any(p => p.botName == tbBotName.Text)) //cmb -> botName
            {
               AutoBot.BotList = AutoBot.SavedBotList.Where(p => p.botName == tbBotName.Text).ToList();
            }
            else if (lboxBotCategory.Items.Count > 0) //bot is not in saved bot list
            {
               foreach (string category in lboxBotCategory.Items)
               {
                  string botName = tbBotName.Text != string.Empty ? tbBotName.Text : AutoBot.defBotName("default"); //
                  List<string> botQuickFilterTextList = AutoBot.tempBotList.FirstOrDefault(a => a.category == category).quickFilterTextList; //give default bot name when it is not in textbox (+number)
                  AutoBot.BotList.Add(new AutoBot(botName, category, botQuickFilterTextList, Convert.ToInt32(BotInterval.Value), cboxMultiCategory.Checked, Convert.ToInt32(BotFullTime.Value)));
               }
            }
            //AutoBot.BotList.AddRange()
         }
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