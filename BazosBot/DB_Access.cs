using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

namespace BazosBot
{
   class DB_Access
   {
      static List<BazosOffers> ActOffersList = new List<BazosOffers>();
      static readonly string connString = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={Application.StartupPath}Database.mdf;Integrated Security = True; Connect Timeout = 30";
      /// <summary>
      /// 
      /// </summary>
      /// <param name="urlNameID"></param>
      public static void InsertNewOffers(string urlNameID)
      {
         SqlConnection connection = new SqlConnection(connString);
         foreach (BazosOffers item in BazosOffers.ListBazosOffers.ToList()) //aktual downloaded
         {
            string[] updateCmdTexts = new string[]
            {
               $"UPDATE BazosOffers SET Nadpis = {item.nadpis} WHERE URL = {item.url} AND Nadpis != {item.nadpis};",
               $"UPDATE BazosOffers SET Cena = {item.cena} WHERE URL = {item.url} AND Cena != {item.cena};",
               $"UPDATE BazosOffers SET Popis = {item.popis} WHERE URL = {item.url} AND Popis != {item.popis};",
               $"UPDATE BazosOffers SET Datum = {item.datum} WHERE URL = {item.url} AND Datum != {item.datum};",
               $"UPDATE BazosOffers SET Lokace = {item.lokace}, PSC = {item.psc} WHERE URL = {item.url} AND Lokace != {item.lokace};",
               $"UPDATE BazosOffers SET Viewed = {item.viewed} WHERE URL = {item.url} AND Viewed != {item.viewed};",
            };
            foreach (string cmdText in updateCmdTexts)
            {
               SendCommand(connection, cmdText);
            }
            string cmd = $"INSERT INTO BazosOffers (Nadpis, Popis, Datum, Cena, Lokace, PSC, Viewed, URL, CategoryNameUrlID) VALUES({item.nadpis}, {item.popis}, {item.datum}, {item.cena}, {item.lokace}, {item.psc}, {item.viewed}, {item.url}, {BazosOffers.actualCategoryNameURL}) WHERE NOT EXISTS (SELECT Nadpis FROM BazosOffers WHERE URL = {item.url});";
            SqlCommand commandInsertNotExist = new SqlCommand(cmd, connection);
            connection.Open(); //open SQL server connection
            commandInsertNotExist.ExecuteNonQuery();
            connection.Close();

            CheckDeletedOffers(connection);
         }
      }

      /// <summary>
      /// 
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
      /// Check if offer is to delete
      /// </summary>
      /// <param name="connection"></param>
      private static void CheckDeletedOffers(SqlConnection connection)
      {
         //string selectCmdText = "SELECT * FROM BazosOffers";
         //SqlCommand selectCommand = new SqlCommand(selectCmdText, connection);
         //connection.Open();
         //SqlDataReader reader = selectCommand.ExecuteReader();
         //while (reader.Read())
         //{
         //   string categoryNameURL = (string)reader["CategoryNameUrlID"];
         //   string url = (string)reader["url"];
         //   if (categoryNameURL == BazosOffers.actualCategoryNameURL && BazosOffers.ListBazosOffers.Where(p => p.url == url).ToString() != string.Empty) //Offer url in db is not contained in actual offers list
         //   {
         //      UpdateDeletedOffers(url, connection);
         //   }
         //}
         //connection.Close();
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
         //SqlConnection connection = new SqlConnection(connString);
         //string cmdText = !NameOfSetExist(NameOfSet) ? $"INSERT INTO BazosFilterSet (NAME_OF_SET, URL_PAGE, NAME, MAX_CENA) VALUES (@NameOfSet, @Name, @UrlPage, @MaxCena;" : $"UPDATE BazosFilterSet SET URL_PAGE = @UrlPage, NAME = @Name, MAX_CENA = @MaxCena WHERE NAME_OF_SET = @NameOfSet;";
         //SqlCommand cmd = new SqlCommand(cmdText, connection);
         //cmd.Parameters.AddWithValue("@NameOfSet", NameOfSet);
         //cmd.Parameters.AddWithValue("@Name", Name);
         //cmd.Parameters.AddWithValue("@UrlPage", UrlPage);
         //cmd.Parameters.AddWithValue("@MaxCena", MaxCena);
         //connection.Open();
         //cmd.ExecuteNonQuery();
         //connection.Close();
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="NameOfSet"></param>
      /// <returns></returns>
      private static bool NameOfSetExist(string NameOfSet)
      {
         SqlConnection connection = new SqlConnection(connString);
         string selectCmdText = $"SELECT * FROM BazosFilterSet WHERE NAME_OF_SET = @NameOfSet";
         SqlCommand cmd = new SqlCommand(selectCmdText, connection);
         connection.Open();
         SqlDataReader reader = cmd.ExecuteReader();
         cmd.Parameters.AddWithValue("@NameOfSet", NameOfSet);
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
      public static void CreateFilterSet(string filterSetName, List<string> listboxFilterItems, List<string> listboxBlacklistItems)
      {
         //SqlConnection connection = new SqlConnection(connString);
         //string itemSet = string.Empty;
         //string blacklistItems = string.Empty;
         //foreach (var item in listboxFilterItems)
         //{
         //   itemSet += $"{item};";
         //}
         //foreach (var item in listboxBlacklistItems)
         //{
         //   blacklistItems += $"{item};";
         //}
         //string cmdText = !FilterSetExist(filterSetName) ? $"INSERT INTO BazosFilterSet VALUES (@filterSetName, {itemSet}, {blacklistItems};" : $"UPDATE BazosFilterSet SET SetItems = {itemSet}, BlacklistItems = {blacklistItems} WHERE SetName = {filterSetName};";
         //SqlCommand cmd = new SqlCommand(cmdText, connection);
         //cmd.Parameters.AddWithValue("@filterSetName", filterSetName);
         //connection.Open();
         //cmd.ExecuteNonQuery();
         //connection.Close();
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

      //public static void LoadFilterSetToBoxes(string nameOfSet, out string[] setItems, out string[] blacklistItems)
      //{
      //   //SqlConnection connection = new SqlConnection(connString);
      //   //string cmdText = $"SELECT * FROM BazosFilterSet WHERE setName = {nameOfSet}";
      //   //SqlCommand cmd = new SqlCommand(cmdText, connection);
      //   //connection.Open();
      //   //SqlDataReader reader = cmd.ExecuteReader();
      //   //string setItemsString = string.Empty;
      //   //string blacklistItemsString = string.Empty;
      //   //while (reader.Read()) //load level info
      //   //{
      //   //   setItemsString = (string)reader["SetItems"];
      //   //   blacklistItemsString = (string)reader["BlacklistItems"];
      //   //}
      //   //connection.Close();
      //   //setItems = setItemsString.Split(';');
      //   //blacklistItems = blacklistItemsString.Split(';');
      //}

      //public static void LoadFilterSetToBoxes(string urlPage, out string[] setItems, out string[] blacklistItems, out string setName)
      //{
      //   SqlConnection connection = new SqlConnection(connString);
      //   string cmdText = $"SELECT * FROM BazosFilterSet WHERE URL_PAGE = {urlPage}";
      //   SqlCommand cmd = new SqlCommand(cmdText, connection);
      //   connection.Open();
      //   SqlDataReader reader = cmd.ExecuteReader();
      //   string setItemsString = string.Empty;
      //   string blacklistItemsString = string.Empty;
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
   }
}