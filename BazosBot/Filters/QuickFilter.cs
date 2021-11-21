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
      public static List<QuickFilter> QuickFilterList = new List<QuickFilter>();
      public static List<string> Blacklist = new List<string>();
      public static string Name;
      public string nadpis { get; set; }
      public string popis { get; set; }
      public int maxCena { get; set; }
      public bool fullNadpisName { get; set; }
      public QuickFilter(string nadpis, string popis, int maxCena, bool fullNadpisName)
      {
         this.nadpis = nadpis;
         this.popis = popis;
         this.maxCena = maxCena;
         this.fullNadpisName = fullNadpisName;
         QuickFilterList.Add(this);
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="tbQuickFilterText"></param>
      public static void GetQuickFiltersFromTextbox(string tbQuickFilterText)
      {
         Name = "";
         QuickFilterList.Clear();
         List<string> blackListNadpisList = new List<string>();
         string[] filterSplit = new string[] { tbQuickFilterText };
         if (tbQuickFilterText.Contains(";"))
         {
            filterSplit = tbQuickFilterText.Split(";");
         }
         int index = 0;
         foreach (string item in filterSplit)
         {
            string[] ndpsSplit = item.Contains("<") || item.Contains(">") ? item.Split(new char[] { '<', '>' }, StringSplitOptions.RemoveEmptyEntries) : new string[0]; //split from max price
            string nadpis = ndpsSplit.Length == 0 ? Regex.Match(item, @"[0-9]+|[A-Z]+", RegexOptions.IgnoreCase).ToString() : Regex.Match(ndpsSplit[0], @"[0-9]+|[A-Z]+").ToString(); //nadpis
            string popis = string.Empty;
            int maxCena = 0;
            bool fullNadpisName = item.Contains("!") ? true : false; //full nadpis name
            if (item.Contains("."))
            {
               string it = item.Replace(".", string.Empty);
               blackListNadpisList.Add(it);
            }
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
            if (index == 0 && nadpis.Contains("name:"))
            {
               Name = nadpis.Replace("name:", string.Empty);
            }
            else if (nadpis != string.Empty && !blackListNadpisList.Contains(nadpis)) //add quick filter to list
            {
               QuickFilter qf = new QuickFilter(nadpis, popis, maxCena, fullNadpisName);
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
         string nadpis = string.Empty;
         string maxcena = string.Empty;
         string blacklistNadpis = string.Empty;
         foreach (QuickFilter quickFilter in QuickFilterList.ToList())
         {
            string addNadpis = quickFilter.fullNadpisName ? $"{quickFilter.nadpis}!" : quickFilter.nadpis;
            nadpis = nadpis == string.Empty ? addNadpis : $"{nadpis};{addNadpis}";
            maxcena = maxcena == string.Empty ? quickFilter.maxCena.ToString() : $"{maxcena};{quickFilter.maxCena}";
         }
         foreach (string bl in Blacklist)
         {
            blacklistNadpis = blacklistNadpis == string.Empty ? bl : $"{blacklistNadpis};{bl}";
         }
         Name = Name == string.Empty ? "qf" + HighestID() : Name; 
         string cmd = !QuickFilterNameExist() ? $"INSERT INTO BazosQuickFilter " +
               $"(FilterName, Nadpis, MAX_CENA, CategoryNameUrlID, BlackListNadpis) VALUES " +
               $"({Name}, {nadpis}, {maxcena}, {CategoryURL}, {blacklistNadpis});" : $"UPDATE BazosQuickFilter SET Nadpis = {nadpis}, MAX_CENA = {maxcena}, CategoryNameUrlID = {CategoryURL}, BlackListNadpis = {blacklistNadpis})";
         SqlCommand command = new SqlCommand(cmd, connection);
         connection.Open(); //open SQL server connection
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
         SqlConnection connection = new SqlConnection(Settings.DBconnString);
         string cmdText = "select yourcolumn from yourtable where id = (select max(id) from yourtable )";//$"SELECT * FROM BazosQuickFilter WHERE FilterName = '{Name}';";
         SqlCommand cmd = new SqlCommand(cmdText, connection);
         connection.Open();
         SqlDataReader reader = cmd.ExecuteReader();
         int ID = 1;
         while (reader.Read())
         {
            ID = (int)reader["Id"];
         }
         connection.Close();
         return ID;
      }

   }
}
