﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace BazosBot
{
   class QuickFilter
   {
      public static List<QuickFilter> QuickFilterList = new List<QuickFilter>(); //more quickfilters in line
      public static List<string> QuickFilterTextList = new List<string>(); //list of all quickfilter text in combobox
      public static List<string> Blacklist = new List<string>();
      public static List<string> BlacklistPopis = new List<string>();
      public static List<string> notFullPopisList = new List<string>();
      public static Dictionary<string, List<string>> DictActualQuickFilters = DictActualQuickFiltersInDB();
      public static int selectedIndex = 0;
      public string name;
      public string nadpis { get; set; }
      public string popis { get; set; }
      public int maxCena { get; set; }
      public int minCena { get; set; }
      public bool fullName { get; set; }
      public QuickFilter(string name, string nadpis, string popis, int minCena, int maxCena, bool fullName)
      {
         this.name = name;
         this.nadpis = nadpis;
         this.popis = popis;
         this.maxCena = maxCena;
         this.minCena = minCena;
         this.fullName = fullName;
         //QuickFilterList.Add(this);
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="tbQuickFilterText"></param>
      public static void GetQuickFiltersFromTextbox(string tbQuickFilterText)//, out QuickFilter qf)
      {
         QuickFilterList.Clear();
         List<string> blackListNadpisList = new List<string>();
         List<string> blackListPopisList = new List<string>();
         string[] filterSplit = tbQuickFilterText.Contains(";") ? tbQuickFilterText.Split(";") : new string[] { tbQuickFilterText };
         string name = filterSplit[0].Contains(":") ? filterSplit[0].Split(":")[0] : string.Empty;
         filterSplit[0] = filterSplit[0].Replace($"{name}:", string.Empty);
         foreach (string itm in filterSplit)
         {
            string item = itm.Trim();
            if (string.IsNullOrEmpty(item) && item.Length < 2)
            {
               continue;
            }
            //split name from max price:
            string[] ndpsSplit = item.Split(new char[] { '<', '>', '/', '.' });
            //nadpis:
            string nadpis = !string.IsNullOrEmpty(ndpsSplit[0]) ? Regex.Match(ndpsSplit[0], @"\w*\d*", RegexOptions.IgnoreCase).ToString().Trim() : string.Empty;
            nadpis = TextAdjust.RemoveDiacritics(nadpis);
            string popis = string.Empty;
            int maxCena = 0;
            int minCena = 0;
            //full nadpis name:
            bool fullName = item.Contains("!") ? true : false;
            //blacklist:
            if (item.Substring(0, 1).Contains(".") && !item.Substring(1, 1).Contains("?"))
            {
               string it = TextAdjust.RemoveDiacritics(item.Replace(".", string.Empty));
               blackListNadpisList.Add(it);
            }
            else if (item.Substring(0, 2) == ".?") //blacklist popis
            {
               string it = TextAdjust.RemoveDiacritics(item.Replace(".?", string.Empty).Replace("!", string.Empty));
               blackListPopisList.Add(it);
            }
            if (item.Substring(0, 1).Contains("/")) //blacklistSet
            {
               string blacklistSet = BlacklistSet.DictActualBlacklistSet[BazosOffers.actualCategoryURL].FirstOrDefault(p => p.Split(":")[0] == item.Remove(0, 1));
               if (string.IsNullOrEmpty(blacklistSet))
               {
                  continue;
               }
               string[] blacklistSetSplit = blacklistSet.Split(":")[1].Split(";");
               foreach (string i in blacklistSetSplit)
               {
                  if (string.IsNullOrWhiteSpace(i) || i == string.Empty)
                  {
                     continue;
                  }
                  string it = TextAdjust.RemoveDiacritics(i.Replace(".", string.Empty)).Trim();
                  blackListNadpisList.Add(it);
               }
            }
            //popis:
            if (item.Substring(0, 1).Contains("?"))
            {
               popis = item.Replace("?", string.Empty).Replace("!", string.Empty);
               popis = item.Contains("<") || item.Contains(">") ? popis.Split(new char[] { '>', '<' }, StringSplitOptions.RemoveEmptyEntries)[0] : popis;
               popis = TextAdjust.RemoveDiacritics(popis);
            }
            //maxcena:
            if (item.Contains("<"))
            {
               int cena = int.Parse(Regex.Match(item.Split("<")[1], @"\d+").ToString());
               maxCena = item.Contains("=") ? cena : cena - 1;
            }
            //mincena:
            if (item.Contains(">"))
            {
               int cena = int.Parse(Regex.Match(item.Split(">")[1], @"\d+").ToString());
               minCena = item.Contains("=") ? cena : cena + 1;
            }
            QuickFilter qf = new QuickFilter(name, nadpis, popis, minCena, maxCena, fullName);
            if (nadpis != string.Empty || popis != string.Empty && !blackListNadpisList.Contains(nadpis) && !QuickFilterList.Contains(qf)) //add quick filter to list
            {
               QuickFilterList.Add(qf);
            }
         }
         Blacklist = blackListNadpisList;
         BlacklistPopis = blackListPopisList;
         //qf = null;
      }

      /// <summary>
      /// Save new quick filter to database table.
      /// </summary>
      /// <param name="QuickfilterTextboxText"></param>
      public static void SaveQuickFilterToDB(QuickFilter qf, string quickfilterTextboxText, string categoryURL)
      {
         if (string.IsNullOrEmpty(qf.name))
         {
            qf.name = $"qf{DB_Access.FreeID(categoryURL, "BazosQuickFilter", "qf")}";
         }
         else
         {
            quickfilterTextboxText = quickfilterTextboxText.Split(":")[1].Trim();
         }
         string cmd = !QuickFilterNameInCategoryExist(qf, categoryURL) ?
               $"INSERT INTO BazosQuickFilter (FilterName, FilterString, CategoryURL) VALUES ('{qf.name}', '{quickfilterTextboxText}', '{categoryURL}');" : $"UPDATE BazosQuickFilter SET FilterString = '{quickfilterTextboxText}' WHERE FilterName = '{qf.name}' AND CategoryURL = '{categoryURL}';";
         DB_Access.ExecuteNonQuery(cmd);
      }

      /// <summary>
      /// Save new quick filter to database table.
      /// </summary>
      /// <param name="QuickfilterTextboxText"></param>
      public static void SaveQuickFilterToDB(string quickfilterTextboxText, string categoryURL, out string name)
      {
         string[] filterSplit = quickfilterTextboxText.Split(";");
         name = filterSplit[0].Contains(":") ? $"{filterSplit[0].Split(":")[0]}" : $"qf{DB_Access.FreeID(categoryURL, "BazosQuickFilter", "qf")}";
         if (filterSplit[0].Contains(":"))
         {
            quickfilterTextboxText = quickfilterTextboxText.Split(":")[1].Trim();
         }
         DictActualQuickFilters[categoryURL].Add($"{name}: {quickfilterTextboxText}");
         string cmd = !QuickFilterNameInCategoryExist(name, categoryURL) ?
               $"INSERT INTO BazosQuickFilter (FilterName, FilterString, CategoryURL) VALUES ('{name}', '{quickfilterTextboxText}', '{categoryURL}');" : $"UPDATE BazosQuickFilter SET FilterString = '{quickfilterTextboxText}' WHERE FilterName = '{name}' AND CategoryURL = '{categoryURL}';";
         DB_Access.ExecuteNonQuery(cmd);
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="quickfilterText"></param>
      /// <param name="categoryURL"></param>
      /// <returns></returns>
      public static string GetQuickFilterName(string quickfilterText, string categoryURL)
      {
         string[] filterSplit = quickfilterText.Split(";");
         return filterSplit[0].Contains(":") ? $"{filterSplit[0].Split(":")[0]}" : $"qf{DB_Access.FreeID(categoryURL, "BazosQuickFilter", "qf")}";
      }

      /// <summary>
      /// Quick filter name exist in database table.
      /// </summary>
      /// <returns></returns>
      private static bool QuickFilterNameInCategoryExist(QuickFilter qf, string categoryURL)
      {
         SqlConnection connection = new SqlConnection(Settings.DBconnString);
         string cmdText = $"SELECT * FROM BazosQuickFilter WHERE FilterName = '{qf.name}' AND CategoryURL = '{categoryURL}';";
         SqlCommand cmd = new SqlCommand(cmdText, connection);
         connection.Open();
         SqlDataReader reader = cmd.ExecuteReader();
         while (reader.Read())
         {
            connection.Close();
            return true;
         }
         connection.Close();
         return false;
      }

      /// <summary>
      /// Quick filter name exist in database table.
      /// </summary>
      /// <returns></returns>
      private static bool QuickFilterNameInCategoryExist(string name, string categoryURL)
      {
         SqlConnection connection = new SqlConnection(Settings.DBconnString);
         string cmdText = $"SELECT * FROM BazosQuickFilter WHERE FilterName = '{name}' AND CategoryURL = '{categoryURL}';";
         SqlCommand cmd = new SqlCommand(cmdText, connection);
         connection.Open();
         SqlDataReader reader = cmd.ExecuteReader();
         while (reader.Read())
         {
            connection.Close();
            return true;
         }
         connection.Close();
         return false;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      public static Dictionary<string, List<string>> DictActualQuickFiltersInDB()
      {
         Dictionary<string, List<string>> DictActualQFilters = new Dictionary<string, List<string>>();
         List<string> urlCategory = new List<string>();
         SqlConnection conn = new SqlConnection(Settings.DBconnString);
         string cmdText = $"SELECT DISTINCT CategoryURL FROM BazosOffers;";
         SqlCommand cmd = new SqlCommand(cmdText, conn);
         conn.Open();
         SqlDataReader reader = cmd.ExecuteReader();
         while (reader.Read())
         {
            urlCategory.Add((string)reader["CategoryURL"]);
         }
         conn.Close();
         foreach (string url in urlCategory)
         {
            DictActualQFilters.Add(url, ListActualQuickFiltersInDB(url));
         }
         return DictActualQFilters;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="actualCategoryUrl"></param>
      public static List<string> ListActualQuickFiltersInDB(string actualCategoryUrl)
      {
         List<string> listQuickFilters = new List<string>();
         SqlConnection conn = new SqlConnection(Settings.DBconnString);
         string cmdText = $"SELECT FilterName, FilterString FROM BazosQuickFilter WHERE CategoryURL = '{actualCategoryUrl}';";
         SqlCommand cmd = new SqlCommand(cmdText, conn);
         conn.Open();
         SqlDataReader reader = cmd.ExecuteReader();
         while (reader.Read())
         {
            string filterName = (string)reader["FilterName"];
            string filterString = (string)reader["FilterString"];
            //QuickFilter qf;
            //GetQuickFiltersFromTextbox(filterString);//, out qf); //and add it to list
            listQuickFilters.Add($"{filterName}: {filterString}");
         }
         conn.Close();
         QuickFilterTextList = listQuickFilters;
         return listQuickFilters;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="index">Selected index from combobox.</param>
      public static void DeleteQuickFilterFromDB(string itemText, string categoryURL)
      {
         QuickFilterTextList.Remove(itemText);
         DictActualQuickFilters[categoryURL].Remove(itemText);
         string[] filterSplit = itemText.Split(";");
         string name = filterSplit[0].Contains(":") ? filterSplit[0].Split(":")[0] : string.Empty;
         SqlConnection connection = new SqlConnection(Settings.DBconnString);
         string cmdText = $"DELETE FROM BazosQuickFilter WHERE FilterName = @filterName AND CategoryURL = @categoryURL;";
         SqlCommand cmd = new SqlCommand(cmdText, connection);
         cmd.Parameters.AddWithValue("@filterName", name);
         cmd.Parameters.AddWithValue("@categoryURL", categoryURL);
         connection.Open();
         cmd.ExecuteNonQuery();
         connection.Close();
      }

      /// <summary>
      /// 
      /// </summary>
      public static void GetQuickFilterObjectByName()
      {

      }
   }
}