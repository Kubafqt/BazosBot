using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using System.Text.RegularExpressions;
//+updated text adjust

namespace BazosBot
{
   class DB_Access
   {
      public static List<string> catchedCommands = new List<string>();
      public static List<BazosOffers> newOffersList;
      public static List<BazosOffers> updatedList;
      public static List<BazosOffers> deletedList;
      static readonly string connString = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\..\"))}BazosDb.mdf;Integrated Security = True; Connect Timeout = 30";
      public static bool downloaded = false;
      public static double i = 0; //update to db item count
      public static double offersCount = 0;
      public static bool notUpdateViewedAndLastChecked = false;
      public static List<string> updateChangeTypes = new List<string>() //without date - not report to messagebox
      {
         "nadpis",
         "popis",
         "lokace"
      };
      public static List<string> showUpdateChanges = new List<string>()
      {

      };

      /// <summary> 
      /// Insert new offers to offers DB, update updated offers in DB, then delete deleted offers from DB and add it to deleted offers table.
      /// </summary>
      /// <param name="urlNameID"></param>
      public static void InsertNewOffers(string urlNameID, bool onlyNewOffers = false)
      {
         downloaded = false;
         SqlConnection connection = new SqlConnection(connString);
         List<BazosOffers> actualDBList = ListActualOffersInDB(urlNameID);
         newOffersList = new List<BazosOffers>();
         updatedList = new List<BazosOffers>();
         deletedList = new List<BazosOffers>();
         offersCount = BazosOffers.ListBazosOffers.Count;
         i = 0;
         foreach (BazosOffers item in BazosOffers.ListBazosOffers.ToList()) //aktual downloaded
         {
            i++;
            if (actualDBList.Count > 0 && actualDBList.ToList().Any(p => p.url == item.url)) //offer is contained in db - check to update
            {
               //BazosOffers dbOffer;
               //GetItemFromDbByURL(out offer, item.url); //perfomance test - DB vs object get
               BazosOffers dbItem = actualDBList.ToList().FirstOrDefault(p => p.url == item.url);
               List<string> updateCmdTextList = GetUpdateCmdTextList(item, dbItem, urlNameID); //get commands for updated only
               Dictionary<string, string> DictBeforeUpdateChange = new Dictionary<string, string>()
               {
                  { "nadpis", dbItem.nadpis },
                  { "cena", dbItem.cena },
                  { "popis", dbItem.popis },
                  { "datum", dbItem.datum },
                  { "lokace", $"{dbItem.lokace} - {dbItem.psc}" }
               };
               Dictionary<string, string> DictAfterUpdateChange = new Dictionary<string, string>()
                     {
                        { "nadpis", item.nadpis },
                        { "cena", item.cena },
                        { "popis", item.popis },
                        { "datum", item.datum },
                        { "lokace", $"{item.lokace} - {item.psc}" }
                     };
               foreach (string cmdText in updateCmdTextList.ToList())
               {
                  bool changeReportIsContainedInCommand = DictBeforeUpdateChange.Any(r => cmdText.Substring(0, 7).Contains(r.Key));
                  string pureCmdText = changeReportIsContainedInCommand ? cmdText.Split(new[] { ':' }, 2)[1] : cmdText;
                  int recordAffect = ExecuteNonQuery(pureCmdText); //update all updated properties to db
                  if (recordAffect > 0 && changeReportIsContainedInCommand) //report changes to property
                  {
                     string changedType = cmdText.Split(":")[0];
                     if (!updatedList.Contains(item))
                     {
                        updatedList.Add(item);
                     }
                     item.changed = item.changed == string.Empty ?
                        $"\n{changedType}: \n{DictBeforeUpdateChange[changedType]}\n->\n{DictAfterUpdateChange[changedType]}" :
                        item.changed + $" \n\n\n{changedType}: \n{DictBeforeUpdateChange[changedType]}\n->\n{DictAfterUpdateChange[changedType]}";
                  }
               }
            }
            if (!actualDBList.Any(p => p.url == item.url)) //insert new offer, exception: category goes from Another CategoryURL - now url set to not unique;
            {
               string cmd = $"INSERT INTO BazosOffers " +
                  $"(Nadpis, Popis, Datum, Cena, Lokace, PSC, Viewed, URL, CategoryURL, LastChecked) VALUES " +
                  $"(@nadpis, @popis, @datum, @cena, @lokace, @psc, @viewed, @url, @urlNameID, '{DateTime.Now}');"; //WHERE NOT EXISTS (SELECT Nadpis FROM BazosOffers WHERE URL = @url);";
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
         if (!onlyNewOffers && !Download.stopped) //when not get only new offers
         {
            CheckDeletedOffers(connection);
         }
         Download.stopped = false;
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
         string cmdText = $"SELECT * FROM BazosOffers WHERE CategoryURL = '{urlNameID}';";
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
      /// </summaryTe
      /// <returns></returns>
      public static List<string> ListActualOffersCategoryURLInDB()
      {
         List<string> listOfferCategoryURL = new List<string>();
         SqlConnection conn = new SqlConnection(connString);
         string cmdText = $"SELECT DISTINCT CategoryURL FROM BazosOffers;";
         SqlCommand cmd = new SqlCommand(cmdText, conn);
         conn.Open();
         SqlDataReader reader = cmd.ExecuteReader();
         while (reader.Read())
         {
            listOfferCategoryURL.Add((string)reader["CategoryURL"]);
         }
         conn.Close();
         return listOfferCategoryURL;
      }

      /// <summary>
      /// Used on get only new offers.
      /// </summary>
      /// <param name="url"></param>
      /// <param name="categoryUrl"></param>
      /// <returns></returns>
      public static bool DBContainsUrl(string url, string categoryUrl, out bool urlNewDate, out string nabCena)
      {
         urlNewDate = false;
         nabCena = string.Empty;

         SqlConnection connection = new SqlConnection(connString);
         string selectCmdText = $"SELECT Cena FROM BazosOffers WHERE CategoryURL = '{categoryUrl}' AND URL = '{url}';";
         SqlCommand cmd = new SqlCommand(selectCmdText, connection);
         connection.Open();
         SqlDataReader reader = cmd.ExecuteReader();
         while (reader.Read())
         {
            urlNewDate = true;
            nabCena = (string)reader["Cena"];
         }
         connection.Close();

         selectCmdText = $"SELECT Nadpis FROM BazosOffers WHERE CategoryURL = '{categoryUrl}' AND URL = '{url}';";
         cmd = new SqlCommand(selectCmdText, connection);
         connection.Open();
         reader = cmd.ExecuteReader();
         while (reader.Read()) //load level info
         {
            string test = (string)reader["nadpis"];
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

      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      private static List<string> GetUpdateCmdTextList(BazosOffers item, BazosOffers dbItem, string urlNameID)
      {
         List<string> updateCmdTextList = new List<string>();
         string[] updateCmdTexts = new string[]
               {  // optimalize - create view of table or object comparison (?)
               $"UPDATE BazosOffers SET Nadpis = '{item.nadpis}' WHERE CategoryURL = '{urlNameID}' AND URL = '{item.url}' AND Nadpis != '{item.nadpis}'",
               $"UPDATE BazosOffers SET Cena = '{item.cena}' WHERE CategoryURL = '{urlNameID}' AND URL = '{item.url}' AND Cena != '{item.cena}'",
               $"UPDATE BazosOffers SET Popis = '{item.popis}' WHERE CategoryURL = '{urlNameID}' AND URL = '{item.url}' AND Popis != '{item.popis}'",
               $"UPDATE BazosOffers SET Datum = '{item.datum}' WHERE CategoryURL = '{urlNameID}' AND URL = '{item.url}' AND Datum != '{item.datum}'",
               $"UPDATE BazosOffers SET Lokace = '{item.lokace}', PSC = '{item.psc}' WHERE URL = '{item.url}' AND Lokace != '{item.lokace}'",
               $"UPDATE BazosOffers SET Viewed = '{item.viewed}' WHERE CategoryURL = '{urlNameID}' AND URL = '{item.url}' AND Viewed != '{item.viewed}'",
               $"UPDATE BazosOffers SET LastChecked = '{DateTime.Now}' WHERE CategoryURL = '{urlNameID}' AND URL = '{item.url}' AND LastChecked != '{item.lastChecked}'",
               };
         string updateCmdTextToSplit = !notUpdateViewedAndLastChecked ? $"{updateCmdTexts[6]};" : string.Empty;
         updateCmdTextToSplit = dbItem.nadpis != item.nadpis ? $"nadpis:{TextAdjust.RemoveSemicolons(updateCmdTexts[0])};{updateCmdTextToSplit}" : updateCmdTextToSplit;
         updateCmdTextToSplit = dbItem.cena != item.cena ? $"cena:{updateCmdTexts[1]};{updateCmdTextToSplit}" : updateCmdTextToSplit;
         updateCmdTextToSplit = TextAdjust.RemoveDiacritics(dbItem.popis) != TextAdjust.RemoveDiacritics(item.popis) ? $"popis:{TextAdjust.RemoveSemicolons(updateCmdTexts[2])};{updateCmdTextToSplit}" : updateCmdTextToSplit;
         updateCmdTextToSplit = dbItem.datum != item.datum ? $"datum:{updateCmdTexts[3]};{updateCmdTextToSplit}" : updateCmdTextToSplit;
         updateCmdTextToSplit = dbItem.lokace != item.lokace ? $"lokace:{updateCmdTexts[4]};{updateCmdTextToSplit}" : updateCmdTextToSplit;
         updateCmdTextToSplit = dbItem.viewed != item.viewed && !notUpdateViewedAndLastChecked ? $"{updateCmdTexts[5]};{updateCmdTextToSplit}" : updateCmdTextToSplit;
         if (updateCmdTextToSplit.Length > 0)
         {
            updateCmdTextList.AddRange(updateCmdTextToSplit.Split(";"));
            updateCmdTextList.RemoveAt(updateCmdTextList.Count - 1); //after last semicolon is empty value
         }
         return updateCmdTextList;
      }

      /// <summary>
      /// Return true when some item is in table.
      /// </summary>
      public static bool DbTableContainsItem(string tableName)
      {
         SqlConnection connection = new SqlConnection(Settings.DBconnString);
         string cmdText = $"SELECT * FROM {tableName}";
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
      public static void LoadSettings(out bool quickfilterHelp)
      {
         SqlConnection connection = new SqlConnection(Settings.DBconnString);
         string cmdText = $"SELECT * FROM Settings;";
         SqlCommand cmd = new SqlCommand(cmdText, connection);
         connection.Open();
         SqlDataReader reader = cmd.ExecuteReader();
         while (reader.Read())
         {
            quickfilterHelp = (bool)reader["QuickFilterHelpButton"];
            connection.Close();
            return;
         }
         quickfilterHelp = true;
         connection.Close();
      }

      /// <summary>
      /// 
      /// </summary>
      public static void SaveSettings(bool quickfilterHelpButton)
      {
         bool notEmptyTable = DbTableContainsItem("Settings");
         int quickfilterHelpBtn = quickfilterHelpButton ? 1 : 0;
         string cmd = notEmptyTable ? $"UPDATE Settings SET QuickFilterHelpButton = '{quickfilterHelpBtn}';" : $"INSERT INTO Settings VALUES ('{quickfilterHelpBtn}');";
         ExecuteNonQuery(cmd);
      }

      #region Deleted offers
      /// <summary>
      /// Check if offer is deleted from offers category.
      /// </summary>
      /// <param name="connection"></param>
      private static void CheckDeletedOffers(SqlConnection connection)
      {
         string selectCmdText = $"SELECT * FROM BazosOffers WHERE CategoryURL = '{BazosOffers.actualCategoryURL}';";
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
      private static void UpdateDeletedOffers(List<string> urls, SqlConnection connection, string CategoryURL)
      {
         List<BazosOffers> DBlist = ListActualOffersInDB(CategoryURL);
         foreach (string url in urls)
         {
            string insertCmdText = $"INSERT INTO BazosDeletedOffers " +
            $"(Nadpis, Popis, Datum, Cena, Lokace, PSC, Viewed, LastViewed, CategoryURL, EndedDateTimeGetted) SELECT " +
            $"Nadpis, Popis, Datum, Cena, Lokace, PSC, Viewed, LastChecked, CategoryURL, '{DateTime.Now}' FROM BazosOffers WHERE URL='{url}';";
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
            item.endedDateTimeGetted = DateTime.Now.ToString();
            deletedList.Add(item);
         }
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="categoryUrlID"></param>
      public static void DeleteOffersCategoryFromDB(string categoryUrlID)
      {
         string deleteOffers = $"DELETE FROM BazosOffers WHERE CategoryURL='{categoryUrlID}'";
         string deleteQuickFilters = $"DELETE FROM BazosQuickFilter WHERE CategoryURL='{categoryUrlID}'";
         ExecuteNonQuery(deleteOffers);
         ExecuteNonQuery(deleteQuickFilters);
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
      public static int ExecuteNonQuery(string cmdText)
      {
         try
         {
            SqlConnection connection = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(cmdText, connection);
            connection.Open();
            int recordsAffected = cmd.ExecuteNonQuery();
            connection.Close();
            return recordsAffected;
         }
         catch (Exception e)
         {
            catchedCommands.Add($"{cmdText} - {e.GetType()}");
            return 0;
         }
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

      /// <summary>
      /// Return highest ID from table.
      /// </summary>
      /// <returns></returns>
      public static int FreeID(string categoryUrl, string tableName, string name, string tableFilterName = "FilterName")
      {
         List<int> IDlist = new List<int>();
         if (DbTableContainsItem(tableName))
         {
            SqlConnection connection = new SqlConnection(Settings.DBconnString);
            string cmdText = $"SELECT {tableFilterName} FROM {tableName} WHERE CategoryURL = '{categoryUrl}';";
            SqlCommand cmd = new SqlCommand(cmdText, connection);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
               string filterName = (string)reader[tableFilterName];
               int lenght = filterName.Length; //< 10 ? filterName.Length : 10;
               if (filterName.Substring(0, lenght).Contains(name))
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
         for (int i = 1; i <= maxValue + 1; i++)
         {
            if (!IDlist.Contains(i))
            {
               return i;
            }
         }
         return maxValue;
      }


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