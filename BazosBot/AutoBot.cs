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

namespace BazosBot
{
	class AutoBot
	{
		public static List<AutoBot> AutoBotList = new List<AutoBot>();
		public static Queue<AutoBot> AutoBotQueue = new Queue<AutoBot>();
		public static AutoBot LastAutoBot;
		//public Queue<string> CategoryQueue = new Queue<string>();
		//public Dictionary<string, List<QuickFilter>> DictCategoryUrlQuickFilterList = new Dictionary<string, List<QuickFilter>>();
		//public List<string> CategoryUrlList = new List<string>();
		public List<QuickFilter> QuickFilterList = new List<QuickFilter>();
		public string category; //kategorie, která uložená je -> quickfiltery k ní na výbìr & možnost nových kategoriíí, možnost nových quickfilterù
		public bool isRunning;
		//interval work:
		public int interval; //seconds
		public int fullInterval; //every x times - expensive work
		public int timesUsed;
		public Stopwatch sw;
	   //public DateTime lastRun;
		//public DateTime lastDownloadedTime;
		//public bool sendMail; //later implementation
		public AutoBot(string category, List<QuickFilter> QuickFilterList, int interval, int fullInterval)
		{
			sw = new Stopwatch();
			timesUsed = 0;
			this.category = category;
			this.QuickFilterList = QuickFilterList;
			this.interval = interval;
			this.fullInterval = fullInterval;
		}


	}
}