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
         AddPanelsToCombobox();
         LoadDefaultFilters();
      }

      private void LoadDefaultFilters()
      {
         cmbSelectUrl.Items.AddRange(DB_Access.LoadDefaultUrls());
      }

      /// <summary>
      /// 
      /// </summary>
      private void AddPanelsToCombobox()
      {
         foreach (Control control in Controls.OfType<Panel>())
         {
            if (control.Tag == "mainPanels")
            {
               cmbSelectPanel.Items.Add(Settings.DictPanelNameValue[control.Name]);
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
         Download.DownloadAllFromCategory(tbSearchUrl.Text);
         BazosOffers.actualCategoryNameURL = tbSearchUrl.Text;
         int itemCount = 0;
         foreach (BazosOffers nabidka in BazosOffers.ListBazosOffers.ToList())
         {
            itemCount++;
            resultLbox.Items.Add($"{itemCount}) {nabidka.nadpis} for {nabidka.cena} - {nabidka.lokace} / {nabidka.psc} - {nabidka.datum} - {nabidka.viewed} x - {nabidka.url}"); //url show on click
         }
      }

      private void resultLbox_SelectedIndexChanged(object sender, EventArgs e)
      {
         offerLbox.Items.Clear();
         BazosOffers nabidka = BazosOffers.ListBazosOffers[resultLbox.SelectedIndex];
         foreach (var prop in nabidka.GetType().GetProperties())
         {
           offerLbox.Items.Add($"{prop.Name}: {prop.GetValue(nabidka, null)}");
         }
         //foreach (var p in nabidka.GetType().GetProperties().Where(p => !p.GetGetMethod().GetParameters().Any()))
         //{
         //   offerLbox.Items.Add(p.GetValue(nabidka, null));
         //}
         //offerLbox.Items.Add($"{nabidka.nadpis}\n{nabidka.cena}\n{nabidka.url}\n{nabidka.lokace}\n{nabidka.psc}\n{nabidka.datum}\n{nabidka.viewed} x");
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
         ChangePanel(cmbSelectPanel.SelectedItem.ToString());
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
         var controlName = Settings.DictPanelNameValue.FirstOrDefault(x => x.Value == cmbSelectPanel.SelectedItem.ToString()).Key;
         Controls[controlName].Show();
         foreach (Control control in Controls.OfType<Panel>())
         {
            control.Hide();
         }
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

      }

      private void btnAddToFilter_Click(object sender, EventArgs e)
      {
         int MaxCena;
         if (int.TryParse(tbMaxCena.Text, out MaxCena))
         {
            DB_Access.AddToFilter(NameOfSet: tbFilterSetName.Text, UrlPage: tbFilterPageUrl.Text, Name: tbFilterName.Text, MaxCena: MaxCena);
         }

         foreach (Control textbox in Controls.OfType<TextBox>().Where(p => p.Tag == "FilterSet"))
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
         DB_Access.CreateFilterSet(tbFilterSetName.Text, ListListboxFilter, ListListboxBlacklist);
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
            string[] setItems;
            string[] blacklistItems;
            DB_Access.LoadFilterSetToBoxes(cmbText, out setItems, out blacklistItems);
            lboxFilterSet.Items.Clear();
            lboxBlacklistSet.Items.Clear();
            lboxFilterSet.Items.AddRange(setItems);
            lboxBlacklistSet.Items.AddRange(blacklistItems);
            tbFilterSetName.Text = cmbText;
         }
      }

      private void cmbSelectUrl_SelectedIndexChanged(object sender, EventArgs e)
      {

      }
   }
} 