using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;
using System.IO;
//NORDDOCK.DK

namespace BazosBot
{
   class FilterSet
   {
      public static List<FilterSet> ListFilterSet = new List<FilterSet>();
      public string NameOfFilter { get; set; }
      public string PageUrl { get; set; }
      public string Name { get; set; }
      public int MaxCena { get; set; }
      public FilterSet(string NameOfFilter, string PageUrl, string Name, int MaxCena)
      {
         this.NameOfFilter = NameOfFilter;
         this.PageUrl = PageUrl;
         this.Name = Name;
         this.MaxCena = MaxCena;
         ListFilterSet.Add(this);
      }

      public static Dictionary<string, string> DictFilterPanelNames = new Dictionary<string, string>()
      {
         { "Filter Set", "filterSetPanel" },
         { "Filter Panel", "filterPanel" },
         { "Blacklist Panel", "blacklistPanel" }
      };
      
      public static Dictionary<string, string> Dict_URL_PAGE_FilterName = new Dictionary<string, string>();
      public static Dictionary<string, string> DictFilterNameValues = new Dictionary<string, string>();

      static readonly string connString = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\..\"))}Database.mdf;Integrated Security = True; Connect Timeout = 30";

      public static void InitDictionary_PageURL_FilterName()
      {
         SqlConnection connection = new SqlConnection(connString);
         string selectCmdText = $"SELECT DISTINCT URL_PAGE, NAME_OF_FILTER FROM BazosFilter;";
         SqlCommand cmd = new SqlCommand(selectCmdText, connection);
         connection.Open();
         SqlDataReader reader = cmd.ExecuteReader();
         string url = string.Empty;
         while (reader.Read()) //load level info
         {
            url = (string)reader["URL_PAGE"];
            if (Dict_URL_PAGE_FilterName.ContainsKey(url))
            {
               Dict_URL_PAGE_FilterName[url] += $";{(string)reader["NAME_OF_FILTER"]}";
            }
            else
            {
               Dict_URL_PAGE_FilterName.Add(url, $"{(string)reader["NAME_OF_FILTER"]}");
            }
         }
         connection.Close();
      }

      public static void InitFilterObjects()
      {
         SqlConnection connection = new SqlConnection(connString);
         string selectCmdText = $"SELECT * FROM BazosFilter;";
         SqlCommand cmd = new SqlCommand(selectCmdText, connection);
         connection.Open();
         SqlDataReader reader = cmd.ExecuteReader();
         string url = string.Empty;
         while (reader.Read()) //load level info
         {
            FilterSet f = new FilterSet((string)reader["NAME_OF_FILTER"], (string)reader["URL_PAGE"], (string)reader["NAME"], (int)reader["MAX_CENA"]);           
         }
         connection.Close();
      }


   }
}
