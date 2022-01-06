using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Text.RegularExpressions;

namespace BazosBot
{
   class BlacklistSet
   {

      //public static List<QuickFilter> QuickFilterList = new List<QuickFilter>();
      public static List<string> BlacklistSetTextList = new List<string>(); 
      public static Dictionary<string, List<string>> DictActualBlacklistSet = DictActualBlacklistSetInDB();

      public string name { get; set; }
      public string blacklistString { get; set; }
      public BlacklistSet(string name, string blacklistString)
      {
         this.name = name;
         this.blacklistString = blacklistString;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="blacklistSetText"></param>
      /// <param name="categoryURL"></param>
      /// <param name="name"></param>
      public static void SaveBlacklistSetToDB(string blacklistSetText, string categoryURL, out string name)
      {
         string[] filterSplit = blacklistSetText.Split(";");
         name = filterSplit[0].Contains(":") ? $"{filterSplit[0].Split(":")[0]}" : $"blSet{DB_Access.FreeID(categoryURL, "BazosQuickFilter", "blSet", "Name")}";
         if (filterSplit[0].Contains(":"))
         {
            blacklistSetText = blacklistSetText.Split(":")[1].Trim();
         }
         DictActualBlacklistSet[categoryURL].Add($"{name}: {blacklistSetText}");
         string cmd = !BlacklistSetNameInCategoryExist(name, categoryURL) ?
               $"INSERT INTO BazosBlackListSet (Name, FilterString, CategoryURL) VALUES ('{name}', '{blacklistSetText}', '{categoryURL}');" : $"UPDATE BazosQuickFilter SET FilterString = '{blacklistSetText}' WHERE Name = '{name}' AND CategoryURL = '{categoryURL}';";
         DB_Access.ExecuteNonQuery(cmd);
      }

      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      public static Dictionary<string, List<string>> DictActualBlacklistSetInDB()
      {
         Dictionary<string, List<string>> DictActualQFilters = new Dictionary<string, List<string>>();
         List<string> urlCategory = new List<string>();
         SqlConnection conn = new SqlConnection(Settings.DBconnString);
         string cmdText = $"SELECT DISTINCT CategoryURL FROM BazosBlackListSet;";
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
           // DictActualQFilters.Add(url, ListActuaBlackListSetInDB(url));
         }
         return DictActualQFilters;
      }
      
      /// <summary>
      /// Quick filter name exist in database table.
      /// </summary>
      /// <returns></returns>
      private static bool BlacklistSetNameInCategoryExist(string name, string categoryURL)
      {
         SqlConnection connection = new SqlConnection(Settings.DBconnString);
         string cmdText = $"SELECT * FROM BazosBlackListSet WHERE FilterName = '{name}' AND CategoryURL = '{categoryURL}';";
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
      //public static List<string> ListActualBlacklistSetInDB(string categoryURL)
      //{
      //   List<string> listBlacklistSet = new List<string>();
      //   SqlConnection conn = new SqlConnection(Settings.DBconnString);
      //   string cmdText = $"SELECT Name, FilterString FROM BazosBlacklistSet WHERE CategoryURL = '{categoryURL}';";
      //   SqlCommand cmd = new SqlCommand(cmdText, conn);
      //   conn.Open();
      //   SqlDataReader reader = cmd.ExecuteReader();
      //   while (reader.Read())
      //   {
      //      string filterName = (string)reader["FilterName"];
      //      string filterString = (string)reader["FilterString"];
      //      //QuickFilter qf;
      //      GetBlacklistSetFromTextbox(filterString);//, out qf); //and add it to list
      //      listQuickFilters.Add($"{filterName}: {filterString}");
      //   }
      //   conn.Close();
      //   QuickFilterTextList = listQuickFilters;
      //   return listQuickFilters;
      //}


      private static void GetBlacklistSetFromTextbox(string tbBlacklistSetText)
      {

      }

   }
}