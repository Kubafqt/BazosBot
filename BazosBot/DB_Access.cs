using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
//corona filter program - 

namespace BazosBot
{
   class DB_Access
   {
      public static List<BazosOffers> newOffersList;
      public static List<BazosOffers> updatedList;
      public static List<BazosOffers> deletedList;
      static readonly string connString = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\..\"))}Database.mdf;Integrated Security = True; Connect Timeout = 30";
      public static bool downloaded = false;
      private static Dictionary<int, string> DictUpdateChangeNames = new Dictionary<int, string>()
               {
                  { 0, "nadpis" },
                  { 1, "cena" },
                  { 2, "popis" },
                  { 3, "datum" },
                  { 4, $"lokace" }
               };
      public static double i = 0;
      public static double offersCount = 0;

      /// <summary>
      /// Insert new offers to offers DB, update updated offers in DB, then delete deleted offers from DB and add it to deleted offers table.
      /// </summary>
      /// <param name="urlNameID"></param>
      public static void InsertNewOffers(string urlNameID, bool onlyNewOffers = false)
      {
         SqlConnection connection = new SqlConnection(connString);
         List<BazosOffers> actualList = ListActualOffersInDB(urlNameID);
         newOffersList = new List<BazosOffers>();
         updatedList = new List<BazosOffers>();
         deletedList = new List<BazosOffers>();
         offersCount = BazosOffers.ListBazosOffers.Count;
         i = 0;
         foreach (BazosOffers item in BazosOffers.ListBazosOffers.ToList()) //aktual downloaded
         {
            i++;
            if (actualList.Count > 0 && !onlyNewOffers)
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
               $"UPDATE BazosOffers SET LastChecked = '{DateTime.Now}' WHERE URL = '{item.url}' AND LastChecked != '{item.lastChecked}';",
               };
               int index = 0;
               foreach (string cmdText in updateCmdTexts)
               {
                  int recordAffect = ExecuteNonQuery(cmdText);
                  if (recordAffect > 0 && !cmdText.Contains("Viewed") && !cmdText.Contains("LastChecked"))
                  {  //report changes to property:
                     BazosOffers offer;
                     GetItemFromDbByURL(out offer, item.url);
                     Dictionary<int, string> DictAfterUpdateChange = new Dictionary<int, string>()
                     {
                        { 0, offer.nadpis },
                        { 1, offer.cena },
                        { 2, offer.popis },
                        { 3, offer.datum },
                        { 4, $"{offer.lokace} - {offer.psc}" }
                     };
                     if (!updatedList.Contains(item))
                     {
                        updatedList.Add(item);
                     }
                     item.changed = item.changed == string.Empty ? $"changed:\n {DictUpdateChangeNames[index]}: {DictUpdateChange[index]} -> {DictAfterUpdateChange[index]}" : item.changed + $"\n {DictUpdateChangeNames[index]}: {DictUpdateChange[index]} -> {DictAfterUpdateChange[index]}";
                  }
                  index++;
               }
            }
            if (!actualList.Any(p => p.url == item.url)) //exception: category goes from Another CategoryNameUrl - now url set to not unique;
            {
               string cmd = $"INSERT INTO BazosOffers " +
                  $"(Nadpis, Popis, Datum, Cena, Lokace, PSC, Viewed, URL, CategoryNameUrlID, LastChecked) VALUES " +
                  $"(@nadpis, @popis, @datum, @cena, @lokace, @psc, @viewed, @url, @urlNameID, '{DateTime.Now}');";// WHERE NOT EXISTS (SELECT Nadpis FROM BazosOffers WHERE URL = @url);";
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
         }
         if (!onlyNewOffers)
         {
            CheckDeletedOffers(connection);
         }
         downloaded = true;
      }

      /// <summary>
      /// Get list of actual offers in DB.
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
         conn.Close();
         return listOffers;
      }

      /// <summary>
      /// Get list of actual offer names in DB.
      /// </summary>
      /// <returns></returns>
      public static List<string> ListActualOffersCategoryURLInDB()
      {
         List<string> listOfferCategoryURL = new List<string>();
         SqlConnection conn = new SqlConnection(connString);
         string cmdText = $"SELECT DISTINCT CategoryNameUrlID FROM BazosOffers;";
         SqlCommand cmd = new SqlCommand(cmdText, conn);
         conn.Open();
         SqlDataReader reader = cmd.ExecuteReader();
         while (reader.Read())
         {
            listOfferCategoryURL.Add((string)reader["CategoryNameUrlID"]);
         }
         conn.Close();
         return listOfferCategoryURL;
      }

      /// <summary>
      /// Used when get only new offers.
      /// </summary>
      /// <param name="url"></param>
      /// <param name="categoryUrl"></param>
      /// <returns></returns>
      public static bool DBContainsUrl(string url, string categoryUrl)
      {
         SqlConnection connection = new SqlConnection(connString);
         string selectCmdText = $"SELECT * FROM BazosOffers WHERE CategoryNameUrlID = '{categoryUrl}' AND URL = '{url}';";
         SqlCommand cmd = new SqlCommand(selectCmdText, connection);
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

      /// <summary>
      /// 
      /// </summary>
      /// <param name="item"></param>
      /// <param name="url"></param>
      private static void GetItemFromDbByURL(out BazosOffers item, string url)
      {
         SqlConnection connection = new SqlConnection(connString);
         string selectCmdText = $"SELECT * FROM BazosOffers WHERE URL = '{url}';";
         SqlCommand cmd = new SqlCommand(selectCmdText, connection);
         connection.Open();
         SqlDataReader reader = cmd.ExecuteReader();
         item = null;
         while (reader.Read()) //load level info
         {
            item = new BazosOffers((string)reader["Nadpis"], (string)reader["Popis"], (string)reader["Datum"], url, (string)reader["Cena"], (int)reader["Viewed"], (string)reader["Lokace"], (string)reader["PSC"], (string)reader["LastChecked"]);
         }
         connection.Close();
      }

      #region Deleted offers
      /// <summary>
      /// Check if offer is deleted from offers category.
      /// </summary>
      /// <param name="connection"></param>
      private static void CheckDeletedOffers(SqlConnection connection)
      {
         string selectCmdText = $"SELECT * FROM BazosOffers WHERE CategoryNameUrlID = '{BazosOffers.actualCategoryURL}';";
         SqlCommand selectCommand = new SqlCommand(selectCmdText, connection);
         connection.Open();
         //List<BazosOffers> actualList = BazosOffers.ListBazosOffers;
         SqlDataReader reader = selectCommand.ExecuteReader();
         List<string> urls = new List<string>();
         while (reader.Read())
         {
            string urlDB = (string)reader["url"];
            if (!BazosOffers.ListBazosOffers.Any(p => p.url == urlDB)) //Offer url in DBs is not contained in actual offers list
            {
               urls.Add(urlDB);
            }
         }
         connection.Close();
         UpdateDeletedOffers(urls, connection, BazosOffers.actualCategoryURL);
      }

      /// <summary>
      /// Add deleted offer to BazosEndedOffers table and delete it from BazosOffers table.
      /// </summary>
      /// <param name="url"></param>
      /// <param name="connection"></param>
      private static void UpdateDeletedOffers(List<string> urls, SqlConnection connection, string categoryNameURL)
      {
         List<BazosOffers> DBlist = ListActualOffersInDB(categoryNameURL);
         foreach (string url in urls)
         {
            string insertCmdText = $"INSERT INTO BazosDeletedOffers " +
            $"(Nadpis, Popis, Datum, Cena, Lokace, PSC, Viewed, LastViewed, CategoryNameUrlID, EndedDateTimeGetted) SELECT " +
            $"Nadpis, Popis, Datum, Cena, Lokace, PSC, Viewed, LastChecked, CategoryNameUrlID, '{DateTime.Now}' FROM BazosOffers WHERE URL='{url}';";
            string deleteCmdText = $"DELETE FROM BazosOffers WHERE URL='{url}'";
            SqlCommand cmdInsertToEnded = new SqlCommand(insertCmdText, connection);
            SqlCommand cmdDeleteFromOffers = new SqlCommand(deleteCmdText, connection);
            connection.Open();
            cmdInsertToEnded.ExecuteNonQuery();
            cmdDeleteFromOffers.ExecuteNonQuery();
            connection.Close();
         }
         AddDeletedOffersToList(urls, DBlist);
      }

      /// <summary>
      /// 
      /// </summary>
      private static void AddDeletedOffersToList(List<string> urls, List<BazosOffers> DBlist)
      {
         foreach (string url in urls)
         {
            BazosOffers item = DBlist.FirstOrDefault(p => p.url == url);
            deletedList.Add(item);
         }
      }

      #endregion

      #region Execute SQL commands
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

      #endregion

      #region Filter
      //used in clickable UI
      ///// <summary>
      ///// 
      ///// </summary>
      ///// <param name="NameOfSet"></param>
      ///// <param name="Name"></param>
      ///// <param name="UrlPage"></param>
      ///// <param name="MaxCena"></param>
      //public static void AddToFilter(string NameOfFilter, string Name, string UrlPage, int MaxCena)
      //{
      //   int index = Filters.ListFilters.IndexOf(Filters.ListFilters.FirstOrDefault(p => p.NameOfFilter == NameOfFilter));
      //   if (index >= 0)
      //   {
      //      Filters.ListFilters[index].Name = Name;
      //      Filters.ListFilters[index].PageUrl = UrlPage;
      //      Filters.ListFilters[index].MaxCena = MaxCena;
      //   }
      //   else
      //   {
      //      Filters filter = new Filters(NameOfFilter, UrlPage, Name, MaxCena);
      //   }
      //   SqlConnection connection = new SqlConnection(connString);
      //   string cmdText = !NameOfSetExist(NameOfFilter) ? $"INSERT INTO BazosFilter (NAME_OF_FILTER, NAME, URL_PAGE, MAX_CENA) VALUES (@NameOfSet, @Name, @UrlPage, @MaxCena);" : $"UPDATE BazosFilter SET URL_PAGE = @UrlPage, NAME = @Name, MAX_CENA = @MaxCena WHERE NAME_OF_FILTER = @NameOfSet;";
      //   SqlCommand cmd = new SqlCommand(cmdText, connection);
      //   cmd.Parameters.AddWithValue("@NameOfSet", NameOfFilter);
      //   cmd.Parameters.AddWithValue("@Name", Name);
      //   cmd.Parameters.AddWithValue("@UrlPage", UrlPage);
      //   cmd.Parameters.AddWithValue("@MaxCena", MaxCena);
      //   connection.Open();
      //   cmd.ExecuteNonQuery();
      //   connection.Close();
      //}

      ///// <summary>
      ///// 
      ///// </summary>
      ///// <param name="NameOfSet"></param>
      ///// <returns></returns>
      //private static bool NameOfSetExist(string NameOfSet)
      //{
      //   SqlConnection connection = new SqlConnection(connString);
      //   string selectCmdText = $"SELECT * FROM BazosFilter WHERE NAME_OF_FILTER = @NameOfSet;";
      //   SqlCommand cmd = new SqlCommand(selectCmdText, connection);
      //   cmd.Parameters.AddWithValue("@NameOfSet", NameOfSet);
      //   connection.Open();
      //   SqlDataReader reader = cmd.ExecuteReader();
      //   while (reader.Read()) //load level info
      //   {
      //      connection.Close();
      //      return true;
      //   }
      //   connection.Close();
      //   return false;
      //}

      #endregion

      #region FilterSet
      //used in clickable UI
      ///// <summary>
      ///// 
      ///// </summary>
      ///// <param name="filterSetName"></param>
      ///// <param name="listboxItems"></param>
      //public static void CreateFilterSet(string filterSetName, List<string> listboxFilterItems, List<string> listboxBlacklistItems, string urlPage)
      //{
      //   SqlConnection connection = new SqlConnection(connString);
      //   string itemSet = string.Empty;
      //   string blacklistItems = string.Empty;
      //   foreach (var item in listboxFilterItems) //itemSet string
      //   {
      //      itemSet = listboxFilterItems.IndexOf(item) == 0 ? item : $"{itemSet};{item}";
      //   }
      //   foreach (var item in listboxBlacklistItems) //blaclistItems string
      //   {
      //      blacklistItems = listboxBlacklistItems.IndexOf(item) == 0 ? item : $"{blacklistItems};{item}";
      //   }
      //   string cmdText = !FilterSetExist(filterSetName) ? $"INSERT INTO BazosFilterSet VALUES (@filterSetName, '{itemSet}', '{blacklistItems}', '{urlPage}');" : $"UPDATE BazosFilterSet SET SetFilters = '{itemSet}', SetBlacklists = '{blacklistItems}', URL_PAGE = '{urlPage}' WHERE SetName = @filterSetName;";
      //   SqlCommand cmd = new SqlCommand(cmdText, connection);
      //   cmd.Parameters.AddWithValue("@filterSetName", filterSetName);
      //   connection.Open();
      //   cmd.ExecuteNonQuery();
      //   connection.Close();
      //}

      ///// <summary>
      ///// 
      ///// </summary>
      ///// <param name="filterSetName"></param>
      ///// <returns></returns>
      //public static bool FilterSetExist(string filterSetName)
      //{
      //   SqlConnection connection = new SqlConnection(connString);
      //   string selectCmdText = $"SELECT * FROM BazosFilterSet WHERE SetName = @filterSetName";
      //   SqlCommand cmd = new SqlCommand(selectCmdText, connection);
      //   cmd.Parameters.AddWithValue("@filterSetName", filterSetName);
      //   connection.Open();
      //   SqlDataReader reader = cmd.ExecuteReader();
      //   while (reader.Read()) //load level info
      //   {
      //      connection.Close();
      //      return true;
      //   }
      //   connection.Close();
      //   return false;
      //}

      ///// <summary>
      ///// 
      ///// </summary>
      ///// <param name="List_URL_PAGE"></param>
      //public static void InitializeFilters(out List<string> List_URL_PAGE)//, out List<string> NAME_OF_FILTER)
      //{
      //   SqlConnection connection = new SqlConnection(connString);
      //   string selectCmdText = $"SELECT URL_PAGE FROM BazosFilter;";
      //   SqlCommand cmd = new SqlCommand(selectCmdText, connection);
      //   connection.Open();
      //   SqlDataReader reader = cmd.ExecuteReader();
      //   List_URL_PAGE = new List<string>();
      //   //NAME_mOF_FILTER = new List<string>();
      //   while (reader.Read()) //load level info
      //   {
      //      List_URL_PAGE.Add((string)reader["URL_PAGE"]);
      //      //NAME_OF_FILTER.Add((string)reader["NAME_OF_FILTER"]);
      //   }
      //   connection.Close();
      //}

      ///// <summary>
      ///// 
      ///// </summary>
      ///// <param name="nameOfSet"></param>
      ///// <param name="setItems"></param>
      ///// <param name="blacklistItems"></param>
      //public static void LoadFilterSetToBoxes(string nameOfSet, out string[] setItems, out string[] blacklistItems)
      //{
      //   SqlConnection connection = new SqlConnection(connString);
      //   string cmdText = $"SELECT * FROM BazosFilterSet WHERE setName = {nameOfSet}";
      //   SqlCommand cmd = new SqlCommand(cmdText, connection);
      //   connection.Open();
      //   SqlDataReader reader = cmd.ExecuteReader();
      //   string setItemsString = string.Empty;
      //   string blacklistItemsString = string.Empty;
      //   while (reader.Read()) //load level info
      //   {
      //      setItemsString = (string)reader["SetItems"];
      //      blacklistItemsString = (string)reader["BlacklistItems"];
      //   }
      //   connection.Close();
      //   setItems = setItemsString.Split(';');
      //   blacklistItems = blacklistItemsString.Split(';');
      //}

      ///// <summary>
      ///// 
      ///// </summary>
      ///// <param name="urlPage"></param>
      ///// <param name="setItems"></param>
      ///// <param name="blacklistItems"></param>
      ///// <param name="setName"></param>
      //public static void LoadFilterSetToBoxes(string urlPage, out string[] setItems, out string[] blacklistItems, out string setName)
      //{
      //   SqlConnection connection = new SqlConnection(connString);
      //   string cmdText = $"SELECT * FROM BazosFilterSet WHERE URL_PAGE = {urlPage}";
      //   SqlCommand cmd = new SqlCommand(cmdText, connection);
      //   connection.Open();
      //   SqlDataReader reader = cmd.ExecuteReader();
      //   string setItemsString = string.Empty;
      //   string blacklistItemsString = string.Empty;
      //   setName = string.Empty;
      //   while (reader.Read()) //load level info
      //   {
      //      setItemsString = (string)reader["SetItems"];
      //      blacklistItemsString = (string)reader["BlacklistItems"];
      //      setName = (string)reader["SetName"];
      //   }
      //   connection.Close();
      //   setItems = setItemsString.Split(';');
      //   blacklistItems = blacklistItemsString.Split(';');
      //}

      ///// <summary>
      ///// 
      ///// </summary>
      ///// <returns></returns>
      //public static string[] LoadDefaultUrls()
      //{
      //   //SqlConnection connection = new SqlConnection(connString);
      //   //string cmdText = $"SELECT DISTINCT URL_PAGE FROM BazosFilter UNION SELECT DISTINCT URL_PAGE FROM BazosBlackList";
      //   //SqlCommand cmd = new SqlCommand(cmdText, connection);
      //   //List<string> ListUrl = new List<string>();
      //   //connection.Open();
      //   //SqlDataReader reader = cmd.ExecuteReader();
      //   //while (reader.Read()) //load level info
      //   //{
      //   //   ListUrl.Add((string)reader["URL_PAGE"]);       
      //   //}
      //   //connection.Close();
      //   //return ListUrl.ToArray();
      //   return new string[0];
      //}

      #endregion

   }
}