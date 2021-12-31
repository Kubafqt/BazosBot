using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace BazosBot
{
   class Settings
   {
      public static int downLimit = 25; //limit of html source download to not overload the server
      public static Point downLimitMinMax = new Point(1500, 2500);
      public static string[] proxyList = new string[] { "190.15.200.31:8080" };
      public static readonly string DBconnString = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\..\"))}BazosDb.mdf;Integrated Security = True; Connect Timeout = 30";
      public static bool selectBotAfterCreateOrEdit = true;

      public static Dictionary<string, Size> FormSize = new Dictionary<string, Size>()
      {
         { "default", new Size(1064, 711) },
         { "botted", new Size(1064, 761) }
      };
      public static Dictionary<string, Point> panelLocation = new Dictionary<string, Point>()
      {
         { "default", new Point(17, 48) },
         { "botted", new Point(17, 88) },
      };
      public static void MainPanelLocation(string type, Control.ControlCollection Controls)
      {
         foreach (Control item in Controls.OfType<Panel>().Where(p => p.Tag == "mainPanels"))
         {
            item.Location = panelLocation[type];
         }
      }
      public static Size defaultPanelSize = new Size(1015, 643);//(1000, 549);
      public static Point defaultPanelLocation = new Point(12, 34);
      public static Dictionary<string, string> DictMainPanelsNameValue = new Dictionary<string, string>()
      {
         { "main panel", "panelMain" },
         { "auto bot", "panelAutoBot" },
         //{ "settings", "settingsPanel" },
      };

      public static Point filterPanelLocation = new Point(10, 39);
      public static Size filterPanelSize = new Size(838, 495);
      public static Dictionary<string, string> DictFilterPanelsNames = new Dictionary<string, string>()
      {
         { "Filter Set", "filterSetPanel" },
         { "Filter Panel", "filterPanel" },
         { "Blacklist Panel", "blacklistPanel" }
      };

   }
}