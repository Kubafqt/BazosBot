using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;
using System.IO;

namespace BazosBot
{
   class FilterSet
   {
      static readonly string connString = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\..\"))}Database.mdf;Integrated Security = True; Connect Timeout = 30";

      public static Dictionary<string, string> Dict_URL_PAGE_FilterSetName = new Dictionary<string, string>();
      public static List<FilterSet> ListFilterSet = new List<FilterSet>();
      public string SetName { get; set; }
      public string SetFilters { get; set; }
      public string SetBlacklists { get; set; }
      public string PageUrl { get; set; }
      public FilterSet(string SetName, string SetFilters, string SetBlacklists, string PageUrl)
      {
         this.SetName = SetName;
         this.SetFilters = SetFilters;
         this.SetBlacklists = SetBlacklists;
         this.PageUrl = PageUrl;
         ListFilterSet.Add(this);
      }

      /// <summary>
      /// 
      /// </summary>
      public static void InitFilterSetObjects()
      {
         ListFilterSet.Clear();
         SqlConnection connection = new SqlConnection(connString);
         string selectCmdText = $"SELECT * FROM BazosFilterSet;";
         SqlCommand cmd = new SqlCommand(selectCmdText, connection);
         connection.Open();
         SqlDataReader reader = cmd.ExecuteReader();
         string url = string.Empty;
         while (reader.Read()) //load level info
         {
            FilterSet f = new FilterSet((string)reader["SetName"], (string)reader["SetFilters"], (string)reader["SetBlacklists"], (string)reader["URL_PAGE"]);
         }
         connection.Close();
      }

      /// <summary>
      /// 
      /// </summary>
      public static void InitFilterSetDictionary()
      {
         Dict_URL_PAGE_FilterSetName.Clear();
         SqlConnection connection = new SqlConnection(connString);
         string selectCmdText = $"SELECT URL_PAGE, SetName FROM BazosFilterSet;";
         SqlCommand cmd = new SqlCommand(selectCmdText, connection);
         connection.Open();
         SqlDataReader reader = cmd.ExecuteReader();
         string url = string.Empty;
         while (reader.Read()) //load level info
         {
            url = (string)reader["URL_PAGE"];
            if (Dict_URL_PAGE_FilterSetName.ContainsKey(url))
            {
               Dict_URL_PAGE_FilterSetName[url] += $";{(string)reader["SetName"]}";
            }
            else
            {
               Dict_URL_PAGE_FilterSetName.Add(url, $"{(string)reader["SetName"]}");
            }
         }
         connection.Close();

      }



   }
}
