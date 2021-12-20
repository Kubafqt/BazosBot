using System.Linq;
using System.Text;
using System.Globalization;

namespace BazosBot
{
   class TextAdjust
   {
      /// <summary>
      /// 
      /// </summary>
      /// <param name="text"></param>
      /// <returns></returns>
      public static string RemoveDiacritics(string text)
      {
         if (string.IsNullOrWhiteSpace(text))
         { return text; }

         text = text.Normalize(NormalizationForm.FormD);
         var chars = text.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray();
         return new string(chars).Normalize(NormalizationForm.FormC);
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="text"></param>
      /// <returns></returns>
      public static string RemoveSemicolons(string text)
      {
         if (string.IsNullOrWhiteSpace(text))
         { return text; }

         text = text.Replace(";", string.Empty);
         return text;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="text"></param>
      /// <returns></returns>
      public static string RemoveSpecialCharacters(string text)
      {
         if (string.IsNullOrWhiteSpace(text))
         {
            return text;
         }
         //+more characters, ... ;
         text = text.Replace("'", string.Empty);
         return text;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="text"></param>
      /// <returns></returns>
      public static string PrepareToCommand(string text)
      {
         if (string.IsNullOrWhiteSpace(text))
         {
            return text;
         }
         text = RemoveSemicolons(text);
         text = RemoveSpecialCharacters(text);
         return text;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="text"></param>
      /// <returns></returns>
      public static string RemoveSemicolonsAndDiacritics(string text)
      {
         if (string.IsNullOrWhiteSpace(text))
         { return text; }

         text = text.Replace(";", string.Empty);
         text = text.Normalize(NormalizationForm.FormD);
         var chars = text.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray();
         return text;
      }
   }
}
