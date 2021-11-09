using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace BazosBot
{
   class Sleeping
   {
      static Random random = new Random();
      public static void MicroSleep(int min = 10, int max = 20)
      {
         Thread.Sleep(random.Next(min, max));
      }

   }
}
