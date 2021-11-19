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

namespace BazosBot
{
   public partial class Form1 : Form
   {
      string activePanel;
      public Form1()
      {
         InitializeComponent();
         resultLbox.HorizontalScrollbar = true;
         offerLbox.HorizontalScrollbar = true;
         activePanel = "main";
         LoadDefaultFilters();
         string[] cmbSelectOffersTypeString = new string[] { "all offers", "new offers", "updated", "deleted" };
         cmbSelectOffersType.Items.AddRange(cmbSelectOffersTypeString);
         //List<string> List_URL_PAGE = new List<string>();
         //DB_Access.InitializeFilters(out List_URL_PAGE);
         //cmbSelectUrl.Items.AddRange(List_URL_PAGE.ToArray());
         cmbSelectFilter.Items.Add("Filter Set");
         cmbSelectFilter.Items.Add("Filter Panel");
         cmbSelectFilter.Items.Add("Blacklist Panel");
         AddUrlPageFilterName();
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
         AddPanelsToCombobox();
      }

      private void AddUrlPageFilterName()
      {
         FilterSet.InitFilterObjects();
         FilterSet.InitDictionary_PageURL_FilterName();
         foreach (string key in FilterSet.Dict_URL_PAGE_FilterName.Keys)
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

      /// <summary>
      /// 
      /// </summary>
      private void AddPanelsToCombobox()
      {
         foreach (Control control in Controls.OfType<Panel>())
         {
            cmbSelectPanel.Items.Add(Settings.DictPanelNameValue.FirstOrDefault(c => c.Value == control.Name).Key); //by value 
            if (control.Tag == "mainPanels")
            {           
               control.Size = Settings.defaultPanelSize;
               control.Location = Settings.defaultPanelLocation;
            }
            if (control.Tag == "filterPanel")
            {
               control.Location = Settings.defaultPanelLocation;
               control.Size = Settings.defaultPanelSize;
            }
         }
         cmbSelectPanel.SelectedItem = "main panel";
      }

      /// <summary>
      /// 
      /// </summary>
      private void btnGetBazos_Click(object sender, EventArgs e)
      {
         ResetList();
         Download.DownloadAllFromCategory(tbSearchUrl.Text);
         //await download;
         BazosOffers.actualCategoryNameURL = tbSearchUrl.Text;
         int itemCount = 1;
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
            resultLbox.Items.Add($"{itemCount}) {item.nadpis} for {item.cena} - {item.lokace} / {item.psc} - {item.datum} - {item.viewed} x - {item.url}");
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

      private void ResetList()
      {
         BazosOffers.ListBazosOffers.Clear();
         resultLbox.Items.Clear();
      }

      private void resultLbox_SelectedIndexChanged(object sender, EventArgs e)
      {
         offerLbox.Items.Clear();
         BazosOffers item = BazosOffers.ListBazosOffers[resultLbox.SelectedIndex];
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
         string controlName = Settings.DictPanelNameValue[key];
         foreach (Control control in Controls.OfType<Panel>())
         {
            control.Hide();
         }
         Controls[controlName].Show();
         //foreach (Control control in Controls) //optimalize !
         //{
         //   if (control.Name == controlName)
         //   {
         //      control.Show();
         //      break;
         //   }
         //}
      }

      private void cmbSelectFilter_SelectedIndexChanged(object sender, EventArgs e)
      {
         foreach (Control panel in Controls.OfType<Panel>())//.Where(p => p.Name == FilterSet.DictFilterPanelNames[cmbSelectFilter.Text]))
         {
            if (panel.Tag == "filterPanel" && panel.Name == FilterSet.DictFilterPanelNames[cmbSelectFilter.Text])
            {
               panel.Show();
            }
         }
         foreach (Control panel in Controls.OfType<Panel>().Where(p => p.Name != FilterSet.DictFilterPanelNames[cmbSelectFilter.Text]))
         {
            if (panel.Tag == "filterPanel")
            {
               panel.Hide();
            }
         }
      }

      private void btnAddToFilter_Click(object sender, EventArgs e)
      {
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
            DB_Access.AddToFilter(NameOfSet: tbFilterSetName.Text, UrlPage: tbFilterPageUrl.Text, Name: tbFilterName.Text, MaxCena: MaxCena);
            if (!cmbAddFilter.Items.Contains(tbFilterSetName.Text))
            {
               cmbAddFilter.Items.Add(tbFilterSetName.Text);
            }
            AddUrlPageFilterName();
         }

         foreach (Control textbox in Controls.OfType<TextBox>())//().Where(p => p.Tag == "FilterSet"))
         {
            (textbox as TextBox).Clear();
         }
      }

      private void btnCreateFilterSet_Click(object sender, EventArgs e)
      {
         List<string> ListListboxFilter = new List<string>();
         List<string> ListListboxBlacklist = new List<string>();
         foreach (string item in lboxFilterSet.Items)
         {
            ListListboxFilter.Add(item);
         }
         foreach (string item in lboxBlacklistSet.Items)
         {
            ListListboxBlacklist.Add(item);
         }
         DB_Access.CreateFilterSet(tbFilterSetName.Text, ListListboxFilter, ListListboxBlacklist, cmbSelectUrl.Text);
      }
         

      private void btnAddFilter_Click(object sender, EventArgs e)
      {
         if (cmbAddFilter.SelectedItem.ToString() != string.Empty)
         {
            lboxFilterSet.Items.Add(cmbAddFilter.SelectedItem.ToString());
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
         string cmbText = cmbSelectFilter.SelectedItem.ToString();
         if (cmbText != string.Empty && ((lboxFilterSet.Items.Count == 0 && lboxBlacklistSet.Items.Count == 0) || MessageBox.Show("Přemazat rozdělaný filter set?", "Delete", MessageBoxButtons.YesNo) == DialogResult.Yes))
         {
#pragma warning disable CS0168 // Variable is declared but never used
            string[] setItems;
#pragma warning restore CS0168 // Variable is declared but never used
            string[] blacklistItems;
            //DB_Access.LoadFilterSetToBoxes(cmbText, out setItems, out blacklistItems);
            lboxFilterSet.Items.Clear();
            lboxBlacklistSet.Items.Clear();
            //lboxFilterSet.Items.AddRange(setItems);
            //lboxBlacklistSet.Items.AddRange(blacklistItems);
            tbFilterSetName.Text = cmbText;
         }
      }

      private void cmbSelectUrl_SelectedIndexChanged(object sender, EventArgs e)
      {
         string selectedUrl = cmbSelectUrl.Text;
         cmbAddFilter.Items.Clear();
         cmbAddFilter.Items.AddRange(FilterSet.Dict_URL_PAGE_FilterName[selectedUrl].Split(';'));
         //cmbAddFilter.Items.AddRange(DB_Access.AddFilters(selectedUrl));
         //cmbAddBlacklist.Items.AddRange(DB_Access.AddBLacklists(selectedUrl));
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

      }

      private void lboxFilterSet_SelectedIndexChanged(object sender, EventArgs e)
      {
         lboxSetDetails.Items.Clear();
         FilterSet aktualFilter = FilterSet.ListFilterSet.FirstOrDefault(p => p.NameOfFilter == lboxFilterSet.SelectedItem.ToString());
         lboxSetDetails.Items.Add("page url: " + aktualFilter.PageUrl);
         lboxSetDetails.Items.Add("name of filter: " + aktualFilter.NameOfFilter);
         lboxSetDetails.Items.Add("search name: " + aktualFilter.Name);
         lboxSetDetails.Items.Add("max cena: " + aktualFilter.MaxCena);
      }
   }
} 