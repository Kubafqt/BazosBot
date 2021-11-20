﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BazosBot
{
   class Settings
   {
      public static Size defaultPanelSize = new Size(1000, 549);
      public static Point defaultPanelLocation = new Point(12, 34);
      public static Dictionary<string, string> DictMainPanelsNameValue = new Dictionary<string, string>()
      {
         { "main panel", "panelMain" },
         { "settings", "settingsPanel" },
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
