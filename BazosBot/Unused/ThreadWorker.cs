using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BazosBot
{
   class ThreadWorker
   {
      //public IEnumerable<int> DownloadAllFromCategory(string url, object state, bool getOnlyNewOffers = false) //from bazos section
      //{
      //   IEnumerable<int> enumerable = Download.DownloadAllFromCategory(url, getOnlyNewOffers);
      //   Action completeAction = (Action)state;
      //   completeAction.Invoke();
      //   return enumerable;
      //}

      public void Run(object state)
      {
         Action completeAction = (Action)state;
         completeAction.Invoke();
      }

   }
   //source: https://stackoverflow.com/questions/1584062/how-can-i-wait-for-a-thread-to-finish-with-net
}