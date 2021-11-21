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
//.net maui

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
         lboxFilters.MouseUp += new MouseEventHandler(List_RightClick);
      }

      private void Form1_Load(object sender, EventArgs e)
      {
         cmbSelectOffers.Items.AddRange(DB_Access.ListActualOffersCategoryURLInDB().ToArray());
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
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void btnGetBazos_Click(object sender, EventArgs e)
      {
         ResetList(); //reset list of offers and result lisbox
         BazosOffers.actualCategoryNameURL = tbSearchUrl.Text != string.Empty ? tbSearchUrl.Text : cmbSelectOffers.Text; //actual category url id
         Download.DownloadAllFromCategory(tbSearchUrl.Text, cboxDownOnlyLast.Checked); //await download
         AddItemsToResultLbox(BazosOffers.ListBazosOffers); //result lisbox
         //labels:
         lbAllOffers.Text = $"all offers: {BazosOffers.ListBazosOffers.Count}";
         lbNewOffers.Text = $"new offers: {DB_Access.newOffersList.Count}";
         lbUpdatedCount.Text = $"updated: {DB_Access.updatedList.Count}";
         lbDeletedCount.Text = $"deleted: {DB_Access.deletedList.Count}";
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
         foreach (BazosOffers item in offersList)
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
                  if ((!quickfilter.fullNadpisName || item.nadpis.Split(' ').Contains(quickfilter.nadpis)) && (quickfilter.maxCena == 0 || (cena > 0 && cena <= quickfilter.maxCena) || cena == 0) && !QuickFilter.Blacklist.Any(nadpis => item.nadpis.Contains(nadpis, StringComparison.OrdinalIgnoreCase))) //test if item is matched to max cena
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

      private void btnCreateQuickFilter_Click(object sender, EventArgs e)
      {
         QuickFilter.GetQuickFiltersFromTextbox(tbQuickFilter.Text);
         QuickFilter.SaveQuickFilterToDB(tbQuickFilter.Text, tbSearchUrl.Text);
      }

      #endregion

      #region Filter Panels

      /// <summary>
      /// buttons
      /// </summary>
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

      private void btnClearFilterTextboxes_Click(object sender, EventArgs e)
      {
         if (ListBoxesNotClear() && MessageBox.Show("Vymazat rozdělaný filter set?", "Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
         {
            ClearFilterTextboxes();
         }
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


      /// <summary>
      /// comboboxes
      /// </summary>
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

      private void cmbAddFilter_KeyDown(object sender, KeyEventArgs e)
      {
         if (e.KeyCode == Keys.Delete && MessageBox.Show("Opravdu odstranit filter?", "Odstranit filter?", MessageBoxButtons.YesNo) == DialogResult.Yes)
         {
            Filters.RemoveFilter(cmbAddFilter.Text);
            cmbAddFilter.Items.Remove(cmbAddFilter.SelectedItem);
         }
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


      /// <summary>
      /// listboxes
      /// </summary>
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

      #endregion

   }
}