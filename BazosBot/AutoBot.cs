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
		public static AutoBot LastBot;
		public static List<AutoBot> BotList = new List<AutoBot>();
		public static Queue<AutoBot> BotQueue = new Queue<AutoBot>();
		public static List<AutoBot> SavedBotList = new List<AutoBot>();
		//object variables:
		public List<string> QuickFilterTextList = new List<string>();
		public List<QuickFilter> QuickFilterList = new List<QuickFilter>();
		public string name;
		public string category; //kategorie, která uložená je -> quickfiltery k ní na výbìr & možnost nových kategoriíí, možnost nových quickfilterù
		public int interval; //seconds
		public int? fullInterval; //every x times - expensive work
		public int timesUsed;
		public Stopwatch sw;
		public bool isRunning;

		//public Queue<string> CategoryQueue = new Queue<string>();
		//public Dictionary<string, List<QuickFilter>> DictCategoryUrlQuickFilterList = new Dictionary<string, List<QuickFilter>>();
		//public List<string> CategoryUrlList = new List<string>();
		//public DateTime lastRun;
		//public DateTime lastDownloadedTime;
		//public bool sendMail; //later implementation

		public AutoBot(string name, string category, List<string> QuickFilterTextList, int interval, int? fullInterval = 0)
		{
			sw = new Stopwatch();
			timesUsed = 0;
			this.name = name;
			this.category = category;
			this.QuickFilterTextList = QuickFilterTextList;
			this.interval = interval;
			this.fullInterval = fullInterval;
			this.isRunning = false;
		}

      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      public static List<string> GetBotNamesFromDB()
      {
         List<string> listSavedBotNames = new List<string>();
         SqlConnection conn = new SqlConnection(Settings.DBconnString);
         string cmdText = $"SELECT * FROM BazosAutobot;";
         SqlCommand cmd = new SqlCommand(cmdText, conn);
         conn.Open();
         SqlDataReader reader = cmd.ExecuteReader();
         while (reader.Read())
         {
				string test = (string)reader["QuickFilterList"];
				List<string> testQuickList = test.Split(";").ToList();
				string name = (string)reader["Name"];
				SavedBotList.Add(new AutoBot(name, (string)reader["CategoryURL"], testQuickList, (int)reader["Interval"], (int)reader["FullInterval"]));
				listSavedBotNames.Add(name);

			}
         conn.Close();
         return listSavedBotNames;
      }
   }
}