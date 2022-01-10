using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace BazosBot
{
   class Browsers
   {
      private static bool sixtyFourBitSystem = IntPtr.Size == 8 ? true : false;
      private static string programFiles = sixtyFourBitSystem ? "ProgramFiles" : "ProgramFiles(x86)";
      private static string programFilesPath = Environment.GetEnvironmentVariable(programFiles);

      public static string chromePath = $@"{programFilesPath}\Google\Chrome\Application\chrome.exe";
      public static string firefoxPath = $@"{programFilesPath}\Mozilla Firefox\firefox.exe";
      public static string browserName = File.Exists(chromePath) ? chromePath : File.Exists(firefoxPath) ? firefoxPath : "iexplore";

      public static void StartBrowser(string url)
      {
         Process process = new Process();
         process.StartInfo = new ProcessStartInfo()
         {
            FileName = browserName,
            Arguments = url
         };
         process.Start();
      }


   }
}