using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;
using System.IO;


namespace BazosBot
{
   class Filters
   {
      public static Dictionary<string, string> Dict_URL_PAGE_FilterName = new Dictionary<string, string>();
      public static Dictionary<string, string> DictFilterNameValues = new Dictionary<string, string>();

      public static List<Filters> ListFilters = new List<Filters>();
      public string NameOfFilter { get; set; }
      public string PageUrl { get; set; }
      public string Name { get; set; }
      public int MaxCena { get; set; }
      public Filters(string NameOfFilter, string PageUrl, string Name, int MaxCena)
      {
         this.NameOfFilter = NameOfFilter;
         this.PageUrl = PageUrl;
         this.Name = Name;
         this.MaxCena = MaxCena;
         ListFilters.Add(this);
      }

      public static void RemoveFilter(string filterName)
      {
         Filters filter = ListFilters.FirstOrDefault(f => f.NameOfFilter == filterName);
         ListFilters.Remove(filter);
         SqlConnection connection = new SqlConnection(Settings.DBconnString);
         string deleteCmdText = $"DELETE FROM BazosFilter WHERE NAME_OF_FILTER = '{filterName}';";
         SqlCommand cmd = new SqlCommand(deleteCmdText, connection);
         connection.Open();
         SqlDataReader reader = cmd.ExecuteReader();
         string url = string.Empty;
         while (reader.Read()) //load level info
         {
            Filters f = new Filters((string)reader["NAME_OF_FILTER"], (string)reader["URL_PAGE"], (string)reader["NAME"], (int)reader["MAX_CENA"]);
         }
         connection.Close();
      }

      /// <summary>
      /// 
      /// </summary>
      public static void InitFilterObjects()
      {
         ListFilters.Clear();
         SqlConnection connection = new SqlConnection(Settings.DBconnString);
         string selectCmdText = $"SELECT * FROM BazosFilter;";
         SqlCommand cmd = new SqlCommand(selectCmdText, connection);
         connection.Open();
         SqlDataReader reader = cmd.ExecuteReader();
         string url = string.Empty;
         while (reader.Read()) //load level info
         {
            Filters f = new Filters((string)reader["NAME_OF_FILTER"], (string)reader["URL_PAGE"], (string)reader["NAME"], (int)reader["MAX_CENA"]);
         }
         connection.Close();
      }

      /// <summary>
      /// 
      /// </summary>
      public static void InitDictionary_PageURL_FilterName()
      {
         Dict_URL_PAGE_FilterName.Clear();
         SqlConnection connection = new SqlConnection(Settings.DBconnString);
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

   }
}
