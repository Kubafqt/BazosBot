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

namespace BazosBot
{
   public partial class Form1 : Form
   {
      string activePanel;
      public Form1()
      {
         InitializeComponent();
         PrepareUserInterface();
         AddUrlPageFilterName();
         lboxFilters.MouseUp += new System.Windows.Forms.MouseEventHandler(this.List_RightClick);
      }

      private void List_RightClick(object sender, MouseEventArgs e)
      {

         if (e.Button == MouseButtons.Right)
         {
            int index = lboxFilters.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
              // lboxFilters.Ite[index];
            }
         }

      }



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
         cmbSelectFilter.Items.Add("Filter Set");
         cmbSelectFilter.Items.Add("Filter Panel");
         cmbSelectFilter.Items.Add("Blacklist Panel");
         cmbSelectOffersType.Items.AddRange(cmbSelectOffersTypeString);
         cmbSelectOffersType.SelectedIndex = 0;
         if (cmbAddFilter.Items.Count > 0)
         {
            cmbAddFilter.SelectedIndex = 0;
         }
         if (cmbSelectUrl.Items.Count > 0)
         {
            cmbSelectUrl.SelectedIndex = 0;
         }
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

      /// <summary>
      /// 
      /// </summary>
      private void AddUrlPageFilterName()
      {
         Filters.InitFilterObjects();
         Filters.InitDictionary_PageURL_FilterName();
         FilterSet.InitFilterSetObjects();
         FilterSet.InitFilterSetDictionary();
         foreach (string key in Filters.Dict_URL_PAGE_FilterName.Keys)
         {
            if (!cmbSelectUrl.Items.Contains(key))
            {
               cmbSelectUrl.Items.Add(key);
            }
         }
      }

      private void LoadDefaultFilters()
      {
         //cmbSelectUrl.Items.AddRange(DB_Access.LoadDefaultUrls());
      }
 
      #endregion

      /// <summary>
      /// 
      /// </summary>
      private void btnGetBazos_Click(object sender, EventArgs e)
      {
         ResetList();
         int itemCount = 1;
         BazosOffers.actualCategoryNameURL = tbSearchUrl.Text;
         Download.DownloadAllFromCategory(tbSearchUrl.Text, cboxDownOnlyLast.Checked);
         //await download;
         //List<BazosOffers> lastOffers = DB_Access.ListActualOffersInDB();
         //foreach (BazosOffers item in BazosOffers.ListBazosOffers.ToList())
         //{
         //   resultLbox.Items.Add($"{itemCount}) {item.nadpis} for {item.cena} - {item.lokace} / {item.psc} - {item.datum} - {item.viewed} x - {item.url}");
         //   itemCount++;
         //   //if (!lastOffers.Contains(item)) //and checkbox checked
         //   //{
         //   //   itemCount++;
         //   //   resultLbox.Items.Add($"{itemCount}) {item.nadpis} for {item.cena} - {item.lokace} / {item.psc} - {item.datum} - {item.viewed} x - {item.url}"); //url show on click
         //   //}
         //   //}
         //}
         foreach (BazosOffers item in BazosOffers.ListBazosOffers.ToList())
         {
            AddItemsToResultLbox(itemCount, item);
            //resultLbox.Items.Add($"{itemCount}) {item.nadpis} for {item.cena} - {item.lokace} / {item.psc} - {item.datum} - {item.viewed} x - {item.url}");
            itemCount++;
            //if (!lastOffers.Contains(item)) //and checkbox checked
            //{
            //   itemCount++;
            //   resultLbox.Items.Add($"{itemCount}) {item.nadpis} for {item.cena} - {item.lokace} / {item.psc} - {item.datum} - {item.viewed} x - {item.url}"); //url show on click
            //}
            //}
         }
         DB_Access.InsertNewOffers(BazosOffers.actualCategoryNameURL);
         lbNewOffers.Text = $"new offers: {DB_Access.newOffersList.Count}";
         lbUpdatedCount.Text = $"updated: {DB_Access.updatedList.Count}";
         lbDeletedCount.Text = $"deleted: {DB_Access.deletedList.Count}";
      }

      private void AddItemsToResultLbox(int itemCount, BazosOffers item)
      {
         resultLbox.Items.Add($"{itemCount}) {item.nadpis} for {item.cena} - {item.lokace} / {item.psc} - {item.datum} - {item.viewed} x - {item.url}");
      }

      private void AddItemsToResultLbox(List<BazosOffers> itemList, int itemCount = 1)
      {
         foreach (BazosOffers item in itemList)
         {
            resultLbox.Items.Add($"{itemCount}) {item.nadpis} for {item.cena} - {item.lokace} / {item.psc} - {item.datum} - {item.viewed} x - {item.url}");
            itemCount++;
         }     
      }

      private void AddItemsResultLbox()
      {

      }

      private void ResetList()
      {
         BazosOffers.ListBazosOffers.Clear();
         resultLbox.Items.Clear();
      }

      private void resultLbox_SelectedIndexChanged(object sender, EventArgs e)
      {
         offerLbox.Items.Clear();
         BazosOffers item = BazosOffers.ListBazosOffers.FirstOrDefault(of => of.nadpis == Regex.Replace(resultLbox.SelectedItem.ToString().Split(')', 2)[1], "for", ";").Split(";")[0].Trim());
         foreach (var prop in item.GetType().GetProperties())
         {
           offerLbox.Items.Add($"{prop.Name}: {prop.GetValue(item, null)}");
         }
         //foreach (var p in item.GetType().GetProperties().Where(p => !p.GetGetMethod().GetParameters().Any()))
         //{
         //   offerLbox.Items.Add(p.GetValue(item, null));
         //}
         //offerLbox.Items.Add($"{item.nadpis}\n{item.cena}\n{item.url}\n{item.lokace}\n{item.psc}\n{item.datum}\n{item.viewed} x");
         //offerLbox.Items.Add(
      }

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

      private void resultLbox_DoubleClick(object sender, EventArgs e)
      {
         //Process.Start("chrome", BazosOffers.ListBazosOffers[resultLbox.SelectedIndex].url);

         string url = BazosOffers.ListBazosOffers[resultLbox.SelectedIndex].url;

         Process process = new Process();
         process.StartInfo = new ProcessStartInfo()
         {
            FileName = Environment.GetEnvironmentVariable("ProgramFiles") + @"\Google\Chrome\Application\chrome.exe",
            Arguments = url
         };
         process.Start();
         //startInfo.FileName = "chrome.exe";
         //startInfo.Arguments = url;
         //Process.Start(startInfo);
      }

      private void btnSelectPanel_Click(object sender, EventArgs e)
      {
         //ChangePanel(Settings.DictPanelNameValue[cmbSelectPanel.SelectedItem.ToString()]);
         //DB_Access.InsertNewOffers("test");
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

      private void cmbSelectFilter_SelectedIndexChanged(object sender, EventArgs e)
      {
         foreach (Control panel in Controls.OfType<Panel>())//.Where(p => p.Name == FilterSet.DictFilterPanelNames[cmbSelectFilter.Text]))
         {
            if (panel.Tag == "filterPanel" && panel.Name == Settings.DictFilterPanelsNames[cmbSelectFilter.Text])
            {
               panel.Show();
            }
         }
         foreach (Control panel in Controls.OfType<Panel>().Where(p => p.Name != Settings.DictFilterPanelsNames[cmbSelectFilter.Text]))
         {
            if (panel.Tag == "filterPanel")
            {
               panel.Hide();
            }
         }
      }

      private void btnAddToFilter_Click(object sender, EventArgs e)
      {
         //search by popis later (cm)
         int MaxCena;
         foreach (Control textbox in Controls.OfType<TextBox>())//.Where(p => p.Tag == "FilterSet"))
         {
            if (textbox.Text == string.Empty && textbox.Tag == "FilterSet")
            {
               return;
            }
         }

         if (int.TryParse(tbFilterMaxCena.Text, out MaxCena)) // dohodou, ... ;
         {
            DB_Access.AddToFilter(NameOfFilter: tbFilterName.Text, UrlPage: tbFilterPageUrl.Text, Name: tbFilterNadpis.Text, MaxCena: MaxCena);
            string url = tbFilterPageUrl.Text;
            cmbSelectUrl.SelectedItem = url;
            if (!cmbAddFilter.Items.Contains(tbFilterName.Text))
            {
               cmbAddFilter.Items.Add(tbFilterName.Text);
            }
            if (Filters.Dict_URL_PAGE_FilterName.ContainsKey(url))
            {
               Filters.Dict_URL_PAGE_FilterName[url] += $";{tbFilterName.Text}";
            }
            else
            {
               Filters.Dict_URL_PAGE_FilterName.Add(url, tbFilterName.Text);
            }
            foreach (string key in Filters.Dict_URL_PAGE_FilterName.Keys)
            {
               if (!cmbSelectUrl.Items.Contains(key))
               {
                  cmbSelectUrl.Items.Add(key);
               }
            }
            //AddUrlPageFilterName();
            ClearFilterTextboxes();
         }
      }

      private void ClearFilterTextboxes()
      {
         foreach (Control textbox in Controls.OfType<TextBox>())//().Where(p => p.Tag == "FilterSet"))
         {
            if (textbox.Tag == "Filter")
            {
               (textbox as TextBox).Clear();
            }
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

      private void btnCreateFilterSet_Click(object sender, EventArgs e)
      {
         List<string> ListListboxFilter = new List<string>();
         List<string> ListListboxBlacklist = new List<string>();
         foreach (string item in lboxFilters.Items)
         {
            ListListboxFilter.Add(item);
         }
         foreach (string item in lboxBlacklistSet.Items)
         {
            ListListboxBlacklist.Add(item);
         }
         cmbSelectFilterSet.Items.Add(tbFiltersetName.Text);
         DB_Access.CreateFilterSet(tbFiltersetName.Text, ListListboxFilter, ListListboxBlacklist, cmbSelectUrl.Text);
         AddUrlPageFilterName();
         tbFiltersetName.Clear();
         lboxFilters.Items.Clear();
         lboxBlacklistSet.Items.Clear();
      }
         
      private void btnAddFilter_Click(object sender, EventArgs e)
      {
         if (cmbAddFilter.SelectedItem.ToString() != string.Empty && !lboxFilters.Items.Contains(cmbAddFilter.Text))
         {
            lboxFilters.Items.Add(cmbAddFilter.SelectedItem.ToString());
            cmbAddFilter.SelectedIndex = 0;
         }
      }

      private void btnAddBlacklist_Click(object sender, EventArgs e)
      {
         if (cmbAddBlacklist.SelectedItem.ToString() != string.Empty)
         {
            lboxBlacklistSet.Items.Add(cmbAddBlacklist.SelectedItem.ToString());
            cmbAddBlacklist.SelectedIndex = 0;
         }
      }

      private void cmbSelectFilterSet_SelectedIndexChanged(object sender, EventArgs e)
      {
         string cmbText = cmbSelectFilterSet.SelectedItem.ToString();
         if (cmbText != string.Empty && ((lboxFilters.Items.Count == 0 && lboxBlacklistSet.Items.Count == 0) || MessageBox.Show("Přemazat rozdělaný filter set?", "Delete", MessageBoxButtons.YesNo) == DialogResult.Yes))
         {
            List<string> setFilters = new List<string>();
            List<string> setBlacklists = new List<string>();
            foreach (FilterSet item in FilterSet.ListFilterSet)
            {
               if (item.SetName == cmbSelectFilterSet.Text)
               {
                  setFilters.AddRange(item.SetFilters.Split(";"));
                  setBlacklists.AddRange(item.SetBlacklists.Split(";"));
               }
            }
            tbFiltersetName.Text = cmbSelectFilterSet.Text;
            //string[] setFilters = FilterSet.ListFilterSet.GetRange(0, FilterSet.ListFilterSet.Count).;
            //string[] setBlacklists;
            //DB_Access.LoadFilterSetToBoxes(cmbText, out setItems, out blacklistItems);
            lboxFilters.Items.Clear();
            lboxBlacklistSet.Items.Clear();
            //lboxFilterSet.Items.AddRange(setItems);
            //lboxBlacklistSet.Items.AddRange(blacklistItems);
            lboxFilters.Items.AddRange(setFilters.ToArray());
            lboxBlacklistSet.Items.AddRange(setBlacklists.ToArray());
         }
      }

      string cmbSelectUrl_lastSelectedItem;
      private void cmbSelectUrl_SelectedIndexChanged(object sender, EventArgs e)
      {
         string selectedUrl = cmbSelectUrl.Text;
         if (selectedUrl == cmbSelectUrl_lastSelectedItem || (lboxFilters.Items.Count == 0 && lboxBlacklistSet.Items.Count == 0) || MessageBox.Show("Přemazat rozdělaný filter set?", "Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
         {
            cmbAddFilter.Items.Clear();
            cmbAddBlacklist.Items.Clear();
            cmbSelectFilterSet.Items.Clear();
            lboxFilters.Items.Clear();
            lboxBlacklistSet.Items.Clear();
            cmbAddFilter.Items.AddRange(Filters.Dict_URL_PAGE_FilterName[selectedUrl].Split(';'));
            if (FilterSet.Dict_URL_PAGE_FilterSetName.ContainsKey(selectedUrl))
            {
               cmbSelectFilterSet.Items.AddRange(FilterSet.Dict_URL_PAGE_FilterSetName[selectedUrl].Split(';'));
               //cmbAddFilter.Items.AddRange(DB_Access.AddFilters(selectedUrl));
               //cmbAddBlacklist.Items.AddRange(DB_Access.AddBLacklists(selectedUrl));
            }
         }
         cmbSelectUrl_lastSelectedItem = cmbSelectUrl.Text;
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

      private void AddOffersToResultLbox(List<BazosOffers> offersList, string name)
      {
         int itemCount = 1;
         foreach (BazosOffers item in offersList)
         {
            resultLbox.Items.Add($"{itemCount}) {item.nadpis} for {item.cena} - {item.lokace} / {item.psc} - {item.datum} - {item.viewed} x - {item.url}");
            itemCount++;
         }
      }

      private void lboxBlacklistSet_SelectedIndexChanged(object sender, EventArgs e)
      {
         lboxSetDetails.Items.Clear();
         //Filters aktualFilter = Filters.ListFilters.FirstOrDefault(p => p.NameOfFilter == lboxFilters.SelectedItem.ToString());
         //lboxSetDetails.Items.Add("page url: " + aktualFilter.PageUrl);
         //lboxSetDetails.Items.Add("name of filter: " + aktualFilter.NameOfFilter);
         //lboxSetDetails.Items.Add("search name: " + aktualFilter.Name);
         //lboxSetDetails.Items.Add("max cena: " + aktualFilter.MaxCena);
         //tbFilterName.Text = aktualFilter.NameOfFilter;
         //tbFilterNadpis.Text = aktualFilter.Name;
         //tbFilterPageUrl.Text = aktualFilter.PageUrl;
         //tbFilterMaxCena.Text = aktualFilter.MaxCena.ToString();
      }

      private void lboxFilterSet_SelectedIndexChanged(object sender, EventArgs e)
      {
         lboxSetDetails.Items.Clear();
         if (lboxFilters.Items.Count > 0 && lboxFilters.SelectedItem != null)
         {
            Filters aktualFilter = Filters.ListFilters.FirstOrDefault(p => p.NameOfFilter == lboxFilters.SelectedItem.ToString());
            lboxSetDetails.Items.Add("page url: " + aktualFilter.PageUrl);
            lboxSetDetails.Items.Add("name of filter: " + aktualFilter.NameOfFilter);
            lboxSetDetails.Items.Add("search name: " + aktualFilter.Name);
            lboxSetDetails.Items.Add("max cena: " + aktualFilter.MaxCena);
            tbFilterName.Text = aktualFilter.NameOfFilter;
            tbFilterNadpis.Text = aktualFilter.Name;
            tbFilterPageUrl.Text = aktualFilter.PageUrl;
            tbFilterMaxCena.Text = aktualFilter.MaxCena.ToString();
         }
      }

      private void lboxFilters_KeyDown(object sender, KeyEventArgs e)
      {
         Keys key = e.KeyCode;
         if (key == Keys.Delete)
         {
            lboxFilters.Items.Remove(lboxFilters.SelectedItem);
            //if (lboxFilters.Items.Count > 0)
            //{
            //   lboxFilters.SelectedIndex = 0;
            //}
         }
      }

      private void cmbAddFilter_SelectedIndexChanged(object sender, EventArgs e)
      {
         lboxSetDetails.Items.Clear();
         Filters aktualFilter = Filters.ListFilters.FirstOrDefault(p => p.NameOfFilter == cmbAddFilter.SelectedItem.ToString());
         lboxSetDetails.Items.Add("page url: " + aktualFilter.PageUrl);
         lboxSetDetails.Items.Add("name of filter: " + aktualFilter.NameOfFilter);
         lboxSetDetails.Items.Add("search name: " + aktualFilter.Name);
         lboxSetDetails.Items.Add("max cena: " + aktualFilter.MaxCena);
         tbFilterName.Text = aktualFilter.NameOfFilter;
         tbFilterNadpis.Text = aktualFilter.Name;
         tbFilterPageUrl.Text = aktualFilter.PageUrl;
         tbFilterMaxCena.Text = aktualFilter.MaxCena.ToString();
      }

      private void btnClearFilterTextboxes_Click(object sender, EventArgs e)
      {
         if (ListBoxesNotClear() && MessageBox.Show("Vymazat rozdělaný filter set?", "Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
         {
            ClearFilterTextboxes();
         }
      }

      private void cmbAddFilter_KeyDown(object sender, KeyEventArgs e)
      {
         if (e.KeyCode == Keys.Delete && MessageBox.Show("Opravdu odstranit filter?", "Odstranit filter?", MessageBoxButtons.YesNo) == DialogResult.Yes)
         {
            Filters.RemoveFilter(cmbAddFilter.Text);
            cmbAddFilter.Items.Remove(cmbAddFilter.SelectedItem);
         }
      }

      private void btnApplyQuickFilter_Click(object sender, EventArgs e)
      {
         QuickFilter.QuickFilterList.Clear();
         string[] filterSplit = new string[] { tbQuickFilter.Text };
         if (tbQuickFilter.Text.Contains(";"))
         {
            filterSplit = tbQuickFilter.Text.Split(";");
         }
         foreach (string item in filterSplit)
         {
            string[] ndpsSplit = item.Contains("<") || item.Contains(">") ? item.Split(new char[] { '<', '>' }, StringSplitOptions.RemoveEmptyEntries) : new string[0];
            string nadpis = ndpsSplit.Length == 0 ? Regex.Match(item, @"[0-9]+|[A-Z]+", RegexOptions.IgnoreCase).ToString() : Regex.Match(ndpsSplit[0], @"[0-9]+|[A-Z]+").ToString(); //nadpis
            string popis = string.Empty;
            int maxCena = 0;
            bool fullNadpisName = item.Contains("!") ? true : false; //full nadpis name
            if (item.Contains("?")) //popis
            {
               popis = item.Replace("?", string.Empty).Replace("!", string.Empty);
               popis = item.Contains("<") ? popis.Split("<")[0] : popis;
            }
            if (item.Contains("<")) //maxcena
            {
               //mincena later, ... ;
               int cena = int.Parse(Regex.Match(item.Split("<")[1], @"\d+").ToString());
               maxCena = item.Contains("=") ? cena : cena - 1;
            }
            if (nadpis != string.Empty) //add quick filter to list
            {
               QuickFilter qf = new QuickFilter(nadpis, popis, maxCena, fullNadpisName);
            }
         }
         ApplyQuickFilterChanges();
      }

      /// <summary>
      /// 
      /// </summary>
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
               if (qfList.Any(qf => item.nadpis.Contains(qf.nadpis, StringComparison.OrdinalIgnoreCase))) //nadpis is matched in quickfilter
               {
                  QuickFilter quickfilter = qfList.FirstOrDefault(qf => item.nadpis.Contains(qf.nadpis, StringComparison.OrdinalIgnoreCase));

                  if (!quickfilter.fullNadpisName || Regex.IsMatch(item.nadpis, @"\s" + quickfilter.nadpis+ @"\s") && quickfilter.maxCena == 0 || int.Parse(item.cena) <= quickfilter.maxCena) //test if item is matched to max cena
                  {
                     AddItemsToResultLbox(itemCount, item);
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

   }
} 