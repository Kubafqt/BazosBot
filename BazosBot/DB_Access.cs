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
   class DB_Access
   {
      public static List<BazosOffers> newOffersList = new List<BazosOffers>();
      public static List<BazosOffers> updatedList = new List<BazosOffers>();
      public static List<BazosOffers> deletedList = new List<BazosOffers>();
      static readonly string connString = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\..\"))}Database.mdf;Integrated Security = True; Connect Timeout = 30";
      public static bool downloaded = false;

      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      public static List<BazosOffers> ListActualOffersInDB(string urlNameID)
      {
         List<BazosOffers> listOffers = new List<BazosOffers>();
         SqlConnection conn = new SqlConnection(connString);
         string cmdText = $"SELECT * FROM BazosOffers WHERE CategoryNameUrlID = '{urlNameID}';";
         SqlCommand cmd = new SqlCommand(cmdText, conn);
         conn.Open();
         SqlDataReader reader = cmd.ExecuteReader();
         while (reader.Read())
         {
            listOffers.Add(new BazosOffers((string)reader["Nadpis"], (string)reader["Popis"], (string)reader["Datum"], (string)reader["URL"], (string)reader["Cena"], (int)reader["Viewed"], (string)reader["Lokace"], (string)reader["PSC"], (string)reader["lastChecked"]));
         }
         return listOffers;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="urlNameID"></param>
      public static void InsertNewOffers(string urlNameID)
      {
         SqlConnection connection = new SqlConnection(connString);
         List<BazosOffers> actualList = ListActualOffersInDB(urlNameID);
         updatedList = new List<BazosOffers>();
         //string cmd = $"INSERT INTO BazosOffers (Nadpis, Popis, Datum, Cena, Lokace, PSC, Viewed, URL, CategoryNameUrlID) VALUES('ndps', 'pps', 'dtm', 'cn', 'lkc', 'psc', 100, 'url', @urlNameID);";
         //SqlCommand command = new SqlCommand(cmd, connection);
         //command.Parameters.AddWithValue("@urlNameID", urlNameID);
         //connection.Open();
         //command.ExecuteNonQuery();
         //connection.Close();
         //try
         //{
         int i = 0;
         foreach (BazosOffers item in BazosOffers.ListBazosOffers.ToList()) //aktual downloaded
         {
            i++;
            if (actualList.Count > 0)
            {
               Dictionary<int, string> DictUpdateChange = new Dictionary<int, string>()
               {
                  { 0, item.nadpis },
                  { 1, item.cena },
                  { 2, item.popis },
                  { 3, item.datum },
                  { 4, $"{item.lokace} - {item.psc}" }
               };
               string[] updateCmdTexts = new string[]
               {
               $"UPDATE BazosOffers SET Nadpis = '{item.nadpis}' WHERE URL = '{item.url}' AND Nadpis != '{item.nadpis}';",
               $"UPDATE BazosOffers SET Cena = '{item.cena}' WHERE URL = '{item.url}' AND Cena != '{item.cena}';",
               $"UPDATE BazosOffers SET Popis = '{item.popis}' WHERE URL = '{item.url}' AND Popis != '{item.popis}';",
               $"UPDATE BazosOffers SET Datum = '{item.datum}' WHERE URL = '{item.url}' AND Datum != '{item.datum}';",
               $"UPDATE BazosOffers SET Lokace = '{item.lokace}', PSC = '{item.psc}' WHERE URL = '{item.url}' AND Lokace != '{item.lokace}';",
               $"UPDATE BazosOffers SET Viewed = '{item.viewed}' WHERE URL = '{item.url}' AND Viewed != '{item.viewed}';",
               $"UPDATE BazosOffers SET LastChecked = '{DateTime.Now.ToString()}' WHERE URL = '{item.url}' AND LastChecked != '{item.lastChecked}';",
               };
               int index = 0;
               foreach (string cmdText in updateCmdTexts)
               {
                  int recordAffect = ExecuteNonQuery(cmdText);
                  if (recordAffect > 0 && !cmdText.Contains("Viewed") && !cmdText.Contains("LastChecked"))
                  {
                     Dictionary<int, string> DictUpdateAfterChange = new Dictionary<int, string>()
                     {
                        { 0, item.nadpis },
                        { 1, item.cena },
                        { 2, item.popis },
                        { 3, item.datum },
                        { 4, $"{item.lokace} - {item.psc}" }
                     };
                     if (!updatedList.Contains(item))
                     {
                        updatedList.Add(item);
                     }
                     item.changed = item.changed == string.Empty ? $"changed:\n {DictUpdateChange[index]} - {DictUpdateAfterChange[index]}" : item.changed + $"\n{DictUpdateChange[index]} - {DictUpdateAfterChange[index]}";
                  }
                  index++;
               }
            }
            if (!actualList.Any(p => p.url == item.url))
            {
               string cmd = $"INSERT INTO BazosOffers " +
                  $"(Nadpis, Popis, Datum, Cena, Lokace, PSC, Viewed, URL, CategoryNameUrlID, LastChecked) VALUES " +
                  $"(@nadpis, @popis, @datum, @cena, @lokace, @psc, @viewed, @url, @urlNameID, '{DateTime.Now.ToString()}');";// WHERE NOT EXISTS (SELECT Nadpis FROM BazosOffers WHERE URL = @url);";
               SqlCommand command = new SqlCommand(cmd, connection);
               command.Parameters.AddWithValue("@nadpis", item.nadpis);
               command.Parameters.AddWithValue("@popis", item.popis);
               command.Parameters.AddWithValue("@datum", item.datum);
               command.Parameters.AddWithValue("@cena", item.cena);
               command.Parameters.AddWithValue("@lokace", item.lokace);
               command.Parameters.AddWithValue("@psc", item.psc);
               command.Parameters.AddWithValue("@viewed", item.viewed);
               command.Parameters.AddWithValue("@url", item.url);
               command.Parameters.AddWithValue("@urlNameID", urlNameID);
               connection.Open(); //open SQL server connection
               command.ExecuteNonQuery();
               connection.Close();
               newOffersList.Add(item);
            }
            if (i > 3950)
            {
               Console.WriteLine("test");
            }
            //CheckDeletedOffers(connection);
         }
         downloaded = true;
         //}
         //catch
         //{


         //}
      }

      /// <summary>
      /// Check if offer is to delete
      /// </summary>
      /// <param name="connection"></param>
      private static void CheckDeletedOffers(SqlConnection connection)
      {
         string selectCmdText = "SELECT * FROM BazosOffers";
         SqlCommand selectCommand = new SqlCommand(selectCmdText, connection);
         connection.Open();
         SqlDataReader reader = selectCommand.ExecuteReader();
         while (reader.Read())
         {
            string dbUrl = (string)reader["url"];
            string categoryNameURL = (string)reader["CategoryNameUrlID"];
            if (categoryNameURL == BazosOffers.actualCategoryNameURL && BazosOffers.ListBazosOffers.Where(p => p.url == dbUrl).ToString() != string.Empty) //Offer url in db is not contained in actual offers list
            {
               UpdateDeletedOffers(dbUrl, connection);
            }
         }
         connection.Close();
      }

      /// <summary>
      /// Add deleted offer to BazosEndedOffers table and delete it from BazosOffers table.
      /// </summary>
      /// <param name="url"></param>
      /// <param name="connection"></param>
      private static void UpdateDeletedOffers(string url, SqlConnection connection)
      {
         string insertCmdText = $"INSERT INTO BazosEndedOffers (Nadpis, Popis, Cena, Lokace, PSC, Viewed, LastViewed) SELECT (Nadpis, Popis, Cena, Lokace, PSC, Viewed, LastChecked) FROM BazosOffers;";
         string deleteCmdText = $"DELETE FROM BazosOffers WHERE URL = {url}";
         //string cmdText = "SELECT * FROM BazosOffers";
         SqlCommand cmdInsertToEnded = new SqlCommand(insertCmdText, connection);
         SqlCommand cmdDeleteFromOffers = new SqlCommand(deleteCmdText, connection);
         connection.Open();
         cmdInsertToEnded.ExecuteNonQuery();
         cmdDeleteFromOffers.ExecuteNonQuery();
         connection.Close();
         AddDeletedOffersToList();
      }

      private static void AddDeletedOffersToList()
      {
         SqlConnection connection = new SqlConnection(connString);
         string selectCmdText = $"SELECT * FROM BazosDeletedTable;";
         SqlCommand cmd = new SqlCommand(selectCmdText, connection);
         connection.Open();
         SqlDataReader reader = cmd.ExecuteReader();
         while (reader.Read()) //load level info
         {
            BazosOffers item = new BazosOffers((string)reader["Nadpis"], (string)reader["Popis"], (string)reader["Datum"], (string)reader["URL"], (string)reader["Cena"], (int)reader["Viewed"], (string)reader["Lokace"], (string)reader["PSC"], (string)reader["EndedDateTimeGetted"]);
            deletedList.Add(item);
         }
         connection.Close();
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="NameOfSet"></param>
      /// <param name="Name"></param>
      /// <param name="UrlPage"></param>
      /// <param name="MaxCena"></param>
      public static void AddToFilter(string NameOfSet, string Name, string UrlPage, int MaxCena)
      {
         Filters filter = new Filters(NameOfSet, UrlPage, Name, MaxCena);
         SqlConnection connection = new SqlConnection(connString);
         string cmdText = !NameOfSetExist(NameOfSet) ? $"INSERT INTO BazosFilter (NAME_OF_FILTER, NAME, URL_PAGE, MAX_CENA) VALUES (@NameOfSet, @Name, @UrlPage, @MaxCena);" : $"UPDATE BazosFilter SET URL_PAGE = @UrlPage, NAME = @Name, MAX_CENA = @MaxCena WHERE NAME_OF_FILTER = @NameOfSet;";
         SqlCommand cmd = new SqlCommand(cmdText, connection);
         cmd.Parameters.AddWithValue("@NameOfSet", NameOfSet);
         cmd.Parameters.AddWithValue("@Name", Name);
         cmd.Parameters.AddWithValue("@UrlPage", UrlPage);
         cmd.Parameters.AddWithValue("@MaxCena", MaxCena);      
         connection.Open();
         cmd.ExecuteNonQuery();
         connection.Close();
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="NameOfSet"></param>
      /// <returns></returns>
      private static bool NameOfSetExist(string NameOfSet)
      {
         SqlConnection connection = new SqlConnection(connString);
         string selectCmdText = $"SELECT * FROM BazosFilter WHERE NAME_OF_FILTER = @NameOfSet;";
         SqlCommand cmd = new SqlCommand(selectCmdText, connection);
         cmd.Parameters.AddWithValue("@NameOfSet", NameOfSet);
         connection.Open();
         SqlDataReader reader = cmd.ExecuteReader();
         while (reader.Read()) //load level info
         {
            connection.Close();
            return true;
         }
         connection.Close();
         return false;
      }

      #region FilterSet
      /// <summary>
      /// 
      /// </summary>
      /// <param name="filterSetName"></param>
      /// <param name="listboxItems"></param>
      public static void CreateFilterSet(string filterSetName, List<string> listboxFilterItems, List<string> listboxBlacklistItems, string urlPage)
      {
         SqlConnection connection = new SqlConnection(connString);
         string itemSet = string.Empty;
         string blacklistItems = string.Empty;
         foreach (var item in listboxFilterItems) //itemSet string
         {
            itemSet = listboxFilterItems.IndexOf(item) == 0 ? item : $"{itemSet};{item}";
         }
         foreach (var item in listboxBlacklistItems) //blaclistItems string
         {
            blacklistItems = listboxBlacklistItems.IndexOf(item) == 0 ? item : $"{blacklistItems};{item}";
         }
         string cmdText = !FilterSetExist(filterSetName) ? $"INSERT INTO BazosFilterSet VALUES (@filterSetName, '{itemSet}', '{blacklistItems}', '{urlPage}');" : $"UPDATE BazosFilterSet SET SetItems = '{itemSet}', BlacklistItems = '{blacklistItems}', URL_PAGE = '{urlPage}' WHERE SetName = @filterSetName;";
         SqlCommand cmd = new SqlCommand(cmdText, connection);
         cmd.Parameters.AddWithValue("@filterSetName", filterSetName);
         connection.Open();
         cmd.ExecuteNonQuery();
         connection.Close();
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="filterSetName"></param>
      /// <returns></returns>
      public static bool FilterSetExist(string filterSetName)
      {
         SqlConnection connection = new SqlConnection(connString);
         string selectCmdText = $"SELECT * FROM BazosFilterSet WHERE SetName = @filterSetName";
         SqlCommand cmd = new SqlCommand(selectCmdText, connection);
         cmd.Parameters.AddWithValue("@filterSetName", filterSetName);
         connection.Open();
         SqlDataReader reader = cmd.ExecuteReader();
         while (reader.Read()) //load level info
         {
            connection.Close();
            return true;
         }
         connection.Close();
         return false;
      }

      public static void InitializeFilters(out List<string> List_URL_PAGE)//, out List<string> NAME_OF_FILTER)
      {
         SqlConnection connection = new SqlConnection(connString);
         string selectCmdText = $"SELECT URL_PAGE FROM BazosFilter;";
         SqlCommand cmd = new SqlCommand(selectCmdText, connection);
         connection.Open();
         SqlDataReader reader = cmd.ExecuteReader();
         List_URL_PAGE = new List<string>();
         //NAME_mOF_FILTER = new List<string>();
         while (reader.Read()) //load level info
         {
            List_URL_PAGE.Add((string)reader["URL_PAGE"]);
            //NAME_OF_FILTER.Add((string)reader["NAME_OF_FILTER"]);
         }
         connection.Close();
      }

      public static void LoadFilterSetToBoxes(string nameOfSet, out string[] setItems, out string[] blacklistItems)
      {
         SqlConnection connection = new SqlConnection(connString);
         string cmdText = $"SELECT * FROM BazosFilterSet WHERE setName = {nameOfSet}";
         SqlCommand cmd = new SqlCommand(cmdText, connection);
         connection.Open();
         SqlDataReader reader = cmd.ExecuteReader();
         string setItemsString = string.Empty;
         string blacklistItemsString = string.Empty;
         while (reader.Read()) //load level info
         {
            setItemsString = (string)reader["SetItems"];
            blacklistItemsString = (string)reader["BlacklistItems"];
         }
         connection.Close();
         setItems = setItemsString.Split(';');
         blacklistItems = blacklistItemsString.Split(';');
      }

      public static void LoadFilterSetToBoxes(string urlPage, out string[] setItems, out string[] blacklistItems, out string setName)
      {
         SqlConnection connection = new SqlConnection(connString);
         string cmdText = $"SELECT * FROM BazosFilterSet WHERE URL_PAGE = {urlPage}";
         SqlCommand cmd = new SqlCommand(cmdText, connection);
         connection.Open();
         SqlDataReader reader = cmd.ExecuteReader();
         string setItemsString = string.Empty;
         string blacklistItemsString = string.Empty;
         setName = string.Empty;
         while (reader.Read()) //load level info
         {
            setItemsString = (string)reader["SetItems"];
            blacklistItemsString = (string)reader["BlacklistItems"];
            setName = (string)reader["SetName"];
         }
         connection.Close();
         setItems = setItemsString.Split(';');
         blacklistItems = blacklistItemsString.Split(';');
      }

      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      public static string[] LoadDefaultUrls()
      {
         //SqlConnection connection = new SqlConnection(connString);
         //string cmdText = $"SELECT DISTINCT URL_PAGE FROM BazosFilter UNION SELECT DISTINCT URL_PAGE FROM BazosBlackList";
         //SqlCommand cmd = new SqlCommand(cmdText, connection);
         //List<string> ListUrl = new List<string>();
         //connection.Open();
         //SqlDataReader reader = cmd.ExecuteReader();
         //while (reader.Read()) //load level info
         //{
         //   ListUrl.Add((string)reader["URL_PAGE"]);       
         //}
         //connection.Close();
         //return ListUrl.ToArray
         return new string[0];
      }
      #endregion


      /// <summary>
      /// Execute SQL command
      /// </summary>
      /// <param name="connection"></param>
      /// <param name="cmdText"></param>
      private static void SendCommand(SqlConnection connection, string cmdText)
      {
         SqlCommand cmd = new SqlCommand(cmdText, connection);
         connection.Open(); //open SQL server connection
         cmd.ExecuteNonQuery();
         connection.Close();
      }

      /// <summary>
      /// Execute SQL command
      /// </summary>
      /// <param name="cmdText">command text</param>
      private static int ExecuteNonQuery(string cmdText)
      {
         SqlConnection connection = new SqlConnection(connString);
         SqlCommand cmd = new SqlCommand(cmdText, connection);
         connection.Open();
         int recordsAffected = cmd.ExecuteNonQuery();
         connection.Close();
         return recordsAffected;
      }

      //private static int ExecuteNonQuery(string cmdText, List<string> parameters)
      //{
      //   SqlConnection connection = new SqlConnection(connString);
      //   SqlCommand cmd = new SqlCommand(cmdText, connection);
      //   foreach (var item in parameters)
      //   {
      //      cmd.Parameters.AddWithValue()
      //   }
      //   connection.Open();
      //   int recordsAffected = cmd.ExecuteNonQuery();
      //   connection.Close();
      //   return recordsAffected;
      //}

   }
}