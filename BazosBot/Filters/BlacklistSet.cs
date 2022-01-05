using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace BazosBot
{
   class BlacklistSet
   {
      public static Dictionary<string, List<string>> DictActualBlacklistSet = DictActualBlacklistSetInDB();

      public string name { get; set; }
      public string blacklistString { get; set; }
      public BlacklistSet(string name, string blacklistString)
      {
         this.name = name;
         this.blacklistString = blacklistString;
      }


      public static void SaveBlackListSetToDB(string blacklistSetText, string categoryURL, out string name)
      {
         string[] filterSplit = blacklistSetText.Split(";");
         name = filterSplit[0].Contains(":") ? $"{filterSplit[0].Split(":")[0]}" : $"qf{FreeID(categoryURL)}";
         if (filterSplit[0].Contains(":"))
         {
            blacklistSetText = blacklistSetText.Split(":")[1].Trim();
         }
         DictActualBlacklistSet[categoryURL].Add($"{name}: {blacklistSetText}");
         string cmd = !BlacklistSetNameInCategoryExist(name, categoryURL) ?
               $"INSERT INTO BazosQuickFilter (FilterName, FilterString, CategoryNameUrlID) VALUES ('{name}', '{blacklistSetText}', '{categoryURL}');" : $"UPDATE BazosQuickFilter SET FilterString = '{blacklistSetText}' WHERE FilterName = '{name}' AND CategoryNameUrlID = '{categoryURL}';";
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
         string cmdText = $"SELECT DISTINCT CategoryNameUrlID FROM BazosOffers;";
         SqlCommand cmd = new SqlCommand(cmdText, conn);
         conn.Open();
         SqlDataReader reader = cmd.ExecuteReader();
         while (reader.Read())
         {
            urlCategory.Add((string)reader["CategoryNameUrlID"]);
         }
         conn.Close();
         foreach (string url in urlCategory)
         {
            DictActualQFilters.Add(url, ListActualQuickFiltersInDB(url));
         }
         return DictActualQFilters;
      }

      /// <summary>
      /// Return highest ID from table.
      /// </summary>
      /// <returns></returns>
      private static int FreeID(string categoryUrl)
      {
         List<int> IDlist = new List<int>();
         if (DbContainsItem())
         {
            SqlConnection connection = new SqlConnection(Settings.DBconnString);
            string cmdText = $"SELECT FilterName FROM BazosQuickFilter Where CategoryNameUrlID = '{categoryUrl}';";
            SqlCommand cmd = new SqlCommand(cmdText, connection);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
               string filterName = (string)reader["FilterName"];
               if (filterName.Substring(0, 2).Contains("qf"))
               {
                  IDlist.Add(Convert.ToInt32(Regex.Match(filterName, @"\d+").ToString()));
               }
            }
            connection.Close();
         }
         return PossibleFreeID(IDlist);
      }

      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      private static int PossibleFreeID(List<int> IDlist)
      {
         int maxValue = IDlist.Count > 0 && IDlist.Max() >= 1 ? IDlist.Max() : 1;
         if (maxValue >= 1)
         {
            for (int i = 1; i <= maxValue + 1; i++)
            {
               if (!IDlist.Contains(i))
               {
                  return i;
               }
            }
         }
         return maxValue;
      }

      /// <summary>
      /// Quick filter name exist in database table.
      /// </summary>
      /// <returns></returns>
      private static bool BlacklistSetNameInCategoryExist(string name, string categoryURL)
      {
         SqlConnection connection = new SqlConnection(Settings.DBconnString);
         string cmdText = $"SELECT * FROM BazosBlackListSet WHERE FilterName = '{name}' AND CategoryNameUrl = '{categoryURL}';";
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


   }
}
