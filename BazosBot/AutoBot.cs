using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.Data.SqlClient;


namespace BazosBot
{
	class AutoBot
	{
		//class variables:
		public static AutoBot actualBot; //autobot in creating
		public static AutoBot LastBot; //foreach category is one bot
		public static List<AutoBot> BotList = new List<AutoBot>();
		public static Queue<AutoBot> BotQueue = new Queue<AutoBot>();
		public static List<AutoBot> SavedBotList = new List<AutoBot>(); //getted from DB - for each category = 1 bot object, for each bot = united name
		public static List<AutoBot> tempBotList = new List<AutoBot>(); //actual creating bots - each for 1 category
		public static bool botRunning = false;
		public static string runningBotName = string.Empty;
		//public static string lastBotName;
		//public static Dictionary<string, List<string>> DictCategoryQuickFilterText = new Dictionary<string, List<string>>();
		//public static Dictionary<string, List<object>> DictNameBotDicts = new Dictionary<string, List<object>>();
		//object variables:
		public string botName { get; set; }
		public string category;
		public List<string> quickFilterTextList = new List<string>();
		public int interval; //seconds
		public int? fullInterval; //every x times full download from section - expensive work
		public int timesUsed;
		public Stopwatch sw = new Stopwatch();
		public bool isRunning;
		public bool stoppedRunning;
		public bool multicategory;

		//random test variables:
		//public List<string> QuickFilterTextList = new List<string>();
		//public List<QuickFilter> QuickFilterList = new List<QuickFilter>();
		//public string category; //kategorie, která uložená je -> quickfiltery k ní na výbìr & možnost nových kategoriíí, možnost nových quickfilterù
		//public Dictionary<string, List<string>> DictCategoryQuickFilters = new Dictionary<string, List<string>>();
		//public Dictionary<string, int> DictCategoryInterval = new Dictionary<string, int>();
		//public Dictionary<string, int?> DictCategoryFullInterval = new Dictionary<string, int?>();
		//public Dictionary<string, int> DictCategoryTimesUsed = new Dictionary<string, int>();
		//public Dictionary<string, Stopwatch> DictCategoryStopWatch = new Dictionary<string, Stopwatch>();

		//single autobot object variables:
		//public Queue<string> CategoryQueue = new Queue<string>();
		//public Dictionary<string, List<QuickFilter>> DictCategoryUrlQuickFilterList = new Dictionary<string, List<QuickFilter>>();
		//public List<string> CategoryUrlList = new List<string>();
		//public DateTime lastRun;
		//public DateTime lastDownloadedTime;
		//public bool sendMail; //later implementation

		public AutoBot(string name, string category, List<string> QuickFilterTextList, int interval, bool multicategory, int? fullInterval = 0)
		{
			this.botName = name;
			this.category = category;
			this.quickFilterTextList = QuickFilterTextList;
			this.interval = interval;
			this.fullInterval = fullInterval;
			this.timesUsed = 0;
			this.isRunning = false;
			this.multicategory = multicategory;
			//if (!DictCategoryQuickFilters.ContainsKey(category)) //test it
			//DictCategoryQuickFilters.Add(category, QuickFilterTextList);
			//DictCategoryInterval[category] = interval;
			//DictCategoryFullInterval[category] = fullInterval;
			//DictCategoryTimesUsed[category] = 0;
			//DictCategoryStopWatch[category] = new Stopwatch();
		}

		/// <summary>
		/// Returns default bot name with next free number in default bot names list.
		/// </summary>
		/// <param name="botName"></param>
		/// <returns></returns>
		public static string defBotName(string botName)
		{
			if (SavedBotList.Any(p => p.botName.Contains(botName.Substring(0, 5)))) //any bot with this name exist in list
			{
				List<AutoBot> speedUpList = SavedBotList.FindAll(p => p.botName.Contains(botName.Substring(0, 5)));
				int maxBotNumber = speedUpList.Max(p => Convert.ToInt32(Regex.Match(p.botName.Substring(0, p.botName.Length), @"\d+").ToString()));
				for (int i = 1; i < maxBotNumber; i++)
				{
					if (!speedUpList.Any(p => Convert.ToInt32(Regex.Match(p.botName.Substring(0, p.botName.Length), @"\d+").ToString()) == i))
					{
						return $"{botName}{i}"; //can be faster than line bellow
						//return botName + i;
					}
				}
				return $"{botName}{maxBotNumber + 1}"; //can be faster than line bellow
				//return botName + (maxBotNumber + 1);
			}
			return $"{botName}1";
		}


		/// <summary>
		/// 
		/// </summary>
		public static void SaveBotToDB(bool multicategory, string lastBotName = "", string botName = "")
      {
			List<AutoBot> botListDB = lastBotName != string.Empty ? GetBotsFromDB(lastBotName) : new List<AutoBot>(); //;.FindAll(p => tem);
			List<string> tempBotListCategories = tempBotList.Select(p => p.category).ToList();//tempBotList.ToList(p => p.category);
			List<string> DBBotListCategories = botListDB.Select(p => p.category).ToList();
			List<string> deletedCategories = DBBotListCategories.FindAll(p => !tempBotListCategories.Contains(p));
			foreach (AutoBot bot in tempBotList)
			{
				string quickFilterTextsToSplit = string.Empty;
				foreach (string qfText in bot.quickFilterTextList)
				{
					quickFilterTextsToSplit = quickFilterTextsToSplit == string.Empty ? qfText : $"{quickFilterTextsToSplit},{qfText}";
				}
				int mCategory = multicategory ? 1 : 0;
				botName = botName == string.Empty ? bot.botName : botName;
				lastBotName = lastBotName == string.Empty ? botName : lastBotName;
				bool botNameCategoryExist = BotNameAndCategoryExistInDB(botName, bot.category); //update saved bot with new bot
				string cmd = !botListDB.Any(p => p.category == bot.category) && !botNameCategoryExist /*update bot category*/ ? $"INSERT INTO BazosAutobot VALUES('{botName}', '{bot.category}', '{quickFilterTextsToSplit}', '{bot.interval}', '{bot.fullInterval}', '{mCategory}');" : !botNameCategoryExist ?
					$"UPDATE BazosAutobot SET Name = '{botName}', QuickFilterList = '{quickFilterTextsToSplit}', CategoryURL = '{bot.category}', Interval = '{bot.interval}', FullInterval = '{bot.fullInterval}' WHERE Name = '{lastBotName}' AND CategoryURL = '{bot.category}';" :
					$"UPDATE BazosAutobot SET Name = '{botName}', QuickFilterList = '{quickFilterTextsToSplit}', CategoryURL = '{bot.category}', Interval = '{bot.interval}', FullInterval = '{bot.fullInterval}' WHERE Name = '{botName}' AND CategoryURL = '{bot.category}';";
				cmd = botNameCategoryExist && lastBotName != botName ? $"{cmd} DELETE FROM BazosAutoBot WHERE Name = '{lastBotName}' AND CategoryURL = '{bot.category}';" : cmd;
            foreach (string category in deletedCategories)
            {
					string command = BotNameAndCategoryExistInDB(botName) ? $"DELETE FROM BazosAutobot WHERE CategoryURL = '{category}' AND Name = '{botName}';" : $"DELETE FROM BazosAutobot WHERE CategoryURL = '{category}' AND Name = '{lastBotName}';";
					DB_Access.ExecuteNonQuery(command);
            }
				DB_Access.ExecuteNonQuery(cmd);
			}
			GetBotNamesFromDB();
		}

		/// <summary>
		/// 
		/// </summary>´{bot
		/// <returns></returns>
		private static bool BotNameAndCategoryExistInDB(string botName, string categoryUrl = "")
		{ 
			SqlConnection conn = new SqlConnection(Settings.DBconnString);
			string cmdText = categoryUrl != string.Empty ? $"SELECT * FROM BazosAutobot WHERE Name = '{botName}' AND CategoryURL = '{categoryUrl}';" : $"SELECT * FROM BazosAutobot WHERE Name = '{botName}'";
			SqlCommand cmd = new SqlCommand(cmdText, conn);
			conn.Open();
			SqlDataReader reader = cmd.ExecuteReader();
			while (reader.Read())
			{
				conn.Close();
				return true;
			}
			conn.Close();
			return false;
		}

		/// <summary>
		/// 
		/// </summary>
		public static void DeleteBotFromDB(string botName)
      {
         string cmd = $"DELETE FROM BazosAutobot WHERE Name='{botName}';";
         DB_Access.ExecuteNonQuery(cmd);
      }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="botName"></param>
		//public static void SaveBotToDB(string botName)
		//{
		//   string quickFilterTextsToSplit = string.Empty;
		//   foreach (string qfText in bot.quickFilterTextList)
		//   {
		//      quickFilterTextsToSplit = quickFilterTextsToSplit == string.Empty ? quickFilterTextsToSplit : $",{quickFilterTextsToSplit}";
		//   }
		//   string cmd = $"INSERT INTO BazosAutobot VALUES('{bot.botName}', '{bot.category}', '{quickFilterTextsToSplit}', '{bot.interval}', '{bot.fullInterval}')";
		//   DB_Access.ExecuteNonQuery(cmd);
		//}


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public static List<AutoBot> GetBotsFromDB(string lastBotName)
      {
			List<AutoBot> listBotDB = new List<AutoBot>();
			SqlConnection conn = new SqlConnection(Settings.DBconnString);
			string cmdText = $"SELECT * FROM BazosAutobot WHERE Name = '{lastBotName}';";
			SqlCommand cmd = new SqlCommand(cmdText, conn);
			conn.Open();
			SqlDataReader reader = cmd.ExecuteReader();
			while (reader.Read())
			{
				string qfTextToSplit = (string)reader["QuickFilterList"];
				List<string> quickFilterTextList = qfTextToSplit.Split(",").ToList();
				listBotDB.Add(new AutoBot((string)reader["Name"], (string)reader["CategoryURL"], quickFilterTextList, (int)reader["Interval"], (bool)reader["MultiCategory"], (int)reader["FullInterval"]));
			}
			conn.Close();
			return listBotDB;
		}

      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      public static List<string> GetBotNamesFromDB()
		{
			SavedBotList.Clear();
			List<string> listSavedBotNames = new List<string>();
			SqlConnection conn = new SqlConnection(Settings.DBconnString);
			string cmdText = $"SELECT * FROM BazosAutobot;";
			SqlCommand cmd = new SqlCommand(cmdText, conn);
			conn.Open();
			SqlDataReader reader = cmd.ExecuteReader();
			while (reader.Read())
			{
				string test = (string)reader["QuickFilterList"];
				List<string> quickFilterTextList = test.Split(",").ToList();
				quickFilterTextList.Remove("");
				string name = (string)reader["Name"];
				SavedBotList.Add(new AutoBot(name, (string)reader["CategoryURL"], quickFilterTextList, (int)reader["Interval"], (bool)reader["MultiCategory"], (int)reader["FullInterval"]));
				if (!listSavedBotNames.Contains(name))
            {
					listSavedBotNames.Add(name);
            }
			}
			conn.Close();
			//cmdText = $"SELECT DISTINCT Name FROM BazosAutobot;";
			//cmd = new SqlCommand(cmdText, conn);
			//conn.Open();
			//reader = cmd.ExecuteReader();
			//while (reader.Read())
			//{
			//	listSavedBotNames.Add((string)reader["Name"];
			//}
			//conn.Close();
			return listSavedBotNames;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public static List<string> ListActualMultiCategoryInDB(string categoryUrl)
      {
         //List<string> listOfferCategoryURL = new List<string>();
         //string cmdText = $"SELECT DISTINCT CategoryNameUrlID FROM BazosOffers;";
         //SqlCommand cmd = new SqlCommand(cmdText, conn);
         //conn.Open();
         //SqlDataReader reader = cmd.ExecuteReader();
         //while (reader.Read())
         //{
         //   listOfferCategoryURL.Add((string)reader["CategoryNameUrlID"]);
         //}
         //conn.Close();
         //return listOfferCategoryURL;

         SavedBotList.Clear();
         List<string> listSavedBotNames = new List<string>();
         SqlConnection conn = new SqlConnection(Settings.DBconnString);
         string cmdText = $"SELECT * FROM BazosAutobot;";
         SqlCommand cmd = new SqlCommand(cmdText, conn);
         conn.Open();
         SqlDataReader reader = cmd.ExecuteReader();
         while (reader.Read())
         {
            string test = (string)reader["QuickFilterList"];
            List<string> quickFilterTextList = test.Split(",").ToList();
            quickFilterTextList.Remove("");
            string name = (string)reader["Name"];
            SavedBotList.Add(new AutoBot(name, (string)reader["CategoryURL"], quickFilterTextList, (int)reader["Interval"], (bool)reader["MultiCategory"], (int)reader["FullInterval"]));
            if (!listSavedBotNames.Contains(name))
            {
               listSavedBotNames.Add(name);
            }
         }
         conn.Close();
         return new List<string>(); 
		}

	}
}