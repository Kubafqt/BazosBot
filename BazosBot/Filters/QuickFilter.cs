using System;
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
      public static int selectedIndex = 0;
      public static string Name;
      public string nadpis { get; set; }
      public string popis { get; set; }
      public int maxCena { get; set; }
      public bool FullNadpisName { get; set; }
      public QuickFilter(string nadpis, string popis, int maxCena, bool fullNadpisName)
      {
         this.nadpis = nadpis;
         this.popis = popis;
         this.maxCena = maxCena;
         this.FullNadpisName = fullNadpisName;
         //QuickFilterList.Add(this);
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="tbQuickFilterText"></param>
      public static void GetQuickFiltersFromTextbox(string tbQuickFilterText)
      {
         QuickFilterList.Clear();
         List<string> blackListNadpisList = new List<string>();
         string[] filterSplit = tbQuickFilterText.Contains(";") ? filterSplit = tbQuickFilterText.Split(";") : new string[] { tbQuickFilterText };
         Name = filterSplit[0].Contains(":") ? filterSplit[0].Split(":")[0] : string.Empty;
         filterSplit[0] = filterSplit[0].Replace($"{Name}:", string.Empty);
         foreach (string item in filterSplit)
         {
            //split name from max price:
            string[] ndpsSplit = item.Contains("<") || item.Contains(">") ? item.Split(new char[] { '<', '>' }, StringSplitOptions.RemoveEmptyEntries) : new string[0];
            //nadpis:
            string nadpis = ndpsSplit.Length == 0 ? Regex.Match(item, @"[0-9]+|[A-Z]+", RegexOptions.IgnoreCase).ToString() : Regex.Match(ndpsSplit[0], @"[0-9]+|[A-Z]+").ToString();
            nadpis = TextAdjust.RemoveDiacritics(nadpis);
            string popis = string.Empty;
            int maxCena = 0;
            //full nadpis name:
            bool fullNadpisName = item.Contains("!") ? true : false;
            //blacklist:
            if (item.Contains("."))
            {
               string it = TextAdjust.RemoveDiacritics(item.Replace(".", string.Empty));
               blackListNadpisList.Add(it);
            }
            //popis:
            if (item.Contains("?"))
            {
               popis = item.Replace("?", string.Empty).Replace("!", string.Empty);
               popis = item.Contains("<") ? popis.Split("<")[0] : popis;
               popis = TextAdjust.RemoveDiacritics(popis);
            }
            //maxcena:
            if (item.Contains("<"))
            {
               //mincena later, ... ;
               int cena = int.Parse(Regex.Match(item.Split("<")[1], @"\d+").ToString());
               maxCena = item.Contains("=") ? cena : cena - 1;
            }
            QuickFilter qf = new QuickFilter(nadpis, popis, maxCena, fullNadpisName);
            if (nadpis != string.Empty && !blackListNadpisList.Contains(nadpis) && !QuickFilterList.Contains(qf)) //add quick filter to list
            {
               QuickFilterList.Add(qf);
            }
         }
         Blacklist = blackListNadpisList;
      }

      /// <summary>
      /// Save new quick filter to database table.
      /// </summary>
      /// <param name="QuickfilterTextboxText"></param>
      public static void SaveQuickFilterToDB(string QuickfilterTextboxText, string CategoryURL)
      {
         SqlConnection connection = new SqlConnection(Settings.DBconnString);
         if (string.IsNullOrEmpty(Name))
         {
                Name = "qf" + HighestID();
         }
         else
         {
            QuickfilterTextboxText = QuickfilterTextboxText.Split(":")[1].Trim();
         }
         string cmd = !QuickFilterNameExist() ?
               $"INSERT INTO BazosQuickFilter (FilterName, FilterString, CategoryNameUrlID) VALUES ('{Name}', '{QuickfilterTextboxText}', '{CategoryURL}');" : $"UPDATE BazosQuickFilter SET FilterString = '{QuickfilterTextboxText}' WHERE FilterName = '{Name}';";
         SqlCommand command = new SqlCommand(cmd, connection);
         connection.Open();
         command.ExecuteNonQuery();
         connection.Close();
      }

      /// <summary>
      /// Quick filter name exist in database table.
      /// </summary>
      /// <returns></returns>
      private static bool QuickFilterNameExist()
      {
         SqlConnection connection = new SqlConnection(Settings.DBconnString);
         string cmdText = $"SELECT * FROM BazosQuickFilter WHERE FilterName = '{Name}';";
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
      /// Return highest ID from table.
      /// </summary>
      /// <returns></returns>
      private static int HighestID()
      {
         int ID = 1;
         if (DbContainsItem())
         {
            SqlConnection connection = new SqlConnection(Settings.DBconnString);
            string cmdText = "SELECT MAX(Id) FROM BazosQuickFilter";
            SqlCommand cmd = new SqlCommand(cmdText, connection);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
               ID = (int)reader["Id"];
            }
            connection.Close();
         }
         return ID;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      private static bool DbContainsItem()
      {
         SqlConnection connection = new SqlConnection(Settings.DBconnString);
         string cmdText = "SELECT * FROM BazosQuickFilter";
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
      /// <param name="actualCategoryUrl"></param>
      public static List<string> ListActualQuickFiltersInDB(string actualCategoryUrl)
      {
         List<string> listQuickFilters = new List<string>();
         SqlConnection conn = new SqlConnection(Settings.DBconnString);
         string cmdText = $"SELECT FilterName, FilterString FROM BazosQuickFilter WHERE CategoryNameUrlID = '{actualCategoryUrl}';";
         SqlCommand cmd = new SqlCommand(cmdText, conn);
         conn.Open();
         SqlDataReader reader = cmd.ExecuteReader();
         while (reader.Read())
         {
            string filterName = (string)reader["FilterName"];
            string filterString = (string)reader["FilterString"];
            GetQuickFiltersFromTextbox(filterString); //and add it to list
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
      public static void DeleteQuickFilter(int index)
      {
         QuickFilterTextList.RemoveAt(index);
         SqlConnection connection = new SqlConnection(Settings.DBconnString);
         string cmdText = $"DELETE FROM BazosQuickFilter WHERE FilterName = @filterName";
         SqlCommand cmd = new SqlCommand(cmdText, connection);
         cmd.Parameters.AddWithValue("@filterName", Name);
         connection.Open();
         cmd.ExecuteNonQuery();
         connection.Close();
      }


   }
}